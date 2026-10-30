namespace FavoriteCims.Utils.Services
{
    public sealed class FamilyActivityData
    {
        public string Status = string.Empty;
        public InstanceID TargetId;
        public string TargetName = string.Empty;
        public string TargetDistrictName = string.Empty;
        public InstanceID VehicleId;
        public string VehicleName = string.Empty;
        public string VehiclePrefabName = string.Empty;
        public ItemClass.Service VehicleService;
        public ItemClass.SubService VehicleSubService;
        public bool IsPrisonTransport;
        public bool IsJailed;
        public bool HasCurrentPosition;
        public byte CurrentDistrictId;
        public string CurrentDistrictName = string.Empty;
        public bool HasVehicle => !VehicleId.IsEmpty;
    }
}