# SkiaSharp.Elements

<a href="https://www.nuget.org/packages/SkiaSharp.Elements"><img src="https://img.shields.io/nuget/dt/SkiaSharp.Elements.svg" /></a>

Simple way to draw and interact with elements using the library SkiaSharp.
Compatible with Xamarin Forms

## Using Elements
Install the [NuGet package SkiaSharp.Elements](https://www.nuget.org/packages/SkiaSharp.Elements):
```
nuget install SkiaSharp.Element
```

## Getting Started
[Samples](https://github.com/FelipeNicoletto/SkiaSharp.Elements/tree/master/Samples)

### Elements
- Rectangle
- Ellipse
- Image
- Text
- Polygon
- Line
- Group


### Drawing Rectangle
```csharp
var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(20, 20, 100, 100))
{
    FillColor = SKColors.SpringGreen
};
canvas.Elements.Add(rectangle);
```

### Transformation
```csharp
var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(120, 150, 100, 100))
{
    FillColor = SKColors.SpringGreen
    Transformation = SKMatrix.CreateRotationDegrees(45)
};
canvas.Elements.Add(rectangle);
```

### Animation
```csharp
new Animation((value) =>
{
    rectangle.Transformation = SKMatrix.CreateRotationDegrees(360 * (float)value);
})
.Commit(this, "Anim", length: 2000, easing: Easing.SpringOut);
```
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/animation.gif)
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/animation2.gif)

## Samples
Get sample ![here](https://github.com/FelipeNicoletto/SkiaSharp.Elements/tree/master/Samples)

![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_1.png)
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_2.png)
