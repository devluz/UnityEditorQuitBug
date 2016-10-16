using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Threading;

public class NativePlugin : MonoBehaviour {


    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern int normalCall();

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void saveCallback(Action callback);

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void callCallback();



    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void saveCallbackParallel(Action callback);

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void callCallbackParallel();

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void cleanup();

    private static bool mCallbackCalled = false;
    private static bool mCallbackParallelCalled = false;
    // Use this for initialization
    void Start () {

        //Normal call to native function is no problem
        Debug.Log("Test call to a native function (should work):");
        int val = normalCall();
        if(val == 42)
        {
            Debug.Log("Worked");
        }
        else
        {
            Debug.LogError("Failed");
        }

        //Callbacks are no problem too!
        Debug.Log("Saving and calling a callback(should work):");
        saveCallback(Callback);
        callCallback();
        if(mCallbackCalled)
        {
            Debug.Log("Worked");
        }
        else
        {
            Debug.LogError("Failed");
        }
         
        //Callbacks triggered by a native thread -> Debugger crashes after the callback
        saveCallbackParallel(CallbackParallel);
        Debug.Log("Calling a callback in parallel thread. After this callback is called the debugger will crash." +
            "Without debugger this will run just fine!");
        callCallbackParallel();
        //take a nap for a second and wait for the parallel callback to get trough
        //debugger will crash after this line but without debugger everything works just fine!
        Thread.Sleep(1000);
        if (mCallbackParallelCalled)
        {
            Debug.Log("Worked");
        }
        else
        {
            Debug.LogError("Failed");
        }
        cleanup();
        //Even after cleanup the debugger will still crash
        Debug.Log("Test done. Thread ran trough and thread handle is closed. Attaching the debugger and pressing the pause button"
            + " will still crash and freeze unity! This state persists until unity editor is restarted.");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Callback()
    {
        mCallbackCalled = true;
        Debug.Log("C++ called Callback!");
    }
    public void CallbackParallel()
    {
        mCallbackParallelCalled = true;
        Debug.Log("C++ called CallbackParallel!");
    }
}
