using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Preserve]

//Add new namespaces below to make them visible when using Custom Namespace : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1

[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Converters.ValueConverters")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.DatePicker")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.RadioButtonGroup")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Popup")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.TrendGraph")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Resources")]