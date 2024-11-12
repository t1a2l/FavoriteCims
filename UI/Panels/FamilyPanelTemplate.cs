using System;
using System.Runtime.CompilerServices;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
    public class FamilyPanelTemplate : UIPanel
    {
        private const float Run = 0.3f;

        private float seconds = 0.3f;

        private bool FirstRun = true;

        private bool execute = false;

        private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private DistrictManager MyDistrict = Singleton<DistrictManager>.instance;

        private static readonly string[] sHappinessLevels = new string[] { "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy" };

        private static readonly string[] sHealthLevels = new string[] { "VerySick", "Sick", "PoorHealth", "Healthy", "VeryHealthy", "ExcellentHealth" };

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
                    bool flag = MyInstance.SelectInstance(Target);
                    if (flag)
                    {
                        bool flag2 = eventParam.buttons == UIMouseButton.Middle;
                        if (flag2)
                        {
                            WorldInfoPanel.Show<CitizenWorldInfoPanel>(base.position, Target);
                        }
                        else
                        {
                            bool flag3 = eventParam.buttons == UIMouseButton.Right;
                            if (flag3)
                            {
                                this.MyInstanceID = Target;
                                this.execute = true;
                                this.LateUpdate();
                            }
                            else
                            {
                                ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
                                WorldInfoPanel.Show<CitizenWorldInfoPanel>(base.position, Target);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Error("Can't find the Citizen " + ex.ToString());
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
                    bool flag = this.MyInstance.SelectInstance(Target);
                    if (flag)
                    {
                        bool flag2 = eventParam.buttons == UIMouseButton.Middle;
                        if (flag2)
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
            bool flag = m_citizen == 0U;
            if (!flag)
            {
                try
                {
                    ushort vehicle = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                    ushort parkedVehicle = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_parkedVehicle;
                    bool flag2 = vehicle > 0;
                    if (flag2)
                    {
                        MyCitVeh.Vehicle = vehicle;
                        VehicleInfo info = this.MyVehicle.m_vehicles.m_buffer[(int)vehicle].Info;
                        bool flag3 = info.m_vehicleAI.GetOwnerID(vehicle, ref this.MyVehicle.m_vehicles.m_buffer[(int)vehicle]).Citizen == m_citizen;
                        if (flag3)
                        {
                            sPrite.texture = TextureDB.BubbleCar;
                            sPrite.playAudioEvents = true;
                            sPrite.tooltip = string.Concat(new string[]
                            {
                                this.MyVehicle.GetVehicleName(vehicle),
                                " - ",
                                Locale.Get("VEHICLE_OWNER"),
                                " ",
                                this.MyCitizen.GetCitizenName(m_citizen)
                            });
                        }
                    }
                    else
                    {
                        bool flag4 = parkedVehicle > 0;
                        if (flag4)
                        {
                            MyCitVeh.ParkedVehicle = parkedVehicle;
                            VehicleParked vehicleParked = this.MyVehicle.m_parkedVehicles.m_buffer[(int)parkedVehicle];
                            bool flag5 = vehicleParked.m_ownerCitizen == m_citizen;
                            if (flag5)
                            {
                                sPrite.texture = TextureDB.BubbleCar;
                                sPrite.playAudioEvents = true;
                                sPrite.tooltip = string.Concat(new string[]
                                {
                                    this.MyVehicle.GetParkedVehicleName(parkedVehicle),
                                    " (",
                                    Locale.Get("VEHICLE_STATUS_PARKED"),
                                    ") - ",
                                    Locale.Get("VEHICLE_OWNER"),
                                    " ",
                                    this.MyCitizen.GetCitizenName(m_citizen)
                                });
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
            bool flag = m_citizen == 0U;
            if (!flag)
            {
                ushort instance = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_instance;
                CitizenInstance citizenInstance = this.MyCitizen.m_instances.m_buffer[(int)instance];
                bool flag2 = citizenInstance.m_targetBuilding > 0;
                if (flag2)
                {
                    ushort vehicle = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                    bool flag3 = vehicle > 0;
                    if (flag3)
                    {
                        VehID.Vehicle = vehicle;
                        ButtVehicle.isEnabled = true;
                        VehicleInfo info = this.MyVehicle.m_vehicles.m_buffer[(int)vehicle].Info;
                        string text = this.MyVehicle.GetVehicleName(vehicle);
                        bool flag4 = info.m_class.m_service == ItemClass.Service.Residential;
                        if (flag4)
                        {
                            bool flag5 = text.Like("Bicycle");
                            if (flag5)
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
                            bool flag6 = info.m_class.m_service == ItemClass.Service.PublicTransport;
                            if (flag6)
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
                                            bool flag7 = text == "VEHICLE_TITLE[Train Passenger]:0";
                                            if (flag7)
                                            {
                                                text = Locale.Get("VEHICLE_TITLE", "Train Engine");
                                            }
                                            ButtVehicle.tooltip = Locale.Get("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get("SUBSERVICE_DESC", "Train");
                                            break;
                                        }
                                    case ItemClass.SubService.PublicTransportShip:
                                        {
                                            bool flag8 = text.Like("Ferry");
                                            if (flag8)
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
                                            bool flag9 = text.Like("Blimp");
                                            if (flag9)
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
                        ButtVehicle.tooltip = FavCimsLang.text("Vehicle_on_foot");
                    }
                }
                else
                {
                    ButtVehicle.disabledBgSprite = "InfoIconPopulationDisabled";
                    ButtVehicle.isEnabled = false;
                    ButtVehicle.tooltip = null;
                }
                this.CitizenInstanceID.Citizen = m_citizen;
                CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].GetCitizenInfo(m_citizen);
                string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(m_citizen, ref this.MyCitizen.m_citizens.m_buffer[(int)m_citizen], out MyTargetID);
                string buildingName = this.MyBuilding.GetBuildingName(MyTargetID.Building, this.CitizenInstanceID);
                ButtDestination.text = localizedStatus + " " + buildingName;
                bool flag10 = !MyTargetID.IsEmpty;
                if (flag10)
                {
                    int district = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)MyTargetID.Index].m_position);
                    bool flag11 = district == 0;
                    if (flag11)
                    {
                        ButtDestination.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
                    }
                    else
                    {
                        ButtDestination.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(district);
                    }
                }
                bool flag12 = this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)m_citizen)))].Arrested && this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)m_citizen)))].Criminal;
                if (flag12)
                {
                    bool flag13 = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].CurrentLocation == Citizen.Location.Moving;
                    if (flag13)
                    {
                        this.policeveh = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_vehicle;
                        bool flag14 = this.policeveh > 0;
                        if (flag14)
                        {
                            VehID.Vehicle = this.policeveh;
                            ButtVehicle.atlas = MyAtlas.FavCimsAtlas;
                            ButtVehicle.normalBgSprite = "FavCimsPoliceVehicle";
                            ButtVehicle.isEnabled = true;
                            ButtVehicle.playAudioEvents = true;
                            ButtVehicle.tooltip = this.MyVehicle.GetVehicleName(this.policeveh) + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
                            ButtDestination.isEnabled = false;
                            ButtDestination.text = FavCimsLang.text("Transported_to_Prison");
                        }
                    }
                    else
                    {
                        ButtDestination.isEnabled = true;
                        ButtDestination.text = FavCimsLang.text("Jailed_into") + buildingName;
                        ButtVehicle.atlas = UIView.GetAView().defaultAtlas;
                    }
                }
            }
        }

        internal void FamilyPet(uint m_citizen)
        {
            bool flag = m_citizen == 0U;
            if (!flag)
            {
                try
                {
                    this.CitizenInstance = this.MyCitizen.m_citizens.m_buffer[(int)m_citizen].m_instance;
                    this.Pet = (ushort)Array.FindIndex<CitizenInstance>(this.MyCitizen.m_instances.m_buffer, (CitizenInstance element) => element.m_targetBuilding == this.CitizenInstance);
                    this.PetInstance = this.MyCitizen.m_instances.m_buffer[(int)this.Pet];
                    bool flag2 = this.PetInstance.Info.m_citizenAI.IsAnimal();
                    if (flag2)
                    {
                        this.DogOwner = m_citizen;
                        this.MyPetID.CitizenInstance = this.Pet;
                        bool flag3 = !this.MyPetID.IsEmpty;
                        if (flag3)
                        {
                            string instanceName = this.MyCitizen.GetInstanceName(this.Pet);
                            CitizenInfo info = this.PetInstance.Info;
                            InstanceID instanceID;
                            string localizedStatus = info.m_citizenAI.GetLocalizedStatus(this.Pet, ref this.PetInstance, out instanceID);
                            this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDog;
                            this.BubbleFamilyBarDogButton.tooltip = string.Concat(new string[]
                            {
                                instanceName,
                                " - ",
                                localizedStatus,
                                " ",
                                this.MyCitizen.GetCitizenName(m_citizen)
                            });
                            this.BubbleFamilyBarDogButton.playAudioEvents = true;
                        }
                    }
                    else
                    {
                        this.DogOwner = 0U;
                        this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
                        this.BubbleFamilyBarDogButton.tooltip = null;
                        this.BubbleFamilyBarDogButton.playAudioEvents = false;
                        this.MyPetID = InstanceID.Empty;
                    }
                }
                catch
                {
                }
            }
        }

        internal static string GetHappinessString(Citizen.Happiness happinessLevel)
        {
            return "NotificationIcon" + FamilyPanelTemplate.sHappinessLevels[(int)happinessLevel];
        }

        internal static string GetHealthString(Citizen.Health healthLevel)
        {
            return "NotificationIcon" + FamilyPanelTemplate.sHealthLevels[(int)healthLevel];
        }

        public override void Start()
        {
            UITextureAtlas favCimsAtlas = MyAtlas.FavCimsAtlas;
            base.width = 250f;
            base.height = 500f;
            base.clipChildren = true;
            int num = 30;
            int num2 = Screen.width / 4;
            int num3 = 100;
            int num4 = Screen.height - (int)base.height * 2 - num3;
            System.Random random = new System.Random();
            this.FavCimsOtherInfoSprite = base.AddUIComponent<UITextureSprite>();
            this.FavCimsOtherInfoSprite.name = "FavCimsOtherInfoSprite";
            this.FavCimsOtherInfoSprite.texture = TextureDB.FavCimsOtherInfoTexture;
            this.FavCimsOtherInfoSprite.width = base.width;
            this.FavCimsOtherInfoSprite.height = base.height;
            this.FavCimsOtherInfoSprite.relativePosition = Vector3.zero;
            this.BubbleHeaderPanel = base.AddUIComponent<UIPanel>();
            this.BubbleHeaderPanel.name = "BubbleHeaderPanel";
            this.BubbleHeaderPanel.width = 250f;
            this.BubbleHeaderPanel.height = 41f;
            this.BubbleHeaderPanel.relativePosition = new Vector3(0f, 0f);
            this.BubbleHeaderIconSprite = this.BubbleHeaderPanel.AddUIComponent<UITextureSprite>();
            this.BubbleHeaderIconSprite.name = "BubbleHeaderIconSprite";
            this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            this.BubbleHeaderIconSprite.relativePosition = new Vector3(9f, this.BubbleHeaderPanel.relativePosition.y + 9f);
            this.BubbleHeaderCitizenName = this.BubbleHeaderPanel.AddUIComponent<UIButton>();
            this.BubbleHeaderCitizenName.name = "BubbleHeaderCitizenName";
            this.BubbleHeaderCitizenName.width = this.BubbleHeaderPanel.width;
            this.BubbleHeaderCitizenName.height = this.BubbleHeaderPanel.height;
            this.BubbleHeaderCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleHeaderCitizenName.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleHeaderCitizenName.playAudioEvents = false;
            this.BubbleHeaderCitizenName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleHeaderCitizenName.font.size = 15;
            this.BubbleHeaderCitizenName.textScale = 1f;
            this.BubbleHeaderCitizenName.wordWrap = true;
            this.BubbleHeaderCitizenName.textPadding.left = 5;
            this.BubbleHeaderCitizenName.textPadding.right = 5;
            this.BubbleHeaderCitizenName.textColor = new Color32(204, 204, 51, 40);
            this.BubbleHeaderCitizenName.hoveredTextColor = new Color32(204, 204, 51, 40);
            this.BubbleHeaderCitizenName.pressedTextColor = new Color32(204, 204, 51, 40);
            this.BubbleHeaderCitizenName.focusedTextColor = new Color32(204, 204, 51, 40);
            this.BubbleHeaderCitizenName.useDropShadow = true;
            this.BubbleHeaderCitizenName.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleHeaderCitizenName.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleHeaderCitizenName.relativePosition = Vector3.zero;
            this.BubbleHeaderCitizenName.eventMouseDown += delegate
            {
                bool mouseButton = Input.GetMouseButton(0);
                if (mouseButton)
                {
                    bool flag = base.GetComponentInChildren<WindowController>() != null;
                    if (flag)
                    {
                        this.PanelMover = base.GetComponentInChildren<WindowController>();
                        this.PanelMover.ComponentToMove = this;
                        this.PanelMover.Stop = false;
                        this.PanelMover.Start();
                    }
                    else
                    {
                        this.PanelMover = base.AddUIComponent(typeof(WindowController)) as WindowController;
                        this.PanelMover.ComponentToMove = this;
                    }
                    base.opacity = 0.5f;
                }
            };
            this.BubbleHeaderCitizenName.eventMouseUp += delegate
            {
                bool flag2 = this.PanelMover != null;
                if (flag2)
                {
                    this.PanelMover.Stop = true;
                    this.PanelMover.ComponentToMove = null;
                    this.PanelMover = null;
                }
                base.opacity = 1f;
            };
            this.BubbleCloseButton = base.AddUIComponent<UIButton>();
            this.BubbleCloseButton.name = "BubbleCloseButton";
            this.BubbleCloseButton.width = 26f;
            this.BubbleCloseButton.height = 26f;
            this.BubbleCloseButton.normalBgSprite = "buttonclose";
            this.BubbleCloseButton.hoveredBgSprite = "buttonclosehover";
            this.BubbleCloseButton.pressedBgSprite = "buttonclosepressed";
            this.BubbleCloseButton.opacity = 0.9f;
            this.BubbleCloseButton.playAudioEvents = true;
            this.BubbleCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleCloseButton.eventClick += delegate
            {
                try
                {
                    base.Hide();
                    this.MyInstanceID = InstanceID.Empty;
                }
                catch (Exception ex)
                {
                    Debug.Error("Can't remove family panel " + ex.ToString());
                }
            };
            this.BubbleCloseButton.relativePosition = new Vector3(this.BubbleHeaderPanel.width - 36f, 7f);
            this.BubbleFamilyPortraitPanel = base.AddUIComponent<UIPanel>();
            this.BubbleFamilyPortraitPanel.name = "BubbleFamilyPortraitPanel";
            this.BubbleFamilyPortraitPanel.width = 242f;
            this.BubbleFamilyPortraitPanel.height = 156f;
            this.BubbleFamilyPortraitPanel.relativePosition = new Vector3(4f, this.BubbleHeaderPanel.relativePosition.y + this.BubbleHeaderPanel.height);
            this.BubbleFamPortBgSpriteBack = this.BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            this.BubbleFamPortBgSpriteBack.name = "BubbleFamPortBgSpriteBack";
            this.BubbleFamPortBgSpriteBack.texture = TextureDB.BubbleFamPortBgSpriteBackTexture;
            this.BubbleFamPortBgSpriteBack.relativePosition = new Vector3(4f, 4f);
            this.BubbleFamPortBgSprite = this.BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite>();
            this.BubbleFamPortBgSprite.name = "BubbleFamPortBgSprite";
            this.BubbleFamPortBgSprite.texture = TextureDB.BubbleFamPortBgSpriteTexture;
            this.BubbleFamPortBgSprite.relativePosition = Vector3.zero;
            this.BubbleRow1Panel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleRow1Panel.name = "BubbleRow1Panel";
            this.BubbleRow1Panel.width = 234f;
            this.BubbleRow1Panel.height = 36f;
            this.BubbleRow1Panel.relativePosition = new Vector3(4f, 4f);
            this.BubbleRow1HappyPanel = this.BubbleRow1Panel.AddUIComponent<UIPanel>();
            this.BubbleRow1HappyPanel.name = "BubbleRow1Panel";
            this.BubbleRow1HappyPanel.width = 36f;
            this.BubbleRow1HappyPanel.height = 36f;
            this.BubbleRow1HappyPanel.relativePosition = Vector3.zero;
            this.BubbleRow1HappyIcon = this.BubbleRow1HappyPanel.AddUIComponent<UIButton>();
            this.BubbleRow1HappyIcon.width = 26f;
            this.BubbleRow1HappyIcon.height = 26f;
            this.BubbleRow1HappyIcon.isEnabled = false;
            this.BubbleRow1HappyIcon.playAudioEvents = false;
            this.BubbleRow1HappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleRow1HappyIcon.relativePosition = new Vector3(4f, 5f);
            this.BubbleRow2WellbeingIcon = this.BubbleRow1HappyPanel.AddUIComponent<UIButton>();
            this.BubbleRow2WellbeingIcon.width = 11f;
            this.BubbleRow2WellbeingIcon.height = 11f;
            this.BubbleRow2WellbeingIcon.isEnabled = false;
            this.BubbleRow2WellbeingIcon.playAudioEvents = false;
            this.BubbleRow2WellbeingIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleRow2WellbeingIcon.relativePosition = new Vector3(24f, 5f);
            this.BubbleRow1TextPanel = this.BubbleRow1Panel.AddUIComponent<UIPanel>();
            this.BubbleRow1TextPanel.name = "BubbleRow1TextPanel";
            this.BubbleRow1TextPanel.width = 198f;
            this.BubbleRow1TextPanel.height = 37f;
            this.BubbleRow1TextPanel.relativePosition = new Vector3(36f, 0f);
            this.BubbleRow1LabelsSprite = this.BubbleRow1TextPanel.AddUIComponent<UITextureSprite>();
            this.BubbleRow1LabelsSprite.name = "BubbleRow1LabelsSprite";
            this.BubbleRow1LabelsSprite.width = 198f;
            this.BubbleRow1LabelsSprite.height = 34f;
            this.BubbleRow1LabelsSprite.texture = TextureDB.BubbleBgBar1Big;
            this.BubbleRow1LabelsSprite.relativePosition = new Vector3(0f, 3f);
            this.BubbleRow1AgeLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            this.BubbleRow1AgeLabelPanel.name = "BubbleRow1AgeLabelPanel";
            this.BubbleRow1AgeLabelPanel.width = 32f;
            this.BubbleRow1AgeLabelPanel.height = 17f;
            this.BubbleRow1AgeLabelPanel.relativePosition = Vector3.zero;
            this.BubbleCitizenAge = this.BubbleRow1AgeLabelPanel.AddUIComponent<UIButton>();
            this.BubbleCitizenAge.name = "BubbleCitizenAge";
            this.BubbleCitizenAge.width = this.BubbleRow1AgeLabelPanel.width;
            this.BubbleCitizenAge.height = this.BubbleRow1AgeLabelPanel.height;
            this.BubbleCitizenAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleCitizenAge.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleCitizenAge.font.size = 15;
            this.BubbleCitizenAge.textScale = 0.8f;
            this.BubbleCitizenAge.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAge.outlineSize = 1;
            this.BubbleCitizenAge.textColor = new Color32(0, 51, 102, 140);
            this.BubbleCitizenAge.isInteractive = false;
            this.BubbleCitizenAge.useDropShadow = true;
            this.BubbleCitizenAge.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleCitizenAge.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAge.relativePosition = new Vector3(0f, 1f);
            this.BubbleRow1AgePhaseLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            this.BubbleRow1AgePhaseLabelPanel.name = "BubbleRow1AgePhaseLabelPanel";
            this.BubbleRow1AgePhaseLabelPanel.width = 100f;
            this.BubbleRow1AgePhaseLabelPanel.height = 17f;
            this.BubbleRow1AgePhaseLabelPanel.relativePosition = new Vector3(32f, 0f);
            this.BubbleCitizenAgePhase = this.BubbleRow1AgePhaseLabelPanel.AddUIComponent<UIButton>();
            this.BubbleCitizenAgePhase.name = "BubbleCitizenAgePhase";
            this.BubbleCitizenAgePhase.width = this.BubbleRow1AgePhaseLabelPanel.width;
            this.BubbleCitizenAgePhase.height = this.BubbleRow1AgePhaseLabelPanel.height;
            this.BubbleCitizenAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleCitizenAgePhase.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleCitizenAgePhase.font.size = 15;
            this.BubbleCitizenAgePhase.textScale = 0.8f;
            this.BubbleCitizenAgePhase.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgePhase.outlineSize = 1;
            this.BubbleCitizenAgePhase.textColor = new Color32(0, 51, 102, 140);
            this.BubbleCitizenAgePhase.isInteractive = false;
            this.BubbleCitizenAgePhase.useDropShadow = true;
            this.BubbleCitizenAgePhase.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleCitizenAgePhase.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgePhase.relativePosition = new Vector3(0f, 1f);
            this.BubbleRow1EducationLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            this.BubbleRow1EducationLabelPanel.name = "BubbleRow1LabelsPanel";
            this.BubbleRow1EducationLabelPanel.width = 66f;
            this.BubbleRow1EducationLabelPanel.height = 17f;
            this.BubbleRow1EducationLabelPanel.relativePosition = new Vector3(132f, 0f);
            this.BubbleCitizenEducation = this.BubbleRow1EducationLabelPanel.AddUIComponent<UIButton>();
            this.BubbleCitizenEducation.name = "BubbleCitizenEducation";
            this.BubbleCitizenEducation.width = this.BubbleRow1EducationLabelPanel.width;
            this.BubbleCitizenEducation.height = this.BubbleRow1EducationLabelPanel.height;
            this.BubbleCitizenEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleCitizenEducation.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleCitizenEducation.font.size = 15;
            this.BubbleCitizenEducation.textScale = 0.8f;
            this.BubbleCitizenEducation.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenEducation.outlineSize = 1;
            this.BubbleCitizenEducation.textColor = new Color32(0, 51, 102, 140);
            this.BubbleCitizenEducation.isInteractive = false;
            this.BubbleCitizenEducation.useDropShadow = true;
            this.BubbleCitizenEducation.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleCitizenEducation.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenEducation.relativePosition = new Vector3(0f, 1f);
            this.BubbleRow1ValuesPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel>();
            this.BubbleRow1ValuesPanel.name = "BubbleRow1ValuesPanel";
            this.BubbleRow1ValuesPanel.width = 198f;
            this.BubbleRow1ValuesPanel.height = 17f;
            this.BubbleRow1ValuesPanel.relativePosition = new Vector3(0f, 17f);
            this.BubbleRow1AgeValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            this.BubbleRow1AgeValuePanel.name = "BubbleRow1AgeValuePanel";
            this.BubbleRow1AgeValuePanel.width = 32f;
            this.BubbleRow1AgeValuePanel.height = 17f;
            this.BubbleRow1AgeValuePanel.relativePosition = Vector3.zero;
            this.BubbleCitizenAgeVal = this.BubbleRow1AgeValuePanel.AddUIComponent<UIButton>();
            this.BubbleCitizenAgeVal.name = "BubbleCitizenAgeVal";
            this.BubbleCitizenAgeVal.width = this.BubbleRow1AgeValuePanel.width;
            this.BubbleCitizenAgeVal.height = this.BubbleRow1AgeValuePanel.height;
            this.BubbleCitizenAgeVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleCitizenAgeVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleCitizenAgeVal.font.size = 15;
            this.BubbleCitizenAgeVal.textScale = 0.85f;
            this.BubbleCitizenAgeVal.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgeVal.outlineSize = 1;
            this.BubbleCitizenAgeVal.textColor = new Color32(0, 51, 102, 140);
            this.BubbleCitizenAgeVal.isInteractive = false;
            this.BubbleCitizenAgeVal.useDropShadow = true;
            this.BubbleCitizenAgeVal.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleCitizenAgeVal.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgeVal.relativePosition = new Vector3(0f, 0f);
            this.BubbleRow1AgePhaseValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            this.BubbleRow1AgePhaseValuePanel.name = "BubbleRow1AgePhaseValuePanel";
            this.BubbleRow1AgePhaseValuePanel.width = 100f;
            this.BubbleRow1AgePhaseValuePanel.height = 17f;
            this.BubbleRow1AgePhaseValuePanel.relativePosition = new Vector3(32f, 0f);
            this.BubbleCitizenAgePhaseVal = this.BubbleRow1AgePhaseValuePanel.AddUIComponent<UIButton>();
            this.BubbleCitizenAgePhaseVal.name = "BubbleCitizenAgePhaseVal";
            this.BubbleCitizenAgePhaseVal.width = this.BubbleRow1AgePhaseValuePanel.width;
            this.BubbleCitizenAgePhaseVal.height = this.BubbleRow1AgePhaseValuePanel.height;
            this.BubbleCitizenAgePhaseVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleCitizenAgePhaseVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleCitizenAgePhaseVal.font.size = 15;
            this.BubbleCitizenAgePhaseVal.textScale = 0.85f;
            this.BubbleCitizenAgePhaseVal.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgePhaseVal.outlineSize = 1;
            this.BubbleCitizenAgePhaseVal.textColor = new Color32(0, 51, 102, 140);
            this.BubbleCitizenAgePhaseVal.isInteractive = false;
            this.BubbleCitizenAgePhaseVal.useDropShadow = true;
            this.BubbleCitizenAgePhaseVal.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleCitizenAgePhaseVal.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleCitizenAgePhaseVal.relativePosition = new Vector3(0f, 0f);
            this.BubbleRow1EducationValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            this.BubbleRow1EducationValuePanel.name = "BubbleRow1LabelsPanel";
            this.BubbleRow1EducationValuePanel.width = 66f;
            this.BubbleRow1EducationValuePanel.height = 17f;
            this.BubbleRow1EducationValuePanel.relativePosition = new Vector3(132f, 0f);
            this.BubbleEduLevel1 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            this.BubbleEduLevel1.width = 18f;
            this.BubbleEduLevel1.height = 17f;
            this.BubbleEduLevel1.normalBgSprite = "InfoIconEducation";
            this.BubbleEduLevel1.disabledBgSprite = "InfoIconEducationDisabled";
            this.BubbleEduLevel1.isEnabled = false;
            this.BubbleEduLevel1.playAudioEvents = false;
            this.BubbleEduLevel1.relativePosition = new Vector3(2f, 0f);
            this.BubbleEduLevel2 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            this.BubbleEduLevel2.width = this.BubbleEduLevel1.width;
            this.BubbleEduLevel2.height = this.BubbleEduLevel1.height;
            this.BubbleEduLevel2.normalBgSprite = "InfoIconEducation";
            this.BubbleEduLevel2.disabledBgSprite = "InfoIconEducationDisabled";
            this.BubbleEduLevel2.isEnabled = false;
            this.BubbleEduLevel2.playAudioEvents = false;
            this.BubbleEduLevel2.relativePosition = new Vector3(24f, 0f);
            this.BubbleEduLevel3 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton>();
            this.BubbleEduLevel3.width = this.BubbleEduLevel1.width;
            this.BubbleEduLevel3.height = this.BubbleEduLevel1.height;
            this.BubbleEduLevel3.normalBgSprite = "InfoIconEducation";
            this.BubbleEduLevel3.disabledBgSprite = "InfoIconEducationDisabled";
            this.BubbleEduLevel3.isEnabled = false;
            this.BubbleEduLevel3.playAudioEvents = false;
            this.BubbleEduLevel3.relativePosition = new Vector3(46f, 0f);
            this.BubbleRow1EducationTooltipArea = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel>();
            this.BubbleRow1EducationTooltipArea.name = "BubbleRow1EducationTooltipArea";
            this.BubbleRow1EducationTooltipArea.width = this.BubbleRow1EducationValuePanel.width;
            this.BubbleRow1EducationTooltipArea.height = this.BubbleRow1EducationValuePanel.height;
            this.BubbleRow1EducationTooltipArea.absolutePosition = this.BubbleRow1EducationValuePanel.absolutePosition;
            this.BubbleRow1EducationTooltipArea.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleTargetPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleTargetPanel.name = "BubbleTargetPanel";
            this.BubbleTargetPanel.width = 58f;
            this.BubbleTargetPanel.height = 36f;
            this.BubbleTargetPanel.relativePosition = new Vector3(4f, 35f);
            this.BubbleTargetIcon = this.BubbleTargetPanel.AddUIComponent<UIButton>();
            this.BubbleTargetIcon.width = 28f;
            this.BubbleTargetIcon.height = 28f;
            this.BubbleTargetIcon.normalBgSprite = "LocationMarkerNormal";
            this.BubbleTargetIcon.hoveredBgSprite = "LocationMarkerHovered";
            this.BubbleTargetIcon.focusedBgSprite = "LocationMarkerFocused";
            this.BubbleTargetIcon.pressedBgSprite = "LocationMarkerPressed";
            this.BubbleTargetIcon.disabledBgSprite = "LocationMarkerDisabled";
            this.BubbleTargetIcon.playAudioEvents = true;
            this.BubbleTargetIcon.relativePosition = new Vector3(4f, 0f);
            this.BubbleTargetIcon.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.MyInstanceID, eventParam);
            };
            this.BubbleRow2Panel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleRow2Panel.name = "BubbleRow2Panel";
            this.BubbleRow2Panel.width = 198f;
            this.BubbleRow2Panel.height = 34f;
            this.BubbleRow2Panel.relativePosition = new Vector3(40f, 44f);
            this.BubbleWealthHealthSprite = this.BubbleRow2Panel.AddUIComponent<UITextureSprite>();
            this.BubbleWealthHealthSprite.name = "BubbleWealthHealthSprite";
            this.BubbleWealthHealthSprite.width = 198f;
            this.BubbleWealthHealthSprite.height = 34f;
            this.BubbleWealthHealthSprite.texture = TextureDB.BubbleBgBar1Big;
            this.BubbleWealthHealthSprite.relativePosition = Vector3.zero;
            this.BubbleWealthSpritePanel = this.BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
            this.BubbleWealthSpritePanel.name = "BubbleWealthSpritePanel";
            this.BubbleWealthSpritePanel.width = 37f;
            this.BubbleWealthSpritePanel.height = 34f;
            this.BubbleWealthSpritePanel.relativePosition = new Vector3(0f, 0f);
            this.BubbleWealthSprite = this.BubbleWealthSpritePanel.AddUIComponent<UIButton>();
            this.BubbleWealthSprite.name = "BubbleWealthSprite";
            this.BubbleWealthSprite.width = 25f;
            this.BubbleWealthSprite.height = 25f;
            this.BubbleWealthSprite.normalBgSprite = "MoneyThumb";
            this.BubbleWealthSprite.playAudioEvents = false;
            this.BubbleWealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleWealthSprite.relativePosition = new Vector3(10f, 5f);
            this.BubbleRow2WealthValueVal = this.BubbleWealthHealthSprite.AddUIComponent<UIButton>();
            this.BubbleRow2WealthValueVal.name = "BubbleRow2WealthValueVal";
            this.BubbleRow2WealthValueVal.width = 70f;
            this.BubbleRow2WealthValueVal.height = 34f;
            this.BubbleRow2WealthValueVal.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleRow2WealthValueVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleRow2WealthValueVal.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleRow2WealthValueVal.textPadding.top = 1;
            this.BubbleRow2WealthValueVal.font.size = 15;
            this.BubbleRow2WealthValueVal.textScale = 0.8f;
            this.BubbleRow2WealthValueVal.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleRow2WealthValueVal.outlineSize = 1;
            this.BubbleRow2WealthValueVal.textColor = new Color32(0, 51, 102, 140);
            this.BubbleRow2WealthValueVal.isInteractive = false;
            this.BubbleRow2WealthValueVal.useDropShadow = true;
            this.BubbleRow2WealthValueVal.wordWrap = true;
            this.BubbleRow2WealthValueVal.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleRow2WealthValueVal.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleRow2WealthValueVal.relativePosition = new Vector3(37f, 0f);
            this.BubbleHealthSpritePanel = this.BubbleWealthHealthSprite.AddUIComponent<UIPanel>();
            this.BubbleHealthSpritePanel.name = "BubbleHealthSpritePanel";
            this.BubbleHealthSpritePanel.width = 26f;
            this.BubbleHealthSpritePanel.height = 34f;
            this.BubbleHealthSpritePanel.relativePosition = new Vector3(107f, 0f);
            this.BubbleHealthSprite = this.BubbleHealthSpritePanel.AddUIComponent<UIButton>();
            this.BubbleHealthSprite.name = "BubbleWealthSprite";
            this.BubbleHealthSprite.width = 26f;
            this.BubbleHealthSprite.height = 26f;
            this.BubbleHealthSprite.playAudioEvents = false;
            this.BubbleHealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleHealthSprite.relativePosition = new Vector3(0f, 4f);
            this.BubbleHealthValue = this.BubbleWealthHealthSprite.AddUIComponent<UIButton>();
            this.BubbleHealthValue.name = "BubbleHealthValue";
            this.BubbleHealthValue.width = 65f;
            this.BubbleHealthValue.height = 34f;
            this.BubbleHealthValue.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleHealthValue.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleHealthValue.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleHealthValue.textPadding.left = 5;
            this.BubbleHealthValue.textPadding.right = 5;
            this.BubbleHealthValue.textPadding.top = 1;
            this.BubbleHealthValue.font.size = 15;
            this.BubbleHealthValue.textScale = 0.85f;
            this.BubbleHealthValue.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleHealthValue.outlineSize = 1;
            this.BubbleHealthValue.textColor = new Color32(0, 51, 102, 140);
            this.BubbleHealthValue.isInteractive = false;
            this.BubbleHealthValue.useDropShadow = true;
            this.BubbleHealthValue.wordWrap = true;
            this.BubbleHealthValue.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleHealthValue.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleHealthValue.relativePosition = new Vector3(133f, 0f);
            this.WorkBuildingPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.WorkBuildingPanel.name = "WorkBuildingPanel";
            this.WorkBuildingPanel.width = 234f;
            this.WorkBuildingPanel.height = 25f;
            this.WorkBuildingPanel.relativePosition = new Vector3(4f, 82f);
            this.BubbleWorkBuildingSprite = this.WorkBuildingPanel.AddUIComponent<UITextureSprite>();
            this.BubbleWorkBuildingSprite.name = "BubbleWorkBuildingSprite";
            this.BubbleWorkBuildingSprite.width = this.WorkBuildingPanel.width;
            this.BubbleWorkBuildingSprite.height = this.WorkBuildingPanel.height;
            this.BubbleWorkBuildingSprite.texture = TextureDB.BubbleBg1Special;
            this.BubbleWorkBuildingSprite.relativePosition = Vector3.zero;
            this.BubbleWorkBuildingSprite.clipChildren = true;
            this.FavCimsWorkingPlace = this.BubbleWorkBuildingSprite.AddUIComponent<UIButton>();
            this.FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
            this.FavCimsWorkingPlace.width = this.BubbleWorkBuildingSprite.width;
            this.FavCimsWorkingPlace.height = this.BubbleWorkBuildingSprite.height;
            this.FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.FavCimsWorkingPlace.textHorizontalAlignment = 0;
            this.FavCimsWorkingPlace.playAudioEvents = true;
            this.FavCimsWorkingPlace.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.FavCimsWorkingPlace.font.size = 15;
            this.FavCimsWorkingPlace.textScale = 0.85f;
            this.FavCimsWorkingPlace.textPadding.left = 40;
            this.FavCimsWorkingPlace.textPadding.right = 5;
            this.FavCimsWorkingPlace.outlineColor = new Color32(0, 0, 0, 0);
            this.FavCimsWorkingPlace.outlineSize = 1;
            this.FavCimsWorkingPlace.textColor = new Color32(21, 59, 96, 140);
            this.FavCimsWorkingPlace.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.FavCimsWorkingPlace.pressedTextColor = new Color32(153, 0, 0, 0);
            this.FavCimsWorkingPlace.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.FavCimsWorkingPlace.disabledTextColor = new Color32(51, 51, 51, 160);
            this.FavCimsWorkingPlace.useDropShadow = true;
            this.FavCimsWorkingPlace.dropShadowOffset = new Vector2(1f, -1f);
            this.FavCimsWorkingPlace.dropShadowColor = new Color32(0, 0, 0, 0);
            this.FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.FavCimsWorkingPlace.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.WorkPlaceID, eventParam);
            };
            this.FavCimsWorkingPlace.relativePosition = new Vector3(0f, 1f);
            this.BubbleWorkIconPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleWorkIconPanel.name = "BubbleRow2Panel";
            this.BubbleWorkIconPanel.width = 36f;
            this.BubbleWorkIconPanel.height = 40f;
            this.BubbleWorkIconPanel.absolutePosition = new Vector3(this.BubbleFamPortBgSprite.absolutePosition.x + 4f, this.BubbleFamPortBgSprite.absolutePosition.y + 71f);
            this.FavCimsWorkingPlaceSprite = this.BubbleWorkIconPanel.AddUIComponent<UITextureSprite>();
            this.FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
            this.FavCimsWorkingPlaceSprite.width = 20f;
            this.FavCimsWorkingPlaceSprite.height = 40f;
            this.FavCimsWorkingPlaceSprite.relativePosition = new Vector3(9f, 3f);
            this.FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.FavCimsWorkingPlaceButtonGamDefImg = this.FavCimsWorkingPlaceSprite.AddUIComponent<UIButton>();
            this.FavCimsWorkingPlaceButtonGamDefImg.name = "FavCimsWorkingPlaceButtonGamDefImg";
            this.FavCimsWorkingPlaceButtonGamDefImg.width = 20f;
            this.FavCimsWorkingPlaceButtonGamDefImg.height = 20f;
            this.FavCimsWorkingPlaceButtonGamDefImg.relativePosition = new Vector3(0f, 10f);
            this.FavCimsWorkingPlaceButtonGamDefImg.isInteractive = false;
            this.FavCimsWorkingPlaceButtonGamDefImg.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.FavCimsCitizenWorkPlaceLevelSprite = this.FavCimsWorkingPlaceSprite.AddUIComponent<UITextureSprite>();
            this.FavCimsCitizenWorkPlaceLevelSprite.name = "FavCimsCitizenWorkPlaceLevelSprite";
            this.FavCimsCitizenWorkPlaceLevelSprite.relativePosition = new Vector3(0f, 0f);
            this.BubblePersonalCarButton = this.BubbleFamPortBgSprite.AddUIComponent<UITextureSprite>();
            this.BubblePersonalCarButton.name = "BubblePersonalCarButton";
            this.BubblePersonalCarButton.width = 30f;
            this.BubblePersonalCarButton.height = 20f;
            this.BubblePersonalCarButton.texture = TextureDB.BubbleCarDisabled;
            this.BubblePersonalCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubblePersonalCarButton.absolutePosition = new Vector3(this.BubbleTargetIcon.absolutePosition.x, this.BubbleTargetIcon.absolutePosition.y + this.BubbleTargetIcon.height);
            this.BubblePersonalCarButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.PersonalVehicleID, eventParam);
            };
            this.BubblePersonalCarButton.BringToFront();
            this.BubbleActivityPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleActivityPanel.name = "BubbleActivityPanel";
            this.BubbleActivityPanel.width = 234f;
            this.BubbleActivityPanel.height = 18f;
            this.BubbleActivityPanel.relativePosition = new Vector3(4f, this.WorkBuildingPanel.relativePosition.y + 31f);
            this.BubbleActivitySprite = this.BubbleActivityPanel.AddUIComponent<UITextureSprite>();
            this.BubbleActivitySprite.name = "BubbleActivitySprite";
            this.BubbleActivitySprite.width = this.BubbleActivityPanel.width;
            this.BubbleActivitySprite.height = this.BubbleActivityPanel.height;
            this.BubbleActivitySprite.texture = TextureDB.BubbleBg1Special2;
            this.BubbleActivitySprite.relativePosition = Vector3.zero;
            this.BubbleActivityVehiclePanel = this.BubbleActivitySprite.AddUIComponent<UIPanel>();
            this.BubbleActivityVehiclePanel.name = "BubbleActivityVehiclePanel";
            this.BubbleActivityVehiclePanel.width = 234f;
            this.BubbleActivityVehiclePanel.height = 18f;
            this.BubbleActivityVehiclePanel.relativePosition = new Vector3(4f, 0f);
            this.FavCimsLastActivityVehicleButton = this.BubbleActivityVehiclePanel.AddUIComponent<UIButton>();
            this.FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
            this.FavCimsLastActivityVehicleButton.width = 18f;
            this.FavCimsLastActivityVehicleButton.height = 17f;
            this.FavCimsLastActivityVehicleButton.relativePosition = new Vector3(0f, 0f);
            this.FavCimsLastActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.MyVehicleID, eventParam);
            };
            this.FavCimsLastActivity = this.BubbleActivitySprite.AddUIComponent<UIButton>();
            this.FavCimsLastActivity.name = "FavCimsLastActivity";
            this.FavCimsLastActivity.width = this.BubbleActivitySprite.width - 27f;
            this.FavCimsLastActivity.height = this.BubbleActivitySprite.height;
            this.FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.FavCimsLastActivity.textHorizontalAlignment = 0;
            this.FavCimsLastActivity.playAudioEvents = true;
            this.FavCimsLastActivity.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.FavCimsLastActivity.font.size = 15;
            this.FavCimsLastActivity.textScale = 0.75f;
            this.FavCimsLastActivity.textPadding.left = 0;
            this.FavCimsLastActivity.textPadding.right = 5;
            this.FavCimsLastActivity.outlineColor = new Color32(0, 0, 0, 0);
            this.FavCimsLastActivity.outlineSize = 1;
            this.FavCimsLastActivity.textColor = new Color32(21, 59, 96, 140);
            this.FavCimsLastActivity.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.FavCimsLastActivity.pressedTextColor = new Color32(153, 0, 0, 0);
            this.FavCimsLastActivity.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.FavCimsLastActivity.disabledTextColor = new Color32(51, 51, 51, 160);
            this.FavCimsLastActivity.useDropShadow = true;
            this.FavCimsLastActivity.dropShadowOffset = new Vector2(1f, -1f);
            this.FavCimsLastActivity.dropShadowColor = new Color32(0, 0, 0, 0);
            this.FavCimsLastActivity.maximumSize = new Vector2(this.BubbleActivitySprite.width - 40f, this.BubbleActivitySprite.height);
            this.FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.FavCimsLastActivity.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.MyTargetID, eventParam);
            };
            this.FavCimsLastActivity.relativePosition = new Vector3(27f, 1f);
            this.BubbleDistrictPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel>();
            this.BubbleDistrictPanel.name = "BubbleDistrictPanel";
            this.BubbleDistrictPanel.width = 234f;
            this.BubbleDistrictPanel.height = 15f;
            this.BubbleDistrictPanel.relativePosition = new Vector3(4f, this.BubbleActivityPanel.relativePosition.y + 21f);
            this.BubbleDistrictSprite = this.BubbleDistrictPanel.AddUIComponent<UITextureSprite>();
            this.BubbleDistrictSprite.name = "BubbleDistrictSprite";
            this.BubbleDistrictSprite.width = this.BubbleDistrictPanel.width;
            this.BubbleDistrictSprite.height = this.BubbleDistrictPanel.height;
            this.BubbleDistrictSprite.texture = TextureDB.BubbleBg1Special2;
            this.BubbleDistrictSprite.relativePosition = Vector3.zero;
            this.FavCimsDistrictLabel = this.BubbleDistrictSprite.AddUIComponent<UIButton>();
            this.FavCimsDistrictLabel.name = "FavCimsDistrictLabel";
            this.FavCimsDistrictLabel.width = 60f;
            this.FavCimsDistrictLabel.height = 15f;
            this.FavCimsDistrictLabel.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.FavCimsDistrictLabel.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.FavCimsDistrictLabel.playAudioEvents = true;
            this.FavCimsDistrictLabel.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.FavCimsDistrictLabel.font.size = 15;
            this.FavCimsDistrictLabel.textScale = 0.7f;
            this.FavCimsDistrictLabel.textPadding.left = 0;
            this.FavCimsDistrictLabel.textPadding.right = 5;
            this.FavCimsDistrictLabel.outlineColor = new Color32(0, 0, 0, 0);
            this.FavCimsDistrictLabel.outlineSize = 1;
            this.FavCimsDistrictLabel.textColor = new Color32(153, 0, 0, 0);
            this.FavCimsDistrictLabel.isInteractive = false;
            this.FavCimsDistrictLabel.useDropShadow = true;
            this.FavCimsDistrictLabel.dropShadowOffset = new Vector2(1f, -1f);
            this.FavCimsDistrictLabel.dropShadowColor = new Color32(0, 0, 0, 0);
            this.FavCimsDistrictLabel.relativePosition = new Vector3(4f, 1f);
            this.FavCimsDistrictValue = this.BubbleDistrictSprite.AddUIComponent<UIButton>();
            this.FavCimsDistrictValue.name = "FavCimsDistrictValue";
            this.FavCimsDistrictValue.width = this.BubbleDistrictPanel.width - 74f;
            this.FavCimsDistrictValue.height = 15f;
            this.FavCimsDistrictValue.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.FavCimsDistrictValue.textHorizontalAlignment = 0;
            this.FavCimsDistrictValue.playAudioEvents = true;
            this.FavCimsDistrictValue.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.FavCimsDistrictValue.font.size = 15;
            this.FavCimsDistrictValue.textScale = 0.7f;
            this.FavCimsDistrictValue.textPadding.left = 0;
            this.FavCimsDistrictValue.textPadding.right = 5;
            this.FavCimsDistrictValue.outlineColor = new Color32(0, 0, 0, 0);
            this.FavCimsDistrictValue.outlineSize = 1;
            this.FavCimsDistrictValue.textColor = new Color32(21, 59, 96, 140);
            this.FavCimsDistrictValue.disabledTextColor = new Color32(21, 59, 96, 140);
            this.FavCimsDistrictValue.isEnabled = false;
            this.FavCimsDistrictValue.useDropShadow = true;
            this.FavCimsDistrictValue.dropShadowOffset = new Vector2(1f, -1f);
            this.FavCimsDistrictValue.dropShadowColor = new Color32(0, 0, 0, 0);
            this.FavCimsDistrictValue.relativePosition = new Vector3(64f, 1f);
            this.BubbleDetailsPanel = base.AddUIComponent<UIPanel>();
            this.BubbleDetailsPanel.name = "BubbleDetailsPanel";
            this.BubbleDetailsPanel.width = 235f;
            this.BubbleDetailsPanel.height = 60f;
            this.BubbleDetailsPanel.relativePosition = new Vector3(7f, this.BubbleFamilyPortraitPanel.relativePosition.y + this.BubbleFamilyPortraitPanel.height + 1f);
            this.BubbleDetailsBgSprite = this.BubbleDetailsPanel.AddUIComponent<UITextureSprite>();
            this.BubbleDetailsBgSprite.name = "BubbleFamPortBgSprite";
            this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
            this.BubbleDetailsBgSprite.relativePosition = Vector3.zero;
            this.BubbleHomeIcon = this.BubbleDetailsPanel.AddUIComponent<UITextureSprite>();
            this.BubbleHomeIcon.name = "FavCimsCitizenHomeSprite";
            this.BubbleHomeIcon.relativePosition = new Vector3(10f, 10f);
            this.BubbleHomeIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleHomeLevel = this.BubbleHomeIcon.AddUIComponent<UITextureSprite>();
            this.BubbleHomeLevel.name = "FavCimsCitizenResidentialLevelSprite";
            this.BubbleHomeLevel.relativePosition = Vector3.zero;
            this.BubbleHomePanel = this.BubbleDetailsPanel.AddUIComponent<UIPanel>();
            this.BubbleHomePanel.name = "BubbleHomePanel";
            this.BubbleHomePanel.width = 181f;
            this.BubbleHomePanel.height = 30f;
            this.BubbleHomePanel.maximumSize = new Vector2(181f, 40f);
            this.BubbleHomePanel.autoLayoutDirection = 0;
            this.BubbleHomePanel.autoLayout = true;
            this.BubbleHomePanel.relativePosition = new Vector3(40f, 4f);
            this.BubbleHomeName = this.BubbleHomePanel.AddUIComponent<UIButton>();
            this.BubbleHomeName.name = "BubbleHomeName";
            this.BubbleHomeName.width = this.BubbleHomePanel.width;
            this.BubbleHomeName.height = this.BubbleHomePanel.height;
            this.BubbleHomeName.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleHomeName.textHorizontalAlignment = 0;
            this.BubbleHomeName.playAudioEvents = true;
            this.BubbleHomeName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleHomeName.font.size = 15;
            this.BubbleHomeName.textScale = 0.9f;
            this.BubbleHomeName.wordWrap = true;
            this.BubbleHomeName.textPadding.left = 2;
            this.BubbleHomeName.textPadding.right = 5;
            this.BubbleHomeName.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleHomeName.outlineSize = 1;
            this.BubbleHomeName.textColor = new Color32(21, 59, 96, 140);
            this.BubbleHomeName.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleHomeName.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleHomeName.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleHomeName.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleHomeName.useDropShadow = true;
            this.BubbleHomeName.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleHomeName.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleHomeName.maximumSize = new Vector2(this.BubbleHomePanel.width, this.BubbleHomePanel.height);
            this.BubbleHomeName.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleHomeName.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.CitizenHomeID, eventParam);
            };
            this.BubbleHomeName.relativePosition = Vector3.zero;
            this.BubbleDetailsIconsPanel = this.BubbleDetailsPanel.AddUIComponent<UIPanel>();
            this.BubbleDetailsIconsPanel.name = "BubbleDetailsIconsPanel";
            this.BubbleDetailsIconsPanel.width = 181f;
            this.BubbleDetailsIconsPanel.height = 20f;
            this.BubbleDetailsIconsPanel.maximumSize = new Vector2(181f, 30f);
            this.BubbleDetailsIconsPanel.autoLayoutDirection = 0;
            this.BubbleDetailsIconsPanel.autoLayout = true;
            this.BubbleDetailsIconsPanel.relativePosition = new Vector3(this.BubbleHomePanel.relativePosition.x, 30f);
            this.BubbleDetailsElettricity = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsElettricity.name = "BubbleDetailsElettricity";
            this.BubbleDetailsElettricity.normalBgSprite = "ToolbarIconElectricity";
            this.BubbleDetailsElettricity.width = 20f;
            this.BubbleDetailsElettricity.height = 20f;
            this.BubbleDetailsElettricity.playAudioEvents = false;
            this.BubbleDetailsElettricity.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsWater = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsWater.name = "BubbleDetailsWater";
            this.BubbleDetailsWater.normalBgSprite = "IconPolicyWaterSaving";
            this.BubbleDetailsWater.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsWater.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsWater.playAudioEvents = false;
            this.BubbleDetailsWater.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsLandValue = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsLandValue.name = "BubbleDetailsLandValue";
            this.BubbleDetailsLandValue.normalBgSprite = "InfoIconLandValue";
            this.BubbleDetailsLandValue.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsLandValue.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsLandValue.playAudioEvents = false;
            this.BubbleDetailsLandValue.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsCrime = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsCrime.name = "BubbleDetailsCrime";
            this.BubbleDetailsCrime.normalBgSprite = "InfoIconCrime";
            this.BubbleDetailsCrime.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsCrime.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsCrime.playAudioEvents = false;
            this.BubbleDetailsCrime.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsNoise = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsNoise.name = "BubbleDetailsNoise";
            this.BubbleDetailsNoise.normalBgSprite = "InfoIconNoisePollution";
            this.BubbleDetailsNoise.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsNoise.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsNoise.playAudioEvents = false;
            this.BubbleDetailsNoise.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsGarbage = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsGarbage.name = "BubbleDetailsGarbage";
            this.BubbleDetailsGarbage.normalBgSprite = "InfoIconGarbage";
            this.BubbleDetailsGarbage.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsGarbage.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsGarbage.playAudioEvents = false;
            this.BubbleDetailsGarbage.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsDeath = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsDeath.name = "BubbleDetailsDeath";
            this.BubbleDetailsDeath.normalBgSprite = "NotificationIconVerySick";
            this.BubbleDetailsDeath.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsDeath.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsDeath.playAudioEvents = false;
            this.BubbleDetailsDeath.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsFire = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsFire.name = "BubbleDetailsFire";
            this.BubbleDetailsFire.normalBgSprite = "ToolbarIconFireDepartment";
            this.BubbleDetailsFire.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsFire.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsFire.playAudioEvents = false;
            this.BubbleDetailsFire.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleDetailsPollution = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton>();
            this.BubbleDetailsPollution.name = "BubbleDetailsPollution";
            this.BubbleDetailsPollution.normalBgSprite = "InfoIconPollution";
            this.BubbleDetailsPollution.width = this.BubbleDetailsElettricity.width;
            this.BubbleDetailsPollution.height = this.BubbleDetailsElettricity.height;
            this.BubbleDetailsPollution.playAudioEvents = false;
            this.BubbleDetailsPollution.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleFamilyBarPanel = base.AddUIComponent<UIPanel>();
            this.BubbleFamilyBarPanel.name = "BubbleFamilyBarPanel";
            this.BubbleFamilyBarPanel.width = 236f;
            this.BubbleFamilyBarPanel.height = 20f;
            this.BubbleFamilyBarPanel.relativePosition = new Vector3(7f, this.BubbleDetailsPanel.relativePosition.y + this.BubbleDetailsPanel.height + 2f);
            this.BubbleFamilyBarPanelBg = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyBarPanelBg.name = "BubbleFamilyBarPanelBg";
            this.BubbleFamilyBarPanelBg.width = this.BubbleFamilyBarPanel.width;
            this.BubbleFamilyBarPanelBg.height = this.BubbleFamilyBarPanel.height;
            this.BubbleFamilyBarPanelBg.texture = TextureDB.BubbleBgBarHover;
            this.BubbleFamilyBarPanelBg.relativePosition = Vector3.zero;
            this.BubbleFamilyBarLabel = this.BubbleFamilyBarPanel.AddUIComponent<UILabel>();
            this.BubbleFamilyBarLabel.name = "BubbleFamilyBarLabel";
            this.BubbleFamilyBarLabel.height = this.BubbleFamilyBarPanel.height;
            this.BubbleFamilyBarLabel.width = 221f;
            this.BubbleFamilyBarLabel.font.size = 11;
            this.BubbleFamilyBarLabel.textScale = 1f;
            this.BubbleFamilyBarLabel.textColor = new Color32(102, 0, 51, 220);
            this.BubbleFamilyBarLabel.relativePosition = new Vector3(7f, 2f);
            this.BubbleFamilyBarDogButton = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyBarDogButton.name = "BubbleFamilyBarDogButton";
            this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
            this.BubbleFamilyBarDogButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleFamilyBarDogButton.relativePosition = new Vector3(175f, 0f);
            this.BubbleFamilyBarDogButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.MyPetID, eventParam);
            };
            this.BubbleFamilyBarCarButton = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyBarCarButton.name = "BubbleFamilyBarCarButton";
            this.BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
            this.BubbleFamilyBarCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            this.BubbleFamilyBarCarButton.relativePosition = new Vector3(this.BubbleFamilyBarDogButton.relativePosition.x + this.BubbleFamilyBarDogButton.width + 10f, 0f);
            this.BubbleFamilyBarCarButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.FamilyVehicleID, eventParam);
            };
            this.BubbleFamilyPanel = base.AddUIComponent<UIPanel>();
            this.BubbleFamilyPanel.name = "BubbleFamilyPanel";
            this.BubbleFamilyPanel.width = 236f;
            this.BubbleFamilyPanel.height = 212f;
            this.BubbleFamilyPanel.clipChildren = true;
            this.BubbleFamilyPanel.padding = new RectOffset(0, 0, 0, 0);
            this.BubbleFamilyPanel.autoLayout = true;
            this.BubbleFamilyPanel.autoLayoutDirection = LayoutDirection.Vertical;
            this.BubbleFamilyPanel.relativePosition = new Vector3(7f, this.BubbleFamilyBarPanel.relativePosition.y + this.BubbleFamilyBarPanel.height);
            this.NoPartnerPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.NoPartnerPanel.name = "NoPartnerPanel";
            this.NoPartnerPanel.width = this.BubbleFamilyPanel.width;
            this.NoPartnerPanel.height = 52f;
            this.NoPartnerPanel.Hide();
            this.NoPartnerBSprite = this.NoPartnerPanel.AddUIComponent<UIButton>();
            this.NoPartnerBSprite.name = "NoPartnerBSprite";
            this.NoPartnerBSprite.normalBgSprite = "InfoIconHealthDisabled";
            this.NoPartnerBSprite.width = 36f;
            this.NoPartnerBSprite.height = 36f;
            this.NoPartnerBSprite.relativePosition = new Vector3(7f, 5f);
            this.NoPartnerFButton = this.NoPartnerPanel.AddUIComponent<UIButton>();
            this.NoPartnerFButton.name = "NoPartnerFButton";
            this.NoPartnerFButton.width = 155f;
            this.NoPartnerFButton.height = this.NoPartnerBSprite.height;
            this.NoPartnerFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.NoPartnerFButton.textHorizontalAlignment = 0;
            this.NoPartnerFButton.playAudioEvents = false;
            this.NoPartnerFButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.NoPartnerFButton.font.size = 16;
            this.NoPartnerFButton.textScale = 0.9f;
            this.NoPartnerFButton.wordWrap = true;
            this.NoPartnerFButton.useDropShadow = true;
            this.NoPartnerFButton.dropShadowOffset = new Vector2(1f, -1f);
            this.NoPartnerFButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.NoPartnerFButton.textPadding.left = 5;
            this.NoPartnerFButton.textPadding.right = 5;
            this.NoPartnerFButton.isEnabled = false;
            this.NoPartnerFButton.disabledTextColor = new Color32(51, 51, 51, 160);
            this.NoPartnerFButton.relativePosition = new Vector3(this.NoPartnerBSprite.relativePosition.x + this.NoPartnerBSprite.width, this.NoPartnerBSprite.relativePosition.y);
            this.PartnerPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.PartnerPanel.name = "PartnerPanel";
            this.PartnerPanel.width = this.BubbleFamilyPanel.width;
            this.PartnerPanel.height = 52f;
            this.PartnerPanel.clipChildren = true;
            this.PartnerPanel.padding = new RectOffset(0, 0, 0, 0);
            this.PartnerPanel.autoLayout = true;
            this.PartnerPanel.autoLayoutDirection = LayoutDirection.Vertical;
            this.BubblePartnerBgBar = this.PartnerPanel.AddUIComponent<UITextureSprite>();
            this.BubblePartnerBgBar.name = "BubblePartnerBgBar";
            this.BubblePartnerBgBar.width = this.PartnerPanel.width;
            this.BubblePartnerBgBar.height = 26f;
            this.BubblePartnerBgBar.texture = TextureDB.BubbleBgBar1;
            this.BubblePartnerLove = this.BubblePartnerBgBar.AddUIComponent<UIButton>();
            this.BubblePartnerLove.name = "BubblePartnerLove";
            this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
            this.BubblePartnerLove.width = 22f;
            this.BubblePartnerLove.height = 22f;
            this.BubblePartnerLove.isInteractive = false;
            this.BubblePartnerLove.relativePosition = new Vector3(7f, 2f);
            this.BubblePartnerName = this.BubblePartnerBgBar.AddUIComponent<UIButton>();
            this.BubblePartnerName.name = "BubblePartnerName";
            this.BubblePartnerName.width = 135f;
            this.BubblePartnerName.height = this.BubblePartnerBgBar.height;
            this.BubblePartnerName.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubblePartnerName.textHorizontalAlignment = 0;
            this.BubblePartnerName.playAudioEvents = true;
            this.BubblePartnerName.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubblePartnerName.font.size = 15;
            this.BubblePartnerName.textScale = 0.8f;
            this.BubblePartnerName.wordWrap = true;
            this.BubblePartnerName.useDropShadow = true;
            this.BubblePartnerName.dropShadowOffset = new Vector2(1f, -1f);
            this.BubblePartnerName.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubblePartnerName.textPadding.left = 5;
            this.BubblePartnerName.textPadding.right = 5;
            this.BubblePartnerName.textColor = new Color32(204, 204, 51, 40);
            this.BubblePartnerName.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubblePartnerName.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubblePartnerName.focusedTextColor = new Color32(153, 0, 0, 0);
            this.BubblePartnerName.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubblePartnerName.relativePosition = new Vector3(this.BubblePartnerLove.relativePosition.x + this.BubblePartnerLove.width, 2f);
            this.BubblePartnerName.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.PartnerID, eventParam);
            };
            this.BubbleParnerAgeButton = this.BubblePartnerBgBar.AddUIComponent<UIButton>();
            this.BubbleParnerAgeButton.name = "BubbleParnerAgeButton";
            this.BubbleParnerAgeButton.width = 23f;
            this.BubbleParnerAgeButton.height = 18f;
            this.BubbleParnerAgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleParnerAgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleParnerAgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleParnerAgeButton.textScale = 0.9f;
            this.BubbleParnerAgeButton.font.size = 15;
            this.BubbleParnerAgeButton.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleParnerAgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleParnerAgeButton.isInteractive = false;
            this.BubbleParnerAgeButton.relativePosition = new Vector3(this.BubblePartnerName.relativePosition.x + this.BubblePartnerName.width + 6f, 6f);
            this.BubblePartnerFollowToggler = this.BubblePartnerBgBar.AddUIComponent<UIButton>();
            this.BubblePartnerFollowToggler.name = "BubblePartnerFollowToggler";
            this.BubblePartnerFollowToggler.atlas = favCimsAtlas;
            this.BubblePartnerFollowToggler.size = new Vector2(18f, 18f);
            this.BubblePartnerFollowToggler.playAudioEvents = true;
            this.BubblePartnerFollowToggler.relativePosition = new Vector3(this.BubbleParnerAgeButton.relativePosition.x + this.BubbleParnerAgeButton.width + 12f, 4f);
            this.BubblePartnerFollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(this.PartnerID);
            };
            this.BubblePartnerActivityBar = this.PartnerPanel.AddUIComponent<UITextureSprite>();
            this.BubblePartnerActivityBar.name = "BubblePartnerActivityBar";
            this.BubblePartnerActivityBar.width = this.PartnerPanel.width;
            this.BubblePartnerActivityBar.height = 26f;
            this.BubblePartnerActivityBar.texture = TextureDB.BubbleBgBar2;
            this.BubblePartnerVehicleButton = this.BubblePartnerActivityBar.AddUIComponent<UIButton>();
            this.BubblePartnerVehicleButton.name = "BubblePartnerVehicleButton";
            this.BubblePartnerVehicleButton.width = 22f;
            this.BubblePartnerVehicleButton.height = 22f;
            this.BubblePartnerVehicleButton.relativePosition = new Vector3(7f, 2f);
            this.BubblePartnerVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.PartnerVehID, eventParam);
            };
            this.BubblePartnerDestination = this.BubblePartnerActivityBar.AddUIComponent<UIButton>();
            this.BubblePartnerDestination.name = "BubblePartnerDestination";
            this.BubblePartnerDestination.width = this.BubblePartnerActivityBar.width - this.BubblePartnerVehicleButton.width;
            this.BubblePartnerDestination.height = this.BubblePartnerActivityBar.height;
            this.BubblePartnerDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubblePartnerDestination.textHorizontalAlignment = 0;
            this.BubblePartnerDestination.playAudioEvents = true;
            this.BubblePartnerDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubblePartnerDestination.font.size = 15;
            this.BubblePartnerDestination.textScale = 0.75f;
            this.BubblePartnerDestination.wordWrap = true;
            this.BubblePartnerDestination.textPadding.left = 0;
            this.BubblePartnerDestination.textPadding.right = 5;
            this.BubblePartnerDestination.outlineColor = new Color32(0, 0, 0, 0);
            this.BubblePartnerDestination.outlineSize = 1;
            this.BubblePartnerDestination.textColor = new Color32(21, 59, 96, 140);
            this.BubblePartnerDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubblePartnerDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubblePartnerDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubblePartnerDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubblePartnerDestination.useDropShadow = true;
            this.BubblePartnerDestination.dropShadowOffset = new Vector2(1f, -1f);
            this.BubblePartnerDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubblePartnerDestination.maximumSize = new Vector2(this.BubblePartnerDestination.width, this.BubblePartnerActivityBar.height);
            this.BubblePartnerDestination.relativePosition = new Vector3(this.BubblePartnerVehicleButton.relativePosition.x + this.BubblePartnerVehicleButton.width + 5f, 2f);
            this.BubblePartnerDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.PartnerTarget, eventParam);
            };
            this.Parent1Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.Parent1Panel.name = "PartnerPanel";
            this.Parent1Panel.width = this.BubbleFamilyPanel.width;
            this.Parent1Panel.height = 52f;
            this.Parent1Panel.clipChildren = true;
            this.Parent1Panel.padding = new RectOffset(0, 0, 0, 0);
            this.Parent1Panel.autoLayout = true;
            this.Parent1Panel.autoLayoutDirection = LayoutDirection.Vertical;
            this.Parent1Panel.relativePosition = new Vector3(0f, 0f);
            this.Parent1Panel.Hide();
            this.BubbleParent1BgBar = this.Parent1Panel.AddUIComponent<UITextureSprite>();
            this.BubbleParent1BgBar.name = "BubbleParent1BgBar";
            this.BubbleParent1BgBar.width = this.Parent1Panel.width;
            this.BubbleParent1BgBar.height = 26f;
            this.BubbleParent1BgBar.texture = TextureDB.BubbleBgBar1;
            this.BubbleParent1Love = this.BubbleParent1BgBar.AddUIComponent<UIButton>();
            this.BubbleParent1Love.name = "BubbleParent1Love";
            this.BubbleParent1Love.normalBgSprite = "InfoIconAge";
            this.BubbleParent1Love.width = 22f;
            this.BubbleParent1Love.height = 22f;
            this.BubbleParent1Love.isInteractive = false;
            this.BubbleParent1Love.relativePosition = new Vector3(7f, 2f);
            this.BubbleParent1Name = this.BubbleParent1BgBar.AddUIComponent<UIButton>();
            this.BubbleParent1Name.name = "BubbleParent1Name";
            this.BubbleParent1Name.width = 135f;
            this.BubbleParent1Name.height = this.BubbleParent1BgBar.height;
            this.BubbleParent1Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleParent1Name.textHorizontalAlignment = 0;
            this.BubbleParent1Name.playAudioEvents = true;
            this.BubbleParent1Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleParent1Name.font.size = 15;
            this.BubbleParent1Name.textScale = 0.8f;
            this.BubbleParent1Name.wordWrap = true;
            this.BubbleParent1Name.useDropShadow = true;
            this.BubbleParent1Name.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleParent1Name.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleParent1Name.textPadding.left = 5;
            this.BubbleParent1Name.textPadding.right = 5;
            this.BubbleParent1Name.textColor = new Color32(204, 204, 51, 40);
            this.BubbleParent1Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleParent1Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleParent1Name.focusedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleParent1Name.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleParent1Name.relativePosition = new Vector3(this.BubbleParent1Love.relativePosition.x + this.BubbleParent1Love.width, 2f);
            this.BubbleParent1Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.Parent1ID, eventParam);
            };
            this.BubbleParent1AgeButton = this.BubbleParent1BgBar.AddUIComponent<UIButton>();
            this.BubbleParent1AgeButton.name = "BubbleParent1AgeButton";
            this.BubbleParent1AgeButton.width = 23f;
            this.BubbleParent1AgeButton.height = 18f;
            this.BubbleParent1AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleParent1AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleParent1AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleParent1AgeButton.textScale = 0.9f;
            this.BubbleParent1AgeButton.font.size = 15;
            this.BubbleParent1AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleParent1AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleParent1AgeButton.isInteractive = false;
            this.BubbleParent1AgeButton.relativePosition = new Vector3(this.BubbleParent1Name.relativePosition.x + this.BubbleParent1Name.width + 6f, 6f);
            this.BubbleParent1FollowToggler = this.BubbleParent1BgBar.AddUIComponent<UIButton>();
            this.BubbleParent1FollowToggler.name = "BubbleParent1FollowToggler";
            this.BubbleParent1FollowToggler.atlas = favCimsAtlas;
            this.BubbleParent1FollowToggler.size = new Vector2(18f, 18f);
            this.BubbleParent1FollowToggler.playAudioEvents = true;
            this.BubbleParent1FollowToggler.relativePosition = new Vector3(this.BubbleParent1AgeButton.relativePosition.x + this.BubbleParent1AgeButton.width + 12f, 4f);
            this.BubbleParent1FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(this.Parent1ID);
            };
            this.BubbleParent1ActivityBar = this.Parent1Panel.AddUIComponent<UITextureSprite>();
            this.BubbleParent1ActivityBar.name = "BubbleParent1ActivityBar";
            this.BubbleParent1ActivityBar.width = this.Parent1Panel.width;
            this.BubbleParent1ActivityBar.height = 26f;
            this.BubbleParent1ActivityBar.texture = TextureDB.BubbleBgBar2;
            this.BubbleParent1VehicleButton = this.BubbleParent1ActivityBar.AddUIComponent<UIButton>();
            this.BubbleParent1VehicleButton.name = "BubbleParent1VehicleButton";
            this.BubbleParent1VehicleButton.width = 22f;
            this.BubbleParent1VehicleButton.height = 22f;
            this.BubbleParent1VehicleButton.relativePosition = new Vector3(7f, 2f);
            this.BubbleParent1VehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent1VehID, eventParam);
            };
            this.BubbleParent1Destination = this.BubbleParent1ActivityBar.AddUIComponent<UIButton>();
            this.BubbleParent1Destination.name = "BubbleParent1Destination";
            this.BubbleParent1Destination.width = this.BubbleParent1ActivityBar.width - this.BubbleParent1VehicleButton.width;
            this.BubbleParent1Destination.height = this.BubbleParent1ActivityBar.height;
            this.BubbleParent1Destination.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleParent1Destination.textHorizontalAlignment = 0;
            this.BubbleParent1Destination.playAudioEvents = true;
            this.BubbleParent1Destination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleParent1Destination.font.size = 15;
            this.BubbleParent1Destination.textScale = 0.75f;
            this.BubbleParent1Destination.wordWrap = true;
            this.BubbleParent1Destination.textPadding.left = 0;
            this.BubbleParent1Destination.textPadding.right = 5;
            this.BubbleParent1Destination.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleParent1Destination.outlineSize = 1;
            this.BubbleParent1Destination.textColor = new Color32(21, 59, 96, 140);
            this.BubbleParent1Destination.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleParent1Destination.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleParent1Destination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleParent1Destination.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleParent1Destination.useDropShadow = true;
            this.BubbleParent1Destination.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleParent1Destination.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleParent1Destination.maximumSize = new Vector2(this.BubbleParent1Destination.width, this.BubbleParent1ActivityBar.height);
            this.BubbleParent1Destination.relativePosition = new Vector3(this.BubblePartnerDestination.relativePosition.x, 2f);
            this.BubbleParent1Destination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent1Target, eventParam);
            };
            this.NoChildsPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.NoChildsPanel.name = "NoChildsPanel";
            this.NoChildsPanel.width = this.BubbleFamilyPanel.width;
            this.NoChildsPanel.height = 52f;
            this.NoChildsPanel.Hide();
            this.NoChildsBSprite = this.NoChildsPanel.AddUIComponent<UIButton>();
            this.NoChildsBSprite.name = "NoChildsBSprite";
            this.NoChildsBSprite.normalBgSprite = "InfoIconHappinessDisabled";
            this.NoChildsBSprite.width = 36f;
            this.NoChildsBSprite.height = 36f;
            this.NoChildsBSprite.relativePosition = new Vector3(7f, 5f);
            this.NoChildsFButton = this.NoChildsPanel.AddUIComponent<UIButton>();
            this.NoChildsFButton.name = "NoChildsFButton";
            this.NoChildsFButton.width = 155f;
            this.NoChildsFButton.height = this.NoChildsBSprite.height;
            this.NoChildsFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.NoChildsFButton.textHorizontalAlignment = 0;
            this.NoChildsFButton.playAudioEvents = false;
            this.NoChildsFButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.NoChildsFButton.font.size = 16;
            this.NoChildsFButton.textScale = 0.9f;
            this.NoChildsFButton.wordWrap = true;
            this.NoChildsFButton.useDropShadow = true;
            this.NoChildsFButton.dropShadowOffset = new Vector2(1f, -1f);
            this.NoChildsFButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.NoChildsFButton.textPadding.left = 5;
            this.NoChildsFButton.textPadding.right = 5;
            this.NoChildsFButton.isEnabled = false;
            this.NoChildsFButton.disabledTextColor = new Color32(51, 51, 51, 160);
            this.NoChildsFButton.relativePosition = new Vector3(this.NoChildsBSprite.relativePosition.x + this.NoChildsBSprite.width, this.NoChildsBSprite.relativePosition.y);
            this.FamilyMember2Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.FamilyMember2Panel.name = "FamilyMember2Panel";
            this.FamilyMember2Panel.width = this.BubbleFamilyPanel.width;
            this.FamilyMember2Panel.height = 52f;
            this.FamilyMember2Panel.clipChildren = true;
            this.FamilyMember2Panel.padding = new RectOffset(0, 0, 0, 0);
            this.FamilyMember2Panel.autoLayout = true;
            this.FamilyMember2Panel.autoLayoutDirection = LayoutDirection.Vertical;
            this.FamilyMember2Panel.relativePosition = new Vector3(0f, 0f);
            this.FamilyMember2Panel.Hide();
            this.BubbleFamilyMember2BgBar = this.FamilyMember2Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember2BgBar.name = "BubbleFamilyMember2BgBar";
            this.BubbleFamilyMember2BgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember2BgBar.height = 26f;
            this.BubbleFamilyMember2BgBar.texture = TextureDB.BubbleBgBar1;
            this.BubbleFamilyMember2IconSprite = this.BubbleFamilyMember2BgBar.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember2IconSprite.name = "BubbleFamilyMember2IconSprite";
            this.BubbleFamilyMember2IconSprite.width = 18f;
            this.BubbleFamilyMember2IconSprite.height = 18f;
            this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            this.BubbleFamilyMember2IconSprite.relativePosition = new Vector3(7f, 4f);
            this.BubbleFamilyMember2Name = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember2Name.name = "BubbleFamilyMember2Name";
            this.BubbleFamilyMember2Name.width = 135f;
            this.BubbleFamilyMember2Name.height = this.BubbleFamilyMember2BgBar.height;
            this.BubbleFamilyMember2Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember2Name.textHorizontalAlignment = 0;
            this.BubbleFamilyMember2Name.playAudioEvents = true;
            this.BubbleFamilyMember2Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember2Name.font.size = 15;
            this.BubbleFamilyMember2Name.textScale = 0.8f;
            this.BubbleFamilyMember2Name.wordWrap = true;
            this.BubbleFamilyMember2Name.useDropShadow = true;
            this.BubbleFamilyMember2Name.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember2Name.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember2Name.textPadding.left = 5;
            this.BubbleFamilyMember2Name.textPadding.right = 5;
            this.BubbleFamilyMember2Name.textColor = new Color32(204, 204, 51, 40);
            this.BubbleFamilyMember2Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember2Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember2Name.focusedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember2Name.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember2Name.relativePosition = new Vector3(this.BubbleFamilyMember2IconSprite.relativePosition.x + this.BubbleFamilyMember2IconSprite.width + 2f, 2f);
            this.BubbleFamilyMember2Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.Parent2ID, eventParam);
            };
            this.BubbleFamilyMember2AgeButton = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember2AgeButton.name = "BubbleFamilyMember2AgeButton";
            this.BubbleFamilyMember2AgeButton.width = 23f;
            this.BubbleFamilyMember2AgeButton.height = 18f;
            this.BubbleFamilyMember2AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleFamilyMember2AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember2AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember2AgeButton.textScale = 0.9f;
            this.BubbleFamilyMember2AgeButton.font.size = 15;
            this.BubbleFamilyMember2AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember2AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember2AgeButton.isInteractive = false;
            this.BubbleFamilyMember2AgeButton.relativePosition = new Vector3(this.BubbleFamilyMember2Name.relativePosition.x + this.BubbleFamilyMember2Name.width + 6f, 6f);
            this.BubbleFamilyMember2FollowToggler = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember2FollowToggler.name = "BubbleFamilyMember2FollowToggler";
            this.BubbleFamilyMember2FollowToggler.atlas = favCimsAtlas;
            this.BubbleFamilyMember2FollowToggler.size = new Vector2(18f, 18f);
            this.BubbleFamilyMember2FollowToggler.playAudioEvents = true;
            this.BubbleFamilyMember2FollowToggler.relativePosition = new Vector3(this.BubbleFamilyMember2AgeButton.relativePosition.x + this.BubbleFamilyMember2AgeButton.width + 12f, 4f);
            this.BubbleFamilyMember2FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(this.Parent2ID);
            };
            this.BubbleFamilyMember2ActivityBgBar = this.FamilyMember2Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember2ActivityBgBar.name = "BubbleFamilyMember2ActivityBgBar";
            this.BubbleFamilyMember2ActivityBgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember2ActivityBgBar.height = 26f;
            this.BubbleFamilyMember2ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            this.BubbleFamilyMember2ActivityVehicleButton = this.BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember2ActivityVehicleButton.name = "BubbleFamilyMember2ActivityVehicleButton";
            this.BubbleFamilyMember2ActivityVehicleButton.width = 22f;
            this.BubbleFamilyMember2ActivityVehicleButton.height = 22f;
            this.BubbleFamilyMember2ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            this.BubbleFamilyMember2ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent2VehID, eventParam);
            };
            this.BubbleFamilyMember2ActivityDestination = this.BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember2ActivityDestination.name = "BubbleFamilyMember2ActivityDestination";
            this.BubbleFamilyMember2ActivityDestination.width = this.BubbleFamilyMember2ActivityBgBar.width - this.BubbleFamilyMember2ActivityVehicleButton.width;
            this.BubbleFamilyMember2ActivityDestination.height = this.BubbleFamilyMember2ActivityBgBar.height;
            this.BubbleFamilyMember2ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember2ActivityDestination.textHorizontalAlignment = 0;
            this.BubbleFamilyMember2ActivityDestination.playAudioEvents = true;
            this.BubbleFamilyMember2ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember2ActivityDestination.font.size = 15;
            this.BubbleFamilyMember2ActivityDestination.textScale = 0.75f;
            this.BubbleFamilyMember2ActivityDestination.wordWrap = true;
            this.BubbleFamilyMember2ActivityDestination.textPadding.left = 0;
            this.BubbleFamilyMember2ActivityDestination.textPadding.right = 5;
            this.BubbleFamilyMember2ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember2ActivityDestination.outlineSize = 1;
            this.BubbleFamilyMember2ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            this.BubbleFamilyMember2ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember2ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember2ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember2ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember2ActivityDestination.useDropShadow = true;
            this.BubbleFamilyMember2ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember2ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember2ActivityDestination.maximumSize = new Vector2(this.BubbleFamilyMember2ActivityDestination.width, this.BubbleFamilyMember2ActivityBgBar.height);
            this.BubbleFamilyMember2ActivityDestination.relativePosition = new Vector3(this.BubblePartnerDestination.relativePosition.x, 2f);
            this.BubbleFamilyMember2ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent2Target, eventParam);
            };
            this.FamilyMember3Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.FamilyMember3Panel.name = "FamilyMember3Panel";
            this.FamilyMember3Panel.width = this.BubbleFamilyPanel.width;
            this.FamilyMember3Panel.height = 52f;
            this.FamilyMember3Panel.clipChildren = true;
            this.FamilyMember3Panel.padding = new RectOffset(0, 0, 0, 0);
            this.FamilyMember3Panel.autoLayout = true;
            this.FamilyMember3Panel.autoLayoutDirection = LayoutDirection.Vertical;
            this.FamilyMember3Panel.relativePosition = new Vector3(0f, 0f);
            this.FamilyMember3Panel.Hide();
            this.BubbleFamilyMember3BgBar = this.FamilyMember3Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember3BgBar.name = "BubbleFamilyMember3BgBar";
            this.BubbleFamilyMember3BgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember3BgBar.height = 26f;
            this.BubbleFamilyMember3BgBar.texture = TextureDB.BubbleBgBar1;
            this.BubbleFamilyMember3IconSprite = this.BubbleFamilyMember3BgBar.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember3IconSprite.name = "BubbleFamilyMember3IconSprite";
            this.BubbleFamilyMember3IconSprite.width = 18f;
            this.BubbleFamilyMember3IconSprite.height = 18f;
            this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            this.BubbleFamilyMember3IconSprite.relativePosition = new Vector3(7f, 4f);
            this.BubbleFamilyMember3Name = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember3Name.name = "BubbleFamilyMember3Name";
            this.BubbleFamilyMember3Name.width = 135f;
            this.BubbleFamilyMember3Name.height = this.BubbleFamilyMember3BgBar.height;
            this.BubbleFamilyMember3Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember3Name.textHorizontalAlignment = 0;
            this.BubbleFamilyMember3Name.playAudioEvents = true;
            this.BubbleFamilyMember3Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember3Name.font.size = 15;
            this.BubbleFamilyMember3Name.textScale = 0.8f;
            this.BubbleFamilyMember3Name.wordWrap = true;
            this.BubbleFamilyMember3Name.useDropShadow = true;
            this.BubbleFamilyMember3Name.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember3Name.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember3Name.textPadding.left = 5;
            this.BubbleFamilyMember3Name.textPadding.right = 5;
            this.BubbleFamilyMember3Name.textColor = new Color32(204, 204, 51, 40);
            this.BubbleFamilyMember3Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember3Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember3Name.focusedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember3Name.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember3Name.relativePosition = new Vector3(this.BubbleFamilyMember3IconSprite.relativePosition.x + this.BubbleFamilyMember3IconSprite.width + 2f, 2f);
            this.BubbleFamilyMember3Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.Parent3ID, eventParam);
            };
            this.BubbleFamilyMember3AgeButton = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember3AgeButton.name = "BubbleFamilyMember3AgeButton";
            this.BubbleFamilyMember3AgeButton.width = 23f;
            this.BubbleFamilyMember3AgeButton.height = 18f;
            this.BubbleFamilyMember3AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleFamilyMember3AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember3AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember3AgeButton.textScale = 0.9f;
            this.BubbleFamilyMember3AgeButton.font.size = 15;
            this.BubbleFamilyMember3AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember3AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember3AgeButton.isInteractive = false;
            this.BubbleFamilyMember3AgeButton.relativePosition = new Vector3(this.BubbleFamilyMember3Name.relativePosition.x + this.BubbleFamilyMember3Name.width + 6f, 6f);
            this.BubbleFamilyMember3FollowToggler = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember3FollowToggler.name = "BubbleFamilyMember3FollowToggler";
            this.BubbleFamilyMember3FollowToggler.atlas = favCimsAtlas;
            this.BubbleFamilyMember3FollowToggler.size = new Vector2(18f, 18f);
            this.BubbleFamilyMember3FollowToggler.playAudioEvents = true;
            this.BubbleFamilyMember3FollowToggler.relativePosition = new Vector3(this.BubbleFamilyMember3AgeButton.relativePosition.x + this.BubbleFamilyMember3AgeButton.width + 12f, 4f);
            this.BubbleFamilyMember3FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(this.Parent3ID);
            };
            this.BubbleFamilyMember3ActivityBgBar = this.FamilyMember3Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember3ActivityBgBar.name = "BubbleFamilyMember3ActivityBgBar";
            this.BubbleFamilyMember3ActivityBgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember3ActivityBgBar.height = 26f;
            this.BubbleFamilyMember3ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            this.BubbleFamilyMember3ActivityVehicleButton = this.BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember3ActivityVehicleButton.name = "BubbleFamilyMember3ActivityVehicleButton";
            this.BubbleFamilyMember3ActivityVehicleButton.width = 22f;
            this.BubbleFamilyMember3ActivityVehicleButton.height = 22f;
            this.BubbleFamilyMember3ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            this.BubbleFamilyMember3ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent3VehID, eventParam);
            };
            this.BubbleFamilyMember3ActivityDestination = this.BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember3ActivityDestination.name = "BubbleFamilyMember3ActivityDestination";
            this.BubbleFamilyMember3ActivityDestination.width = this.BubbleFamilyMember3ActivityBgBar.width - this.BubbleFamilyMember3ActivityVehicleButton.width;
            this.BubbleFamilyMember3ActivityDestination.height = this.BubbleFamilyMember3ActivityBgBar.height;
            this.BubbleFamilyMember3ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember3ActivityDestination.textHorizontalAlignment = 0;
            this.BubbleFamilyMember3ActivityDestination.playAudioEvents = true;
            this.BubbleFamilyMember3ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember3ActivityDestination.font.size = 15;
            this.BubbleFamilyMember3ActivityDestination.textScale = 0.75f;
            this.BubbleFamilyMember3ActivityDestination.wordWrap = true;
            this.BubbleFamilyMember3ActivityDestination.textPadding.left = 0;
            this.BubbleFamilyMember3ActivityDestination.textPadding.right = 5;
            this.BubbleFamilyMember3ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember3ActivityDestination.outlineSize = 1;
            this.BubbleFamilyMember3ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            this.BubbleFamilyMember3ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember3ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember3ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember3ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember3ActivityDestination.useDropShadow = true;
            this.BubbleFamilyMember3ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember3ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember3ActivityDestination.maximumSize = new Vector2(this.BubbleFamilyMember3ActivityDestination.width, this.BubbleFamilyMember3ActivityBgBar.height);
            this.BubbleFamilyMember3ActivityDestination.relativePosition = new Vector3(this.BubblePartnerDestination.relativePosition.x, 2f);
            this.BubbleFamilyMember3ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent3Target, eventParam);
            };
            this.FamilyMember4Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
            this.FamilyMember4Panel.name = "FamilyMember4Panel";
            this.FamilyMember4Panel.width = this.BubbleFamilyPanel.width;
            this.FamilyMember4Panel.height = 52f;
            this.FamilyMember4Panel.clipChildren = true;
            this.FamilyMember4Panel.padding = new RectOffset(0, 0, 0, 0);
            this.FamilyMember4Panel.autoLayout = true;
            this.FamilyMember4Panel.autoLayoutDirection = LayoutDirection.Vertical;
            this.FamilyMember4Panel.relativePosition = new Vector3(0f, 0f);
            this.FamilyMember4Panel.Hide();
            this.BubbleFamilyMember4BgBar = this.FamilyMember4Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember4BgBar.name = "BubbleFamilyMember4BgBar";
            this.BubbleFamilyMember4BgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember4BgBar.height = 26f;
            this.BubbleFamilyMember4BgBar.texture = TextureDB.BubbleBgBar1;
            this.BubbleFamilyMember4IconSprite = this.BubbleFamilyMember4BgBar.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember4IconSprite.name = "BubbleFamilyMember4IconSprite";
            this.BubbleFamilyMember4IconSprite.width = 18f;
            this.BubbleFamilyMember4IconSprite.height = 18f;
            this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
            this.BubbleFamilyMember4IconSprite.relativePosition = new Vector3(7f, 4f);
            this.BubbleFamilyMember4Name = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember4Name.name = "BubbleFamilyMember4Name";
            this.BubbleFamilyMember4Name.width = 135f;
            this.BubbleFamilyMember4Name.height = this.BubbleFamilyMember4BgBar.height;
            this.BubbleFamilyMember4Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember4Name.textHorizontalAlignment = 0;
            this.BubbleFamilyMember4Name.playAudioEvents = true;
            this.BubbleFamilyMember4Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember4Name.font.size = 15;
            this.BubbleFamilyMember4Name.textScale = 0.8f;
            this.BubbleFamilyMember4Name.wordWrap = true;
            this.BubbleFamilyMember4Name.useDropShadow = true;
            this.BubbleFamilyMember4Name.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember4Name.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember4Name.textPadding.left = 5;
            this.BubbleFamilyMember4Name.textPadding.right = 5;
            this.BubbleFamilyMember4Name.textColor = new Color32(204, 204, 51, 40);
            this.BubbleFamilyMember4Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember4Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember4Name.focusedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember4Name.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember4Name.relativePosition = new Vector3(this.BubbleFamilyMember4IconSprite.relativePosition.x + this.BubbleFamilyMember4IconSprite.width + 2f, 2f);
            this.BubbleFamilyMember4Name.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToCitizen(this.Parent4ID, eventParam);
            };
            this.BubbleFamilyMember4AgeButton = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember4AgeButton.name = "BubbleFamilyMember4AgeButton";
            this.BubbleFamilyMember4AgeButton.width = 23f;
            this.BubbleFamilyMember4AgeButton.height = 18f;
            this.BubbleFamilyMember4AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            this.BubbleFamilyMember4AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember4AgeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember4AgeButton.textScale = 0.9f;
            this.BubbleFamilyMember4AgeButton.font.size = 15;
            this.BubbleFamilyMember4AgeButton.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember4AgeButton.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember4AgeButton.isInteractive = false;
            this.BubbleFamilyMember4AgeButton.relativePosition = new Vector3(this.BubbleFamilyMember4Name.relativePosition.x + this.BubbleFamilyMember4Name.width + 6f, 6f);
            this.BubbleFamilyMember4FollowToggler = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember4FollowToggler.name = "BubbleFamilyMember4FollowToggler";
            this.BubbleFamilyMember4FollowToggler.atlas = favCimsAtlas;
            this.BubbleFamilyMember4FollowToggler.width = 18f;
            this.BubbleFamilyMember4FollowToggler.height = 18f;
            this.BubbleFamilyMember4FollowToggler.playAudioEvents = true;
            this.BubbleFamilyMember4FollowToggler.relativePosition = new Vector3(this.BubbleFamilyMember4AgeButton.relativePosition.x + this.BubbleFamilyMember4AgeButton.width + 12f, 4f);
            this.BubbleFamilyMember4FollowToggler.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.AddToFavorites(this.Parent4ID);
            };
            this.BubbleFamilyMember4ActivityBgBar = this.FamilyMember4Panel.AddUIComponent<UITextureSprite>();
            this.BubbleFamilyMember4ActivityBgBar.name = "BubbleFamilyMember4ActivityBgBar";
            this.BubbleFamilyMember4ActivityBgBar.width = this.BubbleFamilyPanel.width;
            this.BubbleFamilyMember4ActivityBgBar.height = 26f;
            this.BubbleFamilyMember4ActivityBgBar.texture = TextureDB.BubbleBgBar2;
            this.BubbleFamilyMember4ActivityVehicleButton = this.BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember4ActivityVehicleButton.name = "BubbleFamilyMember4ActivityVehicleButton";
            this.BubbleFamilyMember4ActivityVehicleButton.width = 22f;
            this.BubbleFamilyMember4ActivityVehicleButton.height = 22f;
            this.BubbleFamilyMember4ActivityVehicleButton.relativePosition = new Vector3(7f, 2f);
            this.BubbleFamilyMember4ActivityVehicleButton.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent4VehID, eventParam);
            };
            this.BubbleFamilyMember4ActivityDestination = this.BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton>();
            this.BubbleFamilyMember4ActivityDestination.name = "BubbleFamilyMember4ActivityDestination";
            this.BubbleFamilyMember4ActivityDestination.width = this.BubbleFamilyMember4ActivityBgBar.width - this.BubbleFamilyMember4ActivityVehicleButton.width;
            this.BubbleFamilyMember4ActivityDestination.height = this.BubbleFamilyMember4ActivityBgBar.height;
            this.BubbleFamilyMember4ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
            this.BubbleFamilyMember4ActivityDestination.textHorizontalAlignment = 0;
            this.BubbleFamilyMember4ActivityDestination.playAudioEvents = true;
            this.BubbleFamilyMember4ActivityDestination.font = UIDynamicFont.FindByName("OpenSans-Regular");
            this.BubbleFamilyMember4ActivityDestination.font.size = 15;
            this.BubbleFamilyMember4ActivityDestination.textScale = 0.75f;
            this.BubbleFamilyMember4ActivityDestination.wordWrap = true;
            this.BubbleFamilyMember4ActivityDestination.textPadding.left = 0;
            this.BubbleFamilyMember4ActivityDestination.textPadding.right = 5;
            this.BubbleFamilyMember4ActivityDestination.outlineColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember4ActivityDestination.outlineSize = 1;
            this.BubbleFamilyMember4ActivityDestination.textColor = new Color32(21, 59, 96, 140);
            this.BubbleFamilyMember4ActivityDestination.hoveredTextColor = new Color32(204, 102, 0, 20);
            this.BubbleFamilyMember4ActivityDestination.pressedTextColor = new Color32(153, 0, 0, 0);
            this.BubbleFamilyMember4ActivityDestination.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            this.BubbleFamilyMember4ActivityDestination.disabledTextColor = new Color32(51, 51, 51, 160);
            this.BubbleFamilyMember4ActivityDestination.useDropShadow = true;
            this.BubbleFamilyMember4ActivityDestination.dropShadowOffset = new Vector2(1f, -1f);
            this.BubbleFamilyMember4ActivityDestination.dropShadowColor = new Color32(0, 0, 0, 0);
            this.BubbleFamilyMember4ActivityDestination.maximumSize = new Vector2(this.BubbleFamilyMember4ActivityDestination.width, this.BubbleFamilyMember4ActivityBgBar.height);
            this.BubbleFamilyMember4ActivityDestination.relativePosition = new Vector3(this.BubblePartnerDestination.relativePosition.x, 2f);
            this.BubbleFamilyMember4ActivityDestination.eventMouseUp += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                this.GoToInstance(this.Parent4Target, eventParam);
            };
            base.absolutePosition = new Vector3((float)random.Next(num, num2), 200f);
        }

        public override void Update()
        {
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool isEmpty = this.MyInstanceID.IsEmpty;
                if (!isEmpty)
                {
                    bool flag = this.MyInstanceID != this.PrevMyInstanceID;
                    if (flag)
                    {
                        this.DogOwner = 0U;
                        this.FirstRun = true;
                        this.PrevMyInstanceID = this.MyInstanceID;
                    }
                    this.seconds -= 1f * Time.deltaTime;
                    bool flag2 = this.seconds <= 0f || this.FirstRun;
                    if (flag2)
                    {
                        this.execute = true;
                        this.seconds = 0.3f;
                    }
                    else
                    {
                        this.execute = false;
                    }
                }
            }
        }

        public override void LateUpdate()
        {
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool flag = this.execute || this.FirstRun;
                if (flag)
                {
                    bool isVisible = base.isVisible;
                    if (isVisible)
                    {
                        try
                        {
                            this.citizen = this.MyInstanceID.Citizen;
                            this.CitizenData = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen];
                            this.BubbleCitizenAge.text = FavCimsLang.text("FavCimsAgeColText_text");
                            this.BubbleCitizenAgePhase.text = FavCimsLang.text("FavCimsAgePhaseColText_text");
                            this.BubbleCitizenEducation.text = FavCimsLang.text("FavCimsEduColText_text");
                            this.BubbleWealthSprite.tooltip = FavCimsLang.text("Wealth_Label");
                            this.FavCimsDistrictLabel.text = FavCimsLang.text("District_Label");
                            this.BubbleFamilyBarLabel.text = FavCimsLang.text("Citizen_Family_unit");
                            this.NoChildsFButton.text = FavCimsLang.text("Citizen_Details_No_Childs");
                            this.NoPartnerFButton.text = FavCimsLang.text("Citizen_Details_No_Partner");
                            this.MyCitizenUnit = this.CitizenData.GetContainingUnit(this.citizen, this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenData.m_homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                            bool flag2 = this.MyCitizenUnit > 0U;
                            if (flag2)
                            {
                                this.Family = this.MyCitizen.m_units.m_buffer[(int)this.MyCitizenUnit];
                                this.BubbleHeaderCitizenName.text = this.MyCitizen.GetCitizenName(this.citizen);
                                Citizen.Gender gender = Citizen.GetGender(this.citizen);
                                bool flag3 = gender == Citizen.Gender.Female;
                                if (flag3)
                                {
                                    this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    this.BubbleHeaderCitizenName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    this.BubbleHeaderCitizenName.hoveredTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    this.BubbleHeaderCitizenName.pressedTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    this.BubbleHeaderCitizenName.focusedTextColor = new Color32(byte.MaxValue, 102, 204, 213);
                                }
                                else
                                {
                                    this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    this.BubbleHeaderCitizenName.textColor = new Color32(204, 204, 51, 40);
                                    this.BubbleHeaderCitizenName.hoveredTextColor = new Color32(204, 204, 51, 40);
                                    this.BubbleHeaderCitizenName.pressedTextColor = new Color32(204, 204, 51, 40);
                                    this.BubbleHeaderCitizenName.focusedTextColor = new Color32(204, 204, 51, 40);
                                }
                                int health = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_health;
                                string healthString = FamilyPanelTemplate.GetHealthString(Citizen.GetHealthLevel(health));
                                this.BubbleHealthSprite.normalBgSprite = healthString;
                                this.BubbleHealthSprite.tooltip = Locale.Get("INFO_HEALTH_TITLE");
                                this.BubbleHealthValue.text = FavCimsLang.text("Health_Level_" + FamilyPanelTemplate.sHealthLevels[(int)Citizen.GetHealthLevel(health)] + "_" + Citizen.GetGender(this.citizen).ToString());
                                switch (Citizen.GetHealthLevel(health))
                                {
                                    case Citizen.Health.VerySick:
                                        this.BubbleHealthValue.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                        break;
                                    case Citizen.Health.Sick:
                                        this.BubbleHealthValue.textColor = new Color32(153, 0, 0, 0);
                                        break;
                                    case Citizen.Health.PoorHealth:
                                        this.BubbleHealthValue.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        break;
                                    case Citizen.Health.Healthy:
                                        this.BubbleHealthValue.textColor = new Color32(102, 204, 0, 60);
                                        break;
                                    case Citizen.Health.VeryHealthy:
                                        this.BubbleHealthValue.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                    case Citizen.Health.ExcellentHealth:
                                        this.BubbleHealthValue.textColor = new Color32(0, 102, 51, 100);
                                        break;
                                }
                                Citizen.Education educationLevel = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].EducationLevel;
                                string text = educationLevel.ToString();
                                this.BubbleRow1EducationTooltipArea.tooltip = FavCimsLang.text("Education_" + text + "_" + Citizen.GetGender(this.citizen).ToString());
                                bool flag4 = text == "ThreeSchools";
                                if (flag4)
                                {
                                    this.BubbleEduLevel3.isEnabled = true;
                                    this.BubbleEduLevel2.isEnabled = true;
                                    this.BubbleEduLevel1.isEnabled = true;
                                }
                                else
                                {
                                    bool flag5 = text == "TwoSchools";
                                    if (flag5)
                                    {
                                        this.BubbleEduLevel3.isEnabled = false;
                                        this.BubbleEduLevel2.isEnabled = true;
                                        this.BubbleEduLevel1.isEnabled = true;
                                    }
                                    else
                                    {
                                        bool flag6 = text == "OneSchool";
                                        if (flag6)
                                        {
                                            this.BubbleEduLevel3.isEnabled = false;
                                            this.BubbleEduLevel2.isEnabled = false;
                                            this.BubbleEduLevel1.isEnabled = true;
                                        }
                                        else
                                        {
                                            this.BubbleEduLevel3.isEnabled = false;
                                            this.BubbleEduLevel2.isEnabled = false;
                                            this.BubbleEduLevel1.isEnabled = false;
                                        }
                                    }
                                }
                                int wellbeing = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_wellbeing;
                                string happinessString = FamilyPanelTemplate.GetHappinessString(Citizen.GetHappinessLevel(wellbeing));
                                this.BubbleRow2WellbeingIcon.normalBgSprite = happinessString;
                                this.BubbleRow2WellbeingIcon.tooltip = FavCimsLang.text("WellBeingLabel") + FavCimsLang.text(happinessString);
                                Citizen.Wealth wealthLevel = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].WealthLevel;
                                this.BubbleRow2WealthValueVal.tooltip = FavCimsLang.text("Wealth_Label");
                                bool flag7 = wealthLevel == 0;
                                if (flag7)
                                {
                                    this.BubbleRow2WealthValueVal.text = FavCimsLang.text("Low_Wealth_" + Citizen.GetGender(this.citizen).ToString());
                                    this.BubbleRow2WealthValueVal.textColor = new Color32(153, 0, 0, 0);
                                }
                                else
                                {
                                    bool flag8 = wealthLevel == Citizen.Wealth.Medium;
                                    if (flag8)
                                    {
                                        this.BubbleRow2WealthValueVal.text = FavCimsLang.text("Mid_Wealth_" + Citizen.GetGender(this.citizen).ToString());
                                        this.BubbleRow2WealthValueVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                    }
                                    else
                                    {
                                        this.BubbleRow2WealthValueVal.text = FavCimsLang.text("High_Wealth_" + Citizen.GetGender(this.citizen).ToString());
                                        this.BubbleRow2WealthValueVal.textColor = new Color32(102, 204, 0, 60);
                                    }
                                }
                                int happiness = Citizen.GetHappiness(health, wellbeing);
                                string happinessString2 = FamilyPanelTemplate.GetHappinessString(Citizen.GetHappinessLevel(happiness));
                                bool flag9 = this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)this.citizen)))].Arrested && this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)this.citizen)))].Criminal;
                                if (flag9)
                                {
                                    this.BubbleRow1HappyIcon.atlas = MyAtlas.FavCimsAtlas;
                                    this.BubbleRow1HappyIcon.normalBgSprite = "FavCimsCrimeArrested";
                                    this.BubbleRow1HappyIcon.tooltip = FavCimsLang.text("Citizen_Arrested");
                                }
                                else
                                {
                                    this.BubbleRow1HappyIcon.atlas = UIView.GetAView().defaultAtlas;
                                    this.BubbleRow1HappyIcon.normalBgSprite = happinessString2;
                                    this.BubbleRow1HappyIcon.tooltip = FavCimsLang.text("HappinessLabel") + FavCimsLang.text(happinessString2);
                                }
                                int age = (int)this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_age;
                                string text2 = Citizen.GetAgeGroup(age).ToString();
                                this.BubbleCitizenAgePhaseVal.text = FavCimsLang.text("AgePhase_" + text2 + "_" + Citizen.GetGender(this.citizen).ToString());
                                this.RealAge = FavCimsCore.CalculateCitizenAge(age);
                                bool flag10 = this.RealAge <= 12;
                                if (flag10)
                                {
                                    this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                    this.BubbleCitizenAgeVal.textColor = new Color32(83, 166, 0, 60);
                                    this.BubbleCitizenAgePhaseVal.textColor = new Color32(83, 166, 0, 60);
                                }
                                else
                                {
                                    bool flag11 = this.RealAge <= 19;
                                    if (flag11)
                                    {
                                        this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                        this.BubbleCitizenAgeVal.textColor = new Color32(0, 102, 51, 100);
                                        this.BubbleCitizenAgePhaseVal.textColor = new Color32(0, 102, 51, 100);
                                    }
                                    else
                                    {
                                        bool flag12 = this.RealAge <= 25;
                                        if (flag12)
                                        {
                                            this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                            this.BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            this.BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                        }
                                        else
                                        {
                                            bool flag13 = this.RealAge <= 65;
                                            if (flag13)
                                            {
                                                this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                                this.BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                this.BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                            }
                                            else
                                            {
                                                bool flag14 = this.RealAge <= 90;
                                                if (flag14)
                                                {
                                                    this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                                    this.BubbleCitizenAgeVal.textColor = new Color32(153, 0, 0, 0);
                                                    this.BubbleCitizenAgePhaseVal.textColor = new Color32(153, 0, 0, 0);
                                                }
                                                else
                                                {
                                                    this.BubbleCitizenAgeVal.text = this.RealAge.ToString();
                                                    this.BubbleCitizenAgeVal.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    this.BubbleCitizenAgePhaseVal.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                }
                                            }
                                        }
                                    }
                                }
                                this.WorkPlace = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_workBuilding;
                                bool flag15 = MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].GetCurrentSchoolLevel(this.citizen) != ItemClass.Level.None;
                                if (flag15)
                                {
                                    this.isStudent = true;
                                    this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                    this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    this.FavCimsWorkingPlace.tooltip = Locale.Get("CITIZEN_SCHOOL_LEVEL", this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].GetCurrentSchoolLevel(this.citizen).ToString()) + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
                                }
                                else
                                {
                                    bool flag16 = this.WorkPlace == 0;
                                    if (flag16)
                                    {
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                        bool flag17 = age >= 180;
                                        if (flag17)
                                        {
                                            this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
                                            this.FavCimsWorkingPlace.text = FavCimsLang.text("Citizen_Retired");
                                            this.FavCimsWorkingPlace.isEnabled = false;
                                            this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Citizen_Retired_tooltip");
                                            this.FavCimsWorkingPlaceSprite.tooltip = null;
                                            this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                            this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                        else
                                        {
                                            this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture;
                                            this.FavCimsWorkingPlace.text = Locale.Get("CITIZEN_OCCUPATION_UNEMPLOYED");
                                            this.FavCimsWorkingPlace.isEnabled = false;
                                            this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Unemployed_tooltip");
                                            this.FavCimsWorkingPlaceSprite.tooltip = null;
                                            this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                            this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                }
                                bool flag18 = this.WorkPlace > 0;
                                if (flag18)
                                {
                                    string text3 = string.Empty;
                                    bool flag19 = !this.isStudent;
                                    if (flag19)
                                    {
                                        CommonBuildingAI commonBuildingAI = this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlace].Info.m_buildingAI as CommonBuildingAI;
                                        bool flag20 = commonBuildingAI != null;
                                        if (flag20)
                                        {
                                            text3 = commonBuildingAI.GetTitle(gender, educationLevel, this.WorkPlace, this.citizen);
                                        }
                                        bool flag21 = text3 == string.Empty;
                                        if (flag21)
                                        {
                                            int num = new Randomizer((uint)this.WorkPlace + this.citizen).Int32(1, 5);
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
                                    this.WorkPlaceID.Building = this.WorkPlace;
                                    this.FavCimsWorkingPlace.text = text3 + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
                                    this.FavCimsWorkingPlace.isEnabled = true;
                                    this.WorkInfo = this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlaceID.Index].Info;
                                    bool flag22 = this.WorkInfo.m_class.m_service == ItemClass.Service.Commercial;
                                    if (flag22)
                                    {
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                        bool flag23 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh;
                                        if (flag23)
                                        {
                                            this.FavCimsWorkingPlace.textColor = new Color32(0, 51, 153, 147);
                                            this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                            this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 4.ToString());
                                        }
                                        else
                                        {
                                            bool flag24 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialEco;
                                            if (flag24)
                                            {
                                                this.FavCimsWorkingPlace.textColor = new Color32(0, 150, 136, 116);
                                                this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialEco");
                                            }
                                            else
                                            {
                                                bool flag25 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLeisure;
                                                if (flag25)
                                                {
                                                    this.FavCimsWorkingPlace.textColor = new Color32(219, 68, 55, 3);
                                                    this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                    this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialLeisure");
                                                }
                                                else
                                                {
                                                    bool flag26 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialTourist;
                                                    if (flag26)
                                                    {
                                                        this.FavCimsWorkingPlace.textColor = new Color32(156, 39, 176, 194);
                                                        this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
                                                        this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Buildings_Type_CommercialTourist");
                                                    }
                                                    else
                                                    {
                                                        this.FavCimsWorkingPlace.textColor = new Color32(0, 153, 204, 130);
                                                        this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 3.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        bool flag27 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow;
                                        if (flag27)
                                        {
                                            ItemClass.Level level = this.WorkInfo.m_class.m_level;
                                            ItemClass.Level level2 = level;
                                            if (level2 != ItemClass.Level.Level2)
                                            {
                                                if (level2 != ItemClass.Level.Level3)
                                                {
                                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[1];
                                                }
                                                else
                                                {
                                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[3];
                                                }
                                            }
                                            else
                                            {
                                                this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel[2];
                                            }
                                        }
                                        else
                                        {
                                            this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                        }
                                    }
                                    else
                                    {
                                        bool flag28 = this.WorkInfo.m_class.m_service == ItemClass.Service.Industrial;
                                        if (flag28)
                                        {
                                            this.FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Industrial");
                                            ItemClass.SubService subService = this.WorkInfo.m_class.m_subService;
                                            ItemClass.SubService subService2 = subService;
                                            switch (subService2)
                                            {
                                                case ItemClass.SubService.IndustrialForestry:
                                                    this.FavCimsWorkingPlaceSprite.texture = null;
                                                    this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                                    break;
                                                case ItemClass.SubService.IndustrialFarming:
                                                    this.FavCimsWorkingPlaceSprite.texture = null;
                                                    this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                                    break;
                                                case ItemClass.SubService.IndustrialOil:
                                                    this.FavCimsWorkingPlaceSprite.texture = null;
                                                    this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                                    break;
                                                case ItemClass.SubService.IndustrialOre:
                                                    this.FavCimsWorkingPlaceSprite.texture = null;
                                                    this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                                    break;
                                                default:
                                                    switch (subService2)
                                                    {
                                                        case ItemClass.SubService.PlayerIndustryForestry:
                                                            this.FavCimsWorkingPlaceSprite.texture = null;
                                                            this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryFarming:
                                                            this.FavCimsWorkingPlaceSprite.texture = null;
                                                            this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOil:
                                                            this.FavCimsWorkingPlaceSprite.texture = null;
                                                            this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                                            break;
                                                        case ItemClass.SubService.PlayerIndustryOre:
                                                            this.FavCimsWorkingPlaceSprite.texture = null;
                                                            this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                                            break;
                                                        default:
                                                            this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                                            this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
                                                            break;
                                                    }
                                                    break;
                                            }
                                            bool flag29 = this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric;
                                            if (flag29)
                                            {
                                                ItemClass.Level level3 = this.WorkInfo.m_class.m_level;
                                                ItemClass.Level level4 = level3;
                                                if (level4 != ItemClass.Level.Level2)
                                                {
                                                    if (level4 != ItemClass.Level.Level3)
                                                    {
                                                        this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[1];
                                                    }
                                                    else
                                                    {
                                                        this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[3];
                                                    }
                                                }
                                                else
                                                {
                                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel[2];
                                                }
                                            }
                                            else
                                            {
                                                this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                            }
                                        }
                                        else
                                        {
                                            bool flag30 = this.WorkInfo.m_class.m_service == ItemClass.Service.Office;
                                            if (flag30)
                                            {
                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
                                                this.FavCimsWorkingPlace.textColor = new Color32(0, 204, byte.MaxValue, 128);
                                                this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;
                                                ItemClass.SubService subService3 = this.WorkInfo.m_class.m_subService;
                                                ItemClass.SubService subService4 = subService3;
                                                if (subService4 != ItemClass.SubService.OfficeHightech)
                                                {
                                                    this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office");
                                                }
                                                else
                                                {
                                                    this.FavCimsWorkingPlace.tooltip = Locale.Get("ZONEDBUILDING_TITLE", "Office") + " Eco";
                                                }
                                                ItemClass.Level level5 = this.WorkInfo.m_class.m_level;
                                                ItemClass.Level level6 = level5;
                                                if (level6 != ItemClass.Level.Level2)
                                                {
                                                    if (level6 != ItemClass.Level.Level3)
                                                    {
                                                        this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[1];
                                                    }
                                                    else
                                                    {
                                                        this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[3];
                                                    }
                                                }
                                                else
                                                {
                                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel[2];
                                                }
                                            }
                                            else
                                            {
                                                this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                                this.FavCimsWorkingPlaceSprite.texture = null;
                                                this.FavCimsWorkingPlace.textColor = new Color32(153, 102, 51, 20);
                                                switch (this.WorkInfo.m_class.m_service)
                                                {
                                                    case ItemClass.Service.Electricity:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyPowerSaving";
                                                        this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Electricity_job");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Water:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
                                                        this.FavCimsWorkingPlace.tooltip = FavCimsLang.text("Water_job");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Beautification:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarBeautificationParksnPlazas";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Beautification");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Garbage:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyRecycling";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Garbage");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.HealthCare:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconHealthcareFocused";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Healthcare");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.PoliceDepartment:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconPolice";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Police");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Education:
                                                        this.FavCimsWorkingPlace.textColor = new Color32(0, 102, 51, 100);
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconEducationPressed";
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Monument:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "FeatureMonumentLevel6";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Monuments");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.FireDepartment:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconFireSafety";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "FireDepartment");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.PublicTransport:
                                                        {
                                                            ItemClass.SubService subService5 = this.WorkInfo.m_class.m_subService;
                                                            ItemClass.SubService subService6 = subService5;
                                                            if (subService6 != ItemClass.SubService.PublicTransportPost)
                                                            {
                                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFreePublicTransport";
                                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                                            }
                                                            else
                                                            {
                                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarPublicTransportPost";
                                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                                            }
                                                            goto IL_19C1;
                                                        }
                                                    case ItemClass.Service.Disaster:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarFireDepartmentDisaster";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Museums:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaMuseums";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.VarsitySports:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                                        goto IL_19C1;
                                                    case ItemClass.Service.Fishing:
                                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarIndustryFishing";
                                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                                        goto IL_19C1;
                                                }
                                                this.FavCimsWorkingPlace.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyNone";
                                                this.FavCimsWorkingPlace.tooltip = null;
                                            IL_19C1:;
                                            }
                                        }
                                    }
                                    this.WorkDistrict = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlaceID.Index].m_position);
                                    bool flag31 = this.WorkDistrict == 0;
                                    if (flag31)
                                    {
                                        this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
                                    }
                                    else
                                    {
                                        this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(this.WorkDistrict);
                                    }
                                }
                                else
                                {
                                    this.FavCimsWorkingPlace.isEnabled = false;
                                    this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
                                    this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
                                    this.FavCimsWorkingPlaceSprite.tooltip = null;
                                }
                                this.CitizenHome = this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Index].m_homeBuilding;
                                bool flag32 = this.CitizenHome > 0;
                                if (flag32)
                                {
                                    this.CitizenHomeID.Building = this.CitizenHome;
                                    this.BubbleHomeName.text = this.MyBuilding.GetBuildingName(this.CitizenHome, this.MyInstanceID);
                                    this.BubbleHomeName.isEnabled = true;
                                    this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                    this.HomeInfo = this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenHomeID.Index].Info;
                                    bool flag33 = this.HomeInfo.m_class.m_service == ItemClass.Service.Residential;
                                    if (flag33)
                                    {
                                        this.BubbleHomeName.tooltip = null;
                                        bool flag34 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh;
                                        if (flag34)
                                        {
                                            this.BubbleHomeName.textColor = new Color32(0, 102, 51, 100);
                                            this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
                                            this.BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString());
                                        }
                                        else
                                        {
                                            bool flag35 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHighEco;
                                            if (flag35)
                                            {
                                                this.BubbleHomeName.textColor = new Color32(0, 102, 51, 100);
                                                this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
                                                this.BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 2.ToString()) + " Eco";
                                            }
                                            else
                                            {
                                                bool flag36 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLowEco;
                                                if (flag36)
                                                {
                                                    this.BubbleHomeName.textColor = new Color32(0, 153, 0, 80);
                                                    this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                                    this.BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString()) + " Eco";
                                                }
                                                else
                                                {
                                                    bool flag37 = this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow;
                                                    if (flag37)
                                                    {
                                                        this.BubbleHomeName.textColor = new Color32(0, 153, 0, 80);
                                                        this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
                                                        this.BubbleHomeName.tooltip = Locale.Get("ZONEDBUILDING_TITLE", 1.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        switch (this.HomeInfo.m_class.m_level)
                                        {
                                            case ItemClass.Level.Level2:
                                                this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[2];
                                                break;
                                            case ItemClass.Level.Level3:
                                                this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[3];
                                                break;
                                            case ItemClass.Level.Level4:
                                                this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[4];
                                                break;
                                            case ItemClass.Level.Level5:
                                                this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[5];
                                                break;
                                            default:
                                                this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel[1];
                                                break;
                                        }
                                        this.HomeDistrict = (int)this.MyDistrict.GetDistrict(this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenHomeID.Index].m_position);
                                        bool flag38 = this.HomeDistrict == 0;
                                        if (flag38)
                                        {
                                            this.BubbleHomeIcon.tooltip = FavCimsLang.text("DistrictLabel") + FavCimsLang.text("DistrictNameNoDistrict");
                                        }
                                        else
                                        {
                                            this.BubbleHomeIcon.tooltip = FavCimsLang.text("DistrictLabel") + this.MyDistrict.GetDistrictName(this.HomeDistrict);
                                        }
                                        Notification.ProblemStruct problems = this.MyBuilding.m_buildings.m_buffer[(int)this.CitizenHome].m_problems;
                                        bool flag39 = problems != Notification.ProblemStruct.None;
                                        if (flag39)
                                        {
                                            this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSpriteProblems;
                                        }
                                        else
                                        {
                                            this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
                                        }
                                        bool flag40 = problems.ToString().Contains(2L.ToString());
                                        if (flag40)
                                        {
                                            this.BubbleDetailsElettricity.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_TITLE", "Electricity");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsElettricity.normalFgSprite = null;
                                            this.BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag41 = problems.ToString().Contains(16384L.ToString());
                                        if (flag41)
                                        {
                                            this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Sewage");
                                        }
                                        else
                                        {
                                            bool flag42 = problems.ToString().Contains(16L.ToString());
                                            if (flag42)
                                            {
                                                this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NORMAL", "DirtyWater");
                                            }
                                            else
                                            {
                                                bool flag43 = problems.ToString().Contains(4L.ToString());
                                                if (flag43)
                                                {
                                                    this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                    this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Water");
                                                }
                                                else
                                                {
                                                    bool flag44 = problems.ToString().Contains(268435456L.ToString());
                                                    if (flag44)
                                                    {
                                                        this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
                                                        this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE", "Flood");
                                                    }
                                                    else
                                                    {
                                                        this.BubbleDetailsWater.normalFgSprite = null;
                                                        this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NONE");
                                                    }
                                                }
                                            }
                                        }
                                        bool flag45 = problems.ToString().Contains(32768L.ToString());
                                        if (flag45)
                                        {
                                            this.BubbleDetailsDeath.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_TITLE", "Death");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsDeath.normalFgSprite = null;
                                            this.BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag46 = problems.ToString().Contains(8L.ToString());
                                        if (flag46)
                                        {
                                            this.BubbleDetailsFire.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_TITLE", "Fire");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsFire.normalFgSprite = null;
                                            this.BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag47 = problems.ToString().Contains(1L.ToString());
                                        if (flag47)
                                        {
                                            this.BubbleDetailsGarbage.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_TITLE", "Garbage");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsGarbage.normalFgSprite = null;
                                            this.BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag48 = problems.ToString().Contains(512L.ToString());
                                        if (flag48)
                                        {
                                            this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "LandValueLow");
                                        }
                                        else
                                        {
                                            bool flag49 = problems.ToString().Contains(256L.ToString());
                                            if (flag49)
                                            {
                                                this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                                this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "ToofewServices");
                                            }
                                            else
                                            {
                                                bool flag50 = problems.ToString().Contains(67108864L.ToString());
                                                if (flag50)
                                                {
                                                    this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
                                                    this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE", "TaxesTooHigh");
                                                }
                                                else
                                                {
                                                    this.BubbleDetailsLandValue.normalFgSprite = null;
                                                    this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_NONE");
                                                }
                                            }
                                        }
                                        bool flag51 = problems.ToString().Contains(16777216L.ToString());
                                        if (flag51)
                                        {
                                            this.BubbleDetailsNoise.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NORMAL", "Noise");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsNoise.normalFgSprite = null;
                                            this.BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag52 = problems.ToString().Contains(64L.ToString());
                                        if (flag52)
                                        {
                                            this.BubbleDetailsPollution.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NORMAL", "Pollution");
                                        }
                                        else
                                        {
                                            this.BubbleDetailsPollution.normalFgSprite = null;
                                            this.BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NONE");
                                        }
                                        bool flag53 = this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)this.citizen)))].Arrested && this.MyCitizen.m_citizens.m_buffer[(int)((IntPtr)((long)((ulong)this.citizen)))].Criminal;
                                        if (flag53)
                                        {
                                            this.BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
                                            this.BubbleDetailsCrime.tooltip = FavCimsLang.text("Citizen_Arrested");
                                        }
                                        else
                                        {
                                            bool flag54 = problems.ToString().Contains(32L.ToString());
                                            if (flag54)
                                            {
                                                this.BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
                                                this.BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_TITLE", "Crime");
                                            }
                                            else
                                            {
                                                this.BubbleDetailsCrime.normalFgSprite = null;
                                                this.BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_NONE");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.BubbleHomeName.text = FavCimsLang.text("Citizen_HomeLess");
                                    this.BubbleHomeName.isEnabled = false;
                                    this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
                                    this.BubbleHomeIcon.tooltip = FavCimsLang.text("DistrictNameNoDistrict");
                                    this.BubbleHomeName.tooltip = FavCimsLang.text("Citizen_HomeLess_tooltip");
                                }
                                this.Activity(this.citizen, this.FavCimsLastActivityVehicleButton, this.FavCimsLastActivity, out this.MyVehicleID, out this.MyTargetID);
                                this.MainCitizenInstance = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_instance;
                                this.CitizenDistrict = (int)this.MyDistrict.GetDistrict(this.MyCitizen.m_instances.m_buffer[(int)this.MainCitizenInstance].GetSmoothPosition(this.MainCitizenInstance));
                                bool flag55 = this.CitizenDistrict == 0;
                                if (flag55)
                                {
                                    this.FavCimsDistrictValue.tooltip = FavCimsLang.text("District_Label_tooltip");
                                    this.FavCimsDistrictValue.text = FavCimsLang.text("DistrictNameNoDistrict");
                                }
                                else
                                {
                                    this.FavCimsDistrictValue.tooltip = FavCimsLang.text("District_Label_tooltip");
                                    this.FavCimsDistrictValue.text = this.MyDistrict.GetDistrictName(this.CitizenDistrict);
                                }
                                this.FamilyVehicle(this.citizen, this.BubblePersonalCarButton, out this.PersonalVehicleID);
                                this.FamilyVehicle(this.Family.m_citizen0, this.BubbleFamilyBarCarButton, out this.FamilyVehicleID);
                                bool flag56 = false;
                                int num2 = 0;
                                bool flag57 = this.Family.m_citizen0 != 0U && this.citizen == this.Family.m_citizen1;
                                if (flag57)
                                {
                                    this.CitizenPartner = this.Family.m_citizen0;
                                    this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
                                    bool flag58 = this.DogOwner > 0U;
                                    if (flag58)
                                    {
                                        this.FamilyPet(this.DogOwner);
                                    }
                                    else
                                    {
                                        this.FamilyPet(this.Family.m_citizen1);
                                    }
                                }
                                else
                                {
                                    bool flag59 = this.Family.m_citizen1 != 0U && this.citizen == this.Family.m_citizen0;
                                    if (flag59)
                                    {
                                        this.CitizenPartner = this.Family.m_citizen1;
                                        this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
                                        bool flag60 = this.DogOwner > 0U;
                                        if (flag60)
                                        {
                                            this.FamilyPet(this.DogOwner);
                                        }
                                        else
                                        {
                                            this.FamilyPet(this.Family.m_citizen0);
                                        }
                                    }
                                    else
                                    {
                                        bool flag61 = this.citizen == this.Family.m_citizen0;
                                        if (flag61)
                                        {
                                            bool flag62 = this.DogOwner > 0U;
                                            if (flag62)
                                            {
                                                this.FamilyPet(this.DogOwner);
                                            }
                                            else
                                            {
                                                this.FamilyPet(this.citizen);
                                            }
                                            this.CitizenPartner = 0U;
                                        }
                                        else
                                        {
                                            this.BubblePartnerLove.normalBgSprite = "InfoIconAge";
                                            this.CitizenPartner = this.Family.m_citizen0;
                                            flag56 = true;
                                        }
                                    }
                                }
                                bool flag63 = this.CitizenPartner > 0U;
                                if (flag63)
                                {
                                    this.PartnerID.Citizen = this.CitizenPartner;
                                    int num3 = (int)(uint)((UIntPtr)this.CitizenPartner);
                                    this.BubblePartnerName.text = this.MyCitizen.GetCitizenName(this.CitizenPartner);
                                    bool flag64 = Citizen.GetGender(this.CitizenPartner) == Citizen.Gender.Female;
                                    if (flag64)
                                    {
                                        this.BubblePartnerName.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                    }
                                    else
                                    {
                                        this.BubblePartnerName.textColor = new Color32(204, 204, 51, 40);
                                    }
                                    bool isEmpty = this.PartnerID.IsEmpty;
                                    if (isEmpty)
                                    {
                                        this.BubblePartnerName.tooltip = null;
                                        this.BubblePartnerName.isEnabled = false;
                                    }
                                    else
                                    {
                                        this.BubblePartnerName.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
                                        this.BubblePartnerName.isEnabled = true;
                                    }
                                    bool flag65 = this.DogOwner > 0U;
                                    if (flag65)
                                    {
                                        this.FamilyPet(this.DogOwner);
                                    }
                                    else
                                    {
                                        this.FamilyPet(this.CitizenPartner);
                                    }
                                    this.Activity(this.CitizenPartner, this.BubblePartnerVehicleButton, this.BubblePartnerDestination, out this.PartnerVehID, out this.PartnerTarget);
                                    this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.CitizenPartner].m_age);
                                    bool flag66 = this.RealAge <= 12;
                                    if (flag66)
                                    {
                                        this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                        this.BubbleParnerAgeButton.textColor = new Color32(83, 166, 0, 60);
                                    }
                                    else
                                    {
                                        bool flag67 = this.RealAge <= 19;
                                        if (flag67)
                                        {
                                            this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                            this.BubbleParnerAgeButton.textColor = new Color32(0, 102, 51, 100);
                                        }
                                        else
                                        {
                                            bool flag68 = this.RealAge <= 25;
                                            if (flag68)
                                            {
                                                this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                                this.BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            }
                                            else
                                            {
                                                bool flag69 = this.RealAge <= 65;
                                                if (flag69)
                                                {
                                                    this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                                    this.BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                }
                                                else
                                                {
                                                    bool flag70 = this.RealAge <= 90;
                                                    if (flag70)
                                                    {
                                                        this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                                        this.BubbleParnerAgeButton.textColor = new Color32(153, 0, 0, 0);
                                                    }
                                                    else
                                                    {
                                                        this.BubbleParnerAgeButton.text = this.RealAge.ToString();
                                                        this.BubbleParnerAgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    bool flag71 = FavCimsCore.RowID.ContainsKey(num3);
                                    if (flag71)
                                    {
                                        this.BubblePartnerFollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        this.BubblePartnerFollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    this.PartnerPanel.Show();
                                    bool flag72 = !flag56;
                                    if (flag72)
                                    {
                                        this.NoPartnerPanel.Hide();
                                    }
                                }
                                else
                                {
                                    this.PartnerPanel.Hide();
                                    bool flag73 = !flag56;
                                    if (flag73)
                                    {
                                        this.NoPartnerPanel.Show();
                                    }
                                }
                                bool flag74 = flag56;
                                if (flag74)
                                {
                                    this.NoPartnerPanel.Hide();
                                    bool flag75 = this.Family.m_citizen1 > 0U;
                                    if (flag75)
                                    {
                                        this.CitizenPartner = this.Family.m_citizen1;
                                        this.Parent1ID.Citizen = this.CitizenPartner;
                                        int num4 = (int)(uint)((UIntPtr)this.CitizenPartner);
                                        this.BubbleParent1Name.text = this.MyCitizen.GetCitizenName(this.CitizenPartner);
                                        bool flag76 = Citizen.GetGender(this.CitizenPartner) == Citizen.Gender.Female;
                                        if (flag76)
                                        {
                                            this.BubbleParent1Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        }
                                        else
                                        {
                                            this.BubbleParent1Name.textColor = new Color32(204, 204, 51, 40);
                                        }
                                        bool isEmpty2 = this.Parent1ID.IsEmpty;
                                        if (isEmpty2)
                                        {
                                            this.BubbleParent1Name.isEnabled = false;
                                            this.BubbleParent1Name.tooltip = null;
                                        }
                                        else
                                        {
                                            this.BubbleParent1Name.isEnabled = true;
                                            this.BubbleParent1Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
                                        }
                                        this.Activity(this.CitizenPartner, this.BubbleParent1VehicleButton, this.BubbleParent1Destination, out this.Parent1VehID, out this.Parent1Target);
                                        this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.CitizenPartner].m_age);
                                        bool flag77 = this.RealAge <= 12;
                                        if (flag77)
                                        {
                                            this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                            this.BubbleParent1AgeButton.textColor = new Color32(83, 166, 0, 60);
                                        }
                                        else
                                        {
                                            bool flag78 = this.RealAge <= 19;
                                            if (flag78)
                                            {
                                                this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                                this.BubbleParent1AgeButton.textColor = new Color32(0, 102, 51, 100);
                                            }
                                            else
                                            {
                                                bool flag79 = this.RealAge <= 25;
                                                if (flag79)
                                                {
                                                    this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                                    this.BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                }
                                                else
                                                {
                                                    bool flag80 = this.RealAge <= 65;
                                                    if (flag80)
                                                    {
                                                        this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                    }
                                                    else
                                                    {
                                                        bool flag81 = this.RealAge <= 90;
                                                        if (flag81)
                                                        {
                                                            this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                                            this.BubbleParent1AgeButton.textColor = new Color32(153, 0, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            this.BubbleParent1AgeButton.text = this.RealAge.ToString();
                                                            this.BubbleParent1AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        bool flag82 = FavCimsCore.RowID.ContainsKey(num4);
                                        if (flag82)
                                        {
                                            this.BubbleParent1FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                        }
                                        else
                                        {
                                            this.BubbleParent1FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                        }
                                        this.Parent1Panel.Show();
                                    }
                                    else
                                    {
                                        this.Parent1Panel.Hide();
                                    }
                                }
                                else
                                {
                                    this.Parent1Panel.Hide();
                                }
                                bool flag83 = this.Family.m_citizen2 != 0U && this.Family.m_citizen2 != this.citizen;
                                if (flag83)
                                {
                                    this.CitizenParent2 = this.Family.m_citizen2;
                                    this.Parent2ID.Citizen = this.CitizenParent2;
                                    int num5 = (int)(uint)((UIntPtr)this.CitizenParent2);
                                    this.BubbleFamilyMember2Name.text = this.MyCitizen.GetCitizenName(this.CitizenParent2);
                                    bool flag84 = Citizen.GetGender(this.CitizenParent2) == Citizen.Gender.Female;
                                    if (flag84)
                                    {
                                        this.BubbleFamilyMember2Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember2Name.textColor = new Color32(204, 204, 51, 40);
                                        this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty3 = this.Parent2ID.IsEmpty;
                                    if (isEmpty3)
                                    {
                                        this.BubbleFamilyMember2Name.isEnabled = false;
                                        this.BubbleFamilyMember2Name.tooltip = null;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember2Name.isEnabled = true;
                                        this.BubbleFamilyMember2Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
                                    }
                                    bool flag85 = this.DogOwner > 0U;
                                    if (flag85)
                                    {
                                        this.FamilyPet(this.DogOwner);
                                    }
                                    else
                                    {
                                        this.FamilyPet(this.Family.m_citizen2);
                                    }
                                    this.Activity(this.CitizenParent2, this.BubbleFamilyMember2ActivityVehicleButton, this.BubbleFamilyMember2ActivityDestination, out this.Parent2VehID, out this.Parent2Target);
                                    this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.CitizenParent2].m_age);
                                    bool flag86 = this.RealAge <= 12;
                                    if (flag86)
                                    {
                                        this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                        this.BubbleFamilyMember2AgeButton.textColor = new Color32(83, 166, 0, 60);
                                    }
                                    else
                                    {
                                        bool flag87 = this.RealAge <= 19;
                                        if (flag87)
                                        {
                                            this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                            this.BubbleFamilyMember2AgeButton.textColor = new Color32(0, 102, 51, 100);
                                        }
                                        else
                                        {
                                            bool flag88 = this.RealAge <= 25;
                                            if (flag88)
                                            {
                                                this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                                this.BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            }
                                            else
                                            {
                                                bool flag89 = this.RealAge <= 65;
                                                if (flag89)
                                                {
                                                    this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                                    this.BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                }
                                                else
                                                {
                                                    bool flag90 = this.RealAge <= 90;
                                                    if (flag90)
                                                    {
                                                        this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember2AgeButton.textColor = new Color32(153, 0, 0, 0);
                                                    }
                                                    else
                                                    {
                                                        this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember2AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    bool flag91 = FavCimsCore.RowID.ContainsKey(num5);
                                    if (flag91)
                                    {
                                        this.BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    this.FamilyMember2Panel.Show();
                                    bool flag92 = !flag56;
                                    if (flag92)
                                    {
                                        num2++;
                                    }
                                }
                                else
                                {
                                    bool flag93 = this.Family.m_citizen2 == this.citizen;
                                    if (flag93)
                                    {
                                        bool flag94 = this.DogOwner > 0U;
                                        if (flag94)
                                        {
                                            this.FamilyPet(this.DogOwner);
                                        }
                                        else
                                        {
                                            this.FamilyPet(this.Family.m_citizen2);
                                        }
                                    }
                                    this.FamilyMember2Panel.Hide();
                                }
                                bool flag95 = this.Family.m_citizen3 != 0U && this.Family.m_citizen3 != this.citizen;
                                if (flag95)
                                {
                                    this.CitizenParent3 = this.Family.m_citizen3;
                                    this.Parent3ID.Citizen = this.CitizenParent3;
                                    int num6 = (int)(uint)((UIntPtr)this.CitizenParent3);
                                    this.BubbleFamilyMember3Name.text = this.MyCitizen.GetCitizenName(this.CitizenParent3);
                                    bool flag96 = Citizen.GetGender(this.CitizenParent3) == Citizen.Gender.Female;
                                    if (flag96)
                                    {
                                        this.BubbleFamilyMember3Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember3Name.textColor = new Color32(204, 204, 51, 40);
                                        this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty4 = this.Parent3ID.IsEmpty;
                                    if (isEmpty4)
                                    {
                                        this.BubbleFamilyMember3Name.isEnabled = false;
                                        this.BubbleFamilyMember3Name.tooltip = null;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember3Name.isEnabled = true;
                                        this.BubbleFamilyMember3Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
                                    }
                                    bool flag97 = this.DogOwner > 0U;
                                    if (flag97)
                                    {
                                        this.FamilyPet(this.DogOwner);
                                    }
                                    else
                                    {
                                        this.FamilyPet(this.Family.m_citizen3);
                                    }
                                    this.Activity(this.CitizenParent3, this.BubbleFamilyMember3ActivityVehicleButton, this.BubbleFamilyMember3ActivityDestination, out this.Parent3VehID, out this.Parent3Target);
                                    this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.CitizenParent3].m_age);
                                    bool flag98 = this.RealAge <= 12;
                                    if (flag98)
                                    {
                                        this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                        this.BubbleFamilyMember3AgeButton.textColor = new Color32(83, 166, 0, 60);
                                    }
                                    else
                                    {
                                        bool flag99 = this.RealAge <= 19;
                                        if (flag99)
                                        {
                                            this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                            this.BubbleFamilyMember3AgeButton.textColor = new Color32(0, 102, 51, 100);
                                        }
                                        else
                                        {
                                            bool flag100 = this.RealAge <= 25;
                                            if (flag100)
                                            {
                                                this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                                this.BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            }
                                            else
                                            {
                                                bool flag101 = this.RealAge <= 65;
                                                if (flag101)
                                                {
                                                    this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                                    this.BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                }
                                                else
                                                {
                                                    bool flag102 = this.RealAge <= 90;
                                                    if (flag102)
                                                    {
                                                        this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember3AgeButton.textColor = new Color32(153, 0, 0, 0);
                                                    }
                                                    else
                                                    {
                                                        this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember3AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    bool flag103 = FavCimsCore.RowID.ContainsKey(num6);
                                    if (flag103)
                                    {
                                        this.BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    this.FamilyMember3Panel.Show();
                                    bool flag104 = !flag56;
                                    if (flag104)
                                    {
                                        num2++;
                                    }
                                }
                                else
                                {
                                    bool flag105 = this.Family.m_citizen3 == this.citizen;
                                    if (flag105)
                                    {
                                        bool flag106 = this.DogOwner > 0U;
                                        if (flag106)
                                        {
                                            this.FamilyPet(this.DogOwner);
                                        }
                                        else
                                        {
                                            this.FamilyPet(this.Family.m_citizen3);
                                        }
                                    }
                                    this.FamilyMember3Panel.Hide();
                                }
                                bool flag107 = this.Family.m_citizen4 != 0U && this.Family.m_citizen4 != this.citizen;
                                if (flag107)
                                {
                                    this.CitizenParent4 = this.Family.m_citizen4;
                                    this.Parent4ID.Citizen = this.CitizenParent4;
                                    int num7 = (int)(uint)((UIntPtr)this.CitizenParent4);
                                    this.BubbleFamilyMember4Name.text = this.MyCitizen.GetCitizenName(this.CitizenParent4);
                                    bool flag108 = Citizen.GetGender(this.CitizenParent4) == Citizen.Gender.Female;
                                    if (flag108)
                                    {
                                        this.BubbleFamilyMember4Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
                                        this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember4Name.textColor = new Color32(204, 204, 51, 40);
                                        this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
                                    }
                                    bool isEmpty5 = this.Parent4ID.IsEmpty;
                                    if (isEmpty5)
                                    {
                                        this.BubbleFamilyMember4Name.isEnabled = false;
                                        this.BubbleFamilyMember4Name.tooltip = null;
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember4Name.isEnabled = true;
                                        this.BubbleFamilyMember4Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
                                    }
                                    bool flag109 = this.DogOwner > 0U;
                                    if (flag109)
                                    {
                                        this.FamilyPet(this.DogOwner);
                                    }
                                    else
                                    {
                                        this.FamilyPet(this.Family.m_citizen4);
                                    }
                                    this.Activity(this.CitizenParent4, this.BubbleFamilyMember4ActivityVehicleButton, this.BubbleFamilyMember4ActivityDestination, out this.Parent4VehID, out this.Parent4Target);
                                    this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.CitizenParent4].m_age);
                                    bool flag110 = this.RealAge <= 12;
                                    if (flag110)
                                    {
                                        this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                        this.BubbleFamilyMember4AgeButton.textColor = new Color32(83, 166, 0, 60);
                                    }
                                    else
                                    {
                                        bool flag111 = this.RealAge <= 19;
                                        if (flag111)
                                        {
                                            this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                            this.BubbleFamilyMember4AgeButton.textColor = new Color32(0, 102, 51, 100);
                                        }
                                        else
                                        {
                                            bool flag112 = this.RealAge <= 25;
                                            if (flag112)
                                            {
                                                this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                                this.BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                            }
                                            else
                                            {
                                                bool flag113 = this.RealAge <= 65;
                                                if (flag113)
                                                {
                                                    this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                                    this.BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                }
                                                else
                                                {
                                                    bool flag114 = this.RealAge <= 90;
                                                    if (flag114)
                                                    {
                                                        this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember4AgeButton.textColor = new Color32(153, 0, 0, 0);
                                                    }
                                                    else
                                                    {
                                                        this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString();
                                                        this.BubbleFamilyMember4AgeButton.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    bool flag115 = FavCimsCore.RowID.ContainsKey(num7);
                                    if (flag115)
                                    {
                                        this.BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_subscribed";
                                    }
                                    else
                                    {
                                        this.BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
                                    }
                                    this.FamilyMember4Panel.Show();
                                    bool flag116 = !flag56;
                                    if (flag116)
                                    {
                                        num2++;
                                    }
                                }
                                else
                                {
                                    bool flag117 = this.Family.m_citizen4 == this.citizen;
                                    if (flag117)
                                    {
                                        bool flag118 = this.DogOwner > 0U;
                                        if (flag118)
                                        {
                                            this.FamilyPet(this.DogOwner);
                                        }
                                        else
                                        {
                                            this.FamilyPet(this.Family.m_citizen4);
                                        }
                                    }
                                    this.FamilyMember4Panel.Hide();
                                }
                                bool flag119 = num2 == 0 && !flag56;
                                if (flag119)
                                {
                                    this.NoChildsPanel.Show();
                                }
                                else
                                {
                                    this.NoChildsPanel.Hide();
                                }
                                bool firstRun = this.FirstRun;
                                if (firstRun)
                                {
                                    this.FirstRun = false;
                                }
                            }
                            else
                            {
                                base.Hide();
                                this.MyInstanceID = InstanceID.Empty;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public FamilyPanelTemplate()
        {
        }

        static FamilyPanelTemplate()
        {
        }

    }
}
