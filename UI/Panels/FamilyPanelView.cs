using System;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public enum FamilyPanelTarget
    {
        CitizenHomeID,
        MyInstanceID,
        MyPetID,
        MyTargetID,
        MyVehicleID,
        PersonalVehicleID,
        WorkPlaceID
    }

    public sealed partial class FamilyPanelView : IDisposable
    {
        private bool buildAttempted;

        private InstanceID FamilyVehicleID;

        private Action adaptiveFamilyLayout;

        private UIScrollablePanel adaptiveFamilyScroll;
        private MouseEventHandler adaptiveFamilyWheelHandler;

        public bool IsReady { get; private set; }

        public event Action<FamilyPanelTarget, UIMouseEventParameter> CitizenRequested;
        public event Action<FamilyPanelTarget, UIMouseEventParameter> InstanceRequested;
        public event Action CloseRequested;
        public event MouseEventHandler DragStarted;
        public event MouseEventHandler DragEnded;

        public event Action<InstanceID, UIMouseEventParameter> HouseholdCitizenRequested;

        public event Action<InstanceID, UIMouseEventParameter> HouseholdInstanceRequested;

        public event Action<InstanceID> HouseholdFavoriteRequested;

        internal UITextureSprite BubbleHeaderIconSprite { get; private set; }

        internal UIButton BubbleHeaderCitizenName { get; private set; }

        internal UIButton BubbleRow1HappyIcon { get; private set; }

        internal UIButton BubbleCitizenAge { get; private set; }

        internal UIButton BubbleCitizenAgePhase { get; private set; }

        internal UIButton BubbleCitizenEducation { get; private set; }

        internal UIButton BubbleEduLevel1 { get; private set; }

        internal UIButton BubbleEduLevel2 { get; private set; }

        internal UIButton BubbleEduLevel3 { get; private set; }

        internal UIPanel BubbleRow1EducationTooltipArea { get; private set; }

        internal UIButton BubbleWealthSprite { get; private set; }

        internal UIButton BubbleHealthSprite { get; private set; }

        internal UIButton BubbleHealthValue { get; private set; }

        internal UIButton BubbleCitizenAgeVal { get; private set; }

        internal UIButton BubbleCitizenAgePhaseVal { get; private set; }

        internal UIButton BubbleRow2WealthValueVal { get; private set; }

        internal UIButton BubbleRow2WellbeingIcon { get; private set; }

        internal UIButton FavCimsWorkingPlace { get; private set; }

        private UIPanel BubbleWorkIconPanel { get; set; }

        internal UITextureSprite FavCimsWorkingPlaceSprite { get; private set; }

        internal UIButton FavCimsWorkingPlaceButtonGamDefImg { get; private set; }

        internal UITextureSprite FavCimsCitizenWorkPlaceLevelSprite { get; private set; }

        internal UIButton FavCimsLastActivityVehicleButton { get; private set; }

        internal UIButton FavCimsLastActivity { get; private set; }

        internal UIButton FavCimsDistrictValue { get; private set; }

        internal UITextureSprite BubbleDetailsBgSprite { get; private set; }

        internal UITextureSprite BubbleHomeIcon { get; private set; }

        internal UITextureSprite BubbleHomeLevel { get; private set; }

        internal UIButton BubbleHomeName { get; private set; }

        private UIPanel BubbleDetailsIconsPanel { get; set; }

        internal UIButton BubbleDetailsLandValue { get; private set; }

        internal UIButton BubbleDetailsCrime { get; private set; }

        internal UIButton BubbleDetailsNoise { get; private set; }

        internal UIButton BubbleDetailsWater { get; private set; }

        internal UIButton BubbleDetailsElettricity { get; private set; }

        internal UIButton BubbleDetailsGarbage { get; private set; }

        internal UIButton BubbleDetailsDeath { get; private set; }

        internal UIButton BubbleDetailsFire { get; private set; }

        internal UIButton BubbleDetailsPollution { get; private set; }

        private UITextureSprite BubbleFamilyBarCarButton { get; set; }

        internal UITextureSprite BubblePersonalCarButton { get; private set; }

        private UITextureSprite BubbleFamilyBarDogButton { get; set; }

        private UIButton FavCimsDistrictLabel { get; set; }

        private UILabel BubbleFamilyBarLabel { get; set; }

        public void Build(UIPanel host)
        {
            if (IsReady) return;
            if (buildAttempted) throw new InvalidOperationException("FamilyPanelView build already attempted.");
            if (host == null) throw new ArgumentNullException("host");
            buildAttempted = true;
            
            UITextureAtlas favCimsAtlas = MyAtlas.FavCimsAtlas;
            host.width = 250f;
            host.height = 500f;
            host.clipChildren = true;
            var FavCimsOtherInfoSprite = host.AddUIComponent<UITextureSprite>();
            FavCimsOtherInfoSprite.name = "FavCimsOtherInfoSprite";
            FavCimsOtherInfoSprite.texture = TextureDB.FavCimsOtherInfoTexture;
            FavCimsOtherInfoSprite.width = host.width;
            FavCimsOtherInfoSprite.height = host.height;
            FavCimsOtherInfoSprite.relativePosition = Vector3.zero;
            var BubbleHeaderPanel = host.AddUIComponent<UIPanel>();
            BubbleHeaderPanel.name = "BubbleHeaderPanel";
            BubbleHeaderPanel.width = 250f;
            BubbleHeaderPanel.height = 41f;
            BubbleHeaderPanel.relativePosition = new Vector3(0f, 0f);
            BubbleHeaderIconSprite = BubbleHeaderPanel.AddUIComponent<UITextureSprite>();
            BubbleHeaderIconSprite.name = "BubbleHeaderIconSprite";
            BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            BubbleHeaderIconSprite.relativePosition = new Vector3(9f, BubbleHeaderPanel.relativePosition.y + 9f);
            BubbleHeaderCitizenName = BubbleHeaderPanel.AddUIComponent<UIButton>();
            BubbleHeaderCitizenName.name = "BubbleHeaderCitizenName";
            BubbleHeaderCitizenName.width = BubbleHeaderPanel.width;
            BubbleHeaderCitizenName.height = BubbleHeaderPanel.height;
            BubbleHeaderCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleHeaderCitizenName.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleHeaderCitizenName.playAudioEvents = false;
            BubbleHeaderCitizenName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleHeaderCitizenName.font.size = 15;
            BubbleHeaderCitizenName.textScale = 1f;
            BubbleHeaderCitizenName.wordWrap = true;
            BubbleHeaderCitizenName.textPadding.left = 5;
            BubbleHeaderCitizenName.textPadding.right = 5;
            BubbleHeaderCitizenName.textColor = new Color32(204, 204, 51, 40);
            BubbleHeaderCitizenName.hoveredTextColor = new Color32(204, 204, 51, 40);
            BubbleHeaderCitizenName.pressedTextColor = new Color32(204, 204, 51, 40);
            BubbleHeaderCitizenName.focusedTextColor = new Color32(204, 204, 51, 40);
            BubbleHeaderCitizenName.useDropShadow = true;
            BubbleHeaderCitizenName.dropShadowOffset = new Vector2(1f, -1f);
            BubbleHeaderCitizenName.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleHeaderCitizenName.relativePosition = Vector3.zero;
            BubbleHeaderCitizenName.eventMouseDown += (component, param) => DragStarted?.Invoke(component, param);
            BubbleHeaderCitizenName.eventMouseUp += (component, param) => DragEnded?.Invoke(component, param);
            var BubbleCloseButton = host.AddUIComponent<UIButton>();
            BubbleCloseButton.name = "BubbleCloseButton";
            BubbleCloseButton.width = 26f;
            BubbleCloseButton.height = 26f;
            BubbleCloseButton.normalBgSprite = "buttonclose";
            BubbleCloseButton.hoveredBgSprite = "buttonclosehovered";
            BubbleCloseButton.pressedBgSprite = "buttonclosepressed";
            BubbleCloseButton.opacity = 0.9f;
            BubbleCloseButton.playAudioEvents = true;
            BubbleCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleCloseButton.eventClick += (component, param) => CloseRequested?.Invoke();
            BubbleCloseButton.relativePosition = new Vector3(BubbleHeaderPanel.width - 36f, 7f);
            var BubbleFamilyPortraitPanel = host.AddUIComponent<UIPanel>();
            BubbleFamilyPortraitPanel.name = "BubbleFamilyPortraitPanel";
            BubbleFamilyPortraitPanel.width = 242f;
            BubbleFamilyPortraitPanel.height = 156f;
            BubbleFamilyPortraitPanel.relativePosition = new Vector3(4f, BubbleHeaderPanel.relativePosition.y + BubbleHeaderPanel.height);
            var BubbleFamPortBgSpriteBack = BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            BubbleFamPortBgSpriteBack.name = "BubbleFamPortBgSpriteBack";
            BubbleFamPortBgSpriteBack.texture = TextureDB.BubbleFamPortBgSpriteBackTexture;
            BubbleFamPortBgSpriteBack.relativePosition = new Vector3(4f, 4f);
            var BubbleFamPortBgSprite = BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            BubbleFamPortBgSprite.name = "BubbleFamPortBgSprite";
            BubbleFamPortBgSprite.texture = TextureDB.BubbleFamPortBgSpriteTexture;
            BubbleFamPortBgSprite.relativePosition = Vector3.zero;
            var BubbleRow1Panel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleRow1Panel.name = "BubbleRow1Panel";
            BubbleRow1Panel.width = 234f;
            BubbleRow1Panel.height = 36f;
            BubbleRow1Panel.relativePosition = new Vector3(4f, 4f);
            var BubbleRow1HappyPanel = BubbleRow1Panel.AddUIComponent<UIPanel>();
            BubbleRow1HappyPanel.name = "BubbleRow1Panel";
            BubbleRow1HappyPanel.width = 36f;
            BubbleRow1HappyPanel.height = 36f;
            BubbleRow1HappyPanel.relativePosition = Vector3.zero;
            BubbleRow1HappyIcon = BubbleRow1HappyPanel.AddUIComponent<UIButton>();
            BubbleRow1HappyIcon.width = 26f;
            BubbleRow1HappyIcon.height = 26f;
            BubbleRow1HappyIcon.isEnabled = false;
            BubbleRow1HappyIcon.playAudioEvents = false;
            BubbleRow1HappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleRow1HappyIcon.relativePosition = new Vector3(4f, 5f);
            BubbleRow2WellbeingIcon = BubbleRow1HappyPanel.AddUIComponent<UIButton>();
            BubbleRow2WellbeingIcon.width = 11f;
            BubbleRow2WellbeingIcon.height = 11f;
            BubbleRow2WellbeingIcon.isEnabled = false;
            BubbleRow2WellbeingIcon.playAudioEvents = false;
            BubbleRow2WellbeingIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleRow2WellbeingIcon.relativePosition = new Vector3(24f, 5f);
            var BubbleRow1TextPanel = BubbleRow1Panel.AddUIComponent<UIPanel>();
            BubbleRow1TextPanel.name = "BubbleRow1TextPanel";
            BubbleRow1TextPanel.width = 198f;
            BubbleRow1TextPanel.height = 37f;
            BubbleRow1TextPanel.relativePosition = new Vector3(36f, 0f);
            var BubbleRow1LabelsSprite = BubbleRow1TextPanel.AddUIComponent<UITextureSprite>();
            BubbleRow1LabelsSprite.name = "BubbleRow1LabelsSprite";
            BubbleRow1LabelsSprite.width = 198f;
            BubbleRow1LabelsSprite.height = 34f;
            BubbleRow1LabelsSprite.texture = TextureDB.BubbleBgBar1Big;
            BubbleRow1LabelsSprite.relativePosition = new Vector3(0f, 3f);
            var BubbleRow1AgeLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            BubbleRow1AgeLabelPanel.name = "BubbleRow1AgeLabelPanel";
            BubbleRow1AgeLabelPanel.width = 32f;
            BubbleRow1AgeLabelPanel.height = 17f;
            BubbleRow1AgeLabelPanel.relativePosition = Vector3.zero;
            BubbleCitizenAge = BubbleRow1AgeLabelPanel.AddUIComponent<UIButton>();
            BubbleCitizenAge.name = "BubbleCitizenAge";
            BubbleCitizenAge.width = BubbleRow1AgeLabelPanel.width;
            BubbleCitizenAge.height = BubbleRow1AgeLabelPanel.height;
            BubbleCitizenAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleCitizenAge.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleCitizenAge.font.size = 15;
            BubbleCitizenAge.textScale = 0.8f;
            BubbleCitizenAge.outlineColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAge.outlineSize = 1;
            BubbleCitizenAge.textColor = new Color32(0, 51, 102, 140);
            BubbleCitizenAge.isInteractive = false;
            BubbleCitizenAge.useDropShadow = true;
            BubbleCitizenAge.dropShadowOffset = new Vector2(1f, -1f);
            BubbleCitizenAge.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAge.relativePosition = new Vector3(0f, 1f);
            var BubbleRow1AgePhaseLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            BubbleRow1AgePhaseLabelPanel.name = "BubbleRow1AgePhaseLabelPanel";
            BubbleRow1AgePhaseLabelPanel.width = 100f;
            BubbleRow1AgePhaseLabelPanel.height = 17f;
            BubbleRow1AgePhaseLabelPanel.relativePosition = new Vector3(32f, 0f);
            BubbleCitizenAgePhase = BubbleRow1AgePhaseLabelPanel.AddUIComponent<UIButton>();
            BubbleCitizenAgePhase.name = "BubbleCitizenAgePhase";
            BubbleCitizenAgePhase.width = BubbleRow1AgePhaseLabelPanel.width;
            BubbleCitizenAgePhase.height = BubbleRow1AgePhaseLabelPanel.height;
            BubbleCitizenAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleCitizenAgePhase.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleCitizenAgePhase.font.size = 15;
            BubbleCitizenAgePhase.textScale = 0.8f;
            BubbleCitizenAgePhase.outlineColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgePhase.outlineSize = 1;
            BubbleCitizenAgePhase.textColor = new Color32(0, 51, 102, 140);
            BubbleCitizenAgePhase.isInteractive = false;
            BubbleCitizenAgePhase.useDropShadow = true;
            BubbleCitizenAgePhase.dropShadowOffset = new Vector2(1f, -1f);
            BubbleCitizenAgePhase.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgePhase.relativePosition = new Vector3(0f, 1f);
            var BubbleRow1EducationLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            BubbleRow1EducationLabelPanel.name = "BubbleRow1LabelsPanel";
            BubbleRow1EducationLabelPanel.width = 66f;
            BubbleRow1EducationLabelPanel.height = 17f;
            BubbleRow1EducationLabelPanel.relativePosition = new Vector3(132f, 0f);
            BubbleCitizenEducation = BubbleRow1EducationLabelPanel.AddUIComponent<UIButton>();
            BubbleCitizenEducation.name = "BubbleCitizenEducation";
            BubbleCitizenEducation.width = BubbleRow1EducationLabelPanel.width;
            BubbleCitizenEducation.height = BubbleRow1EducationLabelPanel.height;
            BubbleCitizenEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleCitizenEducation.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleCitizenEducation.font.size = 15;
            BubbleCitizenEducation.textScale = 0.8f;
            BubbleCitizenEducation.outlineColor = new Color32(0, 0, 0, 0);
            BubbleCitizenEducation.outlineSize = 1;
            BubbleCitizenEducation.textColor = new Color32(0, 51, 102, 140);
            BubbleCitizenEducation.isInteractive = false;
            BubbleCitizenEducation.useDropShadow = true;
            BubbleCitizenEducation.dropShadowOffset = new Vector2(1f, -1f);
            BubbleCitizenEducation.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleCitizenEducation.relativePosition = new Vector3(0f, 1f);
            var BubbleRow1ValuesPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            BubbleRow1ValuesPanel.name = "BubbleRow1ValuesPanel";
            BubbleRow1ValuesPanel.width = 198f;
            BubbleRow1ValuesPanel.height = 17f;
            BubbleRow1ValuesPanel.relativePosition = new Vector3(0f, 17f);
            var BubbleRow1AgeValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            BubbleRow1AgeValuePanel.name = "BubbleRow1AgeValuePanel";
            BubbleRow1AgeValuePanel.width = 32f;
            BubbleRow1AgeValuePanel.height = 17f;
            BubbleRow1AgeValuePanel.relativePosition = Vector3.zero;
            BubbleCitizenAgeVal = BubbleRow1AgeValuePanel.AddUIComponent<UIButton>();
            BubbleCitizenAgeVal.name = "BubbleCitizenAgeVal";
            BubbleCitizenAgeVal.width = BubbleRow1AgeValuePanel.width;
            BubbleCitizenAgeVal.height = BubbleRow1AgeValuePanel.height;
            BubbleCitizenAgeVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleCitizenAgeVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleCitizenAgeVal.font.size = 15;
            BubbleCitizenAgeVal.textScale = 0.85f;
            BubbleCitizenAgeVal.outlineColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgeVal.outlineSize = 1;
            BubbleCitizenAgeVal.textColor = new Color32(0, 51, 102, 140);
            BubbleCitizenAgeVal.isInteractive = false;
            BubbleCitizenAgeVal.useDropShadow = true;
            BubbleCitizenAgeVal.dropShadowOffset = new Vector2(1f, -1f);
            BubbleCitizenAgeVal.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgeVal.relativePosition = new Vector3(0f, 0f);
            var BubbleRow1AgePhaseValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            BubbleRow1AgePhaseValuePanel.name = "BubbleRow1AgePhaseValuePanel";
            BubbleRow1AgePhaseValuePanel.width = 100f;
            BubbleRow1AgePhaseValuePanel.height = 17f;
            BubbleRow1AgePhaseValuePanel.relativePosition = new Vector3(32f, 0f);
            BubbleCitizenAgePhaseVal = BubbleRow1AgePhaseValuePanel.AddUIComponent<UIButton>();
            BubbleCitizenAgePhaseVal.name = "BubbleCitizenAgePhaseVal";
            BubbleCitizenAgePhaseVal.width = BubbleRow1AgePhaseValuePanel.width;
            BubbleCitizenAgePhaseVal.height = BubbleRow1AgePhaseValuePanel.height;
            BubbleCitizenAgePhaseVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleCitizenAgePhaseVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleCitizenAgePhaseVal.font.size = 15;
            BubbleCitizenAgePhaseVal.textScale = 0.85f;
            BubbleCitizenAgePhaseVal.outlineColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgePhaseVal.outlineSize = 1;
            BubbleCitizenAgePhaseVal.textColor = new Color32(0, 51, 102, 140);
            BubbleCitizenAgePhaseVal.isInteractive = false;
            BubbleCitizenAgePhaseVal.useDropShadow = true;
            BubbleCitizenAgePhaseVal.dropShadowOffset = new Vector2(1f, -1f);
            BubbleCitizenAgePhaseVal.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleCitizenAgePhaseVal.relativePosition = new Vector3(0f, 0f);
            var BubbleRow1EducationValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            BubbleRow1EducationValuePanel.name = "BubbleRow1LabelsPanel";
            BubbleRow1EducationValuePanel.width = 66f;
            BubbleRow1EducationValuePanel.height = 17f;
            BubbleRow1EducationValuePanel.relativePosition = new Vector3(132f, 0f);
            BubbleEduLevel1 = BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            BubbleEduLevel1.width = 18f;
            BubbleEduLevel1.height = 17f;
            BubbleEduLevel1.normalBgSprite = "InfoIconEducation";
            BubbleEduLevel1.disabledBgSprite = "InfoIconEducationDisabled";
            BubbleEduLevel1.isEnabled = false;
            BubbleEduLevel1.playAudioEvents = false;
            BubbleEduLevel1.relativePosition = new Vector3(2f, 0f);
            BubbleEduLevel2 = BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            BubbleEduLevel2.width = BubbleEduLevel1.width;
            BubbleEduLevel2.height = BubbleEduLevel1.height;
            BubbleEduLevel2.normalBgSprite = "InfoIconEducation";
            BubbleEduLevel2.disabledBgSprite = "InfoIconEducationDisabled";
            BubbleEduLevel2.isEnabled = false;
            BubbleEduLevel2.playAudioEvents = false;
            BubbleEduLevel2.relativePosition = new Vector3(24f, 0f);
            BubbleEduLevel3 = BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            BubbleEduLevel3.width = BubbleEduLevel1.width;
            BubbleEduLevel3.height = BubbleEduLevel1.height;
            BubbleEduLevel3.normalBgSprite = "InfoIconEducation";
            BubbleEduLevel3.disabledBgSprite = "InfoIconEducationDisabled";
            BubbleEduLevel3.isEnabled = false;
            BubbleEduLevel3.playAudioEvents = false;
            BubbleEduLevel3.relativePosition = new Vector3(46f, 0f);
            BubbleRow1EducationTooltipArea = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            BubbleRow1EducationTooltipArea.name = "BubbleRow1EducationTooltipArea";
            BubbleRow1EducationTooltipArea.width = BubbleRow1EducationValuePanel.width;
            BubbleRow1EducationTooltipArea.height = BubbleRow1EducationValuePanel.height;
            BubbleRow1EducationTooltipArea.absolutePosition = BubbleRow1EducationValuePanel.absolutePosition;
            BubbleRow1EducationTooltipArea.tooltipBox = UIView.GetAView().defaultTooltipBox;
            var BubbleTargetPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleTargetPanel.name = "BubbleTargetPanel";
            BubbleTargetPanel.width = 58f;
            BubbleTargetPanel.height = 36f;
            BubbleTargetPanel.relativePosition = new Vector3(4f, 35f);
            var BubbleTargetIcon = BubbleTargetPanel.AddUIComponent<UIButton>();
            BubbleTargetIcon.width = 28f;
            BubbleTargetIcon.height = 28f;
            BubbleTargetIcon.normalBgSprite = "LocationMarkerNormal";
            BubbleTargetIcon.hoveredBgSprite = "LocationMarkerHovered";
            BubbleTargetIcon.focusedBgSprite = "LocationMarkerFocused";
            BubbleTargetIcon.pressedBgSprite = "LocationMarkerPressed";
            BubbleTargetIcon.disabledBgSprite = "LocationMarkerDisabled";
            BubbleTargetIcon.playAudioEvents = true;
            BubbleTargetIcon.relativePosition = new Vector3(4f, 0f);
            BubbleTargetIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleTargetIcon.tooltip = FamilyPanelText.CitizenNavigation;
            BubbleTargetIcon.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                CitizenRequested?.Invoke(FamilyPanelTarget.MyInstanceID, eventParam);
            };
            var BubbleRow2Panel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleRow2Panel.name = "BubbleRow2Panel";
            BubbleRow2Panel.width = 198f;
            BubbleRow2Panel.height = 34f;
            BubbleRow2Panel.relativePosition = new Vector3(40f, 44f);
            var BubbleWealthHealthSprite = BubbleRow2Panel.AddUIComponent<UITextureSprite>();
            BubbleWealthHealthSprite.name = "BubbleWealthHealthSprite";
            BubbleWealthHealthSprite.width = 198f;
            BubbleWealthHealthSprite.height = 34f;
            BubbleWealthHealthSprite.texture = TextureDB.BubbleBgBar1Big;
            BubbleWealthHealthSprite.relativePosition = Vector3.zero;
            var BubbleWealthSpritePanel = BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
            BubbleWealthSpritePanel.name = "BubbleWealthSpritePanel";
            BubbleWealthSpritePanel.width = 37f;
            BubbleWealthSpritePanel.height = 34f;
            BubbleWealthSpritePanel.relativePosition = new Vector3(0f, 0f);
            BubbleWealthSprite = BubbleWealthSpritePanel.AddUIComponent<UIButton>();
            BubbleWealthSprite.name = "BubbleWealthSprite";
            BubbleWealthSprite.width = 25f;
            BubbleWealthSprite.height = 25f;
            BubbleWealthSprite.normalBgSprite = "MoneyThumb";
            BubbleWealthSprite.playAudioEvents = false;
            BubbleWealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleWealthSprite.relativePosition = new Vector3(10f, 5f);
            BubbleRow2WealthValueVal = BubbleWealthHealthSprite.AddUIComponent<UIButton>();
            BubbleRow2WealthValueVal.name = "BubbleRow2WealthValueVal";
            BubbleRow2WealthValueVal.width = 70f;
            BubbleRow2WealthValueVal.height = 34f;
            BubbleRow2WealthValueVal.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleRow2WealthValueVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleRow2WealthValueVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleRow2WealthValueVal.textPadding.top = 1;
            BubbleRow2WealthValueVal.font.size = 15;
            BubbleRow2WealthValueVal.textScale = 0.8f;
            BubbleRow2WealthValueVal.outlineColor = new Color32(0, 0, 0, 0);
            BubbleRow2WealthValueVal.outlineSize = 1;
            BubbleRow2WealthValueVal.textColor = new Color32(0, 51, 102, 140);
            BubbleRow2WealthValueVal.isInteractive = false;
            BubbleRow2WealthValueVal.useDropShadow = true;
            BubbleRow2WealthValueVal.wordWrap = true;
            BubbleRow2WealthValueVal.dropShadowOffset = new Vector2(1f, -1f);
            BubbleRow2WealthValueVal.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleRow2WealthValueVal.relativePosition = new Vector3(37f, 0f);
            var BubbleHealthSpritePanel = BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
            BubbleHealthSpritePanel.name = "BubbleHealthSpritePanel";
            BubbleHealthSpritePanel.width = 26f;
            BubbleHealthSpritePanel.height = 34f;
            BubbleHealthSpritePanel.relativePosition = new Vector3(107f, 0f);
            BubbleHealthSprite = BubbleHealthSpritePanel.AddUIComponent<UIButton>();
            BubbleHealthSprite.name = "BubbleWealthSprite";
            BubbleHealthSprite.width = 26f;
            BubbleHealthSprite.height = 26f;
            BubbleHealthSprite.playAudioEvents = false;
            BubbleHealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleHealthSprite.relativePosition = new Vector3(0f, 4f);
            BubbleHealthValue = BubbleWealthHealthSprite.AddUIComponent<UIButton>();
            BubbleHealthValue.name = "BubbleHealthValue";
            BubbleHealthValue.width = 65f;
            BubbleHealthValue.height = 34f;
            BubbleHealthValue.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleHealthValue.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleHealthValue.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleHealthValue.textPadding.left = 5;
            BubbleHealthValue.textPadding.right = 5;
            BubbleHealthValue.textPadding.top = 1;
            BubbleHealthValue.font.size = 15;
            BubbleHealthValue.textScale = 0.85f;
            BubbleHealthValue.outlineColor = new Color32(0, 0, 0, 0);
            BubbleHealthValue.outlineSize = 1;
            BubbleHealthValue.textColor = new Color32(0, 51, 102, 140);
            BubbleHealthValue.isInteractive = false;
            BubbleHealthValue.useDropShadow = true;
            BubbleHealthValue.wordWrap = true;
            BubbleHealthValue.dropShadowOffset = new Vector2(1f, -1f);
            BubbleHealthValue.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleHealthValue.relativePosition = new Vector3(133f, 0f);
            var WorkBuildingPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            WorkBuildingPanel.name = "WorkBuildingPanel";
            WorkBuildingPanel.width = 234f;
            WorkBuildingPanel.height = 25f;
            WorkBuildingPanel.relativePosition = new Vector3(4f, 82f);
            var BubbleWorkBuildingSprite = WorkBuildingPanel.AddUIComponent<UITextureSprite>();
            BubbleWorkBuildingSprite.name = "BubbleWorkBuildingSprite";
            BubbleWorkBuildingSprite.width = WorkBuildingPanel.width;
            BubbleWorkBuildingSprite.height = WorkBuildingPanel.height;
            BubbleWorkBuildingSprite.texture = TextureDB.BubbleBg1Special;
            BubbleWorkBuildingSprite.relativePosition = Vector3.zero;
            BubbleWorkBuildingSprite.clipChildren = true;
            FavCimsWorkingPlace = BubbleWorkBuildingSprite.AddUIComponent<UIButton>();
            FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
            FavCimsWorkingPlace.width = BubbleWorkBuildingSprite.width;
            FavCimsWorkingPlace.height = BubbleWorkBuildingSprite.height;
            FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
            FavCimsWorkingPlace.textHorizontalAlignment = 0;
            FavCimsWorkingPlace.playAudioEvents = true;
            FavCimsWorkingPlace.font = UIDynamicFont.FindByName("OpenSans-Regular");
            FavCimsWorkingPlace.font.size = 15;
            FavCimsWorkingPlace.textScale = 0.85f;
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
            FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
            FavCimsWorkingPlace.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.WorkPlaceID, eventParam);
            };
            FavCimsWorkingPlace.relativePosition = new Vector3(0f, 1f);
            BubbleWorkIconPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleWorkIconPanel.name = "BubbleRow2Panel";
            BubbleWorkIconPanel.width = 36f;
            BubbleWorkIconPanel.height = 40f;
            BubbleWorkIconPanel.absolutePosition = new Vector3(BubbleFamPortBgSprite.absolutePosition.x + 4f, BubbleFamPortBgSprite.absolutePosition.y + 71f);
            FavCimsWorkingPlaceSprite = BubbleWorkIconPanel.AddUIComponent<UITextureSprite>();
            FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
            FavCimsWorkingPlaceSprite.width = 20f;
            FavCimsWorkingPlaceSprite.height = 40f;
            FavCimsWorkingPlaceSprite.relativePosition = new Vector3(9f, 3f);
            FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            FavCimsWorkingPlaceButtonGamDefImg = FavCimsWorkingPlaceSprite.AddUIComponent<UIButton>();
            FavCimsWorkingPlaceButtonGamDefImg.name = "FavCimsWorkingPlaceButtonGamDefImg";
            FavCimsWorkingPlaceButtonGamDefImg.width = 20f;
            FavCimsWorkingPlaceButtonGamDefImg.height = 20f;
            FavCimsWorkingPlaceButtonGamDefImg.relativePosition = new Vector3(0f, 10f);
            FavCimsWorkingPlaceButtonGamDefImg.isInteractive = false;
            FavCimsWorkingPlaceButtonGamDefImg.tooltipBox = UIView.GetAView().defaultTooltipBox;
            FavCimsCitizenWorkPlaceLevelSprite = FavCimsWorkingPlaceSprite.AddUIComponent<UITextureSprite>();
            FavCimsCitizenWorkPlaceLevelSprite.name = "FavCimsCitizenWorkPlaceLevelSprite";
            FavCimsCitizenWorkPlaceLevelSprite.relativePosition = new Vector3(0f, 0f);
            BubblePersonalCarButton = BubbleFamPortBgSprite.AddUIComponent<UITextureSprite>();
            BubblePersonalCarButton.name = "BubblePersonalCarButton";
            BubblePersonalCarButton.width = 30f;
            BubblePersonalCarButton.height = 20f;
            BubblePersonalCarButton.texture = TextureDB.BubbleCarDisabled;
            BubblePersonalCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubblePersonalCarButton.absolutePosition = new Vector3(BubbleTargetIcon.absolutePosition.x, BubbleTargetIcon.absolutePosition.y + BubbleTargetIcon.height);
            BubblePersonalCarButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.PersonalVehicleID, eventParam);
            };
            BubblePersonalCarButton.BringToFront();
            var BubbleActivityPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleActivityPanel.name = "BubbleActivityPanel";
            BubbleActivityPanel.width = 234f;
            BubbleActivityPanel.height = 18f;
            BubbleActivityPanel.relativePosition = new Vector3(4f, WorkBuildingPanel.relativePosition.y + 31f);
            var BubbleActivitySprite = BubbleActivityPanel.AddUIComponent<UITextureSprite>();
            BubbleActivitySprite.name = "BubbleActivitySprite";
            BubbleActivitySprite.width = BubbleActivityPanel.width;
            BubbleActivitySprite.height = BubbleActivityPanel.height;
            BubbleActivitySprite.texture = TextureDB.BubbleBg1Special2;
            BubbleActivitySprite.relativePosition = Vector3.zero;
            var BubbleActivityVehiclePanel = BubbleActivitySprite.AddUIComponent<UIPanel>();
            BubbleActivityVehiclePanel.name = "BubbleActivityVehiclePanel";
            BubbleActivityVehiclePanel.width = 234f;
            BubbleActivityVehiclePanel.height = 18f;
            BubbleActivityVehiclePanel.relativePosition = new Vector3(4f, 0f);
            FavCimsLastActivityVehicleButton = BubbleActivityVehiclePanel.AddUIComponent<UIButton>();
            FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
            FavCimsLastActivityVehicleButton.width = 18f;
            FavCimsLastActivityVehicleButton.height = 17f;
            FavCimsLastActivityVehicleButton.relativePosition = new Vector3(0f, 0f);
            FavCimsLastActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.MyVehicleID, eventParam);
            };
            FavCimsLastActivity = BubbleActivitySprite.AddUIComponent<UIButton>();
            FavCimsLastActivity.name = "FavCimsLastActivity";
            FavCimsLastActivity.width = BubbleActivitySprite.width - 27f;
            FavCimsLastActivity.height = BubbleActivitySprite.height;
            FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
            FavCimsLastActivity.textHorizontalAlignment = 0;
            FavCimsLastActivity.playAudioEvents = true;
            FavCimsLastActivity.font = UIDynamicFont.FindByName("OpenSans-Regular");
            FavCimsLastActivity.font.size = 15;
            FavCimsLastActivity.textScale = 0.75f;
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
            FavCimsLastActivity.maximumSize = new Vector2(BubbleActivitySprite.width - 40f, BubbleActivitySprite.height);
            FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
            FavCimsLastActivity.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.MyTargetID, eventParam);
            };
            FavCimsLastActivity.relativePosition = new Vector3(27f, 1f);
            var BubbleDistrictPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleDistrictPanel.name = "BubbleDistrictPanel";
            BubbleDistrictPanel.width = 234f;
            BubbleDistrictPanel.height = 15f;
            BubbleDistrictPanel.relativePosition = new Vector3(4f, BubbleActivityPanel.relativePosition.y + 21f);
            var BubbleDistrictSprite = BubbleDistrictPanel.AddUIComponent<UITextureSprite>();
            BubbleDistrictSprite.name = "BubbleDistrictSprite";
            BubbleDistrictSprite.width = BubbleDistrictPanel.width;
            BubbleDistrictSprite.height = BubbleDistrictPanel.height;
            BubbleDistrictSprite.texture = TextureDB.BubbleBg1Special2;
            BubbleDistrictSprite.relativePosition = Vector3.zero;
            FavCimsDistrictLabel = BubbleDistrictSprite.AddUIComponent<UIButton>();
            FavCimsDistrictLabel.name = "FavCimsDistrictLabel";
            FavCimsDistrictLabel.width = 60f;
            FavCimsDistrictLabel.height = 15f;
            FavCimsDistrictLabel.textVerticalAlignment = UIVerticalAlignment.Middle;
            FavCimsDistrictLabel.textHorizontalAlignment = UIHorizontalAlignment.Center;
            FavCimsDistrictLabel.playAudioEvents = true;
            FavCimsDistrictLabel.font = UIDynamicFont.FindByName("OpenSans-Regular");
            FavCimsDistrictLabel.font.size = 15;
            FavCimsDistrictLabel.textScale = 0.7f;
            FavCimsDistrictLabel.textPadding.left = 0;
            FavCimsDistrictLabel.textPadding.right = 5;
            FavCimsDistrictLabel.outlineColor = new Color32(0, 0, 0, 0);
            FavCimsDistrictLabel.outlineSize = 1;
            FavCimsDistrictLabel.textColor = new Color32(153, 0, 0, 0);
            FavCimsDistrictLabel.isInteractive = false;
            FavCimsDistrictLabel.useDropShadow = true;
            FavCimsDistrictLabel.dropShadowOffset = new Vector2(1f, -1f);
            FavCimsDistrictLabel.dropShadowColor = new Color32(0, 0, 0, 0);
            FavCimsDistrictLabel.relativePosition = new Vector3(4f, 1f);
            FavCimsDistrictValue = BubbleDistrictSprite.AddUIComponent<UIButton>();
            FavCimsDistrictValue.name = "FavCimsDistrictValue";
            FavCimsDistrictValue.width = BubbleDistrictPanel.width - 74f;
            FavCimsDistrictValue.height = 15f;
            FavCimsDistrictValue.textVerticalAlignment = UIVerticalAlignment.Middle;
            FavCimsDistrictValue.textHorizontalAlignment = 0;
            FavCimsDistrictValue.playAudioEvents = true;
            FavCimsDistrictValue.font = UIDynamicFont.FindByName("OpenSans-Regular");
            FavCimsDistrictValue.font.size = 15;
            FavCimsDistrictValue.textScale = 0.7f;
            FavCimsDistrictValue.textPadding.left = 0;
            FavCimsDistrictValue.textPadding.right = 5;
            FavCimsDistrictValue.outlineColor = new Color32(0, 0, 0, 0);
            FavCimsDistrictValue.outlineSize = 1;
            FavCimsDistrictValue.textColor = new Color32(21, 59, 96, 140);
            FavCimsDistrictValue.disabledTextColor = new Color32(21, 59, 96, 140);
            FavCimsDistrictValue.isEnabled = false;
            FavCimsDistrictValue.useDropShadow = true;
            FavCimsDistrictValue.dropShadowOffset = new Vector2(1f, -1f);
            FavCimsDistrictValue.dropShadowColor = new Color32(0, 0, 0, 0);
            FavCimsDistrictValue.relativePosition = new Vector3(64f, 1f);
            var BubbleDetailsPanel = host.AddUIComponent<UIPanel>();
            BubbleDetailsPanel.name = "BubbleDetailsPanel";
            BubbleDetailsPanel.width = 235f;
            BubbleDetailsPanel.height = 60f;
            BubbleDetailsPanel.relativePosition = new Vector3(7f, BubbleFamilyPortraitPanel.relativePosition.y + BubbleFamilyPortraitPanel.height + 1f);
            BubbleDetailsBgSprite = BubbleDetailsPanel.AddUIComponent<UITextureSprite>();
            BubbleDetailsBgSprite.name = "BubbleFamPortBgSprite";
            BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
            BubbleDetailsBgSprite.relativePosition = Vector3.zero;
            BubbleHomeIcon = BubbleDetailsPanel.AddUIComponent<UITextureSprite>();
            BubbleHomeIcon.name = "FavCimsCitizenHomeSprite";
            BubbleHomeIcon.relativePosition = new Vector3(10f, 10f);
            BubbleHomeIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleHomeLevel = BubbleHomeIcon.AddUIComponent<UITextureSprite>();
            BubbleHomeLevel.name = "FavCimsCitizenResidentialLevelSprite";
            BubbleHomeLevel.relativePosition = Vector3.zero;
            var BubbleHomePanel = BubbleDetailsPanel.AddUIComponent<UIPanel>();
            BubbleHomePanel.name = "BubbleHomePanel";
            BubbleHomePanel.width = 181f;
            BubbleHomePanel.height = 30f;
            BubbleHomePanel.maximumSize = new Vector2(181f, 40f);
            BubbleHomePanel.autoLayoutDirection = 0;
            BubbleHomePanel.autoLayout = true;
            BubbleHomePanel.relativePosition = new Vector3(40f, 4f);
            BubbleHomeName = BubbleHomePanel.AddUIComponent<UIButton>();
            BubbleHomeName.name = "BubbleHomeName";
            BubbleHomeName.width = BubbleHomePanel.width;
            BubbleHomeName.height = BubbleHomePanel.height;
            BubbleHomeName.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleHomeName.textHorizontalAlignment = 0;
            BubbleHomeName.playAudioEvents = true;
            BubbleHomeName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleHomeName.font.size = 15;
            BubbleHomeName.textScale = 0.9f;
            BubbleHomeName.wordWrap = true;
            BubbleHomeName.textPadding.left = 2;
            BubbleHomeName.textPadding.right = 5;
            BubbleHomeName.outlineColor = new Color32(0, 0, 0, 0);
            BubbleHomeName.outlineSize = 1;
            BubbleHomeName.textColor = new Color32(21, 59, 96, 140);
            BubbleHomeName.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleHomeName.pressedTextColor = new Color32(153, 0, 0, 0);
            BubbleHomeName.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleHomeName.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleHomeName.useDropShadow = true;
            BubbleHomeName.dropShadowOffset = new Vector2(1f, -1f);
            BubbleHomeName.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleHomeName.maximumSize = new Vector2(BubbleHomePanel.width, BubbleHomePanel.height);
            BubbleHomeName.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleHomeName.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.CitizenHomeID, eventParam);
            };
            BubbleHomeName.relativePosition = Vector3.zero;
            BubbleDetailsIconsPanel = BubbleDetailsPanel.AddUIComponent<UIPanel>();
            BubbleDetailsIconsPanel.name = "BubbleDetailsIconsPanel";
            BubbleDetailsIconsPanel.width = 181f;
            BubbleDetailsIconsPanel.height = 20f;
            BubbleDetailsIconsPanel.maximumSize = new Vector2(181f, 30f);
            BubbleDetailsIconsPanel.autoLayoutDirection = 0;
            BubbleDetailsIconsPanel.autoLayout = true;
            BubbleDetailsIconsPanel.relativePosition = new Vector3(BubbleHomePanel.relativePosition.x, 30f);
            BubbleDetailsElettricity = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsElettricity.name = "BubbleDetailsElettricity";
            BubbleDetailsElettricity.normalBgSprite = "ToolbarIconElectricity";
            BubbleDetailsElettricity.width = 20f;
            BubbleDetailsElettricity.height = 20f;
            BubbleDetailsElettricity.playAudioEvents = false;
            BubbleDetailsElettricity.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsWater = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsWater.name = "BubbleDetailsWater";
            BubbleDetailsWater.normalBgSprite = "IconPolicyWaterSaving";
            BubbleDetailsWater.width = BubbleDetailsElettricity.width;
            BubbleDetailsWater.height = BubbleDetailsElettricity.height;
            BubbleDetailsWater.playAudioEvents = false;
            BubbleDetailsWater.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsLandValue = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsLandValue.name = "BubbleDetailsLandValue";
            BubbleDetailsLandValue.normalBgSprite = "InfoIconLandValue";
            BubbleDetailsLandValue.width = BubbleDetailsElettricity.width;
            BubbleDetailsLandValue.height = BubbleDetailsElettricity.height;
            BubbleDetailsLandValue.playAudioEvents = false;
            BubbleDetailsLandValue.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsCrime = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsCrime.name = "BubbleDetailsCrime";
            BubbleDetailsCrime.normalBgSprite = "InfoIconCrime";
            BubbleDetailsCrime.width = BubbleDetailsElettricity.width;
            BubbleDetailsCrime.height = BubbleDetailsElettricity.height;
            BubbleDetailsCrime.playAudioEvents = false;
            BubbleDetailsCrime.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsNoise = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsNoise.name = "BubbleDetailsNoise";
            BubbleDetailsNoise.normalBgSprite = "InfoIconNoisePollution";
            BubbleDetailsNoise.width = BubbleDetailsElettricity.width;
            BubbleDetailsNoise.height = BubbleDetailsElettricity.height;
            BubbleDetailsNoise.playAudioEvents = false;
            BubbleDetailsNoise.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsGarbage = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsGarbage.name = "BubbleDetailsGarbage";
            BubbleDetailsGarbage.normalBgSprite = "InfoIconGarbage";
            BubbleDetailsGarbage.width = BubbleDetailsElettricity.width;
            BubbleDetailsGarbage.height = BubbleDetailsElettricity.height;
            BubbleDetailsGarbage.playAudioEvents = false;
            BubbleDetailsGarbage.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsDeath = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsDeath.name = "BubbleDetailsDeath";
            BubbleDetailsDeath.normalBgSprite = "NotificationIconVerySick";
            BubbleDetailsDeath.width = BubbleDetailsElettricity.width;
            BubbleDetailsDeath.height = BubbleDetailsElettricity.height;
            BubbleDetailsDeath.playAudioEvents = false;
            BubbleDetailsDeath.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsFire = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsFire.name = "BubbleDetailsFire";
            BubbleDetailsFire.normalBgSprite = "ToolbarIconFireDepartment";
            BubbleDetailsFire.width = BubbleDetailsElettricity.width;
            BubbleDetailsFire.height = BubbleDetailsElettricity.height;
            BubbleDetailsFire.playAudioEvents = false;
            BubbleDetailsFire.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleDetailsPollution = BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            BubbleDetailsPollution.name = "BubbleDetailsPollution";
            BubbleDetailsPollution.normalBgSprite = "InfoIconPollution";
            BubbleDetailsPollution.width = BubbleDetailsElettricity.width;
            BubbleDetailsPollution.height = BubbleDetailsElettricity.height;
            BubbleDetailsPollution.playAudioEvents = false;
            BubbleDetailsPollution.tooltipBox = UIView.GetAView().defaultTooltipBox;
            var BubbleFamilyBarPanel = host.AddUIComponent<UIPanel>();
            BubbleFamilyBarPanel.name = "BubbleFamilyBarPanel";
            BubbleFamilyBarPanel.width = 236f;
            BubbleFamilyBarPanel.height = 20f;
            BubbleFamilyBarPanel.relativePosition = new Vector3(7f, BubbleDetailsPanel.relativePosition.y + BubbleDetailsPanel.height + 2f);
            var BubbleFamilyBarPanelBg = BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            BubbleFamilyBarPanelBg.name = "BubbleFamilyBarPanelBg";
            BubbleFamilyBarPanelBg.width = BubbleFamilyBarPanel.width;
            BubbleFamilyBarPanelBg.height = BubbleFamilyBarPanel.height;
            BubbleFamilyBarPanelBg.texture = TextureDB.BubbleBgBarHover;
            BubbleFamilyBarPanelBg.relativePosition = Vector3.zero;
            BubbleFamilyBarLabel = BubbleFamilyBarPanel.AddUIComponent<UILabel>();
            BubbleFamilyBarLabel.name = "BubbleFamilyBarLabel";
            BubbleFamilyBarLabel.height = BubbleFamilyBarPanel.height;
            BubbleFamilyBarLabel.width = 221f;
            BubbleFamilyBarLabel.font.size = 11;
            BubbleFamilyBarLabel.textScale = 1f;
            BubbleFamilyBarLabel.textColor = new Color32(102, 0, 51, 220);
            BubbleFamilyBarLabel.relativePosition = new Vector3(7f, 2f);
            BubbleFamilyBarDogButton = BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            BubbleFamilyBarDogButton.name = "BubbleFamilyBarDogButton";
            BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
            BubbleFamilyBarDogButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleFamilyBarDogButton.relativePosition = new Vector3(175f, 0f);
            BubbleFamilyBarDogButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                InstanceRequested?.Invoke(FamilyPanelTarget.MyPetID, eventParam);
            };
            BubbleFamilyBarCarButton = BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            BubbleFamilyBarCarButton.name = "BubbleFamilyBarCarButton";
            BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
            BubbleFamilyBarCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleFamilyBarCarButton.relativePosition = new Vector3(BubbleFamilyBarDogButton.relativePosition.x + BubbleFamilyBarDogButton.width + 10f, 0f);
            BubbleFamilyBarCarButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter param)
            {
                HouseholdInstanceRequested?.Invoke(FamilyVehicleID, param);
            };
            var BubbleFamilyPanel = host.AddUIComponent<UIScrollablePanel>();
            BubbleFamilyPanel.name = "BubbleFamilyPanel";
            BubbleFamilyPanel.width = 224f;
            BubbleFamilyPanel.height = 212f;
            BubbleFamilyPanel.clipChildren = true;
            BubbleFamilyPanel.autoLayout = true;
            BubbleFamilyPanel.autoLayoutDirection = LayoutDirection.Vertical;
            BubbleFamilyPanel.relativePosition = new Vector3(7f, BubbleFamilyBarPanel.relativePosition.y + BubbleFamilyBarPanel.height);

            InitializeHouseholdRows(BubbleFamilyPanel);

            // Household scrollbar.
            adaptiveFamilyScroll = BubbleFamilyPanel;

            var familyScrollbar = host.AddUIComponent<UIScrollbar>();
            familyScrollbar.name = "FamilyMembersScrollbar";
            familyScrollbar.width = 10f;
            familyScrollbar.orientation = UIOrientation.Vertical;
            familyScrollbar.minValue = 0f;
            familyScrollbar.value = 0f;
            familyScrollbar.incrementAmount = 26f;

            var familyTrack = familyScrollbar.AddUIComponent<UISlicedSprite>();
            familyTrack.name = "FamilyMembersScrollbarTrack";
            familyTrack.atlas = UIView.GetAView().defaultAtlas;
            familyTrack.spriteName = "ScrollbarTrack";
            familyTrack.relativePosition = Vector3.zero;
            familyTrack.width = familyScrollbar.width;

            var familyThumb = familyScrollbar.AddUIComponent<UISlicedSprite>();
            familyThumb.name = "FamilyMembersScrollbarThumb";
            familyThumb.atlas = UIView.GetAView().defaultAtlas;
            familyThumb.spriteName = "ScrollbarThumb";
            familyThumb.relativePosition = Vector3.zero;
            familyThumb.width = familyScrollbar.width;
            familyThumb.height = 20f;

            familyScrollbar.trackObject = familyTrack;
            familyScrollbar.thumbObject = familyThumb;

            BubbleFamilyPanel.verticalScrollbar = familyScrollbar;

            adaptiveFamilyWheelHandler = OnAdaptiveFamilyMouseWheel;
            BubbleFamilyPanel.eventMouseWheel += adaptiveFamilyWheelHandler;

            // Store a layout action using the controls already built above.
            adaptiveFamilyLayout = () =>
            {
                // Workplace.
                FavCimsWorkingPlace.autoSize = false;
                FavCimsWorkingPlace.wordWrap = true;
                FavCimsWorkingPlace.maximumSize = Vector2.zero;

                FitFamilyTextBar(FavCimsWorkingPlace, 25f, false);

                WorkBuildingPanel.height = BubbleWorkBuildingSprite.height;

                // Activity must follow the resized workplace.
                SetFamilyControlY(BubbleActivityPanel, WorkBuildingPanel.relativePosition.y + WorkBuildingPanel.height + 6f);

                // Activity padding must be set before measurement.
                FavCimsLastActivity.textPadding.top = 3;
                FavCimsLastActivity.textPadding.bottom = 3;

                // Selected citizen: allow the complete activity to wrap.
                FitFamilyTextBar(FavCimsLastActivity, 18f, true);

                BubbleActivityPanel.height = BubbleActivitySprite.height;
                BubbleActivityVehiclePanel.height = BubbleActivitySprite.height;

                SetFamilyControlY(BubbleActivityVehiclePanel, 0f);

                CenterFamilyControl(FavCimsLastActivityVehicleButton, BubbleActivitySprite.height);

                SetFamilyControlY(BubbleDistrictPanel, BubbleActivityPanel.relativePosition.y + BubbleActivityPanel.height + 3f);

                // Grow the portrait area instead of overlapping the home section.
                float portraitHeight = BubbleDistrictPanel.relativePosition.y + BubbleDistrictPanel.height + 4f;

                BubbleFamPortBgSprite.height = portraitHeight;
                BubbleFamPortBgSpriteBack.height = portraitHeight;
                BubbleFamilyPortraitPanel.height = portraitHeight + 4f;

                SetFamilyControlY(BubbleDetailsPanel, BubbleFamilyPortraitPanel.relativePosition.y + BubbleFamilyPortraitPanel.height + 1f);

                SetFamilyControlY(BubbleFamilyBarPanel, BubbleDetailsPanel.relativePosition.y + BubbleDetailsPanel.height + 2f);

                SetFamilyControlY(BubbleFamilyPanel, BubbleFamilyBarPanel.relativePosition.y + BubbleFamilyBarPanel.height);

                float householdHeight = 0f;

                foreach (UIComponent child in BubbleFamilyPanel.components)
                {
                    if (child != null && child.isVisible)
                    {
                        householdHeight += child.height;
                    }
                }

                // Grow to content, but keep the overall window on screen.
                var container = FavCimsMainClass.FullScreenContainer;

                float maximumPanelHeight = container != null ? Mathf.Max(0f, container.height - 16f) : 700f;

                float fixedHeight = BubbleFamilyPanel.relativePosition.y + 8f;

                float availableListHeight = Mathf.Max(0f, maximumPanelHeight - fixedHeight);

                BubbleFamilyPanel.height = Mathf.Min(householdHeight, availableListHeight);

                host.height = fixedHeight + BubbleFamilyPanel.height;

                FavCimsOtherInfoSprite.height = host.height;

                familyScrollbar.relativePosition = new Vector3(host.width - 13f, BubbleFamilyPanel.relativePosition.y);

                familyScrollbar.height = BubbleFamilyPanel.height;
                familyTrack.height = familyScrollbar.height;

                familyScrollbar.isVisible = householdHeight > BubbleFamilyPanel.height + 0.5f;

                // Keep existing scrolling within the newly sized content.
                Vector2 scroll = BubbleFamilyPanel.scrollPosition;

                scroll.y = Mathf.Clamp(scroll.y, 0f, Mathf.Max(0f, householdHeight - BubbleFamilyPanel.height));

                BubbleFamilyPanel.scrollPosition = scroll;

                if (container != null)
                {
                    Vector3 position = host.absolutePosition;

                    float top = container.absolutePosition.y + 8f;
                    float bottom = container.absolutePosition.y + container.height - 8f;

                    position.y = Mathf.Clamp(position.y, top, Mathf.Max(top, bottom - host.height));
                    host.absolutePosition = position;
                }
            };

            IsReady = true;
        }

        public void Dispose()
        {
            ReleaseHouseholdRows();

            CitizenRequested = null;
            InstanceRequested = null;
            CloseRequested = null;
            DragStarted = null;
            DragEnded = null;
            HouseholdCitizenRequested = null;
            HouseholdInstanceRequested = null;
            HouseholdFavoriteRequested = null;
            IsReady = false;
        }

        public void RefreshAdaptiveLayout()
        {
            if (!IsReady || adaptiveFamilyLayout == null)
            {
                return;
            }

            adaptiveFamilyLayout.Invoke();
        }

        private void OnAdaptiveFamilyMouseWheel(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (adaptiveFamilyScroll == null)
            {
                return;
            }

            float direction = Math.Sign(eventParam.wheelDelta);
            adaptiveFamilyScroll.scrollPosition += new Vector2(0f, -direction * 26f);
            eventParam.Use();
        }

        private static void SetFamilyControlY(UIComponent component, float y)
        {
            Vector3 position = component.relativePosition;
            position.y = y;
            component.relativePosition = position;
        }

        private static void CenterFamilyControl(UIComponent component, float barHeight)
        {
            SetFamilyControlY(component, Mathf.Max(0f, (barHeight - component.height) * 0.5f));
        }

        private static float MeasureFamilyTextHeight(UIButton button)
        {
            if (button.font == null || string.IsNullOrEmpty(button.text))
            {
                return 0f;
            }

            var padding = button.textPadding;

            float availableWidth = Mathf.Max(1f, button.width - padding.left - padding.right);

            using var renderer = button.font.ObtainRenderer();

            renderer.pixelRatio = 1f;
            renderer.textScale = button.textScale;
            renderer.wordWrap = true;
            renderer.multiLine = true;

            renderer.maxSize = new Vector2(availableWidth, float.MaxValue);

            return Mathf.Ceil(renderer.MeasureString(button.text).y) + padding.top + padding.bottom + 4f;
        }

        private static void FitFamilyTextBar(UIButton text, float minimumHeight, bool fillAvailableWidth)
        {
            UIComponent bar = text.parent;

            text.autoSize = false;
            text.wordWrap = true;

            // Remove constraints before setting the measured dimensions.
            text.minimumSize = Vector2.zero;
            text.maximumSize = Vector2.zero;
            bar.maximumSize = Vector2.zero;

            // Keep the background at the height assigned by this layout.
            if (bar is UITextureSprite background)
            {
                background.autoSize = false;
            }

            if (fillAvailableWidth)
            {
                text.width = Mathf.Max(1f, bar.width - text.relativePosition.x - 5f);
            }

            float textHeight = MeasureFamilyTextHeight(text);

            float barHeight = Mathf.Ceil(Mathf.Max(minimumHeight, textHeight + 4f));

            bar.height = barHeight;

            text.height = barHeight - 4f;
            text.textVerticalAlignment = UIVerticalAlignment.Top;

            SetFamilyControlY(text, 2f);

            foreach (UIComponent child in bar.components)
            {
                if (child != text)
                {
                    CenterFamilyControl(child, barHeight);
                }
            }
        }

        private static string JoinFamilyActivity(string status, string targetName)
        {
            if (string.IsNullOrEmpty(status))
            {
                return targetName ?? string.Empty;
            }

            return string.IsNullOrEmpty(targetName) ? status : status + " " + targetName;
        }

        private static Color32 GetFamilyMemberAgeColor(int age)
        {
            if (age <= 12)
            {
                return new Color32(83, 166, 0, 255);
            }

            if (age <= 19)
            {
                return new Color32(0, 102, 51, 255);
            }

            if (age <= 25)
            {
                return new Color32(140, 110, 0, 255);
            }

            if (age <= 65)
            {
                return new Color32(180, 75, 0, 255);
            }

            if (age <= 90)
            {
                return new Color32(153, 0, 0, 255);
            }

            return new Color32(220, 0, 0, 255);
        }

        private static FamilyCitizenData FindHouseholdSlot(FamilyPanelData snapshot, int slot)
        {
            foreach (var member in snapshot.Household)
            {
                if (member.HouseholdSlot == slot)
                {
                    return member;
                }
            }

            return null;
        }

        private void RenderHouseholdVehicle(FamilyPanelData snapshot)
        {
            FamilyVehicleID = InstanceID.Empty;

            BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
            BubbleFamilyBarCarButton.tooltip = string.Empty;
            BubbleFamilyBarCarButton.playAudioEvents = false;

            var owner = FindHouseholdSlot(snapshot, 0);

            if (owner == null || !owner.HasOwnedVehicle)
            {
                return;
            }

            FamilyVehicleID = owner.OwnedVehicleInstanceId;

            BubbleFamilyBarCarButton.texture = TextureDB.BubbleCar;
            BubbleFamilyBarCarButton.playAudioEvents = true;

            BubbleFamilyBarCarButton.tooltip = owner.OwnedVehicleName + (owner.OwnedVehicleIsParked ? " (" + Locale.Get("VEHICLE_STATUS_PARKED") + ")" : string.Empty) + " - " + Locale.Get("VEHICLE_OWNER") + " " + owner.Name;
            BubbleFamilyBarCarButton.tooltip = FamilyPanelText.AppendHint(BubbleFamilyBarCarButton.tooltip, FamilyPanelText.InstanceNavigation);
        }
    }
}
