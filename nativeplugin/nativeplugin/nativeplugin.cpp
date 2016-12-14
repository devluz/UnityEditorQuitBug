// nativeplugin.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "nativeplugin.h"




HANDLE threadHandle = NULL;

void(*ptrCallbackParallel)();

NATIVEPLUGIN_API void saveCallbackParallel(void(*callbackParallel)())
{
	ptrCallbackParallel = callbackParallel;
}

DWORD WINAPI ThreadStartLoop(LPVOID lpParam)
{
	ptrCallbackParallel();

	//loop and sleep.
	while (true)
	{
		Sleep(100);
	}
	return 0;
}

NATIVEPLUGIN_API void loopThread(void)
{
	threadHandle = CreateThread(
		NULL,                   // default security attributes
		0,                      // use default stack size  
		ThreadStartLoop,       // thread function name
		nullptr,          // argument to thread function 
		0,                      // use default creation flags 
		nullptr);
}
NATIVEPLUGIN_API void cleanup()
{
	if (threadHandle != NULL)
	{
		TerminateThread(threadHandle, 0);
		CloseHandle(threadHandle);
		threadHandle = NULL;
	}
}

