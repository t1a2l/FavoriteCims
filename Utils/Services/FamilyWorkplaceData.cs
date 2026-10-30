namespace FavoriteCims.Utils.Services
{
    public sealed class FamilyWorkplaceData
    {
        public bool HasAssignment;
        public bool IsAvailable;
        public bool IsStudent;
        public bool IsRetired;
        public ushort BuildingId;
        public InstanceID InstanceId;
        public string Name = string.Empty;
        public string Profession = string.Empty;
        public ItemClass.Level SchoolLevel;
        public ItemClass.Service Service;
        public ItemClass.SubService SubService;
        public ItemClass.Level Level;
        public byte DistrictId;
        public string DistrictName = string.Empty;
    }
}