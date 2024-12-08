using AlgernonCommons;
using AlgernonCommons.Translation;
using FavoriteCims.UI.Panels;
using ICities;

namespace FavoriteCims
{
    public sealed class FavoriteCimsModMain : OptionsMod<OptionsPanel>, IUserMod
    {
        public override string BaseName => "Favorite Cims";
        public override string LogName => "FavoriteCimsMod";
        public string Description => Translations.Translate("MOD_DESCRIPTION");
        public override void LoadSettings() => FavCimsSettings.Load();

        public override void SaveSettings() => FavCimsSettings.Save();
    }
}
