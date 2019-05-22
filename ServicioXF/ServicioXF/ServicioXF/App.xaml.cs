using Matcha.BackgroundService;
using ServicioXF.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ServicioXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new PeriodicWebCall(3));
            //BackgroundAggregatorService.Add(() => new PeriodicCall2(4));

            //Start the background service
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //BackgroundAggregatorService.StopBackgroundService();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
