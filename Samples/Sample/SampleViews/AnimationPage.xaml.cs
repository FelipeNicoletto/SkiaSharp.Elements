using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        private SkiaSharp.Elements.Rectangle _rectangle;
        private SKPoint _startLocation;

        public AnimationPage()
        {
            InitializeComponent();

            AddRectangle();

            Play();
        }

        private void Play()
        {
            new Animation((value) =>
            {
                canvas.SuspendLayout();

                _rectangle.Transformation = SKMatrix.MakeRotationDegrees(360 * (float)value);

                _rectangle.Location = new SKPoint(_startLocation.X + (100 * (float)value),
                                                  _startLocation.Y + (100 * (float)value));

                canvas.ResumeLayout(true);

            })
            .Commit(this, "Anim", length: 2000, easing: Easing.SpringOut);
        }

        private void AddRectangle()
        {
            _startLocation = new SKPoint(70, 70);
            _rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(_startLocation, new SKSize(200, 200)))
            {
                FillColor = SKColors.SpringGreen
            };
            canvas.Elements.Add(_rectangle);
        }

        private void Canvas_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if(e.ActionType == SkiaSharp.Views.Forms.SKTouchAction.Pressed)
            {
                Play();
            }
        }
    }
}