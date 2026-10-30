using System;
using System.Collections.Generic;
using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class FamilyPetDataService
    {
        public static FamilyPetData Read(FamilyPanelData snapshot)
        {
            var result = new FamilyPetData();
            if (snapshot == null || snapshot.SelectedCitizen == null) return result;

            try
            {
                var manager = Singleton<CitizenManager>.instance;
                var instances = manager.m_instances.m_buffer;
                var owners = new Dictionary<ushort, FamilyCitizenData>();
                AddOwner(snapshot.SelectedCitizen, owners);
                foreach (var member in snapshot.Household)
                { 
                    AddOwner(member, owners); 
                }

                if (owners.Count == 0) return result;

                int limit = Math.Min(instances.Length, ushort.MaxValue + 1);

                for (int index = 1; index < limit; index++)
                {
                    var candidate = instances[index];
                    if ((candidate.m_flags & CitizenInstance.Flags.Created) == 0) continue;
                    if (candidate.m_targetBuilding == 0 || !owners.TryGetValue(candidate.m_targetBuilding, out FamilyCitizenData owner)) continue;
                    var info = candidate.Info;
                    if (info == null || info.m_citizenAI == null || !info.m_citizenAI.IsAnimal()) continue;

                    ushort petId = (ushort)index;
                    result.State = FamilyPetReadState.Found;
                    result.OwnerCitizenId = owner.CitizenId;
                    result.OwnerName = owner.Name ?? string.Empty;
                    result.InstanceId = new InstanceID { CitizenInstance = petId };
                    result.Name = manager.GetInstanceName(petId) ?? string.Empty;

                    // AI status is read from a local copy, not written back to the buffer.
                    try
                    {
                        result.Status = info.m_citizenAI.GetLocalizedStatus(petId, ref candidate, out InstanceID target) ?? string.Empty;
                    }
                    catch (Exception)
                    {
                        // Optional status must not invalidate an otherwise readable pet.
                        result.Status = string.Empty;
                    }
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.Error("Failed to read household pet data: " + ex);
                return new FamilyPetData { State = FamilyPetReadState.Unavailable };
            }
        }

        private static void AddOwner(FamilyCitizenData citizen, Dictionary<ushort, FamilyCitizenData> owners)
        {
            if (citizen == null || citizen.CitizenId == 0) return;
            var manager = Singleton<CitizenManager>.instance;
            var citizens = manager.m_citizens.m_buffer;
            var instances = manager.m_instances.m_buffer;
            if (citizen.CitizenId >= (uint)citizens.Length) return;
            var raw = citizens[(int)citizen.CitizenId];
            if ((raw.m_flags & Citizen.Flags.Created) == 0) return;
            ushort instanceId = raw.m_instance;
            if (instanceId == 0 || instanceId >= instances.Length) return;
            var instance = instances[instanceId];
            if ((instance.m_flags & CitizenInstance.Flags.Created) == 0 || instance.m_citizen != citizen.CitizenId) return;
            owners[instanceId] = citizen;
        }
    }
}