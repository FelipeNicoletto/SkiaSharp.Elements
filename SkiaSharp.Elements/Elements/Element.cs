using SkiaSharp.Elements.Events;
using SkiaSharp.Elements.Extensions;
using SkiaSharp.Elements.Interfaces;

namespace SkiaSharp.Elements
{
    public abstract class Element : IInvalidatable
    {
        #region Constructors

        public Element()
        {
            _transformationPivot = new SKPoint(.5f, .5f);
        }

        #endregion Constructors

        #region Properties

        private int _suspendLayout;
        private int _suspendDrawBeforeAfter;
        private SKMatrix? _appliedTransformation;

        internal IElementContainer Parent { get; set; }

        public SKPoint Location
        {
            get => Bounds.Location;
            set { Bounds = SKRect.Create(value, Size); }
        }

        public float X
        {
            get => Location.X;
            set { Location = new SKPoint(value, Y); }
        }

        public float Y
        {
            get => Location.Y;
            set { Location = new SKPoint(X, value); }
        }
        
        public SKSize Size
        {
            get => Bounds.Size;
            set { Bounds = SKRect.Create(Location, value); }
        }

        public float Width
        {
            get => Size.Width;
            set { Size = new SKSize(value, Height); }
        }

        public float Height
        {
            get => Size.Height;
            set { Size = new SKSize(Width, value); }
        }

        public float Left => Bounds.Left;
        
        public float Top => Bounds.Top;

        public float Right => Bounds.Right;
        
        public float Bottom => Bounds.Bottom;

        public SKPoint Center
        {
            get
            {
                var bounds = Bounds;
                return new SKPoint(bounds.MidX, bounds.MidY);
            }
            set { Location = new SKPoint(value.X - (Width / 2), value.Y - (Height / 2)); }
        }

        private SKRect _bounds;
        public virtual SKRect Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;
                Invalidate();
            }
        }

        private bool _visible = true;
        public virtual bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                Invalidate();
            }
        }

        private SKMatrix? _transformation;
        public SKMatrix? Transformation
        {
            get => _transformation;
            set
            {
                _transformation = value;
                Invalidate();
            }
        }

        private SKPoint _transformationPivot;
        public SKPoint TransformationPivot
        {
            get => _transformationPivot;
            set
            {
                _transformationPivot = value;
                Invalidate();
            }
        }
        
        public virtual bool EnableDrag { get; set; }

        public virtual bool PreventTouch { get; set; }

        #endregion Properties

        #region Public methods

        public virtual void Draw(SKCanvas canvas)
        {

        }

        public void SuspendLayout()
        {
            _suspendLayout++;
        }

        public void ResumeLayout(bool invalidate = false)
        {
            if (_suspendLayout > 0)
            {
                _suspendLayout--;
            }
            if (invalidate)
            {
                Invalidate();
            }
        }

        public void Invalidate()
        {
            if (_suspendLayout == 0)
            {
                Parent?.Invalidate();
            }
        }

        public virtual bool IsPointInside(SKPoint point)
        {
            var transformation = GetTransformation(true);
            if (transformation.HasValue && transformation.Value.TryInvert(out var invert))
            {
                point = invert.MapPoint(point);
            }
            
            return Bounds.Contains(point);
        }

        public void BringToFront()
        {
            var collector = Parent as IElementsCollector;
            var parentElement = Parent as Element;

            if (collector != null || parentElement != null)
            {
                var controller = GetController();
                controller?.SuspendLayout();

                if (collector != null)
                {
                    collector.Elements.BringToFront(this);
                }

                if (parentElement != null)
                {
                    parentElement.BringToFront();
                }

                controller?.ResumeLayout(true);
            }
        }

        #endregion Public methods

        #region Protected/Internal methods

        protected void DrawBefore(SKCanvas canvas)
        {
            if (_suspendDrawBeforeAfter == 0)
            {
                if (Transformation != null)
                {
                    var transformation = GetTransformation(false).Value;

                    canvas.Concat(ref transformation);
                    _appliedTransformation = transformation;
                }

                OnDrawBefore?.Invoke(this, new ElementDrawEventArgs(canvas));
            }
        }

        protected void DrawAfter(SKCanvas canvas)
        {
            if (_suspendDrawBeforeAfter == 0)
            {
                if (Transformation != null && _appliedTransformation.HasValue)
                {
                    if (_appliedTransformation.Value.TryInvert(out var m))
                    {
                        canvas.Concat(ref m);
                    }
                    _appliedTransformation = null;
                }

                OnDrawAfter?.Invoke(this, new ElementDrawEventArgs(canvas));
            }
        }

        protected void SuspendDrawBeforeAfter()
        {
            _suspendDrawBeforeAfter++;
        }

        protected void ResumeDrawBeforeAfter()
        {
            if (_suspendDrawBeforeAfter > 0)
            {
                _suspendDrawBeforeAfter--;
            }
        }

        internal SKMatrix? GetTransformation(bool concatParents)
        {
            SKMatrix? transformation = null;
            if (Transformation.HasValue)
            {
                var bounds = Bounds;

                var tx = bounds.Left + (bounds.Width * _transformationPivot.X);
                var ty = bounds.Top + (bounds.Height * _transformationPivot.Y);

                var anchor = SKMatrix.CreateTranslation(tx, ty);
                var anchorN = SKMatrix.CreateTranslation(-tx, -ty);
                
                transformation = anchor.Concat(Transformation.Value)
                                       .Concat(anchorN);
            }
            
            if (concatParents)
            {
                if (Parent is Element parent)
                {
                    var t = parent.GetTransformation(true);

                    if (t.HasValue)
                    {
                        if (!transformation.HasValue)
                        {
                            transformation = SKMatrix.CreateIdentity();
                        }

                        transformation = t.Value.Concat(transformation.Value);
                    }
                }
            }
            
            return transformation;
        }
        
        #endregion Protected/Internal methods

        #region Private methods

        private ElementsController GetController()
        {
            if (Parent is ElementsController controller)
            {
                return controller;
            }

            var parent = Parent as Element;

            while (parent != null)
            {
                controller = parent.Parent as ElementsController;
                if (controller != null)
                {
                    return controller;
                }

                parent = parent.Parent as Element;
            }

            return null;
        }

        #endregion Private methods

        #region Events
        
        public event ElementDrawEventHandler OnDrawBefore;

        public event ElementDrawEventHandler OnDrawAfter;

        #endregion Events
    }
}
