using System;
using System.Globalization;
using System.Resources;

namespace Naxam.I18n
{
	public class LocalizedResourceProvider : ILocalizedResourceProvider
	{
		readonly ResourceManager resmgr;
		readonly ILocalizer localizer;

		public LocalizedResourceProvider(ILocalizer localizer, ResourceManager resourceManager)
		{
			this.localizer = localizer;
			resmgr = resourceManager;
		}

		public string GetText(string resourceKey, params object[] objects)
		{
			if (string.IsNullOrWhiteSpace(resourceKey))
			{
				return resourceKey;
			}

			var ci = localizer.GetCurrentCultureInfo();
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
