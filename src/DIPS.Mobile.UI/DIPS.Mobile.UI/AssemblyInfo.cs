using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.Droid")]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.iOS")]

[assembly: Preserve]
//Add new namespaces below to make them visible when using Custom Namespace : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1
[assembly:XmlnsPrefix("http://dips.com/mobile.ui", "dui")]

[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pages")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Buttons")]