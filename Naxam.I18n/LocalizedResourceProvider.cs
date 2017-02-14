using System;
using System.Globalization;
using System.Resources;

namespace Naxam.I18n
{
	public class LocalizedResourceProvider : ILocalizedResourceProvider
	{
		readonly ResourceManager resmgr;
		readonly CultureInfo ci;

		public LocalizedResourceProvider(ILocalizer localizer, ResourceManager resourceManager)
		{
			resmgr = resourceManager;
			ci = localizer.GetCurrentCultureInfo();
		}

		public string GetText(string resourceKey, params object[] objects)
		{
			if (string.IsNullOrWhiteSpace(resourceKey))
			{
				return resourceKey;
			}

			var translation = resmgr.GetString(resourceKey, ci);

			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found for culture '{1}'.", resourceKey, ci.Name),
nameof(resourceKey));
#else
				translation = resourceKey; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}

			return string.Format(translation, objects);
		}
	}
}
