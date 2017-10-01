using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Sample.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            App.DisplayScreenWidth = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;
            App.DisplayScreenHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;
            App.DisplayScaleFactor = Resources.DisplayMetrics.Density;
            
            LoadApplication(new App());
        }
    }
}