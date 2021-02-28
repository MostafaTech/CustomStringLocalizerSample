using System;
using System.Collections.Generic;

namespace CustomStringLocalizerSample
{
    public interface ILocalizationService
    {
        string GetCurrentLocale();
        void SetCurrentLocale(string locale);
        IDictionary<string, string> CurrentResource { get; }
        string GetValue(string key);
    }
}
