using System.Collections.Generic;

namespace FavoriteCims.Utils.Services
{
    public enum BuildingOccupantRole
    {
        Unknown = 0,
        Resident = 1,
        Employee = 2,
        Visitor = 3,
        HotelGuest = 4,
        Student = 5
    }

    public struct BuildingOccupant
    {
        public uint CitizenId;
        public uint UnitId;
        public int SlotIndex;

        public BuildingOccupantRole Role;
        public BuildingPresenceState Presence;

        public CitizenUnit.Flags UnitFlags;
        public Citizen.Location Location;

        public bool IsTourist;
        public bool IsStudent;
        public bool IsDead;
        public bool IsArrested;
        public bool IsCriminal;

        public ushort HomeBuilding;
        public ushort WorkBuilding;

        public string Name;
        public string Status;
        public string TargetName;

        public InstanceID TargetInstanceId;

        public readonly bool IsPresent => Presence == BuildingPresenceState.Present;

        public readonly bool IsHeadingHere => Presence == BuildingPresenceState.HeadingHere;

        public readonly bool IsAway => Presence == BuildingPresenceState.Away;
    }

    public sealed class BuildingOccupantGroup
    {
        public uint UnitId;

        public string Title;

        public readonly List<BuildingOccupant> Members = [];

        public bool HasMembers => Members.Count > 0;
    }

    public sealed class BuildingOccupantsSnapshot
    {
        public ushort BuildingId;

        public bool IsValid;

        public bool IsResidential;
        public bool IsHotel;
        public bool IsCimCare;
        public bool IsInternationalTradeOffice;

        public string Title;

        public readonly List<BuildingOccupantGroup> ResidentGroups = [];

        public readonly List<BuildingOccupant> Employees = [];
        public readonly List<BuildingOccupant> Visitors = [];
        public readonly List<BuildingOccupant> HotelGuests = [];
        public readonly List<BuildingOccupant> Students = [];

        public int PresentEmployeeCount;
        public int AssignedEmployeeCount;

        public int PresentVisitorCount;
        public int IncomingVisitorCount;

        public int RegisteredHotelGuestCount;
        public int PresentHotelGuestCount;

        public int TotalOccupantCount;

        public bool HasEmployees => Employees.Count > 0;
        public bool HasVisitors => Visitors.Count > 0;
        public bool HasHotelGuests => HotelGuests.Count > 0;
        public bool HasResidents => ResidentGroups.Count > 0;
        public bool HasStudents => Students.Count > 0;
    }
}