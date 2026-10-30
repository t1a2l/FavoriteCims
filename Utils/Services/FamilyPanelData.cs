using System.Collections.Generic;

namespace FavoriteCims.Utils.Services
{
    public enum FamilyPanelReadState
    {
        Unavailable = 0,
        NoHome = 1,
        HomeUnavailable = 2,
        NoHousehold = 3,
        Ready = 4
    }

    public sealed class FamilyPanelData
    {
        public FamilyPanelReadState State;

        public FamilyCitizenData SelectedCitizen;
        public FamilyHomeData Home;

        public uint HouseholdUnitId;

        // Includes the selected citizen, in original household-slot order.
        public readonly List<FamilyCitizenData> Household = [];

        public FamilyPetData Pet;

        public bool HasHousehold => State == FamilyPanelReadState.Ready;
    }

    public sealed class FamilyCitizenData
    {
        public uint CitizenId;
        public int HouseholdSlot = -1;

        public InstanceID InstanceId;

        public string Name = string.Empty;

        public Citizen.Gender Gender;
        public Citizen.Education Education;
        public Citizen.Wealth Wealth;

        public int GameAge;
        public int RealAge;
        public int Health;
        public int Wellbeing;
        public int Happiness;

        public bool IsDead;
        public bool IsArrested;
        public bool IsCriminal;
        public bool IsStudent;
        public bool IsFavorite;

        public ushort HomeBuilding;
        public ushort WorkBuilding;
        public Citizen.Location Location;
        public FamilyWorkplaceData Workplace;

        public string Status = string.Empty;

        public InstanceID TargetInstanceId;
        public string TargetName = string.Empty;
        public string TargetDistrictName = string.Empty;

        public InstanceID CurrentVehicleInstanceId;
        public string CurrentVehicleName = string.Empty;

        public InstanceID OwnedVehicleInstanceId;
        public string OwnedVehicleName = string.Empty;
        public bool OwnedVehicleIsParked;

        public FamilyActivityData Activity;

        public bool HasCurrentVehicle => !CurrentVehicleInstanceId.IsEmpty;

        public bool HasOwnedVehicle => !OwnedVehicleInstanceId.IsEmpty;
    }

    public sealed class FamilyHomeData
    {
        public ushort BuildingId;
        public InstanceID InstanceId;

        public string Name = string.Empty;

        public ItemClass.Service Service;
        public ItemClass.SubService SubService;
        public ItemClass.Level Level;

        public byte DistrictId;
        public string DistrictName = string.Empty;

        public Notification.ProblemStruct Problems;
    }
}