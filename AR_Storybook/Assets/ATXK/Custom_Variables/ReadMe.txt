Custom Variables
========================================
Includes:
- CV_Base.cs
- CV_Int.cs
- CV_Float.cs
- CV_Double.cs
- CV_String.cs
- TestDamage.cs
- Player.cs
- HealthBar.cs

CV_Base
- Abstract base class based on ScriptableObject containing the 3 functions that all Custom Variables require
	- Reset() just resets the runtime value back to the initial value.
	- OnBeforeSerialize() and OnAfterDeserialize() are functions needed from the ISerializationCallbackReceiver interface.

CV_Int
- Custom variable containing an integer.
- Has an Initial value that is set in the Inspector, which is what the Runtime value will always default to.

CV_Float
- Custom variable containing a float.
- Has an Initial value that is set in the Inspector, which is what the Runtime value will always default to.

CV_Double
- Custom variable containing a double.
- Has an Initial value that is set in the Inspector, which is what the Runtime value will always default to.

CV_String 
- Custom variable containing a string.
- Has an Initial value that is set in the Inspector, which is what the Runtime value will always default to.

TestDamage
- Sample script to deal damage to the Player.
- Backspace to deal damage, R to reset the player health.

Player
- Sample script that references a CV_Int object as the player's health.

HealthBar
- Sample script that references Player's health CV_Int and displays it on-screen.