namespace Sample.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Sample.App.DisplayScreenHeight = (float)Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Height;
            Sample.App.DisplayScreenWidth = (float)Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Width;
            Sample.App.DisplayScaleFactor = (float)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            LoadApplication(new Sample.App());
        }
    }
}