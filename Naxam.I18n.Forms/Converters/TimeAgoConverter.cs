using System;
using System.Globalization;
using Xamarin.Forms;

namespace Naxam.I18n.Forms.Converters
{
	public class TimeAgoConverter : IValueConverter
	{
		public static TimeAgoConverter Preserve()
		{
			return new TimeAgoConverter();
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var d = value as DateTimeOffset?;

			if (value is DateTime)
			{
				d = (DateTime)value;
			}

			ILocalizedResourceProvider res = null;

			try
			{
				res = DependencyService.Get<IDependencyGetter>().Get<ILocalizedResourceProvider>();
			}
			catch
			{
				res = null;
			}

			if (!d.HasValue)
			{
				return res?.GetText("TimeAgo.NA") ?? "N/A";
			}

			DateTimeOffset now = DateTime.Now;
			var timespan = now.Subtract(d.Value);
			if (timespan < TimeSpan.FromSeconds(60))
			{
				return res?.GetText("TimeAgo.LessThan1Minute") ?? "Just now";
			}
			if (timespan < TimeSpan.FromMinutes(5))
			{
				return res?.GetText("TimeAgo.LessThan5Minutes") ?? "Few minutes ago";
			}

			if (timespan < TimeSpan.FromMinutes(60))
			{
				return res?.GetText("TimeAgo.LessThan1Hour", timespan.Minutes) ?? $"{timespan.Minutes} minutes ago";
			}

			if (timespan < TimeSpan.FromHours(24))
			{
				return res?.GetText("TimeAgo.LessThan1Day", timespan.Hours) ?? $"{timespan.Hours} hours ago";
			}

			if (timespan < TimeSpan.FromDays(2))
			{
				return res?.GetText("TimeAgo.Yesterday") ?? $"Yesterday";
			}

			if (timespan < TimeSpan.FromDays(7))
			{
				return res?.GetText("TimeAgo.LessThan1Week", timespan.Days) ?? $"{timespan.Days} days ago";
			}

			return res?.GetText("TimeAgo.Past", d.Value) ?? d.Value.ToString("MMM dd, YYYY");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
