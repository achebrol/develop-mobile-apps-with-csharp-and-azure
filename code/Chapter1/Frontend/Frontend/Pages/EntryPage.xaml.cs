using Frontend.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            InitializeComponent();
            BindingContext = new EntryPageViewModel();
        }
    }
}