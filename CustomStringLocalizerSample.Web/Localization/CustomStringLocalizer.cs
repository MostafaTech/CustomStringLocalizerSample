using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace CustomStringLocalizerSample.Web.Localization
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        private readonly ILocalizationService _localizationService;
        public CustomStringLocalizer(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = _localizationService.GetValue(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = _localizationService.GetValue(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            _localizationService.SetCurrentLocale(culture.Name);
            return new CustomStringLocalizer(_localizationService);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizationService.CurrentResource.Select(x => new LocalizedString(x.Key, x.Value, false));
        }
    }
}
