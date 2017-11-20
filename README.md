# dot-screencap

## Features
+ Take screenshots

 ``` csharp
var screencap = new ScreenCapture();
screencap.TakeScreenshot();
 ```
+ Record animations  

 ``` csharp
var screencap = new ScreenCapture
{
    // Recommended for large resolution
    // recordings and long GIFs.
    ScalingFactor = 2
};

// Records a GIF with 100 frames,
// every 50 ms is a frame recorded.
screencap.RecordAnimation(100, 50);
 ```

+ Select screenregion

 ``` csharp
var screencap = new ScreenCapture();
screencap.ScreenRegion = new ScreenRegion(...);
 ```

***

## How to use it in your solution
Clone this repository and build the solution.  
Add a reference to the built DotScreencap.dll in your solution.  
