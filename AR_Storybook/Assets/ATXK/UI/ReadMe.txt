UI_System
========================================
Includes:
- UI_Manager.cs
- UI_Screen.cs

UI_Manager
- Is a manager for all User Interface elements within a scene.
- Has functions to toggle between screens and to toggle the global screen fader.
	- ToggleScreens(UI_Screen screen) switches out the current screen to the input variable, and keeps a reference to the old current screen.
	- ToggleFader() toggles the global fade on/off with a tween animation for the alpha/transparency.

UI_Screen
- Is a class for all UI screens in a canvas, ie any screens that will show up to the player (fullscreen/pop-up) such as Main menu, Options menu, Pause menu, etc.