namespace SkiaSharp.Elements.Interfaces
{
    public interface IInvalidatable
    {
        void Invalidate();
        void SuspendLayout();
        void ResumeLayout(bool invalidate = false);
    }
}
