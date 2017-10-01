using Sample.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sample
{
    public partial class App : Application
    {
        public static float DisplayScreenWidth;
        public static float DisplayScreenHeight;
        public static float DisplayScaleFactor;

        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new MainPage();
        }
    }
}
