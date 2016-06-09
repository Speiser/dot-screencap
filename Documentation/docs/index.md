# dot-screencap

## How to use it in your solution
[Download](https://github.com/Speisaa/dot-screencap) and build it.  
Add a reference in your solution.

## Getting started
#### Initialize a new ScreenCapture instance.
``` csharp
var screencap = new ScreenCapture();
```

#### Take screenshots of your primary screen.
``` csharp
screencap.TakeScreenshot("filename");    // Adding a filename is optional.
```
or  
``` csharp
screencap.GetBitmapOfScreen();
screencap.ConvertBitmapToBitmapImage();
Screenshot.SaveScreenshotAsJPG(screencap.ScreenBitmapImage, filename);
```
#### Record your primary screen and save it as gif.
WARNING: `CreateGIF()` is still in an experimental stage and will throw a `OutOfMemoryException` if `recordingTime` is too high!
``` csharp
int recordingTime = 5;                   // Time in seconds
screencap.CreateGif(recordingTime);
```
<br><br>
Lastest updated: dot-screencap v0.1.1
