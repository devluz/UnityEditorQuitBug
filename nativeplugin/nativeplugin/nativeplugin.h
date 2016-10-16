// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the NATIVEPLUGIN_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// NATIVEPLUGIN_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef NATIVEPLUGIN_EXPORTS
#define NATIVEPLUGIN_API __declspec(dllexport)
#else
#define NATIVEPLUGIN_API __declspec(dllimport)
#endif


extern "C"
{
	NATIVEPLUGIN_API int normalCall(void);


	NATIVEPLUGIN_API void saveCallback(void(*callback)());
	NATIVEPLUGIN_API void callCallback();

	NATIVEPLUGIN_API void saveCallbackParallel(void(*callback)());
	NATIVEPLUGIN_API void callCallbackParallel();

	NATIVEPLUGIN_API void cleanup();
}
