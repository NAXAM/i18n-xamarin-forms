using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Naxam.I18n.Demo
{
	public partial class NewPage : ContentPage
	{
		public NewPage()
		{
			InitializeComponent();

			var items = new List<DateTime>();

			for (int i = 0; i < 100; i++)
			{
				items.Add(DateTime.Now.AddMinutes(-i*(5 + i%13)));
			}

			lstDates.ItemsSource = items;
		}
	}
}
