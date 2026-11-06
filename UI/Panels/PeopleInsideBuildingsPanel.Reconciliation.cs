using System.Collections.Generic;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class PeopleInsideBuildingsPanel
    {
        private void RefreshFromSnapshot(BuildingOccupantsSnapshot snapshot)
        {
            var currentOccupants = new HashSet<uint>();
            titleBuildingName.text = snapshot.Title;
            int householdCount = 0;

            foreach (var household in snapshot.ResidentGroups)
            {
                if (householdCount >= MaxHouseHolds) break;
                if (household.Members.Count == 0) continue;

                int baseRow = householdCount * CitizensPerUnit;

                residentialPanels[householdCount].Show();
                residentialPanelIcons[householdCount].normalFgSprite = "HomeIcon";
                residentialPanelTexts[householdCount].text = FavCimsLang.Text("BuildingHouseholdTitle") + " " + (householdCount + 1);

                var householdRows = new List<UIComponent>();

                for (int slot = 0; slot < CitizensPerUnit; slot++)
                {
                    var row = residentialRows[baseRow + slot];
                    if (slot >= household.Members.Count || household.Members[slot].CitizenId == 0 || currentOccupants.Contains(household.Members[slot].CitizenId))
                    {
                        if (row != null) ResetBuildingRow(row);
                        continue;
                    }
                    AssignBuildingRow(row, household.Members[slot].CitizenId, Citizen.Location.Home, currentOccupants);
                    householdRows.Add(row);
                }

                householdCollapses[householdCount].SetRows(householdRows);

                householdCount++;
            }

            ResetBuildingRowsFrom(residentialRows, householdCount * CitizensPerUnit);

            int workersUsed = BindBuildingSection(snapshot.Workers, workerRows, 0, Citizen.Location.Work, currentOccupants);
            ResetBuildingRowsFrom(workerRows, workersUsed);

            int studentsUsed = BindBuildingSection(snapshot.Students, studentRows, 0, Citizen.Location.Work, currentOccupants);
            ResetBuildingRowsFrom(studentRows, studentsUsed);

            int visitorsUsed = BindBuildingSection(snapshot.Visitors, visitorRows, 0, Citizen.Location.Visit, currentOccupants);
            ResetBuildingRowsFrom(visitorRows, visitorsUsed);

            int hotelUsed = BindBuildingSection(snapshot.HotelGuests, hotelRoomRows, 0, Citizen.Location.Hotel, currentOccupants);
            ResetBuildingRowsFrom(hotelRoomRows, hotelUsed);

            for (int i = 0; i < hotelUsed; i++)
            {
                int baseRow = i * CitizensPerUnit;

                hotelRoomCollapses[i].SetRows(TakeRows(hotelRoomRows, baseRow, Mathf.Min(CitizensPerUnit, hotelUsed - baseRow)));
            }

            for (int i = 0; i < hotelUsed; i++)
            {
                hotelRoomIcons[i].normalFgSprite = "HomeIcon";
                hotelRoomTexts[i].text = FavCimsLang.Text("BuildingHotelRoomTitle") + " " + (i + 1);
            }

            UpdateGroupVisibility(snapshot, householdCount, hotelUsed);
            RemoveFormerOccupants(currentOccupants);

            // Apply collapse visibility only after binding and cleanup.
            for (int i = 0; i < householdCollapses.Count; i++)
            {
                if (i >= householdCount)
                {
                    householdCollapses[i].ClearRows(raiseEvent: false);
                    continue;
                }

                var rows = new List<UIComponent>();

                int baseRow = i * CitizensPerUnit;

                for (int slot = 0; slot < CitizensPerUnit; slot++)
                {
                    var row = residentialRows[baseRow + slot];

                    if (row != null && row.citizen != 0 && row.OnBuilding == BuildingID.Building)
                    {
                        rows.Add(row);
                    }
                }

                householdCollapses[i].SetRows(rows, raiseEvent: false);
            }

            visitorCollapse.SetRows(TakeRows(visitorRows, visitorsUsed),  raiseEvent: false);

            for (int i = 0; i < hotelRoomCollapses.Count; i++)
            {
                if (i < hotelUsed)
                {
                    hotelRoomCollapses[i].SetRows([hotelRoomRows[i]], raiseEvent: false);
                }
                else
                {
                    hotelRoomCollapses[i].ClearRows(raiseEvent: false);
                }
            }

            WorkersCount = snapshot.PresentWorkerCount;
            StudentsCount = snapshot.PresentStudentCount;
            VisitorsCount = snapshot.PresentVisitorCount;
            HotelGuestsCount = snapshot.RegisteredHotelGuestCount;
        }

        private int BindBuildingSection<TRow>(List<BuildingOccupant> occupants, TRow[] rows, int firstRow, Citizen.Location location, HashSet<uint> currentOccupants) where TRow : ResidentialBuildingPanelRow
        {
            int used = firstRow;
            foreach (var occupant in occupants)
            {
                if (used >= rows.Length) break;
                if (occupant.CitizenId == 0 || currentOccupants.Contains(occupant.CitizenId)) continue;
                if (rows[used] == null) continue;
                AssignBuildingRow(rows[used], occupant.CitizenId, location, currentOccupants);
                used++;
            }
            return used;
        }

        private void ResetBuildingRowsFrom<TRow>(TRow[] rows, int firstUnused) where TRow : ResidentialBuildingPanelRow
        {
            for (int i = firstUnused; i < rows.Length; i++)
            {
                var row = rows[i];
                if (row != null && (row.citizen != 0 || row.isVisible)) ResetBuildingRow(row);
            }
        }

        private void UpdateGroupVisibility(BuildingOccupantsSnapshot snapshot, int householdCount, int hotelRowCount)
        {
            for (int i = 0; i < MaxHouseHolds; i++)
            { 
                residentialPanels[i].isVisible = i < householdCount; 
            }

            workersPanel.isVisible = snapshot.HasWorkers;
            workersPanelIcon.normalFgSprite = "WorkersIcon";
            workersPanelText.text = FavCimsLang.Text(snapshot.HasWorkers ? "OnBuildingWorkers" : "OnBuildingNoWorkers");

            studentsPanel.isVisible = snapshot.HasStudents;
            UpdateStudentHeader(snapshot);

            visitorsPanel.isVisible = snapshot.HasVisitors;
            UpdateVisitorHeader(snapshot);

            for (int i = 0; i < MaxHotelRooms; i++)
            { 
                hotelRoomPanels[i].isVisible = i < hotelRowCount; 
            }
        }

        private static IEnumerable<UIComponent> TakeRows(UIComponent[] rows, int count)
        {
            for (int i = 0; i < count && i < rows.Length; i++)
            {
                yield return rows[i];
            }
        }

        private static IEnumerable<UIComponent> TakeRows(UIComponent[] rows, int startIndex, int count)
        {
            int end = Mathf.Min(rows.Length, startIndex + count);

            for (int i = startIndex; i < end; i++)
            {
                yield return rows[i];
            }
        }
    }
}