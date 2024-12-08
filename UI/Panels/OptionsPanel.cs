using AlgernonCommons.Translation;
using AlgernonCommons.UI;

namespace FavoriteCims.UI.Panels
{
    public sealed class OptionsPanel : OptionsPanelBase
    {
        protected override void Setup()
        {
            var language_DropDown = UIDropDowns.AddPlainDropDown(this, 5f, 5f, Translations.Translate("LANGUAGE_CHOICE"), Translations.LanguageList, Translations.Index);
            language_DropDown.eventSelectedIndexChanged += (control, index) =>
            {
                Translations.Index = index;
                OptionsPanelManager<OptionsPanel>.LocaleChanged();
            };
        }
    }
}
