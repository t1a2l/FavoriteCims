using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class PassengersInsideVehiclesButton : UIButton
    {
        private InstanceID VehicleID = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private PeopleInsideVehiclesPanel VehiclePanel;

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
            VehiclePanel = MainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideVehiclesPanel)) as PeopleInsideVehiclesPanel;
            VehiclePanel.VehicleID = InstanceID.Empty;
            VehiclePanel.Hide();
            eventClick += delegate
            {
                if (!VehicleID.IsEmpty && !VehiclePanel.isVisible)
                {
                    VehiclePanel.VehicleID = VehicleID;
                    VehiclePanel.RefPanel = RefPanel;
                    VehiclePanel.enabled = true;
                    VehiclePanel.Show();
                }
                else
                {
                    VehiclePanel.VehicleID = InstanceID.Empty;
                    VehiclePanel.Hide();
                    VehiclePanel.enabled = false;
                }
            };
        }

        public override void Update()
        {
            if (MainClass.UnLoading)
            {
                return;
            }
            if (!isVisible)
            {
                VehiclePanel.Hide();
                VehiclePanel.enabled = false;
                VehicleID = InstanceID.Empty;
                Unfocus();
                return;
            }

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
            var vehicleAI = VehicleManager.m_vehicles.m_buffer[VehicleID.Vehicle].Info.GetAI();

            if ((service == ItemClass.Service.PublicTransport && sub_service != ItemClass.SubService.PublicTransportPost) ||
                (service == ItemClass.Service.HealthCare && (vehicleAI is AmbulanceAI || vehicleAI is AmbulanceCopterAI)) ||
                (service == ItemClass.Service.PoliceDepartment))
            {
                isEnabled = true;
                VehiclePanel.IsPTVehicle = true;
                tooltip = Translations.Translate("View_PassengersList");
            }
            else if (!VehicleID.IsEmpty && VehicleID.Type == InstanceType.Vehicle)
            {
                isEnabled = true;
                VehiclePanel.IsPTVehicle = false;
                tooltip = Translations.Translate("View_PassengersList");
            }
            else
            {
                isEnabled = false;
                VehiclePanel.Hide();
                VehiclePanel.enabled = false;
                Unfocus();
            }
        }
    }
}
