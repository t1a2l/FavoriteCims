using System;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.UI.Buttons;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using ICities;
using UnityEngine;

namespace FavoriteCims
{
    public partial class FavCimsMainClass : LoadingExtensionBase
    {
        public const int MaxTemplates = 5;

        public static bool UnLoading;

        public static UIPanel FullScreenContainer;

        public static UIPanel FavCimsPanel;

        public static FamilyPanelTemplate[] Templates = new FamilyPanelTemplate[MaxTemplates];

        public static UIPanel FavCimsHumanPanel;
        public static UIPanel FavCimsTouristHumanPanel;
        public static UIPanel FavCimsHumanPassengerPanel;
        public static UIPanel FavCimsHumanPublicTransportPanel;
        public static UIPanel FavCimsPublicTransportTaxiPanel;

        public static UIPanel FavCimsPeopleZonedBuildingPanel;
        public static UIPanel FavCimsPeopleServiceBuildingPanel;
        public static UIPanel FavCimsPeopleFootballEventBuildingPanel;
        public static UIPanel FavCimsPeopleVarsitySportsArenaBuildingPanel;
        public static UIPanel FavCimsPeopleWarehouseBuildingPanel;
        public static UIPanel FavCimsPeopleShelterBuildingPanel;
        public static UIPanel FavCimsPeopleUniqueFactoryBuildingPanel;
        public static UIPanel FavCimsPeopleHotelBuildingPanel;
        public static UIPanel FavCimsPeopleServiceResidentBuildingPanel;

        public static UIButton mainButton;

        private readonly MyAtlas Atlas = new();

        private AddToFavButton favoriteButton;
        private AddToFavButton favoriteTouristButton;

        private VehiclePassengersButton passengerButton;
        private VehiclePassengersButtonPT publicTransportPassengersButton;
        private VehiclePassengersButtonPT taxiPassengersButton;

        private PeopleInsideBuildingsButton peopleZonedBuildingButton;
        private PeopleInsideBuildingsButton peopleServiceBuildingButton;
        private PeopleInsideBuildingsButton peopleFootballEventBuildingButton;
        private PeopleInsideBuildingsButton peopleVarsitySportsArenaBuildingButton;
        private PeopleInsideBuildingsButton peopleWarehouseBuildingButton;
        private PeopleInsideBuildingsButton peopleShelterBuildingButton;
        private PeopleInsideBuildingsButton peopleUniqueFactoryBuildingButton;
        private PeopleInsideBuildingsButton peopleHotelBuildingButton;

        private UIGroupPanel groupPanel;

        private UIComponent panelTimeTrigger;
        private UIComponent chirperTrigger;
        private UIComponent escTrigger;
        private UIComponent infoPanelTrigger;
        private UIComponent bottomBarTrigger;

        private MouseEventHandler chirperClickHandler;
        private MouseEventHandler escClickHandler;
        private MouseEventHandler infoPanelClickHandler;
        private MouseEventHandler bottomBarClickHandler;
        private MouseEventHandler mainButtonClickHandler;
        private MouseEventHandler fullScreenContainerMouseDownHandler;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
            {
                return;
            }

            UnLoading = false;
            CreateGraphics();
        }

        public override void OnLevelUnloading()
        {
            DestroyGraphics();
        }

        internal void GenerateFamilyDetailsTemplates()
        {
            for (int i = 0; i < MaxTemplates; i++)
            {
                string templateName = "FavCimsFamilyTemplate" + i;

                var existingTemplate = FullScreenContainer.Find<FamilyPanelTemplate>(templateName);

                if (existingTemplate != null)
                {
                    Templates[i] = existingTemplate;
                }
                else
                {
                    Templates[i] = FullScreenContainer.AddUIComponent<FamilyPanelTemplate>();
                    Templates[i].name = templateName;
                }

                Templates[i].MyInstanceID = InstanceID.Empty;
                Templates[i].Hide();
            }
        }

        public static void FavCimsPanelToggle()
        {
            if (FavCimsPanel == null)
            {
                return;
            }

            if (!FavCimsPanel.isVisible)
            {
                FavCimsPanel.CenterTo(FullScreenContainer);
                FavCimsPanel.Show();
            }
            else
            {
                FavCimsPanel.Hide();
            }
        }

        public void FavCimsPanelOff()
        {
            if (FavCimsPanel == null || !FavCimsPanel.isVisible || FavCimsPanel.containsMouse || (mainButton != null && mainButton.containsMouse) || (panelTimeTrigger != null && panelTimeTrigger.containsMouse))
            {
                return;
            }

            FavCimsPanel.Hide();
        }

        internal void CreateGraphics()
        {
            try
            {
                if (GameObject.Find("FavCimsMenuPanel") != null)
                {
                    return;
                }

                FindTriggers();
                SubscribeTriggerHandlers();

                TextureDB.LoadFavCimsTextures();
                Atlas.LoadAtlasIcons();

                CreateToolbarButton();
                CreateMainPanel();
                CreateCitizenButtons();
                CreateVehicleButtons();
                CreateBuildingButtons();
                GenerateFamilyDetailsTemplates();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("OnLoad List Error: " + ex);
            }
        }

        private void FindTriggers()
        {
            chirperTrigger = UIView.Find("ChirperPanel");
            escTrigger = UIView.Find("Esc");
            infoPanelTrigger = UIView.Find("InfoPanel");
            bottomBarTrigger = UIView.Find("TSBar");
            panelTimeTrigger = UIView.Find("PanelTime");
        }

        private void SubscribeTriggerHandlers()
        {
            chirperClickHandler = OnUiTriggerClicked;
            escClickHandler = OnUiTriggerClicked;
            infoPanelClickHandler = OnUiTriggerClicked;
            bottomBarClickHandler = OnUiTriggerClicked;

            if (chirperTrigger != null && panelTimeTrigger != null)
            {
                chirperTrigger.eventClick += chirperClickHandler;
            }

            if (escTrigger != null && panelTimeTrigger != null)
            {
                escTrigger.eventClick += escClickHandler;
            }

            if (infoPanelTrigger != null && panelTimeTrigger != null)
            {
                infoPanelTrigger.eventClick += infoPanelClickHandler;
            }

            if (bottomBarTrigger != null && panelTimeTrigger != null)
            {
                bottomBarTrigger.eventClick += bottomBarClickHandler;
            }
        }

        private void OnUiTriggerClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            FavCimsPanelOff();
        }

        private void CreateToolbarButton()
        {
            var tabstrip = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();

            if (tabstrip == null)
            {
                Utils.Debug.Warning("Favorite Cims could not find the main toolbar tab strip.");
                return;
            }

            GameObject buttonTemplate = UITemplateManager.GetAsGameObject("MainToolbarButtonTemplate");
            GameObject panelTemplate = UITemplateManager.GetAsGameObject("ScrollableSubPanelTemplate");

            mainButton = tabstrip.AddTab("FavCimsMenuPanel", buttonTemplate, panelTemplate, [typeof(UIGroupPanel)]) as UIButton;

            if (mainButton == null)
            {
                Utils.Debug.Warning("Favorite Cims could not create its toolbar button.");
                return;
            }

            mainButton.normalBgSprite = "FavoriteCimsButton";
            mainButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
            mainButton.focusedBgSprite = "FavoriteCimsButtonFocused";
            mainButton.pressedBgSprite = "FavoriteCimsButtonPressed";
            mainButton.playAudioEvents = true;
            mainButton.name = "FavCimsButton";
            mainButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            mainButton.atlas = MyAtlas.FavCimsAtlas;
            mainButton.size = new Vector2(49f, 49f);
            mainButton.tooltip = "Favorite Cims " + FavoriteCimsModMain.Version;

            mainButtonClickHandler = OnMainButtonClicked;
            mainButton.eventClick += mainButtonClickHandler;

            AddToolbarLocaleEntries();
            IncrementToolbarObjectIndex();

            groupPanel = tabstrip.GetComponentInContainer(mainButton, typeof(UIGroupPanel)) as UIGroupPanel;

            if (groupPanel != null)
            {
                groupPanel.name = "FavCimsTabMenuPanel";
                groupPanel.enabled = true;
                groupPanel.component.isInteractive = true;
                groupPanel.m_OptionsBar = ToolsModifierControl.mainToolbar.m_OptionsBar;
                groupPanel.m_DefaultInfoTooltipAtlas = ToolsModifierControl.mainToolbar.m_DefaultInfoTooltipAtlas;

                if (ToolsModifierControl.mainToolbar.enabled)
                {
                    groupPanel.RefreshPanel();
                }
            }

            FullScreenContainer = UIView.Find("FullScreenContainer") as UIPanel;
        }

        private void OnMainButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            FavCimsPanelToggle();
        }

        private void AddToolbarLocaleEntries()
        {
            var locale = (Locale)typeof(LocaleManager).GetField("m_Locale", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(SingletonLite<LocaleManager>.instance);

            AddLocaleString(locale, "TUTORIAL_ADVISER_TITLE", mainButton.name, "Favorite Cims " + FavoriteCimsModMain.Version);

            AddLocaleString(locale, "TUTORIAL_ADVISER", mainButton.name,
                "Thanks for subscribing to Favorite Cims!\n\n"
                + "Hotkey = Press Middle Mouse Button + F\n"
                + "If you like the mod please consider leaving a rating on the Steam Workshop.\n"
                + "Mod Version: " + FavoriteCimsModMain.Version + " by t1a2l");
        }

        private static void AddLocaleString(Locale locale, string identifier, string key, string value)
        {
            var localeKey = default(Locale.Key);
            localeKey.m_Identifier = identifier;
            localeKey.m_Key = key;

            if (!locale.Exists(localeKey))
            {
                locale.AddLocalizedString(localeKey, value);
            }
        }

        private void IncrementToolbarObjectIndex()
        {
            FieldInfo objectIndexField = typeof(MainToolbar).GetField("m_ObjectIndex", BindingFlags.Instance | BindingFlags.NonPublic);

            objectIndexField?.SetValue(ToolsModifierControl.mainToolbar, (int)objectIndexField.GetValue(ToolsModifierControl.mainToolbar) + 1);
        }

        private void CreateMainPanel()
        {
            if (FullScreenContainer == null)
            {
                Utils.Debug.Warning("Favorite Cims could not find FullScreenContainer.");
                return;
            }

            FavCimsPanel = FullScreenContainer.AddUIComponent<FavoriteCimsMainPanel>();
            FavCimsPanel.Hide();

            fullScreenContainerMouseDownHandler = OnFullScreenContainerMouseDown;
            FullScreenContainer.eventMouseDown += fullScreenContainerMouseDownHandler;
        }

        private void OnFullScreenContainerMouseDown(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (FavCimsPanel == null)
            {
                return;
            }

            if (!FavCimsPanel.containsMouse)
            {
                FavCimsPanel.SendToBack();
            }
            else
            {
                FavCimsPanel.BringToFront();
            }
        }

        private void CreateCitizenButtons()
        {
            FavCimsHumanPanel = FullScreenContainer.Find("(Library) CitizenWorldInfoPanel") as UIPanel;

            if (FavCimsHumanPanel != null)
            {
                favoriteButton = GetOrCreateButton<AddToFavButton>(FavCimsHumanPanel);
                favoriteButton.RefPanel = FavCimsHumanPanel;
                favoriteButton.Alignment = UIAlignAnchor.BottomRight;
            }

            FavCimsTouristHumanPanel = FullScreenContainer.Find("(Library) TouristWorldInfoPanel") as UIPanel;

            if (FavCimsTouristHumanPanel != null)
            {
                favoriteTouristButton = GetOrCreateButton<AddToFavButton>(FavCimsTouristHumanPanel);
                favoriteTouristButton.RefPanel = FavCimsTouristHumanPanel;
                favoriteTouristButton.Alignment = UIAlignAnchor.BottomRight;
            }
        }

        private void CreateVehicleButtons()
        {
            FavCimsHumanPassengerPanel = FullScreenContainer.Find("(Library) CitizenVehicleWorldInfoPanel") as UIPanel;

            if (FavCimsHumanPassengerPanel != null)
            {
                passengerButton = GetOrCreateButton<VehiclePassengersButton>(FavCimsHumanPassengerPanel);
                passengerButton.RefPanel = FavCimsHumanPassengerPanel;
                passengerButton.Alignment = UIAlignAnchor.BottomRight;
            }

            FavCimsHumanPublicTransportPanel = FullScreenContainer.Find("(Library) PublicTransportVehicleWorldInfoPanel") as UIPanel;

            if (FavCimsHumanPublicTransportPanel != null)
            {
                publicTransportPassengersButton = GetOrCreateButton<VehiclePassengersButtonPT>(FavCimsHumanPublicTransportPanel);
                publicTransportPassengersButton.RefPanel = FavCimsHumanPublicTransportPanel;
                publicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
            }

            FavCimsPublicTransportTaxiPanel = FullScreenContainer.Find("(Library) CityServiceVehicleWorldInfoPanel") as UIPanel;

            if (FavCimsPublicTransportTaxiPanel != null)
            {
                taxiPassengersButton = GetOrCreateButton<VehiclePassengersButtonPT>(FavCimsPublicTransportTaxiPanel);
                taxiPassengersButton.RefPanel = FavCimsPublicTransportTaxiPanel;
                taxiPassengersButton.Alignment = UIAlignAnchor.BottomRight;
            }
        }

        private void CreateBuildingButtons()
        {
            peopleZonedBuildingButton = CreateBuildingButton("(Library) ZonedBuildingWorldInfoPanel", ref FavCimsPeopleZonedBuildingPanel, ref peopleZonedBuildingButton);

            peopleServiceBuildingButton = CreateBuildingButton("(Library) CityServiceWorldInfoPanel", ref FavCimsPeopleServiceBuildingPanel, ref peopleServiceBuildingButton);

            peopleFootballEventBuildingButton = CreateBuildingButton("(Library) FootballPanel", ref FavCimsPeopleFootballEventBuildingPanel, ref peopleFootballEventBuildingButton);

            peopleVarsitySportsArenaBuildingButton = CreateBuildingButton("(Library) VarsitySportsArenaPanel", ref FavCimsPeopleVarsitySportsArenaBuildingPanel, ref peopleVarsitySportsArenaBuildingButton);

            peopleWarehouseBuildingButton = CreateBuildingButton("(Library) WarehouseWorldInfoPanel", ref FavCimsPeopleWarehouseBuildingPanel, ref peopleWarehouseBuildingButton);

            peopleShelterBuildingButton = CreateBuildingButton("(Library) ShelterWorldInfoPanel", ref FavCimsPeopleShelterBuildingPanel, ref peopleShelterBuildingButton);

            peopleUniqueFactoryBuildingButton = CreateBuildingButton("(Library) UniqueFactoryWorldInfoPanel", ref FavCimsPeopleUniqueFactoryBuildingPanel, ref peopleUniqueFactoryBuildingButton);

            peopleHotelBuildingButton = CreateBuildingButton("(Library) HotelWorldInfoPanel", ref FavCimsPeopleHotelBuildingPanel, ref peopleHotelBuildingButton);
        }

        private PeopleInsideBuildingsButton CreateBuildingButton(string panelName, ref UIPanel sourcePanel, ref PeopleInsideBuildingsButton button)
        {
            sourcePanel = FullScreenContainer.Find<UIPanel>(panelName);

            if (sourcePanel == null)
            {
                return null;
            }

            button = GetOrCreateButton<PeopleInsideBuildingsButton>(sourcePanel);
            button.RefPanel = sourcePanel;
            button.Alignment = UIAlignAnchor.BottomRight;

            return button;
        }

        private static TButton GetOrCreateButton<TButton>(UIPanel sourcePanel) where TButton : UIComponent
        {
            var existingButton = sourcePanel.GetComponentInChildren<TButton>();
            return existingButton ?? sourcePanel.AddUIComponent<TButton>();
        }

        internal void DestroyGraphics()
        {
            UnLoading = true;

            UnsubscribeTriggerHandlers();

            try
            {
                DestroyWorldInfoButtons();

                DestroyIfNotNull(FavCimsPanel);
                DestroyIfNotNull(mainButton);

                for (int i = 0; i < Templates.Length; i++)
                {
                    if (Templates[i] != null)
                    {
                        UnityEngine.Object.Destroy(Templates[i].gameObject);
                    }

                    Templates[i] = null;
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to destroy Favorite Cims graphics: " + ex);
            }
            finally
            {
                FavCimsPanel = null;
                mainButton = null;
                FullScreenContainer = null;

                favoriteButton = null;
                favoriteTouristButton = null;
                passengerButton = null;
                publicTransportPassengersButton = null;
                taxiPassengersButton = null;

                peopleZonedBuildingButton = null;
                peopleServiceBuildingButton = null;
                peopleFootballEventBuildingButton = null;
                peopleVarsitySportsArenaBuildingButton = null;
                peopleWarehouseBuildingButton = null;
                peopleShelterBuildingButton = null;
                peopleUniqueFactoryBuildingButton = null;
                peopleHotelBuildingButton = null;

                groupPanel = null;

                panelTimeTrigger = null;
                chirperTrigger = null;
                escTrigger = null;
                infoPanelTrigger = null;
                bottomBarTrigger = null;

                FavCimsHumanPanel = null;
                FavCimsTouristHumanPanel = null;
                FavCimsHumanPassengerPanel = null;
                FavCimsHumanPublicTransportPanel = null;
                FavCimsPublicTransportTaxiPanel = null;

                FavCimsPeopleZonedBuildingPanel = null;
                FavCimsPeopleServiceBuildingPanel = null;
                FavCimsPeopleFootballEventBuildingPanel = null;
                FavCimsPeopleVarsitySportsArenaBuildingPanel = null;
                FavCimsPeopleWarehouseBuildingPanel = null;
                FavCimsPeopleShelterBuildingPanel = null;
                FavCimsPeopleUniqueFactoryBuildingPanel = null;
                FavCimsPeopleHotelBuildingPanel = null;
                FavCimsPeopleServiceResidentBuildingPanel = null;
            }

            FavCimsCore.ClearIdArray();
        }

        private void UnsubscribeTriggerHandlers()
        {
            if (chirperTrigger != null && chirperClickHandler != null)
            {
                chirperTrigger.eventClick -= chirperClickHandler;
            }

            if (escTrigger != null && escClickHandler != null)
            {
                escTrigger.eventClick -= escClickHandler;
            }

            if (infoPanelTrigger != null && infoPanelClickHandler != null)
            {
                infoPanelTrigger.eventClick -= infoPanelClickHandler;
            }

            if (bottomBarTrigger != null && bottomBarClickHandler != null)
            {
                bottomBarTrigger.eventClick -= bottomBarClickHandler;
            }

            if (mainButton != null && mainButtonClickHandler != null)
            {
                mainButton.eventClick -= mainButtonClickHandler;
            }

            if (FullScreenContainer != null && fullScreenContainerMouseDownHandler != null)
            {
                FullScreenContainer.eventMouseDown -= fullScreenContainerMouseDownHandler;
            }
        }

        private static void DestroyIfNotNull(UIComponent component)
        {
            if (component != null)
            {
                UnityEngine.Object.Destroy(component.gameObject);
            }
        }

        public class UIGroupPanel : GeneratedGroupPanel
        {
            public override ItemClass.Service service => 0;

            public override string serviceName => "FavoriteCims";

            protected override bool IsServiceValid(PrefabInfo info)
            {
                return true;
            }

            public void Update()
            {
                if (Input.GetMouseButton(2) && Input.GetKeyDown(KeyCode.F))
                {
                    FavCimsPanelToggle();
                }

                var panel = GameObject.Find("FavCimsTabMenuPanel")?.GetComponent<UIPanel>();

                if (panel != null && panel.isVisible)
                {
                    panel.Hide();
                }
            }
        }
    }
}