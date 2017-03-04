using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Naxam.I18n.Forms;
using Naxam.I18n.Droid;
using System.Collections.Generic;

namespace Naxam.I18n.Demo.Droid
{
	[Activity(Label = "Naxam.I18n.Demo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.Forms.DependencyService.Register<IDependencyGetter, DepenencyGetter>();

			LoadApplication(new App());
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
