# dot-screencap

## How to use it in your solution
[Download](https://github.com/Speisaa/dot-screencap) this repository and build it.  
Add a reference to the built DotScreencap.dll in your solution.  
Add `using DotScreencap;` to your project files.

## Want to Contribute?
Please read [CONTRIBUTING.md](https://github.com/Speisaa/dot-screencap/blob/master/CONTRIBUTING.md).

## Getting started
#### Initialize a new ScreenCapture instance.
``` csharp
var screencap = new ScreenCapture();
```

#### Take screenshots of your primary screen.
 - ```void TakeScreenshot(params string[] filename)```
``` csharp
screencap.TakeScreenshot();
```

#### Record your primary screen and save it as gif.
 - ```void CreateGIF(int frames, int wait, params string[] filename)```
``` csharp
screencap.CreateGIF(50, 100); // Will record 50 frames, 10 per second.
```

#### Select screenregion.  
 - ```void SetUpperLeftCorner()```
 - ```void SetLowerRightCorner()```
``` csharp
using System.Drawing;

screencap.ScreenRegion.UpperLeftCorner = new Point(100, 100);
screencap.ScreenRegion.SetLowerRightCorner(); // Will set the Point to the current mouse position.
```

#### Change monitor.
 - ```void ChangeScreen(Screen screen)```  
``` csharp
using System.Windows.Forms;

// Default screen is your Primary Screen!
Screen[] myScreens = screencap.AllScreens;
// Check the array for the screen you want to capture.
// Then paste it into:
screencap.ChangeScreen(myScreens[1]);   // myScreens[1] is my second screen.
```

#### Subscribe to events.
``` csharp
screencap.OnScreenshotTaken += Screencap_OnScreenshotTaken;
screencap.AnimationCreator.OnOutOfMemoryExceptionThrown += AnimationCreator_OnOutOfMemoryExceptionThrown;
screencap.OnAnimationRecorded += Screencap_OnAnimationRecorded;
screencap.OnAnimationCreated += Screencap_OnAnimationCreated;
```

## Events
| EventName                    | EventLocation    | Is fired after...                                 |
| :--------------------------- | :--------------- | :------------------------------------------------ |
| OnAnimationCreated           | ScreenCapture    | ... an animation was created.                     |
| OnAnimationRecorded          | AnimationCreator | ... recording was finished, before file is saved. |
| OnOutOfMemoryExceptionThrown | AnimationCreator | ... an OutOfMemoryException was thrown.           |
| OnScreenshotTaken            | ScreenCapture    | ... a screenshot was taken.                       |


## Examples
#### Create a screenshot of your primary screen using the console.
``` csharp
namespace ScreenShot
{
    using System;
    using DotScreencap;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Initializes a new instance of the ScreenCapture class.
            var screencap = new ScreenCapture();

            // Subscribes to the OnScreenshotTaken event.
            screencap.OnScreenshotTaken += Screencap_OnScreenshotTaken;

            // Takes a screenshot and saves it to the execution folder (HelloDocs.jpg).
            screencap.TakeScreenshot("HelloDocs");
            Console.ReadLine();
        }

        private static void Screencap_OnScreenshotTaken(object sender, ScreenCaptureOnScreenshotTakenEventArgs e)
        {
            Console.WriteLine($"{e.PictureCreator.Filename}.jpg has been saved.");
        }
    }
}
```

<br><br>
Lastest updated: dot-screencap v0.1.63
