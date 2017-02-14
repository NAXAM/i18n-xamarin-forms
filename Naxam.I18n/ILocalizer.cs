using System.Globalization;

namespace Naxam.I18n
{
	public interface ILocalizer
	{
		CultureInfo GetCurrentCultureInfo();
		void SetLocale(CultureInfo ci);
	}
}
