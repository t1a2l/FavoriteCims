using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using UnityEngine;

namespace FavoriteCims
{
    public class FavCimsCore : MonoBehaviour
    {
        private static FieldInfo instanceLockField;
        private static FieldInfo instanceNamesField;
        private static bool reflectionInitialized;

        public static InstanceID ThisHuman;

        public static readonly HashSet<uint> RowID = [];

        private static readonly string[] Hotel_Names =
        [
            "Hotel",
            "hotel",
            "Crescent",
            "Obsidian",
            "Yggdrasil",
            "K207",
            "Rental",
            "Inn",
            "Babylon"
        ];

        public static T GetPrivateVariable<T>(object obj, string fieldName)
        {
            return (T)obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
        }

        public static Dictionary<InstanceID, string> FavoriteCimsList()
        {
            if (!TryEnterInstanceLock(out object lockObject))
            {
                Utils.Debug.Warning("Could not acquire InstanceManager lock while reading favorites.");
                return null;
            }

            try
            {
                var instanceManager = Singleton<InstanceManager>.instance;

                return instanceNamesField.GetValue(instanceManager) is not Dictionary<InstanceID, string> names
                    ? []
                    : new Dictionary<InstanceID, string>(names);
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to read favorite citizen names: " + ex);
                return null;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        public static void InsertIdIntoArray(uint citID)
        {
            if (citID != 0)
            {
                RowID.Add(citID);
            }
        }

        public static void RemoveIdFromArray(uint citID)
        {
            RowID.Remove(citID);
        }

        public static void ClearIdArray()
        {
            RowID.Clear();
        }

        public static bool IsFavorite(uint citizenId)
        {
            return citizenId != 0 && RowID.Contains(citizenId);
        }

        public static void ToggleCitizenFavorite(InstanceID citizenInstance)
        {
            if (!IsCitizenInstance(citizenInstance))
            {
                return;
            }

            if (IsFavorite(citizenInstance.Citizen))
            {
                RemoveFavorite(citizenInstance);
            }
            else
            {
                AddFavorite(citizenInstance);
            }
        }

        public static bool RemoveFavorite(InstanceID citizenInstance)
        {
            if (!IsCitizenInstance(citizenInstance))
            {
                return false;
            }

            if (!TryEnterInstanceLock(out object lockObject))
            {
                Utils.Debug.Warning("Could not acquire InstanceManager lock while removing favorite.");
                return false;
            }

            try
            {
                var instanceManager = Singleton<InstanceManager>.instance;

                instanceManager.SetName(citizenInstance, null);
                RowID.Remove(citizenInstance.Citizen);

                return true;
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to remove favorite citizen: " + ex);
                return false;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        private static bool AddFavorite(InstanceID citizenInstance)
        {
            if (!IsCitizenInstance(citizenInstance))
            {
                return false;
            }

            var rowsPanel = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel;

            if (rowsPanel == null)
            {
                Utils.Debug.Warning("Cannot add favorite: the favorites rows panel is not ready.");
                return false;
            }

            string citizenName;

            if (!TryEnterInstanceLock(out object lockObject))
            {
                Utils.Debug.Warning("Could not acquire InstanceManager lock while adding favorite.");
                return false;
            }

            try
            {
                var instanceManager = Singleton<InstanceManager>.instance;

                citizenName = instanceManager.GetName(citizenInstance);

                if (string.IsNullOrEmpty(citizenName))
                {
                    citizenName = Singleton<CitizenManager>.instance.GetCitizenName(citizenInstance.Citizen);
                }

                if (string.IsNullOrEmpty(citizenName))
                {
                    return false;
                }

                instanceManager.SetName(citizenInstance, citizenName);
                RowID.Add(citizenInstance.Citizen);
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to add favorite citizen: " + ex);
                return false;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            // UI creation is deliberately outside the InstanceManager lock.
            EnsureFavoriteRow(citizenInstance, citizenName);

            return true;
        }

        private static void EnsureFavoriteRow(InstanceID citizenInstance, string citizenName)
        {
            var rowsPanel = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel;

            if (rowsPanel == null)
            {
                return;
            }

            CitizenRow row = null;

            try
            {
                if (FavoriteCimsMainPanel.RowsAlreadyExist(citizenInstance))
                {
                    return;
                }

                row = rowsPanel.AddUIComponent<CitizenRow>();

                if (row == null)
                {
                    Utils.Debug.Warning("Favorite was registered, but its UI row could not be created.");
                    return;
                }

                row.MyInstanceID = citizenInstance;
                row.MyInstancedName = citizenName;
            }
            catch (Exception ex)
            {
                if (row != null)
                {
                    UnityEngine.Object.Destroy(row.gameObject);
                }

                Utils.Debug.Error("Favorite was registered, but creating its UI row failed: " + ex);
            }
        }

        public static void GoToCitizen(Vector3 position, InstanceID target, bool tourist, UIMouseEventParameter eventParam)
        {
            if (target.IsEmpty)
            {
                return;
            }

            try
            {
                var instance = Singleton<InstanceManager>.instance;

                if (!instance.SelectInstance(target))
                {
                    return;
                }

                var tooltip = UIView.Find("DefaultTooltip");
                tooltip?.Hide();

                switch (eventParam.buttons)
                {
                    case UIMouseButton.Middle:
                        ShowCitizenInfoPanel(position, target, tourist);
                        break;

                    case UIMouseButton.Left:
                        ToolsModifierControl.cameraController.SetTarget(
                            target,
                            ToolsModifierControl.cameraController.transform.position,
                            true);
                        break;

                    default:
                        ToolsModifierControl.cameraController.SetTarget(
                            target,
                            ToolsModifierControl.cameraController.transform.position,
                            true);
                        ShowCitizenInfoPanel(position, target, tourist);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Can't find the Citizen " + ex);
            }
        }

        public static int CalculateCitizenAge(int gameAge)
        {
            if (gameAge <= 0)
            {
                return 0;
            }

            if (gameAge <= 15)
            {
                return 1 + (int)((12 - 1) / 100.0 * (gameAge / 15.0 * 100.0));
            }

            if (gameAge <= 45)
            {
                return 13 + (int)((19 - 13) / 100.0 * ((gameAge - 15.0) / 30.0 * 100.0));
            }

            if (gameAge <= 90)
            {
                return 20 + (int)((25 - 20) / 100.0 * ((gameAge - 45.0) / 45.0 * 100.0));
            }

            if (gameAge <= 180)
            {
                return 26 + (int)((65 - 26) / 100.0 * ((gameAge - 90.0) / 90.0 * 100.0));
            }

            if (gameAge <= 240)
            {
                return 66 + (int)((90 - 66) / 100.0 * ((gameAge - 180.0) / 60.0 * 100.0));
            }

            const int maxGameAge = 400;
            return 91 + (int)((114 - 91) / 100.0 * ((gameAge - 240.0) / (maxGameAge - 240) * 100.0));
        }

        public static bool IsHotel(ushort buildingId)
        {
            if (!TryReadClassificationInfo(buildingId, out BuildingInfo info))
            {
                return false;
            }

            if (info.m_class.m_service == ItemClass.Service.Hotel)
            {
                return true;
            }

            if (info.m_class.m_service == ItemClass.Service.Commercial && info.m_class.m_subService == ItemClass.SubService.CommercialTourist)
            {
                string prefabName = info.name;

                if (!string.IsNullOrEmpty(prefabName))
                {
                    foreach (string hotelName in Hotel_Names)
                    {
                        if (!string.IsNullOrEmpty(hotelName) && prefabName.Contains(hotelName))
                        {
                            return true;
                        }
                    }
                }
            }

            var ai = info.m_buildingAI;

            if (ai == null)
            {
                return false;
            }

            string aiName = ai.GetType().Name;

            return aiName.Contains("AirportHotelAI") || aiName.Contains("ParkHotelAI");
        }

        public static bool IsAreaResidentalBuilding(ushort buildingId)
        {
            if (!TryReadClassificationInfo(buildingId, out BuildingInfo info))
            {
                return false;
            }

            var ai = info.m_buildingAI;

            if (ai == null)
            {
                return false;
            }

            string aiName = ai.GetType().Name;

            bool isBarracks = ai is AuxiliaryBuildingAI && aiName.Contains("BarracksAI");

            bool isDormitory = ai is CampusBuildingAI && aiName.Contains("DormsAI");

            return isBarracks || isDormitory;
        }

        public static class CimCareFlags
        {
            public const ushort NursingHome = 0x0400; // bit 10
            public const ushort Orphanage = 0x0800; // bit 11
        }

        public static bool IsCimCareBuilding(ushort buildingId)
        {
            if (!TryReadClassificationInfo(buildingId, out BuildingInfo info))
            {
                return false;
            }

            var ai = info.m_buildingAI;

            if (ai == null)
            {
                return false;
            }

            string aiName = ai.GetType().Name;

            return aiName.Contains("NursingHomeAI")|| aiName.Contains("OrphanageAI");
        }

        public static bool IsInternationalTradeOfficeBuilding(ushort buildingId)
        {
            if (!TryReadClassificationInfo(buildingId, out BuildingInfo info))
            {
                return false;
            }

            var ai = info.m_buildingAI;

            if (ai == null)
            {
                return false;
            }

            return ai.GetType().Name.Contains("InternationalTradeOfficeBuildingAI");
        }

        private static bool TryGetReflectionFields()
        {
            if (reflectionInitialized)
            {
                return instanceLockField != null && instanceNamesField != null;
            }

            try
            {
                var instanceManagerType = typeof(InstanceManager);

                instanceLockField = instanceManagerType.GetField("m_lock", BindingFlags.Instance | BindingFlags.NonPublic);
                instanceNamesField = instanceManagerType.GetField("m_names", BindingFlags.Instance | BindingFlags.NonPublic);

                reflectionInitialized = true;
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to reflect InstanceManager fields: " + ex);
            }

            return instanceLockField != null && instanceNamesField != null;
        }

        private static bool TryEnterInstanceLock(out object lockObject)
        {
            lockObject = null;

            if (!TryGetReflectionFields())
            {
                return false;
            }

            lockObject = instanceLockField.GetValue(Singleton<InstanceManager>.instance);

            if (lockObject == null)
            {
                return false;
            }

            return Monitor.TryEnter(lockObject, SimulationManager.SYNCHRONIZE_TIMEOUT);
        }

        private static bool IsCitizenInstance(InstanceID instance)
        {
            return !instance.IsEmpty && instance.Type == InstanceType.Citizen && instance.Citizen != 0;
        }

        private static void ShowCitizenInfoPanel(Vector3 position, InstanceID target, bool tourist)
        {
            if (tourist)
            {
                WorldInfoPanel.Show<TouristWorldInfoPanel>(position, target);
            }
            else
            {
                WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, target);
            }
        }

        private static bool TryReadClassificationInfo(ushort buildingId, out BuildingInfo info)
        {
            info = null;

            if (buildingId == 0)
            {
                return false;
            }

            var buildings = Singleton<BuildingManager>.instance.m_buildings.m_buffer;

            if (buildingId >= buildings.Length)
            {
                return false;
            }

            var building = buildings[buildingId];

            if ((building.m_flags & Building.Flags.Created) == 0)
            {
                return false;
            }

            info = building.Info;

            return info != null && info.m_class != null;
        }
    }
}