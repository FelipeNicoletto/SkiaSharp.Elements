using System;

namespace SkiaSharp.Elements
{
    public class Text : Rectangle, IDisposable
    {
        #region Constructors

        public Text(string content) : this()
        {
            _content = content;
        }

        private Text() : base(new SKRect())
        {
            _foreColor = SKColors.Black;
            _fontSize = 15f;
            _autoSize = true;
        }

        #endregion Constructors

        #region Properties

        private static SKTypeface _defaultFont;
        public static SKTypeface DefaultFont { get => _defaultFont = _defaultFont ?? SKTypeface.FromFamilyName("Verdana"); }
        
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                Invalidate();
            }
        }
        
        private SKColor _foreColor;
        public SKColor ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                Invalidate();
            }
        }
        
        public string FontFamily
        {
            get => Font?.FamilyName ?? DefaultFont.FamilyName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Font?.Dispose();
                    Font = null;
                }
                else if(Font == null || !Font.FamilyName.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    Font = SKTypeface.FromFamilyName(value);
                }
            }
        }

        private SKTypeface _font;
        public SKTypeface Font
        {
            get => _font;
            set
            {
                _font = value;
                Invalidate();
            }
        }

        private float _fontSize;
        public float FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                Invalidate();
            }
        }
        
        private bool _autoSize;
        public bool AutoSize
        {
            get => _autoSize;
            set
            {
                if(value && _autoSize != value)
                {
                    _bounds = null;
                }
                _autoSize = value;
            }
        }
        
        private SKPoint _location;
        private SKRect? _bounds;
        public override SKRect Bounds
        {
            get
            {
                if (!_bounds.HasValue)
                {
                    using (var paint = CreatePaint())
                    {
                        var b = new SKRect();
                        paint.MeasureText(Content, ref b);
                        _bounds = SKRect.Create(_location, b.Size);
                    }
                }
                return _bounds.Value;
            }
            set
            {
                _location = value.Location;
                if (AutoSize)
                {
                    _bounds = null;
                }
                else
                {
                    _bounds = value;
                }
                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            DrawBefore(canvas);

            SuspendDrawBeforeAfter();
            base.Draw(canvas);
            ResumeDrawBeforeAfter();

            if (!string.IsNullOrWhiteSpace(Content))
            {
                using (var paint = CreatePaint())
                {
                    canvas.DrawText(Content, _location.X, _location.Y + Bounds.Height - paint.FontMetrics.Descent, paint);
                }
            }
                
            DrawAfter(canvas);
        }

        public void Dispose()
        {
            Font?.Dispose();
            Font = null;
        }

        #endregion Public methods

        #region Private methods

        private SKPaint CreatePaint()
        {
            return new SKPaint
            {
                IsAntialias = true,
                Color = ForeColor,
                Typeface = Font ?? DefaultFont,
                TextSize = FontSize
            };
        }

        #endregion Private methods
    }
}
