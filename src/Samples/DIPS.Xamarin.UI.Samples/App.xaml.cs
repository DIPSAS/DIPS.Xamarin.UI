using System;
using DIPS.Xamarin.UI.Samples.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DIPS.Xamarin.UI.Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //This has to be called once per application in order to use Custom Namespace in XAML : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1
            DIPS.Xamarin.UI.Library.Initialize();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
