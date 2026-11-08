using System;
using FavoriteCims.UI.Panels;
using ICities;
using UnityEngine;

namespace FavoriteCims.Serializer
{
    public class FavoriteCimsSerializer : ISerializableDataExtension
    {
        private const uint TupleStart = 0xFEFEFEFE;
        private const uint TupleEnd = 0xFAFAFAFA;

        public const ushort DataVersion = 1;
        public const string DataID = "FavoriteCims";

        public static ushort SaveGameFileVersion;
        public static FavoriteCimsSerializer instance;

        private ISerializableData m_serializableData;

        public void OnCreated(ISerializableData serializedData)
        {
            instance = this;
            m_serializableData = serializedData;
        }

        public void OnLoadData()
        {
            FavoriteCimsMainPanel.ResetMainColumnVisibility();
            SaveGameFileVersion = 0;

            try
            {
                if (m_serializableData == null)
                {
                    Debug.LogWarning("FavoriteCims: serializable data is unavailable.");
                    return;
                }

                byte[] data = m_serializableData.LoadData(DataID);

                if (data == null || data.Length == 0)
                {
                    Debug.Log("FavoriteCims: no saved column settings; using defaults.");
                    return;
                }

                int index = 0;

                RequireBytes(data, index, 2);

                SaveGameFileVersion = StorageData.ReadUInt16(data, ref index);

                if (SaveGameFileVersion != DataVersion)
                {
                    Debug.LogWarning("FavoriteCims: unsupported save data version " + SaveGameFileVersion + "; expected " + DataVersion + ". Using default column settings.");
                    return;
                }

                CheckTuple(data, ref index, TupleStart, "start");
                bool[] visibility = FavoriteCimsMainPanelColumnsSerializer.LoadData(SaveGameFileVersion, data, ref index);
                CheckTuple(data, ref index, TupleEnd, "end");

                if (index != data.Length)
                {
                    throw new InvalidOperationException("Unexpected trailing FavoriteCims save data.");
                }

                // Commit only after the entire record is validated.
                FavoriteCimsMainPanel.RestoreMainColumnVisibility(visibility);

                Debug.Log("FavoriteCims: loaded per-save column settings.");
            }
            catch (Exception ex)
            {
                FavoriteCimsMainPanel.ResetMainColumnVisibility();

                Debug.LogError("FavoriteCims: loading column settings failed. Using defaults.\n" + ex);
            }
        }

        public void OnSaveData()
        {
            try
            {
                if (m_serializableData == null)
                {
                    Debug.LogWarning("FavoriteCims: cannot save column settings; serializable data is unavailable.");
                    return;
                }

                var data = new FastList<byte>();

                StorageData.WriteUInt16(DataVersion, data);
                StorageData.WriteUInt32(TupleStart, data);

                FavoriteCimsMainPanelColumnsSerializer.SaveData(data);

                StorageData.WriteUInt32(TupleEnd, data);

                m_serializableData.SaveData(DataID, data.ToArray());

                Debug.Log("FavoriteCims: saved per-save column settings.");
            }
            catch (Exception ex)
            {
                Debug.LogError("FavoriteCims: saving column settings failed.\n" + ex);
            }
        }

        private static void CheckTuple(byte[] data, ref int index, uint expected, string boundary)
        {
            RequireBytes(data, index, 4);

            uint actual = StorageData.ReadUInt32(data, ref index);

            if (actual != expected)
            {
                throw new InvalidOperationException("FavoriteCims " + boundary + " tuple marker was not found.");
            }
        }

        private static void RequireBytes(byte[] data, int index, int count)
        {
            if (data == null || index < 0 || count < 0 || index > data.Length || count > data.Length - index)
            {
                throw new InvalidOperationException("FavoriteCims save data is incomplete.");
            }
        }

        public void OnReleased()
        {
            m_serializableData = null;

            if (instance == this)
            {
                instance = null;
            }

            SaveGameFileVersion = 0;
            FavoriteCimsMainPanel.ResetMainColumnVisibility();
        }
    }
}