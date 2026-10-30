namespace FavoriteCims.Utils.Services
{
    public enum FamilyPetReadState
    {
        NoActiveMatch = 0,
        Found = 1,
        Unavailable = 2
    }

    public sealed class FamilyPetData
    {
        public FamilyPetReadState State;
        public uint OwnerCitizenId;
        public string OwnerName = string.Empty;
        public InstanceID InstanceId;
        public string Name = string.Empty;
        public string Status = string.Empty;

        public bool HasPet => State == FamilyPetReadState.Found && !InstanceId.IsEmpty;
    }
}