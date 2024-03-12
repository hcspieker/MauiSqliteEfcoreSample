using MauiMseApp.Views;

namespace MauiMseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ManageTodoListPage();
        }
    }
}
