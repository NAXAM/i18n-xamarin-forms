using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Naxam.I18n.Forms
{
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		internal static TranslateExtension Preserve()
		{
			return new TranslateExtension(false);
		}

		readonly ILocalizedResourceProvider resProvider;

		public TranslateExtension() : this(true) { }

		internal TranslateExtension(bool shouldInit)
		{
			if (shouldInit)
			{
				resProvider = DependencyService.Get<IDependencyGetter>().Get<ILocalizedResourceProvider>();
			}
		}

		public string Text { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			return resProvider?.GetText(Text);
		}
	}
}
