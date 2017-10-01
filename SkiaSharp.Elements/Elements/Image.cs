namespace SkiaSharp.Elements
{
    public class Image : Element
    {
        #region Constructors

        public Image(SKBitmap bitmap) : this()
        {
            _bitmap = bitmap;
            Size = new SKSize(bitmap.Width, bitmap.Height);
        }

        public Image()
        {
            _borderColor = SKColors.Transparent;
            _sizeMode = ImageSizeMode.Stretch;
            _alignMode = ImageAlignMode.LeftTop;
            _borderWidth = 1;
        }

        #endregion Constructors

        #region Properties

        private bool _drawBorder;

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
        
        private SKBitmap _bitmap;
        public SKBitmap Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                Invalidate();
            }
        }

        private ImageSizeMode _sizeMode;
        public ImageSizeMode SizeMode
        {
            get => _sizeMode;
            set
            {
                _sizeMode = value;
                Invalidate();
            }
        }

        private ImageAlignMode _alignMode;
        public ImageAlignMode AlignMode
        {
            get => _alignMode;
            set
            {
                _alignMode = value;
                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            if (_bitmap != null || _drawBorder)
            {
                DrawBefore(canvas);
                using (var paint = new SKPaint { IsAntialias = true })
                {
                    if (_bitmap != null)
                    {
                        DrawBitmap(canvas, paint);
                    }

                    if (_drawBorder)
                    {
                        if (_drawBorder)
                        {
                            paint.Style = SKPaintStyle.Stroke;
                            paint.Color = BorderColor;
                            paint.StrokeWidth = BorderWidth;
                            canvas.DrawRect(Bounds, paint);
                        }
                    }
                }
                DrawAfter(canvas);
            }
        }

        #endregion Public methods

        #region Private methods

        private void DrawBitmap(SKCanvas canvas, SKPaint paint)
        {
            var imageSrc = GetBitmapSourceRect();
            
            canvas.DrawBitmap(_bitmap, imageSrc, Bounds, paint);
        }

        private SKRect GetBitmapSourceRect()
        {
            if (SizeMode == ImageSizeMode.Stretch)
            {
                return SKRect.Create(0, 0, _bitmap.Width, _bitmap.Height);
            }
            else if (SizeMode == ImageSizeMode.Normal)
            {
                return GetBitmapSourceRectAligned(Width, Height);
            }
            else if (SizeMode == ImageSizeMode.Cover ||
                     SizeMode == ImageSizeMode.Contain)
            {
                float w, h;
                float heightDest = _bitmap.Height * Width / _bitmap.Width;

                if ((SizeMode == ImageSizeMode.Cover && heightDest < Height) ||
                    (SizeMode == ImageSizeMode.Contain && heightDest > Height))
                {
                    h = _bitmap.Height;
                    var widthDest = _bitmap.Width * Height / _bitmap.Height;
                    w = _bitmap.Width * Width / widthDest;
                }
                else
                {
                    w = _bitmap.Width;
                    h = _bitmap.Height * Height / heightDest;
                }
                
                return GetBitmapSourceRectAligned(w, h);
            }
            return SKRect.Empty;
        }

        private SKRect GetBitmapSourceRectAligned(float w, float h)
        {
            float x = 0, y = 0;
            switch (AlignMode)
            {
                case ImageAlignMode.LeftMiddle:
                case ImageAlignMode.CenterMiddle:
                case ImageAlignMode.RightMiddle:
                    y = (_bitmap.Height - h) / 2;
                    break;

                case ImageAlignMode.LeftBottom:
                case ImageAlignMode.CenterBottom:
                case ImageAlignMode.RightBottom:
                    y = _bitmap.Height - h;
                    break;
            }

            switch (AlignMode)
            {
                case ImageAlignMode.CenterTop:
                case ImageAlignMode.CenterMiddle:
                case ImageAlignMode.CenterBottom:
                    x = (_bitmap.Width - w) / 2;
                    break;

                case ImageAlignMode.RightTop:
                case ImageAlignMode.RightMiddle:
                case ImageAlignMode.RightBottom:
                    x = _bitmap.Width - w;
                    break;
            }

            return SKRect.Create(x, y, w, h);
        }

        #endregion Private methods

        #region Enums

        public enum ImageSizeMode
        {
            Stretch,
            Normal,
            Cover,
            Contain
        }

        public enum ImageAlignMode
        {
            LeftTop,
            CenterTop,
            RightTop,

            LeftMiddle,
            CenterMiddle,
            RightMiddle,
            
            LeftBottom,
            CenterBottom,
            RightBottom
        }

        #endregion Enums
    }
}
