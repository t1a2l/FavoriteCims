using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        public void RenderWorkplace(FamilyWorkplaceData workplace)
        {
            if (!IsReady) return;

            ResetWorkplacePresentation();

            if (workplace == null) return;

            if (!workplace.IsAvailable)
            {
                // An assigned but unreadable building is not unemployment.
                if (workplace.HasAssignment) return;
                if (workplace.IsStudent)
                {
                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
                    return;
                }
                FavCimsWorkingPlaceSprite.texture = workplace.IsRetired ? TextureDB.FavCimsWorkingPlaceTextureRetired : TextureDB.FavCimsWorkingPlaceTexture;
                FavCimsWorkingPlace.text = workplace.IsRetired ? FavCimsLang.Text("CitizenRetiredStatus") : FavCimsLang.Text("CitizenUnemployedStatus");
                FavCimsWorkingPlace.tooltip = workplace.IsRetired ? FavCimsLang.Text("CitizenRetiredTooltip") : FavCimsLang.Text("CitizenUnemployedTooltip");
                return;
            }

            FavCimsWorkingPlace.isEnabled = true;
            FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.Text("DistrictLabel") + " " + (workplace.DistrictId == 0 ? FavCimsLang.Text("DistrictNoneText") : workplace.DistrictName);

            if (workplace.IsStudent)
            {
                FavCimsWorkingPlace.text = workplace.Name;
                FavCimsWorkingPlace.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", workplace.SchoolLevel.ToString()) + " " + workplace.Name;
                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
                FavCimsWorkingPlace.tooltip = FamilyPanelText.AppendHint(FavCimsWorkingPlace.tooltip, FamilyPanelText.InstanceNavigation);
                return;
            }

            FavCimsWorkingPlace.text = string.IsNullOrEmpty(workplace.Profession) ? workplace.Name : workplace.Profession + " " + workplace.Name;
            RenderWorkplaceStyle(workplace);
            Color32 textColor = FavCimsWorkingPlace.textColor;
            textColor.a = 255;
            FavCimsWorkingPlace.textColor = textColor;
        }

        private void ResetWorkplacePresentation()
        {
            FavCimsWorkingPlace.text = string.Empty;
            FavCimsWorkingPlace.tooltip = string.Empty;
            FavCimsWorkingPlace.isEnabled = false;
            FavCimsWorkingPlace.textColor = new Color32(21, 59, 96, 255);
            FavCimsWorkingPlaceSprite.texture = null;
            FavCimsWorkingPlaceSprite.tooltip = string.Empty;
            FavCimsWorkingPlaceButtonGamDefImg.atlas = UIView.GetAView().defaultAtlas;
            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = string.Empty;
            FavCimsWorkingPlaceButtonGamDefImg.tooltip = string.Empty;
            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
        }

        private void RenderWorkplaceStyle(FamilyWorkplaceData workplace)
        {
            if (workplace.Service == ItemClass.Service.Commercial)
            {
                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;

                switch (workplace.SubService)
                {
                    case ItemClass.SubService.CommercialHigh:
                        FavCimsWorkingPlace.textColor = new Color32(0, 51, 153, 147);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                        FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
                        break;
                    case ItemClass.SubService.CommercialEco:
                        FavCimsWorkingPlace.textColor = new Color32(0, 150, 136, 116);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("BuildingTypeCommercialEcoTitle");
                        break;
                    case ItemClass.SubService.CommercialLeisure:
                        FavCimsWorkingPlace.textColor = new Color32(219, 68, 55, 3);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("BuildingTypeCommercialLeisureTitle");
                        break;
                    case ItemClass.SubService.CommercialTourist:
                        FavCimsWorkingPlace.textColor = new Color32(156, 39, 176, 194);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("BuildingTypeCommercialTourismTitle");
                        break;
                    case ItemClass.SubService.CommercialWallToWall:
                        FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                        FavCimsWorkingPlace.tooltip = FavCimsLang.Text("BuildingTypeCommercialWallToWallTitle");
                        break;
                    default:
                        FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
                        FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
                        break;
                }

                if (workplace.SubService == ItemClass.SubService.CommercialHigh || workplace.SubService == ItemClass.SubService.CommercialLow)
                {
                    FavCimsCitizenWorkPlaceLevelSprite.texture = workplace.Level switch
                    {
                        ItemClass.Level.Level2 => TextureDB.FavCimsCommercialLevel[2],
                        ItemClass.Level.Level3 => TextureDB.FavCimsResidentialLevel[3],
                        _ => TextureDB.FavCimsResidentialLevel[1],
                    };
                }
                else
                {
                    FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                }
            }
            else
            {
                if (workplace.Service == ItemClass.Service.Industrial)
                {
                    FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                    FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
                    ItemClass.SubService subService = workplace.SubService;
                    ItemClass.SubService subService2 = subService;
                    switch (subService2)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                            break;
                        case ItemClass.SubService.IndustrialFarming:
                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                            break;
                        case ItemClass.SubService.IndustrialOil:
                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                            break;
                        case ItemClass.SubService.IndustrialOre:
                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                            break;
                        default:
                            switch (subService2)
                            {
                                case ItemClass.SubService.PlayerIndustryForestry:
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                    break;
                                case ItemClass.SubService.PlayerIndustryFarming:
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                    break;
                                case ItemClass.SubService.PlayerIndustryOil:
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                    break;
                                case ItemClass.SubService.PlayerIndustryOre:
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                    break;
                                default:
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                    break;
                            }
                            break;
                    }
                    if (workplace.SubService == ItemClass.SubService.IndustrialGeneric)
                    {
                        FavCimsCitizenWorkPlaceLevelSprite.texture = workplace.Level switch
                        {
                            ItemClass.Level.Level2 => TextureDB.FavCimsIndustrialLevel[2],
                            ItemClass.Level.Level3 => TextureDB.FavCimsIndustrialLevel[3],
                            _ => TextureDB.FavCimsIndustrialLevel[1],
                        };
                    }
                    else
                    {
                        FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                    }
                }
                else
                {
                    if (workplace.Service == ItemClass.Service.Office)
                    {
                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                        FavCimsWorkingPlace.textColor = new Color32(0, 204, byte.MaxValue, 128);
                        FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;
                        ItemClass.SubService subService3 = workplace.SubService;
                        ItemClass.SubService subService4 = subService3;
                        if (subService4 != ItemClass.SubService.OfficeHightech)
                        {
                            FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office");
                        }
                        else
                        {
                            FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office") + " Eco";
                        }

                        FavCimsCitizenWorkPlaceLevelSprite.texture = workplace.Level switch
                        {
                            ItemClass.Level.Level2 => TextureDB.FavCimsOfficeLevel[2],
                            ItemClass.Level.Level3 => TextureDB.FavCimsOfficeLevel[3],
                            _ => TextureDB.FavCimsOfficeLevel[1],
                        };
                    }
                    else
                    {
                        FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                        FavCimsWorkingPlaceSprite.texture = null;
                        FavCimsWorkingPlace.textColor = new Color32(153, 102, 51, 20);
                        bool found_service = false;
                        switch (workplace.Service)
                        {
                            case ItemClass.Service.Electricity:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyPowerSaving";
                                FavCimsWorkingPlace.tooltip = FavCimsLang.Text("CitizenElectricityWorkplaceTooltip");
                                found_service = true;
                                break;
                            case ItemClass.Service.Water:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
                                FavCimsWorkingPlace.tooltip = FavCimsLang.Text("CitizenWaterWorkplaceTooltip");
                                found_service = true;
                                break;
                            case ItemClass.Service.Beautification:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarBeautificationParksnPlazas";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Beautification");
                                found_service = true;
                                break;
                            case ItemClass.Service.Garbage:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyRecycling";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Garbage");
                                found_service = true;
                                break;
                            case ItemClass.Service.HealthCare:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconHealthcareFocused";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Healthcare");
                                found_service = true;
                                break;
                            case ItemClass.Service.PoliceDepartment:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconPolice";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Police");
                                found_service = true;
                                break;
                            case ItemClass.Service.Education:
                                FavCimsWorkingPlace.textColor = new Color32(0, 102, 51, 100);
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconEducationPressed";
                                found_service = true;
                                break;
                            case ItemClass.Service.Monument:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "FeatureMonumentLevel6";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Monuments");
                                found_service = true;
                                break;
                            case ItemClass.Service.FireDepartment:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconFireSafety";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "FireDepartment");
                                found_service = true;
                                break;
                            case ItemClass.Service.PublicTransport:
                                {
                                    ItemClass.SubService subService5 = workplace.SubService;
                                    ItemClass.SubService subService6 = subService5;
                                    if (subService6 != ItemClass.SubService.PublicTransportPost)
                                    {
                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFreePublicTransport";
                                        FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                    }
                                    else
                                    {
                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarPublicTransportPost";
                                        FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                    }
                                    found_service = true;
                                    break;
                                }
                            case ItemClass.Service.Disaster:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarFireDepartmentDisaster";
                                FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                found_service = true;
                                break;
                            case ItemClass.Service.Museums:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaMuseums";
                                FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                found_service = true;
                                break;
                            case ItemClass.Service.VarsitySports:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                found_service = true;
                                break;
                            case ItemClass.Service.Fishing:
                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarIndustryFishing";
                                FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                found_service = true;
                                break;
                        }
                        if (!found_service)
                        {
                            FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyNone";
                            FavCimsWorkingPlace.tooltip = null;
                        }
                    }
                }
            }

            FavCimsWorkingPlace.tooltip = FamilyPanelText.AppendHint(FavCimsWorkingPlace.tooltip, FamilyPanelText.InstanceNavigation);
        }
    }
}