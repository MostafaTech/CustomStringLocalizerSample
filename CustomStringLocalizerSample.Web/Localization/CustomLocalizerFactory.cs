using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace CustomStringLocalizerSample.Web.Localization
{
    public class CustomLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ILocalizationService _localizationService;
        public CustomLocalizerFactory(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new CustomStringLocalizer(_localizationService);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new CustomStringLocalizer(_localizationService);
        }
    }
}
