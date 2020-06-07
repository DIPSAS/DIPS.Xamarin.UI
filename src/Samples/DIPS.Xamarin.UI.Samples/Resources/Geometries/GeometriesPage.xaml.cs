using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions.Markup;
using DIPS.Xamarin.UI.Resources.Geometries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Resources.Geometries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeometriesPage
    {
        public GeometriesPage()
        {
            InitializeComponent();
        }



        public string BPM   
        {
            get => (string)GetValue(BPMProperty);
            set => SetValue(BPMProperty, value);
        }

        public static readonly BindableProperty BPMProperty = BindableProperty.Create(nameof(BPM), typeof(string), typeof(GeometriesPage), propertyChanged:OnBPMChanged);

        private static async void OnBPMChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is GeometriesPage geometriesPage))
                return;
            while ((int.TryParse(geometriesPage.BPM, out var bpm)))
            {
                var BPS = (bpm / 60);
                if (BPS == 0)
                    return;
                var length = (uint)(1000 / BPS) / 2;
                await geometriesPage.Heart.ScaleTo(1.2, easing: Easing.SpringOut, length: length);
                await geometriesPage.Heart.ScaleTo(1, length: length);
            }
        }
    }
}