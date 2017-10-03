using SkiaSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Animation2Page : ContentPage
    {
        public Animation2Page()
        {
            InitializeComponent();

            AddRectangles();

            Play();
        }

        private void Play()
        {
            new Animation((value) =>
            {
                canvas.SuspendLayout();
                
                for (var y = 0; y < 10; y ++)
                {
                    var index = y * 10;
                    for (var x = 0; x < 10; x ++)
                    {
                        var ele = canvas.Elements[index + x];
                        
                        var startX = ((x+1) * 40);
                        var diffX = startX - 400;
                        if(diffX < 0)
                        {
                            diffX *= -1;
                        }
                        diffX = diffX - startX;
                        startX += 60;

                        var startY = ((y+1) * 40);
                        var diffY = startY - 400;
                        if (diffY < 0)
                        {
                            diffY *= -1;
                        }
                        diffY = diffY - startY;
                        startY += 60;
                        
                        ele.Location = new SKPoint(startX + (diffX * (float)value),
                                                   startY + (diffY * (float)value));
                        
                        ele.Transformation = SKMatrix.MakeRotationDegrees(360 * (float)value);
                    }
                }
                
                canvas.ResumeLayout(true);

            })
            .Commit(this, "Anim", length: 3000, easing: Easing.SpringOut, repeat: () => true);
        }

        private void AddRectangles()
        {
            for (var y = 60; y < 460; y += 40)
            {
                var rnd = new Random(y);

                for (var x = 60; x < 460; x += 40)
                {
                    var rect = new SkiaSharp.Elements.Rectangle(SKRect.Create(x, y, 40, 40))
                    {
                        FillColor = new SKColor((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255))
                    };
                    canvas.Elements.Add(rect);

                }
            }
        }
    }
}