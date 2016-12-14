using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Threading;

public class NativePlugin : MonoBehaviour
{

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void saveCallbackParallel(Action callback);

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void loopThread();

    [DllImport("nativeplugin", CallingConvention = CallingConvention.StdCall)]
    public static extern void cleanup();




    // Use this for initialization
    void Start ()
    {
        //Callbacks triggered by a native thread -> Debugger crashes after the callback
        saveCallbackParallel(CallbackParallel);

        Debug.Log("Start a native thread that calls the callback, then loops and waits forever");
        //starts a thread that calls the callback once
        //then loops loops forever using while(true) Sleep(100);
        loopThread();

        //do not stop the thread
        //this is usually no problem but if it called a C# callback before then
        //the thread won't be terminated on shutdown instead mono waits
        //forever for it to quit (assumed)

        //uncommend this and the error will be gone.
        //cleanup();
        

    }

    // Update is called once per frame
    void Update () {
	
	}
    
    public void CallbackParallel()
    {
        Debug.Log("C++ called CallbackParallel!");
    }
}
