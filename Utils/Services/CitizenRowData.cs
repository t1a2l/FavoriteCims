using ColossalFramework.UI;

namespace FavoriteCims.Utils.Services
{
    public struct CitizenRowData
    {
        public uint CitizenId;
        public string Name;
        public Citizen.Gender Gender;
        public bool IsTourist;
        public bool IsStudent;
        public bool IsDead;
        public bool IsArrested;
        public bool IsCriminal;
        public bool HasLeftCity;

        public int Health;
        public int Wellbeing;
        public int Happiness;
        public int GameAge;
        public int RealAge;

        public Citizen.Education Education;
        public Citizen.Happiness HappinessLevel;

        public BuildingCellData Home;
        public BuildingCellData Workplace;

        public string Status;

        public InstanceID TargetInstanceId;
        public string TargetName;

        public string ActivityText;
        public bool ActivityEnabled;
        public string ActivityTooltip;

        public ushort Vehicle;
        public InstanceID VehicleInstanceId;
        public UITextureAtlas VehicleAtlas;
        public string VehicleIcon;
        public string VehicleHoverIcon;
        public string VehicleDisabledIcon;
        public bool VehicleEnabled;
        public string VehicleTooltip;

        public string NameTooltip;
        public string DeathDate;
        public string DeathTime;
        public int DeathRealAge;
    }
}
