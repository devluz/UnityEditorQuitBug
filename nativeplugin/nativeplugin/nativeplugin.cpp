// nativeplugin.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "nativeplugin.h"




// This is an example of an exported function.
NATIVEPLUGIN_API int normalCall(void)
{
	return 42;
}

void (*ptrCallback)();

NATIVEPLUGIN_API void saveCallback(void(*callback)())
{
	ptrCallback = callback;
}
NATIVEPLUGIN_API void callCallback()
{
	ptrCallback();
}


void(*ptrCallbackParallel)();

NATIVEPLUGIN_API void saveCallbackParallel(void(*callbackParallel)())
{
	ptrCallbackParallel = callbackParallel;
}

HANDLE threadHandle = NULL;
DWORD WINAPI ThreadStart(LPVOID lpParam)
{
	ptrCallbackParallel();
	return 0;
}
NATIVEPLUGIN_API void callCallbackParallel()
{
	//ptrCallbackParallel();

	threadHandle = CreateThread(
		NULL,                   // default security attributes
		0,                      // use default stack size  
		ThreadStart,       // thread function name
		nullptr,          // argument to thread function 
		0,                      // use default creation flags 
		nullptr);
}

NATIVEPLUGIN_API void cleanup()
{
	if (threadHandle != NULL)
	{
		CloseHandle(threadHandle);
		threadHandle = NULL;
	}
}

