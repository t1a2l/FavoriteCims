using AlgernonCommons;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.UI.Buttons;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using ICities;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FavoriteCims
{
    public sealed class FavCimsMainClass : LoadingBase<OptionsPanel>
    {
        public UIView uiView;
        public static bool UnLoading => !IsLoaded;

        private readonly MyAtlas Atlas = new();

        public const int MaxTemplates = 5;

        public static FamilyPanelTemplate[] Templates = new FamilyPanelTemplate[5];

        public static UIPanel FullScreenContainer;

        public static UIPanel FavCimsHumanPanel;

        public static UIPanel FavCimsTouristHumanPanel;

        private AddToFavButton FavStarButton;

        private AddToFavButton FavStarTouristButton;

        public static UIPanel FavCimsHumanPassengerPanel;

        private VehiclePassengersButton PassengerButton;

        public static UIPanel FavCimsHumanPublicTransportPanel;

        private VehiclePassengersButtonPT PublicTransportPassengersButton;

        public static UIButton mainButton;

        private UIGroupPanel m_groupPanel;

        public static UIPanel FavCimsPeopleZonedBuildingPanel;

        public static UIPanel FavCimsPeopleServiceBuildingPanel;

        public static UIPanel FavCimsPeopleFootballEventBuildingPanel;

        public static UIPanel FavCimsPeopleVarsitySportsArenaBuildingPanel;

        public static UIPanel FavCimsPeopleWarehouseBuildingPanel;

        public static UIPanel FavCimsPeopleShelterBuildingPanel;

        public static UIPanel FavCimsPeopleUniqueFactoryBuildingPanel;

        public static UIPanel FavCimsPeopleHotelBuildingPanel;

        private PeopleInsideBuildingsButton PeopleZonedBuildingButton;

        private PeopleInsideBuildingsButton PeopleServiceBuildingButton;

        private PeopleInsideBuildingsButton PeopleFootballEventBuildingButton;

        private PeopleInsideBuildingsButton PeopleVarsitySportsArenaBuildingButton;

        private PeopleInsideBuildingsButton PeopleWarehouseBuildingButton;

        private PeopleInsideBuildingsButton PeopleShelterBuildingButton;

        private PeopleInsideBuildingsButton PeopleUniqueFactoryBuildingButton;

        private PeopleInsideBuildingsButton PeopleHotelBuildingButton;

        public static UIPanel FavCimsPublicTransportTaxiPanel;

        public static UIPanel FavCimsPeopleServiceResidentBuildingPanel;

        private UIComponent FavCimsPanelTrigger_paneltime;

        private UIComponent FavCimsPanelTrigger_chirper;

        private UIComponent FavCimsPanelTrigger_esc;

        private UIComponent FavCimsPanelTrigger_infopanel;

        private UIComponent FavCimsPanelTrigger_bottombars;

        public static UIPanel FavCimsPanel;

        protected override List<AppMode> PermittedModes => new List<AppMode>() { AppMode.Game };
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CreateGraphics();
        }

        internal void GenerateFamilyDetailsTpl()
        {
            for (int i = 0; i < 5; i++)
            {
                if (FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i.ToString()) != null)
                {
                    Templates[i] = FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i.ToString());
                    Templates[i].MyInstanceID = InstanceID.Empty;
                    Templates[i].Hide();
                }
                else
                {
                    Templates[i] = FullScreenContainer.AddUIComponent(typeof(FamilyPanelTemplate)) as FamilyPanelTemplate;
                    Templates[i].name = "FavCimsFamilyTemplate_" + i.ToString();
                    Templates[i].MyInstanceID = InstanceID.Empty;
                    Templates[i].Hide();
                }
            }
        }

        public static void FavCimsPanelToggle()
        {
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
            if (FavCimsPanel.isVisible && !FavCimsPanel.containsMouse && !mainButton.containsMouse && FavCimsPanelTrigger_paneltime != null && !FavCimsPanelTrigger_paneltime.containsMouse)
            {
                FavCimsPanel.Hide();
            }
        }

        internal void CreateGraphics()
        {
            try
            {
                GameObject gameObject = GameObject.Find("FavCimsMenuPanel");
                if (gameObject != null)
                {
                    return;
                }
                FavCimsPanelTrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
                FavCimsPanelTrigger_esc = UIView.Find<UIButton>("Esc");
                FavCimsPanelTrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
                FavCimsPanelTrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
                FavCimsPanelTrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
                if (FavCimsPanelTrigger_chirper != null && FavCimsPanelTrigger_paneltime != null)
                {
                    FavCimsPanelTrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsPanelOff();
                    };
                }
                if (FavCimsPanelTrigger_esc != null && FavCimsPanelTrigger_paneltime != null)
                {
                    FavCimsPanelTrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsPanelOff();
                    };
                }
                if (FavCimsPanelTrigger_infopanel != null && FavCimsPanelTrigger_paneltime != null)
                {
                    FavCimsPanelTrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsPanelOff();
                    };
                }
                if (FavCimsPanelTrigger_bottombars != null && FavCimsPanelTrigger_paneltime != null)
                {
                    FavCimsPanelTrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsPanelOff();
                    };
                }
                UIView aview = UIView.GetAView();
                TextureDB.LoadFavCimsTextures();
                Atlas.LoadAtlasIcons();
                UITabstrip tabstrip = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();
                if (tabstrip.Find("FavCimsMenuPanel") || GameObject.Find("MainToolbarButtonTemplate") || GameObject.Find("ScrollableSubPanelTemplate"))
                {
                    return;
                }

                GameObject asGameObject = UITemplateManager.GetAsGameObject("MainToolbarButtonTemplate");
                GameObject asGameObject2 = UITemplateManager.GetAsGameObject("ScrollableSubPanelTemplate");
                mainButton = tabstrip.AddTab("FavCimsMenuPanel", asGameObject, asGameObject2, [typeof(UIGroupPanel)]) as UIButton;
                mainButton.normalBgSprite = "FavoriteCimsButton";
                mainButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
                mainButton.focusedBgSprite = "FavoriteCimsButtonFocused";
                mainButton.pressedBgSprite = "FavoriteCimsButtonPressed";
                mainButton.playAudioEvents = true;
                mainButton.name = "FavCimsButton";
                mainButton.tooltipBox = aview.defaultTooltipBox;
                mainButton.atlas = MyAtlas.FavCimsAtlas;
                mainButton.size = new Vector2(49f, 49f);
                mainButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    FavCimsPanelToggle();
                };
                mainButton.tooltip = "Favorite Cims v0.4a";
                Locale locale = (Locale)typeof(LocaleManager).GetField("m_Locale", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(SingletonLite<LocaleManager>.instance);
                Locale.Key key = default;
                key.m_Identifier = "TUTORIAL_ADVISER_TITLE";
                key.m_Key = mainButton.name;
                Locale.Key key2 = key;
                if (!locale.Exists(key2))
                {
                    locale.AddLocalizedString(key2, "Favorite Cims v0.4a");
                }
                key = default;
                key.m_Identifier = "TUTORIAL_ADVISER";
                key.m_Key = mainButton.name;
                key2 = key;
                if (!locale.Exists(key2))
                {
                    locale.AddLocalizedString(key2, "Thanks for subscribing to Favorite Cims!\n\nHotkey = Press Middle Mouse Button + F \nIf you like the mod please consider leaving a rating on the steam workshop.\nMod Version : v0.4a by Gianxs");
                }
                FieldInfo field = typeof(MainToolbar).GetField("m_ObjectIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(ToolsModifierControl.mainToolbar, (int)field.GetValue(ToolsModifierControl.mainToolbar) + 1);
                m_groupPanel = tabstrip.GetComponentInContainer(mainButton, typeof(UIGroupPanel)) as UIGroupPanel;
                if (m_groupPanel != null)
                {
                    m_groupPanel.name = "FavCimsTabMenuPanel";
                    m_groupPanel.enabled = true;
                    m_groupPanel.component.isInteractive = true;
                    m_groupPanel.m_OptionsBar = ToolsModifierControl.mainToolbar.m_OptionsBar;
                    m_groupPanel.m_DefaultInfoTooltipAtlas = ToolsModifierControl.mainToolbar.m_DefaultInfoTooltipAtlas;
                    bool enabled = ToolsModifierControl.mainToolbar.enabled;
                    if (enabled)
                    {
                        m_groupPanel.RefreshPanel();
                    }
                }
                FullScreenContainer = UIView.Find<UIPanel>("FullScreenContainer");
                FavCimsPanel = FullScreenContainer.AddUIComponent<FavoriteCimsMainPanel>();
                FavCimsPanel.Hide();
                FullScreenContainer.eventMouseDown += delegate
                {
                    if (!FavCimsPanel.containsMouse)
                    {
                        FavCimsPanel.SendToBack();
                    }
                    else
                    {
                        FavCimsPanel.BringToFront();
                    }
                };
                FavCimsHumanPanel = FullScreenContainer.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
                if (FavCimsHumanPanel != null)
                {
                    if (FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>() != null)
                    {
                        FavStarButton = FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>();
                    }
                    else
                    {
                        FavStarButton = FavCimsHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
                    }
                    FavStarButton.RefPanel = FavCimsHumanPanel;
                    FavStarButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsTouristHumanPanel = FullScreenContainer.Find<UIPanel>("(Library) TouristWorldInfoPanel");
                if (FavCimsTouristHumanPanel != null)
                {
                    if (FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>() != null)
                    {
                        FavStarTouristButton = FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>();
                    }
                    else
                    {
                        FavStarTouristButton = FavCimsTouristHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
                    }
                    FavStarTouristButton.RefPanel = FavCimsTouristHumanPanel;
                    FavStarTouristButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsHumanPassengerPanel = FullScreenContainer.Find<UIPanel>("(Library) CitizenVehicleWorldInfoPanel");
                if (FavCimsHumanPassengerPanel != null)
                {
                    if (FavCimsHumanPassengerPanel.GetComponentInChildren<VehiclePassengersButton>() != null)
                    {
                        PassengerButton = FavCimsHumanPassengerPanel.GetComponentInChildren<VehiclePassengersButton>();
                    }
                    else
                    {
                        PassengerButton = FavCimsHumanPassengerPanel.AddUIComponent(typeof(VehiclePassengersButton)) as VehiclePassengersButton;
                    }
                    PassengerButton.RefPanel = FavCimsHumanPassengerPanel;
                    PassengerButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsHumanPublicTransportPanel = FullScreenContainer.Find<UIPanel>("(Library) PublicTransportVehicleWorldInfoPanel");
                if (FavCimsHumanPublicTransportPanel != null)
                {
                    if (FavCimsHumanPublicTransportPanel.GetComponentInChildren<VehiclePassengersButtonPT>() != null)
                    {
                        PublicTransportPassengersButton = FavCimsHumanPublicTransportPanel.GetComponentInChildren<VehiclePassengersButtonPT>();
                    }
                    else
                    {
                        PublicTransportPassengersButton = FavCimsHumanPublicTransportPanel.AddUIComponent(typeof(VehiclePassengersButtonPT)) as VehiclePassengersButtonPT;
                    }
                    PublicTransportPassengersButton.RefPanel = FavCimsHumanPublicTransportPanel;
                    PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPublicTransportTaxiPanel = FullScreenContainer.Find<UIPanel>("(Library) CityServiceVehicleWorldInfoPanel");
                if (FavCimsPublicTransportTaxiPanel != null)
                {
                    if (FavCimsPublicTransportTaxiPanel.GetComponentInChildren<VehiclePassengersButtonPT>() != null)
                    {
                        PublicTransportPassengersButton = FavCimsPublicTransportTaxiPanel.GetComponentInChildren<VehiclePassengersButtonPT>();
                    }
                    else
                    {
                        PublicTransportPassengersButton = FavCimsPublicTransportTaxiPanel.AddUIComponent(typeof(VehiclePassengersButtonPT)) as VehiclePassengersButtonPT;
                    }
                    PublicTransportPassengersButton.RefPanel = FavCimsPublicTransportTaxiPanel;
                    PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleZonedBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
                if (FavCimsPeopleZonedBuildingPanel != null)
                {
                    if (FavCimsPeopleZonedBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleZonedBuildingButton = FavCimsPeopleZonedBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleZonedBuildingButton = FavCimsPeopleZonedBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleZonedBuildingButton.RefPanel = FavCimsPeopleZonedBuildingPanel;
                    PeopleZonedBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleServiceBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
                if (FavCimsPeopleServiceBuildingPanel != null)
                {
                    if (FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleServiceBuildingButton = FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleServiceBuildingButton = FavCimsPeopleServiceBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleServiceBuildingButton.RefPanel = FavCimsPeopleServiceBuildingPanel;
                    PeopleServiceBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleVarsitySportsArenaBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) VarsitySportsArenaPanel");
                if (FavCimsPeopleVarsitySportsArenaBuildingPanel != null)
                {
                    if (FavCimsPeopleVarsitySportsArenaBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleVarsitySportsArenaBuildingButton = FavCimsPeopleVarsitySportsArenaBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleVarsitySportsArenaBuildingButton = FavCimsPeopleVarsitySportsArenaBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleVarsitySportsArenaBuildingButton.RefPanel = FavCimsPeopleVarsitySportsArenaBuildingPanel;
                    PeopleVarsitySportsArenaBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleFootballEventBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) FootballPanel");
                if (FavCimsPeopleFootballEventBuildingPanel != null)
                {
                    if (FavCimsPeopleFootballEventBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleFootballEventBuildingButton = FavCimsPeopleFootballEventBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleFootballEventBuildingButton = FavCimsPeopleFootballEventBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleFootballEventBuildingButton.RefPanel = FavCimsPeopleFootballEventBuildingPanel;
                    PeopleFootballEventBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleUniqueFactoryBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) UniqueFactoryWorldInfoPanel");
                if (FavCimsPeopleUniqueFactoryBuildingPanel != null)
                {
                    if (FavCimsPeopleUniqueFactoryBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleUniqueFactoryBuildingButton = FavCimsPeopleUniqueFactoryBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleUniqueFactoryBuildingButton = FavCimsPeopleUniqueFactoryBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleUniqueFactoryBuildingButton.RefPanel = FavCimsPeopleUniqueFactoryBuildingPanel;
                    PeopleUniqueFactoryBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleShelterBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) ShelterWorldInfoPanel");
                if (FavCimsPeopleShelterBuildingPanel != null)
                {
                    if (FavCimsPeopleShelterBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleShelterBuildingButton = FavCimsPeopleShelterBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleShelterBuildingButton = FavCimsPeopleShelterBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleShelterBuildingButton.RefPanel = FavCimsPeopleShelterBuildingPanel;
                    PeopleShelterBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleHotelBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) HotelWorldInfoPanel");
                if (FavCimsPeopleHotelBuildingPanel != null)
                {
                    if (FavCimsPeopleHotelBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleHotelBuildingButton = FavCimsPeopleHotelBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleHotelBuildingButton = FavCimsPeopleHotelBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleHotelBuildingButton.RefPanel = FavCimsPeopleHotelBuildingPanel;
                    PeopleHotelBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                FavCimsPeopleWarehouseBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) WarehouseWorldInfoPanel");
                if (FavCimsPeopleWarehouseBuildingPanel != null)
                {
                    if (FavCimsPeopleWarehouseBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleWarehouseBuildingButton = FavCimsPeopleWarehouseBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleWarehouseBuildingButton = FavCimsPeopleWarehouseBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleWarehouseBuildingButton.RefPanel = FavCimsPeopleWarehouseBuildingPanel;
                    PeopleWarehouseBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                GenerateFamilyDetailsTpl();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("OnLoad List Error : " + ex.ToString());
            }
        }

        internal void DestroyGraphics()
        {
            FavCimsCore.ClearIdArray();
            try
            {
                if (FavCimsPanel != null)
                {
                    UnityEngine.Object.Destroy(FavCimsPanel.gameObject);
                }
                if (mainButton != null)
                {
                    UnityEngine.Object.Destroy(mainButton.gameObject);
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error(ex.ToString());
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            DestroyGraphics();
        }

        public class UIGroupPanel : GeneratedGroupPanel
        {
            public override ItemClass.Service service
            {
                get
                {
                    return 0;
                }
            }

            private void Update()
            {
                if (Input.GetMouseButton(2) && Input.GetKeyDown(KeyCode.F))
                {
                    FavCimsPanelToggle();
                }
                GameObject gameObject = GameObject.Find("FavCimsTabMenuPanel");
                if (gameObject != null)
                {
                    UIPanel component = gameObject.GetComponent<UIPanel>();
                    if (component != null)
                    {
                        bool isVisible = component.isVisible;
                        if (isVisible)
                        {
                            component.Hide();
                        }
                    }
                }
            }

            public override string serviceName
            {
                get
                {
                    return "FavoriteCims";
                }
            }

            protected override bool IsServiceValid(PrefabInfo info)
            {
                return true;
            }
        }

    }
}
