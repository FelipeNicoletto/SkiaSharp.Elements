using SkiaSharp;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleBasicPage : ContentPage
    {
        public SampleBasicPage()
        {
            InitializeComponent();

            AddRectangle();

            AddImage();

            AddCircle();

            AddRectangle2();
        }

        private void AddRectangle()
        {
            var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(20, 20, 100, 100))
            {
                FillColor = SKColors.SpringGreen
            };
            canvas.Elements.Add(rectangle);
        }

        private void AddRectangle2()
        {
            var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(120, 150, 100, 100))
            {
                FillColor = new SKColor(SKColors.SkyBlue.Red, SKColors.SkyBlue.Green, SKColors.SkyBlue.Blue, 200),
                Transformation = SKMatrix.MakeRotationDegrees(45)
            };
            canvas.Elements.Add(rectangle);
        }

        private void AddImage()
        {
            var stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Sample.Images.Sample1.jpg");
            var image = new SkiaSharp.Elements.Image(SKBitmap.Decode(stream));
            image.Bounds = SKRect.Create(80, 80, image.Width * .4f, image.Height * .4f);
            canvas.Elements.Add(image);
        }

        private void AddCircle()
        {
            var circle = new SkiaSharp.Elements.Ellipse(SKRect.Create(70, 100, 100, 100))
            {
                FillColor = SKColors.IndianRed
            };
            canvas.Elements.Add(circle);
        }
    }
}