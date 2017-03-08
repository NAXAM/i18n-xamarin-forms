using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Naxam.I18n.Forms;
using Naxam.I18n.iOS;
using UIKit;

namespace Naxam.I18n.Demo.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			Xamarin.Forms.DependencyService.Register<IDependencyGetter, DepenencyGetter>();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}

	public class DepenencyGetter : IDependencyGetter
	{
        readonly Dictionary<Type, object> cache;
        public DepenencyGetter()
        {
            ILocalizer localizer = new Localizer();
            cache = new Dictionary<Type, object> {
                {
                    typeof(ILocalizer),
                    localizer
                },
                {
                    typeof(ILocalizedResourceProvider),
                    new LocalizedResourceProvider(localizer, App.ResManager)
                }
            };
        }

        public T Get<T>()
		{
			return (T)cache[typeof(T)];
		}
	}
}
