using System;
using Todo.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemList : ContentPage
    {
        public TodoItemList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await TodoItemDatabase.Instance.GetAllItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoItemPage { BindingContext = new TodoItem() });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TodoItemPage { BindingContext = e.SelectedItem as TodoItem });
            }
        }
    }
}