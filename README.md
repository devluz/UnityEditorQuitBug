# UnityDebuggerBug
Demonstration of a bug in Unity. Calling callbacks from native threads into C# will crash the unity debugger and unity editor.

To reproduce the bug:

1. Open the unity project
2. start the "startscene"
3. Either attach the debugger at the start and walk trough the NativePlugin.cs or run the example and attach the debugger after
it and press the pause buggon
4. Unity and visual studio will crash
