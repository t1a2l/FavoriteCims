using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class CitizenRow : UIPanel
	{
        private const float Run = 0.5f;

        private float seconds = 0.5f;

        private float secondsForceRun = 2f;

        private const float SlowRun = 30f;

        private float HiddenRowsSeconds = 30f;

        private bool FirstRun = true;

        private bool execute = false;

        public InstanceID MyInstanceID;

        public string MyInstancedName;

        private Dictionary<string, string> CitizenRowData = new Dictionary<string, string>();

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private readonly InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private readonly DistrictManager MyDistrict = Singleton<DistrictManager>.instance;

        private static readonly string[] sHappinessLevels = new string[] { "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy" };

        private UIPanel FavCimsCitizenSingleRowPanel;

        private Texture FavDot;

        private Texture FavDot_hover;

        private UITextureSprite FavCimsCitizenSingleRowBGSprite;

        private UIPanel FavCimsCitizenHappinessPanel;

        private UIButton FavCimsRowCloseButton;

        private UIPanel FavCimsCitizenNamePanel;

        private UIButton FavCimsCitizenName;

        private UITextureSprite FavCimsNameColText_EmptySprite;

        private UIPanel FavCimsAgePhasePanel;

        private UIButton FavCimsAgePhase;

        private UITextureSprite FavCimsSeparatorSprite2;

        private UITextureSprite FavCimsSeparatorSprite3;

        private UITextureSprite FavCimsSeparatorSprite4;

        private UITextureSprite FavCimsSeparatorSprite5;

        private UITextureSprite FavCimsSeparatorSprite6;

        private UITextureSprite FavCimsSeparatorSprite7;

        private UITextureSprite FavCimsSeparatorSprite8;

        private UITextureSprite FavCimsSeparatorSprite9;

        private UIPanel FavCimsRealAgePanel;

        private UIButton FavCimsRealAge;

        private UIPanel FavCimsEducationPanel;

        private UIButton FavCimsEducation;

        private UIButton FavCimsHappyIcon;

        private UITextureSprite FavCimsHappyOverride;

        private UIPanel FavCimsCitizenHomePanel;

        private UIButton FavCimsCitizenHome;

        private UIButton OtherInfoButton;

        private UIButton FavCimsCitizenHomeButton;

        private UIPanel FavCimsWorkingPlacePanel;

        private UITextureSprite FavCimsWorkingPlaceSprite;

        private UIButton FavCimsWorkingPlace;

        private UIPanel FavCimsLastActivityPanel;

        private UIButton FavCimsLastActivity;

        private UIButton FavCimsLastActivityVehicleButton;

        private UIPanel FavCimsCloseRowPanel;

        private UITextureSprite FavCimsCitizenResidentialLevelSprite;

        private UITextureSprite FavCimsCitizenWorkPlaceLevelSprite;

        private UIButton FavCimsWorkingPlaceButton;

        private FamilyPanelTemplate MyFamily;

        private string rowLang;

        private int citizenINT;

        private int tmp_health;

        private int tmp_wellbeing;

        private int tmp_happiness;

        private int tmp_age;

        private int RealAge;

        private int CitizenDistrict;

        private int HomeDistrict;

        private int WorkDistrict;

        private int TargetDistrict;

        private string CitizenName;

        private string CitizenVehicleName;

        private string DeathDate;

        private string DeathTime;

        private string CitizenTarget;

        private string CitizenStatus;

        private ushort CitizenHome;

        private ushort CitizenVehicle;

        private ushort WorkPlace;

        private ushort InstanceCitizenID;

        private bool GoingOutside;

        private bool LeaveCity = false;

        private bool DeadOrGone = false;

        private bool HomeLess = false;

        private bool CitizenIsDead = false;

        private bool isStudent = false;

        private CitizenInstance citizenInstance;

        private InstanceID WorkPlaceID;

        private InstanceID CitizenHomeID;

        private InstanceID MyTargetID;

        private InstanceID MyVehicleID;

        private CitizenInfo citizenInfo;

        private BuildingInfo HomeInfo;

        private BuildingInfo WorkInfo;

        private VehicleInfo VehInfo;

        private InstanceID CitizenDead;

        private ushort hearse;

        private ushort policeveh;

        internal static int GetTemplate()
		{
			for (int i = 0; i < 5; i++)
			{
				bool isEmpty = FavCimsMainClass.Templates[i].MyInstanceID.IsEmpty;
				if (isEmpty)
				{
					return i;
				}
			}
			return -1;
		}

		public void GoToCitizen(InstanceID Target, UIMouseEventParameter eventParam)
		{
			bool isEmpty = Target.IsEmpty;
			if (!isEmpty)
			{
				try
				{
					bool flag = this.MyInstance.SelectInstance(Target);
					if (flag)
					{
						bool flag2 = eventParam.buttons == UIMouseButton.Middle;
						if (flag2)
						{
							bool flag3 = this.citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
							if (flag3)
							{
								WorldInfoPanel.Show<TouristWorldInfoPanel>(base.position, Target);
							}
							else
							{
								WorldInfoPanel.Show<CitizenWorldInfoPanel>(base.position, Target);
							}
						}
						else
						{
							bool flag4 = eventParam.buttons == UIMouseButton.Right;
							if (flag4)
							{
								FavCimsMainClass.FavCimsPanel.Hide();
								ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
								bool flag5 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
								if (flag5)
								{
									WorldInfoPanel.Show<TouristWorldInfoPanel>(base.position, Target);
								}
								else
								{
									WorldInfoPanel.Show<CitizenWorldInfoPanel>(base.position, Target);
								}
							}
							else
							{
								ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
								bool flag6 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
								if (flag6)
								{
									WorldInfoPanel.Show<TouristWorldInfoPanel>(base.position, Target);
								}
								else
								{
									WorldInfoPanel.Show<CitizenWorldInfoPanel>(base.position, Target);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the Citizen " + ex.ToString());
				}
			}
		}

		private void GoToHome(UIComponent component, UIMouseEventParameter p)
		{
			bool isEmpty = this.CitizenHomeID.IsEmpty;
			if (!isEmpty)
			{
				try
				{
					bool flag = p.buttons == UIMouseButton.Middle;
					if (flag)
					{
						WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(base.position, this.CitizenHomeID);
					}
					else
					{
						bool flag2 = p.buttons == UIMouseButton.Right;
						if (flag2)
						{
							FavCimsMainClass.FavCimsPanel.Hide();
							ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
							WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(base.position, this.CitizenHomeID);
						}
						else
						{
							ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
							WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(base.position, this.CitizenHomeID);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the House " + ex.ToString());
				}
			}
		}

		private void GoToWork(UIComponent component, UIMouseEventParameter p)
		{
			bool isEmpty = this.WorkPlaceID.IsEmpty;
			if (!isEmpty)
			{
				try
				{
					bool flag = p.buttons == UIMouseButton.Middle;
					if (flag)
					{
						DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
					}
					else
					{
						bool flag2 = p.buttons == UIMouseButton.Right;
						if (flag2)
						{
							FavCimsMainClass.FavCimsPanel.Hide();
							ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
						}
						else
						{
							ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the WorkPlace " + ex.ToString());
				}
			}
		}

		private void GoToTarget(UIComponent component, UIMouseEventParameter p)
		{
			bool isEmpty = this.MyTargetID.IsEmpty;
			if (!isEmpty)
			{
				try
				{
					bool flag = p.buttons == UIMouseButton.Middle;
					if (flag)
					{
						DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);
					}
					else
					{
						bool flag2 = p.buttons == UIMouseButton.Right;
						if (flag2)
						{
							FavCimsMainClass.FavCimsPanel.Hide();
							ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);
						}
						else
						{
							ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the Target " + ex.ToString());
				}
			}
		}

		private void GoToVehicle(UIComponent component, UIMouseEventParameter p)
		{
			bool isEmpty = this.MyVehicleID.IsEmpty;
			if (!isEmpty)
			{
				try
				{
					bool flag = p.buttons == UIMouseButton.Middle;
					if (flag)
					{
						DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
					}
					else
					{
						bool flag2 = p.buttons == UIMouseButton.Right;
						if (flag2)
						{
							FavCimsMainClass.FavCimsPanel.Hide();
							ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
						}
						else
						{
							ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
							DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the Vehicle " + ex.ToString());
				}
			}
		}

		internal static string GetHappinessString(Citizen.Happiness happinessLevel)
		{
			return "NotificationIcon" + CitizenRow.sHappinessLevels[(int)happinessLevel];
		}

		public override void Start()
		{
			try
			{
				uint citizen = this.MyInstanceID.Citizen;
				this.citizenINT = (int)(uint)((UIntPtr)citizen);
				this.CitizenName = this.MyInstance.GetName(this.MyInstanceID);
				bool flag = this.MyInstancedName == null;
				if (flag)
				{
					this.MyInstancedName = this.CitizenName;
				}
				bool flag2 = this.citizenINT != 0 && !FavCimsCore.RowID.ContainsKey(this.citizenINT) && this.CitizenName != null && this.CitizenName.Length > 0;
				if (flag2)
				{
					FavCimsCore.InsertIdIntoArray(this.citizenINT);
					base.width = 1134f;
					base.height = 41f;
					base.autoLayoutDirection = LayoutDirection.Vertical;
					base.autoLayout = true;
					base.autoLayoutPadding = new RectOffset(0, 0, 1, 0);
					this.FavCimsCitizenSingleRowPanel = base.AddUIComponent<UIPanel>();
					this.FavCimsCitizenSingleRowPanel.width = base.width;
					this.FavCimsCitizenSingleRowPanel.height = 40f;
					this.FavCimsCitizenSingleRowBGSprite = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UITextureSprite>();
					this.FavCimsCitizenSingleRowBGSprite.name = "FavCimsCitizenSingleRowBGSprite";
					this.FavCimsCitizenSingleRowBGSprite.width = this.FavCimsCitizenSingleRowPanel.width;
					this.FavCimsCitizenSingleRowBGSprite.height = this.FavCimsCitizenSingleRowPanel.height;
					this.FavCimsCitizenSingleRowBGSprite.AlignTo(this.FavCimsCitizenSingleRowPanel, UIAlignAnchor.TopLeft);
					bool flag3 = !FavoriteCimsMainPanel.RowAlternateBackground;
					if (flag3)
					{
						this.FavDot = ResourceLoader.LoadTexture((int)base.width, 40, "UIMainPanel.Rows.bgrow_1.png");
						this.FavDot.name = "FavDot_1";
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;
						FavoriteCimsMainPanel.RowAlternateBackground = true;
					}
					else
					{
						this.FavDot = ResourceLoader.LoadTexture((int)base.width, 40, "UIMainPanel.Rows.bgrow_2.png");
						this.FavDot.name = "FavDot_2";
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;
						FavoriteCimsMainPanel.RowAlternateBackground = false;
					}
					this.FavDot_hover = ResourceLoader.LoadTexture((int)base.width, 40, "UIMainPanel.Rows.bgrow_hover.png");
					this.FavCimsCitizenSingleRowPanel.eventMouseEnter += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot_hover;
					};
					this.FavCimsCitizenSingleRowPanel.eventMouseLeave += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;
					};
					this.FavCimsCitizenHappinessPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsCitizenHappinessPanel.name = "FavCimsCitizenHappinessPanel";
					this.FavCimsCitizenHappinessPanel.width = FavoriteCimsMainPanel.FavCimsHappinesColText.width;
					this.FavCimsCitizenHappinessPanel.height = 40f;
					this.FavCimsCitizenHappinessPanel.relativePosition = new Vector3(0f, 0f);
					this.FavCimsHappyIcon = this.FavCimsCitizenHappinessPanel.AddUIComponent<UIButton>();
					this.FavCimsHappyIcon.width = 30f;
					this.FavCimsHappyIcon.height = 30f;
					this.FavCimsHappyIcon.isEnabled = false;
					this.FavCimsHappyIcon.playAudioEvents = false;
					this.FavCimsHappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsHappyIcon.relativePosition = new Vector3(15f, 5f);
					this.FavCimsHappyOverride = this.FavCimsHappyIcon.AddUIComponent<UITextureSprite>();
					this.FavCimsHappyOverride.width = 30f;
					this.FavCimsHappyOverride.height = 30f;
					this.FavCimsHappyOverride.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenNamePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsCitizenNamePanel.name = "FavCimsCitizenNamePanel";
					this.FavCimsCitizenNamePanel.width = FavoriteCimsMainPanel.FavCimsNameColText.width;
					this.FavCimsCitizenNamePanel.height = 40f;
					this.FavCimsCitizenNamePanel.relativePosition = new Vector3(this.FavCimsCitizenHappinessPanel.relativePosition.x + this.FavCimsCitizenHappinessPanel.width, 0f);
					this.FavCimsCitizenName = this.FavCimsCitizenNamePanel.AddUIComponent<UIButton>();
					this.FavCimsCitizenName.name = "FavCimsCitizenName";
					this.FavCimsCitizenName.width = this.FavCimsCitizenNamePanel.width;
					this.FavCimsCitizenName.height = this.FavCimsCitizenNamePanel.height;
					this.FavCimsCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsCitizenName.textHorizontalAlignment = 0;
					this.FavCimsCitizenName.playAudioEvents = true;
					this.FavCimsCitizenName.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsCitizenName.font.size = 15;
					this.FavCimsCitizenName.textScale = 1f;
					this.FavCimsCitizenName.wordWrap = true;
					this.FavCimsCitizenName.textPadding.left = 40;
					this.FavCimsCitizenName.textPadding.right = 5;
					this.FavCimsCitizenName.textColor = new Color32(204, 204, 51, 40);
					this.FavCimsCitizenName.hoveredTextColor = new Color32(204, 102, 0, 20);
					this.FavCimsCitizenName.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
					this.FavCimsCitizenName.focusedTextColor = new Color32(153, 0, 0, 0);
					this.FavCimsCitizenName.useDropShadow = true;
					this.FavCimsCitizenName.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsCitizenName.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsCitizenName.maximumSize = new Vector2(this.FavCimsCitizenNamePanel.width, this.FavCimsCitizenNamePanel.height);
					this.FavCimsCitizenName.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenName.eventMouseUp += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						this.GoToCitizen(this.MyInstanceID, eventParam);
					};
					this.FavCimsNameColText_EmptySprite = this.FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsNameColText_EmptySprite.width = this.FavCimsCitizenName.width;
					this.FavCimsNameColText_EmptySprite.height = this.FavCimsCitizenName.height;
					this.FavCimsNameColText_EmptySprite.relativePosition = new Vector3(0f, 0f);
					bool columnSpecialBackground = FavoriteCimsMainPanel.ColumnSpecialBackground;
					if (columnSpecialBackground)
					{
						this.FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
						this.FavCimsNameColText_EmptySprite.opacity = 0.7f;
					}
					FavoriteCimsMainPanel.FavCimsNameColText.eventClick += delegate
					{
						bool flag4 = this.FavCimsNameColText_EmptySprite.texture == null;
						if (flag4)
						{
							this.FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
							this.FavCimsNameColText_EmptySprite.opacity = 0.7f;
							FavoriteCimsMainPanel.ColumnSpecialBackground = true;
						}
						else
						{
							this.FavCimsNameColText_EmptySprite.texture = null;
							FavoriteCimsMainPanel.ColumnSpecialBackground = false;
						}
					};
					this.FavCimsCitizenName.BringToFront();
					this.FavCimsCitizenName.relativePosition = new Vector3(0f, 0f);
					this.OtherInfoButton = this.FavCimsCitizenNamePanel.AddUIComponent<UIButton>();
					this.OtherInfoButton.name = "FavCimsOtherInfoButton";
					this.OtherInfoButton.width = 20f;
					this.OtherInfoButton.height = 20f;
					this.OtherInfoButton.playAudioEvents = true;
					this.OtherInfoButton.normalBgSprite = "CityInfo";
					this.OtherInfoButton.hoveredBgSprite = "CityInfoHovered";
					this.OtherInfoButton.pressedBgSprite = "CityInfoPressed";
					this.OtherInfoButton.disabledBgSprite = "CityInfoDisabled";
					this.OtherInfoButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.OtherInfoButton.relativePosition = new Vector3(10f, 10f);
					this.OtherInfoButton.eventClick += delegate
					{
						try
						{
							bool flag5 = this.MyFamily == null && CitizenRow.GetTemplate() >= 0;
							if (flag5)
							{
								this.MyFamily = FavCimsMainClass.Templates[CitizenRow.GetTemplate()];
								this.MyFamily.MyInstanceID = this.MyInstanceID;
								this.MyFamily.Show();
								this.MyFamily.BringToFront();
								this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
							}
							else
							{
								bool flag6 = this.MyFamily != null && !this.MyFamily.isVisible;
								if (flag6)
								{
									this.MyFamily.MyInstanceID = this.MyInstanceID;
									this.MyFamily.Show();
									this.MyFamily.BringToFront();
									this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
								}
								else
								{
									bool flag7 = this.MyFamily != null;
									if (flag7)
									{
										this.MyFamily.Hide();
										this.MyFamily.MyInstanceID = InstanceID.Empty;
										this.MyFamily = null;
										this.OtherInfoButton.normalBgSprite = "CityInfo";
									}
								}
							}
						}
						catch (Exception ex2)
						{
							Debug.Error("Error when loading the template : " + ex2.ToString());
						}
					};
					this.FavCimsSeparatorSprite2 = this.FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite2.name = "FavCimsSeparatorSprite2";
					this.FavCimsSeparatorSprite2.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite2.relativePosition = new Vector3(0f, 0f);
					this.FavCimsAgePhasePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsAgePhasePanel.name = "FavCimsAgePhasePanel";
					this.FavCimsAgePhasePanel.width = FavoriteCimsMainPanel.FavCimsAgePhaseColText.width;
					this.FavCimsAgePhasePanel.height = 40f;
					this.FavCimsAgePhasePanel.relativePosition = new Vector3(this.FavCimsCitizenNamePanel.relativePosition.x + this.FavCimsCitizenNamePanel.width, 0f);
					this.FavCimsAgePhase = this.FavCimsAgePhasePanel.AddUIComponent<UIButton>();
					this.FavCimsAgePhase.name = "FavCimsAgePhase";
					this.FavCimsAgePhase.width = this.FavCimsAgePhasePanel.width;
					this.FavCimsAgePhase.height = this.FavCimsAgePhasePanel.height;
					this.FavCimsAgePhase.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsAgePhase.playAudioEvents = true;
					this.FavCimsAgePhase.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsAgePhase.font.size = 15;
					this.FavCimsAgePhase.textScale = 1f;
					this.FavCimsAgePhase.wordWrap = true;
					this.FavCimsAgePhase.textPadding.left = 5;
					this.FavCimsAgePhase.textPadding.right = 5;
					this.FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 204, 0, 32);
					this.FavCimsAgePhase.outlineSize = 1;
					this.FavCimsAgePhase.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsAgePhase.useDropShadow = true;
					this.FavCimsAgePhase.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsAgePhase.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsAgePhase.maximumSize = new Vector2(this.FavCimsAgePhasePanel.width, this.FavCimsAgePhasePanel.height);
					this.FavCimsAgePhase.isInteractive = false;
					this.FavCimsAgePhase.relativePosition = new Vector3(0f, 0f);
					this.FavCimsSeparatorSprite3 = this.FavCimsAgePhasePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite3.name = "FavCimsSeparatorSprite3";
					this.FavCimsSeparatorSprite3.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite3.relativePosition = new Vector3(0f, 0f);
					this.FavCimsRealAgePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsRealAgePanel.name = "FavCimsRealAgePanel";
					this.FavCimsRealAgePanel.width = FavoriteCimsMainPanel.FavCimsAgeColText.width;
					this.FavCimsRealAgePanel.height = 40f;
					this.FavCimsRealAgePanel.relativePosition = new Vector3(this.FavCimsAgePhasePanel.relativePosition.x + this.FavCimsAgePhasePanel.width, 0f);
					this.FavCimsRealAge = this.FavCimsRealAgePanel.AddUIComponent<UIButton>();
					this.FavCimsRealAge.name = "FavCimsRealAge";
					this.FavCimsRealAge.width = this.FavCimsRealAgePanel.width;
					this.FavCimsRealAge.height = this.FavCimsRealAgePanel.height;
					this.FavCimsRealAge.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsRealAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsRealAge.playAudioEvents = true;
					this.FavCimsRealAge.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsRealAge.font.size = 15;
					this.FavCimsRealAge.textScale = 1f;
					this.FavCimsRealAge.wordWrap = true;
					this.FavCimsRealAge.textPadding.left = 5;
					this.FavCimsRealAge.textPadding.right = 5;
					this.FavCimsRealAge.textColor = new Color32(byte.MaxValue, 204, 0, 32);
					this.FavCimsRealAge.outlineSize = 1;
					this.FavCimsRealAge.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsRealAge.useDropShadow = true;
					this.FavCimsRealAge.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsRealAge.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsRealAge.maximumSize = new Vector2(this.FavCimsRealAgePanel.width, this.FavCimsRealAgePanel.height);
					this.FavCimsRealAge.isInteractive = false;
					this.FavCimsRealAge.relativePosition = new Vector3(0f, 0f);
					this.FavCimsSeparatorSprite4 = this.FavCimsRealAgePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite4.name = "FavCimsSeparatorSprite4";
					this.FavCimsSeparatorSprite4.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite4.relativePosition = new Vector3(0f, 0f);
					this.FavCimsEducationPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsEducationPanel.name = "FavCimsEducationPanel";
					this.FavCimsEducationPanel.width = FavoriteCimsMainPanel.FavCimsEduColText.width;
					this.FavCimsEducationPanel.height = 40f;
					this.FavCimsEducationPanel.relativePosition = new Vector3(this.FavCimsRealAgePanel.relativePosition.x + this.FavCimsRealAgePanel.width, 0f);
					this.FavCimsEducation = this.FavCimsEducationPanel.AddUIComponent<UIButton>();
					this.FavCimsEducation.name = "FavCimsEducation";
					this.FavCimsEducation.width = this.FavCimsEducationPanel.width;
					this.FavCimsEducation.height = this.FavCimsEducationPanel.height;
					this.FavCimsEducation.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsEducation.playAudioEvents = true;
					this.FavCimsEducation.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsEducation.font.size = 15;
					this.FavCimsEducation.textScale = 1f;
					this.FavCimsEducation.wordWrap = true;
					this.FavCimsEducation.textPadding.left = 5;
					this.FavCimsEducation.textPadding.right = 5;
					this.FavCimsEducation.textColor = new Color32(byte.MaxValue, 204, 0, 32);
					this.FavCimsEducation.outlineSize = 1;
					this.FavCimsEducation.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsEducation.useDropShadow = true;
					this.FavCimsEducation.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsEducation.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsEducation.maximumSize = new Vector2(this.FavCimsEducationPanel.width, this.FavCimsEducationPanel.height);
					this.FavCimsEducation.isInteractive = false;
					this.FavCimsEducation.relativePosition = new Vector3(0f, 0f);
					this.FavCimsSeparatorSprite5 = this.FavCimsEducationPanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite5.name = "FavCimsSeparatorSprite5";
					this.FavCimsSeparatorSprite5.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite5.relativePosition = new Vector3(0f, 0f);
					this.FavCimsCitizenHomePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsCitizenHomePanel.name = "FavCimsCitizenHomePanel";
					this.FavCimsCitizenHomePanel.width = FavoriteCimsMainPanel.FavCimsHomeColText.width;
					this.FavCimsCitizenHomePanel.height = 40f;
					this.FavCimsCitizenHomePanel.relativePosition = new Vector3(this.FavCimsEducationPanel.relativePosition.x + this.FavCimsEducationPanel.width, 0f);
					this.FavCimsCitizenHome = this.FavCimsCitizenHomePanel.AddUIComponent<UIButton>();
					this.FavCimsCitizenHome.name = "FavCimsCitizenHome";
					this.FavCimsCitizenHome.width = this.FavCimsCitizenHomePanel.width;
					this.FavCimsCitizenHome.height = this.FavCimsCitizenHomePanel.height;
					this.FavCimsCitizenHome.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsCitizenHome.textHorizontalAlignment = 0;
					this.FavCimsCitizenHome.playAudioEvents = true;
					this.FavCimsCitizenHome.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsCitizenHome.font.size = 15;
					this.FavCimsCitizenHome.textScale = 0.85f;
					this.FavCimsCitizenHome.wordWrap = true;
					this.FavCimsCitizenHome.textPadding.left = 40;
					this.FavCimsCitizenHome.textPadding.right = 5;
					this.FavCimsCitizenHome.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsCitizenHome.outlineSize = 1;
					this.FavCimsCitizenHome.textColor = new Color32(21, 59, 96, 140);
					this.FavCimsCitizenHome.hoveredTextColor = new Color32(204, 102, 0, 20);
					this.FavCimsCitizenHome.pressedTextColor = new Color32(153, 0, 0, 0);
					this.FavCimsCitizenHome.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
					this.FavCimsCitizenHome.disabledTextColor = new Color32(51, 51, 51, 160);
					this.FavCimsCitizenHome.useDropShadow = true;
					this.FavCimsCitizenHome.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsCitizenHome.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsCitizenHome.maximumSize = new Vector2(this.FavCimsCitizenHomePanel.width, this.FavCimsCitizenHomePanel.height);
					this.FavCimsCitizenHome.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenHome.eventMouseUp += new MouseEventHandler(this.GoToHome);
					this.FavCimsCitizenHome.relativePosition = new Vector3(0f, 0f);
					this.FavCimsCitizenHomeButton = this.FavCimsCitizenHomePanel.AddUIComponent<UIButton>();
					this.FavCimsCitizenHomeButton.name = "FavCimsCitizenHomeButton";
					this.FavCimsCitizenHomeButton.atlas = MyAtlas.FavCimsAtlas;
					this.FavCimsCitizenHomeButton.size = new Vector2(20f, 40f);
					this.FavCimsCitizenHomeButton.relativePosition = new Vector3(10f, 0f);
					this.FavCimsCitizenHomeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenResidentialLevelSprite = this.FavCimsCitizenHomeButton.AddUIComponent<UITextureSprite>();
					this.FavCimsCitizenResidentialLevelSprite.name = "FavCimsCitizenResidentialLevelSprite";
					this.FavCimsCitizenResidentialLevelSprite.relativePosition = new Vector3(0f, 0f);
					this.FavCimsSeparatorSprite6 = this.FavCimsCitizenHomePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite6.name = "FavCimsSeparatorSprite6";
					this.FavCimsSeparatorSprite6.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite6.relativePosition = new Vector3(0f, 0f);
					this.FavCimsWorkingPlacePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsWorkingPlacePanel.name = "FavCimsWorkingPlacePanel";
					this.FavCimsWorkingPlacePanel.width = FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width;
					this.FavCimsWorkingPlacePanel.height = 40f;
					this.FavCimsWorkingPlacePanel.relativePosition = new Vector3(this.FavCimsCitizenHomePanel.relativePosition.x + this.FavCimsCitizenHomePanel.width, 0f);
					this.FavCimsWorkingPlace = this.FavCimsWorkingPlacePanel.AddUIComponent<UIButton>();
					this.FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
					this.FavCimsWorkingPlace.width = this.FavCimsWorkingPlacePanel.width;
					this.FavCimsWorkingPlace.height = this.FavCimsWorkingPlacePanel.height;
					this.FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsWorkingPlace.textHorizontalAlignment = 0;
					this.FavCimsWorkingPlace.playAudioEvents = true;
					this.FavCimsWorkingPlace.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsWorkingPlace.font.size = 15;
					this.FavCimsWorkingPlace.textScale = 0.85f;
					this.FavCimsWorkingPlace.wordWrap = true;
					this.FavCimsWorkingPlace.textPadding.left = 40;
					this.FavCimsWorkingPlace.textPadding.right = 5;
					this.FavCimsWorkingPlace.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsWorkingPlace.outlineSize = 1;
					this.FavCimsWorkingPlace.textColor = new Color32(21, 59, 96, 140);
					this.FavCimsWorkingPlace.hoveredTextColor = new Color32(204, 102, 0, 20);
					this.FavCimsWorkingPlace.pressedTextColor = new Color32(153, 0, 0, 0);
					this.FavCimsWorkingPlace.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
					this.FavCimsWorkingPlace.disabledTextColor = new Color32(51, 51, 51, 160);
					this.FavCimsWorkingPlace.useDropShadow = true;
					this.FavCimsWorkingPlace.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsWorkingPlace.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsWorkingPlace.maximumSize = new Vector2(this.FavCimsWorkingPlacePanel.width, this.FavCimsWorkingPlacePanel.height);
					this.FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsWorkingPlace.eventMouseUp += new MouseEventHandler(this.GoToWork);
					this.FavCimsWorkingPlace.relativePosition = new Vector3(0f, 0f);
					this.FavCimsWorkingPlaceSprite = this.FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
					this.FavCimsWorkingPlaceSprite.width = 20f;
					this.FavCimsWorkingPlaceSprite.height = 40f;
					this.FavCimsWorkingPlaceSprite.relativePosition = new Vector3(10f, 0f);
					this.FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsWorkingPlaceButton = this.FavCimsWorkingPlaceSprite.AddUIComponent<UIButton>();
					this.FavCimsWorkingPlaceButton.name = "FavCimsWorkingPlaceButton";
					this.FavCimsWorkingPlaceButton.width = 20f;
					this.FavCimsWorkingPlaceButton.height = 20f;
					this.FavCimsWorkingPlaceButton.relativePosition = new Vector3(0f, 10f);
					this.FavCimsWorkingPlaceButton.isInteractive = false;
					this.FavCimsWorkingPlaceButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenWorkPlaceLevelSprite = this.FavCimsWorkingPlaceSprite.AddUIComponent<UITextureSprite>();
					this.FavCimsCitizenWorkPlaceLevelSprite.name = "FavCimsCitizenWorkPlaceLevelSprite";
					this.FavCimsCitizenWorkPlaceLevelSprite.relativePosition = new Vector3(0f, 0f);
					this.FavCimsSeparatorSprite7 = this.FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite7.name = "FavCimsSeparatorSprite7";
					this.FavCimsSeparatorSprite7.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite7.relativePosition = new Vector3(0f, 0f);
					this.FavCimsLastActivityPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsLastActivityPanel.name = "FavCimsLastActivityPanel";
					this.FavCimsLastActivityPanel.width = FavoriteCimsMainPanel.FavCimsLastActColText.width;
					this.FavCimsLastActivityPanel.height = 40f;
					this.FavCimsLastActivityPanel.relativePosition = new Vector3(this.FavCimsWorkingPlacePanel.relativePosition.x + this.FavCimsWorkingPlacePanel.width, 0f);
					this.FavCimsLastActivity = this.FavCimsLastActivityPanel.AddUIComponent<UIButton>();
					this.FavCimsLastActivity.name = "FavCimsLastActivity";
					this.FavCimsLastActivity.width = this.FavCimsLastActivityPanel.width - 40f;
					this.FavCimsLastActivity.height = this.FavCimsLastActivityPanel.height;
					this.FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsLastActivity.textHorizontalAlignment = 0;
					this.FavCimsLastActivity.playAudioEvents = true;
					this.FavCimsLastActivity.font = UIDynamicFont.FindByName("OpenSans-Regular");
					this.FavCimsLastActivity.font.size = 14;
					this.FavCimsLastActivity.textScale = 0.85f;
					this.FavCimsLastActivity.wordWrap = true;
					this.FavCimsLastActivity.textPadding.left = 0;
					this.FavCimsLastActivity.textPadding.right = 5;
					this.FavCimsLastActivity.outlineColor = new Color32(0, 0, 0, 0);
					this.FavCimsLastActivity.outlineSize = 1;
					this.FavCimsLastActivity.textColor = new Color32(21, 59, 96, 140);
					this.FavCimsLastActivity.hoveredTextColor = new Color32(204, 102, 0, 20);
					this.FavCimsLastActivity.pressedTextColor = new Color32(153, 0, 0, 0);
					this.FavCimsLastActivity.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
					this.FavCimsLastActivity.disabledTextColor = new Color32(51, 51, 51, 160);
					this.FavCimsLastActivity.useDropShadow = true;
					this.FavCimsLastActivity.dropShadowOffset = new Vector2(1f, -1f);
					this.FavCimsLastActivity.dropShadowColor = new Color32(0, 0, 0, 0);
					this.FavCimsLastActivity.maximumSize = new Vector2(this.FavCimsLastActivityPanel.width - 40f, this.FavCimsLastActivityPanel.height);
					this.FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsLastActivity.eventMouseUp += new MouseEventHandler(this.GoToTarget);
					this.FavCimsLastActivity.relativePosition = new Vector3(40f, 0f);
					this.FavCimsLastActivityVehicleButton = this.FavCimsLastActivityPanel.AddUIComponent<UIButton>();
					this.FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
					this.FavCimsLastActivityVehicleButton.width = 26f;
					this.FavCimsLastActivityVehicleButton.height = 26f;
					this.FavCimsLastActivityVehicleButton.relativePosition = new Vector3(5f, 7f);
					this.FavCimsLastActivityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsLastActivityVehicleButton.eventMouseUp += new MouseEventHandler(this.GoToVehicle);
					this.FavCimsSeparatorSprite8 = this.FavCimsLastActivityPanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite8.name = "FavCimsSeparatorSprite8";
					this.FavCimsSeparatorSprite8.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite8.relativePosition = new Vector3(0f, 0f);
					this.FavCimsCloseRowPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
					this.FavCimsCloseRowPanel.name = "FavCimsCloseRowPanel";
					this.FavCimsCloseRowPanel.width = FavoriteCimsMainPanel.FavCimsCloseButtonCol.width;
					this.FavCimsCloseRowPanel.height = 40f;
					this.FavCimsCloseRowPanel.relativePosition = new Vector3(this.FavCimsLastActivityPanel.relativePosition.x + this.FavCimsLastActivityPanel.width, 0f);
					this.FavCimsRowCloseButton = this.FavCimsCloseRowPanel.AddUIComponent<UIButton>();
					this.FavCimsRowCloseButton.name = "FavCimsRowCloseButton";
					this.FavCimsRowCloseButton.width = 26f;
					this.FavCimsRowCloseButton.height = 26f;
					this.FavCimsRowCloseButton.normalBgSprite = "buttonclose";
					this.FavCimsRowCloseButton.hoveredBgSprite = "buttonclosehover";
					this.FavCimsRowCloseButton.pressedBgSprite = "buttonclosepressed";
					this.FavCimsRowCloseButton.opacity = 0.9f;
					this.FavCimsRowCloseButton.playAudioEvents = true;
					this.FavCimsRowCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsRowCloseButton.eventClick += delegate
					{
						try
						{
							FavCimsCore.RemoveRowAndRemoveFav(this.MyInstanceID, this.citizenINT);
							bool flag8 = this.MyFamily != null;
							if (flag8)
							{
								this.MyFamily.Hide();
								this.MyFamily.MyInstanceID = InstanceID.Empty;
								this.MyFamily = null;
							}
							bool flag9 = UIView.Find<UILabel>("DefaultTooltip");
							if (flag9)
							{
								UIView.Find<UILabel>("DefaultTooltip").Hide();
							}
							UnityEngine.Object.Destroy(base.gameObject);
						}
						catch (Exception ex3)
						{
							Debug.Error("Can't remove row " + ex3.ToString());
						}
					};
					this.FavCimsRowCloseButton.relativePosition = new Vector3(this.FavCimsCloseRowPanel.width / 2f - this.FavCimsRowCloseButton.width / 2f, 7f);
					this.FavCimsSeparatorSprite9 = this.FavCimsCloseRowPanel.AddUIComponent<UITextureSprite>();
					this.FavCimsSeparatorSprite9.name = "FavCimsSeparatorSprite9";
					this.FavCimsSeparatorSprite9.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite9.relativePosition = new Vector3(0f, 0f);
				}
			}
			catch (Exception ex)
			{
				Debug.Error("CitizenRow Create Error : " + ex.ToString());
			}
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				bool firstRun = this.FirstRun;
				if (firstRun)
				{
					this.secondsForceRun -= 1f * Time.deltaTime;
					bool flag = this.secondsForceRun > 0f;
					if (flag)
					{
						this.execute = true;
					}
					else
					{
						this.FirstRun = false;
					}
				}
				else
				{
					bool flag2 = !FavCimsMainClass.FavCimsPanel.isVisible || base.IsClippedFromParent();
					if (flag2)
					{
						this.FavCimsCitizenSingleRowPanel.Hide();
						this.HiddenRowsSeconds -= 1f * Time.deltaTime;
						bool flag3 = this.HiddenRowsSeconds <= 0f;
						if (flag3)
						{
							this.execute = true;
							this.HiddenRowsSeconds = 30f;
						}
						else
						{
							this.execute = false;
						}
					}
					else
					{
						this.FavCimsCitizenSingleRowPanel.Show();
						this.seconds -= 1f * Time.deltaTime;
						bool flag4 = this.seconds <= 0f;
						if (flag4)
						{
							this.execute = true;
							this.seconds = 0.5f;
						}
						else
						{
							this.execute = false;
						}
					}
				}
			}
		}

		public override void LateUpdate()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				bool flag = this.MyInstanceID.IsEmpty || !FavCimsCore.RowID.ContainsKey(this.citizenINT);
				if (flag)
				{
					bool flag2 = this.MyFamily != null;
					if (flag2)
					{
						this.MyFamily.Hide();
						this.MyFamily.MyInstanceID = InstanceID.Empty;
						this.MyFamily = null;
					}
					UnityEngine.Object.Destroy(base.gameObject);
				}
				else
				{
					bool flag3 = this.DeadOrGone || this.HomeLess;
					if (flag3)
					{
						this.OtherInfoButton.isEnabled = false;
						this.OtherInfoButton.tooltip = FavCimsLang.text("Citizen_Details_NoUnit");
					}
					else
					{
						bool flag4 = CitizenRow.GetTemplate() == -1 && (this.MyFamily == null || this.MyFamily.MyInstanceID != this.MyInstanceID);
						if (flag4)
						{
							bool flag5 = this.MyFamily != null && this.MyFamily.MyInstanceID != this.MyInstanceID;
							if (flag5)
							{
								this.MyFamily = null;
							}
							this.OtherInfoButton.isEnabled = false;
							this.OtherInfoButton.tooltip = FavCimsLang.text("Citizen_Details_fullTemplate");
						}
						else
						{
							bool flag6 = this.MyFamily != null && this.MyFamily.MyInstanceID == this.MyInstanceID && this.MyFamily.isVisible;
							if (flag6)
							{
								this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
							}
							else
							{
								this.OtherInfoButton.normalBgSprite = "CityInfo";
							}
							this.OtherInfoButton.isEnabled = true;
							this.OtherInfoButton.tooltip = FavCimsLang.text("Citizen_Details");
						}
					}
					uint citizen = this.MyInstanceID.Citizen;
					bool flag7 = citizen != 0U && this.MyCitizen.m_citizens.m_buffer[(int)citizen].Dead && !this.CitizenIsDead;
					if (flag7)
					{
						this.CitizenIsDead = true;
						this.CitizenRowData["deathrealage"] = "0";
					}
					bool flag8 = this.execute;
					if (flag8)
					{
						try
						{
							this.CitizenName = this.MyInstance.GetName(this.MyInstanceID);
							this.citizenINT = (int)(uint)((UIntPtr)citizen);
							bool flag9 = this.CitizenName != null && this.CitizenName.Length > 0 && this.CitizenName != this.MyInstancedName;
							if (flag9)
							{
								this.MyInstancedName = this.CitizenName;
							}
							this.citizenInfo = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].GetCitizenInfo(citizen);
							this.FavCimsRowCloseButton.tooltip = FavCimsLang.text("FavStarButton_disable_tooltip");
							bool flag10 = this.FavCimsCitizenSingleRowPanel != null && citizen != 0U && this.CitizenName == this.MyInstancedName && FavCimsCore.RowID.ContainsKey(this.citizenINT);
							if (flag10)
							{
								Citizen.Gender gender = Citizen.GetGender(citizen);
								this.CitizenRowData["gender"] = gender.ToString();
								this.CitizenRowData["name"] = this.MyCitizen.GetCitizenName(citizen);
								this.FavCimsCitizenName.text = this.CitizenRowData["name"];
								bool flag11 = this.CitizenRowData["gender"] == "Female";
								if (flag11)
								{
									this.FavCimsCitizenName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
								}
								bool flag12 = this.CitizenDistrict == 0;
								if (flag12)
								{
									this.FavCimsCitizenName.tooltip = FavCimsLang.text("NowInThisDistrict") + FavCimsLang.text("DistrictNameNoDistrict");
								}
								else
								{
									this.FavCimsCitizenName.tooltip = FavCimsLang.text("NowInThisDistrict") + this.MyDistrict.GetDistrictName(this.CitizenDistrict);
								}
								this.tmp_health = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_health;
								this.CitizenRowData["health"] = Citizen.GetHealthLevel(this.tmp_health).ToString();
								Citizen.Education educationLevel = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].EducationLevel;
								this.CitizenRowData["education"] = educationLevel.ToString();
								this.FavCimsEducation.text = FavCimsLang.text("Education_" + this.CitizenRowData["education"] + "_" + this.CitizenRowData["gender"]);
								bool flag13 = this.CitizenRowData["education"] == "ThreeSchools";
								if (flag13)
								{
									this.FavCimsEducation.textColor = new Color32(102, 204, 0, 60);
								}
								else
								{
									bool flag14 = this.CitizenRowData["education"] == "TwoSchools";
									if (flag14)
									{
										this.FavCimsEducation.textColor = new Color32(byte.MaxValue, 204, 0, 32);
									}
									else
									{
										bool flag15 = this.CitizenRowData["education"] == "OneSchool";
										if (flag15)
										{
											this.FavCimsEducation.textColor = new Color32(byte.MaxValue, 102, 0, 16);
										}
										else
										{
											this.FavCimsEducation.textColor = new Color32(153, 0, 0, 0);
										}
									}
								}
								this.tmp_wellbeing = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_wellbeing;
								this.CitizenRowData["wellbeing"] = Citizen.GetWellbeingLevel(educationLevel, this.tmp_wellbeing).ToString();
								this.tmp_happiness = Citizen.GetHappiness(this.tmp_health, this.tmp_wellbeing);
								this.CitizenRowData["happiness_icon"] = CitizenRow.GetHappinessString(Citizen.GetHappinessLevel(this.tmp_happiness));
								this.FavCimsHappyIcon.normalBgSprite = this.CitizenRowData["happiness_icon"];
								this.FavCimsHappyIcon.tooltip = FavCimsLang.text(this.CitizenRowData["happiness_icon"]);
								this.tmp_age = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_age;
								this.CitizenRowData["agegroup"] = Citizen.GetAgeGroup(this.tmp_age).ToString();
								this.FavCimsAgePhase.text = FavCimsLang.text("AgePhase_" + this.CitizenRowData["agegroup"] + "_" + this.CitizenRowData["gender"]);
								this.RealAge = FavCimsCore.CalculateCitizenAge(this.tmp_age);
								bool flag16 = this.RealAge <= 12;
								if (flag16)
								{
									this.FavCimsRealAge.text = this.RealAge.ToString();
									this.FavCimsRealAge.textColor = new Color32(102, 204, 0, 60);
									this.FavCimsAgePhase.textColor = new Color32(102, 204, 0, 60);
								}
								else
								{
									bool flag17 = this.RealAge <= 19;
									if (flag17)
									{
										this.FavCimsRealAge.text = this.RealAge.ToString();
										this.FavCimsRealAge.textColor = new Color32(0, 102, 51, 100);
										this.FavCimsAgePhase.textColor = new Color32(0, 102, 51, 100);
									}
									else
									{
										bool flag18 = this.RealAge <= 25;
										if (flag18)
										{
											this.FavCimsRealAge.text = this.RealAge.ToString();
											this.FavCimsRealAge.textColor = new Color32(byte.MaxValue, 204, 0, 32);
											this.FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 204, 0, 32);
										}
										else
										{
											bool flag19 = this.RealAge <= 65;
											if (flag19)
											{
												this.FavCimsRealAge.text = this.RealAge.ToString();
												this.FavCimsRealAge.textColor = new Color32(byte.MaxValue, 102, 0, 16);
												this.FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 102, 0, 16);
											}
											else
											{
												bool flag20 = this.RealAge <= 90;
												if (flag20)
												{
													this.FavCimsRealAge.text = this.RealAge.ToString();
													this.FavCimsRealAge.textColor = new Color32(153, 0, 0, 0);
													this.FavCimsAgePhase.textColor = new Color32(153, 0, 0, 0);
												}
												else
												{
													this.FavCimsRealAge.text = this.RealAge.ToString();
													this.FavCimsRealAge.textColor = new Color32(byte.MaxValue, 0, 0, 0);
													this.FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 0, 0, 0);
												}
											}
										}
									}
								}
								this.CitizenHome = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_homeBuilding;
								bool flag21 = this.CitizenHome > 0;
								if (flag21)
								{
									this.HomeLess = false;
									this.CitizenHomeID.Building = this.CitizenHome;
									this.FavCimsCitizenHome.text = this.MyBuilding.GetBuildingName(this.CitizenHome, this.MyInstanceID);
									this.FavCimsCitizenHome.isEnabled = true;
									this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
									this.HomeInfo = this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenHomeID.Index].Info;
									bool flag22 = this.HomeInfo.m_class.m_service == ItemClass.Service.Residential;
									if (flag22)
									{
										this.FavCimsCitizenHome.tooltip = null;
										bool flag23 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh;
										if (flag23)
										{
											this.FavCimsCitizenHome.textColor = new Color32(0, 102, 51, 100);
											this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
											this.FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString());
										}
										else
										{
											bool flag24 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHighEco;
											if (flag24)
											{
												this.FavCimsCitizenHome.textColor = new Color32(0, 102, 51, 100);
												this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
												this.FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString()) + " Eco";
											}
											else
											{
												bool flag25 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLowEco;
												if (flag25)
												{
													this.FavCimsCitizenHome.textColor = new Color32(0, 153, 0, 80);
													this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
													this.FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString()) + " Eco";
												}
												else
												{
													bool flag26 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow;
													if (flag26)
													{
														this.FavCimsCitizenHome.textColor = new Color32(0, 153, 0, 80);
														this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
														this.FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString());
													}
												}
											}
										}
										switch (this.HomeInfo.m_class.m_level)
										{
										case ItemClass.Level.Level2:
											this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[2];
											break;
										case ItemClass.Level.Level3:
											this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[3];
											break;
										case ItemClass.Level.Level4:
											this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[4];
											break;
										case ItemClass.Level.Level5:
											this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[5];
											break;
										default:
											this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[1];
											break;
										}
										this.HomeDistrict = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenHomeID.Index].m_position);
										bool flag27 = this.HomeDistrict == 0;
										if (flag27)
										{
											this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
										}
										else
										{
											this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(this.HomeDistrict);
										}
									}
								}
								else
								{
									this.FavCimsCitizenHome.text = FavCimsLang.text("Citizen_HomeLess");
									this.FavCimsCitizenHome.isEnabled = false;
									this.FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
									this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text("DistrictNameNoDistrict");
									this.FavCimsCitizenHome.tooltip = FavCimsLang.text("Citizen_HomeLess_tooltip");
									this.FavCimsCitizenResidentialLevelSprite.texture = null;
									this.HomeLess = true;
								}
								this.WorkPlace = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_workBuilding;
								bool flag28 = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].GetCurrentSchoolLevel(citizen) != ItemClass.Level.None;
								if (flag28)
								{
									this.isStudent = true;
									this.FavCimsWorkingPlaceButton.normalBgSprite = null;
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
									this.FavCimsWorkingPlace.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].GetCurrentSchoolLevel(citizen).ToString()) + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
								}
								else
								{
									bool flag29 = this.WorkPlace == 0;
									if (flag29)
									{
										this.FavCimsWorkingPlaceButton.normalBgSprite = null;
										bool flag30 = (this.MyCitizen.m_citizens.m_buffer[(int)citizen].m_flags & Citizen.Flags.Tourist) > 0;
										if (flag30)
										{
											string text = string.Empty;
											bool flag31 = SteamHelper.IsDLCOwned(SteamHelper.DLC.CampusDLC);
                                            if (flag31)
											{
												float num = Singleton<ImmaterialResourceManager>.instance.CheckExchangeStudentAttractivenessBonus() * 100f;
                                                Randomizer m_randomizer = new(citizen);
                                                text = (((double)m_randomizer.Int32(0, 100) >= (double)num) ? Locale.Get("CITIZEN_OCCUPATION_TOURIST") : Locale.Get("CITIZEN_OCCUPATION_EXCHANGESTUDENT"));
											}
											else
											{
												text = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
											}
											this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
											this.FavCimsWorkingPlace.text = text;
											this.FavCimsWorkingPlace.isEnabled = false;
											this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Citizen_Tourist_tooltip");
											this.FavCimsWorkingPlaceSprite.tooltip = null;
											this.FavCimsWorkingPlaceButton.tooltip = null;
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
										}
										else
										{
											bool flag32 = this.tmp_age >= 180;
											if (flag32)
											{
												this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
												this.FavCimsWorkingPlace.text = FavCimsLang.text("Citizen_Retired");
												this.FavCimsWorkingPlace.isEnabled = false;
												this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Citizen_Retired_tooltip");
												this.FavCimsWorkingPlaceSprite.tooltip = null;
												this.FavCimsWorkingPlaceButton.tooltip = null;
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
											}
											else
											{
												this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
												this.FavCimsWorkingPlace.text = Locale.Get("CITIZEN_OCCUPATION_UNEMPLOYED");
												this.FavCimsWorkingPlace.isEnabled = false;
												this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Unemployed_tooltip");
												this.FavCimsWorkingPlaceSprite.tooltip = null;
												this.FavCimsWorkingPlaceButton.tooltip = null;
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
											}
										}
									}
								}
								bool flag33 = this.WorkPlace > 0;
								if (flag33)
								{
									string text2 = string.Empty;
									bool flag34 = !this.isStudent;
									if (flag34)
									{
										CommonBuildingAI commonBuildingAI = this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlace].Info.m_buildingAI as CommonBuildingAI;
										bool flag35 = commonBuildingAI != null;
										if (flag35)
										{
											text2 = commonBuildingAI.GetTitle(gender, educationLevel, this.WorkPlace, citizen);
										}
										bool flag36 = text2 == string.Empty;
										if (flag36)
										{
											int num2 = new Randomizer((uint)this.WorkPlace + citizen).Int32(1, 5);
											switch (educationLevel)
											{
											case Citizen.Education.Uneducated:
												text2 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
												break;
											case Citizen.Education.OneSchool:
												text2 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
												break;
											case Citizen.Education.TwoSchools:
												text2 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
												break;
											case Citizen.Education.ThreeSchools:
												text2 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
												break;
											}
										}
									}
									this.WorkPlaceID.Building = this.WorkPlace;
									this.FavCimsWorkingPlace.text = text2 + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
									this.FavCimsWorkingPlace.isEnabled = true;
									this.WorkInfo = this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlaceID.Index].Info;
									this.FavCimsWorkingPlaceSprite.texture = null;
									bool flag37 = this.WorkInfo.m_class.m_service == ItemClass.Service.Commercial;
									if (flag37)
									{
										this.FavCimsWorkingPlaceButton.normalBgSprite = null;
										bool flag38 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh;
										if (flag38)
										{
											this.FavCimsWorkingPlace.textColor = new Color32(0, 51, 153, 147);
											this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
											this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
										}
										else
										{
											bool flag39 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialEco;
											if (flag39)
											{
												this.FavCimsWorkingPlace.textColor = new Color32(0, 150, 136, 116);
												this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
												this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialEco");
											}
											else
											{
												bool flag40 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLeisure;
												if (flag40)
												{
													this.FavCimsWorkingPlace.textColor = new Color32(219, 68, 55, 3);
													this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
													this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialLeisure");
												}
												else
												{
													bool flag41 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialTourist;
													if (flag41)
													{
														this.FavCimsWorkingPlace.textColor = new Color32(156, 39, 176, 194);
														this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
														this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialTourist");
													}
													else
													{
														this.FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
														this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
														this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
													}
												}
											}
										}
										bool flag42 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow;
										if (flag42)
										{
											ItemClass.Level level = this.WorkInfo.m_class.m_level;
											ItemClass.Level level2 = level;
											if (level2 != ItemClass.Level.Level2)
											{
												if (level2 != ItemClass.Level.Level3)
												{
													this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[1];
												}
												else
												{
													this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[3];
												}
											}
											else
											{
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[2];
											}
										}
										else
										{
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
										}
									}
									else
									{
										bool flag43 = this.WorkInfo.m_class.m_service == ItemClass.Service.Industrial;
										if (flag43)
										{
											this.FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
											this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
											ItemClass.SubService subService = this.WorkInfo.m_class.m_subService;
											ItemClass.SubService subService2 = subService;
											switch (subService2)
											{
											case ItemClass.SubService.IndustrialForestry:
												this.FavCimsWorkingPlaceSprite.texture = null;
												this.FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
												break;
											case ItemClass.SubService.IndustrialFarming:
												this.FavCimsWorkingPlaceSprite.texture = null;
												this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
												break;
											case ItemClass.SubService.IndustrialOil:
												this.FavCimsWorkingPlaceSprite.texture = null;
												this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
												break;
											case ItemClass.SubService.IndustrialOre:
												this.FavCimsWorkingPlaceSprite.texture = null;
												this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
												break;
											default:
												switch (subService2)
												{
												case ItemClass.SubService.PlayerIndustryForestry:
													this.FavCimsWorkingPlaceSprite.texture = null;
													this.FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
													break;
												case ItemClass.SubService.PlayerIndustryFarming:
													this.FavCimsWorkingPlaceSprite.texture = null;
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
													break;
												case ItemClass.SubService.PlayerIndustryOil:
													this.FavCimsWorkingPlaceSprite.texture = null;
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
													break;
												case ItemClass.SubService.PlayerIndustryOre:
													this.FavCimsWorkingPlaceSprite.texture = null;
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
													break;
												default:
													this.FavCimsWorkingPlaceButton.normalBgSprite = null;
													this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
													break;
												}
												break;
											}
											bool flag44 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric;
											if (flag44)
											{
												ItemClass.Level level3 = this.WorkInfo.m_class.m_level;
												ItemClass.Level level4 = level3;
												if (level4 != ItemClass.Level.Level2)
												{
													if (level4 != ItemClass.Level.Level3)
													{
														this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[1];
													}
													else
													{
														this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[3];
													}
												}
												else
												{
													this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[2];
												}
											}
											else
											{
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
											}
										}
										else
										{
											bool flag45 = this.WorkInfo.m_class.m_service == ItemClass.Service.Office;
											if (flag45)
											{
												this.FavCimsWorkingPlaceButton.normalBgSprite = null;
												this.FavCimsWorkingPlace.textColor = new Color32(0, 204, byte.MaxValue, 128);
												this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;
												ItemClass.SubService subService3 = this.WorkInfo.m_class.m_subService;
												ItemClass.SubService subService4 = subService3;
												if (subService4 != ItemClass.SubService.OfficeHightech)
												{
													this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office");
												}
												else
												{
													this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office") + " Eco";
												}
												ItemClass.Level level5 = this.WorkInfo.m_class.m_level;
												ItemClass.Level level6 = level5;
												if (level6 != ItemClass.Level.Level2)
												{
													if (level6 != ItemClass.Level.Level3)
													{
														this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[1];
													}
													else
													{
														this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[3];
													}
												}
												else
												{
													this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[2];
												}
											}
											else
											{
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
												this.FavCimsWorkingPlace.textColor = new Color32(153, 102, 51, 20);
												switch (this.WorkInfo.m_class.m_service)
												{
												case ItemClass.Service.Electricity:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyPowerSaving";
													this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Electricity_job");
													goto IL_1D8D;
												case ItemClass.Service.Water:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyWaterSaving";
													this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Water_job");
													goto IL_1D8D;
												case ItemClass.Service.Beautification:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarBeautificationParksnPlazas";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Beautification");
													goto IL_1D8D;
												case ItemClass.Service.Garbage:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyRecycling";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Garbage");
													goto IL_1D8D;
												case ItemClass.Service.HealthCare:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconHealthcareFocused";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Healthcare");
													goto IL_1D8D;
												case ItemClass.Service.PoliceDepartment:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconPolice";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Police");
													goto IL_1D8D;
												case ItemClass.Service.Education:
													this.FavCimsWorkingPlace.textColor = new Color32(0, 102, 51, 100);
													this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconEducationPressed";
													goto IL_1D8D;
												case ItemClass.Service.Monument:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "FeatureMonumentLevel6";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Monuments");
													goto IL_1D8D;
												case ItemClass.Service.FireDepartment:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconFireSafety";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "FireDepartment");
													goto IL_1D8D;
												case ItemClass.Service.PublicTransport:
												{
													ItemClass.SubService subService5 = this.WorkInfo.m_class.m_subService;
													ItemClass.SubService subService6 = subService5;
													if (subService6 != ItemClass.SubService.PublicTransportPost)
													{
														this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFreePublicTransport";
														this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
													}
													else
													{
														this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarPublicTransportPost";
														this.FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
													}
													goto IL_1D8D;
												}
												case ItemClass.Service.Disaster:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarFireDepartmentDisaster";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
													goto IL_1D8D;
												case ItemClass.Service.Museums:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaMuseums";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
													goto IL_1D8D;
												case ItemClass.Service.VarsitySports:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaVarsitySports";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
													goto IL_1D8D;
												case ItemClass.Service.Fishing:
													this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarIndustryFishing";
													this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
													goto IL_1D8D;
												}
												this.FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
												this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyNone";
												this.FavCimsWorkingPlace.tooltip = null;
												IL_1D8D:
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
											}
										}
									}
									this.WorkDistrict = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlaceID.Index].m_position);
									bool flag46 = this.WorkDistrict == 0;
									if (flag46)
									{
										this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
									}
									else
									{
										this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(this.WorkDistrict);
									}
								}
								else
								{
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
									this.FavCimsWorkingPlaceButton.tooltip = null;
									this.FavCimsWorkingPlaceSprite.tooltip = null;
								}
								this.InstanceCitizenID = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_instance;
								this.citizenInstance = this.MyCitizen.m_instances.m_buffer[(int)this.InstanceCitizenID];
								bool flag47 = this.citizenInstance.m_targetBuilding > 0;
								if (flag47)
								{
									this.CitizenVehicle = this.MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
									this.MyVehicleID = InstanceID.Empty;
									this.GoingOutside = (this.MyBuilding.m_buildings.m_buffer[(int)this.citizenInstance.m_targetBuilding].m_flags & (Building.Flags)192) > 0;
									bool flag48 = this.CitizenVehicle > 0;
									if (flag48)
									{
										this.MyVehicleID.Vehicle = this.CitizenVehicle;
										this.FavCimsLastActivityVehicleButton.isEnabled = true;
										this.VehInfo = this.MyVehicle.m_vehicles.m_buffer[(int)this.CitizenVehicle].Info;
										string text3 = PrefabCollection<VehicleInfo>.PrefabName((uint)this.VehInfo.m_prefabDataIndex);
										bool flag49 = text3 == "Train Passenger";
										if (flag49)
										{
											this.CitizenVehicleName = Locale.Get("VEHICLE_TITLE", "Train Engine");
										}
										else
										{
											this.CitizenVehicleName = this.MyVehicle.GetVehicleName(this.CitizenVehicle);
										}
										bool flag50 = this.VehInfo.m_class.m_service == ItemClass.Service.Residential;
										if (flag50)
										{
											bool flag51 = this.CitizenVehicleName.Like("Bicycle");
											if (flag51)
											{
												this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
												this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconTouristBicycleVehicle";
												this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristBicycleVehicle";
												this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("PROPS_DESC", "bicycle01");
											}
											else
											{
												bool flag52 = this.CitizenVehicleName.Like("Scooter");
												if (flag52)
												{
													this.FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsIconScooter";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "FavCimsIconScooter";
													this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName;
												}
												else
												{
													this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconCitizenVehicle";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristVehicle";
													this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName;
												}
											}
											bool flag53 = this.VehInfo.m_vehicleAI.GetOwnerID(this.CitizenVehicle, ref this.MyVehicle.m_vehicles.m_buffer[(int)this.CitizenVehicle]).Citizen == citizen;
											if (flag53)
											{
												bool goingOutside = this.GoingOutside;
												if (goingOutside)
												{
													this.LeaveCity = true;
												}
											}
										}
										else
										{
											bool flag54 = this.VehInfo.m_class.m_service == ItemClass.Service.PublicTransport;
											if (flag54)
											{
												this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
												bool goingOutside2 = this.GoingOutside;
												if (goingOutside2)
												{
													this.LeaveCity = true;
												}
												ItemClass.SubService subService7 = this.VehInfo.m_class.m_subService;
												ItemClass.SubService subService8 = subService7;
												switch (subService8)
												{
												case ItemClass.SubService.PublicTransportBus:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Bus") + " - " + Locale.Get("SUBSERVICE_DESC", "Bus");
													break;
												case ItemClass.SubService.PublicTransportMetro:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Metro") + " - " + Locale.Get("SUBSERVICE_DESC", "Metro");
													break;
												case ItemClass.SubService.PublicTransportTrain:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get("SUBSERVICE_DESC", "Train");
													break;
												case ItemClass.SubService.PublicTransportShip:
												{
													bool flag55 = this.CitizenVehicleName.Like("Ferry");
													if (flag55)
													{
														this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
														this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
														this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
														this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
														this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get("FEATURES_DESC", "Ferry");
													}
													else
													{
														this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
														this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
														this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
														this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
														this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Ship");
													}
													break;
												}
												case ItemClass.SubService.PublicTransportPlane:
												{
													bool flag56 = this.CitizenVehicleName.Like("Blimp");
													if (flag56)
													{
														this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconPolicyEducationalBlimps";
														this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
														this.FavCimsLastActivityVehicleButton.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
														this.FavCimsLastActivityVehicleButton.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";
														this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get("FEATURES_DESC", "Blimp");
													}
													else
													{
														this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
														this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
														this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
														this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";
														this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Plane");
													}
													break;
												}
												case ItemClass.SubService.PublicTransportTaxi:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTaxi";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get("SUBSERVICE_DESC", "Taxi");
													break;
												case ItemClass.SubService.PublicTransportTram:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTram";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTramHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTramFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTramPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Tram") + " - " + Locale.Get("SUBSERVICE_DESC", "Tram");
													break;
												case ItemClass.SubService.BeautificationParks:
												case ItemClass.SubService.CommercialLeisure:
												case ItemClass.SubService.CommercialTourist:
												case ItemClass.SubService.OfficeGeneric:
												case ItemClass.SubService.OfficeHightech:
												case ItemClass.SubService.CommercialEco:
												case ItemClass.SubService.ResidentialLowEco:
												case ItemClass.SubService.ResidentialHighEco:
													break;
												case ItemClass.SubService.PublicTransportMonorail:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMonorail";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get("SUBSERVICE_DESC", "Monorail");
													break;
												case ItemClass.SubService.PublicTransportCableCar:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportCableCar";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get("SUBSERVICE_DESC", "CableCar");
													break;
												case ItemClass.SubService.PublicTransportTours:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTours";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportToursHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportToursFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportToursPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Tours");
													break;
												case ItemClass.SubService.PublicTransportPost:
													this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPost";
													this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPostHovered";
													this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPostFocused";
													this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPostPressed";
													this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Post");
													break;
												default:
													if (subService8 == ItemClass.SubService.PublicTransportTrolleybus)
													{
														this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrolleybus";
														this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrolleybusHovered";
														this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrolleybusFocused";
														this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrolleybusPressed";
														this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Trolleybus");
													}
													break;
												}
											}
										}
									}
									else
									{
										this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
										bool goingOutside3 = this.GoingOutside;
										if (goingOutside3)
										{
											this.LeaveCity = true;
										}
										this.FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
										this.FavCimsLastActivityVehicleButton.isEnabled = false;
										this.FavCimsLastActivityVehicleButton.tooltip = FavCimsLang.text("Vehicle_on_foot");
									}
								}
								else
								{
									this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
									this.FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
									this.FavCimsLastActivityVehicleButton.isEnabled = false;
									this.FavCimsLastActivityVehicleButton.tooltip = null;
								}
								this.CitizenStatus = this.citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index], out this.MyTargetID);
								this.CitizenTarget = this.MyBuilding.GetBuildingName(this.MyTargetID.Building, this.MyInstanceID);
								this.FavCimsLastActivity.text = this.CitizenStatus + " " + this.CitizenTarget;
								bool flag57 = !this.MyTargetID.IsEmpty;
								if (flag57)
								{
									this.TargetDistrict = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)this.MyTargetID.Index].m_position);
									bool flag58 = this.TargetDistrict == 0;
									if (flag58)
									{
										this.FavCimsLastActivity.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
									}
									else
									{
										this.FavCimsLastActivity.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(this.TargetDistrict);
									}
								}
								this.CitizenDistrict = (int)this.MyDistrict.GetDistrict(this.citizenInstance.GetSmoothPosition(this.InstanceCitizenID));
								bool flag59 = this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)citizen)))].Arrested && this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)citizen)))].Criminal;
								if (flag59)
								{
									this.FavCimsHappyIcon.atlas = MyAtlas.FavCimsAtlas;
									this.FavCimsHappyIcon.normalBgSprite = "FavCimsCrimeArrested";
									this.FavCimsHappyIcon.tooltip = FavCimsLang.text("Citizen_Arrested");
									bool flag60 = this.MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation == Citizen.Location.Moving;
									if (flag60)
									{
										this.policeveh = this.MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
										bool flag61 = this.policeveh > 0;
										if (flag61)
										{
											this.MyVehicleID.Vehicle = this.policeveh;
											this.FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
											this.FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsPoliceVehicle";
											this.FavCimsLastActivityVehicleButton.isEnabled = true;
											this.FavCimsLastActivityVehicleButton.playAudioEvents = true;
											this.FavCimsLastActivityVehicleButton.tooltip = this.MyVehicle.GetVehicleName(this.policeveh) + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
											this.FavCimsLastActivity.isEnabled = false;
											this.FavCimsLastActivity.text = FavCimsLang.text("Transported_to_Prison");
										}
									}
									else
									{
										this.FavCimsLastActivity.isEnabled = true;
										this.FavCimsLastActivity.text = FavCimsLang.text("Jailed_into") + this.CitizenTarget;
										this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
									}
								}
								else
								{
									this.FavCimsHappyIcon.atlas = UIView.GetAView().defaultAtlas;
									this.FavCimsHappyIcon.normalBgSprite = this.CitizenRowData["happiness_icon"];
									this.FavCimsHappyIcon.tooltip = FavCimsLang.text(this.CitizenRowData["happiness_icon"]);
								}
								bool citizenIsDead = this.CitizenIsDead;
								if (citizenIsDead)
								{
									this.FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
									this.FavCimsHappyIcon.tooltip = FavCimsLang.text("People_Life_Status_Dead");
									bool flag62 = this.CitizenRowData["deathrealage"] == "0";
									if (flag62)
									{
										this.CitizenRowData["deathrealage"] = this.RealAge.ToString();
									}
									this.FavCimsRealAge.text = this.CitizenRowData["deathrealage"];
									bool flag63 = this.DeathDate == null;
									if (flag63)
									{
										this.DeathDate = GameTime.FavCimsDate(FavCimsLang.text("time_format"), "n/a");
										this.DeathTime = GameTime.FavCimsTime();
									}
									this.FavCimsCitizenName.tooltip = string.Concat(new string[]
									{
										FavCimsLang.text("People_Life_Status_Dead"),
										" ",
										FavCimsLang.text("People_Life_Status_Dead_date"),
										" ",
										this.DeathDate,
										" ",
										FavCimsLang.text("People_Life_Status_Dead_time"),
										" ",
										this.DeathTime
									});
									bool flag64 = this.MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation == Citizen.Location.Moving;
									if (flag64)
									{
										this.hearse = this.MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
										bool flag65 = this.hearse > 0;
										if (flag65)
										{
											this.CitizenDead.Citizen = citizen;
											this.MyVehicleID.Vehicle = this.hearse;
											this.FavCimsLastActivityVehicleButton.normalBgSprite = "NotificationIconVerySick";
											this.FavCimsLastActivityVehicleButton.isEnabled = true;
											this.FavCimsLastActivityVehicleButton.playAudioEvents = true;
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Hearse");
											this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_on_hearse");
										}
									}
									else
									{
										bool flag66 = this.MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation != Citizen.Location.Moving && this.hearse == 0;
										if (flag66)
										{
											this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_wait_hearse");
											this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
											this.FavCimsLastActivityVehicleButton.isEnabled = false;
										}
										else
										{
											this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_hisfuneral");
											this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
											this.FavCimsLastActivityVehicleButton.isEnabled = false;
										}
									}
								}
							}
							else
							{
								bool flag67 = this.rowLang == null || this.rowLang != FavCimsLang.GameLanguage;
								if (flag67)
								{
									this.DeadOrGone = false;
								}
								bool flag68 = !this.DeadOrGone;
								if (flag68)
								{
									this.rowLang = FavCimsLang.GameLanguage;
									this.DeadOrGone = true;
									bool flag69 = this.FavCimsCitizenSingleRowPanel != null && FavCimsCore.RowID.ContainsKey(this.citizenINT) && this.MyInstancedName.Length > 0;
									if (flag69)
									{
										bool flag70 = this.DeathDate != null;
										if (flag70)
										{
											this.DeathDate = GameTime.FavCimsDate(FavCimsLang.text("time_format"), this.DeathDate);
										}
										bool flag71 = this.DeathDate == null;
										if (flag71)
										{
											this.DeathDate = GameTime.FavCimsDate(FavCimsLang.text("time_format"), "n/a");
											this.DeathTime = GameTime.FavCimsTime();
										}
										this.FavCimsCitizenName.disabledTextColor = new Color32(51, 51, 51, 160);
										this.FavCimsCitizenName.isEnabled = false;
										this.FavCimsEducation.textColor = new Color32(51, 51, 51, 160);
										this.FavCimsRealAge.textColor = new Color32(51, 51, 51, 160);
										this.FavCimsAgePhase.textColor = new Color32(51, 51, 51, 160);
										System.Random random = new System.Random();
										int num3 = random.Next(0, 100);
										bool flag72 = this.RealAge >= 85;
										string text4;
										if (flag72)
										{
											bool flag73 = num3 >= 99;
											if (flag73)
											{
												text4 = "goneaway";
											}
											else
											{
												text4 = "dead";
											}
										}
										else
										{
											bool flag74 = this.RealAge >= 65;
											if (flag74)
											{
												bool flag75 = num3 >= 70;
												if (flag75)
												{
													text4 = "goneaway";
												}
												else
												{
													text4 = "dead";
												}
											}
											else
											{
												bool flag76 = this.RealAge >= 45;
												if (flag76)
												{
													bool flag77 = num3 >= 50;
													if (flag77)
													{
														text4 = "goneaway";
													}
													else
													{
														text4 = "dead";
													}
												}
												else
												{
													bool flag78 = this.RealAge >= 20;
													if (flag78)
													{
														bool flag79 = num3 >= 30;
														if (flag79)
														{
															text4 = "goneaway";
														}
														else
														{
															text4 = "dead";
														}
													}
													else
													{
														bool flag80 = num3 >= 2;
														if (flag80)
														{
															text4 = "goneaway";
														}
														else
														{
															text4 = "dead";
														}
													}
												}
											}
										}
										bool flag81 = !this.LeaveCity && (this.CitizenIsDead || text4 == "dead");
										if (flag81)
										{
											try
											{
												this.FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
												this.FavCimsHappyIcon.tooltip = FavCimsLang.text("People_Life_Status_Dead");
												this.FavCimsCitizenName.text = this.MyInstancedName;
												this.FavCimsCitizenName.tooltip = string.Concat(new string[]
												{
													FavCimsLang.text("People_Life_Status_Dead"),
													" ",
													FavCimsLang.text("People_Life_Status_Dead_date"),
													" ",
													this.DeathDate,
													" ",
													FavCimsLang.text("People_Life_Status_Dead_time"),
													" ",
													this.DeathTime
												});
												this.OtherInfoButton.isEnabled = false;
												this.FavCimsAgePhase.text = FavCimsLang.text("AgePhaseDead_" + this.CitizenRowData["gender"]);
												this.FavCimsCitizenHome.text = FavCimsLang.text("Home_Location_Dead");
												this.FavCimsCitizenHomeButton.normalBgSprite = "houseofthedead";
												this.FavCimsCitizenHome.tooltip = null;
												this.FavCimsCitizenHome.isEnabled = false;
												this.FavCimsCitizenResidentialLevelSprite.texture = null;
												this.FavCimsCitizenHomeButton.tooltip = null;
												this.FavCimsWorkingPlace.isEnabled = false;
												this.FavCimsWorkingPlace.tooltip = null;
												this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
												this.FavCimsWorkingPlaceSprite.tooltip = null;
												this.FavCimsWorkingPlaceButton.tooltip = null;
												this.FavCimsLastActivity.isEnabled = false;
												this.FavCimsLastActivityVehicleButton.isEnabled = false;
												this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
												this.FavCimsLastActivityVehicleButton.tooltip = null;
												this.FavCimsLastActivity.tooltip = null;
												this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_buried");
												this.CitizenRowData.Clear();
											}
											catch (Exception ex)
											{
												Debug.Error("error " + ex.ToString());
											}
										}
										else
										{
											this.FavCimsHappyIcon.normalBgSprite = "";
											this.FavCimsHappyIcon.tooltip = null;
											this.FavCimsHappyOverride.texture = TextureDB.FavCimsHappyOverride_texture;
											this.FavCimsHappyOverride.relativePosition = new Vector3(0f, 0f);
											this.FavCimsHappyOverride.tooltip = FavCimsLang.text("People_Life_Status_IsGone");
											this.FavCimsCitizenName.text = this.MyInstancedName;
											this.FavCimsCitizenName.tooltip = string.Concat(new string[]
											{
												FavCimsLang.text("People_Life_Status_IsGone"),
												" ",
												FavCimsLang.text("People_Life_Status_Dead_date"),
												" ",
												this.DeathDate,
												" ",
												FavCimsLang.text("People_Life_Status_Dead_time"),
												" ",
												this.DeathTime
											});
											this.OtherInfoButton.isEnabled = false;
											this.FavCimsCitizenHome.text = FavCimsLang.text("HomeOutsideTheCity");
											this.FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
											this.FavCimsCitizenHome.tooltip = null;
											this.FavCimsCitizenHome.isEnabled = false;
											this.FavCimsCitizenResidentialLevelSprite.texture = null;
											this.FavCimsCitizenHomeButton.tooltip = null;
											this.FavCimsWorkingPlace.isEnabled = false;
											this.FavCimsWorkingPlace.tooltip = null;
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
											this.FavCimsLastActivity.isEnabled = false;
											this.FavCimsLastActivityVehicleButton.isEnabled = false;
											this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
											this.FavCimsLastActivity.tooltip = null;
											this.CitizenRowData.Clear();
										}
									}
									else
									{
										try
										{
											bool flag82 = this.MyFamily != null;
											if (flag82)
											{
												this.MyFamily.Hide();
												this.MyFamily.MyInstanceID = InstanceID.Empty;
												this.MyFamily = null;
											}
											UnityEngine.Object.Destroy(base.gameObject);
										}
										catch
										{
										}
									}
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}
		
	}
}
