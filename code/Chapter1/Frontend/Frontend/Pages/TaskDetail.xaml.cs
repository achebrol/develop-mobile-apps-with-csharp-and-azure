using Frontend.Models;
using Frontend.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetail : ContentPage
    {
        public TaskDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(item);
        }
    }
}