namespace SkiaSharp.Elements
{
    public class Ellipse : Element
    {
        #region Constructors

        public Ellipse(SKRect bounds) : this()
        {
            Bounds = bounds;
        }

        public Ellipse(SKPoint center, float radius) : this()
        {
            Bounds = SKRect.Create(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        private Ellipse()
        {
            _fillColor = SKColors.Transparent;
            _borderColor = SKColors.Black;
            _borderWidth = 1;
            _drawBorder = true;
        }

        #endregion Constructors

        #region Properties

        private bool _drawBorder;
        private bool _drawFill;

        private SKColor _fillColor;
        public SKColor FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                _drawFill = value != SKColors.Transparent;
                Invalidate();
            }
        }

        private SKColor _borderColor;
        public SKColor BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                _drawBorder = _borderWidth > 0 && _borderColor != SKColors.Transparent;
                Invalidate();
            }
        }

        private float _borderWidth;
        public float BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                _drawBorder = _borderWidth > 0 && _borderColor != SKColors.Transparent;
                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            if (Visible && (_drawFill || _drawBorder))
            {
                DrawBefore(canvas);

                using (var paint = new SKPaint { IsAntialias = true })
                {
                    var bounds = Bounds;

                    if (_drawFill)
                    {
                        paint.Color = FillColor;
                        canvas.DrawOval(bounds, paint);
                    }

                    if (_drawBorder)
                    {
                        paint.Style = SKPaintStyle.Stroke;
                        paint.Color = BorderColor;
                        paint.StrokeWidth = BorderWidth;
                        canvas.DrawOval(bounds, paint);
                    }
                }

                DrawAfter(canvas);
            }
        }

        public override bool IsPointInside(SKPoint point)
        {
            var bounds = Bounds;
            
            var _xRadius = bounds.Width / 2;
            var _yRadius = bounds.Height / 2;

            if (_xRadius <= 0.0 || _yRadius <= 0.0)
            {
                return false;
            }

            var transformation = GetTransformation(true);
            if (transformation.HasValue && transformation.Value.TryInvert(out var invert))
            {
                point = invert.MapPoint(point);
            }

            var normalized = new SKPoint(point.X - bounds.MidX,
                                         point.Y - bounds.MidY);
            
            return ((normalized.X * normalized.X) / (_xRadius * _xRadius)) + 
                   ((normalized.Y * normalized.Y) / (_yRadius * _yRadius)) <= 1f;
        }

        #endregion Public methods

    }
}
