using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class FavCimsVehiclePanel : UIPanel
    {
        private const float RefreshInterval = 0.5f;
        private const int PassengerSlotCount = 5;

        private float refreshTimer = RefreshInterval;

        private bool hasActiveVehicle;

        public bool Wait
        {
            get => rowContext.IsUpdating;
            set => rowContext.IsUpdating = value;
        }

        public InstanceID VehicleID;

        public UIPanel RefPanel;

        public Dictionary<uint, uint> CimsOnVeh => rowContext.Citizens;

        private readonly VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
        private readonly CitizenManager citizenManager = Singleton<CitizenManager>.instance;

        private UIPanel title;
        private UITextureSprite titleBackground;
        private UIButton titleVehicleName;

        private UIPanel body;
        private UITextureSprite bodyBackground;
        private UIScrollablePanel bodyRows;

        private UIPanel footer;
        private UITextureSprite footerBackground;

        private UIScrollablePanel scrollPanel;
        private UIScrollbar scrollbar;
        private UISlicedSprite scrollbarTrack;
        private UISlicedSprite scrollbarThumb;

        private UIPanel driverPanel;
        private UIPanel driverPanelSubRow;
        private UIButton driverPanelIcon;
        private UIButton driverPanelText;

        private UIPanel passengersPanel;
        private UIPanel passengersPanelSubRow;
        private UIButton passengersPanelIcon;
        private UIButton passengersPanelText;

        private DriverPrivateVehiclePanelRow driverRow;
        private readonly PassengersPrivateVehiclePanelRow[] passengerRows = new PassengersPrivateVehiclePanelRow[PassengerSlotCount];

        private UIComponent escButton;
        private MouseEventHandler escClickHandler;

        private readonly CompactCitizenRowContext rowContext = new();

        public override void Start()
        {
            try
            {
                width = 250f;
                height = 0f;
                name = "FavCimsVehiclePanel";
                absolutePosition = Vector3.zero;
                Hide();

                CreateTitle();
                CreateBody();
                CreateDriverSection();
                CreatePassengerSection();
                CreateScrollbar();
                CreateFooter();

                SubscribeEscHandler();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Passengers Panel Start(): " + ex);
            }
        }

        private void CreateTitle()
        {
            title = AddUIComponent<UIPanel>();
            title.name = "FavCimsVehiclePanelTitle";
            title.width = width;
            title.height = 41f;
            title.relativePosition = Vector3.zero;

            titleBackground = title.AddUIComponent<UITextureSprite>();
            titleBackground.name = "FavCimsVehiclePanelTitleBG";
            titleBackground.width = title.width;
            titleBackground.height = title.height;
            titleBackground.texture = TextureDB.VehiclePanelTitleBackground;
            titleBackground.relativePosition = Vector3.zero;

            titleVehicleName = title.AddUIComponent<UIButton>();
            titleVehicleName.name = "TitleVehicleName";
            titleVehicleName.width = title.width;
            titleVehicleName.height = title.height;
            titleVehicleName.textVerticalAlignment = UIVerticalAlignment.Middle;
            titleVehicleName.textHorizontalAlignment = UIHorizontalAlignment.Center;
            titleVehicleName.playAudioEvents = false;
            titleVehicleName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            titleVehicleName.font.size = 15;
            titleVehicleName.textScale = 1f;
            titleVehicleName.wordWrap = true;
            titleVehicleName.textPadding = new RectOffset(5, 5, 0, 0);
            titleVehicleName.textColor = new Color32(204, 204, 51, 40);
            titleVehicleName.useDropShadow = true;
            titleVehicleName.dropShadowOffset = new Vector2(1f, -1f);
            titleVehicleName.dropShadowColor = new Color32(0, 0, 0, 0);
            titleVehicleName.relativePosition = Vector3.zero;
        }

        private void CreateBody()
        {
            body = AddUIComponent<UIPanel>();
            body.name = "VehiclePanelBody";
            body.width = width;
            body.height = 0f;
            body.autoLayoutDirection = LayoutDirection.Vertical;
            body.autoLayout = true;
            body.clipChildren = true;
            body.relativePosition = new Vector3(0f, title.height);

            bodyBackground = body.AddUIComponent<UITextureSprite>();
            bodyBackground.name = "VehiclePanelDataContainer";
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

        private void CreateDriverSection()
        {
            driverPanel = bodyRows.AddUIComponent<UIPanel>();
            driverPanel.name = "LabelPanelDriver";
            driverPanel.width = 226f;
            driverPanel.height = 25f;
            driverPanel.autoLayoutDirection = LayoutDirection.Vertical;
            driverPanel.autoLayout = true;
            driverPanel.Hide();

            driverPanelSubRow = driverPanel.AddUIComponent<UIPanel>();
            driverPanelSubRow.name = "TitlePanelDriver";
            driverPanelSubRow.width = 226f;
            driverPanelSubRow.height = 25f;
            driverPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
            driverPanelSubRow.backgroundSprite = "BgRow2";

            driverPanelIcon = driverPanelSubRow.AddUIComponent<UIButton>();
            driverPanelIcon.name = "LabelPanelIconDriver";
            driverPanelIcon.width = 17f;
            driverPanelIcon.height = 17f;
            driverPanelIcon.atlas = MyAtlas.FavCimsAtlas;
            driverPanelIcon.normalFgSprite = "DriverIcon";
            driverPanelIcon.relativePosition = new Vector3(5f, 4f);

            driverPanelText = driverPanelSubRow.AddUIComponent<UIButton>();
            driverPanelText.name = "LabelPanelTextDriver";
            driverPanelText.width = 200f;
            driverPanelText.height = 25f;
            driverPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
            driverPanelText.textHorizontalAlignment = UIHorizontalAlignment.Left;
            driverPanelText.playAudioEvents = true;
            driverPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
            driverPanelText.font.size = 15;
            driverPanelText.textScale = 0.8f;
            driverPanelText.useDropShadow = true;
            driverPanelText.dropShadowOffset = new Vector2(1f, -1f);
            driverPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
            driverPanelText.textPadding = new RectOffset(5, 5, 0, 0);
            driverPanelText.textColor = new Color32(51, 51, 51, 160);
            driverPanelText.isInteractive = false;
            driverPanelText.relativePosition = new Vector3(driverPanelIcon.relativePosition.x + driverPanelIcon.width, 1f);

            driverRow = CreateOwnedRow<DriverPrivateVehiclePanelRow>();
            driverRow.name = "RowPanelDriver";
            driverRow.OnVehicle = 0;
            driverRow.citizen = 0;
            driverRow.Hide();
        }

        private void CreatePassengerSection()
        {
            passengersPanel = bodyRows.AddUIComponent<UIPanel>();
            passengersPanel.name = "LabelPanelPassengers";
            passengersPanel.width = 226f;
            passengersPanel.height = 25f;
            passengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
            passengersPanel.autoLayout = true;
            passengersPanel.Hide();

            passengersPanelSubRow = passengersPanel.AddUIComponent<UIPanel>();
            passengersPanelSubRow.name = "TitlePanelPassengers";
            passengersPanelSubRow.width = 226f;
            passengersPanelSubRow.height = 25f;
            passengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
            passengersPanelSubRow.backgroundSprite = "BgRow2";

            passengersPanelIcon = passengersPanelSubRow.AddUIComponent<UIButton>();
            passengersPanelIcon.name = "LabelPanelIconPassengers";
            passengersPanelIcon.width = 17f;
            passengersPanelIcon.height = 17f;
            passengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
            passengersPanelIcon.normalFgSprite = "PassengerIcon";
            passengersPanelIcon.relativePosition = new Vector3(5f, 4f);

            passengersPanelText = passengersPanelSubRow.AddUIComponent<UIButton>();
            passengersPanelText.name = "LabelPanelTextPassengers";
            passengersPanelText.width = 200f;
            passengersPanelText.height = 25f;
            passengersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
            passengersPanelText.textHorizontalAlignment = UIHorizontalAlignment.Left;
            passengersPanelText.playAudioEvents = true;
            passengersPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
            passengersPanelText.font.size = 15;
            passengersPanelText.textScale = 0.8f;
            passengersPanelText.useDropShadow = true;
            passengersPanelText.dropShadowOffset = new Vector2(1f, -1f);
            passengersPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
            passengersPanelText.textPadding = new RectOffset(5, 5, 0, 0);
            passengersPanelText.textColor = new Color32(51, 51, 51, 160);
            passengersPanelText.isInteractive = false;
            passengersPanelText.relativePosition = new Vector3(passengersPanelIcon.relativePosition.x + passengersPanelIcon.width, 1f);

            for (int i = 1; i < PassengerSlotCount; i++)
            {
                passengerRows[i] = CreateOwnedRow<PassengersPrivateVehiclePanelRow>();
                passengerRows[i].name = "RowPanelPassengers" + i;
                passengerRows[i].OnVehicle = 0;
                passengerRows[i].citizen = 0;
                passengerRows[i].Hide();
            }
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
            footer.name = "VehiclePanelFooter";
            footer.width = width;
            footer.height = 12f;
            footer.relativePosition = new Vector3(0f, title.height + body.height);

            footerBackground = footer.AddUIComponent<UITextureSprite>();
            footerBackground.name = "VehiclePanelFooterBackground";
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
                ClearOccupants();
                return;
            }

            if (RefPanel == null || !RefPanel.isVisible || VehicleID.IsEmpty || VehicleID.Type != InstanceType.Vehicle || VehicleID.Vehicle == 0)
            {
                ClosePanel();
                return;
            }

            var current = WorldInfoPanel.GetCurrentInstanceID();

            if (current.IsEmpty || current.Type != InstanceType.Vehicle || current != VehicleID)
            {
                ClosePanel();
                return;
            }

            UpdateLayout();

            refreshTimer -= Time.deltaTime;

            if (refreshTimer > 0f && hasActiveVehicle)
            {
                return;
            }

            refreshTimer = RefreshInterval;
            hasActiveVehicle = true;

            RunWithRowsPaused(RefreshOccupants);
        }

        private void UpdateLayout()
        {
            titleVehicleName.text = FavCimsLang.Text("VehiclePassengersTitle");

            absolutePosition = new Vector3(
                RefPanel.absolutePosition.x + RefPanel.width + 5f,
                RefPanel.absolutePosition.y);

            height = RefPanel.height - 15f;

            float preferredHeight = 50f + CimsOnVeh.Count * 25f;
            float availableHeight = height - title.height - footer.height;

            body.height = preferredHeight < availableHeight ? availableHeight : Math.Min(preferredHeight, 400f);

            bodyBackground.height = body.height;
            footer.relativePosition = new Vector3(0f, title.height + body.height);
            bodyRows.height = body.height;
            scrollPanel.height = body.height;
            scrollbar.height = body.height;
            scrollbarTrack.size = scrollbarTrack.parent.size;
        }

        private void AssignDriver(uint citizenId)
        {
            CimsOnVeh[citizenId] = VehicleID.Vehicle;

            driverRow.citizen = citizenId;
            driverRow.OnVehicle = VehicleID.Vehicle;
            driverRow.firstRun = true;
            driverRow.Show();
        }

        private void AssignPassenger(int slot, uint citizenId)
        {
            var row = passengerRows[slot];

            if (row == null)
            {
                return;
            }

            CimsOnVeh[citizenId] = VehicleID.Vehicle;

            row.citizen = citizenId;
            row.OnVehicle = VehicleID.Vehicle;
            row.firstRun = true;
            row.Show();
        }

        private void ClearOccupants()
        {
            if (!hasActiveVehicle)
            {
                return;
            }

            Wait = true;

            try
            {
                CimsOnVeh.Clear();

                ResetDriverRow();

                for (int i = 1; i < PassengerSlotCount; i++)
                {
                    if (passengerRows[i] != null)
                    {
                        ResetPassengerRow(passengerRows[i]);
                    }
                }

                driverPanel?.Hide();
                passengersPanel?.Hide();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to clear vehicle occupants: " + ex);
            }
            finally
            {
                hasActiveVehicle = false;
                Wait = false;
            }
        }

        private void ResetDriverRow()
        {
            if (driverRow == null)
            {
                return;
            }

            driverRow.Hide();
            driverRow.citizen = 0;
            driverRow.OnVehicle = 0;
            driverRow.firstRun = true;
        }

        private void ResetPassengerRow(PassengersPrivateVehiclePanelRow row)
        {
            row.Hide();
            row.citizen = 0;
            row.OnVehicle = 0;
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

            base.OnDestroy();
        }

        private void ClosePanel()
        {
            Hide();
            VehicleID = InstanceID.Empty;

            ClearOccupants();

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
    }
}