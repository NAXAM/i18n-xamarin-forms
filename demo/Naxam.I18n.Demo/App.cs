using System;
using System.Resources;
using Xamarin.Forms;

namespace Naxam.I18n.Demo
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new MyPage());
		}

		public static ResourceManager ResManager { 
			get
			{
				return new ResourceManager(typeof(LocalTexts));
			}
		}
	}
}
