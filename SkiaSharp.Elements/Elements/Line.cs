namespace SkiaSharp.Elements
{
    public class Line : Element
    {
        #region Constructors

        public Line(SKPoint point1, SKPoint point2) : this()
        {
            _point1 = point1;
            _point2 = point2;
        }

        private Line()
        {
            _color = SKColors.Black;
            _lineWidth = 1;
        }

        #endregion Constructors

        #region Properties

        private SKPoint _point1;
        public SKPoint Point1
        {
            get => _point1;
            set
            {
                _point1 = value;
                Invalidate();
            }
        }

        private SKPoint _point2;
        public SKPoint Point2
        {
            get => _point2;
            set
            {
                _point2 = value;
                Invalidate();
            }
        }
        
        private SKColor _color;
        public SKColor Color
        {
            get => _color;
            set
            {
                _color = value;
                Invalidate();
            }
        }

        private float _lineWidth;
        public float LineWidth
        {
            get => _lineWidth;
            set
            {
                _lineWidth = value;
                Invalidate();
            }
        }

        public override SKRect Bounds
        {
            get => new SKRect(Point1.X,
                              Point1.Y,
                              Point2.X,
                              Point2.Y);
            set
            {
                _point1 = new SKPoint(value.Left, value.Top);
                _point2 = new SKPoint(value.Right, value.Bottom);

                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            DrawBefore(canvas);

            using (var paint = new SKPaint { IsAntialias = true })
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = Color;
                paint.StrokeWidth = LineWidth;
                canvas.DrawLine(Point1.X, Point1.Y, Point2.X, Point2.Y, paint);
            }
                
            DrawAfter(canvas);
        }

        #endregion Public methods
    }
}
