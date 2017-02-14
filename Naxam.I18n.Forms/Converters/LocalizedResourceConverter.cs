using System;
using System.Globalization;
using Xamarin.Forms;

namespace Naxam.I18n.Forms.Converters
{
	public class LocalizedResourceConverter : IValueConverter
	{
		public static LocalizedResourceConverter Preserve()
		{
			return new LocalizedResourceConverter();
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ILocalizedResourceProvider res = null;

			try
			{
				res = DependencyService.Get<IDependencyGetter>().Get<ILocalizedResourceProvider>();
			}
			catch
			{
				res = null;
			}

			return res?.GetText(value as string) ?? value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
