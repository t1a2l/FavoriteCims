using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class VechiclePassengersButtonPT : UIButton
    {
        private InstanceID VehicleID = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private FavCimsVehiclePanelPT VehiclePanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
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
            tooltipBox = aview.defaultTooltipBox;
            bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVehiclePanelPT>() != null;
            if (flag)
            {
                VehiclePanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVehiclePanelPT>();
            }
            else
            {
                VehiclePanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(FavCimsVehiclePanelPT)) as FavCimsVehiclePanelPT;
            }
            VehiclePanel.VehicleID = InstanceID.Empty;
            VehiclePanel.Hide();
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                bool flag2 = !VehicleID.IsEmpty && !VehiclePanel.isVisible;
                if (flag2)
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
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool isVisible = base.isVisible;
                if (isVisible)
                {
                    tooltip = FavCimsLang.Text("View_NoPassengers");
                    bool flag = WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty;
                    if (flag)
                    {
                        VehicleID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    bool flag2 = VehiclePanel != null;
                    if (flag2)
                    {
                        bool flag3 = !VehiclePanel.isVisible;
                        if (flag3)
                        {
                            Unfocus();
                        }
                        else
                        {
                            Focus();
                        }
                    }
                    bool flag4 = !VehicleID.IsEmpty && VehicleID.Type == InstanceType.Vehicle;
                    if (flag4)
                    {
                        isEnabled = true;
                        tooltip = FavCimsLang.Text("View_PassengersList");
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
}
