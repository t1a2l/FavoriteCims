using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class FamilyActivityDataService
    {
        public static FamilyActivityData Read(FamilyCitizenData citizen)
        {
            var result = new FamilyActivityData();
            if (citizen == null) return result;
            result.Status = citizen.Status ?? string.Empty;
            result.TargetId = citizen.TargetInstanceId;
            result.TargetName = citizen.TargetName ?? string.Empty;
            result.TargetDistrictName = citizen.TargetDistrictName ?? string.Empty;

            var manager = Singleton<CitizenManager>.instance;
            var citizens = manager.m_citizens.m_buffer;
            if (citizen.CitizenId == 0 || citizen.CitizenId >= (uint)citizens.Length) return result;
            var raw = citizens[(int)citizen.CitizenId];
            if ((raw.m_flags & Citizen.Flags.Created) == 0) return result;

            ushort instanceId = raw.m_instance;
            var instances = manager.m_instances.m_buffer;
            if (instanceId != 0 && instanceId < instances.Length)
            {
                var instance = instances[instanceId];
                if (instance.m_citizen == citizen.CitizenId)
                {
                    result.HasCurrentPosition = true;
                    var districts = Singleton<DistrictManager>.instance;
                    result.CurrentDistrictId = districts.GetDistrict(instance.GetSmoothPosition(instanceId));
                    if (result.CurrentDistrictId != 0) result.CurrentDistrictName = districts.GetDistrictName(result.CurrentDistrictId) ?? string.Empty;
                }
            }

            var vehicles = Singleton<VehicleManager>.instance;
            ushort vehicleId = raw.m_vehicle;
            var buffer = vehicles.m_vehicles.m_buffer;
            if (vehicleId != 0 && vehicleId < buffer.Length)
            {
                var vehicle = buffer[vehicleId];
                if ((vehicle.m_flags & Vehicle.Flags.Created) != 0)
                {
                    var info = vehicle.Info;
                    if (info != null && info.m_class != null)
                    {
                        result.VehicleId = new InstanceID { Vehicle = vehicleId };
                        result.VehicleName = vehicles.GetVehicleName(vehicleId) ?? string.Empty;
                        result.VehiclePrefabName = info.name ?? string.Empty;
                        result.VehicleService = info.m_class.m_service;
                        result.VehicleSubService = info.m_class.m_subService;
                    }
                }
            }
            bool arrested = citizen.IsArrested && citizen.IsCriminal;
            result.IsPrisonTransport = arrested && citizen.Location == Citizen.Location.Moving && result.HasVehicle;
            result.IsJailed = arrested && citizen.Location != Citizen.Location.Moving;
            return result;
        }
    }
}