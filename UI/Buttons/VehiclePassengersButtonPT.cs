using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class VehiclePassengersButtonPT : UIButton
    {
        private InstanceID VehicleID = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private FavCimsVehiclePanelPT VehiclePanel;

        private readonly VehicleManager VehicleManager = Singleton<VehicleManager>.instance;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            name = "FavCimsVehPassButton";
            normalBgSprite = "vehicleButton";
            hoveredBgSprite = "vehicleButtonHovered";
            focusedBgSprite = "vehicleButtonHovered";
            pressedBgSprite = "vehicleButtonHovered";
            disabledBgSprite = "vehicleButtonDisabled";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(36f, 32f);
            playAudioEvents = true;
            AlignTo(RefPanel, Alignment);
            tooltipBox = aView.defaultTooltipBox;
            VehiclePanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(FavCimsVehiclePanelPT)) as FavCimsVehiclePanelPT;
            VehiclePanel.VehicleID = InstanceID.Empty;
            VehiclePanel.Hide();
            eventClick += delegate
            {
                if (!VehicleID.IsEmpty && !VehiclePanel.isVisible)
                {
                    VehiclePanel.VehicleID = VehicleID;
                    VehiclePanel.RefPanel = RefPanel;
                    VehiclePanel.Show();
                }
                else
                {
                    VehiclePanel.VehicleID = InstanceID.Empty;
                    VehiclePanel.Hide();
                }
            };
        }

        public override void Update()
        {
            if (FavCimsMainClass.UnLoading)
            {
                return;
            }
            if (isVisible)
            {
                tooltip = Translations.Translate("View_NoPassengers");
                if (WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty)
                {
                    VehicleID = WorldInfoPanel.GetCurrentInstanceID();
                }
                if (VehiclePanel != null)
                {
                    if (!VehiclePanel.isVisible)
                    {
                        Unfocus();
                    }
                    else
                    {
                        Focus();
                    }
                }
                var service = VehicleManager.m_vehicles.m_buffer[VehicleID.Vehicle].Info.m_class.m_service;
                var sub_service = VehicleManager.m_vehicles.m_buffer[VehicleID.Vehicle].Info.m_class.m_subService;
                if (service != ItemClass.Service.PublicTransport || (service == ItemClass.Service.PublicTransport && sub_service == ItemClass.SubService.PublicTransportPost))
                {
                    isEnabled = false;
                    VehiclePanel.Hide();
                }
                else if (!VehicleID.IsEmpty && VehicleID.Type == InstanceType.Vehicle)
                {
                    isEnabled = true;
                    tooltip = Translations.Translate("View_PassengersList");
                }
                else
                {
                    VehiclePanel.Hide();
                    Unfocus();
                    isEnabled = false;
                }
            }
            else
            {
                isEnabled = false;
                VehiclePanel.Hide();
                VehicleID = InstanceID.Empty;
            }
        }
    }
}
