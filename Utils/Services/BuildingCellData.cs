using UnityEngine;

namespace FavoriteCims.Utils.Services
{
    /// <summary>
    /// Presentation data shared by home and workplace columns.
    /// Readers populate this; the view renders it without
    /// looking up simulation data.
    /// </summary>
    public struct BuildingCellData
    {
        public ushort BuildingId;

        // May also contain a non-building state such as
        // "Retired", "Unemployed", or "Outside the city".
        public string DisplayName;

        public string TextTooltip;
        public string TypeTooltip;

        public int DistrictId;
        public string DistrictTooltip;

        public Color32 TextColor;

        public BuildingIconData Icon;
        public Texture LevelTexture;

        /// <summary>
        /// Derives the navigation target from the building ID,
        /// avoiding two independently stored identities.
        /// </summary>
        public readonly InstanceID InstanceId
        {
            get
            {
                var instance = InstanceID.Empty;

                if (BuildingId != 0)
                {
                    instance.Building = BuildingId;
                }

                return instance;
            }
        }

        /// <summary>
        /// Indicates that the cell has a building navigation target.
        /// The reader remains responsible for validating that building.
        /// </summary>
        public readonly bool CanNavigate => BuildingId != 0;
    }
}