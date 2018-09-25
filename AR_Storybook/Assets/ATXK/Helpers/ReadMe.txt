Helpers
========================================
Includes:
- SingletonBehaviour.cs
- SingletonBehaviourless.cs
- DebugLogger.cs

SingletonBehaviour
- Is a generic Singleton base class for scene objects that require the MonoBehaviour attribute.

SingletonBehaviourless
- Is a generic Singleton base class for general objects that do not require the MonoBehaviour attribute.

DebugLogger
- Is a custom Debug logger that creates standardized Debug/Warning/Error/Assertion logs to Unity Console.
- Appends the name of the class calling the debug log at the start for easier debugging of where each debug log originates from.