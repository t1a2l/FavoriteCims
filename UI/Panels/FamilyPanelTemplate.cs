using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using System;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class FamilyPanelTemplate : UIPanel
    {
        private float seconds = 0.3f;

        private bool FirstRun = true;

        private bool execute = false;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private readonly InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private readonly DistrictManager MyDistrict = Singleton<DistrictManager>.instance;

        private static readonly string[] sHappinessLevels = ["VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy"];

        private static readonly string[] sHealthLevels = ["VerySick", "Sick", "PoorHealth", "Healthy", "VeryHealthy", "ExcellentHealth"];

        public InstanceID MyInstanceID;

        private InstanceID PrevMyInstanceID;

        private InstanceID MyPetID;

        private InstanceID MyVehicleID;

        private InstanceID MyTargetID;

        private InstanceID FamilyVehicleID;

        private InstanceID PersonalVehicleID;

        private InstanceID WorkPlaceID;

        private uint DogOwner = 0U;

        private int RealAge;

        private int HomeDistrict;

        private int WorkDistrict;

        private int CitizenDistrict;

        private InstanceID CitizenInstanceID;

        private uint citizen;

        private uint MyCitizenUnit;

        private Citizen CitizenData;

        private CitizenUnit Family;

        private uint CitizenPartner;

        private uint CitizenParent2;

        private uint CitizenParent3;

        private uint CitizenParent4;

        private InstanceID PartnerID;

        private InstanceID PartnerVehID;

        private InstanceID PartnerTarget;

        private InstanceID Parent1ID;

        private InstanceID Parent1VehID;

        private InstanceID Parent1Target;

        private InstanceID Parent2ID;

        private InstanceID Parent2VehID;

        private InstanceID Parent2Target;

        private InstanceID Parent3ID;

        private InstanceID Parent3VehID;

        private InstanceID Parent3Target;

        private InstanceID Parent4ID;

        private InstanceID Parent4VehID;

        private InstanceID Parent4Target;

        private InstanceID CitizenHomeID;

        private ushort WorkPlace;

        private ushort CitizenInstance;

        private ushort CitizenHome;

        private BuildingInfo HomeInfo;

        private BuildingInfo WorkInfo;

        private ushort MainCitizenInstance;

        private ushort policeveh;

        private bool isStudent;

        private ushort Pet;

        private CitizenInstance PetInstance;

        private WindowController PanelMover;

        private UITextureSprite FavCimsOtherInfoSprite;

        private UIPanel BubbleHeaderPanel;

        private UITextureSprite BubbleHeaderIconSprite;

        private UIButton BubbleHeaderCitizenName;

        private UIButton BubbleCloseButton;

        private UIPanel BubbleFamilyPortraitPanel;

        private UITextureSprite BubbleFamPortBgSprite;

        private UITextureSprite BubbleFamPortBgSpriteBack;

        private UIPanel BubbleRow1Panel;

        private UIPanel BubbleRow1HappyPanel;

        private UIButton BubbleRow1HappyIcon;

        private UIPanel BubbleRow1TextPanel;

        private UITextureSprite BubbleRow1LabelsSprite;

        private UIPanel BubbleRow1AgeLabelPanel;

        private UIButton BubbleCitizenAge;

        private UIPanel BubbleRow1AgePhaseLabelPanel;

        private UIButton BubbleCitizenAgePhase;

        private UIPanel BubbleRow1EducationLabelPanel;

        private UIButton BubbleCitizenEducation;

        private UIButton BubbleEduLevel1;

        private UIButton BubbleEduLevel2;

        private UIButton BubbleEduLevel3;

        private UIPanel BubbleRow1EducationTooltipArea;

        private UITextureSprite BubbleWealthHealthSprite;

        private UIPanel BubbleWealthSpritePanel;

        private UIButton BubbleWealthSprite;

        private UIPanel BubbleHealthSpritePanel;

        private UIButton BubbleHealthSprite;

        private UIButton BubbleHealthValue;

        private UIPanel BubbleRow1ValuesPanel;

        private UIPanel BubbleRow1AgeValuePanel;

        private UIButton BubbleCitizenAgeVal;

        private UIPanel BubbleRow1AgePhaseValuePanel;

        private UIButton BubbleCitizenAgePhaseVal;

        private UIPanel BubbleRow1EducationValuePanel;

        private UIPanel BubbleRow2Panel;

        private UIButton BubbleRow2WealthValueVal;

        private UIButton BubbleRow2WellbeingIcon;

        private UIPanel BubbleTargetPanel;

        private UIButton BubbleTargetIcon;

        private UIPanel WorkBuildingPanel;

        private UITextureSprite BubbleWorkBuildingSprite;

        private UIPanel BubbleActivityPanel;

        private UITextureSprite BubbleActivitySprite;

        private UIPanel BubbleDistrictPanel;

        private UITextureSprite BubbleDistrictSprite;

        private UIButton FavCimsWorkingPlace;

        private UIPanel BubbleWorkIconPanel;

        private UITextureSprite FavCimsWorkingPlaceSprite;

        private UIButton FavCimsWorkingPlaceButtonGamDefImg;

        private UITextureSprite FavCimsCitizenWorkPlaceLevelSprite;

        private UIPanel BubbleActivityVehiclePanel;

        private UIButton FavCimsLastActivityVehicleButton;

        private UIButton FavCimsLastActivity;

        private UIButton FavCimsDistrictLabel;

        private UIButton FavCimsDistrictValue;

        private UIPanel BubbleDetailsPanel;

        private UITextureSprite BubbleDetailsBgSprite;

        private UITextureSprite BubbleHomeIcon;

        private UITextureSprite BubbleHomeLevel;

        private UIPanel BubbleHomePanel;

        private UIButton BubbleHomeName;

        private UIPanel BubbleDetailsIconsPanel;

        private UIButton BubbleDetailsLandValue;

        private UIButton BubbleDetailsCrime;

        private UIButton BubbleDetailsNoise;

        private UIButton BubbleDetailsWater;

        private UIButton BubbleDetailsElettricity;

        private UIButton BubbleDetailsGarbage;

        private UIButton BubbleDetailsDeath;

        private UIButton BubbleDetailsFire;

        private UIButton BubbleDetailsPollution;

        private UIPanel BubbleFamilyBarPanel;

        private UITextureSprite BubbleFamilyBarPanelBg;

        private UILabel BubbleFamilyBarLabel;

        private UITextureSprite BubbleFamilyBarDogButton;

        private UITextureSprite BubbleFamilyBarCarButton;

        private UIPanel BubbleFamilyPanel;

        private UITextureSprite BubblePersonalCarButton;

        private UIPanel NoPartnerPanel;

        private UIButton NoPartnerBSprite;

        private UIButton NoPartnerFButton;

        private UIPanel NoChildsPanel;

        private UIButton NoChildsBSprite;

        private UIButton NoChildsFButton;

        private UIPanel PartnerPanel;

        private UITextureSprite BubblePartnerBgBar;

        private UIButton BubblePartnerLove;

        private UIButton BubblePartnerName;

        private UIButton BubbleParnerAgeButton;

        private UIButton BubblePartnerFollowToggler;

        private UITextureSprite BubblePartnerActivityBar;

        private UIButton BubblePartnerVehicleButton;

        private UIButton BubblePartnerDestination;

        private UIPanel Parent1Panel;

        private UITextureSprite BubbleParent1BgBar;

        private UIButton BubbleParent1Love;

        private UIButton BubbleParent1Name;

        private UIButton BubbleParent1AgeButton;

        private UIButton BubbleParent1FollowToggler;

        private UITextureSprite BubbleParent1ActivityBar;

        private UIButton BubbleParent1VehicleButton;

        private UIButton BubbleParent1Destination;

        private UIPanel FamilyMember2Panel;

        private UITextureSprite BubbleFamilyMember2BgBar;

        private UITextureSprite BubbleFamilyMember2IconSprite;

        private UIButton BubbleFamilyMember2Name;

        private UIButton BubbleFamilyMember2AgeButton;

        private UIButton BubbleFamilyMember2FollowToggler;

        private UITextureSprite BubbleFamilyMember2ActivityBgBar;

        private UIButton BubbleFamilyMember2ActivityVehicleButton;

        private UIButton BubbleFamilyMember2ActivityDestination;

        private UIPanel FamilyMember3Panel;

        private UITextureSprite BubbleFamilyMember3BgBar;

        private UITextureSprite BubbleFamilyMember3IconSprite;

        private UIButton BubbleFamilyMember3Name;

        private UIButton BubbleFamilyMember3AgeButton;

        private UIButton BubbleFamilyMember3FollowToggler;

        private UITextureSprite BubbleFamilyMember3ActivityBgBar;

        private UIButton BubbleFamilyMember3ActivityVehicleButton;

        private UIButton BubbleFamilyMember3ActivityDestination;

        private UIPanel FamilyMember4Panel;

        private UITextureSprite BubbleFamilyMember4BgBar;

        private UITextureSprite BubbleFamilyMember4IconSprite;

        private UIButton BubbleFamilyMember4Name;

        private UIButton BubbleFamilyMember4AgeButton;

        private UIButton BubbleFamilyMember4FollowToggler;

        private UITextureSprite BubbleFamilyMember4ActivityBgBar;

        private UIButton BubbleFamilyMember4ActivityVehicleButton;

        private UIButton BubbleFamilyMember4ActivityDestination;

        public void GoToCitizen(InstanceID Target, UIMouseEventParameter eventParam)
        {
            bool isEmpty = Target.IsEmpty;
            if (!isEmpty)
            {
                try
                {
                    if (MyInstance.SelectInstance(Target))
                    {
                        if (MyInstance.SelectInstance(Target))
                        {
                            WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
                        }
                        else
                        {
                            if (MyInstance.SelectInstance(Target))
                            {
                                MyInstanceID = Target;
                                execute = true;
                                LateUpdate();
                            }
                            else
                            {
                                ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
                                WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
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

        public void GoToInstance(InstanceID Target, UIMouseEventParameter eventParam)
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
                            DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
                        }
                        else
                        {
                            ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
                            DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        internal void FamilyVehicle(uint m_citizen, UITextureSprite sPrite, out InstanceID MyCitVeh)
        {
            MyCitVeh = InstanceID.Empty;
            if (m_citizen != 0U)
            {
                try
                {
                    ushort vehicle = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                    ushort parkedVehicle = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_parkedVehicle;
                    if (vehicle > 0)
                    {
                        MyCitVeh.Vehicle = vehicle;
                        VehicleInfo info = MyVehicle.m_vehicles.m_buffer[vehicle].Info;
                        if (info.m_vehicleAI.GetOwnerID(vehicle, ref MyVehicle.m_vehicles.m_buffer[vehicle]).Citizen == m_citizen)
                        {
                            sPrite.texture = TextureDB.BubbleCar;
                            sPrite.playAudioEvents = true;
                            sPrite.tooltip = string.Concat(
                            [
                                MyVehicle.GetVehicleName(vehicle),
                                " - ",
                                Locale.Get("VEHICLE_OWNER"),
                                " ",
                                MyCitizen.GetCitizenName(m_citizen)
                            ]);
                        }
                    }
                    else
                    {
                        if (parkedVehicle > 0)
                        {
                            MyCitVeh.ParkedVehicle = parkedVehicle;
                            VehicleParked vehicleParked = MyVehicle.m_parkedVehicles.m_buffer[parkedVehicle];
                            if (vehicleParked.m_ownerCitizen == m_citizen)
                            {
                                sPrite.texture = TextureDB.BubbleCar;
                                sPrite.playAudioEvents = true;
                                sPrite.tooltip = string.Concat(
                                [
                                    MyVehicle.GetParkedVehicleName(parkedVehicle),
                                    " (",
                                    Locale.Get("VEHICLE_STATUS_PARKED"),
                                    ") - ",
                                    Locale.Get("VEHICLE_OWNER"),
                                    " ",
                                    MyCitizen.GetCitizenName(m_citizen)
                                ]);
                            }
                        }
                        else
                        {
                            sPrite.texture = TextureDB.BubbleCarDisabled;
                            sPrite.playAudioEvents = false;
                            sPrite.tooltip = null;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        internal void Activity(uint m_citizen, UIButton ButtVehicle, UIButton ButtDestination, out InstanceID VehID, out InstanceID MyTargetID)
        {
            VehID = InstanceID.Empty;
            MyTargetID = InstanceID.Empty;
            if (m_citizen != 0U)
            {
                ushort instance = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_instance;
                CitizenInstance citizenInstance = MyCitizen.m_instances.m_buffer[instance];
                if (citizenInstance.m_targetBuilding > 0)
                {
                    ushort vehicle = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                    if (vehicle > 0)
                    {
                        VehID.Vehicle = vehicle;
                        ButtVehicle.isEnabled = true;
                        VehicleInfo info = MyVehicle.m_vehicles.m_buffer[vehicle].Info;
                        string text = MyVehicle.GetVehicleName(vehicle);
                        if (info.m_class.m_service == ItemClass.Service.Residential)
                        {
                            if (text.Like("Bicycle"))
                            {
                                ButtVehicle.normalBgSprite = "IconTouristBicycleVehicle";
                                ButtVehicle.hoveredBgSprite = "IconTouristBicycleVehicle";
                                ButtVehicle.tooltip = text + " - " + Locale.Get("PROPS_DESC", "bicycle01");
                            }
                            else
                            {
                                ButtVehicle.normalBgSprite = "IconCitizenVehicle";
                                ButtVehicle.hoveredBgSprite = "IconTouristVehicle";
                                ButtVehicle.tooltip = text;
                            }
                        }
                        else
                        {
                            if (info.m_class.m_service == ItemClass.Service.PublicTransport)
                            {
                                switch (info.m_class.m_subService)
                                {
                                    case ItemClass.SubService.PublicTransportBus:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportBus";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportBusHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportBusFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportBusPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Bus") + " - " + Locale.Get("SUBSERVICE_DESC", "Bus");
                                        break;
                                    case ItemClass.SubService.PublicTransportMetro:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportMetro";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportMetroFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportMetroPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Metro") + " - " + Locale.Get("SUBSERVICE_DESC", "Metro");
                                        break;
                                    case ItemClass.SubService.PublicTransportTrain:
                                        {
                                            ButtVehicle.normalBgSprite = "SubBarPublicTransportTrain";
                                            ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
                                            ButtVehicle.focusedBgSprite = "SubBarPublicTransportTrainFocused";
                                            ButtVehicle.pressedBgSprite = "SubBarPublicTransportTrainPressed";
                                            if (text == "VEHICLE_TITLE[Train Passenger]:0")
                                            {
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine");
                                            }
                                            else
                                            {
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get("SUBSERVICE_DESC", "Train");
                                            }
                                            break;
                                        }
                                    case ItemClass.SubService.PublicTransportShip:
                                        {
                                            if (text.Like("Ferry"))
                                            {
                                                ButtVehicle.normalBgSprite = "SubBarPublicTransportShip";
                                                ButtVehicle.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                ButtVehicle.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                ButtVehicle.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get("FEATURES_DESC", "Ferry");
                                            }
                                            else
                                            {
                                                ButtVehicle.normalBgSprite = "SubBarPublicTransportShip";
                                                ButtVehicle.hoveredBgSprite = "SubBarPublicTransportShipHovered";
                                                ButtVehicle.focusedBgSprite = "SubBarPublicTransportShipFocused";
                                                ButtVehicle.pressedBgSprite = "SubBarPublicTransportShipPressed";
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Ship");
                                            }
                                            break;
                                        }
                                    case ItemClass.SubService.PublicTransportPlane:
                                        {
                                            if (text.Like("Blimp"))
                                            {
                                                ButtVehicle.normalBgSprite = "IconPolicyEducationalBlimps";
                                                ButtVehicle.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
                                                ButtVehicle.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
                                                ButtVehicle.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get("FEATURES_DESC", "Blimp");
                                            }
                                            else
                                            {
                                                ButtVehicle.normalBgSprite = "SubBarPublicTransportPlane";
                                                ButtVehicle.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
                                                ButtVehicle.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
                                                ButtVehicle.pressedBgSprite = "SubBarPublicTransportPlanePressed";
                                                ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get("SUBSERVICE_DESC", "Plane");
                                            }
                                            break;
                                        }
                                    case ItemClass.SubService.PublicTransportTaxi:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportTaxi";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get("SUBSERVICE_DESC", "Taxi");
                                        break;
                                    case ItemClass.SubService.PublicTransportTram:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportTram";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTramHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportTramFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportTramPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Tram") + " - " + Locale.Get("SUBSERVICE_DESC", "Tram");
                                        break;
                                    case ItemClass.SubService.PublicTransportMonorail:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportMonorail";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get("SUBSERVICE_DESC", "Monorail");
                                        break;
                                    case ItemClass.SubService.PublicTransportCableCar:
                                        ButtVehicle.normalBgSprite = "SubBarPublicTransportCableCar";
                                        ButtVehicle.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
                                        ButtVehicle.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
                                        ButtVehicle.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
                                        ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get("SUBSERVICE_DESC", "CableCar");
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ButtVehicle.disabledBgSprite = "InfoIconPopulationDisabled";
                        ButtVehicle.isEnabled = false;
                        ButtVehicle.tooltip = Translations.Translate("Vehicle_on_foot");
                    }
                }
                else
                {
                    ButtVehicle.disabledBgSprite = "InfoIconPopulationDisabled";
                    ButtVehicle.isEnabled = false;
                    ButtVehicle.tooltip = null;
                }
                CitizenInstanceID.Citizen = m_citizen;
                CitizenInfo citizenInfo = MyCitizen.m_citizens.m_buffer[(int)m_citizen].GetCitizenInfo(m_citizen);
                string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(m_citizen, ref MyCitizen.m_citizens.m_buffer[(int)m_citizen], out MyTargetID);
                string buildingName = MyBuilding.GetBuildingName(MyTargetID.Building, CitizenInstanceID);
                ButtDestination.text = localizedStatus + " " + buildingName;
                if (!MyTargetID.IsEmpty)
                {
                    int district = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)MyTargetID.Index].m_position);
                    if (district == 0)
                    {
                        ButtDestination.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                    }
                    else
                    {
                        ButtDestination.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(district);
                    }
                }
                if (MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)m_citizen)))].Arrested && MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)m_citizen)))].Criminal)
                {
                    if (MyCitizen.m_citizens.m_buffer[(int)m_citizen].CurrentLocation == Citizen.Location.Moving)
                    {
                        policeveh = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                        if (policeveh > 0)
                        {
                            VehID.Vehicle = policeveh;
                            ButtVehicle.atlas = MyAtlas.FavCimsAtlas;
                            ButtVehicle.normalBgSprite = "FavCimsPoliceVehicle";
                            ButtVehicle.isEnabled = true;
                            ButtVehicle.playAudioEvents = true;
                            ButtVehicle.tooltip = MyVehicle.GetVehicleName(policeveh) + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
                            ButtDestination.isEnabled = false;
                            ButtDestination.text = Translations.Translate("Transported_to_Prison");
                        }
                    }
                    else
                    {
                        ButtDestination.isEnabled = true;
                        ButtDestination.text = Translations.Translate("Jailed_into") + buildingName;
                        ButtVehicle.atlas = UIView.GetAView().defaultAtlas;
                    }
                }
            }
        }

        internal void FamilyPet(uint m_citizen)
        {
            if (m_citizen != 0U)
            {
                try
                {
                    CitizenInstance = MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_instance;
                    Pet = (ushort)Array.FindIndex(MyCitizen.m_instances.m_buffer, (CitizenInstance element) => element.m_targetBuilding == CitizenInstance);
                    PetInstance = MyCitizen.m_instances.m_buffer[Pet];
                    if (PetInstance.Info.m_citizenAI.IsAnimal())
                    {
                        DogOwner = m_citizen;
                        MyPetID.CitizenInstance = Pet;
                        if (!MyPetID.IsEmpty)
                        {
                            string instanceName = MyCitizen.GetInstanceName(Pet);
                            CitizenInfo info = PetInstance.Info;
                            string localizedStatus = info.m_citizenAI.GetLocalizedStatus(Pet, ref PetInstance, out InstanceID instanceID);
                            BubbleFamilyBarDogButton.texture = TextureDB.BubbleDog;
                            BubbleFamilyBarDogButton.tooltip = string.Concat(
                            [
                                instanceName,
                                " - ",
                                localizedStatus,
                                " ",
                                MyCitizen.GetCitizenName(m_citizen)
                            ]);
                            BubbleFamilyBarDogButton.playAudioEvents = true;
                        }
                    }
                    else
                    {
                        DogOwner = 0U;
                        BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
                        BubbleFamilyBarDogButton.tooltip = null;
                        BubbleFamilyBarDogButton.playAudioEvents = false;
                        MyPetID = InstanceID.Empty;
                    }
                }
                catch
                {
                }
            }
        }

        internal static string GetHappinessString(Citizen.Happiness happinessLevel)
        {
            return "NotificationIcon" + sHappinessLevels[(int)happinessLevel];
        }

        internal static string GetHealthString(Citizen.Health healthLevel)
        {
            return "NotificationIcon" + sHealthLevels[(int)healthLevel];
        }

        public override void Start()
        {
            UITextureAtlas favCimsAtlas = MyAtlas.FavCimsAtlas;
            width = 250f;
            height = 500f;
            clipChildren = true;
            int num = 30;
            int num2 = Screen.width / 4;
            int num3 = 100;
            int num4 = Screen.height - (int)height * 2 - num3;
            System.Random random = new();
            FavCimsOtherInfoSprite = AddUIComponent<UITextureSprite>();
            FavCimsOtherInfoSprite.name = "FavCimsOtherInfoSprite";
            FavCimsOtherInfoSprite.texture = TextureDB.FavCimsOtherInfoTexture;
            FavCimsOtherInfoSprite.width = width;
            FavCimsOtherInfoSprite.height = height;
            FavCimsOtherInfoSprite.relativePosition = Vector3.zero;
            BubbleHeaderPanel = AddUIComponent<UIPanel>();
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
            BubbleHeaderCitizenName.eventMouseDown += delegate
            {
                bool mouseButton = Input.GetMouseButton(0);
                if (mouseButton)
                {
                    if (GetComponentInChildren<WindowController>() != null)
                    {
                        PanelMover = GetComponentInChildren<WindowController>();
                        PanelMover.ComponentToMove = this;
                        PanelMover.Stop = false;
                        PanelMover.Start();
                    }
                    else
                    {
                        PanelMover = AddUIComponent(typeof(WindowController)) as WindowController;
                        PanelMover.ComponentToMove = this;
                    }
                    opacity = 0.5f;
                }
            };
            BubbleHeaderCitizenName.eventMouseUp += delegate
            {
                if (PanelMover != null)
                {
                    PanelMover.Stop = true;
                    PanelMover.ComponentToMove = null;
                    PanelMover = null;
                }
                opacity = 1f;
            };
            BubbleCloseButton = AddUIComponent<UIButton>();
            BubbleCloseButton.name = "BubbleCloseButton";
            BubbleCloseButton.width = 26f;
            BubbleCloseButton.height = 26f;
            BubbleCloseButton.normalBgSprite = "buttonclose";
            BubbleCloseButton.hoveredBgSprite = "buttonclosehover";
            BubbleCloseButton.pressedBgSprite = "buttonclosepressed";
            BubbleCloseButton.opacity = 0.9f;
            BubbleCloseButton.playAudioEvents = true;
            BubbleCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleCloseButton.eventClick += delegate
            {
                try
                {
                    Hide();
                    MyInstanceID = InstanceID.Empty;
                }
                catch (Exception ex)
                {
                    Utils.Debug.Error("Can't remove family panel " + ex.ToString());
                }
            };
            BubbleCloseButton.relativePosition = new Vector3(BubbleHeaderPanel.width - 36f, 7f);
            BubbleFamilyPortraitPanel = AddUIComponent<UIPanel>();
            BubbleFamilyPortraitPanel.name = "BubbleFamilyPortraitPanel";
            BubbleFamilyPortraitPanel.width = 242f;
            BubbleFamilyPortraitPanel.height = 156f;
            BubbleFamilyPortraitPanel.relativePosition = new Vector3(4f, BubbleHeaderPanel.relativePosition.y + BubbleHeaderPanel.height);
            BubbleFamPortBgSpriteBack = BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            BubbleFamPortBgSpriteBack.name = "BubbleFamPortBgSpriteBack";
            BubbleFamPortBgSpriteBack.texture = TextureDB.BubbleFamPortBgSpriteBackTexture;
            BubbleFamPortBgSpriteBack.relativePosition = new Vector3(4f, 4f);
            BubbleFamPortBgSprite = BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            BubbleFamPortBgSprite.name = "BubbleFamPortBgSprite";
            BubbleFamPortBgSprite.texture = TextureDB.BubbleFamPortBgSpriteTexture;
            BubbleFamPortBgSprite.relativePosition = Vector3.zero;
            BubbleRow1Panel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleRow1Panel.name = "BubbleRow1Panel";
            BubbleRow1Panel.width = 234f;
            BubbleRow1Panel.height = 36f;
            BubbleRow1Panel.relativePosition = new Vector3(4f, 4f);
            BubbleRow1HappyPanel = BubbleRow1Panel.AddUIComponent<UIPanel>();
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
            BubbleRow1TextPanel = BubbleRow1Panel.AddUIComponent<UIPanel>();
            BubbleRow1TextPanel.name = "BubbleRow1TextPanel";
            BubbleRow1TextPanel.width = 198f;
            BubbleRow1TextPanel.height = 37f;
            BubbleRow1TextPanel.relativePosition = new Vector3(36f, 0f);
            BubbleRow1LabelsSprite = BubbleRow1TextPanel.AddUIComponent<UITextureSprite>();
            BubbleRow1LabelsSprite.name = "BubbleRow1LabelsSprite";
            BubbleRow1LabelsSprite.width = 198f;
            BubbleRow1LabelsSprite.height = 34f;
            BubbleRow1LabelsSprite.texture = TextureDB.BubbleBgBar1Big;
            BubbleRow1LabelsSprite.relativePosition = new Vector3(0f, 3f);
            BubbleRow1AgeLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
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
            BubbleRow1AgePhaseLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
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
            BubbleRow1EducationLabelPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
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
            BubbleRow1ValuesPanel = BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            BubbleRow1ValuesPanel.name = "BubbleRow1ValuesPanel";
            BubbleRow1ValuesPanel.width = 198f;
            BubbleRow1ValuesPanel.height = 17f;
            BubbleRow1ValuesPanel.relativePosition = new Vector3(0f, 17f);
            BubbleRow1AgeValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
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
            BubbleRow1AgePhaseValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
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
            BubbleRow1EducationValuePanel = BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
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
            BubbleTargetPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleTargetPanel.name = "BubbleTargetPanel";
            BubbleTargetPanel.width = 58f;
            BubbleTargetPanel.height = 36f;
            BubbleTargetPanel.relativePosition = new Vector3(4f, 35f);
            BubbleTargetIcon = BubbleTargetPanel.AddUIComponent<UIButton>();
            BubbleTargetIcon.width = 28f;
            BubbleTargetIcon.height = 28f;
            BubbleTargetIcon.normalBgSprite = "LocationMarkerNormal";
            BubbleTargetIcon.hoveredBgSprite = "LocationMarkerHovered";
            BubbleTargetIcon.focusedBgSprite = "LocationMarkerFocused";
            BubbleTargetIcon.pressedBgSprite = "LocationMarkerPressed";
            BubbleTargetIcon.disabledBgSprite = "LocationMarkerDisabled";
            BubbleTargetIcon.playAudioEvents = true;
            BubbleTargetIcon.relativePosition = new Vector3(4f, 0f);
            BubbleTargetIcon.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(MyInstanceID, eventParam);
            };
            BubbleRow2Panel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleRow2Panel.name = "BubbleRow2Panel";
            BubbleRow2Panel.width = 198f;
            BubbleRow2Panel.height = 34f;
            BubbleRow2Panel.relativePosition = new Vector3(40f, 44f);
            BubbleWealthHealthSprite = BubbleRow2Panel.AddUIComponent<UITextureSprite>();
            BubbleWealthHealthSprite.name = "BubbleWealthHealthSprite";
            BubbleWealthHealthSprite.width = 198f;
            BubbleWealthHealthSprite.height = 34f;
            BubbleWealthHealthSprite.texture = TextureDB.BubbleBgBar1Big;
            BubbleWealthHealthSprite.relativePosition = Vector3.zero;
            BubbleWealthSpritePanel = BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
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
            BubbleHealthSpritePanel = BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
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
            WorkBuildingPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            WorkBuildingPanel.name = "WorkBuildingPanel";
            WorkBuildingPanel.width = 234f;
            WorkBuildingPanel.height = 25f;
            WorkBuildingPanel.relativePosition = new Vector3(4f, 82f);
            BubbleWorkBuildingSprite = WorkBuildingPanel.AddUIComponent<UITextureSprite>();
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
                GoToInstance(WorkPlaceID, eventParam);
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
                GoToInstance(PersonalVehicleID, eventParam);
            };
            BubblePersonalCarButton.BringToFront();
            BubbleActivityPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleActivityPanel.name = "BubbleActivityPanel";
            BubbleActivityPanel.width = 234f;
            BubbleActivityPanel.height = 18f;
            BubbleActivityPanel.relativePosition = new Vector3(4f, WorkBuildingPanel.relativePosition.y + 31f);
            BubbleActivitySprite = BubbleActivityPanel.AddUIComponent<UITextureSprite>();
            BubbleActivitySprite.name = "BubbleActivitySprite";
            BubbleActivitySprite.width = BubbleActivityPanel.width;
            BubbleActivitySprite.height = BubbleActivityPanel.height;
            BubbleActivitySprite.texture = TextureDB.BubbleBg1Special2;
            BubbleActivitySprite.relativePosition = Vector3.zero;
            BubbleActivityVehiclePanel = BubbleActivitySprite.AddUIComponent<UIPanel>();
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
                GoToInstance(MyVehicleID, eventParam);
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
                GoToInstance(MyTargetID, eventParam);
            };
            FavCimsLastActivity.relativePosition = new Vector3(27f, 1f);
            BubbleDistrictPanel = BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            BubbleDistrictPanel.name = "BubbleDistrictPanel";
            BubbleDistrictPanel.width = 234f;
            BubbleDistrictPanel.height = 15f;
            BubbleDistrictPanel.relativePosition = new Vector3(4f, BubbleActivityPanel.relativePosition.y + 21f);
            BubbleDistrictSprite = BubbleDistrictPanel.AddUIComponent<UITextureSprite>();
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
            BubbleDetailsPanel = AddUIComponent<UIPanel>();
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
            BubbleHomePanel = BubbleDetailsPanel.AddUIComponent<UIPanel>();
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
                GoToInstance(CitizenHomeID, eventParam);
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
            BubbleFamilyBarPanel = AddUIComponent<UIPanel>();
            BubbleFamilyBarPanel.name = "BubbleFamilyBarPanel";
            BubbleFamilyBarPanel.width = 236f;
            BubbleFamilyBarPanel.height = 20f;
            BubbleFamilyBarPanel.relativePosition = new Vector3(7f, BubbleDetailsPanel.relativePosition.y + BubbleDetailsPanel.height + 2f);
            BubbleFamilyBarPanelBg = BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
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
                GoToInstance(MyPetID, eventParam);
            };
            BubbleFamilyBarCarButton = BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            BubbleFamilyBarCarButton.name = "BubbleFamilyBarCarButton";
            BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
            BubbleFamilyBarCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            BubbleFamilyBarCarButton.relativePosition = new Vector3(BubbleFamilyBarDogButton.relativePosition.x + BubbleFamilyBarDogButton.width + 10f, 0f);
            BubbleFamilyBarCarButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(FamilyVehicleID, eventParam);
            };
            BubbleFamilyPanel = AddUIComponent<UIPanel>();
            BubbleFamilyPanel.name = "BubbleFamilyPanel";
            BubbleFamilyPanel.width = 236f;
            BubbleFamilyPanel.height = 212f;
            BubbleFamilyPanel.clipChildren = true;
            BubbleFamilyPanel.padding = new RectOffset(0, 0, 0, 0);
            BubbleFamilyPanel.autoLayout = true;
            BubbleFamilyPanel.autoLayoutDirection = LayoutDirection.Vertical;
            BubbleFamilyPanel.relativePosition = new Vector3(7f, BubbleFamilyBarPanel.relativePosition.y + BubbleFamilyBarPanel.height);
            NoPartnerPanel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            NoPartnerPanel.name = "NoPartnerPanel";
            NoPartnerPanel.width = BubbleFamilyPanel.width;
            NoPartnerPanel.height = 52f;
            NoPartnerPanel.Hide();
            NoPartnerBSprite = NoPartnerPanel.AddUIComponent<UIButton>();
            NoPartnerBSprite.name = "NoPartnerBSprite";
            NoPartnerBSprite.normalBgSprite = "InfoIconHealthDisabled";
            NoPartnerBSprite.width = 36f;
            NoPartnerBSprite.height = 36f;
            NoPartnerBSprite.relativePosition = new Vector3(7f, 5f);
            NoPartnerFButton = NoPartnerPanel.AddUIComponent<UIButton>();
            NoPartnerFButton.name = "NoPartnerFButton";
            NoPartnerFButton.width = 155f;
            NoPartnerFButton.height = NoPartnerBSprite.height;
            NoPartnerFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            NoPartnerFButton.textHorizontalAlignment = 0;
            NoPartnerFButton.playAudioEvents = false;
            NoPartnerFButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            NoPartnerFButton.font.size = 16;
            NoPartnerFButton.textScale = 0.9f;
            NoPartnerFButton.wordWrap = true;
            NoPartnerFButton.useDropShadow = true;
            NoPartnerFButton.dropShadowOffset = new Vector2(1f, -1f);
            NoPartnerFButton.dropShadowColor = new Color32(0, 0, 0, 0);
            NoPartnerFButton.textPadding.left = 5;
            NoPartnerFButton.textPadding.right = 5;
            NoPartnerFButton.isEnabled = false;
            NoPartnerFButton.disabledTextColor = new Color32(51, 51, 51, 160);
            NoPartnerFButton.relativePosition = new Vector3(NoPartnerBSprite.relativePosition.x + NoPartnerBSprite.width, NoPartnerBSprite.relativePosition.y);
            PartnerPanel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            PartnerPanel.name = "PartnerPanel";
            PartnerPanel.width = BubbleFamilyPanel.width;
            PartnerPanel.height = 52f;
            PartnerPanel.clipChildren = true;
            PartnerPanel.padding = new RectOffset(0, 0, 0, 0);
            PartnerPanel.autoLayout = true;
            PartnerPanel.autoLayoutDirection = LayoutDirection.Vertical;
            BubblePartnerBgBar = PartnerPanel.AddUIComponent<UITextureSprite>();
            BubblePartnerBgBar.name = "BubblePartnerBgBar";
            BubblePartnerBgBar.width = PartnerPanel.width;
            BubblePartnerBgBar.height = 26f;
            BubblePartnerBgBar.texture = TextureDB.BubbleBgBar1;
            BubblePartnerLove = BubblePartnerBgBar.AddUIComponent<UIButton>();
            BubblePartnerLove.name = "BubblePartnerLove";
            BubblePartnerLove.normalBgSprite = "InfoIconHealth";
            BubblePartnerLove.width = 22f;
            BubblePartnerLove.height = 22f;
            BubblePartnerLove.isInteractive = false;
            BubblePartnerLove.relativePosition = new Vector3(7f, 2f);
            BubblePartnerName = BubblePartnerBgBar.AddUIComponent<UIButton>();
            BubblePartnerName.name = "BubblePartnerName";
            BubblePartnerName.width = 135f;
            BubblePartnerName.height = BubblePartnerBgBar.height;
            BubblePartnerName.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubblePartnerName.textHorizontalAlignment = 0;
            BubblePartnerName.playAudioEvents = true;
            BubblePartnerName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubblePartnerName.font.size = 15;
            BubblePartnerName.textScale = 0.8f;
            BubblePartnerName.wordWrap = true;
            BubblePartnerName.useDropShadow = true;
            BubblePartnerName.dropShadowOffset = new Vector2(1f, -1f);
            BubblePartnerName.dropShadowColor = new Color32(0, 0, 0, 0);
            BubblePartnerName.textPadding.left = 5;
            BubblePartnerName.textPadding.right = 5;
            BubblePartnerName.textColor = new Color32(204, 204, 51, 40);
            BubblePartnerName.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubblePartnerName.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubblePartnerName.focusedTextColor = new Color32(153, 0, 0, 0);
            BubblePartnerName.disabledTextColor = new Color32(51, 51, 51, 160);
            BubblePartnerName.relativePosition = new Vector3(BubblePartnerLove.relativePosition.x + BubblePartnerLove.width, 2f);
            BubblePartnerName.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(PartnerID, eventParam);
            };
            BubbleParnerAgeButton = BubblePartnerBgBar.AddUIComponent<UIButton>();
            BubbleParnerAgeButton.name = "BubbleParnerAgeButton";
            BubbleParnerAgeButton.width = 23f;
            BubbleParnerAgeButton.height = 18f;
            BubbleParnerAgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleParnerAgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleParnerAgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleParnerAgeButton.textScale = 0.9f;
            BubbleParnerAgeButton.font.size = 15;
            BubbleParnerAgeButton.dropShadowOffset = new Vector2(1f, -1f);
            BubbleParnerAgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleParnerAgeButton.isInteractive = false;
            BubbleParnerAgeButton.relativePosition = new Vector3(BubblePartnerName.relativePosition.x + BubblePartnerName.width + 6f, 6f);
            BubblePartnerFollowToggler = BubblePartnerBgBar.AddUIComponent<UIButton>();
            BubblePartnerFollowToggler.name = "BubblePartnerFollowToggler";
            BubblePartnerFollowToggler.atlas = favCimsAtlas;
            BubblePartnerFollowToggler.size = new Vector2(18f, 18f);
            BubblePartnerFollowToggler.playAudioEvents = true;
            BubblePartnerFollowToggler.relativePosition = new Vector3(BubbleParnerAgeButton.relativePosition.x + BubbleParnerAgeButton.width + 12f, 4f);
            BubblePartnerFollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(PartnerID);
            };
            BubblePartnerActivityBar = PartnerPanel.AddUIComponent<UITextureSprite>();
            BubblePartnerActivityBar.name = "BubblePartnerActivityBar";
            BubblePartnerActivityBar.width = PartnerPanel.width;
            BubblePartnerActivityBar.height = 26f;
            BubblePartnerActivityBar.texture = TextureDB.BubbleBgBar2;
            BubblePartnerVehicleButton = BubblePartnerActivityBar.AddUIComponent<UIButton>();
            BubblePartnerVehicleButton.name = "BubblePartnerVehicleButton";
            BubblePartnerVehicleButton.width = 22f;
            BubblePartnerVehicleButton.height = 22f;
            BubblePartnerVehicleButton.relativePosition = new Vector3(7f, 2f);
            BubblePartnerVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(PartnerVehID, eventParam);
            };
            BubblePartnerDestination = BubblePartnerActivityBar.AddUIComponent<UIButton>();
            BubblePartnerDestination.name = "BubblePartnerDestination";
            BubblePartnerDestination.width = BubblePartnerActivityBar.width - BubblePartnerVehicleButton.width;
            BubblePartnerDestination.height = BubblePartnerActivityBar.height;
            BubblePartnerDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubblePartnerDestination.textHorizontalAlignment = 0;
            BubblePartnerDestination.playAudioEvents = true;
            BubblePartnerDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubblePartnerDestination.font.size = 15;
            BubblePartnerDestination.textScale = 0.75f;
            BubblePartnerDestination.wordWrap = true;
            BubblePartnerDestination.textPadding.left = 0;
            BubblePartnerDestination.textPadding.right = 5;
            BubblePartnerDestination.outlineColor = new Color32(0, 0, 0, 0);
            BubblePartnerDestination.outlineSize = 1;
            BubblePartnerDestination.textColor = new Color32(21, 59, 96, 140);
            BubblePartnerDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubblePartnerDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            BubblePartnerDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubblePartnerDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            BubblePartnerDestination.useDropShadow = true;
            BubblePartnerDestination.dropShadowOffset = new Vector2(1f, -1f);
            BubblePartnerDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            BubblePartnerDestination.maximumSize = new Vector2(BubblePartnerDestination.width, BubblePartnerActivityBar.height);
            BubblePartnerDestination.relativePosition = new Vector3(BubblePartnerVehicleButton.relativePosition.x + BubblePartnerVehicleButton.width + 5f, 2f);
            BubblePartnerDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(PartnerTarget, eventParam);
            };
            Parent1Panel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            Parent1Panel.name = "PartnerPanel";
            Parent1Panel.width = BubbleFamilyPanel.width;
            Parent1Panel.height = 52f;
            Parent1Panel.clipChildren = true;
            Parent1Panel.padding = new RectOffset(0, 0, 0, 0);
            Parent1Panel.autoLayout = true;
            Parent1Panel.autoLayoutDirection = LayoutDirection.Vertical;
            Parent1Panel.relativePosition = new Vector3(0f, 0f);
            Parent1Panel.Hide();
            BubbleParent1BgBar = Parent1Panel.AddUIComponent<UITextureSprite>();
            BubbleParent1BgBar.name = "BubbleParent1BgBar";
            BubbleParent1BgBar.width = Parent1Panel.width;
            BubbleParent1BgBar.height = 26f;
            BubbleParent1BgBar.texture = TextureDB.BubbleBgBar1;
            BubbleParent1Love = BubbleParent1BgBar.AddUIComponent<UIButton>();
            BubbleParent1Love.name = "BubbleParent1Love";
            BubbleParent1Love.normalBgSprite = "InfoIconAge";
            BubbleParent1Love.width = 22f;
            BubbleParent1Love.height = 22f;
            BubbleParent1Love.isInteractive = false;
            BubbleParent1Love.relativePosition = new Vector3(7f, 2f);
            BubbleParent1Name = BubbleParent1BgBar.AddUIComponent<UIButton>();
            BubbleParent1Name.name = "BubbleParent1Name";
            BubbleParent1Name.width = 135f;
            BubbleParent1Name.height = BubbleParent1BgBar.height;
            BubbleParent1Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleParent1Name.textHorizontalAlignment = 0;
            BubbleParent1Name.playAudioEvents = true;
            BubbleParent1Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleParent1Name.font.size = 15;
            BubbleParent1Name.textScale = 0.8f;
            BubbleParent1Name.wordWrap = true;
            BubbleParent1Name.useDropShadow = true;
            BubbleParent1Name.dropShadowOffset = new Vector2(1f, -1f);
            BubbleParent1Name.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleParent1Name.textPadding.left = 5;
            BubbleParent1Name.textPadding.right = 5;
            BubbleParent1Name.textColor = new Color32(204, 204, 51, 40);
            BubbleParent1Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleParent1Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleParent1Name.focusedTextColor = new Color32(153, 0, 0, 0);
            BubbleParent1Name.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleParent1Name.relativePosition = new Vector3(BubbleParent1Love.relativePosition.x + BubbleParent1Love.width, 2f);
            BubbleParent1Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(Parent1ID, eventParam);
            };
            BubbleParent1AgeButton = BubbleParent1BgBar.AddUIComponent<UIButton>();
            BubbleParent1AgeButton.name = "BubbleParent1AgeButton";
            BubbleParent1AgeButton.width = 23f;
            BubbleParent1AgeButton.height = 18f;
            BubbleParent1AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleParent1AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleParent1AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleParent1AgeButton.textScale = 0.9f;
            BubbleParent1AgeButton.font.size = 15;
            BubbleParent1AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            BubbleParent1AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleParent1AgeButton.isInteractive = false;
            BubbleParent1AgeButton.relativePosition = new Vector3(BubbleParent1Name.relativePosition.x + BubbleParent1Name.width + 6f, 6f);
            BubbleParent1FollowToggler = BubbleParent1BgBar.AddUIComponent<UIButton>();
            BubbleParent1FollowToggler.name = "BubbleParent1FollowToggler";
            BubbleParent1FollowToggler.atlas = favCimsAtlas;
            BubbleParent1FollowToggler.size = new Vector2(18f, 18f);
            BubbleParent1FollowToggler.playAudioEvents = true;
            BubbleParent1FollowToggler.relativePosition = new Vector3(BubbleParent1AgeButton.relativePosition.x + BubbleParent1AgeButton.width + 12f, 4f);
            BubbleParent1FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(Parent1ID);
            };
            BubbleParent1ActivityBar = Parent1Panel.AddUIComponent<UITextureSprite>();
            BubbleParent1ActivityBar.name = "BubbleParent1ActivityBar";
            BubbleParent1ActivityBar.width = Parent1Panel.width;
            BubbleParent1ActivityBar.height = 26f;
            BubbleParent1ActivityBar.texture = TextureDB.BubbleBgBar2;
            BubbleParent1VehicleButton = BubbleParent1ActivityBar.AddUIComponent<UIButton>();
            BubbleParent1VehicleButton.name = "BubbleParent1VehicleButton";
            BubbleParent1VehicleButton.width = 22f;
            BubbleParent1VehicleButton.height = 22f;
            BubbleParent1VehicleButton.relativePosition = new Vector3(7f, 2f);
            BubbleParent1VehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent1VehID, eventParam);
            };
            BubbleParent1Destination = BubbleParent1ActivityBar.AddUIComponent<UIButton>();
            BubbleParent1Destination.name = "BubbleParent1Destination";
            BubbleParent1Destination.width = BubbleParent1ActivityBar.width - BubbleParent1VehicleButton.width;
            BubbleParent1Destination.height = BubbleParent1ActivityBar.height;
            BubbleParent1Destination.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleParent1Destination.textHorizontalAlignment = 0;
            BubbleParent1Destination.playAudioEvents = true;
            BubbleParent1Destination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleParent1Destination.font.size = 15;
            BubbleParent1Destination.textScale = 0.75f;
            BubbleParent1Destination.wordWrap = true;
            BubbleParent1Destination.textPadding.left = 0;
            BubbleParent1Destination.textPadding.right = 5;
            BubbleParent1Destination.outlineColor = new Color32(0, 0, 0, 0);
            BubbleParent1Destination.outlineSize = 1;
            BubbleParent1Destination.textColor = new Color32(21, 59, 96, 140);
            BubbleParent1Destination.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleParent1Destination.pressedTextColor = new Color32(153, 0, 0, 0);
            BubbleParent1Destination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleParent1Destination.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleParent1Destination.useDropShadow = true;
            BubbleParent1Destination.dropShadowOffset = new Vector2(1f, -1f);
            BubbleParent1Destination.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleParent1Destination.maximumSize = new Vector2(BubbleParent1Destination.width, BubbleParent1ActivityBar.height);
            BubbleParent1Destination.relativePosition = new Vector3(BubblePartnerDestination.relativePosition.x, 2f);
            BubbleParent1Destination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent1Target, eventParam);
            };
            NoChildsPanel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            NoChildsPanel.name = "NoChildsPanel";
            NoChildsPanel.width = BubbleFamilyPanel.width;
            NoChildsPanel.height = 52f;
            NoChildsPanel.Hide();
            NoChildsBSprite = NoChildsPanel.AddUIComponent<UIButton>();
            NoChildsBSprite.name = "NoChildsBSprite";
            NoChildsBSprite.normalBgSprite = "InfoIconHappinessDisabled";
            NoChildsBSprite.width = 36f;
            NoChildsBSprite.height = 36f;
            NoChildsBSprite.relativePosition = new Vector3(7f, 5f);
            NoChildsFButton = NoChildsPanel.AddUIComponent<UIButton>();
            NoChildsFButton.name = "NoChildsFButton";
            NoChildsFButton.width = 155f;
            NoChildsFButton.height = NoChildsBSprite.height;
            NoChildsFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            NoChildsFButton.textHorizontalAlignment = 0;
            NoChildsFButton.playAudioEvents = false;
            NoChildsFButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            NoChildsFButton.font.size = 16;
            NoChildsFButton.textScale = 0.9f;
            NoChildsFButton.wordWrap = true;
            NoChildsFButton.useDropShadow = true;
            NoChildsFButton.dropShadowOffset = new Vector2(1f, -1f);
            NoChildsFButton.dropShadowColor = new Color32(0, 0, 0, 0);
            NoChildsFButton.textPadding.left = 5;
            NoChildsFButton.textPadding.right = 5;
            NoChildsFButton.isEnabled = false;
            NoChildsFButton.disabledTextColor = new Color32(51, 51, 51, 160);
            NoChildsFButton.relativePosition = new Vector3(NoChildsBSprite.relativePosition.x + NoChildsBSprite.width, NoChildsBSprite.relativePosition.y);
            FamilyMember2Panel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            FamilyMember2Panel.name = "FamilyMember2Panel";
            FamilyMember2Panel.width = BubbleFamilyPanel.width;
            FamilyMember2Panel.height = 52f;
            FamilyMember2Panel.clipChildren = true;
            FamilyMember2Panel.padding = new RectOffset(0, 0, 0, 0);
            FamilyMember2Panel.autoLayout = true;
            FamilyMember2Panel.autoLayoutDirection = LayoutDirection.Vertical;
            FamilyMember2Panel.relativePosition = new Vector3(0f, 0f);
            FamilyMember2Panel.Hide();
            BubbleFamilyMember2BgBar = FamilyMember2Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember2BgBar.name = "BubbleFamilyMember2BgBar";
            BubbleFamilyMember2BgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember2BgBar.height = 26f;
            BubbleFamilyMember2BgBar.texture = TextureDB.BubbleBgBar1;
            BubbleFamilyMember2IconSprite = BubbleFamilyMember2BgBar.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember2IconSprite.name = "BubbleFamilyMember2IconSprite";
            BubbleFamilyMember2IconSprite.width = 18f;
            BubbleFamilyMember2IconSprite.height = 18f;
            BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            BubbleFamilyMember2IconSprite.relativePosition = new Vector3(7f, 4f);
            BubbleFamilyMember2Name = BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember2Name.name = "BubbleFamilyMember2Name";
            BubbleFamilyMember2Name.width = 135f;
            BubbleFamilyMember2Name.height = BubbleFamilyMember2BgBar.height;
            BubbleFamilyMember2Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember2Name.textHorizontalAlignment = 0;
            BubbleFamilyMember2Name.playAudioEvents = true;
            BubbleFamilyMember2Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember2Name.font.size = 15;
            BubbleFamilyMember2Name.textScale = 0.8f;
            BubbleFamilyMember2Name.wordWrap = true;
            BubbleFamilyMember2Name.useDropShadow = true;
            BubbleFamilyMember2Name.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember2Name.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember2Name.textPadding.left = 5;
            BubbleFamilyMember2Name.textPadding.right = 5;
            BubbleFamilyMember2Name.textColor = new Color32(204, 204, 51, 40);
            BubbleFamilyMember2Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember2Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember2Name.focusedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember2Name.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember2Name.relativePosition = new Vector3(BubbleFamilyMember2IconSprite.relativePosition.x + BubbleFamilyMember2IconSprite.width + 2f, 2f);
            BubbleFamilyMember2Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(Parent2ID, eventParam);
            };
            BubbleFamilyMember2AgeButton = BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember2AgeButton.name = "BubbleFamilyMember2AgeButton";
            BubbleFamilyMember2AgeButton.width = 23f;
            BubbleFamilyMember2AgeButton.height = 18f;
            BubbleFamilyMember2AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleFamilyMember2AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember2AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember2AgeButton.textScale = 0.9f;
            BubbleFamilyMember2AgeButton.font.size = 15;
            BubbleFamilyMember2AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember2AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember2AgeButton.isInteractive = false;
            BubbleFamilyMember2AgeButton.relativePosition = new Vector3(BubbleFamilyMember2Name.relativePosition.x + BubbleFamilyMember2Name.width + 6f, 6f);
            BubbleFamilyMember2FollowToggler = BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember2FollowToggler.name = "BubbleFamilyMember2FollowToggler";
            BubbleFamilyMember2FollowToggler.atlas = favCimsAtlas;
            BubbleFamilyMember2FollowToggler.size = new Vector2(18f, 18f);
            BubbleFamilyMember2FollowToggler.playAudioEvents = true;
            BubbleFamilyMember2FollowToggler.relativePosition = new Vector3(BubbleFamilyMember2AgeButton.relativePosition.x + BubbleFamilyMember2AgeButton.width + 12f, 4f);
            BubbleFamilyMember2FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(Parent2ID);
            };
            BubbleFamilyMember2ActivityBgBar = FamilyMember2Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember2ActivityBgBar.name = "BubbleFamilyMember2ActivityBgBar";
            BubbleFamilyMember2ActivityBgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember2ActivityBgBar.height = 26f;
            BubbleFamilyMember2ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            BubbleFamilyMember2ActivityVehicleButton = BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember2ActivityVehicleButton.name = "BubbleFamilyMember2ActivityVehicleButton";
            BubbleFamilyMember2ActivityVehicleButton.width = 22f;
            BubbleFamilyMember2ActivityVehicleButton.height = 22f;
            BubbleFamilyMember2ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            BubbleFamilyMember2ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent2VehID, eventParam);
            };
            BubbleFamilyMember2ActivityDestination = BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember2ActivityDestination.name = "BubbleFamilyMember2ActivityDestination";
            BubbleFamilyMember2ActivityDestination.width = BubbleFamilyMember2ActivityBgBar.width - BubbleFamilyMember2ActivityVehicleButton.width;
            BubbleFamilyMember2ActivityDestination.height = BubbleFamilyMember2ActivityBgBar.height;
            BubbleFamilyMember2ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember2ActivityDestination.textHorizontalAlignment = 0;
            BubbleFamilyMember2ActivityDestination.playAudioEvents = true;
            BubbleFamilyMember2ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember2ActivityDestination.font.size = 15;
            BubbleFamilyMember2ActivityDestination.textScale = 0.75f;
            BubbleFamilyMember2ActivityDestination.wordWrap = true;
            BubbleFamilyMember2ActivityDestination.textPadding.left = 0;
            BubbleFamilyMember2ActivityDestination.textPadding.right = 5;
            BubbleFamilyMember2ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember2ActivityDestination.outlineSize = 1;
            BubbleFamilyMember2ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            BubbleFamilyMember2ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember2ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember2ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember2ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember2ActivityDestination.useDropShadow = true;
            BubbleFamilyMember2ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember2ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember2ActivityDestination.maximumSize = new Vector2(BubbleFamilyMember2ActivityDestination.width, BubbleFamilyMember2ActivityBgBar.height);
            BubbleFamilyMember2ActivityDestination.relativePosition = new Vector3(BubblePartnerDestination.relativePosition.x, 2f);
            BubbleFamilyMember2ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent2Target, eventParam);
            };
            FamilyMember3Panel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            FamilyMember3Panel.name = "FamilyMember3Panel";
            FamilyMember3Panel.width = BubbleFamilyPanel.width;
            FamilyMember3Panel.height = 52f;
            FamilyMember3Panel.clipChildren = true;
            FamilyMember3Panel.padding = new RectOffset(0, 0, 0, 0);
            FamilyMember3Panel.autoLayout = true;
            FamilyMember3Panel.autoLayoutDirection = LayoutDirection.Vertical;
            FamilyMember3Panel.relativePosition = new Vector3(0f, 0f);
            FamilyMember3Panel.Hide();
            BubbleFamilyMember3BgBar = FamilyMember3Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember3BgBar.name = "BubbleFamilyMember3BgBar";
            BubbleFamilyMember3BgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember3BgBar.height = 26f;
            BubbleFamilyMember3BgBar.texture = TextureDB.BubbleBgBar1;
            BubbleFamilyMember3IconSprite = BubbleFamilyMember3BgBar.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember3IconSprite.name = "BubbleFamilyMember3IconSprite";
            BubbleFamilyMember3IconSprite.width = 18f;
            BubbleFamilyMember3IconSprite.height = 18f;
            BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            BubbleFamilyMember3IconSprite.relativePosition = new Vector3(7f, 4f);
            BubbleFamilyMember3Name = BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember3Name.name = "BubbleFamilyMember3Name";
            BubbleFamilyMember3Name.width = 135f;
            BubbleFamilyMember3Name.height = BubbleFamilyMember3BgBar.height;
            BubbleFamilyMember3Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember3Name.textHorizontalAlignment = 0;
            BubbleFamilyMember3Name.playAudioEvents = true;
            BubbleFamilyMember3Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember3Name.font.size = 15;
            BubbleFamilyMember3Name.textScale = 0.8f;
            BubbleFamilyMember3Name.wordWrap = true;
            BubbleFamilyMember3Name.useDropShadow = true;
            BubbleFamilyMember3Name.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember3Name.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember3Name.textPadding.left = 5;
            BubbleFamilyMember3Name.textPadding.right = 5;
            BubbleFamilyMember3Name.textColor = new Color32(204, 204, 51, 40);
            BubbleFamilyMember3Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember3Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember3Name.focusedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember3Name.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember3Name.relativePosition = new Vector3(BubbleFamilyMember3IconSprite.relativePosition.x + BubbleFamilyMember3IconSprite.width + 2f, 2f);
            BubbleFamilyMember3Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(Parent3ID, eventParam);
            };
            BubbleFamilyMember3AgeButton = BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember3AgeButton.name = "BubbleFamilyMember3AgeButton";
            BubbleFamilyMember3AgeButton.width = 23f;
            BubbleFamilyMember3AgeButton.height = 18f;
            BubbleFamilyMember3AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleFamilyMember3AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember3AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember3AgeButton.textScale = 0.9f;
            BubbleFamilyMember3AgeButton.font.size = 15;
            BubbleFamilyMember3AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember3AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember3AgeButton.isInteractive = false;
            BubbleFamilyMember3AgeButton.relativePosition = new Vector3(BubbleFamilyMember3Name.relativePosition.x + BubbleFamilyMember3Name.width + 6f, 6f);
            BubbleFamilyMember3FollowToggler = BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember3FollowToggler.name = "BubbleFamilyMember3FollowToggler";
            BubbleFamilyMember3FollowToggler.atlas = favCimsAtlas;
            BubbleFamilyMember3FollowToggler.size = new Vector2(18f, 18f);
            BubbleFamilyMember3FollowToggler.playAudioEvents = true;
            BubbleFamilyMember3FollowToggler.relativePosition = new Vector3(BubbleFamilyMember3AgeButton.relativePosition.x + BubbleFamilyMember3AgeButton.width + 12f, 4f);
            BubbleFamilyMember3FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(Parent3ID);
            };
            BubbleFamilyMember3ActivityBgBar = FamilyMember3Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember3ActivityBgBar.name = "BubbleFamilyMember3ActivityBgBar";
            BubbleFamilyMember3ActivityBgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember3ActivityBgBar.height = 26f;
            BubbleFamilyMember3ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            BubbleFamilyMember3ActivityVehicleButton = BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember3ActivityVehicleButton.name = "BubbleFamilyMember3ActivityVehicleButton";
            BubbleFamilyMember3ActivityVehicleButton.width = 22f;
            BubbleFamilyMember3ActivityVehicleButton.height = 22f;
            BubbleFamilyMember3ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            BubbleFamilyMember3ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent3VehID, eventParam);
            };
            BubbleFamilyMember3ActivityDestination = BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember3ActivityDestination.name = "BubbleFamilyMember3ActivityDestination";
            BubbleFamilyMember3ActivityDestination.width = BubbleFamilyMember3ActivityBgBar.width - BubbleFamilyMember3ActivityVehicleButton.width;
            BubbleFamilyMember3ActivityDestination.height = BubbleFamilyMember3ActivityBgBar.height;
            BubbleFamilyMember3ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember3ActivityDestination.textHorizontalAlignment = 0;
            BubbleFamilyMember3ActivityDestination.playAudioEvents = true;
            BubbleFamilyMember3ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember3ActivityDestination.font.size = 15;
            BubbleFamilyMember3ActivityDestination.textScale = 0.75f;
            BubbleFamilyMember3ActivityDestination.wordWrap = true;
            BubbleFamilyMember3ActivityDestination.textPadding.left = 0;
            BubbleFamilyMember3ActivityDestination.textPadding.right = 5;
            BubbleFamilyMember3ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember3ActivityDestination.outlineSize = 1;
            BubbleFamilyMember3ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            BubbleFamilyMember3ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember3ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember3ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember3ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember3ActivityDestination.useDropShadow = true;
            BubbleFamilyMember3ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember3ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember3ActivityDestination.maximumSize = new Vector2(BubbleFamilyMember3ActivityDestination.width, BubbleFamilyMember3ActivityBgBar.height);
            BubbleFamilyMember3ActivityDestination.relativePosition = new Vector3(BubblePartnerDestination.relativePosition.x, 2f);
            BubbleFamilyMember3ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent3Target, eventParam);
            };
            FamilyMember4Panel = BubbleFamilyPanel.AddUIComponent<UIPanel>();
            FamilyMember4Panel.name = "FamilyMember4Panel";
            FamilyMember4Panel.width = BubbleFamilyPanel.width;
            FamilyMember4Panel.height = 52f;
            FamilyMember4Panel.clipChildren = true;
            FamilyMember4Panel.padding = new RectOffset(0, 0, 0, 0);
            FamilyMember4Panel.autoLayout = true;
            FamilyMember4Panel.autoLayoutDirection = LayoutDirection.Vertical;
            FamilyMember4Panel.relativePosition = new Vector3(0f, 0f);
            FamilyMember4Panel.Hide();
            BubbleFamilyMember4BgBar = FamilyMember4Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember4BgBar.name = "BubbleFamilyMember4BgBar";
            BubbleFamilyMember4BgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember4BgBar.height = 26f;
            BubbleFamilyMember4BgBar.texture = TextureDB.BubbleBgBar1;
            BubbleFamilyMember4IconSprite = BubbleFamilyMember4BgBar.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember4IconSprite.name = "BubbleFamilyMember4IconSprite";
            BubbleFamilyMember4IconSprite.width = 18f;
            BubbleFamilyMember4IconSprite.height = 18f;
            BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            BubbleFamilyMember4IconSprite.relativePosition = new Vector3(7f, 4f);
            BubbleFamilyMember4Name = BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember4Name.name = "BubbleFamilyMember4Name";
            BubbleFamilyMember4Name.width = 135f;
            BubbleFamilyMember4Name.height = BubbleFamilyMember4BgBar.height;
            BubbleFamilyMember4Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember4Name.textHorizontalAlignment = 0;
            BubbleFamilyMember4Name.playAudioEvents = true;
            BubbleFamilyMember4Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember4Name.font.size = 15;
            BubbleFamilyMember4Name.textScale = 0.8f;
            BubbleFamilyMember4Name.wordWrap = true;
            BubbleFamilyMember4Name.useDropShadow = true;
            BubbleFamilyMember4Name.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember4Name.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember4Name.textPadding.left = 5;
            BubbleFamilyMember4Name.textPadding.right = 5;
            BubbleFamilyMember4Name.textColor = new Color32(204, 204, 51, 40);
            BubbleFamilyMember4Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember4Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember4Name.focusedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember4Name.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember4Name.relativePosition = new Vector3(BubbleFamilyMember4IconSprite.relativePosition.x + BubbleFamilyMember4IconSprite.width + 2f, 2f);
            BubbleFamilyMember4Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToCitizen(Parent4ID, eventParam);
            };
            BubbleFamilyMember4AgeButton = BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember4AgeButton.name = "BubbleFamilyMember4AgeButton";
            BubbleFamilyMember4AgeButton.width = 23f;
            BubbleFamilyMember4AgeButton.height = 18f;
            BubbleFamilyMember4AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            BubbleFamilyMember4AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember4AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember4AgeButton.textScale = 0.9f;
            BubbleFamilyMember4AgeButton.font.size = 15;
            BubbleFamilyMember4AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember4AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember4AgeButton.isInteractive = false;
            BubbleFamilyMember4AgeButton.relativePosition = new Vector3(BubbleFamilyMember4Name.relativePosition.x + BubbleFamilyMember4Name.width + 6f, 6f);
            BubbleFamilyMember4FollowToggler = BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember4FollowToggler.name = "BubbleFamilyMember4FollowToggler";
            BubbleFamilyMember4FollowToggler.atlas = favCimsAtlas;
            BubbleFamilyMember4FollowToggler.width = 18f;
            BubbleFamilyMember4FollowToggler.height = 18f;
            BubbleFamilyMember4FollowToggler.playAudioEvents = true;
            BubbleFamilyMember4FollowToggler.relativePosition = new Vector3(BubbleFamilyMember4AgeButton.relativePosition.x + BubbleFamilyMember4AgeButton.width + 12f, 4f);
            BubbleFamilyMember4FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(Parent4ID);
            };
            BubbleFamilyMember4ActivityBgBar = FamilyMember4Panel.AddUIComponent<UITextureSprite>();
            BubbleFamilyMember4ActivityBgBar.name = "BubbleFamilyMember4ActivityBgBar";
            BubbleFamilyMember4ActivityBgBar.width = BubbleFamilyPanel.width;
            BubbleFamilyMember4ActivityBgBar.height = 26f;
            BubbleFamilyMember4ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            BubbleFamilyMember4ActivityVehicleButton = BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember4ActivityVehicleButton.name = "BubbleFamilyMember4ActivityVehicleButton";
            BubbleFamilyMember4ActivityVehicleButton.width = 22f;
            BubbleFamilyMember4ActivityVehicleButton.height = 22f;
            BubbleFamilyMember4ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            BubbleFamilyMember4ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent4VehID, eventParam);
            };
            BubbleFamilyMember4ActivityDestination = BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton>();
            BubbleFamilyMember4ActivityDestination.name = "BubbleFamilyMember4ActivityDestination";
            BubbleFamilyMember4ActivityDestination.width = BubbleFamilyMember4ActivityBgBar.width - BubbleFamilyMember4ActivityVehicleButton.width;
            BubbleFamilyMember4ActivityDestination.height = BubbleFamilyMember4ActivityBgBar.height;
            BubbleFamilyMember4ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            BubbleFamilyMember4ActivityDestination.textHorizontalAlignment = 0;
            BubbleFamilyMember4ActivityDestination.playAudioEvents = true;
            BubbleFamilyMember4ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            BubbleFamilyMember4ActivityDestination.font.size = 15;
            BubbleFamilyMember4ActivityDestination.textScale = 0.75f;
            BubbleFamilyMember4ActivityDestination.wordWrap = true;
            BubbleFamilyMember4ActivityDestination.textPadding.left = 0;
            BubbleFamilyMember4ActivityDestination.textPadding.right = 5;
            BubbleFamilyMember4ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember4ActivityDestination.outlineSize = 1;
            BubbleFamilyMember4ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            BubbleFamilyMember4ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            BubbleFamilyMember4ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            BubbleFamilyMember4ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            BubbleFamilyMember4ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            BubbleFamilyMember4ActivityDestination.useDropShadow = true;
            BubbleFamilyMember4ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            BubbleFamilyMember4ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            BubbleFamilyMember4ActivityDestination.maximumSize = new Vector2(BubbleFamilyMember4ActivityDestination.width, BubbleFamilyMember4ActivityBgBar.height);
            BubbleFamilyMember4ActivityDestination.relativePosition = new Vector3(BubblePartnerDestination.relativePosition.x, 2f);
            BubbleFamilyMember4ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                GoToInstance(Parent4Target, eventParam);
            };
            absolutePosition = new Vector3(random.Next(num, num2), 200f);
        }

        public override void Update()
        {
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool isEmpty = MyInstanceID.IsEmpty;
                if (!isEmpty)
                {
                    if (MyInstanceID != PrevMyInstanceID)
                    {
                        DogOwner = 0U;
                        FirstRun = true;
                        PrevMyInstanceID = MyInstanceID;
                    }
                    seconds -= 1f * Time.deltaTime;
                    if (seconds <= 0f || FirstRun)
                    {
                        execute = true;
                        seconds = 0.3f;
                    }
                    else
                    {
                        execute = false;
                    }
                }
            }
        }

        public override void LateUpdate()
        {
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                if (execute || FirstRun)
                {
                    bool isVisible = base.isVisible;
                    if (isVisible)
                    {
                        try
                        {
                            citizen = MyInstanceID.Citizen;
                            CitizenData = MyCitizen.m_citizens.m_buffer[(int)citizen];
                            BubbleCitizenAge.text = Translations.Translate("FavCimsAgeColText_text");
                            BubbleCitizenAgePhase.text = Translations.Translate("FavCimsAgePhaseColText_text");
                            BubbleCitizenEducation.text = Translations.Translate("FavCimsEduColText_text");
                            BubbleWealthSprite.tooltip = Translations.Translate("Wealth_Label");
                            FavCimsDistrictLabel.text = Translations.Translate("District_Label");
                            BubbleFamilyBarLabel.text = Translations.Translate("Citizen_Family_unit");
                            NoChildsFButton.text = Translations.Translate("Citizen_Details_No_Childs");
                            NoPartnerFButton.text = Translations.Translate("Citizen_Details_No_Partner");
                            MyCitizenUnit = CitizenData.GetContainingUnit(citizen, MyBuilding.m_buildings.m_buffer[CitizenData.m_homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                            if (MyCitizenUnit > 0U)
                            {
                                Family = MyCitizen.m_units.m_buffer[(int)MyCitizenUnit];
                                BubbleHeaderCitizenName.text = MyCitizen.GetCitizenName(citizen);
                                Citizen.Gender gender = Citizen.GetGender(citizen);
                                if (gender == Citizen.Gender.Female)
                                {
                                    BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    BubbleHeaderCitizenName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    BubbleHeaderCitizenName.hoveredTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    BubbleHeaderCitizenName.pressedTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    BubbleHeaderCitizenName.focusedTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                }
                                else
                                {
                                    BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    BubbleHeaderCitizenName.textColor = new Color32(204, 204, 51, 40);
                                    BubbleHeaderCitizenName.hoveredTextColor = new Color32(204, 204, 51, 40);
                                    BubbleHeaderCitizenName.pressedTextColor = new Color32(204, 204, 51, 40);
                                    BubbleHeaderCitizenName.focusedTextColor = new Color32(204, 204, 51, 40);
                                }
                                int health = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_health;
                                string healthString = GetHealthString(Citizen.GetHealthLevel(health));
                                BubbleHealthSprite.normalBgSprite = healthString;
                                BubbleHealthSprite.tooltip = Locale.Get("INFO_HEALTH_TITLE");
                                BubbleHealthValue.text = Translations.Translate("Health_Level_" + sHealthLevels[(int)Citizen.GetHealthLevel(health)] + "_" + Citizen.GetGender(citizen).ToString());
                                switch (Citizen.GetHealthLevel(health))
                                {
                                    case Citizen.Health.VerySick:
                                        BubbleHealthValue.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        break;
                                    case Citizen.Health.Sick:
                                        BubbleHealthValue.textColor = new Color32(153, 0, 0, 0);
                                        break;
                                    case Citizen.Health.PoorHealth:
                                        BubbleHealthValue.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        break;
                                    case Citizen.Health.Healthy:
                                        BubbleHealthValue.textColor = new Color32(102, 204, 0, 60);
                                        break;
                                    case Citizen.Health.VeryHealthy:
                                        BubbleHealthValue.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                    case Citizen.Health.ExcellentHealth:
                                        BubbleHealthValue.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                }
                                Citizen.Education educationLevel = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].EducationLevel;
                                string text = educationLevel.ToString();
                                BubbleRow1EducationTooltipArea.tooltip = Translations.Translate("Education_" + text + "_" + Citizen.GetGender(citizen).ToString());

                                switch (text)
                                {
                                    case "ThreeSchools":
                                        BubbleEduLevel3.isEnabled = true;
                                        BubbleEduLevel2.isEnabled = true;
                                        BubbleEduLevel1.isEnabled = true;
                                        break;
                                    case "TwoSchools":
                                        BubbleEduLevel3.isEnabled = false;
                                        BubbleEduLevel2.isEnabled = true;
                                        BubbleEduLevel1.isEnabled = true;
                                        break;
                                    case "OneSchool":
                                        BubbleEduLevel3.isEnabled = false;
                                        BubbleEduLevel2.isEnabled = false;
                                        BubbleEduLevel1.isEnabled = true;
                                        break;
                                    default:
                                        BubbleEduLevel3.isEnabled = false;
                                        BubbleEduLevel2.isEnabled = false;
                                        BubbleEduLevel1.isEnabled = false;
                                        break;
                                }

                                int wellbeing = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_wellbeing;
                                string happinessString = GetHappinessString(Citizen.GetHappinessLevel(wellbeing));
                                BubbleRow2WellbeingIcon.normalBgSprite = happinessString;
                                BubbleRow2WellbeingIcon.tooltip = Translations.Translate("WellBeingLabel") + Translations.Translate(happinessString);
                                Citizen.Wealth wealthLevel = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].WealthLevel;
                                BubbleRow2WealthValueVal.tooltip = Translations.Translate("Wealth_Label");
                                if (wealthLevel == 0)
                                {
                                    BubbleRow2WealthValueVal.text = Translations.Translate("Low_Wealth_" + Citizen.GetGender(citizen).ToString());
                                    BubbleRow2WealthValueVal.textColor = new Color32(153, 0, 0, 0);
                                }
                                else
                                {
                                    if (wealthLevel == Citizen.Wealth.Medium)
                                    {
                                        BubbleRow2WealthValueVal.text = Translations.Translate("Mid_Wealth_" + Citizen.GetGender(citizen).ToString());
                                        BubbleRow2WealthValueVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                    }
                                    else
                                    {
                                        BubbleRow2WealthValueVal.text = Translations.Translate("High_Wealth_" + Citizen.GetGender(citizen).ToString());
                                        BubbleRow2WealthValueVal.textColor = new Color32(102, 204, 0, 60);
                                    }
                                }
                                int happiness = Citizen.GetHappiness(health, wellbeing);
                                string happinessString2 = GetHappinessString(Citizen.GetHappinessLevel(happiness));
                                if (MyCitizen.m_citizens.m_buffer[citizen].Arrested && MyCitizen.m_citizens.m_buffer[citizen].Criminal)
                                {
                                    BubbleRow1HappyIcon.atlas = MyAtlas.FavCimsAtlas;
                                    BubbleRow1HappyIcon.normalBgSprite = "FavCimsCrimeArrested";
                                    BubbleRow1HappyIcon.tooltip = Translations.Translate("Citizen_Arrested");
                                }
                                else
                                {
                                    BubbleRow1HappyIcon.atlas = UIView.GetAView().defaultAtlas;
                                    BubbleRow1HappyIcon.normalBgSprite = happinessString2;
                                    BubbleRow1HappyIcon.tooltip = Translations.Translate("HappinessLabel") + Translations.Translate(happinessString2);
                                }
                                int age = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_age;
                                string text2 = Citizen.GetAgeGroup(age).ToString();
                                BubbleCitizenAgePhaseVal.text = Translations.Translate("AgePhase_" + text2 + "_" + Citizen.GetGender(citizen).ToString());
                                RealAge = FavCimsCore.CalculateCitizenAge(age);

                                switch (RealAge)
                                {
                                    case int n when n <= 12:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(83, 166, 0, 60);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(83, 166, 0, 60);
                                        break;
                                    case int n when n <= 19:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(0, 102, 51, 100);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                    case int n when n <= 25:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        break;
                                    case int n when n <= 65:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                        break;
                                    case int n when n <= 90:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(153, 0, 0, 0);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(153, 0, 0, 0);
                                        break;
                                    default:
                                        BubbleCitizenAgeVal.text = RealAge.ToString();
                                        BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        break;
                                }

                                WorkPlace = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_workBuilding;
                                if (MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].GetCurrentSchoolLevel(citizen) != ItemClass.Level.None)
                                {
                                    isStudent = true;
                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                    FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
                                    FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    FavCimsWorkingPlace.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].GetCurrentSchoolLevel(citizen).ToString()) + " " + MyBuilding.GetBuildingName(WorkPlace, MyInstanceID);
                                }
                                else
                                {
                                    if (WorkPlace == 0)
                                    {
                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                        if (age >= 180)
                                        {
                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
                                            FavCimsWorkingPlace.text = Translations.Translate("Citizen_Retired");
                                            FavCimsWorkingPlace.isEnabled = false;
                                            FavCimsWorkingPlace.tooltip = Translations.Translate("Citizen_Retired_tooltip");
                                            FavCimsWorkingPlaceSprite.tooltip = null;
                                            FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                        else
                                        {
                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
                                            FavCimsWorkingPlace.text = Locale.Get("CITIZEN_OCCUPATION_UNEMPLOYED");
                                            FavCimsWorkingPlace.isEnabled = false;
                                            FavCimsWorkingPlace.tooltip = Translations.Translate("Unemployed_tooltip");
                                            FavCimsWorkingPlaceSprite.tooltip = null;
                                            FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                }
                                if (WorkPlace > 0)
                                {
                                    string text3 = string.Empty;
                                    if (!isStudent)
                                    {
                                        CommonBuildingAI commonBuildingAI = MyBuilding.m_buildings.m_buffer[WorkPlace].Info.m_buildingAI as CommonBuildingAI;
                                        if (commonBuildingAI != null)
                                        {
                                            text3 = commonBuildingAI.GetTitle(gender, educationLevel, WorkPlace, citizen);
                                        }
                                        if (text3 == string.Empty)
                                        {
                                            int num = new Randomizer(WorkPlace + citizen).Int32(1, 5);
                                            switch (educationLevel)
                                            {
                                                case Citizen.Education.Uneducated:
                                                    text3 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.OneSchool:
                                                    text3 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.TwoSchools:
                                                    text3 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                                case Citizen.Education.ThreeSchools:
                                                    text3 = Locale.Get((gender != Citizen.Gender.Female) ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                                    break;
                                            }
                                        }
                                    }
                                    WorkPlaceID.Building = WorkPlace;
                                    FavCimsWorkingPlace.text = text3 + " " + MyBuilding.GetBuildingName(WorkPlace, MyInstanceID);
                                    FavCimsWorkingPlace.isEnabled = true;
                                    WorkInfo = MyBuilding.m_buildings.m_buffer[(int)WorkPlaceID.Index].Info;
                                    if (WorkInfo.m_class.m_service == ItemClass.Service.Commercial)
                                    {
                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;

                                        switch (WorkInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.CommercialHigh:
                                                FavCimsWorkingPlace.textColor = new Color32(0, 51, 153, 147);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
                                                break;
                                            case ItemClass.SubService.CommercialEco:
                                                FavCimsWorkingPlace.textColor = new Color32(0, 150, 136, 116);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                FavCimsWorkingPlace.tooltip = Translations.Translate("Buildings_Type_CommercialEco");
                                                break;
                                            case ItemClass.SubService.CommercialLeisure:
                                                FavCimsWorkingPlace.textColor = new Color32(219, 68, 55, 3);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                FavCimsWorkingPlace.tooltip = Translations.Translate("Buildings_Type_CommercialLeisure");
                                                break;
                                            case ItemClass.SubService.CommercialTourist:
                                                FavCimsWorkingPlace.textColor = new Color32(156, 39, 176, 194);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                FavCimsWorkingPlace.tooltip = Translations.Translate("Buildings_Type_CommercialTourist");
                                                break;
                                            default:
                                                FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
                                                FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
                                                FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
                                                break;
                                        }

                                        if (WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow)
                                        {
                                            switch (WorkInfo.m_class.m_level)
                                            {
                                                case ItemClass.Level.Level2:
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[2];
                                                    break;
                                                case ItemClass.Level.Level3:
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsResidentialLevel[3];
                                                    break;
                                                default:
                                                    FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsResidentialLevel[1];
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                    else
                                    {
                                        if (WorkInfo.m_class.m_service == ItemClass.Service.Industrial)
                                        {
                                            FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
                                            ItemClass.SubService subService = WorkInfo.m_class.m_subService;
                                            ItemClass.SubService subService2 = subService;
                                            switch (subService2)
                                            {
                                                case ItemClass.SubService.IndustrialForestry:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                                    break;
                                                case ItemClass.SubService.IndustrialFarming:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                                    break;
                                                case ItemClass.SubService.IndustrialOil:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                                    break;
                                                case ItemClass.SubService.IndustrialOre:
                                                    FavCimsWorkingPlaceSprite.texture = null;
                                                    FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                                    break;
                                                default:
                                                    switch (subService2)
                                                    {
                                                        case ItemClass.SubService.PlayerIndustryForestry:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryFarming:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOil:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOre:
                                                            FavCimsWorkingPlaceSprite.texture = null;
                                                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                                            break;
                                                        default:
                                                            FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                                            FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                                            break;
                                                    }
                                                    break;
                                            }
                                            if (WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                                            {
                                                switch (WorkInfo.m_class.m_level)
                                                {
                                                    case ItemClass.Level.Level2:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[2];
                                                        break;
                                                    case ItemClass.Level.Level3:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[3];
                                                        break;
                                                    default:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[1];
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                        else
                                        {
                                            if (WorkInfo.m_class.m_service == ItemClass.Service.Office)
                                            {
                                                FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
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

                                                switch (WorkInfo.m_class.m_level)
                                                {
                                                    case ItemClass.Level.Level2:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[2];
                                                        break;
                                                    case ItemClass.Level.Level3:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[3];
                                                        break;
                                                    default:
                                                        FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[1];
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                                FavCimsWorkingPlaceSprite.texture = null;
                                                FavCimsWorkingPlace.textColor = new Color32(153, 102, 51, 20);
                                                bool found_service = false;
                                                switch (WorkInfo.m_class.m_service)
                                                {
                                                    case ItemClass.Service.Electricity:
                                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyPowerSaving";
                                                        FavCimsWorkingPlace.tooltip = Translations.Translate("Electricity_job");
                                                        found_service = true;
                                                        break;
                                                    case ItemClass.Service.Water:
                                                        FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
                                                        FavCimsWorkingPlace.tooltip = Translations.Translate("Water_job");
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
                                                            ItemClass.SubService subService5 = WorkInfo.m_class.m_subService;
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
                                    WorkDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)WorkPlaceID.Index].m_position);
                                    if (WorkDistrict == 0)
                                    {
                                        FavCimsWorkingPlaceSprite.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        FavCimsWorkingPlaceSprite.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(WorkDistrict);
                                    }
                                }
                                else
                                {
                                    FavCimsWorkingPlace.isEnabled = false;
                                    FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                    FavCimsWorkingPlaceSprite.tooltip = null;
                                }
                                CitizenHome = MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Index].m_homeBuilding;
                                if (CitizenHome > 0)
                                {
                                    CitizenHomeID.Building = CitizenHome;
                                    BubbleHomeName.text = MyBuilding.GetBuildingName(CitizenHome, MyInstanceID);
                                    BubbleHomeName.isEnabled = true;
                                    BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                    HomeInfo = MyBuilding.m_buildings.m_buffer[(int)CitizenHomeID.Index].Info;
                                    if (HomeInfo.m_class.m_service == ItemClass.Service.Residential)
                                    {
                                        BubbleHomeName.tooltip = null;

                                        switch (HomeInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.ResidentialHigh:
                                                BubbleHomeName.textColor = new Color32(0, 102, 51, 100);
                                                BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
                                                BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString());
                                                break;
                                            case ItemClass.SubService.ResidentialHighEco:
                                                BubbleHomeName.textColor = new Color32(0, 102, 51, 100);
                                                BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
                                                BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString()) + " Eco";
                                                break;
                                            case ItemClass.SubService.ResidentialLowEco:
                                                BubbleHomeName.textColor = new Color32(0, 153, 0, 80);
                                                BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                                BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString()) + " Eco";
                                                break;
                                            case ItemClass.SubService.ResidentialLow:
                                                BubbleHomeName.textColor = new Color32(0, 153, 0, 80);
                                                BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                                BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString());
                                                break;
                                        }

                                        switch (HomeInfo.m_class.m_level)
                                        {
                                            case ItemClass.Level.Level2:
                                                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[2];
                                                break;
                                            case ItemClass.Level.Level3:
                                                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[3];
                                                break;
                                            case ItemClass.Level.Level4:
                                                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[4];
                                                break;
                                            case ItemClass.Level.Level5:
                                                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[5];
                                                break;
                                            default:
                                                BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[1];
                                                break;
                                        }
                                        HomeDistrict = MyDistrict.GetDistrict(MyBuilding.m_buildings.m_buffer[(int)CitizenHomeID.Index].m_position);
                                        if (HomeDistrict == 0)
                                        {
                                            BubbleHomeIcon.tooltip = Translations.Translate("DistrictLabel") + Translations.Translate("DistrictNameNoDistrict");
                                        }
                                        else
                                        {
                                            BubbleHomeIcon.tooltip = Translations.Translate("DistrictLabel") + MyDistrict.GetDistrictName(HomeDistrict);
                                        }
                                        Notification.ProblemStruct problems = MyBuilding.m_buildings.m_buffer[CitizenHome].m_problems;
                                        if (problems != Notification.ProblemStruct.None)
                                        {
                                            BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSpriteProblems;
                                        }
                                        else
                                        {
                                            BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
                                        }


                                        if (problems.ToString().Contains(Notification.Problem1.Electricity.ToString()))
                                        {
                                            BubbleDetailsElettricity.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_TITLE", "Electricity");
                                        }
                                        else
                                        {
                                            BubbleDetailsElettricity.normalFgSprite = null;
                                            BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Sewage.ToString()))
                                        {
                                            BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Sewage");
                                        }
                                        else
                                        {
                                            if (problems.ToString().Contains(Notification.Problem1.DirtyWater.ToString()))
                                            {
                                                BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NORMAL", "DirtyWater");
                                            }
                                            else
                                            {
                                                if (problems.ToString().Contains(Notification.Problem1.Water.ToString()))
                                                {
                                                    BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                    BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Water");
                                                }
                                                else
                                                {
                                                    if (problems.ToString().Contains(Notification.Problem1.Flood.ToString()))
                                                    {
                                                        BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                        BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Flood");
                                                    }
                                                    else
                                                    {
                                                        BubbleDetailsWater.normalFgSprite = null;
                                                        BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NONE");
                                                    }
                                                }
                                            }
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Death.ToString()))
                                        {
                                            BubbleDetailsDeath.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_TITLE", "Death");
                                        }
                                        else
                                        {
                                            BubbleDetailsDeath.normalFgSprite = null;
                                            BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Fire.ToString()))
                                        {
                                            BubbleDetailsFire.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_TITLE", "Fire");
                                        }
                                        else
                                        {
                                            BubbleDetailsFire.normalFgSprite = null;
                                            BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Garbage.ToString()))
                                        {
                                            BubbleDetailsGarbage.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_TITLE", "Garbage");
                                        }
                                        else
                                        {
                                            BubbleDetailsGarbage.normalFgSprite = null;
                                            BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.LandValueLow.ToString()))
                                        {
                                            BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "LandValueLow");
                                        }
                                        else
                                        {
                                            if (problems.ToString().Contains(Notification.Problem1.TooFewServices.ToString()))
                                            {
                                                BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                                BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "ToofewServices");
                                            }
                                            else
                                            {
                                                if (problems.ToString().Contains(Notification.Problem1.TaxesTooHigh.ToString()))
                                                {
                                                    BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                                    BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "TaxesTooHigh");
                                                }
                                                else
                                                {
                                                    BubbleDetailsLandValue.normalFgSprite = null;
                                                    BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_NONE");
                                                }
                                            }
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Noise.ToString()))
                                        {
                                            BubbleDetailsNoise.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NORMAL", "Noise");
                                        }
                                        else
                                        {
                                            BubbleDetailsNoise.normalFgSprite = null;
                                            BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (problems.ToString().Contains(Notification.Problem1.Pollution.ToString()))
                                        {
                                            BubbleDetailsPollution.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NORMAL", "Pollution");
                                        }
                                        else
                                        {
                                            BubbleDetailsPollution.normalFgSprite = null;
                                            BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        if (MyCitizen.m_citizens.m_buffer[citizen].Arrested && MyCitizen.m_citizens.m_buffer[citizen].Criminal)
                                        {
                                            BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
                                            BubbleDetailsCrime.tooltip = Translations.Translate("Citizen_Arrested");
                                        }
                                        else
                                        {
                                            if (problems.ToString().Contains(Notification.Problem1.Crime.ToString()))
                                            {
                                                BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
                                                BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_TITLE", "Crime");
                                            }
                                            else
                                            {
                                                BubbleDetailsCrime.normalFgSprite = null;
                                                BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_NONE");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    BubbleHomeName.text = Translations.Translate("Citizen_HomeLess");
                                    BubbleHomeName.isEnabled = false;
                                    BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
                                    BubbleHomeIcon.tooltip = Translations.Translate("DistrictNameNoDistrict");
                                    BubbleHomeName.tooltip = Translations.Translate("Citizen_HomeLess_tooltip");
                                }
                                Activity(citizen, FavCimsLastActivityVehicleButton, FavCimsLastActivity, out MyVehicleID, out MyTargetID);
                                MainCitizenInstance = MyCitizen.m_citizens.m_buffer[(int)citizen].m_instance;
                                CitizenDistrict = MyDistrict.GetDistrict(MyCitizen.m_instances.m_buffer[MainCitizenInstance].GetSmoothPosition(MainCitizenInstance));
                                if (CitizenDistrict == 0)
                                {
                                    FavCimsDistrictValue.tooltip = Translations.Translate("District_Label_tooltip");
                                    FavCimsDistrictValue.text = Translations.Translate("DistrictNameNoDistrict");
                                }
                                else
                                {
                                    FavCimsDistrictValue.tooltip = Translations.Translate("District_Label_tooltip");
                                    FavCimsDistrictValue.text = MyDistrict.GetDistrictName(CitizenDistrict);
                                }
                                FamilyVehicle(citizen, BubblePersonalCarButton, out PersonalVehicleID);
                                FamilyVehicle(Family.m_citizen0, BubbleFamilyBarCarButton, out FamilyVehicleID);
                                bool isSon = false;
                                int Sons = 0;
                                if (Family.m_citizen0 != 0U && citizen == Family.m_citizen1)
                                {
                                    CitizenPartner = Family.m_citizen0;
                                    BubblePartnerLove.normalBgSprite = "InfoIconHealth";
                                    if (DogOwner > 0U)
                                    {
                                        FamilyPet(DogOwner);
                                    }
                                    else
                                    {
                                        FamilyPet(Family.m_citizen1);
                                    }
                                }
                                else
                                {
                                    if (Family.m_citizen1 != 0U && citizen == Family.m_citizen0)
                                    {
                                        CitizenPartner = Family.m_citizen1;
                                        BubblePartnerLove.normalBgSprite = "InfoIconHealth";
                                        if (DogOwner > 0U)
                                        {
                                            FamilyPet(DogOwner);
                                        }
                                        else
                                        {
                                            FamilyPet(Family.m_citizen0);
                                        }
                                    }
                                    else
                                    {
                                        if (citizen == Family.m_citizen0)
                                        {
                                            if (DogOwner > 0U)
                                            {
                                                FamilyPet(DogOwner);
                                            }
                                            else
                                            {
                                                FamilyPet(citizen);
                                            }
                                            CitizenPartner = 0U;
                                        }
                                        else
                                        {
                                            BubblePartnerLove.normalBgSprite = "InfoIconAge";
                                            CitizenPartner = Family.m_citizen0;
                                            isSon = true;
                                        }
                                    }
                                }
                                if (CitizenPartner > 0U)
                                {
                                    PartnerID.Citizen = CitizenPartner;
                                    int CitizenPartnerINT = (int)(UIntPtr)CitizenPartner;
                                    BubblePartnerName.text = MyCitizen.GetCitizenName(CitizenPartner);
                                    if (Citizen.GetGender(CitizenPartner) == Citizen.Gender.Female)
                                    {
                                        BubblePartnerName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    }
                                    else
                                    {
                                        BubblePartnerName.textColor = new Color32(204, 204, 51, 40);
                                    }
                                    bool isEmpty = PartnerID.IsEmpty;
                                    if (isEmpty)
                                    {
                                        BubblePartnerName.tooltip = null;
                                        BubblePartnerName.isEnabled = false;
                                    }
                                    else
                                    {
                                        BubblePartnerName.tooltip = Translations.Translate("Right_click_to_swith_tooltip");
                                        BubblePartnerName.isEnabled = true;
                                    }
                                    if (DogOwner > 0U)
                                    {
                                        FamilyPet(DogOwner);
                                    }
                                    else
                                    {
                                        FamilyPet(CitizenPartner);
                                    }
                                    Activity(CitizenPartner, BubblePartnerVehicleButton, BubblePartnerDestination, out PartnerVehID, out PartnerTarget);
                                    RealAge = FavCimsCore.CalculateCitizenAge(MyCitizen.m_citizens.m_buffer[(int)CitizenPartner].m_age);

                                    BubbleParnerAgeButton.text = RealAge.ToString();

                                    switch (RealAge)
                                    {
                                        case int n when n <= 12:
                                            BubbleParnerAgeButton.textColor = new Color32(83, 166, 0, 60);
                                            break;
                                        case int n when n <= 19:
                                            BubbleParnerAgeButton.textColor = new Color32(0, 102, 51, 100);
                                            break;
                                        case int n when n <= 25:
                                            BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            break;
                                        case int n when n <= 65:
                                            BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            break;
                                        case int n when n <= 90:
                                            BubbleParnerAgeButton.textColor = new Color32(153, 0, 0, 0);
                                            break;
                                        default:
                                            BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                            break;
                                    }

                                    if (FavCimsCore.RowID.ContainsKey(CitizenPartnerINT))
                                    {
                                        BubblePartnerFollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        BubblePartnerFollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    PartnerPanel.Show();
                                    if (!isSon)
                                    {
                                        NoPartnerPanel.Hide();
                                    }
                                }
                                else
                                {
                                    PartnerPanel.Hide();
                                    if (!isSon)
                                    {
                                        NoPartnerPanel.Show();
                                    }
                                }
                                if (isSon)
                                {
                                    NoPartnerPanel.Hide();
                                    if (Family.m_citizen1 > 0U)
                                    {
                                        CitizenPartner = Family.m_citizen1;
                                        Parent1ID.Citizen = CitizenPartner;
                                        int CitizenPartnerINT = (int)(UIntPtr)CitizenPartner;
                                        BubbleParent1Name.text = MyCitizen.GetCitizenName(CitizenPartner);
                                        if (Citizen.GetGender(CitizenPartner) == Citizen.Gender.Female)
                                        {
                                            BubbleParent1Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        }
                                        else
                                        {
                                            BubbleParent1Name.textColor = new Color32(204, 204, 51, 40);
                                        }
                                        bool isEmpty2 = Parent1ID.IsEmpty;
                                        if (isEmpty2)
                                        {
                                            BubbleParent1Name.isEnabled = false;
                                            BubbleParent1Name.tooltip = null;
                                        }
                                        else
                                        {
                                            BubbleParent1Name.isEnabled = true;
                                            BubbleParent1Name.tooltip = Translations.Translate("Right_click_to_swith_tooltip");
                                        }
                                        Activity(CitizenPartner, BubbleParent1VehicleButton, BubbleParent1Destination, out Parent1VehID, out Parent1Target);
                                        RealAge = FavCimsCore.CalculateCitizenAge(MyCitizen.m_citizens.m_buffer[(int)CitizenPartner].m_age);

                                        BubbleParent1AgeButton.text = RealAge.ToString();

                                        switch (RealAge)
                                        {
                                            case int n when n <= 12:
                                                BubbleParent1AgeButton.textColor = new Color32(83, 166, 0, 60);
                                                break;
                                            case int n when n <= 19:
                                                BubbleParent1AgeButton.textColor = new Color32(0, 102, 51, 100);
                                                break;
                                            case int n when n <= 25:
                                                BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                break;
                                            case int n when n <= 65:
                                                BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                break;
                                            case int n when n <= 90:
                                                BubbleParent1AgeButton.textColor = new Color32(153, 0, 0, 0);
                                                break;
                                            default:
                                                BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                break;
                                        }

                                        if (FavCimsCore.RowID.ContainsKey(CitizenPartnerINT))
                                        {
                                            BubbleParent1FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                        }
                                        else
                                        {
                                            BubbleParent1FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                        }
                                        Parent1Panel.Show();
                                    }
                                    else
                                    {
                                        Parent1Panel.Hide();
                                    }
                                }
                                else
                                {
                                    Parent1Panel.Hide();
                                }
                                if (Family.m_citizen2 != 0U && Family.m_citizen2 != citizen)
                                {
                                    CitizenParent2 = Family.m_citizen2;
                                    Parent2ID.Citizen = CitizenParent2;
                                    int CitizenPartnerINT = (int)(UIntPtr)CitizenParent2;
                                    BubbleFamilyMember2Name.text = MyCitizen.GetCitizenName(CitizenParent2);
                                    if (Citizen.GetGender(CitizenParent2) == Citizen.Gender.Female)
                                    {
                                        BubbleFamilyMember2Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember2Name.textColor = new Color32(204, 204, 51, 40);
                                        BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty3 = Parent2ID.IsEmpty;
                                    if (isEmpty3)
                                    {
                                        BubbleFamilyMember2Name.isEnabled = false;
                                        BubbleFamilyMember2Name.tooltip = null;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember2Name.isEnabled = true;
                                        BubbleFamilyMember2Name.tooltip = Translations.Translate("Right_click_to_swith_tooltip");
                                    }
                                    if (DogOwner > 0U)
                                    {
                                        FamilyPet(DogOwner);
                                    }
                                    else
                                    {
                                        FamilyPet(Family.m_citizen2);
                                    }
                                    Activity(CitizenParent2, BubbleFamilyMember2ActivityVehicleButton, BubbleFamilyMember2ActivityDestination, out Parent2VehID, out Parent2Target);
                                    RealAge = FavCimsCore.CalculateCitizenAge(MyCitizen.m_citizens.m_buffer[(int)CitizenParent2].m_age);

                                    BubbleFamilyMember2AgeButton.text = RealAge.ToString();

                                    switch (RealAge)
                                    {
                                        case int n when n <= 12:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(83, 166, 0, 60);
                                            break;
                                        case int n when n <= 19:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(0, 102, 51, 100);
                                            break;
                                        case int n when n <= 25:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            break;
                                        case int n when n <= 65:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            break;
                                        case int n when n <= 90:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(153, 0, 0, 0);
                                            break;
                                        default:
                                            BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                            break;
                                    }

                                    if (FavCimsCore.RowID.ContainsKey(CitizenPartnerINT))
                                    {
                                        BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    FamilyMember2Panel.Show();
                                    if (!isSon)
                                    {
                                        Sons++;
                                    }
                                }
                                else
                                {
                                    if (Family.m_citizen2 == citizen)
                                    {
                                        if (DogOwner > 0U)
                                        {
                                            FamilyPet(DogOwner);
                                        }
                                        else
                                        {
                                            FamilyPet(Family.m_citizen2);
                                        }
                                    }
                                    FamilyMember2Panel.Hide();
                                }
                                if (Family.m_citizen3 != 0U && Family.m_citizen3 != citizen)
                                {
                                    CitizenParent3 = Family.m_citizen3;
                                    Parent3ID.Citizen = CitizenParent3;
                                    int CitizenPartnerINT = (int)(UIntPtr)CitizenParent3;
                                    BubbleFamilyMember3Name.text = MyCitizen.GetCitizenName(CitizenParent3);
                                    if (Citizen.GetGender(CitizenParent3) == Citizen.Gender.Female)
                                    {
                                        BubbleFamilyMember3Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember3Name.textColor = new Color32(204, 204, 51, 40);
                                        BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty4 = Parent3ID.IsEmpty;
                                    if (isEmpty4)
                                    {
                                        BubbleFamilyMember3Name.isEnabled = false;
                                        BubbleFamilyMember3Name.tooltip = null;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember3Name.isEnabled = true;
                                        BubbleFamilyMember3Name.tooltip = Translations.Translate("Right_click_to_swith_tooltip");
                                    }
                                    if (DogOwner > 0U)
                                    {
                                        FamilyPet(DogOwner);
                                    }
                                    else
                                    {
                                        FamilyPet(Family.m_citizen3);
                                    }
                                    Activity(CitizenParent3, BubbleFamilyMember3ActivityVehicleButton, BubbleFamilyMember3ActivityDestination, out Parent3VehID, out Parent3Target);
                                    RealAge = FavCimsCore.CalculateCitizenAge(MyCitizen.m_citizens.m_buffer[(int)CitizenParent3].m_age);

                                    BubbleFamilyMember3AgeButton.text = RealAge.ToString();

                                    switch (RealAge)
                                    {
                                        case int n when n <= 12:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(83, 166, 0, 60);
                                            break;
                                        case int n when n <= 19:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(0, 102, 51, 100);
                                            break;
                                        case int n when n <= 25:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            break;
                                        case int n when n <= 65:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            break;
                                        case int n when n <= 90:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(153, 0, 0, 0);
                                            break;
                                        default:
                                            BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                            break;
                                    }

                                    if (FavCimsCore.RowID.ContainsKey(CitizenPartnerINT))
                                    {
                                        BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    FamilyMember3Panel.Show();
                                    if (!isSon)
                                    {
                                        Sons++;
                                    }
                                }
                                else
                                {
                                    if (Family.m_citizen3 == citizen)
                                    {
                                        if (DogOwner > 0U)
                                        {
                                            FamilyPet(DogOwner);
                                        }
                                        else
                                        {
                                            FamilyPet(Family.m_citizen3);
                                        }
                                    }
                                    FamilyMember3Panel.Hide();
                                }
                                if (Family.m_citizen4 != 0U && Family.m_citizen4 != citizen)
                                {
                                    CitizenParent4 = Family.m_citizen4;
                                    Parent4ID.Citizen = CitizenParent4;
                                    int CitizenPartnerINT = (int)(UIntPtr)CitizenParent4;
                                    BubbleFamilyMember4Name.text = MyCitizen.GetCitizenName(CitizenParent4);
                                    if (Citizen.GetGender(CitizenParent4) == Citizen.Gender.Female)
                                    {
                                        BubbleFamilyMember4Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember4Name.textColor = new Color32(204, 204, 51, 40);
                                        BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty5 = Parent4ID.IsEmpty;
                                    if (isEmpty5)
                                    {
                                        BubbleFamilyMember4Name.isEnabled = false;
                                        BubbleFamilyMember4Name.tooltip = null;
                                    }
                                    else
                                    {
                                        BubbleFamilyMember4Name.isEnabled = true;
                                        BubbleFamilyMember4Name.tooltip = Translations.Translate("Right_click_to_swith_tooltip");
                                    }
                                    if (DogOwner > 0U)
                                    {
                                        FamilyPet(DogOwner);
                                    }
                                    else
                                    {
                                        FamilyPet(Family.m_citizen4);
                                    }
                                    Activity(CitizenParent4, BubbleFamilyMember4ActivityVehicleButton, BubbleFamilyMember4ActivityDestination, out Parent4VehID, out Parent4Target);
                                    RealAge = FavCimsCore.CalculateCitizenAge(MyCitizen.m_citizens.m_buffer[(int)CitizenParent4].m_age);

                                    BubbleFamilyMember4AgeButton.text = RealAge.ToString();

                                    switch (RealAge)
                                    {
                                        case int n when n <= 12:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(83, 166, 0, 60);
                                            break;
                                        case int n when n <= 19:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(0, 102, 51, 100);
                                            break;
                                        case int n when n <= 25:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            break;
                                        case int n when n <= 65:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            break;
                                        case int n when n <= 90:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(153, 0, 0, 0);
                                            break;
                                        default:
                                            BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                            break;
                                    }

                                    if (FavCimsCore.RowID.ContainsKey(CitizenPartnerINT))
                                    {
                                        BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    FamilyMember4Panel.Show();
                                    if (!isSon)
                                    {
                                        Sons++;
                                    }
                                }
                                else
                                {
                                    if (Family.m_citizen4 == citizen)
                                    {
                                        if (DogOwner > 0U)
                                        {
                                            FamilyPet(DogOwner);
                                        }
                                        else
                                        {
                                            FamilyPet(Family.m_citizen4);
                                        }
                                    }
                                    FamilyMember4Panel.Hide();
                                }
                                if (Sons == 0 && !isSon)
                                {
                                    NoChildsPanel.Show();
                                }
                                else
                                {
                                    NoChildsPanel.Hide();
                                }
                                bool firstRun = FirstRun;
                                if (firstRun)
                                {
                                    FirstRun = false;
                                }
                            }
                            else
                            {
                                Hide();
                                MyInstanceID = InstanceID.Empty;
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
