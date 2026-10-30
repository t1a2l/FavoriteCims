using System.Collections.Generic;
using ColossalFramework.Globalization;

namespace FavoriteCims.Utils
{
    public static class FamilyPanelText
    {
        private static readonly Dictionary<string, string> English = new Dictionary<string, string>
        {
            { "CitizenNavigation", "Left click: go to citizen\nRight click: go to citizen and open details\nMiddle click: open citizen details" },
            { "InstanceNavigation", "Left/right click: go to target and open details\nMiddle click: open details without moving the camera" },
            { "Resident", "Resident" },
            { "Tourist", "Tourist" },
            { "PetNotFound", "No active household pet was found. This does not confirm that the household has no pet." },
            { "PetUnavailable", "Pet information is temporarily unavailable." },
            { "HomeUnavailable", "The assigned home is currently unavailable." }
        };

        private static readonly Dictionary<string, string> Italian = new Dictionary<string, string>
        {
            { "CitizenNavigation", "Clic sinistro: vai al cittadino\nClic destro: vai al cittadino e apri i dettagli\nClic centrale: apri i dettagli del cittadino" },
            { "InstanceNavigation", "Clic sinistro/destro: vai alla destinazione e apri i dettagli\nClic centrale: apri i dettagli senza spostare la visuale" },
            { "Resident", "Residente" },
            { "Tourist", "Turista" },
            { "PetNotFound", "Nessun animale domestico attivo trovato per questa famiglia. Questo non conferma che la famiglia non ne abbia uno." },
            { "PetUnavailable", "Le informazioni sull'animale domestico non sono temporaneamente disponibili." },
            { "HomeUnavailable", "L'abitazione assegnata non è attualmente disponibile." }
        };

        private static readonly Dictionary<string, string> Dutch = new Dictionary<string, string>
        {
            { "CitizenNavigation", "Linkermuisknop: ga naar inwoner\nRechtermuisknop: ga naar inwoner en open details\nMiddelste muisknop: open inwonerdetails" },
            { "InstanceNavigation", "Linker-/rechtermuisknop: ga naar doel en open details\nMiddelste muisknop: open details zonder de camera te verplaatsen" },
            { "Resident", "Inwoner" },
            { "Tourist", "Toerist" },
            { "PetNotFound", "Geen actief huisdier voor dit huishouden gevonden. Dit betekent niet dat het huishouden geen huisdier heeft." },
            { "PetUnavailable", "Informatie over het huisdier is tijdelijk niet beschikbaar." },
            { "HomeUnavailable", "De toegewezen woning is momenteel niet beschikbaar." }
        };

        private static readonly Dictionary<string, string> Russian = new Dictionary<string, string>
        {
            { "CitizenNavigation", "Левая кнопка: перейти к жителю\nПравая кнопка: перейти к жителю и открыть сведения\nСредняя кнопка: открыть сведения о жителе" },
            { "InstanceNavigation", "Левая/правая кнопка: перейти к объекту и открыть сведения\nСредняя кнопка: открыть сведения без перемещения камеры" },
            { "Resident", "Житель" },
            { "Tourist", "Турист" },
            { "PetNotFound", "Активное домашнее животное не найдено. Это не означает, что у семьи нет питомца." },
            { "PetUnavailable", "Сведения о питомце временно недоступны." },
            { "HomeUnavailable", "Назначенное жильё сейчас недоступно." }
        };

        public static string CitizenNavigation => Get("CitizenNavigation");

        public static string InstanceNavigation => Get("InstanceNavigation");

        public static string Resident => Get("Resident");

        public static string Tourist => Get("Tourist");

        public static string PetNotFound => Get("PetNotFound");

        public static string PetUnavailable => Get("PetUnavailable");

        public static string HomeUnavailable => Get("HomeUnavailable");

        private static string Get(string key)
        {
            Dictionary<string, string> language = Locale.Get("LANGUAGE_ENGLISH") switch
            {
                "(ITALIAN)" => Italian,
                "(DUTCH)" => Dutch,
                "(RUSSIAN)" => Russian,
                _ => English,
            };
            if (language.TryGetValue(key, out string text) && !string.IsNullOrEmpty(text)) return text;
            return English.TryGetValue(key, out text) ? text : string.Empty;
        }

        public static string AppendHint(string description, string hint)
        {
            if (string.IsNullOrEmpty(description)) return hint ?? string.Empty;
            if (string.IsNullOrEmpty(hint)) return description;
            return description.TrimEnd() + "\n\n" + hint;
        }
    }
}