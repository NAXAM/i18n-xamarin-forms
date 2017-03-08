using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;

namespace Naxam.I18n.Demo
{
	public class App : Application
	{
        const string ResourceId = "Naxam.I18n.Demo.LocalTexts";

        public App()
		{
			MainPage = new NavigationPage(new MyPage());
		}

		public static ResourceManager ResManager { 
			get
			{
				return new ResourceManager(ResourceId, typeof(App).GetTypeInfo().Assembly);
			}
		}
	}
}
