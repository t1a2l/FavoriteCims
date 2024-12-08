using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using System;
using System.Collections.Generic;
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

        private UIPanel CitizenSingleRowPanel;

        private Texture FavDot;

        private Texture FavDot_hover;

        private UITextureSprite CitizenSingleRowBGSprite;

        private UIPanel CitizenHappinessPanel;

        private UIPanel CitizenNamePanel;

        private UIButton CitizenNameButton;

        private UITextureSprite NameColText_EmptySprite;

        private UIPanel AgePhasePanel;

        private UIButton AgePhaseButton;

        private UITextureSprite SeparatorSprite2;

        private UITextureSprite SeparatorSprite3;

        private UITextureSprite SeparatorSprite4;

        private UITextureSprite SeparatorSprite5;

        private UITextureSprite SeparatorSprite6;

        private UITextureSprite SeparatorSprite7;

        private UITextureSprite SeparatorSprite8;

        private UITextureSprite SeparatorSprite9;

        private UIPanel RealAgePanel;

        private UIButton RealAgeButton;

        private UIPanel EducationPanel;

        private UIButton EducationButton;

        private UIButton HappyIconButton;

        private UITextureSprite HappySprite;

        private UIPanel CitizenHomePanel;

        private UIButton CitizenHomeButton;

        private UIButton CitizenHomeSpriteButton;

        private UIButton OtherInfoButton;

        private UIPanel WorkingPlacePanel;

        private UITextureSprite WorkingPlaceSprite;

        private UIButton WorkingPlaceButton;

        private UIButton WorkingPlaceSpriteButton;

        private UIPanel LastActivityPanel;

        private UIButton LastActivityButton;

        private UIButton LastActivityVehicleButton;

        private UIPanel CloseRowPanel;

        private UIButton CloseRowButton;

        private UITextureSprite CitizenResidentialLevelSprite;

        private UITextureSprite CitizenWorkPlaceLevelSprite;

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

        private string CitizenNameText;

        private string CitizenVehicleName;

        private string DeathDate;

        private string DeathTime;

        private string CitizenTarget;

        private string CitizenStatus;

        private ushort CitizenHomeId;

        private ushort CitizenVehicleId;

        private ushort CitizenWorkPlaceId;

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
                bool isEmpty = MainClass.Templates[i].MyInstanceID.IsEmpty;
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
                    if (MyInstance.SelectInstance(Target))
                    {
                        if (eventParam.buttons == UIMouseButton.Middle)
                        {
                            if (citizenInfo.m_class.m_service == ItemClass.Service.Tourism)
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
                            if (eventParam.buttons == UIMouseButton.Right)
                            {
                                MainClass.Panel.Hide();
                                ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
                                if (citizenInfo.m_class.m_service == ItemClass.Service.Tourism)
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
                                if (citizenInfo.m_class.m_service == ItemClass.Service.Tourism)
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
                    if (p.buttons == UIMouseButton.Middle)
                    {
                        WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel>(position, CitizenHomeID);
                    }
                    else
                    {
                        if (p.buttons == UIMouseButton.Right)
                        {
                            MainClass.Panel.Hide();
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
                    if (p.buttons == UIMouseButton.Middle)
                    {
                        DefaultTool.OpenWorldInfoPanel(WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        if (p.buttons == UIMouseButton.Right)
                        {
                            MainClass.Panel.Hide();
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
                    if (p.buttons == UIMouseButton.Middle)
                    {
                        DefaultTool.OpenWorldInfoPanel(MyTargetID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        if (p.buttons == UIMouseButton.Right)
                        {
                            MainClass.Panel.Hide();
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
                    if (p.buttons == UIMouseButton.Middle)
                    {
                        DefaultTool.OpenWorldInfoPanel(MyVehicleID, ToolsModifierControl.cameraController.transform.position);
                    }
                    else
                    {
                        if (p.buttons == UIMouseButton.Right)
                        {
                            MainClass.Panel.Hide();
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
                CitizenNameText = MyInstance.GetName(MyInstanceID);
                MyInstancedName ??= CitizenNameText;
                if (citizenINT != 0 && !FavCimsCore.RowID.ContainsKey(citizenINT) && CitizenNameText != null && CitizenNameText.Length > 0)
                {
                    FavCimsCore.InsertIdIntoArray(citizenINT);
                    width = 1134f;
                    height = 41f;
                    autoLayoutDirection = LayoutDirection.Vertical;
                    autoLayout = true;
                    autoLayoutPadding = new RectOffset(0, 0, 1, 0);
                    CitizenSingleRowPanel = AddUIComponent<UIPanel>();
                    CitizenSingleRowPanel.width = width;
                    CitizenSingleRowPanel.height = 40f;
                    CitizenSingleRowBGSprite = CitizenSingleRowPanel.AddUIComponent<UITextureSprite>();
                    CitizenSingleRowBGSprite.name = "FavCimsCitizenSingleRowBGSprite";
                    CitizenSingleRowBGSprite.width = CitizenSingleRowPanel.width;
                    CitizenSingleRowBGSprite.height = CitizenSingleRowPanel.height;
                    CitizenSingleRowBGSprite.AlignTo(CitizenSingleRowPanel, UIAlignAnchor.TopLeft);
                    if (!MainPanel.RowAlternateBackground)
                    {
                        FavDot = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_1.png");
                        FavDot.name = "FavDot_1";
                        CitizenSingleRowBGSprite.texture = FavDot;
                        MainPanel.RowAlternateBackground = true;
                    }
                    else
                    {
                        FavDot = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_2.png");
                        FavDot.name = "FavDot_2";
                        CitizenSingleRowBGSprite.texture = FavDot;
                        MainPanel.RowAlternateBackground = false;
                    }
                    FavDot_hover = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.bgrow_hover.png");
                    CitizenSingleRowBGSprite.eventMouseEnter += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        CitizenSingleRowBGSprite.texture = FavDot_hover;
                    };
                    CitizenSingleRowPanel.eventMouseLeave += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        CitizenSingleRowBGSprite.texture = FavDot;
                    };
                    CitizenHappinessPanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    CitizenHappinessPanel.name = "CitizenHappinessPanel";
                    CitizenHappinessPanel.width = MainPanel.HappinesColText.width;
                    CitizenHappinessPanel.height = 40f;
                    CitizenHappinessPanel.relativePosition = new Vector3(0f, 0f);
                    HappyIconButton = CitizenHappinessPanel.AddUIComponent<UIButton>();
                    HappyIconButton.width = 30f;
                    HappyIconButton.height = 30f;
                    HappyIconButton.isEnabled = false;
                    HappyIconButton.playAudioEvents = false;
                    HappyIconButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    HappyIconButton.relativePosition = new Vector3(15f, 5f);
                    HappySprite = HappyIconButton.AddUIComponent<UITextureSprite>();
                    HappySprite.width = 30f;
                    HappySprite.height = 30f;
                    HappySprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CitizenNamePanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    CitizenNamePanel.name = "CitizenNamePanel";
                    CitizenNamePanel.width = MainPanel.NameColText.width;
                    CitizenNamePanel.height = 40f;
                    CitizenNamePanel.relativePosition = new Vector3(CitizenHappinessPanel.relativePosition.x + CitizenHappinessPanel.width, 0f);
                    CitizenNameButton = CitizenNamePanel.AddUIComponent<UIButton>();
                    CitizenNameButton.name = "CitizenNameButton";
                    CitizenNameButton.width = CitizenNamePanel.width;
                    CitizenNameButton.height = CitizenNamePanel.height;
                    CitizenNameButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    CitizenNameButton.textHorizontalAlignment = 0;
                    CitizenNameButton.playAudioEvents = true;
                    CitizenNameButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    CitizenNameButton.font.size = 15;
                    CitizenNameButton.textScale = 1f;
                    CitizenNameButton.wordWrap = true;
                    CitizenNameButton.textPadding.left = 40;
                    CitizenNameButton.textPadding.right = 5;
                    CitizenNameButton.textColor = new Color32(204, 204, 51, 40);
                    CitizenNameButton.hoveredTextColor = new Color32(204, 102, 0, 20);
                    CitizenNameButton.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    CitizenNameButton.focusedTextColor = new Color32(153, 0, 0, 0);
                    CitizenNameButton.useDropShadow = true;
                    CitizenNameButton.dropShadowOffset = new Vector2(1f, -1f);
                    CitizenNameButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    CitizenNameButton.maximumSize = new Vector2(CitizenNamePanel.width, CitizenNamePanel.height);
                    CitizenNameButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CitizenNameButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        GoToCitizen(MyInstanceID, eventParam);
                    };
                    NameColText_EmptySprite = CitizenNamePanel.AddUIComponent<UITextureSprite>();
                    NameColText_EmptySprite.width = CitizenNameButton.width;
                    NameColText_EmptySprite.height = CitizenNameButton.height;
                    NameColText_EmptySprite.relativePosition = new Vector3(0f, 0f);
                    bool columnSpecialBackground = MainPanel.ColumnSpecialBackground;
                    if (columnSpecialBackground)
                    {
                        NameColText_EmptySprite.texture = TextureDB.NameBgOverride_texture;
                        NameColText_EmptySprite.opacity = 0.7f;
                    }
                    MainPanel.NameColText.eventClick += delegate
                    {
                        if (NameColText_EmptySprite.texture == null)
                        {
                            NameColText_EmptySprite.texture = TextureDB.NameBgOverride_texture;
                            NameColText_EmptySprite.opacity = 0.7f;
                            MainPanel.ColumnSpecialBackground = true;
                        }
                        else
                        {
                            NameColText_EmptySprite.texture = null;
                            MainPanel.ColumnSpecialBackground = false;
                        }
                    };
                    CitizenNameButton.BringToFront();
                    CitizenNameButton.relativePosition = new Vector3(0f, 0f);
                    OtherInfoButton = CitizenNamePanel.AddUIComponent<UIButton>();
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
                            if (MyFamily == null && GetTemplate() >= 0)
                            {
                                MyFamily = MainClass.Templates[GetTemplate()];
                                MyFamily.MyInstanceID = MyInstanceID;
                                MyFamily.Show();
                                MyFamily.BringToFront();
                                OtherInfoButton.normalBgSprite = "CityInfoFocused";
                            }
                            else
                            {
                                if (MyFamily != null && !MyFamily.isVisible)
                                {
                                    MyFamily.MyInstanceID = MyInstanceID;
                                    MyFamily.Show();
                                    MyFamily.BringToFront();
                                    OtherInfoButton.normalBgSprite = "CityInfoFocused";
                                }
                                else
                                {
                                    if (MyFamily != null)
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
                    SeparatorSprite2 = CitizenNamePanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite2.name = "FavCimsSeparatorSprite2";
                    SeparatorSprite2.texture = TextureDB.Separator;
                    SeparatorSprite2.relativePosition = new Vector3(0f, 0f);
                    AgePhasePanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    AgePhasePanel.name = "AgePhasePanel";
                    AgePhasePanel.width = MainPanel.AgePhaseColText.width;
                    AgePhasePanel.height = 40f;
                    AgePhasePanel.relativePosition = new Vector3(CitizenNamePanel.relativePosition.x + CitizenNamePanel.width, 0f);
                    AgePhaseButton = AgePhasePanel.AddUIComponent<UIButton>();
                    AgePhaseButton.name = "AgePhaseButton";
                    AgePhaseButton.width = AgePhasePanel.width;
                    AgePhaseButton.height = AgePhasePanel.height;
                    AgePhaseButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    AgePhaseButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    AgePhaseButton.playAudioEvents = true;
                    AgePhaseButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    AgePhaseButton.font.size = 15;
                    AgePhaseButton.textScale = 1f;
                    AgePhaseButton.wordWrap = true;
                    AgePhaseButton.textPadding.left = 5;
                    AgePhaseButton.textPadding.right = 5;
                    AgePhaseButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    AgePhaseButton.outlineSize = 1;
                    AgePhaseButton.outlineColor = new Color32(0, 0, 0, 0);
                    AgePhaseButton.useDropShadow = true;
                    AgePhaseButton.dropShadowOffset = new Vector2(1f, -1f);
                    AgePhaseButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    AgePhaseButton.maximumSize = new Vector2(AgePhasePanel.width, AgePhasePanel.height);
                    AgePhaseButton.isInteractive = false;
                    AgePhaseButton.relativePosition = new Vector3(0f, 0f);
                    SeparatorSprite3 = AgePhasePanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite3.name = "SeparatorSprite3";
                    SeparatorSprite3.texture = TextureDB.Separator;
                    SeparatorSprite3.relativePosition = new Vector3(0f, 0f);
                    RealAgePanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    RealAgePanel.name = "RealAgePanel";
                    RealAgePanel.width = MainPanel.AgeColText.width;
                    RealAgePanel.height = 40f;
                    RealAgePanel.relativePosition = new Vector3(AgePhasePanel.relativePosition.x + AgePhasePanel.width, 0f);
                    RealAgeButton = RealAgePanel.AddUIComponent<UIButton>();
                    RealAgeButton.name = "RealAgeButton";
                    RealAgeButton.width = RealAgePanel.width;
                    RealAgeButton.height = RealAgePanel.height;
                    RealAgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    RealAgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    RealAgeButton.playAudioEvents = true;
                    RealAgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    RealAgeButton.font.size = 15;
                    RealAgeButton.textScale = 1f;
                    RealAgeButton.wordWrap = true;
                    RealAgeButton.textPadding.left = 5;
                    RealAgeButton.textPadding.right = 5;
                    RealAgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    RealAgeButton.outlineSize = 1;
                    RealAgeButton.outlineColor = new Color32(0, 0, 0, 0);
                    RealAgeButton.useDropShadow = true;
                    RealAgeButton.dropShadowOffset = new Vector2(1f, -1f);
                    RealAgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    RealAgeButton.maximumSize = new Vector2(RealAgePanel.width, RealAgePanel.height);
                    RealAgeButton.isInteractive = false;
                    RealAgeButton.relativePosition = new Vector3(0f, 0f);
                    SeparatorSprite4 = RealAgePanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite4.name = "SeparatorSprite4";
                    SeparatorSprite4.texture = TextureDB.Separator;
                    SeparatorSprite4.relativePosition = new Vector3(0f, 0f);
                    EducationPanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    EducationPanel.name = "EducationPanel";
                    EducationPanel.width = MainPanel.EduColText.width;
                    EducationPanel.height = 40f;
                    EducationPanel.relativePosition = new Vector3(RealAgePanel.relativePosition.x + RealAgePanel.width, 0f);
                    EducationButton = EducationPanel.AddUIComponent<UIButton>();
                    EducationButton.name = "EducationButton";
                    EducationButton.width = EducationPanel.width;
                    EducationButton.height = EducationPanel.height;
                    EducationButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    EducationButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
                    EducationButton.playAudioEvents = true;
                    EducationButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    EducationButton.font.size = 15;
                    EducationButton.textScale = 1f;
                    EducationButton.wordWrap = true;
                    EducationButton.textPadding.left = 5;
                    EducationButton.textPadding.right = 5;
                    EducationButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    EducationButton.outlineSize = 1;
                    EducationButton.outlineColor = new Color32(0, 0, 0, 0);
                    EducationButton.useDropShadow = true;
                    EducationButton.dropShadowOffset = new Vector2(1f, -1f);
                    EducationButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    EducationButton.maximumSize = new Vector2(EducationPanel.width, EducationPanel.height);
                    EducationButton.isInteractive = false;
                    EducationButton.relativePosition = new Vector3(0f, 0f);
                    SeparatorSprite5 = EducationPanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite5.name = "SeparatorSprite5";
                    SeparatorSprite5.texture = TextureDB.Separator;
                    SeparatorSprite5.relativePosition = new Vector3(0f, 0f);
                    CitizenHomePanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    CitizenHomePanel.name = "CitizenHomePanel";
                    CitizenHomePanel.width = MainPanel.HomeColText.width;
                    CitizenHomePanel.height = 40f;
                    CitizenHomePanel.relativePosition = new Vector3(EducationPanel.relativePosition.x + EducationPanel.width, 0f);
                    CitizenHomeButton = CitizenHomePanel.AddUIComponent<UIButton>();
                    CitizenHomeButton.name = "CitizenHomeButton";
                    CitizenHomeButton.width = CitizenHomePanel.width;
                    CitizenHomeButton.height = CitizenHomePanel.height;
                    CitizenHomeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    CitizenHomeButton.textHorizontalAlignment = 0;
                    CitizenHomeButton.playAudioEvents = true;
                    CitizenHomeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    CitizenHomeButton.font.size = 15;
                    CitizenHomeButton.textScale = 0.85f;
                    CitizenHomeButton.wordWrap = true;
                    CitizenHomeButton.textPadding.left = 40;
                    CitizenHomeButton.textPadding.right = 5;
                    CitizenHomeButton.outlineColor = new Color32(0, 0, 0, 0);
                    CitizenHomeButton.outlineSize = 1;
                    CitizenHomeButton.textColor = new Color32(21, 59, 96, 140);
                    CitizenHomeButton.hoveredTextColor = new Color32(204, 102, 0, 20);
                    CitizenHomeButton.pressedTextColor = new Color32(153, 0, 0, 0);
                    CitizenHomeButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    CitizenHomeButton.disabledTextColor = new Color32(51, 51, 51, 160);
                    CitizenHomeButton.useDropShadow = true;
                    CitizenHomeButton.dropShadowOffset = new Vector2(1f, -1f);
                    CitizenHomeButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    CitizenHomeButton.maximumSize = new Vector2(CitizenHomePanel.width, CitizenHomePanel.height);
                    CitizenHomeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CitizenHomeButton.eventMouseUp += new MouseEventHandler(GoToHome);
                    CitizenHomeButton.relativePosition = new Vector3(0f, 0f);
                    CitizenHomeSpriteButton = CitizenHomePanel.AddUIComponent<UIButton>();
                    CitizenHomeSpriteButton.name = "CitizenHomeSpriteButton";
                    CitizenHomeSpriteButton.atlas = MyAtlas.FavCimsAtlas;
                    CitizenHomeSpriteButton.size = new Vector2(20f, 40f);
                    CitizenHomeSpriteButton.relativePosition = new Vector3(10f, 0f);
                    CitizenHomeSpriteButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CitizenResidentialLevelSprite = CitizenHomeButton.AddUIComponent<UITextureSprite>();
                    CitizenResidentialLevelSprite.name = "CitizenResidentialLevelSprite";
                    CitizenResidentialLevelSprite.relativePosition = new Vector3(0f, 0f);
                    SeparatorSprite6 = CitizenHomePanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite6.name = "SeparatorSprite6";
                    SeparatorSprite6.texture = TextureDB.Separator;
                    SeparatorSprite6.relativePosition = new Vector3(0f, 0f);
                    WorkingPlacePanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    WorkingPlacePanel.name = "WorkingPlacePanel";
                    WorkingPlacePanel.width = MainPanel.WorkingPlaceColText.width;
                    WorkingPlacePanel.height = 40f;
                    WorkingPlacePanel.relativePosition = new Vector3(CitizenHomePanel.relativePosition.x + CitizenHomePanel.width, 0f);
                    WorkingPlaceButton = WorkingPlacePanel.AddUIComponent<UIButton>();
                    WorkingPlaceButton.name = "WorkingPlaceButton";
                    WorkingPlaceButton.width = WorkingPlacePanel.width;
                    WorkingPlaceButton.height = WorkingPlacePanel.height;
                    WorkingPlaceButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    WorkingPlaceButton.textHorizontalAlignment = 0;
                    WorkingPlaceButton.playAudioEvents = true;
                    WorkingPlaceButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    WorkingPlaceButton.font.size = 15;
                    WorkingPlaceButton.textScale = 0.85f;
                    WorkingPlaceButton.wordWrap = true;
                    WorkingPlaceButton.textPadding.left = 40;
                    WorkingPlaceButton.textPadding.right = 5;
                    WorkingPlaceButton.outlineColor = new Color32(0, 0, 0, 0);
                    WorkingPlaceButton.outlineSize = 1;
                    WorkingPlaceButton.textColor = new Color32(21, 59, 96, 140);
                    WorkingPlaceButton.hoveredTextColor = new Color32(204, 102, 0, 20);
                    WorkingPlaceButton.pressedTextColor = new Color32(153, 0, 0, 0);
                    WorkingPlaceButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    WorkingPlaceButton.disabledTextColor = new Color32(51, 51, 51, 160);
                    WorkingPlaceButton.useDropShadow = true;
                    WorkingPlaceButton.dropShadowOffset = new Vector2(1f, -1f);
                    WorkingPlaceButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    WorkingPlaceButton.maximumSize = new Vector2(WorkingPlacePanel.width, WorkingPlacePanel.height);
                    WorkingPlaceButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    WorkingPlaceButton.eventMouseUp += new MouseEventHandler(GoToWork);
                    WorkingPlaceButton.relativePosition = new Vector3(0f, 0f);
                    WorkingPlaceSprite = WorkingPlacePanel.AddUIComponent<UITextureSprite>();
                    WorkingPlaceSprite.name = "WorkingPlaceSprite";
                    WorkingPlaceSprite.width = 20f;
                    WorkingPlaceSprite.height = 40f;
                    WorkingPlaceSprite.relativePosition = new Vector3(10f, 0f);
                    WorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    WorkingPlaceSpriteButton = WorkingPlaceSprite.AddUIComponent<UIButton>();
                    WorkingPlaceSpriteButton.name = "WorkingPlaceSpriteButton";
                    WorkingPlaceSpriteButton.width = 20f;
                    WorkingPlaceSpriteButton.height = 20f;
                    WorkingPlaceSpriteButton.relativePosition = new Vector3(0f, 10f);
                    WorkingPlaceSpriteButton.isInteractive = false;
                    WorkingPlaceSpriteButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CitizenWorkPlaceLevelSprite = WorkingPlaceSprite.AddUIComponent<UITextureSprite>();
                    CitizenWorkPlaceLevelSprite.name = "CitizenWorkPlaceLevelSprite";
                    CitizenWorkPlaceLevelSprite.relativePosition = new Vector3(0f, 0f);
                    SeparatorSprite7 = WorkingPlacePanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite7.name = "SeparatorSprite7";
                    SeparatorSprite7.texture = TextureDB.Separator;
                    SeparatorSprite7.relativePosition = new Vector3(0f, 0f);
                    LastActivityPanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    LastActivityPanel.name = "LastActivityPanel";
                    LastActivityPanel.width = MainPanel.LastActColText.width;
                    LastActivityPanel.height = 40f;
                    LastActivityPanel.relativePosition = new Vector3(WorkingPlacePanel.relativePosition.x + WorkingPlacePanel.width, 0f);
                    LastActivityButton = LastActivityPanel.AddUIComponent<UIButton>();
                    LastActivityButton.name = "LastActivityButton";
                    LastActivityButton.width = LastActivityPanel.width - 40f;
                    LastActivityButton.height = LastActivityPanel.height;
                    LastActivityButton.textVerticalAlignment = UIVerticalAlignment.Middle;
                    LastActivityButton.textHorizontalAlignment = 0;
                    LastActivityButton.playAudioEvents = true;
                    LastActivityButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
                    LastActivityButton.font.size = 14;
                    LastActivityButton.textScale = 0.85f;
                    LastActivityButton.wordWrap = true;
                    LastActivityButton.textPadding.left = 0;
                    LastActivityButton.textPadding.right = 5;
                    LastActivityButton.outlineColor = new Color32(0, 0, 0, 0);
                    LastActivityButton.outlineSize = 1;
                    LastActivityButton.textColor = new Color32(21, 59, 96, 140);
                    LastActivityButton.hoveredTextColor = new Color32(204, 102, 0, 20);
                    LastActivityButton.pressedTextColor = new Color32(153, 0, 0, 0);
                    LastActivityButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
                    LastActivityButton.disabledTextColor = new Color32(51, 51, 51, 160);
                    LastActivityButton.useDropShadow = true;
                    LastActivityButton.dropShadowOffset = new Vector2(1f, -1f);
                    LastActivityButton.dropShadowColor = new Color32(0, 0, 0, 0);
                    LastActivityButton.maximumSize = new Vector2(LastActivityPanel.width - 40f, LastActivityPanel.height);
                    LastActivityButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    LastActivityButton.eventMouseUp += new MouseEventHandler(GoToTarget);
                    LastActivityButton.relativePosition = new Vector3(40f, 0f);
                    LastActivityVehicleButton = LastActivityPanel.AddUIComponent<UIButton>();
                    LastActivityVehicleButton.name = "LastActivityVehicleButton";
                    LastActivityVehicleButton.width = 26f;
                    LastActivityVehicleButton.height = 26f;
                    LastActivityVehicleButton.relativePosition = new Vector3(5f, 7f);
                    LastActivityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    LastActivityVehicleButton.eventMouseUp += new MouseEventHandler(GoToVehicle);
                    SeparatorSprite8 = LastActivityPanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite8.name = "SeparatorSprite8";
                    SeparatorSprite8.texture = TextureDB.Separator;
                    SeparatorSprite8.relativePosition = new Vector3(0f, 0f);
                    CloseRowPanel = CitizenSingleRowPanel.AddUIComponent<UIPanel>();
                    CloseRowPanel.name = "CloseRowPanel";
                    CloseRowPanel.width = MainPanel.CloseButtonCol.width;
                    CloseRowPanel.height = 40f;
                    CloseRowPanel.relativePosition = new Vector3(LastActivityPanel.relativePosition.x + LastActivityPanel.width, 0f);
                    CloseRowButton = CloseRowPanel.AddUIComponent<UIButton>();
                    CloseRowButton.name = "CloseRowButton";
                    CloseRowButton.width = 26f;
                    CloseRowButton.height = 26f;
                    CloseRowButton.normalBgSprite = "buttonclose";
                    CloseRowButton.hoveredBgSprite = "buttonclosehover";
                    CloseRowButton.pressedBgSprite = "buttonclosepressed";
                    CloseRowButton.opacity = 0.9f;
                    CloseRowButton.playAudioEvents = true;
                    CloseRowButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
                    CloseRowButton.eventClick += delegate
                    {
                        try
                        {
                            FavCimsCore.RemoveRowAndRemoveFav(MyInstanceID, citizenINT);
                            if (MyFamily != null)
                            {
                                MyFamily.Hide();
                                MyFamily.MyInstanceID = InstanceID.Empty;
                                MyFamily = null;
                            }
                            if (UIView.Find<UILabel>("DefaultTooltip"))
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
                    CloseRowButton.relativePosition = new Vector3(CloseRowPanel.width / 2f - CloseRowPanel.width / 2f, 7f);
                    SeparatorSprite9 = CloseRowPanel.AddUIComponent<UITextureSprite>();
                    SeparatorSprite9.name = "SeparatorSprite9";
                    SeparatorSprite9.texture = TextureDB.Separator;
                    SeparatorSprite9.relativePosition = new Vector3(0f, 0f);
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("CitizenRow Create Error : " + ex.ToString());
            }
        }

        public override void Update()
        {
            bool unLoading = MainClass.UnLoading;
            if (!unLoading)
            {
                bool firstRun = FirstRun;
                if (firstRun)
                {
                    secondsForceRun -= 1f * Time.deltaTime;
                    if (secondsForceRun > 0f)
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
                    if (!MainClass.Panel.isVisible || IsClippedFromParent())
                    {
                        CitizenSingleRowPanel.Hide();
                        HiddenRowsSeconds -= 1f * Time.deltaTime;
                        if (HiddenRowsSeconds <= 0f)
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
                        CitizenSingleRowPanel.Show();
                        seconds -= 1f * Time.deltaTime;
                        if (seconds <= 0f)
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
            bool unLoading = MainClass.UnLoading;
            if (!unLoading)
            {
                if (MyInstanceID.IsEmpty || !FavCimsCore.RowID.ContainsKey(citizenINT))
                {
                    if (MyFamily != null)
                    {
                        MyFamily.Hide();
                        MyFamily.MyInstanceID = InstanceID.Empty;
                        MyFamily = null;
                    }
                    Destroy(gameObject);
                }
                else
                {
                    if (DeadOrGone || HomeLess)
                    {
                        OtherInfoButton.isEnabled = false;
                        OtherInfoButton.tooltip = Translations.Translate("Citizen_Details_NoUnit");
                    }
                    else
                    {
                        if (GetTemplate() == -1 && (MyFamily == null || MyFamily.MyInstanceID != MyInstanceID))
                        {
                            if (MyFamily != null && MyFamily.MyInstanceID != MyInstanceID)
                            {
                                MyFamily = null;
                            }
                            OtherInfoButton.isEnabled = false;
                            OtherInfoButton.tooltip = Translations.Translate("Citizen_Details_fullTemplate");
                        }
                        else
                        {
                            if (MyFamily != null && MyFamily.MyInstanceID == MyInstanceID && MyFamily.isVisible)
                            {
                                OtherInfoButton.normalBgSprite = "CityInfoFocused";
                            }
                            else
                            {
                                OtherInfoButton.normalBgSprite = "CityInfo";
                            }
                            OtherInfoButton.isEnabled = true;
                            OtherInfoButton.tooltip = Translations.Translate("Citizen_Details");
                        }
                    }
                    uint citizen = MyInstanceID.Citizen;
                    if (citizen != 0U && MyCitizen.m_citizens.m_buffer[citizen].Dead && !CitizenIsDead)
                    {
                        CitizenIsDead = true;
                        CitizenRowData["deathrealage"] = "0";
                    }
                    if (execute)
                    {
                        try
                        {
                            CitizenNameText = MyInstance.GetName(MyInstanceID);
                            citizenINT = (int)citizen;
                            if (CitizenNameText != null && CitizenNameText.Length > 0 && CitizenNameText != MyInstancedName)
                            {
                                MyInstancedName = CitizenNameText;
                            }
                            citizenInfo = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].GetCitizenInfo(citizen);
                            CloseRowButton.tooltip = Translations.Translate("FavStarButton_disable_tooltip");  
                            if (CitizenSingleRowPanel != null && citizen != 0U && CitizenNameText == MyInstancedName && FavCimsCore.RowID.ContainsKey(citizenINT))
                            {
                                Citizen.Gender gender = Citizen.GetGender(citizen);
                                CitizenRowData["gender"] = gender.ToString();
                                CitizenRowData["name"] = MyCitizen.GetCitizenName(citizen);
                                CitizenNameButton.text = CitizenRowData["name"];
                                if (CitizenRowData["gender"] == "Female")
                                {
                                    CitizenNameButton.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                }
                                if (CitizenDistrict == 0)
                                {
                                    CitizenNameButton.tooltip = Translations.Translate("NowInThisDistrict") + Translations.Translate("DistrictNameNoDistrict");
                                }
                                else
                                {
                                    CitizenNameButton.tooltip = Translations.Translate("NowInThisDistrict") + MyDistrict.GetDistrictName(CitizenDistrict);
                                }
                                tmp_health = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_health;
                                CitizenRowData["health"] = Citizen.GetHealthLevel(tmp_health).ToString();
                                Citizen.Education educationLevel = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].EducationLevel;
                                CitizenRowData["education"] = educationLevel.ToString();
                                EducationButton.text = Translations.Translate("Education_" + CitizenRowData["education"] + "_" + CitizenRowData["gender"]);
                                
                                switch(CitizenRowData["education"])
                                {
                                    case "ThreeSchools":
                                        EducationButton.textColor = new Color32(102, 204, 0, 60);
                                        break;
                                    case "TwoSchools":
                                        EducationButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        break;
                                    case "OneSchool":
                                        EducationButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        break;
                                    default:
                                        EducationButton.textColor = new Color32(153, 0, 0, 0);
                                        break;
                                }
                                
                                tmp_wellbeing = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_wellbeing;
                                CitizenRowData["wellbeing"] = Citizen.GetWellbeingLevel(educationLevel, tmp_wellbeing).ToString();
                                tmp_happiness = Citizen.GetHappiness(tmp_health, tmp_wellbeing);
                                CitizenRowData["happiness_icon"] = GetHappinessString(Citizen.GetHappinessLevel(tmp_happiness));
                                HappyIconButton.normalBgSprite = CitizenRowData["happiness_icon"];
                                HappyIconButton.tooltip = Translations.Translate(CitizenRowData["happiness_icon"]);
                                tmp_age = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_age;
                                CitizenRowData["agegroup"] = Citizen.GetAgeGroup(tmp_age).ToString();
                                AgePhaseButton.text = Translations.Translate("AgePhase_" + CitizenRowData["agegroup"] + "_" + CitizenRowData["gender"]);
                                RealAge = FavCimsCore.CalculateCitizenAge(tmp_age);
                                RealAgeButton.text = RealAge.ToString();
                                switch (RealAge)
                                {
                                    case int n when n <= 12:
                                        RealAgeButton.textColor = new Color32(102, 204, 0, 60);
                                        AgePhaseButton.textColor = new Color32(102, 204, 0, 60);
                                        break;
                                    case int n when n <= 19:
                                        RealAgeButton.textColor = new Color32(0, 102, 51, 100);
                                        AgePhaseButton.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                    case int n when n <= 25:
                                        RealAgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        AgePhaseButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        break;
                                    case int n when n <= 65:
                                        RealAgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        AgePhaseButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        break;
                                    case int n when n <= 90:
                                        RealAgeButton.textColor = new Color32(153, 0, 0, 0);
                                        AgePhaseButton.textColor = new Color32(153, 0, 0, 0);
                                        break;
                                    default:
                                        RealAgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        AgePhaseButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        break;
                                }

                                CitizenHomeId = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_homeBuilding;
                                if (CitizenHomeId > 0)
                                {
                                    HomeLess = false;
                                    CitizenHomeID.Building = CitizenHomeId;
                                    CitizenHomeButton.text = MyBuilding.GetBuildingName(CitizenHomeId, MyInstanceID);
                                    CitizenHomeButton.isEnabled = true;
                                    CitizenHomeSpriteButton.normalBgSprite = "homeIconLow";
                                    HomeInfo = MyBuilding.m_buildings.m_buffer[CitizenHomeID.Index].Info;

                                    if (HomeInfo.m_class.m_service == ItemClass.Service.Residential)
                                    {
                                        CitizenHomeButton.tooltip = null;

                                        switch (HomeInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.ResidentialHigh:
                                                CitizenHomeButton.textColor = new Color32(0, 102, 51, 100);
                                                CitizenHomeSpriteButton.normalBgSprite = "homeIconHigh";
                                                CitizenHomeButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString());
                                                break;
                                            case ItemClass.SubService.ResidentialHighEco:
                                                CitizenHomeButton.textColor = new Color32(0, 102, 51, 100);
                                                CitizenHomeSpriteButton.normalBgSprite = "homeIconHigh";
                                                CitizenHomeButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString()) + " Eco";
                                                break;
                                            case ItemClass.SubService.ResidentialLowEco:
                                                CitizenHomeButton.textColor = new Color32(0, 153, 0, 80);
                                                CitizenHomeSpriteButton.normalBgSprite = "homeIconLow";
                                                CitizenHomeButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString()) + " Eco";
                                                break;
                                            case ItemClass.SubService.ResidentialLow:
                                                CitizenHomeButton.textColor = new Color32(0, 153, 0, 80);
                                                CitizenHomeSpriteButton.normalBgSprite = "homeIconLow";
                                                CitizenHomeButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString());
                                                break;
                                        }

                                        switch (HomeInfo.m_class.m_level)
                                        {
                                            case ItemClass.Level.Level2:
                                                CitizenResidentialLevelSprite.texture = TextureDB.ResidentialLevel[2];
                                                break;
                                            case ItemClass.Level.Level3:
                                                CitizenResidentialLevelSprite.texture = TextureDB.ResidentialLevel[3];
                                                break;
                                            case ItemClass.Level.Level4:
                                                CitizenResidentialLevelSprite.texture = TextureDB.ResidentialLevel[4];
                                                break;
                                            case ItemClass.Level.Level5:
                                                CitizenResidentialLevelSprite.texture = TextureDB.ResidentialLevel[5];
                                                break;
                                            default:
                                                CitizenResidentialLevelSprite.texture = TextureDB.ResidentialLevel[1];
                                                break;
                                        }

                                        HomeDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[CitizenHomeID.Index].m_position);
                                        if (HomeDistrict == 0)
                                        {
                                            CitizenHomeSpriteButton.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                                        }
                                        else
                                        {
                                            CitizenHomeSpriteButton.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(HomeDistrict);
                                        }
                                    }
                                }
                                else
                                {
                                    CitizenHomeButton.text = Translations.Translate("Citizen_HomeLess");
                                    CitizenHomeButton.isEnabled = false;
                                    CitizenHomeSpriteButton.normalBgSprite = "homelessIcon";
                                    CitizenHomeSpriteButton.tooltip = Translations.Translate("DistrictNameNoDistrict");
                                    CitizenHomeButton.tooltip = Translations.Translate("Citizen_HomeLess_tooltip");
                                    CitizenResidentialLevelSprite.texture = null;
                                    HomeLess = true;
                                }

                                CitizenWorkPlaceId = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_workBuilding;
                                if (MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].GetCurrentSchoolLevel(citizen) != ItemClass.Level.None)
                                {
                                    isStudent = true;
                                    WorkingPlaceSpriteButton.normalBgSprite = null;
                                    WorkingPlaceSprite.texture = TextureDB.WorkingPlaceTextureStudent;
                                    CitizenWorkPlaceLevelSprite.texture = null;
                                    WorkingPlaceButton.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].GetCurrentSchoolLevel(citizen).ToString()) + " " + MyBuilding.GetBuildingName(CitizenWorkPlaceId, MyInstanceID);
                                }
                                else
                                {
                                    if (CitizenWorkPlaceId == 0)
                                    {
                                        WorkingPlaceSpriteButton.normalBgSprite = null;
                                        if ((MyCitizen.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) > 0)
                                        {
                                            string text = string.Empty;
                                            if (SteamHelper.IsDLCOwned(SteamHelper.DLC.CampusDLC))
                                            {
                                                float num = Singleton<ImmaterialResourceManager>.instance.CheckExchangeStudentAttractivenessBonus() * 100f;
                                                Randomizer m_randomizer = new(citizen);
                                                text = m_randomizer.Int32(0, 100) >= (double)num ? Locale.Get("CITIZEN_OCCUPATION_TOURIST") : Locale.Get("CITIZEN_OCCUPATION_EXCHANGESTUDENT");
                                            }
                                            else
                                            {
                                                text = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
                                            }
                                            WorkingPlaceSprite.texture = TextureDB.WorkingPlaceTexture;
                                            WorkingPlaceButton.text = text;
                                            WorkingPlaceButton.isEnabled = false;
                                            WorkingPlaceButton.tooltip = Translations.Translate("Citizen_Tourist_tooltip");
                                            WorkingPlaceSprite.tooltip = null;
                                            WorkingPlaceSpriteButton.tooltip = null;
                                            CitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                        else
                                        {
                                            if (tmp_age >= 180)
                                            {
                                                WorkingPlaceSprite.texture = TextureDB.WorkingPlaceTextureRetired;
                                                WorkingPlaceButton.text = Translations.Translate("Citizen_Retired");
                                                WorkingPlaceButton.isEnabled = false;
                                                WorkingPlaceButton.tooltip = Translations.Translate("Citizen_Retired_tooltip");
                                                WorkingPlaceSprite.tooltip = null;
                                                WorkingPlaceSpriteButton.tooltip = null;
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                            else
                                            {
                                                WorkingPlaceSprite.texture = TextureDB.WorkingPlaceTexture;
                                                WorkingPlaceButton.text = Locale.Get("CITIZEN_OCCUPATION_UNEMPLOYED");
                                                WorkingPlaceButton.isEnabled = false;
                                                WorkingPlaceButton.tooltip = Translations.Translate("Unemployed_tooltip");
                                                WorkingPlaceSprite.tooltip = null;
                                                WorkingPlaceSpriteButton.tooltip = null;
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                    }
                                }
                                if (CitizenWorkPlaceId > 0)
                                {
                                    string text2 = string.Empty;
                                    if (!isStudent)
                                    {
                                        CommonBuildingAI commonBuildingAI = MyBuilding.m_buildings.m_buffer[CitizenWorkPlaceId].Info.m_buildingAI as CommonBuildingAI;
                                        if (commonBuildingAI != null)
                                        {
                                            text2 = commonBuildingAI.GetTitle(gender, educationLevel, CitizenWorkPlaceId, citizen);
                                        }
                                        if (text2 == string.Empty)
                                        {
                                            int num2 = new Randomizer(CitizenWorkPlaceId + citizen).Int32(1, 5);
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
                                    WorkPlaceID.Building = CitizenWorkPlaceId;
                                    WorkingPlaceButton.text = text2 + " " + MyBuilding.GetBuildingName(CitizenWorkPlaceId, MyInstanceID);
                                    WorkingPlaceButton.isEnabled = true;
                                    WorkInfo = MyBuilding.m_buildings.m_buffer[WorkPlaceID.Index].Info;
                                    WorkingPlaceSprite.texture = null;
                                    if (WorkInfo.m_class.m_service == ItemClass.Service.Commercial)
                                    {
                                        WorkingPlaceSpriteButton.normalBgSprite = null;

                                        switch (WorkInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.CommercialHigh:
                                                WorkingPlaceButton.textColor = new Color32(0, 51, 153, 147);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenCommercialHighTexture;
                                                WorkingPlaceButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
                                                break;
                                            case ItemClass.SubService.CommercialEco:
                                                WorkingPlaceButton.textColor = new Color32(0, 150, 136, 116);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenCommercialHighTexture;
                                                WorkingPlaceButton.tooltip = Translations.Translate("Buildings_Type_CommercialEco");
                                                break;
                                            case ItemClass.SubService.CommercialLeisure:
                                                WorkingPlaceButton.textColor = new Color32(219, 68, 55, 3);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenCommercialHighTexture;
                                                WorkingPlaceButton.tooltip = Translations.Translate("Buildings_Type_CommercialLeisure");
                                                break;
                                            case ItemClass.SubService.CommercialTourist:
                                                WorkingPlaceButton.textColor = new Color32(156, 39, 176, 194);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenCommercialHighTexture;
                                                WorkingPlaceButton.tooltip = Translations.Translate("Buildings_Type_CommercialTourist");
                                                break;
                                            default:
                                                WorkingPlaceButton.textColor = new Color32(0, 153, 204, 130);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenCommercialLowTexture;
                                                WorkingPlaceButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
                                                break;
                                        }

                                        if (WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow)
                                        {
                                            switch (WorkInfo.m_class.m_level)
                                            {
                                                case ItemClass.Level.Level2:
                                                    CitizenWorkPlaceLevelSprite.texture = TextureDB.CommercialLevel[2];
                                                    break;
                                                case ItemClass.Level.Level3:
                                                    CitizenWorkPlaceLevelSprite.texture = TextureDB.CommercialLevel[3];
                                                    break;
                                                default:
                                                    CitizenWorkPlaceLevelSprite.texture = TextureDB.CommercialLevel[1];
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            CitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                    else
                                    {
                                        if (WorkInfo.m_class.m_service == ItemClass.Service.Industrial)
                                        {
                                            WorkingPlaceButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            WorkingPlaceButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
                                            ItemClass.SubService subService = WorkInfo.m_class.m_subService;
                                            ItemClass.SubService subService2 = subService;
                                            switch (subService2)
                                            {
                                                case ItemClass.SubService.IndustrialForestry:
                                                    WorkingPlaceSprite.texture = null;
                                                    WorkingPlaceSpriteButton.normalBgSprite = "ResourceForestry";
                                                    break;
                                                case ItemClass.SubService.IndustrialFarming:
                                                    WorkingPlaceSprite.texture = null;
                                                    WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyFarming";
                                                    break;
                                                case ItemClass.SubService.IndustrialOil:
                                                    WorkingPlaceSprite.texture = null;
                                                    WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyOil";
                                                    break;
                                                case ItemClass.SubService.IndustrialOre:
                                                    WorkingPlaceSprite.texture = null;
                                                    WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyOre";
                                                    break;
                                                default:
                                                    switch (subService2)
                                                    {
                                                        case ItemClass.SubService.PlayerIndustryForestry:
                                                            WorkingPlaceSprite.texture = null;
                                                            WorkingPlaceSpriteButton.normalBgSprite = "ResourceForestry";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryFarming:
                                                            WorkingPlaceSprite.texture = null;
                                                            WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyFarming";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOil:
                                                            WorkingPlaceSprite.texture = null;
                                                            WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyOil";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOre:
                                                            WorkingPlaceSprite.texture = null;
                                                            WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyOre";
                                                            break;
                                                        default:
                                                            WorkingPlaceSprite.texture = TextureDB.CitizenIndustrialGenericTexture;
                                                            WorkingPlaceSpriteButton.normalBgSprite = null;
                                                            break;
                                                    }
                                                    break;
                                            }
                                            if (WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                                            {
                                                switch (WorkInfo.m_class.m_level)
                                                {
                                                    case ItemClass.Level.Level2:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.IndustrialLevel[2];
                                                        break;
                                                    case ItemClass.Level.Level3:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.IndustrialLevel[3];
                                                        break;
                                                    default:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.IndustrialLevel[1];
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                        else
                                        {
                                            if (WorkInfo.m_class.m_service == ItemClass.Service.Office)
                                            {
                                                WorkingPlaceSpriteButton.normalBgSprite = null;
                                                WorkingPlaceButton.textColor = new Color32(0, 204, byte.MaxValue, 128);
                                                WorkingPlaceSprite.texture = TextureDB.CitizenOfficeTexture;
                                                ItemClass.SubService subService3 = WorkInfo.m_class.m_subService;
                                                ItemClass.SubService subService4 = subService3;
                                                if (subService4 != ItemClass.SubService.OfficeHightech)
                                                {
                                                    WorkingPlaceButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office");
                                                }
                                                else
                                                {
                                                    WorkingPlaceButton.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office") + " Eco";
                                                }

                                                switch (WorkInfo.m_class.m_level)
                                                {
                                                    case ItemClass.Level.Level2:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.OfficeLevel[2];
                                                        break;
                                                    case ItemClass.Level.Level3:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.OfficeLevel[3];
                                                        break;
                                                    default:
                                                        CitizenWorkPlaceLevelSprite.texture = TextureDB.OfficeLevel[1];
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                                WorkingPlaceButton.textColor = new Color32(153, 102, 51, 20);
                                                switch (WorkInfo.m_class.m_service)
                                                {
                                                    case ItemClass.Service.Electricity:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyPowerSaving";
                                                        WorkingPlaceButton.tooltip = Translations.Translate("Electricity_job");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Water:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyWaterSaving";
                                                        WorkingPlaceButton.tooltip = Translations.Translate("Water_job");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Beautification:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "SubBarBeautificationParksnPlazas";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Beautification");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Garbage:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyRecycling";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Garbage");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.HealthCare:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "ToolbarIconHealthcareFocused";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Healthcare");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.PoliceDepartment:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "ToolbarIconPolice";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Police");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Education:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "InfoIconEducationPressed";
                                                        WorkingPlaceButton.textColor = new Color32(0, 102, 51, 100);
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Monument:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "FeatureMonumentLevel6";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Monuments");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.FireDepartment:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "InfoIconFireSafety";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "FireDepartment");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.PublicTransport:
                                                        {
                                                            ItemClass.SubService subService5 = WorkInfo.m_class.m_subService;
                                                            ItemClass.SubService subService6 = subService5;
                                                            if (subService6 != ItemClass.SubService.PublicTransportPost)
                                                            {
                                                                WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyFreePublicTransport";
                                                                WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                                            }
                                                            else
                                                            {
                                                                WorkingPlaceSpriteButton.normalBgSprite = "SubBarPublicTransportPost";
                                                                WorkingPlaceButton.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                                            }
                                                            goto IL_1D8D;
                                                        }
                                                    case ItemClass.Service.Disaster:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "SubBarFireDepartmentDisaster";
                                                        WorkingPlaceButton.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Museums:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "SubBarCampusAreaMuseums";
                                                        WorkingPlaceButton.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.VarsitySports:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                                        goto IL_1D8D;
                                                    case ItemClass.Service.Fishing:
                                                        WorkingPlaceSpriteButton.normalBgSprite = "SubBarIndustryFishing";
                                                        WorkingPlaceButton.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                                        goto IL_1D8D;
                                                }
                                                WorkingPlaceButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                WorkingPlaceSpriteButton.normalBgSprite = "IconPolicyNone";
                                                WorkingPlaceButton.tooltip = null;
                                            IL_1D8D:
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                    }
                                    WorkDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[WorkPlaceID.Index].m_position);
                                    if (WorkDistrict == 0)
                                    {
                                        WorkingPlaceSprite.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        WorkingPlaceSprite.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(WorkDistrict);
                                    }
                                }
                                else
                                {
                                    WorkingPlaceButton.isEnabled = false;
                                    CitizenWorkPlaceLevelSprite.texture = null;
                                    WorkingPlaceSpriteButton.tooltip = null;
                                    WorkingPlaceSprite.tooltip = null;
                                }
                                InstanceCitizenID = MyCitizen.m_citizens.m_buffer[MyInstanceID.Index].m_instance;
                                citizenInstance = MyCitizen.m_instances.m_buffer[InstanceCitizenID];
                                if (citizenInstance.m_targetBuilding > 0)
                                {
                                    CitizenVehicleId = MyCitizen.m_citizens.m_buffer[citizen].m_vehicle;
                                    MyVehicleID = InstanceID.Empty;
                                    GoingOutside = (MyBuilding.m_buildings.m_buffer[citizenInstance.m_targetBuilding].m_flags & (Building.Flags)192) > 0;
                                    if (CitizenVehicleId > 0)
                                    {
                                        MyVehicleID.Vehicle = CitizenVehicleId;
                                        LastActivityVehicleButton.isEnabled = true;
                                        VehInfo = MyVehicle.m_vehicles.m_buffer[CitizenVehicleId].Info;
                                        string text3 = PrefabCollection<VehicleInfo>.PrefabName((uint)VehInfo.m_prefabDataIndex);
                                        if (text3 == "Train Passenger")
                                        {
                                            CitizenVehicleName = Locale.Get("VEHICLE_TITLE", "Train Engine");
                                        }
                                        else
                                        {
                                            CitizenVehicleName = MyVehicle.GetVehicleName(CitizenVehicleId);
                                        }

                                        if (VehInfo.m_class.m_service == ItemClass.Service.Residential)
                                        {
                                            if (CitizenVehicleName.Like("Bicycle"))
                                            {
                                                LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                                LastActivityVehicleButton.normalBgSprite = "IconTouristBicycleVehicle";
                                                LastActivityVehicleButton.hoveredBgSprite = "IconTouristBicycleVehicle";
                                                LastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("PROPS_DESC", "bicycle01");
                                            }
                                            else
                                            {
                                                if (CitizenVehicleName.Like("Scooter"))
                                                {
                                                    LastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
                                                    LastActivityVehicleButton.normalBgSprite = "FavCimsIconScooter";
                                                    LastActivityVehicleButton.hoveredBgSprite = "FavCimsIconScooter";
                                                    LastActivityVehicleButton.tooltip = CitizenVehicleName;
                                                }
                                                else
                                                {
                                                    LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                                    LastActivityVehicleButton.normalBgSprite = "IconCitizenVehicle";
                                                    LastActivityVehicleButton.hoveredBgSprite = "IconTouristVehicle";
                                                    LastActivityVehicleButton.tooltip = CitizenVehicleName;
                                                }
                                            }
                                            if (VehInfo.m_vehicleAI.GetOwnerID(CitizenVehicleId, ref MyVehicle.m_vehicles.m_buffer[CitizenVehicleId]).Citizen == citizen)
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
                                            if (VehInfo.m_class.m_service == ItemClass.Service.PublicTransport)
                                            {
                                                LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
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
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Bus") + " - " + Locale.Get("SUBSERVICE_DESC", "Bus");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportMetro:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Metro") + " - " + Locale.Get("SUBSERVICE_DESC", "Metro");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTrain:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get("SUBSERVICE_DESC", "Train");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportShip:
                                                        {
                                                            if (CitizenVehicleName.Like("Ferry"))
                                                            {
                                                                LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
                                                                LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                                LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                                LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                                LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get("FEATURES_DESC", "Ferry");
                                                            }
                                                            else
                                                            {
                                                                LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
                                                                LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                                LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                                LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                                LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Ship");
                                                            }
                                                            break;
                                                        }
                                                    case ItemClass.SubService.PublicTransportPlane:
                                                        {
                                                            if (CitizenVehicleName.Like("Blimp"))
                                                            {
                                                                LastActivityVehicleButton.normalBgSprite = "IconPolicyEducationalBlimps";
                                                                LastActivityVehicleButton.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
                                                                LastActivityVehicleButton.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
                                                                LastActivityVehicleButton.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";
                                                                LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get("FEATURES_DESC", "Blimp");
                                                            }
                                                            else
                                                            {
                                                                LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
                                                                LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
                                                                LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
                                                                LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";
                                                                LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Plane");
                                                            }
                                                            break;
                                                        }
                                                    case ItemClass.SubService.PublicTransportTaxi:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTaxi";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get("SUBSERVICE_DESC", "Taxi");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTram:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTram";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTramHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTramFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTramPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Tram") + " - " + Locale.Get("SUBSERVICE_DESC", "Tram");
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
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMonorail";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get("SUBSERVICE_DESC", "Monorail");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportCableCar:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportCableCar";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
                                                        LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get("SUBSERVICE_DESC", "CableCar");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportTours:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTours";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportToursHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportToursFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportToursPressed";
                                                        LastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Tours");
                                                        break;
                                                    case ItemClass.SubService.PublicTransportPost:
                                                        LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPost";
                                                        LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPostHovered";
                                                        LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPostFocused";
                                                        LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPostPressed";
                                                        LastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Post");
                                                        break;
                                                    default:
                                                        if (subService8 == ItemClass.SubService.PublicTransportTrolleybus)
                                                        {
                                                            LastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrolleybus";
                                                            LastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrolleybusHovered";
                                                            LastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrolleybusFocused";
                                                            LastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrolleybusPressed";
                                                            LastActivityVehicleButton.tooltip = CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Trolleybus");
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                        bool goingOutside3 = GoingOutside;
                                        if (goingOutside3)
                                        {
                                            LeaveCity = true;
                                        }
                                        LastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
                                        LastActivityVehicleButton.isEnabled = false;
                                        LastActivityVehicleButton.tooltip = Translations.Translate("Vehicle_on_foot");
                                    }
                                }
                                else
                                {
                                    LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                    LastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
                                    LastActivityVehicleButton.isEnabled = false;
                                    LastActivityVehicleButton.tooltip = null;
                                }
                                CitizenStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[MyInstanceID.Index], out MyTargetID);
                                CitizenTarget = MyBuilding.GetBuildingName(MyTargetID.Building, MyInstanceID);
                                LastActivityButton.text = CitizenStatus + " " + CitizenTarget;
                                if (!MyTargetID.IsEmpty)
                                {
                                    TargetDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[MyTargetID.Index].m_position);
                                    if (TargetDistrict == 0)
                                    {
                                        LastActivityButton.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        LastActivityButton.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(TargetDistrict);
                                    }
                                }
                                CitizenDistrict = MyDistrict.GetDistrict(citizenInstance.GetSmoothPosition(InstanceCitizenID));
                                if (MyCitizen.m_citizens.m_buffer[citizen].Arrested && MyCitizen.m_citizens.m_buffer[citizen].Criminal)
                                {
                                    HappyIconButton.atlas = MyAtlas.FavCimsAtlas;
                                    HappyIconButton.normalBgSprite = "FavCimsCrimeArrested";
                                    HappyIconButton.tooltip = Translations.Translate("Citizen_Arrested");
                                    if (MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Moving)
                                    {
                                        policeveh = MyCitizen.m_citizens.m_buffer[citizen].m_vehicle;
                                        if (policeveh > 0)
                                        {
                                            MyVehicleID.Vehicle = policeveh;
                                            LastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
                                            LastActivityVehicleButton.normalBgSprite = "FavCimsPoliceVehicle";
                                            LastActivityVehicleButton.isEnabled = true;
                                            LastActivityVehicleButton.playAudioEvents = true;
                                            LastActivityVehicleButton.tooltip = MyVehicle.GetVehicleName(policeveh) + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
                                            LastActivityButton.isEnabled = false;
                                            LastActivityButton.text = Translations.Translate("Transported_to_Prison");
                                        }
                                    }
                                    else
                                    {
                                        LastActivityButton.isEnabled = true;
                                        LastActivityButton.text = Translations.Translate("Jailed_into") + CitizenTarget;
                                        LastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                    }
                                }
                                else
                                {
                                    HappyIconButton.atlas = UIView.GetAView().defaultAtlas;
                                    HappyIconButton.normalBgSprite = CitizenRowData["happiness_icon"];
                                    HappyIconButton.tooltip = Translations.Translate(CitizenRowData["happiness_icon"]);
                                }
                                bool citizenIsDead = CitizenIsDead;
                                if (citizenIsDead)
                                {
                                    HappyIconButton.normalBgSprite = "NotificationIconDead";
                                    HappyIconButton.tooltip = Translations.Translate("People_Life_Status_Dead");
                                    if (CitizenRowData["deathrealage"] == "0")
                                    {
                                        CitizenRowData["deathrealage"] = RealAge.ToString();
                                    }
                                    RealAgeButton.text = CitizenRowData["deathrealage"];
                                    if (DeathDate == null)
                                    {
                                        DeathDate = GameTime.FavCimsDate(Translations.Translate("time_format"), "n/a");
                                        DeathTime = GameTime.FavCimsTime();
                                    }
                                    CitizenNameButton.tooltip = string.Concat(new string[]
                                    {
                                        Translations.Translate("People_Life_Status_Dead"),
                                        " ",
                                        Translations.Translate("People_Life_Status_Dead_date"),
                                        " ",
                                        DeathDate,
                                        " ",
                                        Translations.Translate("People_Life_Status_Dead_time"),
                                        " ",
                                        DeathTime
                                    });
                                    if (MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Moving)
                                    {
                                        hearse = MyCitizen.m_citizens.m_buffer[citizen].m_vehicle;
                                        if (hearse > 0)
                                        {
                                            CitizenDead.Citizen = citizen;
                                            MyVehicleID.Vehicle = hearse;
                                            LastActivityVehicleButton.normalBgSprite = "NotificationIconVerySick";
                                            LastActivityVehicleButton.isEnabled = true;
                                            LastActivityVehicleButton.playAudioEvents = true;
                                            LastActivityVehicleButton.tooltip = Locale.Get("VEHICLE_TITLE", "Hearse");
                                            LastActivityButton.text = Translations.Translate("Citizen_on_hearse");
                                        }
                                    }
                                    else
                                    {
                                        if (MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation != Citizen.Location.Moving)
                                        {
                                            LastActivityButton.text = Translations.Translate("Citizen_wait_hearse");
                                            LastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
                                            LastActivityVehicleButton.isEnabled = false;
                                        }
                                        else
                                        {
                                            LastActivityButton.text = Translations.Translate("Citizen_hisfuneral");
                                            LastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
                                            LastActivityVehicleButton.isEnabled = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (rowLang == null || rowLang != Translations.CurrentLanguage)
                                {
                                    DeadOrGone = false;
                                }
                                if (!DeadOrGone)
                                {
                                    rowLang = Translations.CurrentLanguage;
                                    DeadOrGone = true;
                                    if (CitizenSingleRowPanel != null && FavCimsCore.RowID.ContainsKey(citizenINT) && MyInstancedName.Length > 0)
                                    {
                                        if (DeathDate != null)
                                        {
                                            DeathDate = GameTime.FavCimsDate(Translations.Translate("time_format"), DeathDate);
                                        }
                                        if (DeathDate == null)
                                        {
                                            DeathDate = GameTime.FavCimsDate(Translations.Translate("time_format"), "n/a");
                                            DeathTime = GameTime.FavCimsTime();
                                        }
                                        CitizenNameButton.disabledTextColor = new Color32(51, 51, 51, 160);
                                        CitizenNameButton.isEnabled = false;
                                        EducationButton.textColor = new Color32(51, 51, 51, 160);
                                        RealAgeButton.textColor = new Color32(51, 51, 51, 160);
                                        AgePhaseButton.textColor = new Color32(51, 51, 51, 160);
                                        System.Random random = new System.Random();
                                        int num3 = random.Next(0, 100);
                                        string text4;
                                        if (RealAge >= 85)
                                        {
                                            if (num3 >= 99)
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
                                            if (RealAge >= 65)
                                            {
                                                if (num3 >= 70)
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
                                                if (RealAge >= 45)
                                                {
                                                    if (num3 >= 50)
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
                                                    if (RealAge >= 20)
                                                    {
                                                        if (num3 >= 30)
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
                                                        if (num3 >= 2)
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
                                        if (!LeaveCity && (CitizenIsDead || text4 == "dead"))
                                        {
                                            try
                                            {
                                                HappyIconButton.normalBgSprite = "NotificationIconDead";
                                                HappyIconButton.tooltip = Translations.Translate("People_Life_Status_Dead");
                                                CitizenNameButton.text = MyInstancedName;
                                                CitizenNameButton.tooltip = string.Concat(
                                                [
                                                    Translations.Translate("People_Life_Status_Dead"),
                                                    " ",
                                                    Translations.Translate("People_Life_Status_Dead_date"),
                                                    " ",
                                                    DeathDate,
                                                    " ",
                                                    Translations.Translate("People_Life_Status_Dead_time"),
                                                    " ",
                                                    DeathTime
                                                ]);
                                                OtherInfoButton.isEnabled = false;
                                                AgePhaseButton.text = Translations.Translate("AgePhaseDead_" + CitizenRowData["gender"]);
                                                CitizenHomeButton.text = Translations.Translate("Home_Location_Dead");
                                                CitizenHomeSpriteButton.normalBgSprite = "houseofthedead";
                                                CitizenHomeButton.tooltip = null;
                                                CitizenHomeButton.isEnabled = false;
                                                CitizenResidentialLevelSprite.texture = null;
                                                CitizenHomeSpriteButton.tooltip = null;
                                                WorkingPlaceButton.isEnabled = false;
                                                WorkingPlaceButton.tooltip = null;
                                                CitizenWorkPlaceLevelSprite.texture = null;
                                                WorkingPlaceSprite.tooltip = null;
                                                WorkingPlaceButton.tooltip = null;
                                                LastActivityButton.isEnabled = false;
                                                LastActivityVehicleButton.isEnabled = false;
                                                LastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
                                                LastActivityVehicleButton.tooltip = null;
                                                LastActivityButton.tooltip = null;
                                                LastActivityButton.text = Translations.Translate("Citizen_buried");
                                                CitizenRowData.Clear();
                                            }
                                            catch (Exception ex)
                                            {
                                                Utils.Debug.Error("error " + ex.ToString());
                                            }
                                        }
                                        else
                                        {
                                            HappyIconButton.normalBgSprite = "";
                                            HappyIconButton.tooltip = null;
                                            HappySprite.texture = TextureDB.HappyOverride_texture;
                                            HappySprite.relativePosition = new Vector3(0f, 0f);
                                            HappySprite.tooltip = Translations.Translate("People_Life_Status_IsGone");
                                            CitizenNameButton.text = MyInstancedName;
                                            CitizenNameButton.tooltip = string.Concat(
                                            [
                                                Translations.Translate("People_Life_Status_IsGone"),
                                                " ",
                                                Translations.Translate("People_Life_Status_Dead_date"),
                                                " ",
                                                DeathDate,
                                                " ",
                                                Translations.Translate("People_Life_Status_Dead_time"),
                                                " ",
                                                DeathTime
                                            ]);
                                            OtherInfoButton.isEnabled = false;
                                            CitizenHomeButton.text = Translations.Translate("HomeOutsideTheCity");
                                            CitizenHomeSpriteButton.normalBgSprite = "homelessIcon";
                                            CitizenHomeButton.tooltip = null;
                                            CitizenHomeButton.isEnabled = false;
                                            CitizenResidentialLevelSprite.texture = null;
                                            CitizenHomeSpriteButton.tooltip = null;
                                            WorkingPlaceButton.isEnabled = false;
                                            WorkingPlaceButton.tooltip = null;
                                            CitizenWorkPlaceLevelSprite.texture = null;
                                            LastActivityButton.isEnabled = false;
                                            LastActivityVehicleButton.isEnabled = false;
                                            LastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
                                            LastActivityButton.tooltip = null;
                                            CitizenRowData.Clear();
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (MyFamily != null)
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
