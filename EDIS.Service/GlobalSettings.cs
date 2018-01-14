using EDIS.Core;

namespace EDIS.Service
{
    public static class GlobalSettings
    {
        public static string BaseURL => string.IsNullOrEmpty(Settings.Api) ? "https://api.electricalcertificates.co.uk" : Settings.Api;
        //public static string BaseURL = "http://api-dev.electricalcertificates.local";
    }
}