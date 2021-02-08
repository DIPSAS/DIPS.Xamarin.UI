using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Preserve]
[assembly:InternalsVisibleTo("DIPS.Xamarin.UI.Tests")]
[assembly: InternalsVisibleTo("DIPS.Xamarin.UI.Android")]
[assembly: InternalsVisibleTo("DIPS.Xamarin.UI.iOS")]

//Add new namespaces below to make them visible when using Custom Namespace : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1
[assembly:XmlnsPrefix("http://dips.xamarin.ui.com", "dxui")]

[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Converters.ValueConverters")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Converters.MultiValueConverters")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.DatePicker")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.TimePicker")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.RadioButtonGroup")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.TrendGraph")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Resources.Colors")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Slidable")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Modality")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Modality.AttachedProperties")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Popup")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Sheet")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Content")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Toast.Toast")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Util")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Content.DataTemplateSelectors")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.FloatingActionMenu")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Tag")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.Skeleton")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Extensions.Markup")]
[assembly: XmlnsDefinition("http://dips.xamarin.ui.com", "DIPS.Xamarin.UI.Controls.BorderBox.BorderBox")]
