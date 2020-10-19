using System.IO;
using System.Xml.Serialization;
using static LanguageExt.Prelude;

namespace BursaCalculator.Persistence
{
    public static class SettingsExtensions
    {
        private static readonly string UserSettingsFilename = System.AppDomain.CurrentDomain.BaseDirectory + "settings.xml";

        public static Settings Persist(Settings settings)
        {
            using var writer = new StreamWriter(UserSettingsFilename);
            var xmls = new XmlSerializer(typeof(Settings));
            xmls.Serialize(writer, settings);

            return settings;
        }

        public static Settings Retrieve()
        {
            return Try(() =>
                {
                    using var reader = new StreamReader(UserSettingsFilename);
                    var xmls = new XmlSerializer(typeof(Settings));
                    return xmls.Deserialize(reader) as Settings;
                })
                .IfFail(() => new Settings());
        }
    }
}