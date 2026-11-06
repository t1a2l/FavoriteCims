using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        private static readonly string[] DetailHappinessNames =
        [
            "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy"
        ];

        private static readonly string[] DetailHealthNames =
        [
            "VerySick", "Sick", "PoorHealth", "Healthy", "VeryHealthy", "ExcellentHealth"
        ];

        public void RenderSelectedCitizen(FamilyCitizenData citizen)
        {
            if (!IsReady || citizen == null) return;

            string gender = citizen.Gender.ToString();
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
            BubbleCitizenAgePhaseVal.text = FavCimsLang.Text("CitizenLifeStage" + Citizen.GetAgeGroup(citizen.GameAge) + gender);
            Color32 ageColor = GetFamilyMemberAgeColor(citizen.RealAge);
            BubbleCitizenAgeVal.textColor = ageColor;
            BubbleCitizenAgePhaseVal.textColor = ageColor;

            BubbleRow1EducationTooltipArea.tooltip = FavCimsLang.Text("CitizenEducation" + citizen.Education + gender);
            BubbleEduLevel1.isEnabled = citizen.Education == Citizen.Education.OneSchool || citizen.Education == Citizen.Education.TwoSchools || citizen.Education == Citizen.Education.ThreeSchools;
            BubbleEduLevel2.isEnabled = citizen.Education == Citizen.Education.TwoSchools || citizen.Education == Citizen.Education.ThreeSchools;
            BubbleEduLevel3.isEnabled = citizen.Education == Citizen.Education.ThreeSchools;

            int healthIndex = Mathf.Clamp((int)Citizen.GetHealthLevel(citizen.Health), 0, DetailHealthNames.Length - 1);
            string healthName = DetailHealthNames[healthIndex];
            BubbleHealthSprite.normalBgSprite = "NotificationIcon" + healthName;
            BubbleHealthSprite.tooltip = Locale.Get("INFO_HEALTH_TITLE");
            BubbleHealthValue.text = FavCimsLang.Text("CitizenHealth" + healthName + gender);
            BubbleHealthValue.textColor = GetDetailHealthColor(healthIndex);

            string wellbeingSprite = GetDetailHappinessSprite(citizen.Wellbeing);
            BubbleRow2WellbeingIcon.normalBgSprite = wellbeingSprite;
            BubbleRow2WellbeingIcon.tooltip = FavCimsLang.Text("CitizenWellbeingLabel") + " " + FavCimsLang.Text(wellbeingSprite);

            BubbleRow2WealthValueVal.tooltip = FavCimsLang.Text("CitizenWealthLabel");
            string wealthKey;
            Color32 wealthColor;
            if (citizen.Wealth == 0)
            {
                wealthKey = "LowWealth";
                wealthColor = new Color32(153, 0, 0, 255);
            }
            else if (citizen.Wealth == Citizen.Wealth.Medium)
            {
                wealthKey = "MidWealth";
                wealthColor = new Color32(140, 110, 0, 255);
            }
            else
            {
                wealthKey = "HighWealth";
                wealthColor = new Color32(83, 166, 0, 255);
            }
            BubbleRow2WealthValueVal.text = FavCimsLang.Text(wealthKey + gender);
            BubbleRow2WealthValueVal.textColor = wealthColor;

            if (citizen.IsArrested && citizen.IsCriminal)
            {
                BubbleRow1HappyIcon.atlas = MyAtlas.FavCimsAtlas;
                BubbleRow1HappyIcon.normalBgSprite = "CitizenArrestedIcon";
                BubbleRow1HappyIcon.tooltip = FavCimsLang.Text("CitizenArrestedStatus");
            }
            else
            {
                string happinessSprite = GetDetailHappinessSprite(citizen.Happiness);
                BubbleRow1HappyIcon.atlas = UIView.GetAView().defaultAtlas;
                BubbleRow1HappyIcon.normalBgSprite = happinessSprite;
                BubbleRow1HappyIcon.tooltip = FavCimsLang.Text("CitizenHappinessLabel") + " " + FavCimsLang.Text(happinessSprite);
            }
        }

        private static string GetDetailHappinessSprite(int value)
        {
            int index = Mathf.Clamp((int)Citizen.GetHappinessLevel(value), 0, DetailHappinessNames.Length - 1);
            return "NotificationIcon" + DetailHappinessNames[index];
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