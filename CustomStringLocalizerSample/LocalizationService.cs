using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace CustomStringLocalizerSample
{
    public class LocalizationService : ILocalizationService
    {
        private readonly LocalizationOptions _options;
        private Dictionary<string, string> _dicEnglish;
        private Dictionary<string, string> _dicPersian;
        private Dictionary<string, IDictionary<string, string>> _resources;
        public LocalizationService(IOptions<LocalizationOptions> options)
        {
            _options = options.Value;

            _dicEnglish = new Dictionary<string, string>()
            {
                { "hi", "Hi" },
                { "yes", "Yes" },
                { "contact", "Contact" },
                { "name", "Name" },
                { "message", "Message" },
                { "error-required", "This field is required" },
            };
            _dicPersian = new Dictionary<string, string>()
            {
                { "hi", "سلام" },
                { "yes", "بله" },
                { "contact", "تماس با ما" },
                { "name", "نام" },
                { "message", "متن پیام" },
                { "error-required", "مقدار این قیلد اجباری است" },
            };
            _resources = new Dictionary<string, IDictionary<string, string>>()
            {
                { "en-US", _dicEnglish },
                { "fa-IR", _dicPersian },
            };
        }

        public string GetCurrentLocale() => _options.Culture;
        public void SetCurrentLocale(string locale) => _options.Culture = locale;

        public IDictionary<string, string> CurrentResource => _resources[_options.Culture];

        public string GetValue(string key)
        {
            if (CurrentResource.ContainsKey(key.ToLower()))
                return CurrentResource[key.ToLower()];

            return key;
        }
    }
}
