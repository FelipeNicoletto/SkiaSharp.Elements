using SkiaSharp.Elements.Collections;
using SkiaSharp.Elements.Interfaces;
using System;

namespace SkiaSharp.Elements
{
    public class ElementsController : IElementsCollector
    {
        #region Events

        public event EventHandler OnInvalidate;

        #endregion Events

        #region Constructors

        public ElementsController()
        {
            Elements = new ElementsCollection(this);
            BackgroundColor = SKColors.White;
        }

        #endregion Constructors

        #region Properties

        private int _suspendLayout;
        protected bool SuspendedLayout => _suspendLayout > 0;

        private SKColor _backgroundColor;
        public SKColor BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        public ElementsCollection Elements { get; }
        
        #endregion Properties

        #region Public methods


        private object _lock = new object();

        public void Invalidate()
        {
            if (!SuspendedLayout)
            {
                lock (_lock)
                {
                    OnInvalidate?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Clear(SKCanvas canvas)
        {
            canvas.Clear(BackgroundColor);
        }

        public void Draw(SKCanvas canvas)
        {
            foreach (var element in Elements)
            {
                element.Draw(canvas);
            }
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
        
        #endregion Public methods

    }
}
