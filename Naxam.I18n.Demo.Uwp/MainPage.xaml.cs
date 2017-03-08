// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Naxam.I18n.Forms;
using Naxam.I18n.Platform.Uwp;
using System;
using System.Collections.Generic;

namespace Naxam.I18n.Demo.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.Forms.DependencyService.Register<IDependencyGetter, DepenencyGetter>();
            
            LoadApplication(new Naxam.I18n.Demo.App());
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
                    new LocalizedResourceProvider(localizer, Demo.App.ResManager)
                }
            };
        }

        public T Get<T>()
        {
            return (T)cache[typeof(T)];
        }
    }
}
