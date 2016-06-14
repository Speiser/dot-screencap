# dot-screencap

###### Example
Created with dot-screencap v0.1.51  
![dot-screencap](https://github.com/speisaa/dot-screencap/raw/master/Documentation/Pictues/v0151showcase.gif)  

***

###### Features
+ Take screenshots of your primary screen.

 ``` csharp
var screencap = new ScreenCapture();  
screencap.OnScreenshotTaken += Screencap_OnScreenshotTaken; // Optional: Subscribe to the event.
screencap.TakeScreenshot();                                 // Optional: Add a filename.
 ```
+ Record your primary screen and save it as gif.  
  Warning: Experimental stage.
 ``` csharp
var screencap = new ScreenCapture();
screencap.AnimationCreator.OnOutOfMemoryExceptionThrown += AnimationCreator_OnOutOfMemoryExceptionThrown;
screencap.OnAnimationCreated += Screencap_OnAnimationCreated;   // Optional: Subscribe to the events.
screencap.CreateGIF(50, 100);                                   // Will record 50 frames, 10 per second.
 ```

+ Multimonitor support.  
  Warning: Only tested with 2 screens, but it should work with more aswell.
 ``` csharp
using System.Windows.Forms;

// Default screen is your Primary Screen!
var screencap = new ScreenCapture();
Screen[] myScreens = screencap.AllScreens;
// Check the array for the screen you want to capture.
// Then paste it into:
screencap.ChangeScreen(myScreens[1]);   // myScreens[1] is my second screen.
 ```

***

###### Documentation
Click [here](http://speisaa.github.io) to see the documentation.

***

###### Goals
* Improve my coding skills :joy:
* Learn to use git
* Learn to write clean documentations
* Make screen capturing easier in C#

***

###### Planned features
- [x] Take Screenshots (added in v0.1.0)
- [x] Create Animations (added in v0.1.1)
- [x] Add Documentation (added in v0.1.2)
- [x] Add Multimonitor support (added in v0.1.5)
- [ ] Add Examples
- [ ] Add selectable screenregion
- [ ] Change output path
- [ ] Record Videos

***

###### How to use it in your solution
Download this repository and build it.  
Add a reference to the built DotScreencap.dll in your solution.  
Add `using DotScreencap;` to your project files.

***

###### Final word
If you like the project please **star it** in order to help to spread the word. That way you will make the framework more significant and in the same time you will motivate me to improve it, so the benefit is mutual.
