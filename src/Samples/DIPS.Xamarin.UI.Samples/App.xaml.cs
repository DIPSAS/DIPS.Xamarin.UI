using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples
{
    public partial class App : Application
    {
        private MainPage m_mainPage;

        public App()
        {
            InitializeComponent();
            //This has to be called once per application in order to use Custom Namespace in XAML : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1
            DIPS.Xamarin.UI.Library.Initialize();

            m_mainPage = new MainPage();
            MainPage = new NavigationPage(m_mainPage); //{ Bar = Color.White };
        }

        protected override void OnStart()
        {
            m_mainPage.OnStart();
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
