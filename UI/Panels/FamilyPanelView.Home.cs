using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        public void RenderSectionLabels()
        {
            if (!IsReady) return;
            FavCimsDistrictLabel.text = FavCimsLang.Text("DistrictLabel");
            BubbleFamilyBarLabel.text = FavCimsLang.Text("CitizenFamilyTitle");
            RefreshHouseholdLabels();
        }

        public void RenderHome(FamilyPanelData snapshot)
        {
            if (!IsReady) return;

            ResetHomePresentation();

            if (snapshot == null || snapshot.SelectedCitizen == null) return;

            var citizen = snapshot.SelectedCitizen;
            var home = snapshot.Home;
            if (home == null)
            {
                if (citizen.HomeBuilding == 0)
                {
                    BubbleHomeName.text = FavCimsLang.Text("CitizenHomeLess");
                    BubbleHomeName.tooltip = FavCimsLang.Text("CitizenHomeLessTooltip");
                    BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
                    BubbleHomeIcon.tooltip = FavCimsLang.Text("DistrictNameNoDistrict");
                }
                else
                {
                    BubbleHomeName.text = FamilyPanelText.HomeUnavailable;
                    BubbleHomeName.tooltip = FamilyPanelText.HomeUnavailable;
                }
                return;
            }

            BubbleHomeName.text = home.Name;
            BubbleHomeName.isEnabled = !home.InstanceId.IsEmpty;
            BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
            BubbleHomeIcon.tooltip = FavCimsLang.Text("DistrictLabel") + " " + (home.DistrictId == 0 ? FavCimsLang.Text("DistrictNameNoDistrict") : home.DistrictName);

            if (home.Service == ItemClass.Service.Residential)
            {
                switch (home.SubService)
                {
                    case ItemClass.SubService.ResidentialHigh:
                    case ItemClass.SubService.ResidentialHighEco:
                        BubbleHomeName.textColor = new Color32(0, 102, 51, 255);
                        BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
                        BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "2") + (home.SubService == ItemClass.SubService.ResidentialHighEco ? " Eco" : string.Empty);
                        break;
                    case ItemClass.SubService.ResidentialLow:
                    case ItemClass.SubService.ResidentialLowEco:
                        BubbleHomeName.textColor = new Color32(0, 120, 0, 255);
                        BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "1") + (home.SubService == ItemClass.SubService.ResidentialLowEco ? " Eco" : string.Empty);
                        break;
                }

                var level = home.Level switch
                {
                    ItemClass.Level.Level2 => 2,
                    ItemClass.Level.Level3 => 3,
                    ItemClass.Level.Level4 => 4,
                    ItemClass.Level.Level5 => 5,
                    _ => 1,
                };
                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[level];
            }

            RenderHomeConditions(home.Problems, citizen.IsArrested && citizen.IsCriminal);

            if (BubbleHomeName.isEnabled)
            {
                BubbleHomeName.tooltip = FamilyPanelText.AppendHint(BubbleHomeName.tooltip, FamilyPanelText.InstanceNavigation);
            }
        }

        private void ResetHomePresentation()
        {
            BubbleHomeName.text = string.Empty;
            BubbleHomeName.tooltip = string.Empty;
            BubbleHomeName.textColor = new Color32(21, 59, 96, 255);
            BubbleHomeName.isEnabled = false;
            BubbleHomeIcon.texture = null;
            BubbleHomeIcon.tooltip = string.Empty;
            BubbleHomeLevel.texture = null;
            BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
            ClearHomeCondition(BubbleDetailsElettricity);
            ClearHomeCondition(BubbleDetailsWater);
            ClearHomeCondition(BubbleDetailsDeath);
            ClearHomeCondition(BubbleDetailsFire);
            ClearHomeCondition(BubbleDetailsGarbage);
            ClearHomeCondition(BubbleDetailsLandValue);
            ClearHomeCondition(BubbleDetailsNoise);
            ClearHomeCondition(BubbleDetailsPollution);
            ClearHomeCondition(BubbleDetailsCrime);
        }

        private void RenderHomeConditions(Notification.ProblemStruct problems, bool arrested)
        {
            BubbleDetailsBgSprite.texture = problems != Notification.ProblemStruct.None ? TextureDB.BubbleDetailsBgSpriteProblems : TextureDB.BubbleDetailsBgSprite;

            // Preserve the existing ProblemStruct string-matching behavior for this extraction.
            string problemText = problems.ToString();
            SetHomeCondition(BubbleDetailsElettricity, HasHomeProblem(problemText, Notification.Problem1.Electricity), "NOTIFICATION_TITLE", "Electricity");
            SetHomeCondition(BubbleDetailsDeath, HasHomeProblem(problemText, Notification.Problem1.Death), "NOTIFICATION_TITLE", "Death");
            SetHomeCondition(BubbleDetailsFire, HasHomeProblem(problemText, Notification.Problem1.Fire), "NOTIFICATION_TITLE", "Fire");
            SetHomeCondition(BubbleDetailsGarbage, HasHomeProblem(problemText, Notification.Problem1.Garbage), "NOTIFICATION_TITLE", "Garbage");
            SetHomeCondition(BubbleDetailsNoise, HasHomeProblem(problemText, Notification.Problem1.Noise), "NOTIFICATION_NORMAL", "Noise");
            SetHomeCondition(BubbleDetailsPollution, HasHomeProblem(problemText, Notification.Problem1.Pollution), "NOTIFICATION_NORMAL", "Pollution");

            if (HasHomeProblem(problemText, Notification.Problem1.Sewage))
            { 
                SetHomeCondition(BubbleDetailsWater, true, "NOTIFICATION_TITLE", "Sewage"); 
            }
            else if (HasHomeProblem(problemText, Notification.Problem1.DirtyWater))
            { 
                SetHomeCondition(BubbleDetailsWater, true, "NOTIFICATION_NORMAL", "DirtyWater"); 
            }
            else if (HasHomeProblem(problemText, Notification.Problem1.Water))
            { 
                SetHomeCondition(BubbleDetailsWater, true, "NOTIFICATION_TITLE", "Water");
            }
            else if (HasHomeProblem(problemText, Notification.Problem1.Flood))
            { 
                SetHomeCondition(BubbleDetailsWater, true, "NOTIFICATION_TITLE", "Flood"); 
            }

            if (HasHomeProblem(problemText, Notification.Problem1.LandValueLow))
            { 
                SetHomeCondition(BubbleDetailsLandValue, true, "NOTIFICATION_TITLE", "LandValueLow"); 
            }
            else if (HasHomeProblem(problemText, Notification.Problem1.TooFewServices))
            { 
                SetHomeCondition(BubbleDetailsLandValue, true, "NOTIFICATION_TITLE", "ToofewServices"); 
            }
            else if (HasHomeProblem(problemText, Notification.Problem1.TaxesTooHigh))
            { 
                SetHomeCondition(BubbleDetailsLandValue, true, "NOTIFICATION_TITLE", "TaxesTooHigh"); 
            }

            if (arrested)
            {
                BubbleDetailsCrime.normalFgSprite = "tutorialglow";
                BubbleDetailsCrime.tooltip = FavCimsLang.Text("CitizenArrestedStatus");
            }
            else
            {
                SetHomeCondition(BubbleDetailsCrime, HasHomeProblem(problemText, Notification.Problem1.Crime), "NOTIFICATION_TITLE", "Crime");
            }
        }

        private static bool HasHomeProblem(string text, Notification.Problem1 problem)
        {
            return !string.IsNullOrEmpty(text) && text.Contains(problem.ToString());
        }

        private static void ClearHomeCondition(UIButton button)
        {
            button.normalFgSprite = string.Empty;
            button.tooltip = Locale.Get("NOTIFICATION_NONE");
        }

        private static void SetHomeCondition(UIButton button, bool active, string localeKey, string value)
        {
            if (!active)
            {
                ClearHomeCondition(button);
                return;
            }
            button.normalFgSprite = "tutorialglow";
            button.tooltip = Locale.Get(localeKey, value);
        }
    }
}