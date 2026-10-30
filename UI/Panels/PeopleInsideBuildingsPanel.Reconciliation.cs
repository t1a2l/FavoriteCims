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
                residentialPanelTexts[householdCount].text = FavCimsLang.Text("OnBuildingResidential") + " " + (householdCount + 1);

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

            int workersUsed = BindBuildingSection(snapshot.Employees, workerRows, 0, Citizen.Location.Work, currentOccupants);
            ResetBuildingRowsFrom(workerRows, workersUsed);

            int guestsUsed = BindBuildingSection(snapshot.Visitors, guestRows, 0, Citizen.Location.Visit, currentOccupants);
            guestsUsed = BindBuildingSection(snapshot.Students, guestRows, guestsUsed, Citizen.Location.Work, currentOccupants);
            ResetBuildingRowsFrom(guestRows, guestsUsed);
            guestCollapse.SetRows(TakeRows(guestRows, guestsUsed));

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
                hotelRoomTexts[i].text = FavCimsLang.Text("OnBuildingHotelRooms") + " " + (i + 1);
            }

            UpdateGroupVisibility(snapshot, householdCount, hotelUsed);
            RemoveFormerOccupants(currentOccupants);
            WorkersCount = snapshot.PresentEmployeeCount;
            GuestsCount = snapshot.PresentVisitorCount;
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
            workersPanel.isVisible = snapshot.HasEmployees;
            workersPanelIcon.normalFgSprite = "WorkersIcon";
            workersPanelText.text = FavCimsLang.Text(snapshot.HasEmployees ? "OnBuildingWorkers" : "OnBuildingNoWorkers");
            guestsPanel.isVisible = snapshot.HasVisitors || snapshot.HasStudents;
            UpdateGuestHeader(snapshot);
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