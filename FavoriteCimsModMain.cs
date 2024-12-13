using AlgernonCommons.Translation;
using AlgernonCommons;
using ICities;
using FavoriteCims.UI.Panels;

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
