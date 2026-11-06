using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class PeopleInsideBuildingsButton : UIButton
    {
        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private InstanceID buildingId = InstanceID.Empty;
        private PeopleInsideBuildingsPanel buildingPanel;
        private bool initialized;

        public override void Start()
        {
            base.Start();

            name = "PeopleInsideBuildingsButton";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(32f, 36f);
            playAudioEvents = true;
            tooltipBox = UIView.GetAView().defaultTooltipBox;
            isEnabled = false;

            if (RefPanel == null || FavCimsMainClass.FullScreenContainer == null)
            {
                Utils.Debug.Warning("Cannot initialize PeopleInsideBuildingsButton: source panel or fullscreen container is missing.");
                return;
            }

            AlignTo(RefPanel, Alignment);

            buildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent<PeopleInsideBuildingsPanel>();

            if (buildingPanel == null)
            {
                Utils.Debug.Warning("Cannot initialize PeopleInsideBuildingsButton: building panel could not be created.");
                return;
            }

            buildingPanel.RefPanel = RefPanel;
            buildingPanel.BuildingID = InstanceID.Empty;
            buildingPanel.Hide();

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
            if (FavCimsMainClass.UnLoading || !isVisible || RefPanel == null || buildingPanel == null)
            {
                DisableButton();
                return;
            }

            if (!TryGetSelectedBuilding(out InstanceID selectedBuilding, out BuildingInfo info, out bool hasCitizens))
            {
                DisableButton();
                return;
            }

            if (buildingPanel.isVisible && buildingPanel.BuildingID != selectedBuilding)
            {
                CloseBuildingPanel();
            }

            buildingId = selectedBuilding;

            if (!ApplyBuildingAppearance(selectedBuilding.Building, info))
            {
                DisableButton();
                return;
            }

            if (!hasCitizens)
            {
                isEnabled = false;
                tooltip = FavCimsLang.Text("BuildingNoOccupantsStatus");
                CloseBuildingPanel();
                return;
            }

            isEnabled = true;

            if (buildingPanel.isVisible)
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

        private static bool TryGetSelectedBuilding(out InstanceID instance, out BuildingInfo info, out bool hasCitizens)
        {
            instance = WorldInfoPanel.GetCurrentInstanceID();
            info = null;
            hasCitizens = false;

            if (instance.IsEmpty || instance.Type != InstanceType.Building || instance.Building == 0)
            {
                instance = InstanceID.Empty;
                return false;
            }

            var buildings = Singleton<BuildingManager>.instance.m_buildings.m_buffer;

            if (instance.Building >= buildings.Length)
            {
                instance = InstanceID.Empty;
                return false;
            }

            ref Building building = ref buildings[instance.Building];
            info = building.Info;

            if (info == null || info.m_class == null)
            {
                instance = InstanceID.Empty;
                return false;
            }

            hasCitizens = building.m_citizenCount != 0;
            return true;
        }

        private bool ApplyBuildingAppearance(ushort selectedBuildingId, BuildingInfo info)
        {
            var service = info.m_class.m_service;

            bool isAreaResidential = (service == ItemClass.Service.PlayerEducation || service == ItemClass.Service.PlayerIndustry) && FavCimsCore.IsAreaResidentalBuilding(selectedBuildingId);

            if (service == ItemClass.Service.Residential || isAreaResidential)
            {
                SetButtonStyle("ResidentialBuildingButton");
                tooltip = FavCimsLang.Text("BuildingViewResidentsText");
                return true;
            }

            switch (service)
            {
                case ItemClass.Service.Commercial:
                    SetButtonStyle("CommercialBuildingButton");
                    tooltip = FavCimsLang.Text("BuildingViewOccupantsText");
                    return true;

                case ItemClass.Service.Office:
                case ItemClass.Service.Industrial:
                    SetButtonStyle(service == ItemClass.Service.Industrial ? "IndustrialBuildingButton" : "CommercialBuildingButton");
                    tooltip = GetListTooltip("BuildingWorkersTitle");
                    return true;

                case ItemClass.Service.Electricity:
                case ItemClass.Service.Water:
                case ItemClass.Service.Garbage:
                case ItemClass.Service.Road:
                case ItemClass.Service.PublicTransport:
                case ItemClass.Service.PlayerIndustry:
                case ItemClass.Service.Fishing:
                    SetButtonStyle("IndustrialBuildingButton");
                    tooltip = GetListTooltip("BuildingWorkersTitle");
                    return true;

                case ItemClass.Service.Beautification:
                    SetButtonStyle("CommercialBuildingButton");
                    tooltip = GetListTooltip("BuildingVisitorsTitle");
                    return true;

                case ItemClass.Service.HealthCare:
                case ItemClass.Service.PoliceDepartment:
                case ItemClass.Service.FireDepartment:
                case ItemClass.Service.Disaster:
                case ItemClass.Service.Education:
                case ItemClass.Service.PlayerEducation:
                case ItemClass.Service.Museums:
                case ItemClass.Service.VarsitySports:
                case ItemClass.Service.ServicePoint:
                case ItemClass.Service.Hotel:
                case ItemClass.Service.Monument:
                    SetButtonStyle("CommercialBuildingButton");
                    tooltip = GetListTooltip("BuildingOccupantsTitle");
                    return true;

                default:
                    return false;
            }
        }

        private void SetButtonStyle(string spritePrefix)
        {
            normalBgSprite = spritePrefix;
            hoveredBgSprite = spritePrefix + "Hovered";
            focusedBgSprite = spritePrefix + "Hovered";
            pressedBgSprite = spritePrefix + "Hovered";
            disabledBgSprite = spritePrefix + "Disabled";
        }

        private static string GetListTooltip(string categoryKey)
        {
            return FavCimsLang.Text("MainViewListText") + FavCimsLang.Text(categoryKey);
        }

        private void OnButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (!initialized || FavCimsMainClass.UnLoading || !isVisible)
            {
                return;
            }

            RefreshState();

            if (!isEnabled || buildingId.IsEmpty || buildingPanel == null)
            {
                return;
            }

            if (buildingPanel.isVisible)
            {
                CloseBuildingPanel();
                return;
            }

            buildingPanel.BuildingID = buildingId;
            buildingPanel.RefPanel = RefPanel;
            buildingPanel.Show();

            Focus();
        }

        private void CloseBuildingPanel()
        {
            if (buildingPanel != null)
            {
                buildingPanel.Hide();
                buildingPanel.BuildingID = InstanceID.Empty;
            }

            if (hasFocus)
            {
                Unfocus();
            }
        }

        private void DisableButton()
        {
            buildingId = InstanceID.Empty;
            isEnabled = false;
            tooltip = string.Empty;

            normalBgSprite = string.Empty;
            hoveredBgSprite = string.Empty;
            focusedBgSprite = string.Empty;
            pressedBgSprite = string.Empty;
            disabledBgSprite = string.Empty;

            CloseBuildingPanel();
        }

        public override void OnDestroy()
        {
            initialized = false;
            eventClick -= OnButtonClicked;

            if (buildingPanel != null)
            {
                Object.Destroy(buildingPanel.gameObject);
                buildingPanel = null;
            }

            base.OnDestroy();
        }
    }
}