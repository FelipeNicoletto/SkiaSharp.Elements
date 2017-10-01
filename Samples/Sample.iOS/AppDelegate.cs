using Foundation;
using UIKit;

namespace Sample.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

            App.DisplayScreenWidth = (float)UIScreen.MainScreen.Bounds.Width;
            App.DisplayScreenHeight = (float)UIScreen.MainScreen.Bounds.Height;
            App.DisplayScaleFactor = (float)UIScreen.MainScreen.Scale;

            LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
