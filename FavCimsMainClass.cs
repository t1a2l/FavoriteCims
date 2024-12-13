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
    public sealed class MainClass : LoadingBase<OptionsPanel>
    {
        public UIView uiView;

        public static bool UnLoading => !IsLoaded;

        private readonly MyAtlas Atlas = new();

        public const int MaxTemplates = 5;

        public static FamilyPanelTemplate[] Templates = new FamilyPanelTemplate[5];

        public static UIPanel FullScreenContainer;

        public static UIPanel HumanPanel;

        public static UIPanel TouristHumanPanel;

        private AddToFavButton FavStarButton;

        private AddToFavButton FavStarTouristButton;

        public static UIPanel HumanPassengerPanel;

        private PassengersInsideVehiclesButton PassengerButton;

        public static UIPanel HumanPublicTransportPanel;

        private PassengersInsidePTVehiclesButton PublicTransportPassengersButton;

        public static UIButton mainButton;

        private UIGroupPanel m_groupPanel;

        public static UIPanel PeopleZonedBuildingPanel;

        public static UIPanel PeopleServiceBuildingPanel;

        public static UIPanel PeopleFootballEventBuildingPanel;

        public static UIPanel PeopleVarsitySportsArenaBuildingPanel;

        public static UIPanel PeopleWarehouseBuildingPanel;

        public static UIPanel PeopleShelterBuildingPanel;

        public static UIPanel PeopleUniqueFactoryBuildingPanel;

        public static UIPanel PeopleHotelBuildingPanel;

        private PeopleInsideBuildingsButton PeopleZonedBuildingButton;

        private PeopleInsideBuildingsButton PeopleServiceBuildingButton;

        private PeopleInsideBuildingsButton PeopleFootballEventBuildingButton;

        private PeopleInsideBuildingsButton PeopleVarsitySportsArenaBuildingButton;

        private PeopleInsideBuildingsButton PeopleWarehouseBuildingButton;

        private PeopleInsideBuildingsButton PeopleShelterBuildingButton;

        private PeopleInsideBuildingsButton PeopleUniqueFactoryBuildingButton;

        private PeopleInsideBuildingsButton PeopleHotelBuildingButton;

        public static UIPanel PublicTransportTaxiPanel;

        public static UIPanel PeopleServiceResidentBuildingPanel;

        private UIComponent PanelTrigger_paneltime;

        private UIComponent PanelTrigger_chirper;

        private UIComponent PanelTrigger_esc;

        private UIComponent PanelTrigger_infopanel;

        private UIComponent PanelTrigger_bottombars;

        public static UIPanel Panel;

        protected override List<AppMode> PermittedModes => [AppMode.Game];

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CreateGraphics();
        }

        internal void GenerateFamilyDetailsTpl()
        {
            for (int i = 0; i < 5; i++)
            {
                if (FullScreenContainer.Find<FamilyPanelTemplate>("FamilyTemplate_" + i.ToString()) != null)
                {
                    Templates[i] = FullScreenContainer.Find<FamilyPanelTemplate>("FamilyTemplate_" + i.ToString());
                    Templates[i].MyInstanceID = InstanceID.Empty;
                    Templates[i].Hide();
                }
                else
                {
                    Templates[i] = FullScreenContainer.AddUIComponent(typeof(FamilyPanelTemplate)) as FamilyPanelTemplate;
                    Templates[i].name = "FamilyTemplate_" + i.ToString();
                    Templates[i].MyInstanceID = InstanceID.Empty;
                    Templates[i].Hide();
                }
            }
        }

        public static void PanelToggle()
        {
            if (!Panel.isVisible)
            {
                Panel.CenterTo(FullScreenContainer);
                Panel.Show();
            }
            else
            {
                Panel.Hide();
            }
        }

        public void PanelOff()
        {
            if (Panel.isVisible && !Panel.containsMouse && !mainButton.containsMouse && PanelTrigger_paneltime != null && !PanelTrigger_paneltime.containsMouse)
            {
                Panel.Hide();
            }
        }

        internal void CreateGraphics()
        {
            try
            {
                GameObject gameObject = GameObject.Find("MenuPanel");
                if (gameObject != null)
                {
                    return;
                }
                PanelTrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
                PanelTrigger_esc = UIView.Find<UIButton>("Esc");
                PanelTrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
                PanelTrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
                PanelTrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
                if (PanelTrigger_chirper != null && PanelTrigger_paneltime != null)
                {
                    PanelTrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        PanelOff();
                    };
                }
                if (PanelTrigger_esc != null && PanelTrigger_paneltime != null)
                {
                    PanelTrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        PanelOff();
                    };
                }
                if (PanelTrigger_infopanel != null && PanelTrigger_paneltime != null)
                {
                    PanelTrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        PanelOff();
                    };
                }
                if (PanelTrigger_bottombars != null && PanelTrigger_paneltime != null)
                {
                    PanelTrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        PanelOff();
                    };
                }
                UIView aview = UIView.GetAView();
                TextureDB.LoadTextures();
                Atlas.LoadAtlasIcons();
                UITabstrip tabstrip = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();
                if (tabstrip.Find("MenuPanel") || GameObject.Find("MainToolbarButtonTemplate") || GameObject.Find("ScrollableSubPanelTemplate"))
                {
                    return;
                }

                GameObject asGameObject = UITemplateManager.GetAsGameObject("MainToolbarButtonTemplate");
                GameObject asGameObject2 = UITemplateManager.GetAsGameObject("ScrollableSubPanelTemplate");
                mainButton = tabstrip.AddTab("MenuPanel", asGameObject, asGameObject2, [typeof(UIGroupPanel)]) as UIButton;
                mainButton.normalBgSprite = "FavoriteCimsButton";
                mainButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
                mainButton.focusedBgSprite = "FavoriteCimsButtonFocused";
                mainButton.pressedBgSprite = "FavoriteCimsButtonPressed";
                mainButton.playAudioEvents = true;
                mainButton.name = "Button";
                mainButton.tooltipBox = aview.defaultTooltipBox;
                mainButton.atlas = MyAtlas.FavCimsAtlas;
                mainButton.size = new Vector2(49f, 49f);
                mainButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    PanelToggle();
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
                    m_groupPanel.name = "TabMenuPanel";
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
                Panel = FullScreenContainer.AddUIComponent<MainPanel>();
                Panel.Hide();
                FullScreenContainer.eventMouseDown += delegate
                {
                    if (!Panel.containsMouse)
                    {
                        Panel.SendToBack();
                    }
                    else
                    {
                        Panel.BringToFront();
                    }
                };
                HumanPanel = FullScreenContainer.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
                if (HumanPanel != null)
                {
                    if (HumanPanel.GetComponentInChildren<AddToFavButton>() != null)
                    {
                        FavStarButton = HumanPanel.GetComponentInChildren<AddToFavButton>();
                    }
                    else
                    {
                        FavStarButton = HumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
                    }
                    FavStarButton.RefPanel = HumanPanel;
                    FavStarButton.Alignment = UIAlignAnchor.BottomRight;
                }
                TouristHumanPanel = FullScreenContainer.Find<UIPanel>("(Library) TouristWorldInfoPanel");
                if (TouristHumanPanel != null)
                {
                    if (TouristHumanPanel.GetComponentInChildren<AddToFavButton>() != null)
                    {
                        FavStarTouristButton = TouristHumanPanel.GetComponentInChildren<AddToFavButton>();
                    }
                    else
                    {
                        FavStarTouristButton = TouristHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
                    }
                    FavStarTouristButton.RefPanel = TouristHumanPanel;
                    FavStarTouristButton.Alignment = UIAlignAnchor.BottomRight;
                }
                HumanPassengerPanel = FullScreenContainer.Find<UIPanel>("(Library) CitizenVehicleWorldInfoPanel");
                if (HumanPassengerPanel != null)
                {
                    if (HumanPassengerPanel.GetComponentInChildren<PassengersInsideVehiclesButton>() != null)
                    {
                        PassengerButton = HumanPassengerPanel.GetComponentInChildren<PassengersInsideVehiclesButton>();
                    }
                    else
                    {
                        PassengerButton = HumanPassengerPanel.AddUIComponent(typeof(PassengersInsideVehiclesButton)) as PassengersInsideVehiclesButton;
                    }
                    PassengerButton.RefPanel = HumanPassengerPanel;
                    PassengerButton.Alignment = UIAlignAnchor.BottomRight;
                }
                HumanPublicTransportPanel = FullScreenContainer.Find<UIPanel>("(Library) PublicTransportVehicleWorldInfoPanel");
                if (HumanPublicTransportPanel != null)
                {
                    if (HumanPublicTransportPanel.GetComponentInChildren<PassengersInsidePTVehiclesButton>() != null)
                    {
                        PublicTransportPassengersButton = HumanPublicTransportPanel.GetComponentInChildren<PassengersInsidePTVehiclesButton>();
                    }
                    else
                    {
                        PublicTransportPassengersButton = HumanPublicTransportPanel.AddUIComponent(typeof(PassengersInsidePTVehiclesButton)) as PassengersInsidePTVehiclesButton;
                    }
                    PublicTransportPassengersButton.RefPanel = HumanPublicTransportPanel;
                    PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PublicTransportTaxiPanel = FullScreenContainer.Find<UIPanel>("(Library) CityServiceVehicleWorldInfoPanel");
                if (PublicTransportTaxiPanel != null)
                {
                    if (PublicTransportTaxiPanel.GetComponentInChildren<PassengersInsidePTVehiclesButton>() != null)
                    {
                        PublicTransportPassengersButton = PublicTransportTaxiPanel.GetComponentInChildren<PassengersInsidePTVehiclesButton>();
                    }
                    else
                    {
                        PublicTransportPassengersButton = PublicTransportTaxiPanel.AddUIComponent(typeof(PassengersInsidePTVehiclesButton)) as PassengersInsidePTVehiclesButton;
                    }
                    PublicTransportPassengersButton.RefPanel = PublicTransportTaxiPanel;
                    PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleZonedBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
                if (PeopleZonedBuildingPanel != null)
                {
                    if (PeopleZonedBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleZonedBuildingButton = PeopleZonedBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleZonedBuildingButton = PeopleZonedBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleZonedBuildingButton.RefPanel = PeopleZonedBuildingPanel;
                    PeopleZonedBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleServiceBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
                if (PeopleServiceBuildingPanel != null)
                {
                    if (PeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleServiceBuildingButton = PeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleServiceBuildingButton = PeopleServiceBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleServiceBuildingButton.RefPanel = PeopleServiceBuildingPanel;
                    PeopleServiceBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleVarsitySportsArenaBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) VarsitySportsArenaPanel");
                if (PeopleVarsitySportsArenaBuildingPanel != null)
                {
                    if (PeopleVarsitySportsArenaBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleVarsitySportsArenaBuildingButton = PeopleVarsitySportsArenaBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleVarsitySportsArenaBuildingButton = PeopleVarsitySportsArenaBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleVarsitySportsArenaBuildingButton.RefPanel = PeopleVarsitySportsArenaBuildingPanel;
                    PeopleVarsitySportsArenaBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleFootballEventBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) FootballPanel");
                if (PeopleFootballEventBuildingPanel != null)
                {
                    if (PeopleFootballEventBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleFootballEventBuildingButton = PeopleFootballEventBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleFootballEventBuildingButton = PeopleFootballEventBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleFootballEventBuildingButton.RefPanel = PeopleFootballEventBuildingPanel;
                    PeopleFootballEventBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleUniqueFactoryBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) UniqueFactoryWorldInfoPanel");
                if (PeopleUniqueFactoryBuildingPanel != null)
                {
                    if (PeopleUniqueFactoryBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleUniqueFactoryBuildingButton = PeopleUniqueFactoryBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleUniqueFactoryBuildingButton = PeopleUniqueFactoryBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleUniqueFactoryBuildingButton.RefPanel = PeopleUniqueFactoryBuildingPanel;
                    PeopleUniqueFactoryBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleShelterBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) ShelterWorldInfoPanel");
                if (PeopleShelterBuildingPanel != null)
                {
                    if (PeopleShelterBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleShelterBuildingButton = PeopleShelterBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleShelterBuildingButton = PeopleShelterBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleShelterBuildingButton.RefPanel = PeopleShelterBuildingPanel;
                    PeopleShelterBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleHotelBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) HotelWorldInfoPanel");
                if (PeopleHotelBuildingPanel != null)
                {
                    if (PeopleHotelBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleHotelBuildingButton = PeopleHotelBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleHotelBuildingButton = PeopleHotelBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleHotelBuildingButton.RefPanel = PeopleHotelBuildingPanel;
                    PeopleHotelBuildingButton.Alignment = UIAlignAnchor.BottomRight;
                }
                PeopleWarehouseBuildingPanel = FullScreenContainer.Find<UIPanel>("(Library) WarehouseWorldInfoPanel");
                if (PeopleWarehouseBuildingPanel != null)
                {
                    if (PeopleWarehouseBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null)
                    {
                        PeopleWarehouseBuildingButton = PeopleWarehouseBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
                    }
                    else
                    {
                        PeopleWarehouseBuildingButton = PeopleWarehouseBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
                    }
                    PeopleWarehouseBuildingButton.RefPanel = PeopleWarehouseBuildingPanel;
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
                if (Panel != null)
                {
                    UnityEngine.Object.Destroy(Panel.gameObject);
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

            public void Update()
            {
                if (Input.GetMouseButton(2) && Input.GetKeyDown(KeyCode.F))
                {
                    PanelToggle();
                }
                GameObject gameObject = GameObject.Find("TabMenuPanel");
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
