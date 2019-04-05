using Frontend.Services;
using Xamarin.Forms;

namespace Frontend
{
    public partial class App : Application
    {
        public static ICloudServiceClient cloudClient = new AzureCloudServiceClient();

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
    }
}
