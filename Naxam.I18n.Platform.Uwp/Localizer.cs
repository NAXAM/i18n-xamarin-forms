using System;
using System.Globalization;

namespace Naxam.I18n.Platform.Uwp
{
    public class Localizer : ILocalizer
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentUICulture;
        }

        public void SetLocale(CultureInfo ci)
        {
            CultureInfo.CurrentUICulture = ci;
        }
    }
}
