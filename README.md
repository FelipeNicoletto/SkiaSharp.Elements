# SkiaSharp.Elements

## Using Elements
Install the NuGet package SkiaSharp.Elements:
```
nuget install SkiaSharp.Elements
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

### Drawing a Rectangle
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
    _rectangle.Transformation = SKMatrix.MakeRotationDegrees(360 * (float)value);
})
.Commit(this, "Anim", length: 2000, easing: Easing.SpringOut);
```

## Samples
Get sample ![here](https://github.com/FelipeNicoletto/SkiaSharp.Elements/tree/master/Samples)

![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_1.png)
![](https://raw.githubusercontent.com/FelipeNicoletto/SkiaSharp.Elements/master/images/image_2.png)
