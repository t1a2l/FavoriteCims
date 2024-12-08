using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using System;
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
            BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideBuildingsPanel)) as PeopleInsideBuildingsPanel;
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
                BuildingPanel.UpdateList();
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

                        tooltip = "";
                        normalBgSprite = "";
                        hoveredBgSprite = "";
                        focusedBgSprite = "";
                        pressedBgSprite = "";
                        disabledBgSprite = "";

                        switch (info.m_class.m_service)
                        {
                            case ItemClass.Service.Residential:
                            case ItemClass.Service.PlayerEducation when FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                            case ItemClass.Service.PlayerIndustry when FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                                tooltip = Translations.Translate("Citizens_HouseHolds");
                                normalBgSprite = "BuildingButtonIcon";
                                hoveredBgSprite = "BuildingButtonIconHovered";
                                focusedBgSprite = "BuildingButtonIconHovered";
                                pressedBgSprite = "BuildingButtonIconHovered";
                                disabledBgSprite = "BuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Commercial:
                                tooltip = Translations.Translate("CitizenOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Office:
                                tooltip = Translations.Translate("WorkersOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Industrial:
                                tooltip = Translations.Translate("WorkersOnBuilding");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Electricity:
                            case ItemClass.Service.Water:
                            case ItemClass.Service.Garbage:
                            case ItemClass.Service.Road:
                            case ItemClass.Service.PublicTransport:
                            case ItemClass.Service.PlayerIndustry when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                            case ItemClass.Service.Fishing:
                                tooltip = Translations.Translate("View_List") + " " + Translations.Translate("OnBuilding_Workers");
                                normalBgSprite = "IndustrialBuildingButtonIcon";
                                hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
                                focusedBgSprite = "IndustrialBuildingButtonIconHovered";
                                pressedBgSprite = "IndustrialBuildingButtonIconHovered";
                                disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Beautification:
                                tooltip = Translations.Translate("View_List") + " " + Translations.Translate("OnBuilding_Guests");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.HealthCare:
                            case ItemClass.Service.PoliceDepartment:
                            case ItemClass.Service.FireDepartment:
                            case ItemClass.Service.Disaster:
                            case ItemClass.Service.Education:
                            case ItemClass.Service.PlayerEducation when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                            case ItemClass.Service.Museums:
                            case ItemClass.Service.VarsitySports:
                            case ItemClass.Service.ServicePoint:
                            case ItemClass.Service.Hotel:
                                tooltip = Translations.Translate("View_List") + " " + Translations.Translate("OnBuilding_Workers");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                break;
                            case ItemClass.Service.Monument:
                                tooltip = Translations.Translate("View_List") + " " + Translations.Translate("CitizenOnBuilding");
                                normalBgSprite = "CommercialBuildingButtonIcon";
                                hoveredBgSprite = "CommercialBuildingButtonIconHovered";
                                focusedBgSprite = "CommercialBuildingButtonIconHovered";
                                pressedBgSprite = "CommercialBuildingButtonIconHovered";
                                disabledBgSprite = "CommercialBuildingButtonIconDisabled";
                                break;
                        }

                        if (Convert.ToInt32(MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenCount) == 0)
                        {
                            BuildingPanel.Hide();
                            tooltip = Translations.Translate("BuildingIsEmpty");
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
