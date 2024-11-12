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
            if (FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>() != null)
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
                if (!BuildingID.IsEmpty && !BuildingPanel.isVisible)
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
                    if (WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty)
                    {
                        BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    if (BuildingPanel != null)
                    {
                        if (!BuildingPanel.isVisible)
                        {
                            Unfocus();
                        }
                        else
                        {
                            Focus();
                        }
                    }
                    if (!BuildingID.IsEmpty && BuildingID.Type == InstanceType.Building)
                    {
                        BuildingInfo info = MyBuilding.m_buildings.m_buffer[BuildingID.Building].Info;
                        bool found_service = false;
                        switch (info.m_class.m_service)
                        {
                            case ItemClass.Service.Electricity:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.Beautification:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Guests");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.Garbage:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.HealthCare:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.PoliceDepartment:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.Education:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.Monument:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("CitizenOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.FireDepartment:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                            case ItemClass.Service.PublicTransport:
                                tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                found_service = true;
                                break;
                        }
                        if(!found_service)
                        {
                            tooltip = FavCimsLang.Text("View_List") + " " + FavCimsLang.Text("OnBuilding_Workers");
                            normalBgSprite = "CommercialBuildingButtonIcon";
                            hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                            focusedBgSprite = "CommercialBuildingButtonIconHovered";
                            pressedBgSprite = "CommercialBuildingButtonIconHovered";
                            disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                        }
                        else
                        {
                            if (Convert.ToInt32(MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenCount) == 0)
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
