using SkiaSharp.Elements.Collections;

namespace SkiaSharp.Elements.Interfaces
{
    public interface IElementsCollector : IElementContainer
    {
        ElementsCollection Elements { get; }
    }
}
