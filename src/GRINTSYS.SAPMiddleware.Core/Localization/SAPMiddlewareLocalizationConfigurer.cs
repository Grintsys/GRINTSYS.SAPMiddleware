using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace GRINTSYS.SAPMiddleware.Localization
{
    public static class SAPMiddlewareLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SAPMiddlewareConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SAPMiddlewareLocalizationConfigurer).GetAssembly(),
                        "GRINTSYS.SAPMiddleware.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
