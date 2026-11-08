using System;
using FavoriteCims.UI.Panels;

namespace FavoriteCims.Serializer
{
    public static class FavoriteCimsMainPanelColumnsSerializer
    {
        private const ushort ColumnsDataVersion = 1;

        // Version 1 storage order:
        // Status, Life stage, Age, Education, Home, Occupation, Activity.
        // Do not reorder these entries without a format migration.
        private static readonly int[] SavedColumnIndices =
        [
            0, 2, 3, 4, 5, 6, 7
        ];

        public static void SaveData(FastList<byte> data)
        {
            StorageData.WriteUInt16(ColumnsDataVersion, data);

            foreach (int columnIndex in SavedColumnIndices)
            {
                StorageData.WriteBool(FavoriteCimsMainPanel.IsMainColumnVisible(columnIndex), data);
            }
        }

        public static bool[] LoadData(int globalVersion, byte[] data, ref int index)
        {
            RequireBytes(data, index, 2);

            ushort version = StorageData.ReadUInt16(data, ref index);

            if (version != ColumnsDataVersion)
            {
                throw new InvalidOperationException("Unsupported FavoriteCims column data version: " + version + " (global version " + globalVersion + ").");
            }

            RequireBytes(data, index, SavedColumnIndices.Length);

            bool[] visibility =
            [
                true, true, true, true, true, true, true, true, true
            ];

            foreach (int columnIndex in SavedColumnIndices)
            {
                byte value = StorageData.ReadByte(data, ref index);

                if (value > 1)
                {
                    throw new InvalidOperationException("Invalid FavoriteCims column visibility value: " + value);
                }

                visibility[columnIndex] = value == 1;
            }

            return visibility;
        }

        private static void RequireBytes(byte[] data, int index, int count)
        {
            if (data == null || index < 0 || count < 0 || index > data.Length || count > data.Length - index)
            {
                throw new InvalidOperationException("FavoriteCims column data is incomplete.");
            }
        }
    }
}