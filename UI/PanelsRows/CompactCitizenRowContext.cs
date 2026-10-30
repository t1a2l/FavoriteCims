using System.Collections.Generic;

namespace FavoriteCims.UI.PanelsRows
{
    public sealed class CompactCitizenRowContext
    {
        public readonly Dictionary<uint, uint> Citizens = [];
        public bool IsUpdating;
    }
}