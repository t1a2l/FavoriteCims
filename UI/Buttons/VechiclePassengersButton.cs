using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
    public class VechiclePassengersButton : UIButton
    {
        private InstanceID VehicleID = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private FavCimsVechiclePanel VehiclePanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
            base.name = "FavCimsVehPassButton";
            base.normalBgSprite = "vehicleButton";
            base.hoveredBgSprite = "vehicleButtonHovered";
            base.focusedBgSprite = "vehicleButtonHovered";
            base.pressedBgSprite = "vehicleButtonHovered";
            base.disabledBgSprite = "vehicleButtonDisabled";
            base.atlas = MyAtlas.FavCimsAtlas;
            base.size = new Vector2(36f, 36f);
            base.playAudioEvents = true;
            base.AlignTo(this.RefPanel, this.Alignment);
            base.tooltipBox = aview.defaultTooltipBox;
            bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVechiclePanel>() != null;
            if (flag)
            {
                this.VehiclePanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVechiclePanel>();
            }
            else
            {
                this.VehiclePanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(FavCimsVechiclePanel)) as FavCimsVechiclePanel;
            }
            this.VehiclePanel.VehicleID = InstanceID.Empty;
            this.VehiclePanel.Hide();
            base.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                bool flag2 = !this.VehicleID.IsEmpty && !this.VehiclePanel.isVisible;
                if (flag2)
                {
                    this.VehiclePanel.VehicleID = this.VehicleID;
                    this.VehiclePanel.RefPanel = this.RefPanel;
                    this.VehiclePanel.Show();
                }
                else
                {
                    this.VehiclePanel.VehicleID = InstanceID.Empty;
                    this.VehiclePanel.Hide();
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
                    base.tooltip = FavCimsLang.Text("View_NoPassengers");
                    bool flag = WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty;
                    if (flag)
                    {
                        this.VehicleID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    bool flag2 = this.VehiclePanel != null;
                    if (flag2)
                    {
                        bool flag3 = !this.VehiclePanel.isVisible;
                        if (flag3)
                        {
                            base.Unfocus();
                        }
                        else
                        {
                            base.Focus();
                        }
                    }
                    bool flag4 = !this.VehicleID.IsEmpty && this.VehicleID.Type == InstanceType.Vehicle;
                    if (flag4)
                    {
                        base.isEnabled = true;
                        base.tooltip = FavCimsLang.Text("View_PassengersList");
                    }
                    else
                    {
                        this.VehiclePanel.Hide();
                        base.Unfocus();
                        base.isEnabled = false;
                    }
                }
                else
                {
                    base.isEnabled = false;
                    this.VehiclePanel.Hide();
                    this.VehicleID = InstanceID.Empty;
                }
            }
        }
    }
}
