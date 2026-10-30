using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils.Services
{
    /// <summary>
    /// Describes one base icon.
    /// Uses either a texture or an atlas sprite, never both.
    /// The default value represents no icon.
    /// </summary>
    public readonly struct BuildingIconData
    {
        public Texture Texture { get; }

        public UITextureAtlas Atlas { get; }

        public string SpriteName { get; }

        public bool HasTexture => Texture != null;

        public bool HasSprite => Atlas != null && !string.IsNullOrEmpty(SpriteName);

        private BuildingIconData(Texture texture, UITextureAtlas atlas, string spriteName)
        {
            Texture = texture;
            Atlas = atlas;
            SpriteName = spriteName;
        }

        /// <summary>
        /// Creates a texture-based icon.
        /// A missing texture produces an empty icon.
        /// </summary>
        public static BuildingIconData FromTexture(Texture texture)
        {
            return texture == null ? default : new BuildingIconData(texture, null, null);
        }

        /// <summary>
        /// Creates an atlas-based icon.
        /// Missing atlas or sprite information produces an empty icon.
        /// </summary>
        public static BuildingIconData FromSprite(UITextureAtlas atlas, string spriteName)
        {
            return atlas == null || string.IsNullOrEmpty(spriteName) ? default : new BuildingIconData(null, atlas, spriteName);
        }
    }
}