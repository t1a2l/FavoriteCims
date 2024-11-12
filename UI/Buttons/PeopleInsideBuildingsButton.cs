using System;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
    public class PeopleInsideBuildingsButton : UIButton
    {
        private InstanceID BuildingID = InstanceID.Empty;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private PeopleInsideBuildingsPanel BuildingPanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
            base.name = "PeopleInsideBuildingsButton";
            base.atlas = MyAtlas.FavCimsAtlas;
            base.size = new Vector2(32f, 36f);
            base.playAudioEvents = true;
            base.AlignTo(this.RefPanel, this.Alignment);
            base.tooltipBox = aview.defaultTooltipBox;
            bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel>() != null;
            if (flag)
            {
                this.BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel>();
            }
            else
            {
                this.BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideBuildingsPanel)) as PeopleInsideBuildingsPanel;
            }
            this.BuildingPanel.BuildingID = InstanceID.Empty;
            this.BuildingPanel.Hide();
            base.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                bool flag2 = !this.BuildingID.IsEmpty && !this.BuildingPanel.isVisible;
                if (flag2)
                {
                    this.BuildingPanel.BuildingID = this.BuildingID;
                    this.BuildingPanel.RefPanel = this.RefPanel;
                    this.BuildingPanel.Show();
                }
                else
                {
                    this.BuildingPanel.BuildingID = InstanceID.Empty;
                    this.BuildingPanel.Hide();
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
                    base.tooltip = null;
                    bool flag = WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty;
                    if (flag)
                    {
                        this.BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    bool flag2 = this.BuildingPanel != null;
                    if (flag2)
                    {
                        bool flag3 = !this.BuildingPanel.isVisible;
                        if (flag3)
                        {
                            base.Unfocus();
                        }
                        else
                        {
                            base.Focus();
                        }
                    }
                    bool flag4 = !this.BuildingID.IsEmpty && this.BuildingID.Type == InstanceType.Building;
                    if (flag4)
                    {
                        BuildingInfo info = this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].Info;
                        bool flag5 = info.m_class.m_service == ItemClass.Service.Residential;
                        if (flag5)
                        {
                            base.tooltip = FavCimsLang.Text("Citizens_HouseHolds");
                            base.normalBgSprite = "BuildingButtonIcon";
                            base.hoveredBgSprite = "BuildingButtonIconHovered";
                            base.focusedBgSprite = "BuildingButtonIconHovered";
                            base.pressedBgSprite = "BuildingButtonIconHovered";
                            base.disabledBgSprite = "BuildingButtonIconDisabled";
                        }
                        else
                        {
                            bool flag6 = info.m_class.m_service == ItemClass.Service.Commercial;
                            if (flag6)
                            {
                                base.tooltip = FavCimsLang.Text("CitizenOnBuilding");
                                base.normalBgSprite = "CommercialBuildingButtonIcon";
                                base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                            }
                            else
                            {
                                bool flag7 = info.m_class.m_service == ItemClass.Service.Office;
                                if (flag7)
                                {
                                    base.tooltip = FavCimsLang.Text("WorkersOnBuilding");
                                    base.normalBgSprite = "CommercialBuildingButtonIcon";
                                    base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                    base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                    base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                    base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                }
                                else
                                {
                                    base.tooltip = FavCimsLang.Text("WorkersOnBuilding");
                                    base.normalBgSprite = "IndustrialBuildingButtonIcon";
                                    base.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                    base.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                    base.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                    base.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                }
                            }
                        }
                        bool flag8 = Convert.ToInt32(this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].m_citizenCount) == 0;
                        if (flag8)
                        {
                            this.BuildingPanel.Hide();
                            base.tooltip = FavCimsLang.Text("BuildingIsEmpty");
                            base.isEnabled = false;
                        }
                        else
                        {
                            base.isEnabled = true;
                        }
                    }
                    else
                    {
                        this.BuildingPanel.Hide();
                        base.Unfocus();
                        base.isEnabled = false;
                    }
                }
                else
                {
                    base.isEnabled = false;
                    this.BuildingPanel.Hide();
                    this.BuildingID = InstanceID.Empty;
                }
            }
        }
    }
}
