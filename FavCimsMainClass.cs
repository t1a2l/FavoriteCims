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
    public class FavCimsMainClass : LoadingExtensionBase
	{
        public UIView uiView;

        public static bool UnLoading = false;

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

        private VechiclePassengersButtonPT PublicTransportPassengersButton;

        public static UIButton mainButton;

        private FavCimsMainClass.UIGroupPanel m_groupPanel;

        public static UIPanel FavCimsPeopleBuildingPanel;

        private PeopleInsideBuildingsButton PeopleBuildingButton;

        public static UIPanel FavCimsPeopleServiceBuildingPanel;

        private PeopleInsideServiceBuildingsButton PeopleServiceBuildingButton;

        public static UIPanel FavCimsPublicTransportTaxiPanel;

        private VechiclePassengersButtonPT PublicTransportTaxiPassengersButton;

        private UIComponent FavCimsPanelTrigger_paneltime;

        private UIComponent FavCimsPanelTrigger_chirper;

        private UIComponent FavCimsPanelTrigger_esc;

        private UIComponent FavCimsPanelTrigger_infopanel;

        private UIComponent FavCimsPanelTrigger_bottombars;

		public static UIPanel FavCimsPanel;

		public override void OnLevelLoaded(LoadMode mode)
		{
			bool flag = mode != LoadMode.LoadGame && mode != LoadMode.NewGame;
			if (!flag)
			{
				FavCimsMainClass.UnLoading = false;
				this.CreateGraphics();
			}
		}

		internal void GenerateFamilyDetailsTpl()
		{
			for (int i = 0; i < 5; i++)
			{
				bool flag = FavCimsMainClass.FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i.ToString()) != null;
				if (flag)
				{
					FavCimsMainClass.Templates[i] = FavCimsMainClass.FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i.ToString());
					FavCimsMainClass.Templates[i].MyInstanceID = InstanceID.Empty;
					FavCimsMainClass.Templates[i].Hide();
				}
				else
				{
					FavCimsMainClass.Templates[i] = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(FamilyPanelTemplate)) as FamilyPanelTemplate;
					FavCimsMainClass.Templates[i].name = "FavCimsFamilyTemplate_" + i.ToString();
					FavCimsMainClass.Templates[i].MyInstanceID = InstanceID.Empty;
					FavCimsMainClass.Templates[i].Hide();
				}
			}
		}

		public static void FavCimsPanelToggle()
		{
			bool flag = !FavCimsMainClass.FavCimsPanel.isVisible;
			if (flag)
			{
				FavCimsMainClass.FavCimsPanel.CenterTo(FavCimsMainClass.FullScreenContainer);
				FavCimsMainClass.FavCimsPanel.Show();
			}
			else
			{
				FavCimsMainClass.FavCimsPanel.Hide();
			}
		}

		public void FavCimsPanelOff()
		{
			bool flag = FavCimsMainClass.FavCimsPanel.isVisible && !FavCimsMainClass.FavCimsPanel.containsMouse && !FavCimsMainClass.mainButton.containsMouse && this.FavCimsPanelTrigger_paneltime != null && !this.FavCimsPanelTrigger_paneltime.containsMouse;
			if (flag)
			{
				FavCimsMainClass.FavCimsPanel.Hide();
			}
		}

		internal void CreateGraphics()
		{
			try
			{
				GameObject gameObject = GameObject.Find("FavCimsMenuPanel");
				bool flag = gameObject != null;
				if (!flag)
				{
					this.FavCimsPanelTrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
					this.FavCimsPanelTrigger_esc = UIView.Find<UIButton>("Esc");
					this.FavCimsPanelTrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
					this.FavCimsPanelTrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
					this.FavCimsPanelTrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
					bool flag2 = this.FavCimsPanelTrigger_chirper != null && this.FavCimsPanelTrigger_paneltime != null;
					if (flag2)
					{
						this.FavCimsPanelTrigger_chirper.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
						{
							this.FavCimsPanelOff();
						};
					}
					bool flag3 = this.FavCimsPanelTrigger_esc != null && this.FavCimsPanelTrigger_paneltime != null;
					if (flag3)
					{
						this.FavCimsPanelTrigger_esc.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
						{
							this.FavCimsPanelOff();
						};
					}
					bool flag4 = this.FavCimsPanelTrigger_infopanel != null && this.FavCimsPanelTrigger_paneltime != null;
					if (flag4)
					{
						this.FavCimsPanelTrigger_infopanel.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
						{
							this.FavCimsPanelOff();
						};
					}
					bool flag5 = this.FavCimsPanelTrigger_bottombars != null && this.FavCimsPanelTrigger_paneltime != null;
					if (flag5)
					{
						this.FavCimsPanelTrigger_bottombars.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
						{
							this.FavCimsPanelOff();
						};
					}
					UIView aview = UIView.GetAView();
					TextureDB.LoadFavCimsTextures();
					this.Atlas.LoadAtlasIcons();
					UITabstrip componentInChildren = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();
					bool flag6 = componentInChildren.Find("FavCimsMenuPanel") || GameObject.Find("MainToolbarButtonTemplate") || GameObject.Find("ScrollableSubPanelTemplate");
					if (!flag6)
					{
						GameObject asGameObject = UITemplateManager.GetAsGameObject("MainToolbarButtonTemplate");
						GameObject asGameObject2 = UITemplateManager.GetAsGameObject("ScrollableSubPanelTemplate");
						FavCimsMainClass.mainButton = componentInChildren.AddTab("FavCimsMenuPanel", asGameObject, asGameObject2, new Type[] { typeof(FavCimsMainClass.UIGroupPanel) }) as UIButton;
						FavCimsMainClass.mainButton.normalBgSprite = "FavoriteCimsButton";
						FavCimsMainClass.mainButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
						FavCimsMainClass.mainButton.focusedBgSprite = "FavoriteCimsButtonFocused";
						FavCimsMainClass.mainButton.pressedBgSprite = "FavoriteCimsButtonPressed";
						FavCimsMainClass.mainButton.playAudioEvents = true;
						FavCimsMainClass.mainButton.name = "FavCimsButton";
						FavCimsMainClass.mainButton.tooltipBox = aview.defaultTooltipBox;
						FavCimsMainClass.mainButton.atlas = MyAtlas.FavCimsAtlas;
						FavCimsMainClass.mainButton.size = new Vector2(49f, 49f);
						FavCimsMainClass.mainButton.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
						{
							FavCimsMainClass.FavCimsPanelToggle();
						};
						FavCimsMainClass.mainButton.tooltip = "Favorite Cims v0.4a";
						Locale locale = (Locale)typeof(LocaleManager).GetField("m_Locale", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(SingletonLite<LocaleManager>.instance);
						Locale.Key key = default;
						key.m_Identifier = "TUTORIAL_ADVISER_TITLE";
						key.m_Key = FavCimsMainClass.mainButton.name;
						Locale.Key key2 = key;
						bool flag7 = !locale.Exists(key2);
						if (flag7)
						{
							locale.AddLocalizedString(key2, "Favorite Cims v0.4a");
						}
						key = default;
						key.m_Identifier = "TUTORIAL_ADVISER";
						key.m_Key = FavCimsMainClass.mainButton.name;
						key2 = key;
						bool flag8 = !locale.Exists(key2);
						if (flag8)
						{
							locale.AddLocalizedString(key2, "Thanks for subscribing to Favorite Cims!\n\nHotkey = Press Middle Mouse Button + F \nIf you like the mod please consider leaving a rating on the steam workshop.\nMod Version : v0.4a by Gianxs");
						}
						FieldInfo field = typeof(MainToolbar).GetField("m_ObjectIndex", BindingFlags.Instance | BindingFlags.NonPublic);
						field.SetValue(ToolsModifierControl.mainToolbar, (int)field.GetValue(ToolsModifierControl.mainToolbar) + 1);
						this.m_groupPanel = componentInChildren.GetComponentInContainer(FavCimsMainClass.mainButton, typeof(FavCimsMainClass.UIGroupPanel)) as FavCimsMainClass.UIGroupPanel;
						bool flag9 = this.m_groupPanel != null;
						if (flag9)
						{
							this.m_groupPanel.name = "FavCimsTabMenuPanel";
							this.m_groupPanel.enabled = true;
							this.m_groupPanel.component.isInteractive = true;
							this.m_groupPanel.m_OptionsBar = ToolsModifierControl.mainToolbar.m_OptionsBar;
							this.m_groupPanel.m_DefaultInfoTooltipAtlas = ToolsModifierControl.mainToolbar.m_DefaultInfoTooltipAtlas;
							bool enabled = ToolsModifierControl.mainToolbar.enabled;
							if (enabled)
							{
								this.m_groupPanel.RefreshPanel();
							}
						}
						FavCimsMainClass.FullScreenContainer = UIView.Find<UIPanel>("FullScreenContainer");
						FavCimsMainClass.FavCimsPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent<FavoriteCimsMainPanel>();
						FavCimsMainClass.FavCimsPanel.Hide();
						FavCimsMainClass.FullScreenContainer.eventMouseDown += delegate
						{
							bool flag22 = !FavCimsMainClass.FavCimsPanel.containsMouse;
							if (flag22)
							{
								FavCimsMainClass.FavCimsPanel.SendToBack();
							}
							else
							{
								FavCimsMainClass.FavCimsPanel.BringToFront();
							}
						};
						FavCimsMainClass.FavCimsHumanPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
						bool flag10 = FavCimsMainClass.FavCimsHumanPanel != null;
						if (flag10)
						{
							bool flag11 = FavCimsMainClass.FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>() != null;
							if (flag11)
							{
								this.FavStarButton = FavCimsMainClass.FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>();
							}
							else
							{
								this.FavStarButton = FavCimsMainClass.FavCimsHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
							}
							this.FavStarButton.RefPanel = FavCimsMainClass.FavCimsHumanPanel;
							this.FavStarButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsTouristHumanPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) TouristWorldInfoPanel");
						bool flag12 = FavCimsMainClass.FavCimsTouristHumanPanel != null;
						if (flag12)
						{
							bool flag13 = FavCimsMainClass.FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>() != null;
							if (flag13)
							{
								this.FavStarTouristButton = FavCimsMainClass.FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>();
							}
							else
							{
								this.FavStarTouristButton = FavCimsMainClass.FavCimsTouristHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
							}
							this.FavStarTouristButton.RefPanel = FavCimsMainClass.FavCimsTouristHumanPanel;
							this.FavStarTouristButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsHumanPassengerPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) CitizenVehicleWorldInfoPanel");
						bool flag14 = FavCimsMainClass.FavCimsHumanPassengerPanel != null;
						if (flag14)
						{
							bool flag15 = FavCimsMainClass.FavCimsHumanPassengerPanel.GetComponentInChildren<VehiclePassengersButton>() != null;
							if (flag15)
							{
								this.PassengerButton = FavCimsMainClass.FavCimsHumanPassengerPanel.GetComponentInChildren<VehiclePassengersButton>();
							}
							else
							{
								this.PassengerButton = FavCimsMainClass.FavCimsHumanPassengerPanel.AddUIComponent(typeof(VehiclePassengersButton)) as VehiclePassengersButton;
							}
							this.PassengerButton.RefPanel = FavCimsMainClass.FavCimsHumanPassengerPanel;
							this.PassengerButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsHumanPublicTransportPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) PublicTransportVehicleWorldInfoPanel");
						bool flag16 = FavCimsMainClass.FavCimsHumanPublicTransportPanel != null;
						if (flag16)
						{
							bool flag17 = FavCimsMainClass.FavCimsHumanPublicTransportPanel.GetComponentInChildren<VechiclePassengersButtonPT>() != null;
							if (flag17)
							{
								this.PublicTransportPassengersButton = FavCimsMainClass.FavCimsHumanPublicTransportPanel.GetComponentInChildren<VechiclePassengersButtonPT>();
							}
							else
							{
								this.PublicTransportPassengersButton = FavCimsMainClass.FavCimsHumanPublicTransportPanel.AddUIComponent(typeof(VechiclePassengersButtonPT)) as VechiclePassengersButtonPT;
							}
							this.PublicTransportPassengersButton.RefPanel = FavCimsMainClass.FavCimsHumanPublicTransportPanel;
							this.PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsPeopleBuildingPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
						bool flag18 = FavCimsMainClass.FavCimsPeopleBuildingPanel != null;
						if (flag18)
						{
							bool flag19 = FavCimsMainClass.FavCimsPeopleBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null;
							if (flag19)
							{
								this.PeopleBuildingButton = FavCimsMainClass.FavCimsPeopleBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
							}
							else
							{
								this.PeopleBuildingButton = FavCimsMainClass.FavCimsPeopleBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
							}
							this.PeopleBuildingButton.RefPanel = FavCimsMainClass.FavCimsPeopleBuildingPanel;
							this.PeopleBuildingButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsPeopleServiceBuildingPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
						bool flag20 = FavCimsMainClass.FavCimsPeopleServiceBuildingPanel != null;
						if (flag20)
						{
							bool flag21 = FavCimsMainClass.FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideServiceBuildingsButton>() != null;
							if (flag21)
							{
								this.PeopleServiceBuildingButton = FavCimsMainClass.FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideServiceBuildingsButton>();
							}
							else
							{
								this.PeopleServiceBuildingButton = FavCimsMainClass.FavCimsPeopleServiceBuildingPanel.AddUIComponent(typeof(PeopleInsideServiceBuildingsButton)) as PeopleInsideServiceBuildingsButton;
							}
							this.PeopleServiceBuildingButton.RefPanel = FavCimsMainClass.FavCimsPeopleServiceBuildingPanel;
							this.PeopleServiceBuildingButton.Alignment = UIAlignAnchor.BottomRight;
						}
						FavCimsMainClass.FavCimsPublicTransportTaxiPanel = FavCimsMainClass.FullScreenContainer.Find<UIPanel>("(Library) CityServiceVehicleWorldInfoPanel");
						bool flag22 = FavCimsMainClass.FavCimsPublicTransportTaxiPanel != null;
						if (flag22)
						{
							bool flag23 = FavCimsMainClass.FavCimsPublicTransportTaxiPanel.GetComponentInChildren<VechiclePassengersButtonPT>() != null;
							if (flag23)
							{
								this.PublicTransportTaxiPassengersButton = FavCimsMainClass.FavCimsPublicTransportTaxiPanel.GetComponentInChildren<VechiclePassengersButtonPT>();
							}
							else
							{
								this.PublicTransportTaxiPassengersButton = FavCimsMainClass.FavCimsPublicTransportTaxiPanel.AddUIComponent(typeof(VechiclePassengersButtonPT)) as VechiclePassengersButtonPT;
							}
							this.PublicTransportTaxiPassengersButton.RefPanel = FavCimsMainClass.FavCimsPublicTransportTaxiPanel;
							this.PublicTransportTaxiPassengersButton.Alignment = UIAlignAnchor.BottomRight;
						}
						this.GenerateFamilyDetailsTpl();
					}
				}
			}
			catch (Exception ex)
			{
                Utils.Debug.Error("OnLoad List Error : " + ex.ToString());
			}
		}

		internal void DestroyGraphics()
		{
			FavCimsMainClass.UnLoading = true;
			FavCimsCore.ClearIdArray();
			try
			{
				bool flag = FavCimsMainClass.FavCimsPanel != null;
				if (flag)
				{
					UnityEngine.Object.Destroy(FavCimsMainClass.FavCimsPanel.gameObject);
				}
				bool flag2 = FavCimsMainClass.mainButton != null;
				if (flag2)
				{
                    UnityEngine.Object.Destroy(FavCimsMainClass.mainButton.gameObject);
				}
			}
			catch (Exception ex)
			{
                Utils.Debug.Error(ex.ToString());
			}
		}

		public override void OnLevelUnloading()
		{
			this.DestroyGraphics();
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
				bool flag = Input.GetMouseButton(2) && Input.GetKeyDown((KeyCode)102);
				if (flag)
				{
					FavCimsMainClass.FavCimsPanelToggle();
				}
				GameObject gameObject = GameObject.Find("FavCimsTabMenuPanel");
				bool flag2 = gameObject != null;
				if (flag2)
				{
					UIPanel component = gameObject.GetComponent<UIPanel>();
					bool flag3 = component != null;
					if (flag3)
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
