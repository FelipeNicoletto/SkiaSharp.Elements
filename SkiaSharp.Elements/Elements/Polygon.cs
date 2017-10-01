namespace SkiaSharp.Elements
{
    public class Polygon : Element
    {
        #region Constructors

        public Polygon(SKPath path) : this()
        {
            _path = path;
        }

        private Polygon()
        {
            _fillColor = SKColors.Transparent;
            _borderColor = SKColors.Transparent;
            _borderWidth = 1;
            _fillType = SKPathFillType.Winding;
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
                _drawBorder = BorderWidth > 0 && BorderColor != SKColors.Transparent;
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
                _drawBorder = BorderWidth > 0 && BorderColor != SKColors.Transparent;
                Invalidate();
            }
        }

        private SKPathFillType _fillType;
        public SKPathFillType FillType
        {
            get => _fillType;
            set
            {
                _fillType = value;
                Invalidate();
            }
        }

        private SKPath _path;
        public SKPath Path
        {
            get => _path;
            set
            {
                _path = value;
                Invalidate();
            }
        }
        
        public override SKRect Bounds
        {
            get => Path?.Bounds ?? SKRect.Empty;
            set
            {
                var bounds = Path.Bounds;
                Path.Transform(SKMatrix.MakeTranslation(value.Location.X - bounds.Left, value.Location.Y - bounds.Top));
                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            if (_drawFill || _drawBorder)
            {
                DrawBefore(canvas);

                using (var paint = new SKPaint { IsAntialias = true })
                {
                    if (_drawFill)
                    {
                        Path.FillType = FillType;
                        paint.Color = FillColor;
                        canvas.DrawPath(Path, paint);
                    }

                    if (_drawBorder)
                    {
                        paint.Style = SKPaintStyle.Stroke;
                        paint.Color = BorderColor;
                        paint.StrokeWidth = BorderWidth;
                        Path.FillType = SKPathFillType.Winding;
                        canvas.DrawPath(Path, paint);
                    }
                }
                
                DrawAfter(canvas);
            }
        }

        #endregion Public methods
    }
}
