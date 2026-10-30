using System;
using System.Collections.Generic;
using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class VehicleOccupantsService
    {
        public static bool TryRead(InstanceID selected, out VehicleOccupantsSnapshot snapshot)
        {
            snapshot = null;

            if (selected.IsEmpty || selected.Type != InstanceType.Vehicle || selected.Vehicle == 0) return false;

            try
            {
                var vehicles = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
                ushort vehicleId = selected.Vehicle;
                if (vehicleId >= vehicles.Length) return false;
                var vehicle = vehicles[vehicleId];
                if ((vehicle.m_flags & Vehicle.Flags.Created) == 0 || vehicle.Info == null) return false;

                var manager = Singleton<CitizenManager>.instance;
                var units = manager.m_units.m_buffer;
                var citizens = manager.m_citizens.m_buffer;
                var result = new VehicleOccupantsSnapshot { VehicleId = vehicleId };
                var visitedUnits = new HashSet<uint>();
                var seenCitizens = new HashSet<uint>();
                uint unitId = vehicle.m_citizenUnits;
                bool firstUnit = true;
                while (unitId != 0)
                {
                    if (unitId >= (uint)units.Length || !visitedUnits.Add(unitId))
                    {
                        Debug.Warning("Invalid occupant unit chain for vehicle " + vehicleId + ".");
                        return false;
                    }
                    var unit = units[(int)unitId];
                    for (int slot = 0; slot < 5; slot++)
                    {
                        uint citizenId = unit.GetCitizen(slot);
                        if (citizenId == 0 || citizenId >= (uint)citizens.Length || (citizens[(int)citizenId].m_flags & Citizen.Flags.Created) == 0 || !seenCitizens.Add(citizenId)) continue;
                        result.Occupants.Add(new VehicleOccupantData
                        {
                            CitizenId = citizenId,
                            UnitId = unitId,
                            SlotIndex = slot
                        });
                        if (firstUnit && slot == 0) result.FirstUnitSlotZeroCitizen = citizenId;
                    }
                    unitId = unit.m_nextUnit;
                    firstUnit = false;
                }
                snapshot = result;
                return true;
            }
            catch (Exception ex)
            {
                Debug.Error("Failed to collect vehicle occupants: " + ex);
                return false;
            }
        }
    }
}