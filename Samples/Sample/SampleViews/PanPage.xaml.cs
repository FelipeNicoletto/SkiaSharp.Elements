using SkiaSharp;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanPage : ContentPage
    {
        private PanGestureRecognizer _panGestureRecognizer;
        private SkiaSharp.Elements.Element _currentElement;
        private SKPoint? _startLocation;


        public PanPage()
        {
            InitializeComponent();

            AddRectangle();

            AddImage();

            _panGestureRecognizer = new PanGestureRecognizer();
            _panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;
            
            canvas.GestureRecognizers.Add(_panGestureRecognizer);
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                if (_currentElement != null)
                {
                    
                    _currentElement.Location = new SKPoint(_startLocation.Value.X + (float)e.TotalX * App.DisplayScaleFactor,
                                                           _startLocation.Value.Y + (float)e.TotalY * App.DisplayScaleFactor);
                }
                
            }
        }
        
        private void Canvas_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SkiaSharp.Views.Forms.SKTouchAction.Pressed)
            {
                _currentElement = canvas.GetElementAtPoint(e.Location);

                if (_currentElement != null)
                {
                    _startLocation = _currentElement.Location;

                    canvas.Elements.BringToFront(_currentElement);
                }
            }
        }

        private void AddRectangle()
        {
            var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(70, 70, 200, 200))
            {
                FillColor = new SKColor(SKColors.SpringGreen.Red, SKColors.SpringGreen.Green, SKColors.SpringGreen.Blue, 200),
            };
            canvas.Elements.Add(rectangle);
        }

        private void AddImage()
        {
            var stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Sample.Images.Sample1.jpg");
            var image = new SkiaSharp.Elements.Image(SKBitmap.Decode(stream));
            image.Bounds = SKRect.Create(80, 140, image.Width * .4f, image.Height * .4f);
            canvas.Elements.Add(image);
        }
    }
}