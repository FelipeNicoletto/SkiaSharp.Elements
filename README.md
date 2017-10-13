# SkiaSharp.Elements

Simple way to draw and interact with elements using the library SkiaSharp.
Compatible with Xamarin Forms

## Using Elements
Install the ![NuGet package SkiaSharp.Elements](https://www.nuget.org/packages/SkiaSharp.Elements/): 
```
nuget install SkiaSharp.Element
```

## Getting Started
```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="Sample.SampleViews.SampleBasicPage"
             xmlns:elements="clr-namespace:SkiaSharp.Elements.Forms;assembly=SkiaSharp.Elements.Forms"
             Title="Detail">
    
    <elements:CanvasView x:Name="canvas" />
    
</ContentPage>
```
### Elements
- Rectangle
- Ellipse
- Image
- Text
- Polygon
- Line
- Group


### Drawing Rectangle
```
var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(20, 20, 100, 100))
{
    FillColor = SKColors.SpringGreen
};
canvas.Elements.Add(rectangle);
```

### Transformation
```
var rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(120, 150, 100, 100))
{
    FillColor = SKColors.SpringGreen
    Transformation = SKMatrix.MakeRotationDegrees(45)
};
canvas.Elements.Add(rectangle);
```

### Animation
```
new Animation((value) =>
{
    rectangle.Transformation = SKMatrix.MakeRotationDegrees(360 * (float)value);
})
.Commit(this, "Anim", length: 2000, easing: Easing.SpringOut);
```
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/animation.gif)
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/animation2.gif)

## Samples
Get sample ![here](https://github.com/FelipeNicoletto/SkiaSharp.Elements/tree/master/Samples)

![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_1.png)
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_2.png)
