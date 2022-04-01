namespace SkiaSharp.Elements.Extensions
{
    public static class SKMatrixExtensions
    {
        public static SKMatrix Rotate(this SKMatrix m, float angle)
            => m.Concat(SKMatrix.CreateRotation(angle));

        public static SKMatrix RotateDegrees(this SKMatrix m, float andegrees)
            => m.Concat(SKMatrix.CreateRotationDegrees(andegrees));

        public static SKMatrix Scale(this SKMatrix m, float sx, float sy)
            => m.Concat(SKMatrix.CreateScale(sx, sy));

        public static SKMatrix Translate(this SKMatrix m, float dx, float dy)
            => m.Concat(SKMatrix.CreateTranslation(dx, dy));

        public static SKMatrix Invert(this SKMatrix m)
            => m.TryInvert(out var m2) ? m2 : m;

        public static SKMatrix Concat(this SKMatrix m, SKMatrix m2)
            => m.PreConcat(m2);
    }
}
