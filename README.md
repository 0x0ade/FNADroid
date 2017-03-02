# FNADroid
### FNA for Android, the ugly way
#### MS-PL-licensed
#### clone recursively
----

#### TL;DR:
* Run `/FNADroid-Native/buildnative.sh`
* Compile `/SDL2Droid-CS/SDL2Droid-CS-Java/` into `/SDL2Droid-CS/SDL2Droid-CS-JBindings/Jars/SDL2Droid-CS-Java.jar`
* Include your game as a dependency to the `FNADroid` project so it gets included in the .apk properly.
* Change `/FNADroid/Bootstrap.cs`, `/FNADroid/Resources/` and `/FNADroid/MainActivity.cs`
* Compile FNADroid using Xamarin.Android (f.e. via Xamarin for Visual Studio).
* Uh... I haven't figured out content management quite yet... *runs away*

#### /SDL2Droid-CS/

*What:* SDL2 for Android. Take a look at https://github.com/0x0ade/SDL2Droid-CS

*Why:* FNA uses SDL2 and SDL2-CS; FNADroid uses SDL2 and SDL2Droid-CS

*Compilation:* Only the SDL2 bindings require manual compilation:  
In your favourite Java IDE, produce a compiled .jar artifact and place it into `/SDL2Droid-CS/SDL2Droid-CS-JBindings/Jars/`

#### /FNADroid-Native/

*What:* Native library `Android.mk` makefiles, extension code and prebuilt libraries

*Why:* Xamarin for Visual Studio has got some problems with compiling native libraries on its own.

*Compilation:* Run `buildnative.sh` (cygwin-compatible) from inside `/FNADroid-Native/`. That's it.

#### /FNADroid/ and /FNADroid-Lib/

*What:* The Mono side of things: Main C# code starting the game and (-Lib) optional extension library to be used by the game.

*Why:* Why not?

*Compilation:*
* Load the solution in Visual Studio with the "Xamarin for Visual Studio" extension installed.
* Modify `/FNADroid/Bootstrap.cs` for your main code, `/SDL2Droid-CS/Resources/` for any strings / icons / ... and `/FNADroid/MainActivity.cs` for the launch config (f.e. orientation).
* Compile as you would any other Xamarin.Android project.