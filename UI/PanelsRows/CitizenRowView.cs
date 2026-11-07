using System;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public partial class CitizenRowView : UIPanel
    {
        public const float RowWidth = 1134f;
        public const float RowHeight = 40f;

        public event MouseEventHandler CitizenClicked;
        public event MouseEventHandler HomeClicked;
        public event MouseEventHandler WorkClicked;
        public event MouseEventHandler TargetClicked;
        public event MouseEventHandler VehicleClicked;
        public event Action FamilyClicked;
        public event Action CloseClicked;

        private UIPanel rowPanel;
        private UITextureSprite rowBackground;

        private Texture normalBackground;
        private Texture hoverBackground;

        private UIButton happinessIcon;
        private UITextureSprite happinessOverride;

        private UIButton nameButton;
        private UITextureSprite nameColumnBackground;
        private UIButton familyButton;

        private UIButton agePhaseButton;
        private UIButton realAgeButton;
        private UIButton educationButton;

        private BuildingCellView homeCell;
        private BuildingCellView workplaceCell;

        private UIButton activityButton;
        private UIButton activityVehicleButton;

        private UIButton closeButton;

        private UILabel citizenTypeLabel;

        private float happinessColumnWidth;
        private float nameColumnWidth;
        private float agePhaseColumnWidth;
        private float realAgeColumnWidth;
        private float educationColumnWidth;
        private float homeColumnWidth;
        private float workplaceColumnWidth;
        private float activityColumnWidth;
        private float closeColumnWidth;

        private static readonly string[] HappinessLevels =
        [
            "VeryUnhappy",
            "Unhappy",
            "Happy",
            "VeryHappy",
            "ExtremelyHappy"
        ];

        private static readonly string[] EducationLevels =
        [
            "Uneducated",
            "Educated",
            "WellEducated",
            "HighlyEducated"
        ];

        public bool IsReady { get; private set; }

        public override void Start()
        {
            try
            {
                width = RowWidth;
                height = RowHeight;
                autoLayoutDirection = LayoutDirection.Vertical;
                autoLayout = false;
                autoLayoutPadding = new RectOffset(0, 0, 0, 0);

                happinessColumnWidth = FavoriteCimsMainPanel.FavCimsHappinesColText.width;
                nameColumnWidth = FavoriteCimsMainPanel.FavCimsNameColText.width;
                agePhaseColumnWidth = FavoriteCimsMainPanel.FavCimsAgePhaseColText.width;
                realAgeColumnWidth = FavoriteCimsMainPanel.FavCimsAgeColText.width;
                educationColumnWidth = FavoriteCimsMainPanel.FavCimsEduColText.width;
                homeColumnWidth = FavoriteCimsMainPanel.FavCimsHomeColText.width;
                workplaceColumnWidth = FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width;
                activityColumnWidth = FavoriteCimsMainPanel.FavCimsLastActColText.width;
                closeColumnWidth = FavoriteCimsMainPanel.FavCimsCloseButtonCol.width;

                CreateRowPanel();
                CreateHappinessColumn();
                CreateNameColumn();
                CreateAgePhaseColumn();
                CreateRealAgeColumn();
                CreateEducationColumn();
                CreateHomeColumn();
                CreateWorkplaceColumn();
                CreateActivityColumn();
                CreateCloseColumn();
                InitializeAdaptiveLayout();

                SetRowVisible(false);

                IsReady = true;
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("CitizenRowView initialization failed: " + ex);
            }
        }

        private void CreateRowPanel()
        {
            rowPanel = AddUIComponent<UIPanel>();
            rowPanel.width = width;
            rowPanel.height = RowHeight;

            rowBackground = rowPanel.AddUIComponent<UITextureSprite>();
            rowBackground.name = "FavCimsCitizenSingleRowBGSprite";
            rowBackground.width = rowPanel.width;
            rowBackground.height = rowPanel.height;
            rowBackground.AlignTo(rowPanel, UIAlignAnchor.TopLeft);

            normalBackground = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.BgRow1.png");

            normalBackground.name = "FavDot1";

            hoverBackground = ResourceLoader.LoadTexture((int)width, 40, "UIMainPanel.Rows.BgRowHover.png");

            rowBackground.texture = normalBackground;

            rowPanel.eventMouseEnter += (component, param) => rowBackground.texture = hoverBackground;
            rowPanel.eventMouseLeave += (component, param) => rowBackground.texture = normalBackground;
        }

        private void CreateHappinessColumn()
        {
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsCitizenHappinessPanel", happinessColumnWidth, 0f);

            happinessIcon = column.AddUIComponent<UIButton>();
            happinessIcon.width = 30f;
            happinessIcon.height = 30f;
            happinessIcon.isEnabled = false;
            happinessIcon.playAudioEvents = false;
            happinessIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            happinessIcon.relativePosition = new Vector3(15f, 5f);

            happinessOverride = happinessIcon.AddUIComponent<UITextureSprite>();
            happinessOverride.width = 30f;
            happinessOverride.height = 30f;
            happinessOverride.tooltipBox = UIView.GetAView().defaultTooltipBox;
        }

        private void CreateNameColumn()
        {
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsCitizenNamePanel", nameColumnWidth, happinessColumnWidth);
            nameButton = CitizenRowColumns.CreateTextButton(column, "FavCimsCitizenName");

            nameButton.textPadding = new RectOffset(40, 5, 0, 0);
            nameButton.textColor = new Color32(204, 204, 51, 40);
            nameButton.hoveredTextColor = new Color32(204, 102, 0, 20);
            nameButton.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            nameButton.focusedTextColor = new Color32(153, 0, 0, 0);
            nameButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

            nameButton.eventMouseUp += (component, param) => CitizenClicked?.Invoke(component, param);

            nameColumnBackground = column.AddUIComponent<UITextureSprite>();
            nameColumnBackground.width = nameButton.width;
            nameColumnBackground.height = nameButton.height;
            nameColumnBackground.relativePosition = Vector3.zero;

            if (FavoriteCimsMainPanel.ColumnSpecialBackground)
            {
                nameColumnBackground.texture = TextureDB.FavCimsNameBgOverride_texture;
                nameColumnBackground.opacity = 0.7f;
            }

            FavoriteCimsMainPanel.FavCimsNameColText.eventClick += ToggleNameColumnBackground;

            nameButton.BringToFront();
            nameButton.relativePosition = Vector3.zero;

            familyButton = column.AddUIComponent<UIButton>();
            familyButton.name = "FavCimsOtherInfoButton";
            familyButton.width = 20f;
            familyButton.height = 20f;
            familyButton.playAudioEvents = true;
            familyButton.normalBgSprite = "CityInfo";
            familyButton.hoveredBgSprite = "CityInfoHovered";
            familyButton.pressedBgSprite = "CityInfoPressed";
            familyButton.disabledBgSprite = "CityInfoDisabled";
            familyButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            familyButton.relativePosition = new Vector3(10f, 10f);

            familyButton.eventClick += (component, param) => FamilyClicked?.Invoke();

            familyButton.isInteractive = true;
            familyButton.BringToFront();

            nameButton.height = 25f;
            nameButton.relativePosition = Vector3.zero;

            citizenTypeLabel = column.AddUIComponent<UILabel>();
            citizenTypeLabel.name = "FavCimsCitizenType";
            citizenTypeLabel.autoSize = false;
            citizenTypeLabel.width = nameColumnWidth - 45f;
            citizenTypeLabel.height = 16f;
            citizenTypeLabel.textScale = 0.65f;
            citizenTypeLabel.isInteractive = false;
            citizenTypeLabel.relativePosition = new Vector3(40f, 24f);

            CitizenRowColumns.CreateSeparator(column);
        }

        private void ToggleNameColumnBackground(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (nameColumnBackground.texture == null)
            {
                nameColumnBackground.texture = TextureDB.FavCimsNameBgOverride_texture;
                nameColumnBackground.opacity = 0.7f;
                FavoriteCimsMainPanel.ColumnSpecialBackground = true;
            }
            else
            {
                nameColumnBackground.texture = null;
                FavoriteCimsMainPanel.ColumnSpecialBackground = false;
            }
        }

        private void CreateAgePhaseColumn()
        {
            var columnX = happinessColumnWidth + nameColumnWidth;
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsAgePhasePanel", agePhaseColumnWidth, columnX);

            agePhaseButton = CitizenRowColumns.CreateTextButton(column, "FavCimsAgePhase");
            agePhaseButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            agePhaseButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
            agePhaseButton.isInteractive = false;

            CitizenRowColumns.CreateSeparator(column);
        }

        private void CreateRealAgeColumn()
        {
            var columnX = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth;
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsRealAgePanel", realAgeColumnWidth, columnX);

            realAgeButton = CitizenRowColumns.CreateTextButton(column, "FavCimsRealAge");

            realAgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            realAgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
            realAgeButton.isInteractive = false;

            CitizenRowColumns.CreateSeparator(column);
        }

        private void CreateEducationColumn()
        {
            var columnX = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth + realAgeColumnWidth;
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsEducationPanel", educationColumnWidth, columnX);

            educationButton = CitizenRowColumns.CreateTextButton(column, "FavCimsEducation");

            educationButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            educationButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
            educationButton.isInteractive = false;

            CitizenRowColumns.CreateSeparator(column);
        }

        private void CreateHomeColumn()
        {
            float x = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth + realAgeColumnWidth + educationColumnWidth;
            homeCell = new BuildingCellView(rowPanel, "FavCimsCitizenHomePanel", "FavCimsCitizenHome", homeColumnWidth, x);
            homeCell.NavigationRequested += OnHomeCellRequested;
        }

        private void CreateWorkplaceColumn()
        {
            float x = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth + realAgeColumnWidth + educationColumnWidth + homeColumnWidth;
            workplaceCell = new BuildingCellView(rowPanel, "FavCimsWorkingPlacePanel", "FavCimsWorkingPlace", workplaceColumnWidth, x);
            workplaceCell.NavigationRequested += OnWorkplaceCellRequested;
        }

        private void OnHomeCellRequested(UIComponent component, UIMouseEventParameter param)
        {
            HomeClicked?.Invoke(component, param);
        }

        private void OnWorkplaceCellRequested(UIComponent component, UIMouseEventParameter param)
        {
            WorkClicked?.Invoke(component, param);
        }

        private void CreateActivityColumn()
        {
            var columnX = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth + realAgeColumnWidth + educationColumnWidth + homeColumnWidth + workplaceColumnWidth;
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsLastActivityPanel", activityColumnWidth, columnX);

            activityButton = CitizenRowColumns.CreateTextButton(column, "FavCimsLastActivity", 0.85f);

            activityButton.width = column.width - 40f;
            activityButton.font.size = 14;
            activityButton.textPadding = new RectOffset(0, 5, 0, 0);
            activityButton.textColor = new Color32(21, 59, 96, 140);
            activityButton.maximumSize = new Vector2(column.width - 40f, 0f);

            activityButton.relativePosition = new Vector3(40f, 0f);
            activityButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

            activityButton.eventMouseUp += (component, param) => TargetClicked?.Invoke(component, param);

            activityVehicleButton = column.AddUIComponent<UIButton>();
            activityVehicleButton.name = "FavCimsLastActivityVehicleButton";
            activityVehicleButton.width = 26f;
            activityVehicleButton.height = 26f;
            activityVehicleButton.relativePosition = new Vector3(5f, 7f);
            activityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

            activityVehicleButton.eventMouseUp += (component, param) => VehicleClicked?.Invoke(component, param);

            CitizenRowColumns.CreateSeparator(column);
        }

        private void CreateCloseColumn()
        {
            var columnX = happinessColumnWidth + nameColumnWidth + agePhaseColumnWidth + realAgeColumnWidth + educationColumnWidth + homeColumnWidth + workplaceColumnWidth + activityColumnWidth;
            var column = CitizenRowColumns.CreateColumn(rowPanel, "FavCimsCloseRowPanel", closeColumnWidth, columnX);

            closeButton = column.AddUIComponent<UIButton>();
            closeButton.name = "FavCimsRowCloseButton";
            closeButton.width = 26f;
            closeButton.height = 26f;
            closeButton.normalBgSprite = "buttonclose";
            closeButton.hoveredBgSprite = "buttonclosehovered";
            closeButton.pressedBgSprite = "buttonclosepressed";
            closeButton.opacity = 0.9f;
            closeButton.playAudioEvents = true;
            closeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            closeButton.relativePosition = new Vector3(column.width / 2f - closeButton.width / 2f, 7f);

            closeButton.eventClick += (component, param) => CloseClicked?.Invoke();

            CitizenRowColumns.CreateSeparator(column);
        }

        public void SetRowVisible(bool visible)
        {
            if (rowPanel == null)
            {
                return;
            }

            if (visible)
            {
                rowPanel.Show();
            }
            else
            {
                rowPanel.Hide();
            }
        }

        public void SetName(string name)
        {
            nameButton.text = name ?? string.Empty;
        }

        public void SetGender(Citizen.Gender gender)
        {
            nameButton.textColor = gender == Citizen.Gender.Female ? new Color32(255, 102, 204, 213) : new Color32(204, 204, 51, 40);
        }

        public void SetCurrentDistrict(string districtName)
        {
            nameButton.tooltip = FavCimsLang.Text("CitizenCurrentDistrictLabel") + districtName;
        }

        public void SetEducation(Citizen.Education education, Citizen.Gender gender)
        {
            string genderSuffix = gender == Citizen.Gender.Female ? "Female" : "Male";

            educationButton.text = FavCimsLang.Text("CitizenEducation" + EducationLevels[(int)education] + genderSuffix);

            switch (education)
            {
                case Citizen.Education.ThreeSchools:
                    educationButton.textColor = new Color32(102, 204, 0, 60);
                    break;

                case Citizen.Education.TwoSchools:
                    educationButton.textColor = new Color32(255, 204, 0, 32);
                    break;

                case Citizen.Education.OneSchool:
                    educationButton.textColor = new Color32(255, 102, 0, 16);
                    break;

                case Citizen.Education.Uneducated:
                    break;

                default:
                    educationButton.textColor = new Color32(153, 0, 0, 0);
                    break;
            }
        }

        public void SetHappiness(Citizen.Happiness happiness)
        {
            happinessIcon.atlas = UIView.GetAView().defaultAtlas;
            happinessIcon.normalBgSprite = GetHappinessSprite(happiness);
            happinessIcon.tooltip = FavCimsLang.Text("CitizenHappiness" + HappinessLevels[(int)happiness] + "Status");

            happinessOverride.texture = null;
            happinessOverride.tooltip = null;
        }

        public void SetAge(int gameAge, int realAge, Citizen.Gender gender)
        {
            string genderSuffix = gender == Citizen.Gender.Female ? "Female" : "Male";

            string ageGroup = Citizen.GetAgeGroup(gameAge).ToString();

            agePhaseButton.text = FavCimsLang.Text("CitizenLifeStage" + ageGroup + genderSuffix);

            realAgeButton.text = realAge.ToString();

            Color32 ageColor = GetAgeColor(realAge);

            agePhaseButton.textColor = ageColor;
            realAgeButton.textColor = ageColor;
        }

        private static Color32 GetAgeColor(int realAge)
        {
            if (realAge <= 12)
            {
                return new Color32(102, 204, 0, 60);
            }

            if (realAge <= 19)
            {
                return new Color32(0, 102, 51, 100);
            }

            if (realAge <= 25)
            {
                return new Color32(255, 204, 0, 32);
            }

            if (realAge <= 65)
            {
                return new Color32(255, 102, 0, 16);
            }

            if (realAge <= 90)
            {
                return new Color32(153, 0, 0, 0);
            }

            return new Color32(255, 0, 0, 0);
        }

        public void SetHome(CitizenRowData data)
        {
            homeCell.Bind(data.Home);
        }

        public void SetWorkplace(CitizenRowData data)
        {
            workplaceCell.Bind(data.Workplace);
        }

        public void SetStudent(CitizenRowData data)
        {
            var school = data.Workplace;
            school.Icon = BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTextureStudent);
            school.LevelTexture = null;
            workplaceCell.Bind(school);
        }

        public void SetTouristWorkplace(CitizenRowData data)
        {
            workplaceCell.ShowState(data.Workplace.DisplayName, FavCimsLang.Text("CitizenTouristTooltip"), BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTexture));
        }

        public void SetRetiredWorkplace()
        {
            workplaceCell.ShowState(FavCimsLang.Text("CitizenRetiredStatus"), FavCimsLang.Text("CitizenRetiredTooltip"), BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTextureRetired));
        }

        public void SetUnemployedWorkplace()
        {
            workplaceCell.ShowState(FavCimsLang.Text("CitizenUnemployedStatus"), FavCimsLang.Text("CitizenUnemployedTooltip"), BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTexture));
        }

        public void SetActivity(CitizenRowData data)
        {
            activityButton.text = data.ActivityText;
            activityButton.isEnabled = data.ActivityEnabled;
            activityButton.tooltip = data.ActivityTooltip;

            activityVehicleButton.atlas = data.VehicleAtlas;
            activityVehicleButton.normalBgSprite = data.VehicleIcon;
            activityVehicleButton.hoveredBgSprite = data.VehicleHoverIcon;
            activityVehicleButton.focusedBgSprite = data.VehicleHoverIcon;
            activityVehicleButton.pressedBgSprite = data.VehicleHoverIcon;
            activityVehicleButton.disabledBgSprite = data.VehicleDisabledIcon;
            activityVehicleButton.isEnabled = data.VehicleEnabled;
            activityVehicleButton.playAudioEvents = data.VehicleEnabled;
            activityVehicleButton.tooltip = data.VehicleTooltip;
        }

        public void SetNormalHappiness(Citizen.Happiness happiness)
        {
            happinessIcon.atlas = UIView.GetAView().defaultAtlas;
            happinessIcon.normalBgSprite = GetHappinessSprite(happiness);
            happinessIcon.tooltip = FavCimsLang.Text("CitizenHappiness" + HappinessLevels[(int)happiness] + "Status");

            happinessOverride.texture = null;
            happinessOverride.tooltip = null;
        }

        public void SetArrested(CitizenRowData data)
        {
            happinessIcon.atlas = MyAtlas.FavCimsAtlas;
            happinessIcon.normalBgSprite = "CitizenArrestedIcon";
            happinessIcon.tooltip = FavCimsLang.Text("CitizenArrestedStatus");

            activityButton.text = data.ActivityText;
            activityButton.isEnabled = data.ActivityEnabled;
            activityButton.tooltip = data.ActivityTooltip;

            activityVehicleButton.atlas = data.VehicleAtlas;
            activityVehicleButton.normalBgSprite = data.VehicleIcon;
            activityVehicleButton.isEnabled = data.VehicleEnabled;
            activityVehicleButton.playAudioEvents = data.VehicleEnabled;
            activityVehicleButton.tooltip = data.VehicleTooltip;
        }

        public void SetDead(CitizenRowData data)
        {
            happinessIcon.normalBgSprite = "NotificationIconDead";
            happinessIcon.tooltip = FavCimsLang.Text("CitizenDeceasedStatus");

            realAgeButton.text = data.DeathRealAge > 0 ? data.DeathRealAge.ToString() : data.RealAge.ToString();

            nameButton.text = data.Name;
            nameButton.tooltip = data.NameTooltip;

            agePhaseButton.text = FavCimsLang.Text("CitizenLifeStageDeceased" + (data.Gender == Citizen.Gender.Female ? "Female" : "Male"));

            homeCell.ShowState(FavCimsLang.Text("CitizenHomeCemeteryStatus"), null, BuildingIconData.FromSprite(MyAtlas.FavCimsAtlas, "GraveIcon"));
            workplaceCell.DisableNavigationAndDetails();

            activityButton.text = data.ActivityText;
            activityButton.isEnabled = false;
            activityButton.tooltip = null;

            activityVehicleButton.isEnabled = false;
            activityVehicleButton.disabledBgSprite = "NotificationIconDead";
            activityVehicleButton.tooltip = null;

            familyButton.isEnabled = false;
        }

        public void SetGone(CitizenRowData data)
        {
            happinessIcon.normalBgSprite = string.Empty;
            happinessIcon.tooltip = null;

            happinessOverride.texture = TextureDB.FavCimsHappyOverride_texture;

            happinessOverride.relativePosition = Vector3.zero;
            happinessOverride.tooltip = FavCimsLang.Text("CitizenLeftCityStatus");

            nameButton.text = data.Name;
            nameButton.tooltip = data.NameTooltip;

            homeCell.ShowState(FavCimsLang.Text("CitizenHomeOutsideCityStatus"), null, BuildingIconData.FromSprite(MyAtlas.FavCimsAtlas, "HomelessIcon"));
            workplaceCell.DisableNavigationAndDetails();

            activityButton.isEnabled = false;
            activityButton.tooltip = null;

            activityVehicleButton.isEnabled = false;
            activityVehicleButton.disabledBgSprite = "NotificationIconDead";

            familyButton.isEnabled = false;
        }

        public void SetFamilyButtonState(bool enabled, bool isOpen)
        {
            familyButton.isEnabled = enabled;

            familyButton.tooltip = FavCimsLang.Text(enabled ? "CitizenDetailsTitle" : "CitizenNoHouseholdStatus");

            familyButton.normalBgSprite = isOpen ? "CityInfoFocused" : "CityInfo";
        }

        private static string GetHappinessSprite(Citizen.Happiness happiness)
        {
            return "NotificationIcon" + HappinessLevels[(int)happiness];
        }

        public override void OnDestroy()
        {
            if (FavoriteCimsMainPanel.FavCimsNameColText != null)
            {
                FavoriteCimsMainPanel.FavCimsNameColText.eventClick -= ToggleNameColumnBackground;
            }

            if (homeCell != null)
            {
                homeCell.NavigationRequested -= OnHomeCellRequested;
                homeCell.Dispose();
            }
            if (workplaceCell != null)
            {
                workplaceCell.NavigationRequested -= OnWorkplaceCellRequested;
                workplaceCell.Dispose();
            }

            base.OnDestroy();
        }

        public void SetHomeOutsideCity()
        {
            SetUnavailableHome(FavCimsLang.Text("CitizenHomeOutsideCityStatus"));
        }

        public void SetNoLocalHome()
        {
            SetUnavailableHome("—");
        }

        private void SetUnavailableHome(string text)
        {
            homeCell.ShowState(text, null, default);
        }

        public void SetCitizenType(bool isTourist)
        {
            if (citizenTypeLabel == null)
            {
                return;
            }

            citizenTypeLabel.text = isTourist ? FamilyPanelText.Tourist : FamilyPanelText.Resident;

            citizenTypeLabel.textColor = isTourist ? new Color32(0, 65, 120, 255) : new Color32(25, 65, 25, 255);

            citizenTypeLabel.textScale = 0.75f;
            citizenTypeLabel.opacity = 1f;
            citizenTypeLabel.useDropShadow = false;
        }
    }
}