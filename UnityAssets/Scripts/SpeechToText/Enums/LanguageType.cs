using dhlworks.utilities;

namespace dhlworks.voicerecon
{
    /// <summary>
    /// Enum of languages compliant to IETF BCP 47
    /// </summary>
    public enum LanguageType
    {
        [StringValue("ar-SA")] ARABIC,
        [StringValue("bn-BD")] BANGLA,
        [StringValue("bn-IN")] BANGLA_INDIA,
        [StringValue("cs-CZ")] CZECH,
        [StringValue("da-DK")] DANISH,
        [StringValue("de-AT")] GERMAN_AUSTRIA,
        [StringValue("de-CH")] GERMAN_SWITZERLAND,
        [StringValue("de-DE")] GERMAN,
        [StringValue("el-GR")] GREEK,
        [StringValue("en-AU")] ENGLISH_AUSTRALIA,
        [StringValue("en-CA")] ENGLISH_CANADA,
        [StringValue("en-GB")] ENGLISH_UK,
        [StringValue("en-IE")] ENGLISH_IRELAND,
        [StringValue("en-IN")] ENGLISH_INDIA,
        [StringValue("en-NZ")] ENGLISH_NEW_ZEALAND,
        [StringValue("en-US")] ENGLISH_US,
        [StringValue("en-ZA")] ENGLISH_SOUTH_AFRICA,
        [StringValue("es-AR")] SPANISH_ARGENTINA,
        [StringValue("es-CL")] SPANISH_CHILE,
        [StringValue("es-CO")] SPANISH_COLOMBIA,
        [StringValue("es-ES")] SPANISH,
        [StringValue("es-MX")] SPANISH_MEXICO,
        [StringValue("es-US")] SPANISH_US,
        [StringValue("fi-FI")] FINNISH,
        [StringValue("fr-BE")] FRENCH_BELGIUM,
        [StringValue("fr-CA")] FRENCH_CANADA,
        [StringValue("fr-CH")] FRENCH_SWITZERLAND,
        [StringValue("fr-FR")] FRENCH,
        [StringValue("he-IL")] HEBREW,
        [StringValue("hi-IN")] HINDI,
        [StringValue("hu-HU")] HUNGARIAN,
        [StringValue("id-ID")] INDONESIAN,
        [StringValue("it-CH")] ITALIAN_SWITZERLAND,
        [StringValue("it-IT")] ITALIAN,
        [StringValue("jp-JP")] JAPANESE,
        [StringValue("ko-KR")] KOREAN,
        [StringValue("nl-BE")] DUTCH_BELGIUM,
        [StringValue("nl-NL")] DUTCH,
        [StringValue("no-NO")] NORWEGIAN,
        [StringValue("pl-PL")] POLISH,
        [StringValue("pt-BR")] PORTUGESE_BRAZIL,
        [StringValue("pt-PT")] PORTUGESE,
        [StringValue("ro-RO")] ROMANIAN,
        [StringValue("ru-RU")] RUSSIAN,
        [StringValue("sk-SK")] SLOVAK,
        [StringValue("sv-SE")] SWEDISH,
        [StringValue("ta-IN")] TAMIL_INDIA,
        [StringValue("ta-LK")] TAMIL_SRI_LANKA,
        [StringValue("th-TH")] THAI,
        [StringValue("tr-TR")] TURKISH,
        [StringValue("zh-CN")] CHINESE,
        [StringValue("zh-HK")] CHINESE_HONG_KONG,
        [StringValue("zh-TW")] CHINESE_TAIWAN
    }
}