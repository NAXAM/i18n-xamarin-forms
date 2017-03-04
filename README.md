# Xamarin Localization Library for Xamarin/Xamarin.Forms
Xamarin has provided very detail documentation about how to do localize your application [here](https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/). However, Xamarin doesn't provide a read-to-use NUGET package.

This project is to provide to Xamrin Developers a ready-to-use NUGET packaget to localize their Xamarin.Forms application.

```
Install-Package Naxam.I18n
```

# What we have
The original code of this package is to use along with Prism, or the like framework, so code is devided into very small pieces which we could inject easily via IoC support.
- **ILocalizer**: To get/set the display language in our app
- **ILocalizedResourceProvider**: To get the localized texts
- **IDependencyGetter**: To work with DependencyService. You must register one instance of this to provide `ILocalizedResourceProvider` so the built-in converters/markup extension to work
- **TranslateExtension**: A markup extension to translate a given text using `ILocalizedResourceProvider`
- **TimeAgoConverter**: A converter to translate a DateTime/DateTimeOffset instance into recent time display using `ILocalizedResourceProvider`

# How to use
You could look at the demo code for more details.

*Provide a ResourceManager instance for our localized texts. If you use IoC support, you should register there instead.*
```c#
public static ResourceManager ResManager { 
    get
    {
        return new ResourceManager(typeof(LocalTexts));
    }
}
```

*Create and register an instance of IDepenencyGetter so our converter and extension could work. If you use IoC, you could resolve these things by using IoC container.*
```c#
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
```
```c#
Xamarin.Forms.DependencyService.Register<IDependencyGetter, DepenencyGetter>();
```

*Get `ILocalizer` and `ILocalizedResourceProvider`. If you use IoC, you could use its way of getting dependencies.*
```c#
var getter = DependencyService.Get<IDependencyGetter>();

var localizer = getter.Get<ILocalizer>();
var localizeResProvider = getter.Get<ILocalizedResourceProvider>();
```

*Change the culture with `ILocalizer`*
```c#
localizer.SetLocale(culture);
```

*Get localized text*
```c#
localizeResProvider.GetText("MainPage.Title");
```

*Use extension and converter*
```xml
xmlns:i18n="clr-namespace:Naxam.I18n.Forms;assembly=Naxam.I18n.Forms"
xmlns:converters="clr-namespace:Naxam.I18n.Forms.Converters;assembly=Naxam.I18n.Forms"
```
```xml
<ResourceDictionary>
    <converters:TimeAgoConverter x:Key="TimeAgoConverter" />
</ResourceDictionary>
```
```xml
<Label
    Text="{i18n:Translate Page.Title}"
    HorizontalTextAlignment="Center"
    Margin="8"
/>
```

# Provide localized texts for `TimeAgoConverter`
To use `TimeAgoConverter`, you need to provide appropriate localized texts for keys as listed below.
```xml
<data name="TimeAgo.NA" xml:space="preserve">
    <value>N/A</value>
    <comment>N/A</comment>
</data>
<data name="TimeAgo.LessThan1Minute" xml:space="preserve">
    <value>Just now</value>
    <comment>Less than 1 minute</comment>
</data>
<data name="TimeAgo.LessThan5Minutes" xml:space="preserve">
    <value>Few minutes</value>
    <comment>Less than 5 minutes</comment>
</data>
<data name="TimeAgo.LessThan1Hour" xml:space="preserve">
    <value>{0} minutes</value>
    <comment>Less than 1 hour</comment>
</data>
<data name="TimeAgo.LessThan1Day" xml:space="preserve">
    <value>{0} hrs</value>
    <comment>Less than 1 day</comment>
</data>
<data name="TimeAgo.Yesterday" xml:space="preserve">
    <value>Yesterday</value>
    <comment>Yesterday</comment>
</data>
<data name="TimeAgo.LessThan1Week" xml:space="preserve">
    <value>{0} days</value>
    <comment>Less than 1 week</comment>
</data>
<data name="TimeAgo.Past" xml:space="preserve">
    <value>{0:dd/MM/yyyy}</value>
    <comment>Past</comment>
</data>
```






