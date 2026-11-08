using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        private static readonly string[] DetailAgeGroupNames =
        [
            "Child", "Teen", "Young", "Adult", "Senior"
        ];

        /// Localization-key suffixes, ordered by Citizen.Wellbeing.
        private static readonly string[] DetailWellbeingNames =
        [
            "VeryUnhappy", "Unhappy", "Satisfied", "Happy", "VeryHappy"
        ];

        /// Localization-key suffixes, ordered by Citizen.Happiness.
        /// The game's Suberb member maps to our correctly spelled Superb key.
        private static readonly string[] DetailHappinessNames =
        [
            "Bad", "Poor", "Good", "Excellent", "Superb"
        ];

        /// Atlas sprite suffixes, independent of localization-key names.
        /// Preserves the existing five-icon progression.
        private static readonly string[] DetailMoodSpriteNames =
        [
            "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy"
        ];

        /// Localization-key suffixes for the health labels.
        private static readonly string[] DetailHealthNames =
        [
            "VerySick", "Sick", "Poor", "Healthy", "VeryHealthy", "Excellent"
        ];

        /// Preserve the existing atlas sprite suffixes.
        private static readonly string[] DetailHealthSpriteNames =
        [
            "VerySick", "Sick", "PoorHealth", "Healthy", "VeryHealthy", "ExcellentHealth"
        ];

        private static readonly string[] DetailEducationNames =
        [
            "Uneducated", "Educated", "WellEducated", "HighlyEducated"
        ];

        public void RenderSelectedCitizen(FamilyCitizenData citizen)
        {
            if (!IsReady || citizen == null) return;

            BubbleCitizenAge.text = FavCimsLang.Text("CitizenAgeColumnTitle");
            BubbleCitizenAgePhase.text = FavCimsLang.Text("CitizenLifeStageColumnTitle");
            BubbleCitizenEducation.text = FavCimsLang.Text("CitizenEducationColumnTitle");
            BubbleWealthSprite.tooltip = FavCimsLang.Text("CitizenWealthLabel");

            BubbleHeaderCitizenName.text = citizen.Name;
            bool female = citizen.Gender == Citizen.Gender.Female;
            BubbleHeaderIconSprite.texture = female ? TextureDB.BubbleHeaderIconSpriteTextureFemale : TextureDB.BubbleHeaderIconSpriteTextureMale;
            Color32 nameColor = female ? new Color32(255, 102, 204, 255) : new Color32(204, 204, 51, 255);
            BubbleHeaderCitizenName.textColor = nameColor;
            BubbleHeaderCitizenName.hoveredTextColor = nameColor;
            BubbleHeaderCitizenName.pressedTextColor = nameColor;
            BubbleHeaderCitizenName.focusedTextColor = nameColor;

            BubbleCitizenAgeVal.text = citizen.RealAge.ToString();

            var ageGroup = Citizen.GetAgeGroup(citizen.GameAge);
            string citizenLifeStage = "CitizenLifeStage" + DetailAgeGroupNames[(int)ageGroup] + "Status";
            BubbleCitizenAgePhaseVal.text = FavCimsLang.Text(citizenLifeStage);

            Color32 ageColor = GetFamilyMemberAgeColor(citizen.RealAge);
            BubbleCitizenAgeVal.textColor = ageColor;
            BubbleCitizenAgePhaseVal.textColor = ageColor;

            string citizenEducation = "CitizenEducation" + DetailEducationNames[(int)citizen.Education] + "Status";
            BubbleRow1EducationTooltipArea.tooltip = FavCimsLang.Text(citizenEducation);
            BubbleEduLevel1.isEnabled = citizen.Education == Citizen.Education.OneSchool || citizen.Education == Citizen.Education.TwoSchools || citizen.Education == Citizen.Education.ThreeSchools;
            BubbleEduLevel2.isEnabled = citizen.Education == Citizen.Education.TwoSchools || citizen.Education == Citizen.Education.ThreeSchools;
            BubbleEduLevel3.isEnabled = citizen.Education == Citizen.Education.ThreeSchools;

            int healthIndex = Mathf.Clamp((int)Citizen.GetHealthLevel(citizen.Health), 0, DetailHealthNames.Length - 1);
            BubbleHealthSprite.normalBgSprite = "NotificationIcon" + DetailHealthSpriteNames[healthIndex];
            BubbleHealthSprite.tooltip = Locale.Get("INFO_HEALTH_TITLE");

            string citizenHealth = "CitizenHealth" + DetailHealthNames[healthIndex] + "Status";

            BubbleHealthValue.text = FavCimsLang.Text(citizenHealth);
            BubbleHealthValue.textColor = GetDetailHealthColor(healthIndex);

            BubbleRow2WellbeingIcon.normalBgSprite = GetDetailWellbeingSprite(citizen.Education, citizen.Wellbeing);

            int wellbeingIndex = Mathf.Clamp((int)Citizen.GetWellbeingLevel(citizen.Education, citizen.Wellbeing), 0, DetailWellbeingNames.Length - 1);

            string citizenWellbeing = "CitizenWellbeing" + DetailWellbeingNames[wellbeingIndex] + "Status";

            BubbleRow2WellbeingIcon.tooltip = FavCimsLang.Text("CitizenWellbeingLabel") + FavCimsLang.Text(citizenWellbeing);

            string wealth;
            Color32 wealthColor;
            if (citizen.Wealth == 0)
            {
                wealth = "Low";
                wealthColor = new Color32(153, 0, 0, 255);
            }
            else if (citizen.Wealth == Citizen.Wealth.Medium)
            {
                wealth = "Medium";
                wealthColor = new Color32(140, 110, 0, 255);
            }
            else
            {
                wealth = "High";
                wealthColor = new Color32(83, 166, 0, 255);
            }

            string citizenWealth = "CitizenWealth" + wealth + "Status";
            BubbleRow2WealthValueVal.text = FavCimsLang.Text(citizenWealth);
            BubbleRow2WealthValueVal.textColor = wealthColor;
            BubbleRow2WealthValueVal.tooltip = FavCimsLang.Text("CitizenWealthLabel");

            if (citizen.IsArrested && citizen.IsCriminal)
            {
                BubbleRow1HappyIcon.atlas = MyAtlas.FavCimsAtlas;
                BubbleRow1HappyIcon.normalBgSprite = "CitizenArrestedIcon";
                BubbleRow1HappyIcon.tooltip = FavCimsLang.Text("CitizenArrestedStatus");
            }
            else
            {
                BubbleRow1HappyIcon.atlas = UIView.GetAView().defaultAtlas;
                BubbleRow1HappyIcon.normalBgSprite = GetDetailHappinessSprite(citizen.Happiness);

                int happinessIndex = Mathf.Clamp((int)Citizen.GetHappinessLevel(citizen.Happiness), 0, DetailHappinessNames.Length - 1);

                string citizenHappiness = "CitizenHappiness" + DetailHappinessNames[happinessIndex] + "Status";

                BubbleRow1HappyIcon.tooltip = FavCimsLang.Text("CitizenHappinessLabel") + FavCimsLang.Text(citizenHappiness);
            }
        }

        private static string GetDetailWellbeingSprite(Citizen.Education education, int value)
        {
            int index = Mathf.Clamp((int)Citizen.GetWellbeingLevel(education, value), 0, DetailWellbeingNames.Length - 1);
            return "NotificationIcon" + DetailMoodSpriteNames[index];
        }

        private static string GetDetailHappinessSprite(int value)
        {
            int index = Mathf.Clamp((int)Citizen.GetHappinessLevel(value), 0, DetailHappinessNames.Length - 1);
            return "NotificationIcon" + DetailMoodSpriteNames[index];
        }

        private static Color32 GetDetailHealthColor(int index)
        {
            return index switch
            {
                0 => new Color32(220, 0, 0, 255),
                1 => new Color32(153, 0, 0, 255),
                2 => new Color32(140, 110, 0, 255),
                3 => new Color32(83, 166, 0, 255),
                _ => new Color32(0, 102, 51, 255),
            };
        }
    }
}