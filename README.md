# dot-screencap

###### Example
Animation created with dot-screencap v0.1.51  
![dot-screencap](https://github.com/Speisaa/dot-screencap/raw/master/Documentation/Pictures/v0151showcase.gif)  

***

###### Features
+ Take screenshots of your primary screen.

 ``` csharp
var screencap = new ScreenCapture();  
screencap.TakeScreenshot();
 ```
+ Record your primary screen and save it as gif.  

 ``` csharp
int wait_ms_between_frames = 100;
int frames = 50;
var screencap = new ScreenCapture();
screencap.CreateAnimation(frames, wait_ms_between_frames); 
 ```

+ Multimonitor support.  

 ``` csharp
var screencap = new ScreenCapture();
Screen[] myScreens = screencap.AllScreens;
screencap.ChangeScreen(myScreens[1]);
 ```

+ Selectable screenregion.  

 ``` csharp
var screencap = new ScreenCapture();
screencap.ScreenRegion.UpperLeftCorner = new Point(100, 100);
// or
screencap.ScreenRegion.SetLowerRightCorner(); // Will set the Point to the current mouse position.
 ```

***

###### Want to Contribute?
Please read [CONTRIBUTING.md](https://github.com/Speisaa/dot-screencap/blob/master/CONTRIBUTING.md).

***

###### Documentation
Added later

***

###### Goals
* Improve my coding skills :joy:
* Learn to use git
* Learn to write clean documentations
* Programmatically take screenshots
* Programmatically create animations

***

###### Planned features
- [x] Take Screenshots (added in v0.1.0)
- [x] Create Animations (added in v0.1.1)
- [x] Add Multimonitor support (added in v0.1.5)
- [x] Add selectable screenregion (added in v0.1.6)
- [ ] Performance improvements
- [ ] Change output path

***

###### How to use it in your solution
Download this repository and build it.  
Add a reference to the built DotScreencap.dll in your solution.  
Add `using DotScreencap;` to your project files.

***

###### Final word
If you like the project please **star it** in order to help to spread the word. That way you will make the framework more significant and in the same time you will motivate me to improve it, so the benefit is mutual.
