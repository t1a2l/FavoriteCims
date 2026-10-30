using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class PeopleInsideBuildingsPanel : UIPanel
    {
        private const float RefreshInterval = 0.5f;
        private const int MaxHouseHolds = 40;
        private const int MaxWorkersUnits = 20;
        private const int MaxGuestsUnits = 20;
        private const int MaxHotelRooms = 40;
        private const int CitizensPerUnit = 5;

        private float refreshTimer = RefreshInterval;

        private bool hasActiveBuilding;

        public bool Wait
        {
            get => rowContext.IsUpdating;
            set => rowContext.IsUpdating = value;
        }

        public InstanceID BuildingID;

        public UIPanel RefPanel;

        public Dictionary<uint, uint> CimsOnBuilding => rowContext.Citizens;

        public int WorkersCount;
        public int GuestsCount;
        public int HotelGuestsCount;

        private readonly BuildingManager buildingManager = Singleton<BuildingManager>.instance;

        private readonly UIPanel[] residentialPanels = new UIPanel[MaxHouseHolds];
        private readonly UIButton[] residentialPanelIcons = new UIButton[MaxHouseHolds];
        private readonly UIButton[] residentialPanelTexts = new UIButton[MaxHouseHolds];
        private readonly ResidentialBuildingPanelRow[] residentialRows = new ResidentialBuildingPanelRow[MaxHouseHolds * CitizensPerUnit];

        private UIPanel workersPanel;
        private UIButton workersPanelIcon;
        private UIButton workersPanelText;
        private readonly WorkersBuildingPanelRow[] workerRows = new WorkersBuildingPanelRow[MaxWorkersUnits * CitizensPerUnit];

        private UIPanel guestsPanel;
        private UIButton guestsPanelIcon;
        private UIButton guestsPanelText;
        private readonly GuestsBuildingPanelRow[] guestRows = new GuestsBuildingPanelRow[MaxGuestsUnits * CitizensPerUnit];

        private readonly UIPanel[] hotelRoomPanels = new UIPanel[MaxHotelRooms];
        private readonly UIButton[] hotelRoomIcons = new UIButton[MaxHotelRooms];
        private readonly UIButton[] hotelRoomTexts = new UIButton[MaxHotelRooms];
        private readonly HotelBuildingPanelRow[] hotelRoomRows = new HotelBuildingPanelRow[MaxHotelRooms];

        private UIPanel title;
        private UITextureSprite titleBackground;
        private UIButton titleBuildingName;

        private UIPanel body;
        private UITextureSprite bodyBackground;
        private UIScrollablePanel bodyRows;

        private UIPanel footer;
        private UITextureSprite footerBackground;

        private UIScrollablePanel scrollPanel;
        private UIScrollbar scrollbar;
        private UISlicedSprite scrollbarTrack;
        private UISlicedSprite scrollbarThumb;

        private UIComponent escButton;
        private MouseEventHandler escClickHandler;

        private readonly CompactCitizenRowContext rowContext = new();

        private readonly List<Action> collapseChangedHandlers = [];

        private readonly List<EmployeeRowPlacement> employeePlacements = [];

        private EmployeeSectionsLayout employeeSections;
        private readonly List<BuildingSectionCollapse> householdCollapses = [];
        private BuildingSectionCollapse guestCollapse;
        private readonly List<BuildingSectionCollapse> hotelRoomCollapses = [];

        public override void Awake()
        {
            try
            {
                base.Awake();

                width = 250f;
                height = 0f;
                name = "FavCimsPeopleInsideBuildingsPanel";
                absolutePosition = Vector3.zero;
                Hide();

                CreateTitle();
                CreateBody();
                CreateResidentialSection();
                CreateWorkerSection();
                CreateGuestSection();
                CreateHotelSection();
                CreateScrollbar();
                CreateFooter();

                employeeSections = new EmployeeSectionsLayout(bodyRows, workersPanel, workersPanelText);

                Action employeeHandler = () => MeasureVisibleBodyContentHeight();
                collapseChangedHandlers.Add(employeeHandler);
                employeeSections.SectionsChanged += employeeHandler;

                for (int i = 0; i < residentialPanels.Length; i++)
                {
                    var collapse = new BuildingSectionCollapse(residentialPanels[i], "ResidentialCollapse" + i, residentialPanelTexts[i]);

                    Action residentialHandler = () => MeasureVisibleBodyContentHeight();

                    collapseChangedHandlers.Add(residentialHandler);
                    collapse.StateChanged += residentialHandler;
                    householdCollapses.Add(collapse);
                }

                guestCollapse = new BuildingSectionCollapse(guestsPanel, "GuestsCollapse", guestsPanelText);

                Action guestHandler = () => MeasureVisibleBodyContentHeight();
                collapseChangedHandlers.Add(guestHandler);
                guestCollapse.StateChanged += guestHandler;

                for (int i = 0; i < hotelRoomPanels.Length; i++)
                {
                    var collapse = new BuildingSectionCollapse(hotelRoomPanels[i], "HotelRoomCollapse" + i, hotelRoomTexts[i]);

                    Action hotelHandler = () => MeasureVisibleBodyContentHeight();

                    collapseChangedHandlers.Add(hotelHandler);
                    collapse.StateChanged += hotelHandler;
                    hotelRoomCollapses.Add(collapse);
                }

                SubscribeEscHandler();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Building Panel Awake(): " + ex);
            }
        }

        private void CreateTitle()
        {
            title = AddUIComponent<UIPanel>();
            title.name = "PeopleInsideBuildingsPanelTitle";
            title.width = width;
            title.height = 41f;
            title.relativePosition = Vector3.zero;

            titleBackground = title.AddUIComponent<UITextureSprite>();
            titleBackground.name = "PeopleInsideBuildingsPanelTitleBG";
            titleBackground.width = title.width;
            titleBackground.height = title.height;
            titleBackground.texture = TextureDB.VehiclePanelTitleBackground;
            titleBackground.relativePosition = Vector3.zero;

            titleBuildingName = title.AddUIComponent<UIButton>();
            titleBuildingName.name = "PeopleInsideBuildingsPanelName";
            titleBuildingName.width = title.width;
            titleBuildingName.height = title.height;
            titleBuildingName.textVerticalAlignment = UIVerticalAlignment.Middle;
            titleBuildingName.textHorizontalAlignment = UIHorizontalAlignment.Center;
            titleBuildingName.playAudioEvents = false;
            titleBuildingName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            titleBuildingName.font.size = 15;
            titleBuildingName.textScale = 1f;
            titleBuildingName.wordWrap = true;
            titleBuildingName.textPadding = new RectOffset(5, 5, 0, 0);
            titleBuildingName.textColor = new Color32(204, 204, 51, 40);
            titleBuildingName.useDropShadow = true;
            titleBuildingName.dropShadowOffset = new Vector2(1f, -1f);
            titleBuildingName.dropShadowColor = new Color32(0, 0, 0, 0);
            titleBuildingName.relativePosition = Vector3.zero;
        }

        private void CreateBody()
        {
            body = AddUIComponent<UIPanel>();
            body.name = "PeopleInsideBuildingsBody";
            body.width = width;
            body.height = 0f;
            body.autoLayoutDirection = LayoutDirection.Vertical;
            body.autoLayout = true;
            body.clipChildren = true;
            body.relativePosition = new Vector3(0f, title.height);

            bodyBackground = body.AddUIComponent<UITextureSprite>();
            bodyBackground.name = "PeopleInsideBuildingsDataContainer";
            bodyBackground.width = body.width;
            bodyBackground.height = body.height;
            bodyBackground.texture = TextureDB.VehiclePanelBackground;
            bodyBackground.relativePosition = Vector3.zero;

            bodyRows = bodyBackground.AddUIComponent<UIScrollablePanel>();
            bodyRows.name = "BodyRows";
            bodyRows.width = bodyBackground.width - 24f;
            bodyRows.autoLayoutDirection = LayoutDirection.Vertical;
            bodyRows.autoLayout = true;
            bodyRows.relativePosition = new Vector3(12f, 0f);
        }

        private void CreateResidentialSection()
        {
            int rowIndex = 0;

            for (int unitIndex = 0; unitIndex < MaxHouseHolds; unitIndex++)
            {
                var panel = CreateGroupHeader("Residential", unitIndex, "HomeIcon", FavCimsLang.Text("OnBuildingResidential") + " " + (unitIndex + 1));

                residentialPanels[unitIndex] = panel.Panel;
                residentialPanelIcons[unitIndex] = panel.Icon;
                residentialPanelTexts[unitIndex] = panel.Text;

                for (int citizenIndex = 0; citizenIndex < CitizensPerUnit; citizenIndex++)
                {
                    var row = CreateOwnedRow<ResidentialBuildingPanelRow>();
                    row.name = "RowResidential" + rowIndex;
                    row.OnBuilding = 0;
                    row.citizen = 0;
                    row.LocType = Citizen.Location.Home;
                    row.Hide();

                    residentialRows[rowIndex] = row;
                    rowIndex++;
                }
            }
        }

        private void CreateWorkerSection()
        {
            var header = CreateGroupHeader("Workers", 0, "WorkersIcon", FavCimsLang.Text("OnBuildingWorkers"));

            workersPanel = header.Panel;
            workersPanelIcon = header.Icon;
            workersPanelText = header.Text;

            for (int i = 0; i < workerRows.Length; i++)
            {
                var row = CreateOwnedRow<WorkersBuildingPanelRow>();
                row.name = "RowWorkers" + i;
                row.OnBuilding = 0;
                row.citizen = 0;
                row.LocType = Citizen.Location.Work;
                row.Hide();

                workerRows[i] = row;
            }
        }

        private void CreateGuestSection()
        {
            var header = CreateGroupHeader("Guests", 0, "GuestsIcon", FavCimsLang.Text("OnBuildingGuests"));

            guestsPanel = header.Panel;
            guestsPanelIcon = header.Icon;
            guestsPanelText = header.Text;

            for (int i = 0; i < guestRows.Length; i++)
            {
                var row = CreateOwnedRow<GuestsBuildingPanelRow>();
                row.name = "RowGuests" + i;
                row.OnBuilding = 0;
                row.citizen = 0;
                row.LocType = Citizen.Location.Visit;
                row.Hide();

                guestRows[i] = row;
            }
        }

        private void CreateHotelSection()
        {
            for (int roomIndex = 0; roomIndex < MaxHotelRooms; roomIndex++)
            {
                var header = CreateGroupHeader("HotelGuests", roomIndex, "HomeIcon", FavCimsLang.Text("OnBuildingHotelRooms") + " " + (roomIndex + 1));

                hotelRoomPanels[roomIndex] = header.Panel;
                hotelRoomIcons[roomIndex] = header.Icon;
                hotelRoomTexts[roomIndex] = header.Text;

                var row = CreateOwnedRow<HotelBuildingPanelRow>();
                row.name = "Row_HotelGuests" + roomIndex;
                row.OnBuilding = 0;
                row.citizen = 0;
                row.LocType = Citizen.Location.Hotel;
                row.Hide();

                hotelRoomRows[roomIndex] = row;
            }
        }

        private GroupHeader CreateGroupHeader(string category, int index, string iconSprite, string labelText)
        {
            var panel = bodyRows.AddUIComponent<UIPanel>();
            panel.name = $"LabelPanel{category}{index}";
            panel.width = 226f;
            panel.height = 25f;
            panel.autoLayout = false;
            panel.relativePosition = Vector3.zero;
            panel.atlas = MyAtlas.FavCimsAtlas;
            panel.backgroundSprite = "BgRow2";
            panel.Hide();

            var icon = panel.AddUIComponent<UIButton>();

            icon.name = $"LabelPanelIcon{category}{index}";
            icon.width = 17f;
            icon.height = 17f;
            icon.atlas = MyAtlas.FavCimsAtlas;
            icon.normalFgSprite = iconSprite;
            icon.relativePosition = new Vector3(5f, 4f);

            var text = panel.AddUIComponent<UIButton>();

            text.name = $"LabelPanelText{category}{index}";
            text.width = 200f;
            text.height = 25f;
            text.font = UIDynamicFont.FindByName("OpenSans-Regular");
            text.textScale = 0.8f;
            text.textHorizontalAlignment = UIHorizontalAlignment.Left;
            text.textVerticalAlignment = UIVerticalAlignment.Middle;
            text.textPadding = new RectOffset(5, 5, 0, 0);
            text.wordWrap = false;

            text.textColor = new Color32(51, 51, 51, 160);
            
            text.useDropShadow = true;
            text.dropShadowOffset = new Vector2(1f, -1f);
            text.dropShadowColor = new Color32(0, 0, 0, 0);
            
            text.isInteractive = false;
            text.playAudioEvents = true;
            text.relativePosition = new Vector3(icon.relativePosition.x + icon.width, 1f);

            text.text = labelText;

            return new GroupHeader(panel, icon, text);
        }

        private void CreateScrollbar()
        {
            scrollPanel = bodyBackground.AddUIComponent<UIScrollablePanel>();
            scrollPanel.name = "BodyPanelScrollBar";
            scrollPanel.width = 10f;
            scrollPanel.relativePosition = new Vector3(bodyRows.width + 12f, 0f);

            scrollbar = scrollPanel.AddUIComponent<UIScrollbar>();
            scrollbar.name = "BodyScrollBar";
            scrollbar.width = 10f;
            scrollbar.orientation = UIOrientation.Vertical;
            scrollbar.pivot = UIPivotPoint.TopRight;
            scrollbar.AlignTo(scrollbar.parent, UIAlignAnchor.TopRight);
            scrollbar.minValue = 0f;
            scrollbar.value = 0f;
            scrollbar.incrementAmount = 25f;

            scrollbarTrack = scrollbar.AddUIComponent<UISlicedSprite>();
            scrollbarTrack.name = "BodyScrollBarTrackSprite";
            scrollbarTrack.autoSize = true;
            scrollbarTrack.fillDirection = UIFillDirection.Vertical;
            scrollbarTrack.atlas = MyAtlas.FavCimsAtlas;
            scrollbarTrack.spriteName = "ScrollbarTrack";
            scrollbarTrack.relativePosition = scrollbar.relativePosition;

            scrollbar.trackObject = scrollbarTrack;

            scrollbarThumb = scrollbar.AddUIComponent<UISlicedSprite>();
            scrollbarThumb.name = "BodyScrollBarThumbSprite";
            scrollbarThumb.autoSize = true;
            scrollbarThumb.width = scrollbarThumb.parent.width;
            scrollbarThumb.fillDirection = UIFillDirection.Vertical;
            scrollbarThumb.atlas = MyAtlas.FavCimsAtlas;
            scrollbarThumb.spriteName = "ScrollbarThumb";
            scrollbarThumb.relativePosition = scrollbar.relativePosition;

            scrollbar.thumbObject = scrollbarThumb;
            bodyRows.verticalScrollbar = scrollbar;

            bodyRows.eventMouseWheel += OnBodyRowsMouseWheel;
        }

        private void CreateFooter()
        {
            footer = AddUIComponent<UIPanel>();
            footer.name = "PeopleInsideBuildingsPanelFooter";
            footer.width = width;
            footer.height = 12f;
            footer.relativePosition = new Vector3(0f, title.height + body.height);

            footerBackground = footer.AddUIComponent<UITextureSprite>();
            footerBackground.name = "PeopleInsideBuildingsPanelFooterBackground";
            footerBackground.width = footer.width;
            footerBackground.height = footer.height;
            footerBackground.texture = TextureDB.VehiclePanelFooterBackground;
            footerBackground.relativePosition = Vector3.zero;
        }

        private void SubscribeEscHandler()
        {
            escButton = UIView.Find("Esc");

            if (escButton == null)
            {
                return;
            }

            escClickHandler = OnEscClicked;
            escButton.eventClick += escClickHandler;
        }

        private void OnBodyRowsMouseWheel(UIComponent component, UIMouseEventParameter eventParam)
        {
            int direction = Math.Sign(eventParam.wheelDelta);
            bodyRows.scrollPosition += new Vector2(0f, direction * -1 * scrollbar.incrementAmount);
        }

        private void OnEscClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Hide();
        }

        public override void Update()
        {
            if (FavCimsMainClass.UnLoading)
            {
                return;
            }

            if (!isVisible)
            {
                ClearPanel();
                return;
            }

            if (RefPanel == null || !RefPanel.isVisible)
            {
                ClosePanel();
                return;
            }

            if (BuildingID.IsEmpty || BuildingID.Type != InstanceType.Building || BuildingID.Building == 0)
            {
                ClosePanel();
                return;
            }

            var current = WorldInfoPanel.GetCurrentInstanceID();

            if (current.IsEmpty || current.Type != InstanceType.Building || current != BuildingID)
            {
                ClosePanel();
                return;
            }

            UpdateLayout();

            refreshTimer -= Time.deltaTime;

            if (hasActiveBuilding && refreshTimer > 0f)
            {
                return;
            }

            refreshTimer = RefreshInterval;

            if (!BuildingOccupantsService.TryRead(BuildingID, out var snapshot))
            {
                Utils.Debug.Warning("Failed to read occupants for building " + BuildingID.Building + "; closing its occupant panel.");

                ClosePanel();
                return;
            }

            hasActiveBuilding = true;

            RunWithRowsPaused(() =>
            {
                RefreshFromSnapshot(snapshot);

                // Apply grouping after all row binding, stale-row cleanup,
                // and the snapshot renderer's original header assignments.
                RefreshEmployeeSections();
            });

            // Measure the new headers and row arrangement immediately.
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (RefPanel == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(titleBuildingName.text))
            {
                titleBuildingName.text = FavCimsLang.Text("OnBuildingWorkers");
            }

            absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);

            float availableHeight = Math.Max(0f, RefPanel.height - 15f - title.height - footer.height);

            float preferredHeight = Math.Max(50f, MeasureVisibleBodyContentHeight());

            body.height = Math.Min(preferredHeight, Math.Min(400f, availableHeight));

            height = title.height + body.height + footer.height;
            bodyBackground.height = body.height;
            bodyRows.height = body.height;

            footer.relativePosition = new Vector3(0f, title.height + body.height);
            
            scrollPanel.height = body.height;
            scrollbar.height = body.height;
            scrollbarTrack.size = scrollbarTrack.parent.size;
        }

        private void AssignBuildingRow(ResidentialBuildingPanelRow row, uint citizenId, Citizen.Location location, HashSet<uint> currentOccupants)
        {
            if (row == null || citizenId == 0)
            {
                return;
            }

            CimsOnBuilding[citizenId] = BuildingID.Building;
            currentOccupants.Add(citizenId);

            row.citizen = citizenId;
            row.OnBuilding = BuildingID.Building;
            row.LocType = location;
            row.firstRun = true;
            row.Show();
        }


        private void UpdateGuestHeader(BuildingOccupantsSnapshot snapshot)
        {
            var buildingInfo = buildingManager.m_buildings.m_buffer[BuildingID.Building].Info;

            if (buildingInfo == null || buildingInfo.m_class == null)
            {
                guestsPanelIcon.normalFgSprite = "GuestsIcon";
                guestsPanelText.text = FavCimsLang.Text("OnBuildingGuests");
                return;
            }

            bool hasGuestRows = snapshot.HasVisitors || snapshot.HasStudents;

            switch (buildingInfo.m_class.m_service)
            {
                case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_subService != ItemClass.SubService.PoliceDepartmentBank:
                    guestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                    guestsPanelIcon.normalFgSprite = "CitizenArrestedIcon";
                    guestsPanelText.text = hasGuestRows ? FavCimsLang.Text("CitizenUnderArrest") : FavCimsLang.Text("OnBuildingNoArrested");
                    break;

                case ItemClass.Service.Education:
                    guestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                    guestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
                    guestsPanelText.text = hasGuestRows ? FavCimsLang.Text("CitizenAtSchool") : FavCimsLang.Text("OnBuildingNoStudents");
                    break;

                case ItemClass.Service.PlayerEducation when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                    guestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                    guestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
                    guestsPanelText.text = hasGuestRows ? FavCimsLang.Text("CitizenAtUniversity") : FavCimsLang.Text("OnBuildingNoStudents");
                    break;

                case ItemClass.Service.HealthCare when !FavCimsCore.IsCimCareBuilding(BuildingID.Building):
                    guestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                    guestsPanelIcon.normalFgSprite = "SubBarHealthcareDefault";
                    guestsPanelText.text = hasGuestRows ? FavCimsLang.Text("CitizenAtClinic") : FavCimsLang.Text("OnBuildingNoPatients");
                    break;

                default:
                    guestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                    guestsPanelIcon.normalFgSprite = "GuestsIcon";
                    guestsPanelText.text = hasGuestRows ? FavCimsLang.Text("OnBuildingGuests") : FavCimsLang.Text("OnBuildingNoGuests");
                    break;
            }
        }

        private void RemoveFormerOccupants(HashSet<uint> currentOccupants)
        {
            List<uint> formerOccupants = null;

            foreach (var occupant in CimsOnBuilding)
            {
                if (!currentOccupants.Contains(occupant.Key))
                {
                    (formerOccupants ??= []).Add(occupant.Key);
                }
            }

            if (formerOccupants != null)
            {
                foreach (uint citizenId in formerOccupants)
                {
                    CimsOnBuilding.Remove(citizenId);
                }
            }

            for (int i = 0; i < residentialRows.Length; i++)
            {
                var row = residentialRows[i];

                if (row != null && (row.citizen == 0 || !currentOccupants.Contains(row.citizen)))
                {
                    ResetBuildingRow(row);
                }
            }

            for (int i = 0; i < workerRows.Length; i++)
            {
                var row = workerRows[i];

                if (row != null && (row.citizen == 0 || !currentOccupants.Contains(row.citizen)))
                {
                    ResetBuildingRow(row);
                }
            }

            for (int i = 0; i < guestRows.Length; i++)
            {
                var row = guestRows[i];

                if (row != null && (row.citizen == 0 || !currentOccupants.Contains(row.citizen)))
                {
                    ResetBuildingRow(row);
                }
            }

            for (int i = 0; i < hotelRoomRows.Length; i++)
            {
                var row = hotelRoomRows[i];

                if (row != null && (row.citizen == 0 || !currentOccupants.Contains(row.citizen)))
                {
                    ResetBuildingRow(row);
                }
            }
        }

        private void ClearPanel()
        {
            if (!hasActiveBuilding)
            {
                return;
            }

            Wait = true;

            try
            {
                CimsOnBuilding.Clear();
                WorkersCount = 0;
                GuestsCount = 0;
                HotelGuestsCount = 0;

                for (int i = 0; i < residentialRows.Length; i++)
                {
                    if (residentialRows[i] != null)
                    {
                        ResetBuildingRow(residentialRows[i]);
                    }
                }

                for (int i = 0; i < workerRows.Length; i++)
                {
                    if (workerRows[i] != null)
                    {
                        ResetBuildingRow(workerRows[i]);
                    }
                }

                for (int i = 0; i < guestRows.Length; i++)
                {
                    if (guestRows[i] != null)
                    {
                        ResetBuildingRow(guestRows[i]);
                    }
                }

                for (int i = 0; i < hotelRoomRows.Length; i++)
                {
                    if (hotelRoomRows[i] != null)
                    {
                        ResetBuildingRow(hotelRoomRows[i]);
                    }
                }

                for (int i = 0; i < MaxHouseHolds; i++)
                {
                    residentialPanels[i]?.Hide();
                }

                for (int i = 0; i < MaxHotelRooms; i++)
                {
                    hotelRoomPanels[i]?.Hide();
                }

                workersPanel?.Hide();
                guestsPanel?.Hide();

                employeePlacements.Clear();
                employeeSections?.Hide();

                employeeSections?.ResetCollapseStates();

                foreach (var collapse in householdCollapses)
                {
                    collapse.Reset();
                }

                guestCollapse?.Reset();

                foreach (var collapse in hotelRoomCollapses)
                {
                    collapse.Reset();
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to clear building occupants: " + ex);
            }
            finally
            {
                hasActiveBuilding = false;
                Wait = false;
            }
        }

        private void ResetBuildingRow(ResidentialBuildingPanelRow row)
        {
            row.Hide();
            row.citizen = 0;
            row.OnBuilding = 0;
            row.LocType = Citizen.Location.Home;
            row.firstRun = true;
        }

        public override void OnDestroy()
        {
            if (escButton != null && escClickHandler != null)
            {
                escButton.eventClick -= escClickHandler;
            }

            escButton = null;
            escClickHandler = null;

            UnsubscribeCollapseHandlers();

            base.OnDestroy();
        }

        private readonly struct GroupHeader(UIPanel panel, UIButton icon, UIButton text)
        {
            public UIPanel Panel { get; } = panel;
            public UIButton Icon { get; } = icon;
            public UIButton Text { get; } = text;
        }

        private void ClosePanel()
        {
            Hide();
            BuildingID = InstanceID.Empty;

            ClearPanel();

            refreshTimer = RefreshInterval;
        }

        private TRow CreateOwnedRow<TRow>() where TRow : CompactCitizenPanelRow
        {
            var row = bodyRows.AddUIComponent<TRow>();

            row.BindOwner(rowContext);

            return row;
        }

        private void RunWithRowsPaused(Action action)
        {
            bool previouslyUpdating = Wait;

            Wait = true;

            try
            {
                action();
            }
            finally
            {
                Wait = previouslyUpdating;
            }
        }

        private void RefreshEmployeeSections()
        {
            if (employeeSections == null)
            {
                return;
            }

            employeePlacements.Clear();

            ushort buildingId = BuildingID.Building;

            if (BuildingID.IsEmpty || BuildingID.Type != InstanceType.Building || buildingId == 0)
            {
                employeeSections.Hide();
                return;
            }

            // Call only after normal employee-row binding and stale-row cleanup.
            foreach (var row in workerRows)
            {
                if (row == null || row.citizen == 0 || row.OnBuilding != buildingId)
                {
                    continue;
                }

                bool? isPresent = null;

                if (BuildingPresenceService.TryRead(row.citizen, buildingId, out BuildingPresenceState presence))
                {
                    isPresent = presence == BuildingPresenceState.Present;
                }

                employeePlacements.Add(new EmployeeRowPlacement(row, isPresent));
            }

            employeeSections.Apply(employeePlacements, FavCimsLang.Text("OnBuildingWorkersPresent"), FavCimsLang.Text("OnBuildingWorkersAway"), FavCimsLang.Text("OnBuildingWorkersUnknown"));
        }

        private float MeasureVisibleBodyContentHeight()
        {
            float height = 0f;

            foreach (UIComponent component in bodyRows.components)
            {
                if (component == null || !component.isVisible)
                {
                    continue;
                }

                height += component.height;
            }

            return height;
        }

        private void UnsubscribeCollapseHandlers()
        {
            if (employeeSections != null && collapseChangedHandlers.Count > 0)
            {
                employeeSections.SectionsChanged -= collapseChangedHandlers[0];
            }

            for (int i = 0; i < householdCollapses.Count; i++)
            {
                int handlerIndex = 1 + i;

                if (handlerIndex < collapseChangedHandlers.Count)
                {
                    householdCollapses[i].StateChanged -= collapseChangedHandlers[handlerIndex];
                }
            }

            int guestHandlerIndex = 1 + householdCollapses.Count;

            if (guestCollapse != null && guestHandlerIndex < collapseChangedHandlers.Count)
            {
                guestCollapse.StateChanged -= collapseChangedHandlers[guestHandlerIndex];
            }

            int hotelStartIndex = guestHandlerIndex + 1;

            for (int i = 0; i < hotelRoomCollapses.Count; i++)
            {
                int handlerIndex = hotelStartIndex + i;

                if (handlerIndex < collapseChangedHandlers.Count)
                {
                    hotelRoomCollapses[i].StateChanged -= collapseChangedHandlers[handlerIndex];
                }
            }

            collapseChangedHandlers.Clear();
        }
    }
}