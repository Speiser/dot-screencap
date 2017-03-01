# dot-screencap

## Features
+ Take screenshots

 ``` csharp
var screencap = new ScreenCapture();
screencap.TakeScreenshot();
 ```
+ Record animations  

 ``` csharp
var screencap = new ScreenCapture();
screencap.RecordAnimation(50, 100);  // 50 frames, 10 per second.
 ```

+ Select screenregion

 ``` csharp
var screencap = new ScreenCapture();
screencap.ScreenRegion = new ScreenRegion(...);
 ```

+ Change monitor  

 ``` csharp
// do stuff
 ```

***

## Want to Contribute?
Please read [CONTRIBUTING.md](https://github.com/Speisaa/dot-screencap/blob/master/CONTRIBUTING.md).

***

## Documentation
Added later

***

## Planned features
- [X] Take screenshots
- [X] Record animations
- [ ] Add multimonitor support
- [X] Add selectable screenregion
- [ ] Performance improvements
- [ ] Change output path

***

## How to use it in your solution
Download this repository and build it.  
Add a reference to the built DotScreencap.dll in your solution.  
Add `using DotScreencap;` to your project files.

***

## Final word
If you like the project please **star it** in order to help to spread the word. That way you will make the framework more significant and in the same time you will motivate me to improve it, so the benefit is mutual.
