# dot-screencap

## How to use it in your solution
[Download](https://github.com/Speisaa/dot-screencap) this repository and build it.  
Add a reference to the built DotScreencap.dll in your solution.  
Add `using DotScreencap;` to your project files.

## Getting started
#### Initialize a new ScreenCapture instance.
``` csharp
var screencap = new ScreenCapture();
```

#### Take screenshots of your primary screen.
``` csharp
screencap.TakeScreenshot();                                 // Optional: Add a filename.
```

#### Record your primary screen and save it as gif.
WARNING: `CreateGIF()` is still in an experimental stage!
``` csharp
// CreateGIF(int amountOfFrames, int delayBetweenEachFrame);
screencap.CreateGIF(50, 100);                                   // Will record 50 frames, 10 per second.
```

## Events
| EventName                    | EventLocation    | Is fired after...                                  |
| ---------------------------- | ---------------- | -------------------------------------------------- |
| OnAnimationCreated           | ScreenCapture    | Is fired after an animation was created.           |
| OnOutOfMemoryExceptionThrown | AnimationCreator | Is fired after an OutOfMemoryException was thrown. |
| OnScreenshotTaken            | ScreenCapture    | Is fired after a screenshot was taken.             |


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
Lastest updated: dot-screencap v0.1.34
