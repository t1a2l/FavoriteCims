using AlgernonCommons.Translation;
using AlgernonCommons.UI;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class PeopleInsideBuildingsPanel : UIPanel
    {
        private float seconds = 0.5f;

        private bool execute = false;

        public InstanceID BuildingID;

        public UIPanel RefPanel;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private Building building;

        private BuildingInfo buildingInfo;

        public static Dictionary<uint, uint> CimsOnBuilding = [];

        private readonly FastList<object> fastList = new();

        public static int WorkersCount = 0;

        public static int GuestsCount = 0;

        public static int HotelGuestsCount = 0;

        private int total_workers = 0;

        private uint BuildingUnits;

        private CitizenUnit CitizenUnit => MyCitizen.m_units.m_buffer[BuildingUnits];

        private UIPanel Title;

        private UITextureSprite TitleSpriteBg;

        private UIButton TitleBuildingName;

        private UIPanel Body;

        private UITextureSprite BodySpriteBg;

        private UIList BodyList;

        private UIPanel Footer;

        private UITextureSprite FooterSpriteBg;

        private bool IsCimCareBuilding => FavCimsCore.IsCimCareBuilding(BuildingID.Building);

        private bool IsAreaResidentalBuilding => FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building);

        private bool IsHotel => FavCimsCore.IsHotel(BuildingID.Building);

        private bool IsInternationalTradeOfficeBuilding => FavCimsCore.IsInternationalTradeOfficeBuilding(BuildingID.Building);

        public override void Awake()
        {
            try
            {
                base.Awake();
                width = 250f;
                height = 500f;
                name = "FavCimsPeopleInsideBuildingsPanel";
                absolutePosition = new Vector3(0f, 0f);
                Hide();
                Title = AddUIComponent<UIPanel>();
                Title.name = "PeopleInsideBuildingsPanelTitle";
                Title.width = width;
                Title.height = 41f;
                Title.relativePosition = Vector3.zero;
                TitleSpriteBg = Title.AddUIComponent<UITextureSprite>();
                TitleSpriteBg.name = "PeopleInsideBuildingsPanelTitleBG";
                TitleSpriteBg.width = Title.width;
                TitleSpriteBg.height = Title.height;
                TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
                TitleSpriteBg.relativePosition = Vector3.zero;
                TitleBuildingName = Title.AddUIComponent<UIButton>();
                TitleBuildingName.name = "PeopleInsideBuildingsPanelName";
                TitleBuildingName.width = Title.width;
                TitleBuildingName.height = Title.height;
                TitleBuildingName.textVerticalAlignment = UIVerticalAlignment.Middle;
                TitleBuildingName.textHorizontalAlignment = UIHorizontalAlignment.Center;
                TitleBuildingName.playAudioEvents = false;
                TitleBuildingName.font = UIFonts.Regular;
                TitleBuildingName.font.size = 15;
                TitleBuildingName.textScale = 1f;
                TitleBuildingName.wordWrap = true;
                TitleBuildingName.textPadding.left = 5;
                TitleBuildingName.textPadding.right = 5;
                TitleBuildingName.textColor = new Color32(204, 204, 51, 40);
                TitleBuildingName.hoveredTextColor = new Color32(204, 204, 51, 40);
                TitleBuildingName.pressedTextColor = new Color32(204, 204, 51, 40);
                TitleBuildingName.focusedTextColor = new Color32(204, 204, 51, 40);
                TitleBuildingName.useDropShadow = true;
                TitleBuildingName.dropShadowOffset = new Vector2(1f, -1f);
                TitleBuildingName.dropShadowColor = new Color32(0, 0, 0, 0);
                TitleBuildingName.relativePosition = Vector3.zero;
                Body = AddUIComponent<UIPanel>();
                Body.name = "PeopleInsideBuildingsBody";
                Body.width = width;
                Body.autoLayoutDirection = LayoutDirection.Vertical;
                Body.autoLayout = true;
                Body.clipChildren = true;
                Body.height = 0f;
                Body.relativePosition = new Vector3(0f, Title.height);
                BodySpriteBg = Body.AddUIComponent<UITextureSprite>();
                BodySpriteBg.name = "PeopleInsideBuildingsDataContainer";
                BodySpriteBg.width = Body.width;
                BodySpriteBg.height = Body.height;
                BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
                BodySpriteBg.relativePosition = Vector3.zero;
                BodyList = UIList.AddUIList<MultiTypeRow>(BodySpriteBg, 12f, 0f, BodySpriteBg.width - 24f, Body.height);
                BodyList.EventSelectionChanged += (_, obj) => BodyList.SelectedIndex = -1;
                BodyList.name = "BodyList";
                Footer = AddUIComponent<UIPanel>();
                Footer.name = "PeopleInsideBuildingsPanelFooter";
                Footer.width = width;
                Footer.height = 12f;
                Footer.relativePosition = new Vector3(0f, Title.height + Body.height);
                FooterSpriteBg = Footer.AddUIComponent<UITextureSprite>();
                FooterSpriteBg.width = Footer.width;
                FooterSpriteBg.height = Footer.height;
                FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
                FooterSpriteBg.relativePosition = Vector3.zero;
                UIComponent uicomponent = UIView.Find<UIButton>("Esc");
                if (uicomponent != null)
                {
                    uicomponent.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                    {
                        Hide();
                    };
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error(" Building Panel Start() : " + ex.ToString());
            }
        }

        public override void Update()
        {
            if (MainClass.UnLoading)
            {
                return;
            }

            if (isVisible && !BuildingID.IsEmpty)
            {
                UpdatePanelLayout();
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
                if (execute)
                {
                    if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty &&
                       WorldInfoPanel.GetCurrentInstanceID().Type == InstanceType.Building &&
                       WorldInfoPanel.GetCurrentInstanceID() != BuildingID)
                    {
                        BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                    }
                    UpdateList();
                }
            }
        }

        public void UpdateList()
        {
            CimsOnBuilding.Clear();
            fastList.Clear();

            WorkersCount = 0;
            GuestsCount = 0;
            HotelGuestsCount = 0;

            building = MyBuilding.m_buildings.m_buffer[BuildingID.Building];
            buildingInfo = building.Info;
            BuildingUnits = MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenUnits;

            int homeCount = 0;
            int workCount = 0;
            int visitCount = 0;
            int studentCount = 0;
            int hotelCount = 0;
            CountCitizenUnits(ref building, ref homeCount, ref workCount, ref visitCount, ref studentCount, ref hotelCount);
            int totalUnitsCount = homeCount + workCount + visitCount + studentCount + hotelCount;

            UpdateBuildingTitles();

            int unitnum = 0;  
            total_workers = 0;

            bool isSubtitleAdded = false;

            if (!(buildingInfo.m_class.m_service == ItemClass.Service.Residential ||
                IsAreaResidentalBuilding ||
                IsCimCareBuilding))
            {
                fastList.Add(new TitleRowInfo(
                 () => WorkersCount == 0,
                 Translations.Translate("OnBuilding_Workers"),
                 $"{Translations.Translate("OnBuilding_NoWorkers")} ({Translations.Translate("OnBuilding_TotalWorkers")}{total_workers})",
                 "BworkingIcon"));
            }

            while (BuildingUnits != 0U && unitnum < totalUnitsCount)
            {
                uint nextUnit = MyCitizen.m_units.m_buffer[BuildingUnits].m_nextUnit;

                if ((buildingInfo.m_class.m_service == ItemClass.Service.Residential ||
                    IsAreaResidentalBuilding ||
                    IsCimCareBuilding) && CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Home))
                {
                    fastList.Add(new TitleRowInfo
                    {
                        atlas = null,
                        spriteName = "BapartmentIcon",
                        text = Translations.Translate("OnBuilding_Residential") + " " + (unitnum + 1).ToString()
                    });
                }

                for (int i = 0; i < 5; i++)
                {
                    uint citizenId = CitizenUnit.GetCitizen(i);

                    if (citizenId != 0U && !CimsOnBuilding.ContainsKey(citizenId))
                    {
                        Citizen citizen = MyCitizen.m_citizens.m_buffer[citizenId];
                        bool isAtBuilding = citizen.GetBuildingByLocation() == BuildingID.Building &&
                                            citizen.CurrentLocation != Citizen.Location.Moving;
                        bool forcedToGuest = false;
                        if (citizen.m_workBuilding == BuildingID.Building &&
                            (buildingInfo.m_class.m_service == ItemClass.Service.Education ||
                            buildingInfo.m_class.m_service == ItemClass.Service.PlayerEducation) &&
                            citizen.m_flags.IsFlagSet(Citizen.Flags.Student))
                        {
                            forcedToGuest = true;
                        }
                        if (BuildingID.Building == citizen.m_workBuilding && !forcedToGuest)
                        {
                            total_workers++;
                        }

                        if ((buildingInfo.m_class.m_service == ItemClass.Service.Residential ||
                            IsAreaResidentalBuilding ||
                            IsCimCareBuilding)
                            && CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Home))
                        {
                            CimsOnBuilding.Add(citizenId, BuildingUnits);
                            fastList.Add(citizenId);
                        }
                        else if ((buildingInfo.m_class.m_service == ItemClass.Service.Industrial ||
                                buildingInfo.m_class.m_service == ItemClass.Service.Office ||
                                IsInternationalTradeOfficeBuilding ||
                                IsCimCareBuilding)
                                && CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Work) && isAtBuilding)
                        {
                            WorkersCount++;
                            CimsOnBuilding.Add(citizenId, BuildingUnits);
                            fastList.Add(citizenId);
                        }
                        else if (IsHotel)
                        {
                            if (CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Work) && isAtBuilding)
                            {
                                WorkersCount++;
                                CimsOnBuilding.Add(citizenId, BuildingUnits);
                                fastList.Add(citizenId);
                            }
                            else if (CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Hotel) && isAtBuilding)
                            {
                                HotelGuestsCount++;
                                fastList.Add(new TitleRowInfo
                                {
                                    atlas = null,
                                    spriteName = "BapartmentIcon",
                                    text = Translations.Translate("OnBuilding_HotelRooms") + " " + (HotelGuestsCount + 1),
                                });
                                CimsOnBuilding.Add(citizenId, BuildingUnits);
                                fastList.Add(citizenId);
                            }
                            else if (CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Visit))
                            {
                                if (!isSubtitleAdded)
                                {
                                    AddOtherSubtitles();
                                    isSubtitleAdded = true;
                                }
                                if (isAtBuilding)
                                {
                                    GuestsCount++;
                                    CimsOnBuilding.Add(citizenId, BuildingUnits);
                                    fastList.Add(citizenId);
                                }
                            }
                        }
                        else
                        {
                            if (isAtBuilding)
                            {
                                if (BuildingID.Building == citizen.m_workBuilding && !forcedToGuest)
                                {
                                    WorkersCount++;
                                }
                                else
                                {
                                    if (!isSubtitleAdded)
                                    {
                                        AddOtherSubtitles();
                                        isSubtitleAdded = true;
                                    }
                                    GuestsCount++;
                                }
                                CimsOnBuilding.Add(citizenId, BuildingUnits);
                                fastList.Add(citizenId);
                            }
                        }
                        
                    }
                }
                BuildingUnits = nextUnit;
                if (++unitnum > Singleton<CitizenManager>.instance.m_units.m_size)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            if (IsHotel && HotelGuestsCount == 0)
            {
                fastList.Add(new TitleRowInfo
                {
                    atlas = null,
                    spriteName = "BapartmentIcon",
                    text = Translations.Translate("OnBuilding_noHotelGuests")
                });
            }

            BodyList.Data = fastList;
            BodyList.Refresh();
        }

        private void UpdateBuildingTitles()
        {
            if (IsHotel)
            {
                TitleBuildingName.text = Translations.Translate("CitizenOnHotelBuildingTitle");
                return;
            }
            if ((buildingInfo.m_class.m_service == ItemClass.Service.Residential ||
                IsAreaResidentalBuilding ||
                IsCimCareBuilding))
            {
                TitleBuildingName.text = Translations.Translate("Citizens_HouseHoldsTitle");
                return;
            }
            if ((buildingInfo.m_class.m_service == ItemClass.Service.Industrial ||
                buildingInfo.m_class.m_service == ItemClass.Service.Office ||
                IsInternationalTradeOfficeBuilding))
            {
                TitleBuildingName.text = Translations.Translate("WorkersOnBuilding");
                return;
            }
            else
            {
                switch (buildingInfo.m_class.m_service)
                {
                    case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                    case ItemClass.Service.HealthCare when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                    case ItemClass.Service.FireDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                        TitleBuildingName.text = Translations.Translate("OnHelicopter_Building_Service");
                        break;
                    case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level4:
                        TitleBuildingName.text = Translations.Translate("OnPrison_Building_Service");
                        break;
                    case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_subService != ItemClass.SubService.PoliceDepartmentBank:
                        TitleBuildingName.text = Translations.Translate("OnPolice_Building_Service");
                        break;
                    case ItemClass.Service.FireDepartment:
                        TitleBuildingName.text = Translations.Translate("OnFire_Building_Service");
                        break;
                    case ItemClass.Service.Disaster when buildingInfo.GetAI() is ShelterAI:
                        TitleBuildingName.text = Translations.Translate("OnShelter_Building_Service");
                        break;
                    case ItemClass.Service.Disaster:
                        TitleBuildingName.text = Translations.Translate("OnRescue_Building_Service");
                        break;
                    case ItemClass.Service.Education:
                        TitleBuildingName.text = Translations.Translate("OnEducation_Building_Service");
                        break;
                    case ItemClass.Service.PlayerEducation when !IsAreaResidentalBuilding:
                        TitleBuildingName.text = Translations.Translate("OnHighEducation_Building_Service");
                        break;
                    case ItemClass.Service.HealthCare when !IsCimCareBuilding:
                        TitleBuildingName.text = Translations.Translate("OnMedical_Building_Service");
                        break;
                    case ItemClass.Service.Beautification:
                        TitleBuildingName.text = Translations.Translate("OnBuilding_Guests");
                        break;
                    case ItemClass.Service.Monument:
                        TitleBuildingName.text = Translations.Translate("CitizenOnBuildingTitle");
                        break;
                    default:
                        TitleBuildingName.text = Translations.Translate("OnBuilding_Workers");
                        break;
                }
            }
        }

        public void AddOtherSubtitles()
        {
            switch (buildingInfo.m_class.m_service)
            {
                case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_subService != ItemClass.SubService.PoliceDepartmentBank:
                    fastList.Add(new TitleRowInfo(
                            () => GuestsCount == 0,
                            Translations.Translate("Citizen_Under_Arrest"),
                            Translations.Translate("OnBuilding_noArrested"),
                            "FavCimsCrimeArrested"));
                    break;
                case ItemClass.Service.Education:
                    fastList.Add(new TitleRowInfo(
                            () => GuestsCount == 0,
                            Translations.Translate("Citizen_at_School"),
                            Translations.Translate("OnBuilding_noStudents"),
                            "IconPolicySchoolsOut",
                            UITextures.InGameAtlas));
                    break;
                case ItemClass.Service.PlayerEducation when !IsAreaResidentalBuilding:
                    fastList.Add(new TitleRowInfo(
                        () => GuestsCount == 0,
                        Translations.Translate("Citizen_at_University"),
                        Translations.Translate("OnBuilding_noStudents"),
                        "IconPolicySchoolsOut",
                        UITextures.InGameAtlas));
                    break;
                case ItemClass.Service.HealthCare when !IsCimCareBuilding:
                    fastList.Add(new TitleRowInfo(
                        () => GuestsCount == 0,
                        Translations.Translate("Citizen_at_Clinic"),
                        Translations.Translate("OnBuilding_noPatients"),
                        "SubBarHealthcareDefault",
                        UITextures.InGameAtlas));
                    break;
                default:
                    fastList.Add(new TitleRowInfo(
                        () => GuestsCount == 0,
                        Translations.Translate("OnBuilding_Guests"),
                        Translations.Translate("OnBuilding_NoGuests"),
                        "BcommercialIcon"));
                    break;
            }
        }

        private void UpdatePanelLayout()
        {
            absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);
            if (50f + CimsOnBuilding.Count * 25f < height - Title.height - Footer.height)
            {
                Body.height = height - Title.height - Footer.height;
            }
            else
            {
                Body.height = Mathf.Min(400f, 50f + CimsOnBuilding.Count * 25f);
            }
            BodySpriteBg.height = Body.height;
            Footer.relativePosition = new Vector3(0f, Title.height + Body.height);
            BodyList.height = Body.height;
        }

        private void CountCitizenUnits(ref Building data, ref int homeCount, ref int workCount, ref int visitCount, ref int studentCount, ref int hotelCount)
        {
            if ((data.m_flags & (Building.Flags.Abandoned | Building.Flags.Collapsed)) != 0)
            {
                return;
            }
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint currentUnit = data.m_citizenUnits;
            while (currentUnit != 0)
            {
                CitizenUnit.Flags flags = instance.m_units.m_buffer[currentUnit].m_flags;
                if ((flags & CitizenUnit.Flags.Home) != 0)
                {
                    homeCount++;
                }
                if ((flags & CitizenUnit.Flags.Work) != 0)
                {
                    workCount++;
                }
                if ((flags & CitizenUnit.Flags.Visit) != 0)
                {
                    visitCount++;
                }
                if ((flags & CitizenUnit.Flags.Student) != 0)
                {
                    studentCount++;
                }
                if ((flags & CitizenUnit.Flags.Hotel) != 0)
                {
                    hotelCount++;
                }
                currentUnit = instance.m_units.m_buffer[currentUnit].m_nextUnit;
            }
        }
    }
}
