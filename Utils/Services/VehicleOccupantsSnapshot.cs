using System.Collections.Generic;

namespace FavoriteCims.Utils.Services
{
    public struct VehicleOccupantData
    {
        public uint CitizenId;
        public uint UnitId;
        public int SlotIndex;
    }

    public sealed class VehicleOccupantsSnapshot
    {
        public ushort VehicleId;
        // Private-vehicle UI convention, not an authoritative AI driver lookup.
        public uint FirstUnitSlotZeroCitizen;
        public readonly List<VehicleOccupantData> Occupants = [];
    }
}