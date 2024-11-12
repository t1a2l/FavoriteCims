using System;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
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
            name = "PeopleInsideBuildingsButton";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(32f, 36f);
            playAudioEvents = true;
            AlignTo(RefPanel, Alignment);
            tooltipBox = aview.defaultTooltipBox;
            bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel>() != null;
            if (flag)
            {
                BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel>();
            }
            else
            {
                BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideBuildingsPanel)) as PeopleInsideBuildingsPanel;
            }
            BuildingPanel.BuildingID = InstanceID.Empty;
            BuildingPanel.Hide();
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                bool flag2 = !BuildingID.IsEmpty && !BuildingPanel.isVisible;
                if (flag2)
                {
                    BuildingPanel.BuildingID = BuildingID;
                    BuildingPanel.RefPanel = RefPanel;
                    BuildingPanel.Show();
                }
                else
                {
                    BuildingPanel.BuildingID = InstanceID.Empty;
                    BuildingPanel.Hide();
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
                    tooltip = null;
                    bool flag = WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty;
                    if (flag)
                    {
                        BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    bool flag2 = BuildingPanel != null;
                    if (flag2)
                    {
                        bool flag3 = !BuildingPanel.isVisible;
                        if (flag3)
                        {
                            Unfocus();
                        }
                        else
                        {
                            Focus();
                        }
                    }
                    bool flag4 = !BuildingID.IsEmpty && BuildingID.Type == InstanceType.Building;
                    if (flag4)
                    {
                        BuildingInfo info = MyBuilding.m_buildings.m_buffer[BuildingID.Building].Info;
                        bool flag5 = info.m_class.m_service == ItemClass.Service.Residential;
                        if (flag5)
                        {
                            tooltip = FavCimsLang.Text("Citizens_HouseHolds");
                            normalBgSprite = "BuildingButtonIcon";
                            hoveredBgSprite = "BuildingButtonIconHovered";
                            focusedBgSprite = "BuildingButtonIconHovered";
                            pressedBgSprite = "BuildingButtonIconHovered";
                            disabledBgSprite = "BuildingButtonIconDisabled";
                        }
                        else
                        {
                            bool flag6 = info.m_class.m_service == ItemClass.Service.Commercial;
                            if (flag6)
                            {
                                tooltip = FavCimsLang.Text("CitizenOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                            }
                            else
                            {
                                bool flag7 = info.m_class.m_service == ItemClass.Service.Office;
                                if (flag7)
                                {
                                    tooltip = FavCimsLang.Text("WorkersOnBuilding");
                                    normalBgSprite = "CommercialBuildingButtonIcon";
                                    hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                    focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                    pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                    disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                }
                                else
                                {
                                    tooltip = FavCimsLang.Text("WorkersOnBuilding");
                                    normalBgSprite = "IndustrialBuildingButtonIcon";
                                    hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                    focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                    pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                    disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                }
                            }
                        }
                        bool flag8 = Convert.ToInt32(MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenCount) == 0;
                        if (flag8)
                        {
                            BuildingPanel.Hide();
                            tooltip = FavCimsLang.Text("BuildingIsEmpty");
                            isEnabled = false;
                        }
                        else
                        {
                            isEnabled = true;
                        }
                    }
                    else
                    {
                        BuildingPanel.Hide();
                        Unfocus();
                        isEnabled = false;
                    }
                }
                else
                {
                    isEnabled = false;
                    BuildingPanel.Hide();
                    BuildingID = InstanceID.Empty;
                }
            }
        }
    }
}
