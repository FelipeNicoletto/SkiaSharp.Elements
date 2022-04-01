using SkiaSharp.Elements.Collections;
using SkiaSharp.Elements.Interfaces;

namespace SkiaSharp.Elements
{
    public class Group : Element, IElementsCollector
    {
        #region Constructors

        public Group()
        {
            Elements = new ElementsCollection(this);
        }

        #endregion Constructors

        #region Properties

        public ElementsCollection Elements { get; }
        
        public override SKRect Bounds
        {
            get
            {
                if (Elements.Count > 0)
                {
                    float left = float.MaxValue,
                          top = float.MaxValue,
                          right = float.MinValue,
                          bottom = float.MinValue;

                    foreach (var element in Elements)
                    {
                        var bounds = element.Bounds;
                        
                        var transformation = element.GetTransformation(false);
                        if (transformation.HasValue)
                        {
                            bounds = transformation.Value.MapRect(bounds);
                        }

                        if (left > bounds.Left)
                        {
                            left = bounds.Left;
                        }

                        if (right < bounds.Right)
                        {
                            right = bounds.Right;
                        }

                        if (top > bounds.Top)
                        {
                            top = bounds.Top;
                        }

                        if (bottom < bounds.Bottom)
                        {
                            bottom = bounds.Bottom;
                        }
                    }
                    return new SKRect(left, top, right, bottom);
                }
                return SKRect.Empty;
            }
            set
            {
                SuspendLayout();
                var bounds = Bounds;
                var xDif = value.Left - bounds.Left;
                var yDif = value.Top - bounds.Top;

                foreach (var element in Elements)
                {
                    var e = element as Element;
                    if (e != null)
                    {
                        var elementBounds = element.Bounds;
                        element.Bounds = SKRect.Create(elementBounds.Left + xDif,
                                                       elementBounds.Top + yDif,
                                                       elementBounds.Width,
                                                       elementBounds.Height);
                    }
                }
                ResumeLayout(true);
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SKCanvas canvas)
        {
            if (Visible && Elements.Count > 0)
            {
                DrawBefore(canvas);
                
                foreach (var element in Elements)
                {
                    element.Draw(canvas);
                }

                DrawAfter(canvas);
            }
        }

        #endregion Public methods
    }
}
