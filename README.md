# dot-screencap

###### Features
+ Take screenshots of your primary screen.

 ``` csharp
var screencap = new ScreenCapture();  
screencap.OnScreenshotTaken += Screencap_OnScreenshotTaken; // Optional: Subscribe to the event.
screencap.TakeScreenshot();                                 // Optional: Add a filename.
 ```
+ Record your primary screen and save it as gif.

 ``` csharp
// Experimental stage.
var screencap = new ScreenCapture();
screencap.AnimationCreator.OnOutOfMemoryExceptionThrown += AnimationCreator_OnOutOfMemoryExceptionThrown;
screencap.OnAnimationCreated += Screencap_OnAnimationCreated;   // Optional: Subscribe to the events.
screencap.CreateGIF(50, 100);                                   // Will record 50 frames, 10 per second.
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
- [x] Take screenshots     (added in v0.1.0)
- [x] Create gifs          (added in v0.1.1)
- [x] Add documentation    (added in v0.1.2)
- [ ] Add examples
- [ ] Change output path
- [ ] Record videos

***

###### How to use it in your solution
Download and build it.  
Add a reference in your solution.

***

###### Final word
If you like the project please **star it** in order to help to spread the word. That way you will make the framework more significant and in the same time you will motivate me to improve it, so the benefit is mutual.
