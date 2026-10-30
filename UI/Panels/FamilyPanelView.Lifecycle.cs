namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        public void ResetForReuse()
        {
            if (!IsReady) return;
            BubbleHeaderCitizenName.text = string.Empty;
            BubbleHeaderCitizenName.tooltip = string.Empty;
            BubbleHeaderIconSprite.texture = null;
            BubbleCitizenAgeVal.text = string.Empty;
            BubbleCitizenAgePhaseVal.text = string.Empty;
            BubbleRow1EducationTooltipArea.tooltip = string.Empty;
            BubbleEduLevel1.isEnabled = false;
            BubbleEduLevel2.isEnabled = false;
            BubbleEduLevel3.isEnabled = false;
            BubbleHealthValue.text = string.Empty;
            BubbleHealthSprite.normalBgSprite = string.Empty;
            BubbleHealthSprite.tooltip = string.Empty;
            BubbleRow2WealthValueVal.text = string.Empty;
            BubbleRow2WealthValueVal.tooltip = string.Empty;
            BubbleRow1HappyIcon.normalBgSprite = string.Empty;
            BubbleRow1HappyIcon.tooltip = string.Empty;
            BubbleRow2WellbeingIcon.normalBgSprite = string.Empty;
            BubbleRow2WellbeingIcon.tooltip = string.Empty;
            RenderWorkplace(null);
            RenderHome(null);
            RenderSelectedActivity(null);
            RenderPersonalVehicle(null);
            ClearHouseholdSection();
            RenderPet(null);
        }
    }
}