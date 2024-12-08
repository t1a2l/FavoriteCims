using AlgernonCommons.XML;
using ColossalFramework.IO;
using System.IO;
using System.Xml.Serialization;

namespace FavoriteCims
{
    public sealed class FavCimsSettings : SettingsXMLBase
    {
        [XmlIgnore]
        internal static readonly string SettingsFileName = Path.Combine(DataLocation.localApplicationData, "FavoriteCims.xml");

        internal static void Load() => XMLFileUtils.Load<FavCimsSettings>(SettingsFileName);

        internal static void Save() => XMLFileUtils.Save<FavCimsSettings>(SettingsFileName);
    }
}