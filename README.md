# UnityDebuggerBug
Demonstration of a bug in Unity. Calling callbacks from native threads into C# and then keeping that thread running in idle will freeze Unity on quit.

To reproduce the bug:

1. Open the unity project
2. start the "startscene"
3. Run the game. It will start a parallel thread that calls the callback once and then loops forever in a loop: while(true) Sleep(100);
4. Quit Unity Editor (or stop the game first, then quit)

The editor will get stuck and probably wait for ever for the native thread to quit.
The bug seems to be very similar to https://github.com/devluz/UnityEditorQuitBug
