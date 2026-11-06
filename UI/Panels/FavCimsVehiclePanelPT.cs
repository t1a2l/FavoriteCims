using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class FavCimsVehiclePanelPT : UIPanel
    {
        private const float RefreshInterval = 0.5f;
        private const int MaxPassengerUnits = 20;
        private const int MaxPassengerRows = MaxPassengerUnits * 5;

        private float refreshTimer = RefreshInterval;

        private bool hasActiveVehicle;

        public bool Wait
        {
            get => rowContext.IsUpdating;
            set => rowContext.IsUpdating = value;
        }

        public InstanceID VehicleID;

        public UIPanel RefPanel;

        public Dictionary<uint, uint> CimsOnPTVeh => rowContext.Citizens;

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

        private UIPanel passengersPanel;
        private UIPanel passengersPanelSubRow;
        private UIButton passengersPanelIcon;
        private UIButton passengersPanelText;

        private readonly PassengersVehiclePanelRow[] passengerRows = new PassengersVehiclePanelRow[MaxPassengerRows];

        private UIComponent escButton;
        private MouseEventHandler escClickHandler;

        private readonly CompactCitizenRowContext rowContext = new();

        public override void Start()
        {
            try
            {
                width = 250f;
                height = 0f;
                name = "FavCimsVehiclePanelPT";
                absolutePosition = Vector3.zero;
                Hide();

                CreateTitle();
                CreateBody();
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
            title.name = "FavCimsVehiclePanelPTTitle";
            title.width = width;
            title.height = 41f;
            title.relativePosition = Vector3.zero;

            titleBackground = title.AddUIComponent<UITextureSprite>();
            titleBackground.name = "FavCimsVehiclePanelPTTitleBG";
            titleBackground.width = title.width;
            titleBackground.height = title.height;
            titleBackground.texture = TextureDB.VehiclePanelTitleBackground;
            titleBackground.relativePosition = Vector3.zero;

            titleVehicleName = title.AddUIComponent<UIButton>();
            titleVehicleName.name = "TitleVehiclePTName";
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
            body.name = "VehiclePanelPTBody";
            body.width = width;
            body.height = 0f;
            body.autoLayoutDirection = LayoutDirection.Vertical;
            body.autoLayout = true;
            body.clipChildren = true;
            body.relativePosition = new Vector3(0f, title.height);

            bodyBackground = body.AddUIComponent<UITextureSprite>();
            bodyBackground.name = "VehiclePanelPTDataContainer";
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

        private void CreatePassengerSection()
        {
            passengersPanel = bodyRows.AddUIComponent<UIPanel>();
            passengersPanel.name = "LabelPanelPT0";
            passengersPanel.width = 226f;
            passengersPanel.height = 25f;
            passengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
            passengersPanel.autoLayout = true;
            passengersPanel.Hide();

            passengersPanelSubRow = passengersPanel.AddUIComponent<UIPanel>();
            passengersPanelSubRow.name = "TitlePanelPT0";
            passengersPanelSubRow.width = 226f;
            passengersPanelSubRow.height = 25f;
            passengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
            passengersPanelSubRow.backgroundSprite = "BgRow2";

            passengersPanelIcon = passengersPanelSubRow.AddUIComponent<UIButton>();
            passengersPanelIcon.name = "LabelPanelIconPT0";
            passengersPanelIcon.width = 17f;
            passengersPanelIcon.height = 17f;
            passengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
            passengersPanelIcon.normalFgSprite = "PassengerIcon";
            passengersPanelIcon.relativePosition = new Vector3(5f, 4f);

            passengersPanelText = passengersPanelSubRow.AddUIComponent<UIButton>();
            passengersPanelText.name = "LabelPanelTextPT0";
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

            for (int i = 0; i < MaxPassengerRows; i++)
            {
                passengerRows[i] = CreateOwnedRow<PassengersVehiclePanelRow>();
                passengerRows[i].name = "RowPanelPT" + i;
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
            footer.name = "VehiclePanelPTFooter";
            footer.width = width;
            footer.height = 12f;
            footer.relativePosition = new Vector3(0f, title.height + body.height);

            footerBackground = footer.AddUIComponent<UITextureSprite>();
            footerBackground.name = "VehiclePanelPTFooterBackground";
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

            absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);
            height = RefPanel.height - 15f;

            float preferredHeight = 25f + CimsOnPTVeh.Count * 25f;
            float availableHeight = height - title.height - footer.height;

            body.height = preferredHeight < availableHeight ? availableHeight : Math.Min(preferredHeight, 400f);

            bodyBackground.height = body.height;
            footer.relativePosition = new Vector3(0f, title.height + body.height);
            bodyRows.height = body.height;
            scrollPanel.height = body.height;
            scrollbar.height = body.height;
            scrollbarTrack.size = scrollbarTrack.parent.size;
        }


        private void AssignPassenger(int rowNumber, uint citizenId)
        {
            var row = passengerRows[rowNumber];

            if (row == null)
            {
                return;
            }

            CimsOnPTVeh[citizenId] = VehicleID.Vehicle;

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
                CimsOnPTVeh.Clear();

                for (int i = 0; i < MaxPassengerRows; i++)
                {
                    if (passengerRows[i] != null)
                    {
                        ResetPassengerRow(passengerRows[i]);
                    }
                }

                passengersPanel?.Hide();    
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to clear public-transport occupants: " + ex);
            }
            finally
            {
                hasActiveVehicle = false;
                Wait = false;
            }
        }

        private void ResetPassengerRow(PassengersVehiclePanelRow row)
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