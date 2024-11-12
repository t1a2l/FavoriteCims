using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class CitizenRow : UIPanel
    {
        private float seconds = 0.5f;

        private float secondsForceRun = 2f;

        private float HiddenRowsSeconds = 30f;

        private bool FirstRun = true;

        private bool execute = false;

        public InstanceID MyInstanceID;

        public string MyInstancedName;

        private readonly Dictionary<string, string> CitizenRowData = [];

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private readonly InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private readonly DistrictManager MyDistrict = Singleton<DistrictManager>.instance;

        private static readonly string[] sHappinessLevels = ["VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy"];

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
                    bool flag = MyInstance.SelectInstance(Target);
                    if (flag)
                    {
                        bool flag2 = eventParam.buttons == UIMouseButton.Middle;
                        if (flag2)
                        {
                            bool flag3 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
                            if (flag3)
                            {
                                WorldInfoPanel.Show<TouristWorldInfoPanel>(position, Target);
                            }
                            else
                            {
                                WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
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
                                    WorldInfoPanel.Show<TouristWorldInfoPanel>(position, Target);
                                }
                                else
                                {
                                    WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
                                }
                            }
                            else
                            {
                                ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
                                bool flag6 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
                                if (flag6)
                                {
                                    WorldInfoPanel.Show<TouristWorldInfoPanel>(position, Target);
                                }
                                else
                                {
                                    WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't find the Citizen " + ex.ToString());
                }
            }
        }

        private void GoToHome(UIComponent component, UIMouseEventParameter p)
        {
            bool isEmpty = CitizenHomeID.IsEmpty;
            if (!isEmpty)
            {
                try
                {
                    bool flag = p.buttons == UIMouseButton.Middle;
                    if (flag)
                    {
                        WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(position, CitizenHomeID);
                    }
                    else
                    {
                        bool flag2 = p.buttons == UIMouseButton.Right;
                        if (flag2)
                        {
                            FavCimsMainClass.FavCimsPanel.Hide();
                            ToolsModifierControl.cameraController.SetTarget(CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
                            WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(position, CitizenHomeID);
                        }
                        else
                        {
                            ToolsModifierControl.cameraController.SetTarget(CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
                            WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(position, CitizenHomeID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't find the House " + ex.ToString());
                }
            }
        }

        private void GoToWork(UIComponent component, UIMouseEventParameter p)
        {
            bool isEmpty = WorkPlaceID.IsEmpty;
            if (!isEmpty)
            {
                try
                {
                    bool flag = p.buttons == UIMouseButton.Middle;
                    if (flag)
                    {
                        DefaultTool.OpenWorldInfoPanel(WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        bool flag2 = p.buttons == UIMouseButton.Right;
                        if (flag2)
                        {
                            FavCimsMainClass.FavCimsPanel.Hide();
                            ToolsModifierControl.cameraController.SetTarget(WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
                        }
                        else
                        {
                            ToolsModifierControl.cameraController.SetTarget(WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't find the WorkPlace " + ex.ToString());
                }
            }
        }

        private void GoToTarget(UIComponent component, UIMouseEventParameter p)
        {
            bool isEmpty = MyTargetID.IsEmpty;
            if (!isEmpty)
            {
                try
                {
                    bool flag = p.buttons == UIMouseButton.Middle;
                    if (flag)
                    {
                        DefaultTool.OpenWorldInfoPanel(MyTargetID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        bool flag2 = p.buttons == UIMouseButton.Right;
                        if (flag2)
                        {
                            FavCimsMainClass.FavCimsPanel.Hide();
                            ToolsModifierControl.cameraController.SetTarget(MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(MyTargetID, ToolsModifierControl.cameraController.transform.position);
                        }
                        else
                        {
                            ToolsModifierControl.cameraController.SetTarget(MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(MyTargetID, ToolsModifierControl.cameraController.transform.position);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't find the Target " + ex.ToString());
                }
            }
        }

        private void GoToVehicle(UIComponent component, UIMouseEventParameter p)
        {
            bool isEmpty = MyVehicleID.IsEmpty;
            if (!isEmpty)
            {
                try
                {
                    bool flag = p.buttons == UIMouseButton.Middle;
                    if (flag)
                    {
                        DefaultTool.OpenWorldInfoPanel(MyVehicleID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        bool flag2 = p.buttons == UIMouseButton.Right;
                        if (flag2)
                        {
                            FavCimsMainClass.FavCimsPanel.Hide();
                            ToolsModifierControl.cameraController.SetTarget(MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(MyVehicleID, ToolsModifierControl.cameraController.transform.position);
                        }
                        else
                        {
                            ToolsModifierControl.cameraController.SetTarget(MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(MyVehicleID, ToolsModifierControl.cameraController.transform.position);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't find the Vehicle " + ex.ToString());
                }
            }
        }

        internal static string GetHappinessString(Citizen.Happiness happinessLevel)
        {
            return "NotificationIcon" + sHappinessLevels[(int)happinessLevel];
        }

        public override void Start()
        {
            try
            {
                uint citizen = MyInstanceID.Citizen;
                citizenINT = (int)(uint)(UIntPtr)citizen;
                CitizenName = MyInstance.GetName(MyInstanceID);
                bool flag = MyInstancedName == null;
                if (flag)
                {
                    MyInstancedName = CitizenName;
                }
                bool flag2 = citizenINT != 0 && !FavCimsCore.RowID.ContainsKey(citizenINT) && CitizenName != null && CitizenName.Length > 0;
                if (flag2)
                {
                    FavCimsCore.InsertIdIntoArray(citizenINT);
                    width = 1134f;
                    height = 41f;
                    autoLayoutDirection = LayoutDirection.Vertical;
                    autoLayout = true;
                    autoLayoutPadding = new RectOffset(0, 0, 1, 0);
                    FavCimsCitizenSingleRowPanel = AddUIComponent<UIPanel>();
                    FavCimsCitizenSingleRowPanel.width = width;
                    FavCimsCitizenSingleRowPanel.height = 40f;
                    FavCimsCitizenSingleRowBGSprite = FavCimsCitizenSingleRowPanel.AddUIComponent<UITextureSprite>();
                    FavCimsCitizenSingleRowBGSprite.name = "FavCimsCitizenSingleRowBGSprite";
                    FavCimsCitizenSingleRowBGSprite.width = FavCimsCitizenSingleRowPanel.width;
                    FavCimsCitizenSingleRowBGSprite.height = FavCimsCitizenSingleRowPanel.height;
                    FavCimsCitizenSingleRowBGSprite.AlignTo(FavCimsCitizenSingleRowPanel, UIAlignAnchor.TopLeft);
                    bool flag3 = !FavoriteCimsMainPanel.RowAlternateBackground;
                    if (flag3)
                    {
                        FavDot = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_1.png");
                        FavDot.name = "FavDot_1";
                        FavCimsCitizenSingleRowBGSprite.texture = FavDot;
                        FavoriteCimsMainPanel.RowAlternateBackground = true;
                    }
                    else
                    {
                        FavDot = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_2.png");
                        FavDot.name = "FavDot_2";
                        FavCimsCitizenSingleRowBGSprite.texture = FavDot;
                        FavoriteCimsMainPanel.RowAlternateBackground = false;
                    }
                    FavDot_hover = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_hover.png");
                    FavCimsCitizenSingleRowPanel.eventMouseEnter += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsCitizenSingleRowBGSprite.texture = FavDot_hover;
                    };
                    FavCimsCitizenSingleRowPanel.eventMouseLeave += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        FavCimsCitizenSingleRowBGSprite.texture = FavDot;
                    };
                    FavCimsCitizenHappinessPanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsCitizenHappinessPanel.name = "FavCimsCitizenHappinessPanel";
                    FavCimsCitizenHappinessPanel.width = FavoriteCimsMainPanel.FavCimsHappinesColText.width;
                    FavCimsCitizenHappinessPanel.height = 40f;
                    FavCimsCitizenHappinessPanel.relativePosition = new Vector3(0f, 0f);
                    FavCimsHappyIcon = FavCimsCitizenHappinessPanel.AddUIComponent<UIButton>();
                    FavCimsHappyIcon.width = 30f;
                    FavCimsHappyIcon.height = 30f;
                    FavCimsHappyIcon.isEnabled = false;
                    FavCimsHappyIcon.playAudioEvents = false;
                    FavCimsHappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsHappyIcon.relativePosition = new Vector3(15f, 5f);
                    FavCimsHappyOverride = FavCimsHappyIcon.AddUIComponent<UITextureSprite>();
                    FavCimsHappyOverride.width = 30f;
                    FavCimsHappyOverride.height = 30f;
                    FavCimsHappyOverride.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsCitizenNamePanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsCitizenNamePanel.name = "FavCimsCitizenNamePanel";
                    FavCimsCitizenNamePanel.width = FavoriteCimsMainPanel.FavCimsNameColText.width;
                    FavCimsCitizenNamePanel.height = 40f;
                    FavCimsCitizenNamePanel.relativePosition = new Vector3(FavCimsCitizenHappinessPanel.relativePosition.x + FavCimsCitizenHappinessPanel.width, 0f);
                    FavCimsCitizenName = FavCimsCitizenNamePanel.AddUIComponent<UIButton>();
                    FavCimsCitizenName.name = "FavCimsCitizenName";
                    FavCimsCitizenName.width = FavCimsCitizenNamePanel.width;
                    FavCimsCitizenName.height = FavCimsCitizenNamePanel.height;
                    FavCimsCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsCitizenName.textHorizontalAlignment = 0;
                    FavCimsCitizenName.playAudioEvents = true;
                    FavCimsCitizenName.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsCitizenName.font.size = 15;
                    FavCimsCitizenName.textScale = 1f;
                    FavCimsCitizenName.wordWrap = true;
                    FavCimsCitizenName.textPadding.left = 40;
                    FavCimsCitizenName.textPadding.right = 5;
                    FavCimsCitizenName.textColor = new Color32(204, 204, 51, 40);
                    FavCimsCitizenName.hoveredTextColor = new Color32(204, 102, 0, 20);
                    FavCimsCitizenName.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    FavCimsCitizenName.focusedTextColor = new Color32(153, 0, 0, 0);
                    FavCimsCitizenName.useDropShadow = true;
                    FavCimsCitizenName.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsCitizenName.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsCitizenName.maximumSize = new Vector2(FavCimsCitizenNamePanel.width, FavCimsCitizenNamePanel.height);
                    FavCimsCitizenName.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsCitizenName.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        GoToCitizen(MyInstanceID, eventParam);
                    };
                    FavCimsNameColText_EmptySprite = FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite>();
                    FavCimsNameColText_EmptySprite.width = FavCimsCitizenName.width;
                    FavCimsNameColText_EmptySprite.height = FavCimsCitizenName.height;
                    FavCimsNameColText_EmptySprite.relativePosition = new Vector3(0f, 0f);
                    bool columnSpecialBackground = FavoriteCimsMainPanel.ColumnSpecialBackground;
                    if (columnSpecialBackground)
                    {
                        FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
                        FavCimsNameColText_EmptySprite.opacity = 0.7f;
                    }
                    FavoriteCimsMainPanel.FavCimsNameColText.eventClick += delegate
                    {
                        bool flag4 = FavCimsNameColText_EmptySprite.texture == null;
                        if (flag4)
                        {
                            FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
                            FavCimsNameColText_EmptySprite.opacity = 0.7f;
                            FavoriteCimsMainPanel.ColumnSpecialBackground = true;
                        }
                        else
                        {
                            FavCimsNameColText_EmptySprite.texture = null;
                            FavoriteCimsMainPanel.ColumnSpecialBackground = false;
                        }
                    };
                    FavCimsCitizenName.BringToFront();
                    FavCimsCitizenName.relativePosition = new Vector3(0f, 0f);
                    OtherInfoButton = FavCimsCitizenNamePanel.AddUIComponent<UIButton>();
                    OtherInfoButton.name = "FavCimsOtherInfoButton";
                    OtherInfoButton.width = 20f;
                    OtherInfoButton.height = 20f;
                    OtherInfoButton.playAudioEvents = true;
                    OtherInfoButton.normalBgSprite = "CityInfo";
                    OtherInfoButton.hoveredBgSprite = "CityInfoHovered";
                    OtherInfoButton.pressedBgSprite = "CityInfoPressed";
                    OtherInfoButton.disabledBgSprite = "CityInfoDisabled";
                    OtherInfoButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    OtherInfoButton.relativePosition = new Vector3(10f, 10f);
                    OtherInfoButton.eventClick += delegate
                    {
                        try
                        {
                            bool flag5 = MyFamily == null && GetTemplate() >= 0;
                            if (flag5)
                            {
                                MyFamily = FavCimsMainClass.Templates[GetTemplate()];
                                MyFamily.MyInstanceID = MyInstanceID;
                                MyFamily.Show();
                                MyFamily.BringToFront();
                                OtherInfoButton.normalBgSprite = "CityInfoFocused";
                            }
                            else
                            {
                                bool flag6 = MyFamily != null && !MyFamily.isVisible;
                                if (flag6)
                                {
                                    MyFamily.MyInstanceID = MyInstanceID;
                                    MyFamily.Show();
                                    MyFamily.BringToFront();
                                    OtherInfoButton.normalBgSprite = "CityInfoFocused";
                                }
                                else
                                {
                                    bool flag7 = MyFamily != null;
                                    if (flag7)
                                    {
                                        MyFamily.Hide();
                                        MyFamily.MyInstanceID = InstanceID.Empty;
                                        MyFamily = null;
                                        OtherInfoButton.normalBgSprite = "CityInfo";
                                    }
                                }
                            }
                        }
                        catch (Exception ex2)
                        {
                            Utils.Debug.Error("Error when loading the template : " + ex2.ToString());
                        }
                    };
                    FavCimsSeparatorSprite2 = FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite2.name = "FavCimsSeparatorSprite2";
                    FavCimsSeparatorSprite2.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite2.relativePosition = new Vector3(0f, 0f);
                    FavCimsAgePhasePanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsAgePhasePanel.name = "FavCimsAgePhasePanel";
                    FavCimsAgePhasePanel.width = FavoriteCimsMainPanel.FavCimsAgePhaseColText.width;
                    FavCimsAgePhasePanel.height = 40f;
                    FavCimsAgePhasePanel.relativePosition = new Vector3(FavCimsCitizenNamePanel.relativePosition.x + FavCimsCitizenNamePanel.width, 0f);
                    FavCimsAgePhase = FavCimsAgePhasePanel.AddUIComponent<UIButton>();
                    FavCimsAgePhase.name = "FavCimsAgePhase";
                    FavCimsAgePhase.width = FavCimsAgePhasePanel.width;
                    FavCimsAgePhase.height = FavCimsAgePhasePanel.height;
                    FavCimsAgePhase.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    FavCimsAgePhase.playAudioEvents = true;
                    FavCimsAgePhase.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsAgePhase.font.size = 15;
                    FavCimsAgePhase.textScale = 1f;
                    FavCimsAgePhase.wordWrap = true;
                    FavCimsAgePhase.textPadding.left = 5;
                    FavCimsAgePhase.textPadding.right = 5;
                    FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    FavCimsAgePhase.outlineSize = 1;
                    FavCimsAgePhase.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsAgePhase.useDropShadow = true;
                    FavCimsAgePhase.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsAgePhase.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsAgePhase.maximumSize = new Vector2(FavCimsAgePhasePanel.width, FavCimsAgePhasePanel.height);
                    FavCimsAgePhase.isInteractive = false;
                    FavCimsAgePhase.relativePosition = new Vector3(0f, 0f);
                    FavCimsSeparatorSprite3 = FavCimsAgePhasePanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite3.name = "FavCimsSeparatorSprite3";
                    FavCimsSeparatorSprite3.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite3.relativePosition = new Vector3(0f, 0f);
                    FavCimsRealAgePanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsRealAgePanel.name = "FavCimsRealAgePanel";
                    FavCimsRealAgePanel.width = FavoriteCimsMainPanel.FavCimsAgeColText.width;
                    FavCimsRealAgePanel.height = 40f;
                    FavCimsRealAgePanel.relativePosition = new Vector3(FavCimsAgePhasePanel.relativePosition.x + FavCimsAgePhasePanel.width, 0f);
                    FavCimsRealAge = FavCimsRealAgePanel.AddUIComponent<UIButton>();
                    FavCimsRealAge.name = "FavCimsRealAge";
                    FavCimsRealAge.width = FavCimsRealAgePanel.width;
                    FavCimsRealAge.height = FavCimsRealAgePanel.height;
                    FavCimsRealAge.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsRealAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    FavCimsRealAge.playAudioEvents = true;
                    FavCimsRealAge.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsRealAge.font.size = 15;
                    FavCimsRealAge.textScale = 1f;
                    FavCimsRealAge.wordWrap = true;
                    FavCimsRealAge.textPadding.left = 5;
                    FavCimsRealAge.textPadding.right = 5;
                    FavCimsRealAge.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    FavCimsRealAge.outlineSize = 1;
                    FavCimsRealAge.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsRealAge.useDropShadow = true;
                    FavCimsRealAge.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsRealAge.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsRealAge.maximumSize = new Vector2(FavCimsRealAgePanel.width, FavCimsRealAgePanel.height);
                    FavCimsRealAge.isInteractive = false;
                    FavCimsRealAge.relativePosition = new Vector3(0f, 0f);
                    FavCimsSeparatorSprite4 = FavCimsRealAgePanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite4.name = "FavCimsSeparatorSprite4";
                    FavCimsSeparatorSprite4.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite4.relativePosition = new Vector3(0f, 0f);
                    FavCimsEducationPanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsEducationPanel.name = "FavCimsEducationPanel";
                    FavCimsEducationPanel.width = FavoriteCimsMainPanel.FavCimsEduColText.width;
                    FavCimsEducationPanel.height = 40f;
                    FavCimsEducationPanel.relativePosition = new Vector3(FavCimsRealAgePanel.relativePosition.x + FavCimsRealAgePanel.width, 0f);
                    FavCimsEducation = FavCimsEducationPanel.AddUIComponent<UIButton>();
                    FavCimsEducation.name = "FavCimsEducation";
                    FavCimsEducation.width = FavCimsEducationPanel.width;
                    FavCimsEducation.height = FavCimsEducationPanel.height;
                    FavCimsEducation.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    FavCimsEducation.playAudioEvents = true;
                    FavCimsEducation.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsEducation.font.size = 15;
                    FavCimsEducation.textScale = 1f;
                    FavCimsEducation.wordWrap = true;
                    FavCimsEducation.textPadding.left = 5;
                    FavCimsEducation.textPadding.right = 5;
                    FavCimsEducation.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    FavCimsEducation.outlineSize = 1;
                    FavCimsEducation.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsEducation.useDropShadow = true;
                    FavCimsEducation.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsEducation.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsEducation.maximumSize = new Vector2(FavCimsEducationPanel.width, FavCimsEducationPanel.height);
                    FavCimsEducation.isInteractive = false;
                    FavCimsEducation.relativePosition = new Vector3(0f, 0f);
                    FavCimsSeparatorSprite5 = FavCimsEducationPanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite5.name = "FavCimsSeparatorSprite5";
                    FavCimsSeparatorSprite5.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite5.relativePosition = new Vector3(0f, 0f);
                    FavCimsCitizenHomePanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsCitizenHomePanel.name = "FavCimsCitizenHomePanel";
                    FavCimsCitizenHomePanel.width = FavoriteCimsMainPanel.FavCimsHomeColText.width;
                    FavCimsCitizenHomePanel.height = 40f;
                    FavCimsCitizenHomePanel.relativePosition = new Vector3(FavCimsEducationPanel.relativePosition.x + FavCimsEducationPanel.width, 0f);
                    FavCimsCitizenHome = FavCimsCitizenHomePanel.AddUIComponent<UIButton>();
                    FavCimsCitizenHome.name = "FavCimsCitizenHome";
                    FavCimsCitizenHome.width = FavCimsCitizenHomePanel.width;
                    FavCimsCitizenHome.height = FavCimsCitizenHomePanel.height;
                    FavCimsCitizenHome.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsCitizenHome.textHorizontalAlignment = 0;
                    FavCimsCitizenHome.playAudioEvents = true;
                    FavCimsCitizenHome.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsCitizenHome.font.size = 15;
                    FavCimsCitizenHome.textScale = 0.85f;
                    FavCimsCitizenHome.wordWrap = true;
                    FavCimsCitizenHome.textPadding.left = 40;
                    FavCimsCitizenHome.textPadding.right = 5;
                    FavCimsCitizenHome.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsCitizenHome.outlineSize = 1;
                    FavCimsCitizenHome.textColor = new Color32(21, 59, 96, 140);
                    FavCimsCitizenHome.hoveredTextColor = new Color32(204, 102, 0, 20);
                    FavCimsCitizenHome.pressedTextColor = new Color32(153, 0, 0, 0);
                    FavCimsCitizenHome.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    FavCimsCitizenHome.disabledTextColor = new Color32(51, 51, 51, 160);
                    FavCimsCitizenHome.useDropShadow = true;
                    FavCimsCitizenHome.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsCitizenHome.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsCitizenHome.maximumSize = new Vector2(FavCimsCitizenHomePanel.width, FavCimsCitizenHomePanel.height);
                    FavCimsCitizenHome.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsCitizenHome.eventMouseUp += new MouseEventHandler(GoToHome);
                    FavCimsCitizenHome.relativePosition = new Vector3(0f, 0f);
                    FavCimsCitizenHomeButton = FavCimsCitizenHomePanel.AddUIComponent<UIButton>();
                    FavCimsCitizenHomeButton.name = "FavCimsCitizenHomeButton";
                    FavCimsCitizenHomeButton.atlas = MyAtlas.FavCimsAtlas;
                    FavCimsCitizenHomeButton.size = new Vector2(20f, 40f);
                    FavCimsCitizenHomeButton.relativePosition = new Vector3(10f, 0f);
                    FavCimsCitizenHomeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsCitizenResidentialLevelSprite = FavCimsCitizenHomeButton.AddUIComponent<UITextureSprite>();
                    FavCimsCitizenResidentialLevelSprite.name = "FavCimsCitizenResidentialLevelSprite";
                    FavCimsCitizenResidentialLevelSprite.relativePosition = new Vector3(0f, 0f);
                    FavCimsSeparatorSprite6 = FavCimsCitizenHomePanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite6.name = "FavCimsSeparatorSprite6";
                    FavCimsSeparatorSprite6.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite6.relativePosition = new Vector3(0f, 0f);
                    FavCimsWorkingPlacePanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsWorkingPlacePanel.name = "FavCimsWorkingPlacePanel";
                    FavCimsWorkingPlacePanel.width = FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width;
                    FavCimsWorkingPlacePanel.height = 40f;
                    FavCimsWorkingPlacePanel.relativePosition = new Vector3(FavCimsCitizenHomePanel.relativePosition.x + FavCimsCitizenHomePanel.width, 0f);
                    FavCimsWorkingPlace = FavCimsWorkingPlacePanel.AddUIComponent<UIButton>();
                    FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
                    FavCimsWorkingPlace.width = FavCimsWorkingPlacePanel.width;
                    FavCimsWorkingPlace.height = FavCimsWorkingPlacePanel.height;
                    FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsWorkingPlace.textHorizontalAlignment = 0;
                    FavCimsWorkingPlace.playAudioEvents = true;
                    FavCimsWorkingPlace.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsWorkingPlace.font.size = 15;
                    FavCimsWorkingPlace.textScale = 0.85f;
                    FavCimsWorkingPlace.wordWrap = true;
                    FavCimsWorkingPlace.textPadding.left = 40;
                    FavCimsWorkingPlace.textPadding.right = 5;
                    FavCimsWorkingPlace.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsWorkingPlace.outlineSize = 1;
                    FavCimsWorkingPlace.textColor = new Color32(21, 59, 96, 140);
                    FavCimsWorkingPlace.hoveredTextColor = new Color32(204, 102, 0, 20);
                    FavCimsWorkingPlace.pressedTextColor = new Color32(153, 0, 0, 0);
                    FavCimsWorkingPlace.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    FavCimsWorkingPlace.disabledTextColor = new Color32(51, 51, 51, 160);
                    FavCimsWorkingPlace.useDropShadow = true;
                    FavCimsWorkingPlace.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsWorkingPlace.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsWorkingPlace.maximumSize = new Vector2(FavCimsWorkingPlacePanel.width, FavCimsWorkingPlacePanel.height);
                    FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsWorkingPlace.eventMouseUp += new MouseEventHandler(GoToWork);
                    FavCimsWorkingPlace.relativePosition = new Vector3(0f, 0f);
                    FavCimsWorkingPlaceSprite = FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite>();
                    FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
                    FavCimsWorkingPlaceSprite.width = 20f;
                    FavCimsWorkingPlaceSprite.height = 40f;
                    FavCimsWorkingPlaceSprite.relativePosition = new Vector3(10f, 0f);
                    FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsWorkingPlaceButton = FavCimsWorkingPlaceSprite.AddUIComponent<UIButton>();
                    FavCimsWorkingPlaceButton.name = "FavCimsWorkingPlaceButton";
                    FavCimsWorkingPlaceButton.width = 20f;
                    FavCimsWorkingPlaceButton.height = 20f;
                    FavCimsWorkingPlaceButton.relativePosition = new Vector3(0f, 10f);
                    FavCimsWorkingPlaceButton.isInteractive = false;
                    FavCimsWorkingPlaceButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsCitizenWorkPlaceLevelSprite = FavCimsWorkingPlaceSprite.AddUIComponent<UITextureSprite>();
                    FavCimsCitizenWorkPlaceLevelSprite.name = "FavCimsCitizenWorkPlaceLevelSprite";
                    FavCimsCitizenWorkPlaceLevelSprite.relativePosition = new Vector3(0f, 0f);
                    FavCimsSeparatorSprite7 = FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite7.name = "FavCimsSeparatorSprite7";
                    FavCimsSeparatorSprite7.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite7.relativePosition = new Vector3(0f, 0f);
                    FavCimsLastActivityPanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsLastActivityPanel.name = "FavCimsLastActivityPanel";
                    FavCimsLastActivityPanel.width = FavoriteCimsMainPanel.FavCimsLastActColText.width;
                    FavCimsLastActivityPanel.height = 40f;
                    FavCimsLastActivityPanel.relativePosition = new Vector3(FavCimsWorkingPlacePanel.relativePosition.x + FavCimsWorkingPlacePanel.width, 0f);
                    FavCimsLastActivity = FavCimsLastActivityPanel.AddUIComponent<UIButton>();
                    FavCimsLastActivity.name = "FavCimsLastActivity";
                    FavCimsLastActivity.width = FavCimsLastActivityPanel.width - 40f;
                    FavCimsLastActivity.height = FavCimsLastActivityPanel.height;
                    FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
                    FavCimsLastActivity.textHorizontalAlignment = 0;
                    FavCimsLastActivity.playAudioEvents = true;
                    FavCimsLastActivity.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    FavCimsLastActivity.font.size = 14;
                    FavCimsLastActivity.textScale = 0.85f;
                    FavCimsLastActivity.wordWrap = true;
                    FavCimsLastActivity.textPadding.left = 0;
                    FavCimsLastActivity.textPadding.right = 5;
                    FavCimsLastActivity.outlineColor = new Color32(0, 0, 0, 0);
                    FavCimsLastActivity.outlineSize = 1;
                    FavCimsLastActivity.textColor = new Color32(21, 59, 96, 140);
                    FavCimsLastActivity.hoveredTextColor = new Color32(204, 102, 0, 20);
                    FavCimsLastActivity.pressedTextColor = new Color32(153, 0, 0, 0);
                    FavCimsLastActivity.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    FavCimsLastActivity.disabledTextColor = new Color32(51, 51, 51, 160);
                    FavCimsLastActivity.useDropShadow = true;
                    FavCimsLastActivity.dropShadowOffset = new Vector2(1f, -1f);
                    FavCimsLastActivity.dropShadowColor = new Color32(0, 0, 0, 0);
                    FavCimsLastActivity.maximumSize = new Vector2(FavCimsLastActivityPanel.width - 40f, FavCimsLastActivityPanel.height);
                    FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsLastActivity.eventMouseUp += new MouseEventHandler(GoToTarget);
                    FavCimsLastActivity.relativePosition = new Vector3(40f, 0f);
                    FavCimsLastActivityVehicleButton = FavCimsLastActivityPanel.AddUIComponent<UIButton>();
                    FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
                    FavCimsLastActivityVehicleButton.width = 26f;
                    FavCimsLastActivityVehicleButton.height = 26f;
                    FavCimsLastActivityVehicleButton.relativePosition = new Vector3(5f, 7f);
                    FavCimsLastActivityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsLastActivityVehicleButton.eventMouseUp += new MouseEventHandler(GoToVehicle);
                    FavCimsSeparatorSprite8 = FavCimsLastActivityPanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite8.name = "FavCimsSeparatorSprite8";
                    FavCimsSeparatorSprite8.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite8.relativePosition = new Vector3(0f, 0f);
                    FavCimsCloseRowPanel = FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    FavCimsCloseRowPanel.name = "FavCimsCloseRowPanel";
                    FavCimsCloseRowPanel.width = FavoriteCimsMainPanel.FavCimsCloseButtonCol.width;
                    FavCimsCloseRowPanel.height = 40f;
                    FavCimsCloseRowPanel.relativePosition = new Vector3(FavCimsLastActivityPanel.relativePosition.x + FavCimsLastActivityPanel.width, 0f);
                    FavCimsRowCloseButton = FavCimsCloseRowPanel.AddUIComponent<UIButton>();
                    FavCimsRowCloseButton.name = "FavCimsRowCloseButton";
                    FavCimsRowCloseButton.width = 26f;
                    FavCimsRowCloseButton.height = 26f;
                    FavCimsRowCloseButton.normalBgSprite = "buttonclose";
                    FavCimsRowCloseButton.hoveredBgSprite = "buttonclosehover";
                    FavCimsRowCloseButton.pressedBgSprite = "buttonclosepressed";
                    FavCimsRowCloseButton.opacity = 0.9f;
                    FavCimsRowCloseButton.playAudioEvents = true;
                    FavCimsRowCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    FavCimsRowCloseButton.eventClick += delegate
                    {
                        try
                        {
                            FavCimsCore.RemoveRowAndRemoveFav(MyInstanceID, citizenINT);
                            bool flag8 = MyFamily != null;
                            if (flag8)
                            {
                                MyFamily.Hide();
                                MyFamily.MyInstanceID = InstanceID.Empty;
                                MyFamily = null;
                            }
                            bool flag9 = UIView.Find<UILabel>("DefaultTooltip");
                            if (flag9)
                            {
                                UIView.Find<UILabel>("DefaultTooltip").Hide();
                            }
                            Destroy(gameObject);
                        }
                        catch (Exception ex3)
                        {
                            Utils.Debug.Error("Can't remove row " + ex3.ToString());
                        }
                    };
                    FavCimsRowCloseButton.relativePosition = new Vector3(FavCimsCloseRowPanel.width / 2f - FavCimsRowCloseButton.width / 2f, 7f);
                    FavCimsSeparatorSprite9 = FavCimsCloseRowPanel.AddUIComponent<UITextureSprite>();
                    FavCimsSeparatorSprite9.name = "FavCimsSeparatorSprite9";
                    FavCimsSeparatorSprite9.texture = TextureDB.FavCimsSeparator;
                    FavCimsSeparatorSprite9.relativePosition = new Vector3(0f, 0f);
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("CitizenRow Create Error : " + ex.ToString());
            }
        }

        public override void Update()
        {
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool firstRun = FirstRun;
                if (firstRun)
                {
                    secondsForceRun -= 1f * Time.deltaTime;
                    bool flag = secondsForceRun > 0f;
                    if (flag)
                    {
                        execute = true;
                    }
                    else
                    {
                        FirstRun = false;
                    }
                }
                else
                {
                    bool flag2 = !FavCimsMainClass.FavCimsPanel.isVisible || IsClippedFromParent();
                    if (flag2)
                    {
                        FavCimsCitizenSingleRowPanel.Hide();
                        HiddenRowsSeconds -= 1f * Time.deltaTime;
                        bool flag3 = HiddenRowsSeconds <= 0f;
                        if (flag3)
                        {
                            execute = true;
                            HiddenRowsSeconds = 30f;
                        }
                        else
                        {
                            execute = false;
                        }
                    }
                    else
                    {
                        FavCimsCitizenSingleRowPanel.Show();
                        seconds -= 1f * Time.deltaTime;
                        bool flag4 = seconds <= 0f;
                        if (flag4)
                        {
                            execute = true;
                            seconds = 0.5f;
                        }
                        else
                        {
                            execute = false;
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
                bool flag = MyInstanceID.IsEmpty || !FavCimsCore.RowID.ContainsKey(citizenINT);
                if (flag)
                {
                    bool flag2 = MyFamily != null;
                    if (flag2)
                    {
                        MyFamily.Hide();
                        MyFamily.MyInstanceID = InstanceID.Empty;
                        MyFamily = null;
                    }
                    Destroy(gameObject);
                }
                else
                {
                    bool flag3 = DeadOrGone || HomeLess;
                    if (flag3)
                    {
                        OtherInfoButton.isEnabled = false;
                        OtherInfoButton.tooltip = FavCimsLang.Text("Citizen_Details_NoUnit");
                    }
                    else
                    {
                        bool flag4 = GetTemplate() == -1 && (MyFamily == null || MyFamily.MyInstanceID != MyInstanceID);
                        if (flag4)
                        {
                            bool flag5 = MyFamily != null && MyFamily.MyInstanceID != MyInstanceID;
                            if (flag5)
                            {
                                MyFamily = null;
                            }
                            OtherInfoButton.isEnabled = false;
                            OtherInfoButton.tooltip = FavCimsLang.Text("Citizen_Details_fullTemplate");
                        }
                        else
                        {
                            bool flag6 = MyFamily != null && MyFamily.MyInstanceID == MyInstanceID && MyFamily.isVisible;
                            if (flag6)
                            {
                                OtherInfoButton.normalBgSprite = "CityInfoFocused";
                            }
                            else
                            {
                                OtherInfoButton.normalBgSprite = "CityInfo";
                            }
                            OtherInfoButton.isEnabled = true;
                            OtherInfoButton.tooltip = FavCimsLang.Text("Citizen_Details");
                        }
                    }
                    uint citizen = MyInstanceID.Citizen;
                    bool flag7 = citizen != 0U && MyCitizen.m_citizens.m_buffer[(int)citizen].Dead && !CitizenIsDead;
                    if (flag7)
                    {
                        CitizenIsDead = true;
                        CitizenRowData["deathrealage"] = "0";
                    }
                    bool flag8 = execute;
                    if (flag8)
                    {
                        try
                        {
                            CitizenName = MyInstance.GetName(MyInstanceID);
                            citizenINT = (int)(uint)(UIntPtr)citizen;
                            bool flag9 = CitizenName != null && CitizenName.Length > 0 && CitizenName != MyInstancedName;
                            if (flag9)
                            {
                                MyInstancedName = CitizenName;
                            }
                            citizenInfo = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].GetCitizenInfo(citizen);
                            FavCimsRowCloseButton.tooltip = FavCimsLang.Text("FavStarButton_disable_tooltip");
                            bool flag10 = FavCimsCitizenSingleRowPanel != null && citizen != 0U && CitizenName == MyInstancedName && FavCimsCore.RowID.ContainsKey(citizenINT);
                            if (flag10)
                            {
                                Citizen.Gender gender = Citizen.GetGender(citizen);
                                CitizenRowData["gender"] = gender.ToString();
                                CitizenRowData["name"] = MyCitizen.GetCitizenName(citizen);
                                FavCimsCitizenName.text = CitizenRowData["name"];
                                bool flag11 = CitizenRowData["gender"] == "Female";
                                if (flag11)
                                {
                                    FavCimsCitizenName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                }
                                bool flag12 = CitizenDistrict == 0;
                                if (flag12)
                                {
                                    FavCimsCitizenName.tooltip = FavCimsLang.Text("NowInThisDistrict") + FavCimsLang.Text("DistrictNameNoDistrict");
                                }
                                else
                                {
                                    FavCimsCitizenName.tooltip = FavCimsLang.Text("NowInThisDistrict") + MyDistrict.GetDistrictName(CitizenDistrict);
                                }
                                tmp_health = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_health;
                                CitizenRowData["health"] = Citizen.GetHealthLevel(tmp_health).ToString();
                                Citizen.Education educationLevel = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].EducationLevel;
                                CitizenRowData["education"] = educationLevel.ToString();
                                FavCimsEducation.text = FavCimsLang.Text("Education_" + CitizenRowData["education"] + "_" + CitizenRowData["gender"]);
                                bool flag13 = CitizenRowData["education"] == "ThreeSchools";
                                if (flag13)
                                {
                                    FavCimsEducation.textColor = new Color32(102, 204, 0, 60);
                                }
                                else
                                {
                                    bool flag14 = CitizenRowData["education"] == "TwoSchools";
                                    if (flag14)
                                    {
                                        FavCimsEducation.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                    }
                                    else
                                    {
                                        bool flag15 = CitizenRowData["education"] == "OneSchool";
                                        if (flag15)
                                        {
                                            FavCimsEducation.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        }
                                        else
                                        {
                                            FavCimsEducation.textColor = new Color32(153, 0, 0, 0);
                                        }
                                    }
                                }
                                tmp_wellbeing = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_wellbeing;
                                CitizenRowData["wellbeing"] = Citizen.GetWellbeingLevel(educationLevel, tmp_wellbeing).ToString();
                                tmp_happiness = Citizen.GetHappiness(tmp_health, tmp_wellbeing);
                                CitizenRowData["happiness_icon"] = GetHappinessString(Citizen.GetHappinessLevel(tmp_happiness));
                                FavCimsHappyIcon.normalBgSprite = CitizenRowData["happiness_icon"];
                                FavCimsHappyIcon.tooltip = FavCimsLang.Text(CitizenRowData["happiness_icon"]);
                                tmp_age = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_age;
                                CitizenRowData["agegroup"] = Citizen.GetAgeGroup(tmp_age).ToString();
                                FavCimsAgePhase.text = FavCimsLang.Text("AgePhase_" + CitizenRowData["agegroup"] + "_" + CitizenRowData["gender"]);
                                RealAge = FavCimsCore.CalculateCitizenAge(tmp_age);
                                bool flag16 = RealAge <= 12;
                                if (flag16)
                                {
                                    FavCimsRealAge.text = RealAge.ToString();
                                    FavCimsRealAge.textColor = new Color32(102, 204, 0, 60);
                                    FavCimsAgePhase.textColor = new Color32(102, 204, 0, 60);
                                }
                                else
                                {
                                    bool flag17 = RealAge <= 19;
                                    if (flag17)
                                    {
                                        FavCimsRealAge.text = RealAge.ToString();
                                        FavCimsRealAge.textColor = new Color32(0, 102, 51, 100);
                                        FavCimsAgePhase.textColor = new Color32(0, 102, 51, 100);
                                    }
                                    else
                                    {
                                        bool flag18 = RealAge <= 25;
                                        if (flag18)
                                        {
                                            FavCimsRealAge.text = RealAge.ToString();
                                            FavCimsRealAge.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        }
                                        else
                                        {
                                            bool flag19 = RealAge <= 65;
                                            if (flag19)
                                            {
                                                FavCimsRealAge.text = RealAge.ToString();
                                                FavCimsRealAge.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            }
                                            else
                                            {
                                                bool flag20 = RealAge <= 90;
                                                if (flag20)
                                                {
                                                    FavCimsRealAge.text = RealAge.ToString();
                                                    FavCimsRealAge.textColor = new Color32(153, 0, 0, 0);
                                                    FavCimsAgePhase.textColor = new Color32(153, 0, 0, 0);
                                                }
                                                else
                                                {
                                                    FavCimsRealAge.text = RealAge.ToString();
                                                    FavCimsRealAge.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    FavCimsAgePhase.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                }
                                            }
                                        }
                                    }
                                }
                                CitizenHome = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_homeBuilding;
                                bool flag21 = CitizenHome > 0;
                                if (flag21)
                                {
                                    HomeLess = false;
                                    CitizenHomeID.Building = CitizenHome;
                                    FavCimsCitizenHome.text = MyBuilding.GetBuildingName(CitizenHome, MyInstanceID);
                                    FavCimsCitizenHome.isEnabled = true;
                                    FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
                                    HomeInfo = MyBuilding.m_buildings.m_buffer[(int)CitizenHomeID.Index].Info;
                                    bool flag22 = HomeInfo.m_class.m_service == ItemClass.Service.Residential;
                                    if (flag22)
                                    {
                                        FavCimsCitizenHome.tooltip = null;
                                        bool flag23 = HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh;
                                        if (flag23)
                                        {
                                            FavCimsCitizenHome.textColor = new Color32(0, 102, 51, 100);
                                            FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
                                            FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString());
                                        }
                                        else
                                        {
                                            bool flag24 = HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHighEco;
                                            if (flag24)
                                            {
                                                FavCimsCitizenHome.textColor = new Color32(0, 102, 51, 100);
                                                FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
                                                FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString()) + " Eco";
                                            }
                                            else
                                            {
                                                bool flag25 = HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLowEco;
                                                if (flag25)
                                                {
                                                    FavCimsCitizenHome.textColor = new Color32(0, 153, 0, 80);
                                                    FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
                                                    FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString()) + " Eco";
                                                }
                                                else
                                                {
                                                    bool flag26 = HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow;
                                                    if (flag26)
                                                    {
                                                        FavCimsCitizenHome.textColor = new Color32(0, 153, 0, 80);
                                                        FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
                                                        FavCimsCitizenHome.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        switch (HomeInfo.m_class.m_level)
                                        {
                                            case ItemClass.Level.Level2:
                                                FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[2];
                                                break;
                                            case ItemClass.Level.Level3:
                                                FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[3];
                                                break;
                                            case ItemClass.Level.Level4:
                                                FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[4];
                                                break;
                                            case ItemClass.Level.Level5:
                                                FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[5];
                                                break;
                                            default:
                                                FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel[1];
                                                break;
                                        }
                                        HomeDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)CitizenHomeID.Index].m_position);
                                        bool flag27 = HomeDistrict == 0;
                                        if (flag27)
                                        {
                                            FavCimsCitizenHomeButton.tooltip = FavCimsLang.Text("DistrictLabel") + FavCimsLang.Text("DistrictNameNoDistrict");
                                        }
                                        else
                                        {
                                            FavCimsCitizenHomeButton.tooltip = FavCimsLang.Text("DistrictLabel") + MyDistrict.GetDistrictName(HomeDistrict);
                                        }
                                    }
                                }
                                else
                                {
                                    FavCimsCitizenHome.text = FavCimsLang.Text("Citizen_HomeLess");
                                    FavCimsCitizenHome.isEnabled = false;
                                    FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
                                    FavCimsCitizenHomeButton.tooltip = FavCimsLang.Text("DistrictNameNoDistrict");
                                    FavCimsCitizenHome.tooltip = FavCimsLang.Text("Citizen_HomeLess_tooltip");
                                    FavCimsCitizenResidentialLevelSprite.texture = null;
                                    HomeLess = true;
                                }
                                WorkPlace = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_workBuilding;
                                bool flag28 = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].GetCurrentSchoolLevel(citizen) != ItemClass.Level.None;
                                if (flag28)
                                {
                                    isStudent = true;
                                    FavCimsWorkingPlaceButton.normalBgSprite = null;
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
                                    FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    FavCimsWorkingPlace.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].GetCurrentSchoolLevel(citizen).ToString()) + " " + MyBuilding.GetBuildingName(WorkPlace, MyInstanceID);
                                }
                                else
                                {
                                    bool flag29 = WorkPlace == 0;
                                    if (flag29)
                                    {
                                        FavCimsWorkingPlaceButton.normalBgSprite = null;
                                        bool flag30 = (MyCitizen.m_citizens.m_buffer[(int)citizen].m_flags & Citizen.Flags.Tourist) > 0;
                                        if (flag30)
                                        {
                                            string text = string.Empty;
                                            bool flag31 = SteamHelper.IsDLCOwned(SteamHelper.DLC.CampusDLC);
                                            if (flag31)
                                            {
                                                float num = Singleton<ImmaterialResourceManager>.instance.CheckExchangeStudentAttractivenessBonus() * 100f;
                                                Randomizer m_randomizer = new(citizen);
                                                text = m_randomizer.Int32(0, 100) >= (double)num ? Locale.Get("CITIZEN_OCCUPATION_TOURIST") : Locale.Get("CITIZEN_OCCUPATION_EXCHANGESTUDENT");
                                            }
                                            else
                                            {
                                                text = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
                                            }
                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
                                            FavCimsWorkingPlace.text = text;
                                            FavCimsWorkingPlace.isEnabled = false;
                                            FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Citizen_Tourist_tooltip");
                                            FavCimsWorkingPlaceSprite.tooltip = null;
                                            FavCimsWorkingPlaceButton.tooltip = null;
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                        else
                                        {
                                            bool flag32 = tmp_age >= 180;
                                            if (flag32)
                                            {
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
                                                FavCimsWorkingPlace.text = FavCimsLang.Text("Citizen_Retired");
                                                FavCimsWorkingPlace.isEnabled = false;
                                                FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Citizen_Retired_tooltip");
                                                FavCimsWorkingPlaceSprite.tooltip = null;
                                                FavCimsWorkingPlaceButton.tooltip = null;
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                            else
                                            {
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
                                                FavCimsWorkingPlace.text = Locale.Get("CITIZEN_OCCUPATION_UNEMPLOYED");
                                                FavCimsWorkingPlace.isEnabled = false;
                                                FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Unemployed_tooltip");
                                                FavCimsWorkingPlaceSprite.tooltip = null;
                                                FavCimsWorkingPlaceButton.tooltip = null;
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                    }
                                }
                                bool flag33 = WorkPlace > 0;
                                if (flag33)
                                {
                                    string text2 = string.Empty;
                                    bool flag34 = !isStudent;
                                    if (flag34)
                                    {
                                        CommonBuildingAI commonBuildingAI = MyBuilding.m_buildings.m_buffer[WorkPlace].Info.m_buildingAI as CommonBuildingAI;
                                        bool flag35 = commonBuildingAI != null;
                                        if (flag35)
                                        {
                                            text2 = commonBuildingAI.GetTitle(gender, educationLevel, WorkPlace, citizen);
                                        }
                                        bool flag36 = text2 == string.Empty;
                                        if (flag36)
                                        {
                                            int num2 = new Randomizer(WorkPlace + citizen).Int32(1, 5);
                                            switch (educationLevel)
                                            {
                                                case Citizen.Education.Uneducated:
                                                    text2 = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.OneSchool:
                                                    text2 = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.TwoSchools:
                                                    text2 = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.ThreeSchools:
                                                    text2 = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", num2.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                            }
                                        }
                                    }
                                    WorkPlaceID.Building = WorkPlace;
                                    FavCimsWorkingPlace.text = text2 + " " + MyBuilding.GetBuildingName(WorkPlace, MyInstanceID);
                                    FavCimsWorkingPlace.isEnabled = true;
                                    WorkInfo = MyBuilding.m_buildings.m_buffer[(int)WorkPlaceID.Index].Info;
                                    FavCimsWorkingPlaceSprite.texture = null;
                                    bool flag37 = WorkInfo.m_class.m_service == ItemClass.Service.Commercial;
                                    if (flag37)
                                    {
                                        FavCimsWorkingPlaceButton.normalBgSprite = null;
                                        bool flag38 = WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh;
                                        if (flag38)
                                        {
                                            FavCimsWorkingPlace.textColor = new Color32(0, 51, 153, 147);
                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                            FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
                                        }
                                        else
                                        {
                                            bool flag39 = WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialEco;
                                            if (flag39)
                                            {
                                                FavCimsWorkingPlace.textColor = new Color32(0, 150, 136, 116);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Buildings_Type_CommercialEco");
                                            }
                                            else
                                            {
                                                bool flag40 = WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLeisure;
                                                if (flag40)
                                                {
                                                    FavCimsWorkingPlace.textColor = new Color32(219, 68, 55, 3);
                                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                    FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Buildings_Type_CommercialLeisure");
                                                }
                                                else
                                                {
                                                    bool flag41 = WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialTourist;
                                                    if (flag41)
                                                    {
                                                        FavCimsWorkingPlace.textColor = new Color32(156, 39, 176, 194);
                                                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Buildings_Type_CommercialTourist");
                                                    }
                                                    else
                                                    {
                                                        FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
                                                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        bool flag42 = WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow;
                                        if (flag42)
                                        {
                                            ItemClass.Level level = WorkInfo.m_class.m_level;
                                            ItemClass.Level level2 = level;
                                            if (level2 != ItemClass.Level.Level2)
                                            {
                                                if (level2 != ItemClass.Level.Level3)
                                                {
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[1];
                                                }
                                                else
                                                {
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[3];
                                                }
                                            }
                                            else
                                            {
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[2];
                                            }
                                        }
                                        else
                                        {
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                    else
                                    {
                                        bool flag43 = WorkInfo.m_class.m_service == ItemClass.Service.Industrial;
                                        if (flag43)
                                        {
                                            FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
                                            ItemClass.SubService subService = WorkInfo.m_class.m_subService;
                                            ItemClass.SubService subService2 = subService;
                                            switch (subService2)
                                            {
                                                case ItemClass.SubService.IndustrialForestry:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
                                                    break;
                                                case ItemClass.SubService.IndustrialFarming:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
                                                    break;
                                                case ItemClass.SubService.IndustrialOil:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
                                                    break;
                                                case ItemClass.SubService.IndustrialOre:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
                                                    break;
                                                default:
                                                    switch (subService2)
                                                    {
                                                        case ItemClass.SubService.PlayerIndustryForestry:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryFarming:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOil:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOre:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
                                                            break;
                                                        default:
                                                            FavCimsWorkingPlaceButton.normalBgSprite = null;
                                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                                            break;
                                                    }
                                                    break;
                                            }
                                            bool flag44 = WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric;
                                            if (flag44)
                                            {
                                                ItemClass.Level level3 = WorkInfo.m_class.m_level;
                                                ItemClass.Level level4 = level3;
                                                if (level4 != ItemClass.Level.Level2)
                                                {
                                                    if (level4 != ItemClass.Level.Level3)
                                                    {
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[1];
                                                    }
                                                    else
                                                    {
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[3];
                                                    }
                                                }
                                                else
                                                {
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[2];
                                                }
                                            }
                                            else
                                            {
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                        else
                                        {
                                            bool flag45 = WorkInfo.m_class.m_service == ItemClass.Service.Office;
                                            if (flag45)
                                            {
                                                FavCimsWorkingPlaceButton.normalBgSprite = null;
                                                FavCimsWorkingPlace.textColor = new Color32(0, 204, byte.MaxValue, 128);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;
                                                ItemClass.SubService subService3 = WorkInfo.m_class.m_subService;
                                                ItemClass.SubService subService4 = subService3;
                                                if (subService4 != ItemClass.SubService.OfficeHightech)
                                                {
                                                    FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office");
                                                }
                                                else
                                                {
                                                    FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office") + " Eco";
                                                }
                                                ItemClass.Level level5 = WorkInfo.m_class.m_level;
                                                ItemClass.Level level6 = level5;
                                                if (level6 != ItemClass.Level.Level2)
                                                {
                                                    if (level6 != ItemClass.Level.Level3)
                                                    {
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[1];
                                                    }
                                                    else
                                                    {
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[3];
                                                    }
                                                }
                                                else
                                                {
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[2];
                                                }
                                            }
                                            else
                                            {
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                                FavCimsWorkingPlace.textColor = new Color32(153, 102, 51, 20);
                                                switch (WorkInfo.m_class.m_service)
                                                {
                                                    case ItemClass.Service.Electricity:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyPowerSaving";
                                                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Electricity_job");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Water:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyWaterSaving";
                                                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("Water_job");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Beautification:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "SubBarBeautificationParksnPlazas";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Beautification");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Garbage:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyRecycling";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Garbage");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.HealthCare:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconHealthcareFocused";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Healthcare");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.PoliceDepartment:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconPolice";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Police");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Education:
                                                        FavCimsWorkingPlace.textColor = new Color32(0, 102, 51, 100);
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconEducationPressed";
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Monument:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "FeatureMonumentLevel6";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Monuments");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.FireDepartment:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconFireSafety";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "FireDepartment");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.PublicTransport:
                                                        {
                                                            ItemClass.SubService subService5 = WorkInfo.m_class.m_subService;
                                                            ItemClass.SubService subService6 = subService5;
                                                            if (subService6 != ItemClass.SubService.PublicTransportPost)
                                                            {
                                                                FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFreePublicTransport";
                                                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                                            }
                                                            else
                                                            {
                                                                FavCimsWorkingPlaceButton.normalBgSprite = "SubBarPublicTransportPost";
                                                                FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                                            }
                                                            goto IL_1D8D;
                                                        }
                                                    case ItemClass.Service.Disaster:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "SubBarFireDepartmentDisaster";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Museums:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaMuseums";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.VarsitySports:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Fishing:
                                                        FavCimsWorkingPlaceButton.normalBgSprite = "SubBarIndustryFishing";
                                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                                        goto IL_1D8D;
                                                }
                                                FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyNone";
                                                FavCimsWorkingPlace.tooltip = null;
                                            IL_1D8D:
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                    }
                                    WorkDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)WorkPlaceID.Index].m_position);
                                    bool flag46 = WorkDistrict == 0;
                                    if (flag46)
                                    {
                                        FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.Text("DistrictLabel") + FavCimsLang.Text("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.Text("DistrictLabel") + MyDistrict.GetDistrictName(WorkDistrict);
                                    }
                                }
                                else
                                {
                                    FavCimsWorkingPlace.isEnabled = false;
                                    FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    FavCimsWorkingPlaceButton.tooltip = null;
                                    FavCimsWorkingPlaceSprite.tooltip = null;
                                }
                                InstanceCitizenID = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_instance;
                                citizenInstance = MyCitizen.m_instances.m_buffer[InstanceCitizenID];
                                bool flag47 = citizenInstance.m_targetBuilding > 0;
                                if (flag47)
                                {
                                    CitizenVehicle = MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
                                    MyVehicleID = InstanceID.Empty;
                                    GoingOutside = (MyBuilding.m_buildings.m_buffer[citizenInstance.m_targetBuilding].m_flags & (Building.Flags)192) > 0;
                                    bool flag48 = CitizenVehicle > 0;
                                    if (flag48)
                                    {
                                        MyVehicleID.Vehicle = CitizenVehicle;
                                        FavCimsLastActivityVehicleButton.isEnabled = true;
                                        VehInfo = MyVehicle.m_vehicles.m_buffer[CitizenVehicle].Info;
                                        string text3 = PrefabCollection<VehicleInfo>.PrefabName((uint)VehInfo.m_prefabDataIndex);
                                        bool flag49 = text3 == "Train Passenger";
                                        if (flag49)
                                        {
                                            CitizenVehicleName = Locale.Get("VEHICLE_TITLE", "Train Engine");
                                        }
                                        else
                                        {
                                            CitizenVehicleName = MyVehicle.GetVehicleName(CitizenVehicle);
                                        }
                                        bool flag50 = VehInfo.m_class.m_service == ItemClass.Service.Residential;
                                        if (flag50)
                                        {
                                            bool flag51 = CitizenVehicleName.Like("Bicycle");
                                            if (flag51)
                                            {
                                                FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                                FavCimsLastActivityVehicleButton.normalBgSprite = "IconTouristBicycleVehicle";
                                                FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristBicycleVehicle";
                                                FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("PROPS_DESC", "bicycle01");
                                            }
                                            else
                                            {
                                                bool flag52 = CitizenVehicleName.Like("Scooter");
                                                if (flag52)
                                                {
                                                    FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
                                                    FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsIconScooter";
                                                    FavCimsLastActivityVehicleButton.hoveredBgSprite = "FavCimsIconScooter";
                                                    FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName;
                                                }
                                                else
                                                {
                                                    FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                                    FavCimsLastActivityVehicleButton.normalBgSprite = "IconCitizenVehicle";
                                                    FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristVehicle";
                                                    FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName;
                                                }
                                            }
                                            bool flag53 = VehInfo.m_vehicleAI.GetOwnerID(CitizenVehicle, ref MyVehicle.m_vehicles.m_buffer[CitizenVehicle]).Citizen == citizen;
                                            if (flag53)
                                            {
                                                bool goingOutside = GoingOutside;
                                                if (goingOutside)
                                                {
                                                    LeaveCity = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool flag54 = VehInfo.m_class.m_service == ItemClass.Service.PublicTransport;
                                            if (flag54)
                                            {
                                                FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                                bool goingOutside2 = GoingOutside;
                                                if (goingOutside2)
                                                {
                                                    LeaveCity = true;
                                                }
                                                ItemClass.SubService subService7 = VehInfo.m_class.m_subService;
                                                ItemClass.SubService subService8 = subService7;
                                                switch (subService8)
                                                {
                                                    case ItemClass.SubService.PublicTransportBus:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Bus") + " - " + Locale.Get("SUBSERVICE_DESC", "Bus");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportMetro:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Metro") + " - " + Locale.Get("SUBSERVICE_DESC", "Metro");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTrain:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get("SUBSERVICE_DESC", "Train");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportShip:
                                                        {
                                                            bool flag55 = CitizenVehicleName.Like("Ferry");
                                                            if (flag55)
                                                            {
                                                                FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
                                                                FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                                FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                                FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                                FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get("FEATURES_DESC", "Ferry");
                                                            }
                                                            else
                                                            {
                                                                FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
                                                                FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                                FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                                FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                                FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Ship");
                                                            }
                                                            break;
                                                        }
                                                    case ItemClass.SubService.PublicTransportPlane:
                                                        {
                                                            bool flag56 = CitizenVehicleName.Like("Blimp");
                                                            if (flag56)
                                                            {
                                                                FavCimsLastActivityVehicleButton.normalBgSprite = "IconPolicyEducationalBlimps";
                                                                FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
                                                                FavCimsLastActivityVehicleButton.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
                                                                FavCimsLastActivityVehicleButton.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";
                                                                FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get("FEATURES_DESC", "Blimp");
                                                            }
                                                            else
                                                            {
                                                                FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
                                                                FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
                                                                FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
                                                                FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";
                                                                FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Plane");
                                                            }
                                                            break;
                                                        }
                                                    case ItemClass.SubService.PublicTransportTaxi:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTaxi";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get("SUBSERVICE_DESC", "Taxi");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTram:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTram";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTramHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTramFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTramPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Tram") + " - " + Locale.Get("SUBSERVICE_DESC", "Tram");
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
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMonorail";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get("SUBSERVICE_DESC", "Monorail");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportCableCar:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportCableCar";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get("SUBSERVICE_DESC", "CableCar");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTours:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTours";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportToursHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportToursFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportToursPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Tours");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportPost:
                                                        FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPost";
                                                        FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPostHovered";
                                                        FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPostFocused";
                                                        FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPostPressed";
                                                        FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Post");
                                                        break;
                                                    default:
                                                        if (subService8 == ItemClass.SubService.PublicTransportTrolleybus)
                                                        {
                                                            FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrolleybus";
                                                            FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrolleybusHovered";
                                                            FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrolleybusFocused";
                                                            FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrolleybusPressed";
                                                            FavCimsLastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Trolleybus");
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                        bool goingOutside3 = GoingOutside;
                                        if (goingOutside3)
                                        {
                                            LeaveCity = true;
                                        }
                                        FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
                                        FavCimsLastActivityVehicleButton.isEnabled = false;
                                        FavCimsLastActivityVehicleButton.tooltip = FavCimsLang.Text("Vehicle_on_foot");
                                    }
                                }
                                else
                                {
                                    FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                    FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
                                    FavCimsLastActivityVehicleButton.isEnabled = false;
                                    FavCimsLastActivityVehicleButton.tooltip = null;
                                }
                                CitizenStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index], out MyTargetID);
                                CitizenTarget = MyBuilding.GetBuildingName(MyTargetID.Building, MyInstanceID);
                                FavCimsLastActivity.text = CitizenStatus + " " + CitizenTarget;
                                bool flag57 = !MyTargetID.IsEmpty;
                                if (flag57)
                                {
                                    TargetDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)MyTargetID.Index].m_position);
                                    bool flag58 = TargetDistrict == 0;
                                    if (flag58)
                                    {
                                        FavCimsLastActivity.tooltip = FavCimsLang.Text("DistrictLabel") + FavCimsLang.Text("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        FavCimsLastActivity.tooltip = FavCimsLang.Text("DistrictLabel") + MyDistrict.GetDistrictName(TargetDistrict);
                                    }
                                }
                                CitizenDistrict = MyDistrict.GetDistrict(citizenInstance.GetSmoothPosition(InstanceCitizenID));
                                bool flag59 = MyCitizen.m_citizens.m_buffer[(int)(IntPtr)(long)(ulong)citizen].Arrested && MyCitizen.m_citizens.m_buffer[(int)(IntPtr)(long)(ulong)citizen].Criminal;
                                if (flag59)
                                {
                                    FavCimsHappyIcon.atlas = MyAtlas.FavCimsAtlas;
                                    FavCimsHappyIcon.normalBgSprite = "FavCimsCrimeArrested";
                                    FavCimsHappyIcon.tooltip = FavCimsLang.Text("Citizen_Arrested");
                                    bool flag60 = MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation == Citizen.Location.Moving;
                                    if (flag60)
                                    {
                                        policeveh = MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
                                        bool flag61 = policeveh > 0;
                                        if (flag61)
                                        {
                                            MyVehicleID.Vehicle = policeveh;
                                            FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
                                            FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsPoliceVehicle";
                                            FavCimsLastActivityVehicleButton.isEnabled = true;
                                            FavCimsLastActivityVehicleButton.playAudioEvents = true;
                                            FavCimsLastActivityVehicleButton.tooltip = MyVehicle.GetVehicleName(policeveh) + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
                                            FavCimsLastActivity.isEnabled = false;
                                            FavCimsLastActivity.text = FavCimsLang.Text("Transported_to_Prison");
                                        }
                                    }
                                    else
                                    {
                                        FavCimsLastActivity.isEnabled = true;
                                        FavCimsLastActivity.text = FavCimsLang.Text("Jailed_into") + CitizenTarget;
                                        FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                    }
                                }
                                else
                                {
                                    FavCimsHappyIcon.atlas = UIView.GetAView().defaultAtlas;
                                    FavCimsHappyIcon.normalBgSprite = CitizenRowData["happiness_icon"];
                                    FavCimsHappyIcon.tooltip = FavCimsLang.Text(CitizenRowData["happiness_icon"]);
                                }
                                bool citizenIsDead = CitizenIsDead;
                                if (citizenIsDead)
                                {
                                    FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
                                    FavCimsHappyIcon.tooltip = FavCimsLang.Text("People_Life_Status_Dead");
                                    bool flag62 = CitizenRowData["deathrealage"] == "0";
                                    if (flag62)
                                    {
                                        CitizenRowData["deathrealage"] = RealAge.ToString();
                                    }
                                    FavCimsRealAge.text = CitizenRowData["deathrealage"];
                                    bool flag63 = DeathDate == null;
                                    if (flag63)
                                    {
                                        DeathDate = GameTime.FavCimsDate(FavCimsLang.Text("time_format"), "n/a");
                                        DeathTime = GameTime.FavCimsTime();
                                    }
                                    FavCimsCitizenName.tooltip = string.Concat(new string[]
                                    {
                                        FavCimsLang.Text("People_Life_Status_Dead"),
                                        " ",
                                        FavCimsLang.Text("People_Life_Status_Dead_date"),
                                        " ",
                                        DeathDate,
                                        " ",
                                        FavCimsLang.Text("People_Life_Status_Dead_time"),
                                        " ",
                                        DeathTime
                                    });
                                    bool flag64 = MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation == Citizen.Location.Moving;
                                    if (flag64)
                                    {
                                        hearse = MyCitizen.m_citizens.m_buffer[(int)citizen].m_vehicle;
                                        bool flag65 = hearse > 0;
                                        if (flag65)
                                        {
                                            CitizenDead.Citizen = citizen;
                                            MyVehicleID.Vehicle = hearse;
                                            FavCimsLastActivityVehicleButton.normalBgSprite = "NotificationIconVerySick";
                                            FavCimsLastActivityVehicleButton.isEnabled = true;
                                            FavCimsLastActivityVehicleButton.playAudioEvents = true;
                                            FavCimsLastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Hearse");
                                            FavCimsLastActivity.text = FavCimsLang.Text("Citizen_on_hearse");
                                        }
                                    }
                                    else
                                    {
                                        bool flag66 = MyCitizen.m_citizens.m_buffer[(int)citizen].CurrentLocation != Citizen.Location.Moving && hearse == 0;
                                        if (flag66)
                                        {
                                            FavCimsLastActivity.text = FavCimsLang.Text("Citizen_wait_hearse");
                                            FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
                                            FavCimsLastActivityVehicleButton.isEnabled = false;
                                        }
                                        else
                                        {
                                            FavCimsLastActivity.text = FavCimsLang.Text("Citizen_hisfuneral");
                                            FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
                                            FavCimsLastActivityVehicleButton.isEnabled = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bool flag67 = rowLang == null || rowLang != FavCimsLang.GameLanguage;
                                if (flag67)
                                {
                                    DeadOrGone = false;
                                }
                                bool flag68 = !DeadOrGone;
                                if (flag68)
                                {
                                    rowLang = FavCimsLang.GameLanguage;
                                    DeadOrGone = true;
                                    bool flag69 = FavCimsCitizenSingleRowPanel != null && FavCimsCore.RowID.ContainsKey(citizenINT) && MyInstancedName.Length > 0;
                                    if (flag69)
                                    {
                                        bool flag70 = DeathDate != null;
                                        if (flag70)
                                        {
                                            DeathDate = GameTime.FavCimsDate(FavCimsLang.Text("time_format"), DeathDate);
                                        }
                                        bool flag71 = DeathDate == null;
                                        if (flag71)
                                        {
                                            DeathDate = GameTime.FavCimsDate(FavCimsLang.Text("time_format"), "n/a");
                                            DeathTime = GameTime.FavCimsTime();
                                        }
                                        FavCimsCitizenName.disabledTextColor = new Color32(51, 51, 51, 160);
                                        FavCimsCitizenName.isEnabled = false;
                                        FavCimsEducation.textColor = new Color32(51, 51, 51, 160);
                                        FavCimsRealAge.textColor = new Color32(51, 51, 51, 160);
                                        FavCimsAgePhase.textColor = new Color32(51, 51, 51, 160);
                                        System.Random random = new System.Random();
                                        int num3 = random.Next(0, 100);
                                        bool flag72 = RealAge >= 85;
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
                                            bool flag74 = RealAge >= 65;
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
                                                bool flag76 = RealAge >= 45;
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
                                                    bool flag78 = RealAge >= 20;
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
                                        bool flag81 = !LeaveCity && (CitizenIsDead || text4 == "dead");
                                        if (flag81)
                                        {
                                            try
                                            {
                                                FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
                                                FavCimsHappyIcon.tooltip = FavCimsLang.Text("People_Life_Status_Dead");
                                                FavCimsCitizenName.text = MyInstancedName;
                                                FavCimsCitizenName.tooltip = string.Concat(new string[]
                                                {
                                                    FavCimsLang.Text("People_Life_Status_Dead"),
                                                    " ",
                                                    FavCimsLang.Text("People_Life_Status_Dead_date"),
                                                    " ",
                                                    DeathDate,
                                                    " ",
                                                    FavCimsLang.Text("People_Life_Status_Dead_time"),
                                                    " ",
                                                    DeathTime
                                                });
                                                OtherInfoButton.isEnabled = false;
                                                FavCimsAgePhase.text = FavCimsLang.Text("AgePhaseDead_" + CitizenRowData["gender"]);
                                                FavCimsCitizenHome.text = FavCimsLang.Text("Home_Location_Dead");
                                                FavCimsCitizenHomeButton.normalBgSprite = "houseofthedead";
                                                FavCimsCitizenHome.tooltip = null;
                                                FavCimsCitizenHome.isEnabled = false;
                                                FavCimsCitizenResidentialLevelSprite.texture = null;
                                                FavCimsCitizenHomeButton.tooltip = null;
                                                FavCimsWorkingPlace.isEnabled = false;
                                                FavCimsWorkingPlace.tooltip = null;
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                                FavCimsWorkingPlaceSprite.tooltip = null;
                                                FavCimsWorkingPlaceButton.tooltip = null;
                                                FavCimsLastActivity.isEnabled = false;
                                                FavCimsLastActivityVehicleButton.isEnabled = false;
                                                FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
                                                FavCimsLastActivityVehicleButton.tooltip = null;
                                                FavCimsLastActivity.tooltip = null;
                                                FavCimsLastActivity.text = FavCimsLang.Text("Citizen_buried");
                                                CitizenRowData.Clear();
                                            }
                                            catch (Exception ex)
                                            {
                                                Utils.Debug.Error("error " + ex.ToString());
                                            }
                                        }
                                        else
                                        {
                                            FavCimsHappyIcon.normalBgSprite = "";
                                            FavCimsHappyIcon.tooltip = null;
                                            FavCimsHappyOverride.texture = TextureDB.FavCimsHappyOverride_texture;
                                            FavCimsHappyOverride.relativePosition = new Vector3(0f, 0f);
                                            FavCimsHappyOverride.tooltip = FavCimsLang.Text("People_Life_Status_IsGone");
                                            FavCimsCitizenName.text = MyInstancedName;
                                            FavCimsCitizenName.tooltip = string.Concat(new string[]
                                            {
                                                FavCimsLang.Text("People_Life_Status_IsGone"),
                                                " ",
                                                FavCimsLang.Text("People_Life_Status_Dead_date"),
                                                " ",
                                                DeathDate,
                                                " ",
                                                FavCimsLang.Text("People_Life_Status_Dead_time"),
                                                " ",
                                                DeathTime
                                            });
                                            OtherInfoButton.isEnabled = false;
                                            FavCimsCitizenHome.text = FavCimsLang.Text("HomeOutsideTheCity");
                                            FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
                                            FavCimsCitizenHome.tooltip = null;
                                            FavCimsCitizenHome.isEnabled = false;
                                            FavCimsCitizenResidentialLevelSprite.texture = null;
                                            FavCimsCitizenHomeButton.tooltip = null;
                                            FavCimsWorkingPlace.isEnabled = false;
                                            FavCimsWorkingPlace.tooltip = null;
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            FavCimsLastActivity.isEnabled = false;
                                            FavCimsLastActivityVehicleButton.isEnabled = false;
                                            FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
                                            FavCimsLastActivity.tooltip = null;
                                            CitizenRowData.Clear();
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            bool flag82 = MyFamily != null;
                                            if (flag82)
                                            {
                                                MyFamily.Hide();
                                                MyFamily.MyInstanceID = InstanceID.Empty;
                                                MyFamily = null;
                                            }
                                            Destroy(gameObject);
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
