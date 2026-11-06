using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class VehiclePassengersButton : UIButton
    {
        private InstanceID vehicleId = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private FavCimsVehiclePanel vehiclePanel;
        private bool initialized;

        public override void Start()
        {
            base.Start();

            name = "FavCimsVehPassButton";
            normalBgSprite = "VehicleButton";
            hoveredBgSprite = "VehicleButtonHovered";
            focusedBgSprite = "VehicleButtonHovered";
            pressedBgSprite = "VehicleButtonHovered";
            disabledBgSprite = "VehicleButtonDisabled";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(36f, 36f);
            playAudioEvents = true;
            tooltipBox = UIView.GetAView().defaultTooltipBox;
            isEnabled = false;

            if (RefPanel == null || FavCimsMainClass.FullScreenContainer == null)
            {
                Utils.Debug.Warning("Cannot initialize VehiclePassengersButton: source panel or fullscreen container is missing.");
                return;
            }

            AlignTo(RefPanel, Alignment);

            vehiclePanel = FavCimsMainClass.FullScreenContainer.AddUIComponent<FavCimsVehiclePanel>();

            if (vehiclePanel == null)
            {
                Utils.Debug.Warning("Cannot initialize VehiclePassengersButton: vehicle panel could not be created.");
                return;
            }

            vehiclePanel.VehicleID = InstanceID.Empty;
            vehiclePanel.RefPanel = RefPanel;
            vehiclePanel.Hide();

            eventClick += OnButtonClicked;

            initialized = true;
            RefreshState();
        }

        public override void Update()
        {
            base.Update();

            if (!initialized)
            {
                return;
            }

            RefreshState();
        }

        private void RefreshState()
        {
            if (FavCimsMainClass.UnLoading || !isVisible || RefPanel == null || vehiclePanel == null)
            {
                DisableButton();
                return;
            }

            var current = WorldInfoPanel.GetCurrentInstanceID();

            if (current.IsEmpty || current.Type != InstanceType.Vehicle || current.Vehicle == 0)
            {
                DisableButton();
                return;
            }

            if (vehiclePanel.isVisible && vehiclePanel.VehicleID != current)
            {
                CloseVehiclePanel();
            }

            vehicleId = current;

            isEnabled = true;
            tooltip = FavCimsLang.Text("VehiclePassengersButtonTooltip");

            if (vehiclePanel.isVisible)
            {
                if (!hasFocus)
                {
                    Focus();
                }
            }
            else if (hasFocus)
            {
                Unfocus();
            }
        }

        private void OnButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (!initialized || FavCimsMainClass.UnLoading || !isVisible)
            {
                return;
            }

            RefreshState();

            if (!isEnabled || vehicleId.IsEmpty || vehiclePanel == null)
            {
                return;
            }

            if (vehiclePanel.isVisible)
            {
                CloseVehiclePanel();
                return;
            }

            vehiclePanel.VehicleID = vehicleId;
            vehiclePanel.RefPanel = RefPanel;
            vehiclePanel.Show();

            Focus();
        }

        private void CloseVehiclePanel()
        {
            if (vehiclePanel != null)
            {
                vehiclePanel.Hide();
                vehiclePanel.VehicleID = InstanceID.Empty;
            }

            if (hasFocus)
            {
                Unfocus();
            }
        }

        private void DisableButton()
        {
            vehicleId = InstanceID.Empty;
            isEnabled = false;
            tooltip = string.Empty;

            CloseVehiclePanel();
        }

        public override void OnDestroy()
        {
            initialized = false;
            eventClick -= OnButtonClicked;

            if (vehiclePanel != null)
            {
                Object.Destroy(vehiclePanel.gameObject);
                vehiclePanel = null;
            }

            base.OnDestroy();
        }
    }
}