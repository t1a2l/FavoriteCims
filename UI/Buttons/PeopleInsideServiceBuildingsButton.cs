using System;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class PeopleInsideServiceBuildingsButton : UIButton
    {
        private InstanceID BuildingID = InstanceID.Empty;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private PeopleInsideServiceBuildingsPanel BuildingPanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
            name = "PeopleInsideServiceBuildingsButton";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(32f, 36f);
            playAudioEvents = true;
            AlignTo(RefPanel, Alignment);
            tooltipBox = aview.defaultTooltipBox;
            bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>() != null;
            if (flag)
            {
                BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>();
            }
            else
            {
                BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideServiceBuildingsPanel)) as PeopleInsideServiceBuildingsPanel;
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
                        switch (info.m_class.m_service)
                        {
                            case ItemClass.Service.Electricity:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.Beautification:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Guests");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.Garbage:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.HealthCare:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.PoliceDepartment:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.Education:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.Monument:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("CitizenOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.FireDepartment:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                goto IL_050D;
                            case ItemClass.Service.PublicTransport:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                goto IL_050D;
                        }
                        tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                        normalBgSprite = "CommercialBuildingButtonIcon";
                        hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                        focusedBgSprite = "CommercialBuildingButtonIconHovered";
                        pressedBgSprite = "CommercialBuildingButtonIconHovered";
                        disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                    IL_050D:
                        bool flag5 = Convert.ToInt32(MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenCount) == 0;
                        if (flag5)
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
