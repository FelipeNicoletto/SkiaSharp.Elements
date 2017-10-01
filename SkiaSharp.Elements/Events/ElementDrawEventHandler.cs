using System;

namespace SkiaSharp.Elements.Events
{
    public delegate void ElementDrawEventHandler(Element sender, ElementDrawEventArgs e);

    public class ElementDrawEventArgs : EventArgs
    {
        public SKCanvas Canvas { get; private set; }

        public ElementDrawEventArgs(SKCanvas canvas)
        {
            Canvas = canvas;
        }
    }
}
