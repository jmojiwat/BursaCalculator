using System.IO;
using System.Xml.Serialization;
using LanguageExt;

namespace BursaCalculator.Persistence
{
    public static class FeeSettingsExtensions
    {
        private static readonly string UserSettingsFilename = System.AppDomain.CurrentDomain.BaseDirectory + "fees.xml";

        public static FeeSettings PersistFeeSettings(FeeSettings settings)
        {
            using var writer = new StreamWriter(UserSettingsFilename);
            var xmls = new XmlSerializer(typeof(FeeSettings));
            xmls.Serialize(writer, settings);

            return settings;
        }

        public static FeeSettings RetrieveFeeSettings()
        {
            return Prelude.Try(() =>
                {
                    using var reader = new StreamReader(UserSettingsFilename);
                    var xmls = new XmlSerializer(typeof(FeeSettings));
                    return xmls.Deserialize(reader) as FeeSettings;
                })
                .IfFail(() => new FeeSettings());
        }
    }
}