using UnityEngine;
using System.IO;

public class AppControllerMod 
{
    public static void UpdateUnityAppController(string path)
    {
        const string filePath = "Classes/UnityAppController.mm";
        string fullPath = Path.Combine(path, filePath);
        
        var reader = new StreamReader(fullPath);
        string textAppController = reader.ReadToEnd();
        reader.Close();
        
        int fixupStart = textAppController.IndexOf("openURL:", System.StringComparison.Ordinal);
        if(fixupStart <= 0)
            return;
        int fixupMid = textAppController.IndexOf("return", fixupStart);
        if(fixupMid <= 0)
            return;
        int fixupEnd = textAppController.IndexOf(';', fixupMid);
        if(fixupEnd <= 0)
            return;
        
        string fixedAppController = textAppController.Substring(0, fixupMid);

		if (textAppController.IndexOf("AppotaGameSDK handleOpenURL", System.StringComparison.Ordinal) <= 0){
			// Base on kind of SDK
			if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "[AppotaGameSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];\n[OCSDKConfigure handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];\nreturn true;";
			}
			else if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "return [AppotaGameSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];";
			}
			else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "return [OCSDKConfigure handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];";
			}

			fixedAppController += textAppController.Substring(fixupEnd+1);
		}
		else {
			fixedAppController += textAppController.Substring(fixupMid+1);
		}

		// Add callback register Push Notification with Token data
		int regPosStart = fixedAppController.IndexOf("didRegisterForRemoteNotificationsWithDeviceToken", System.StringComparison.Ordinal);
		if (regPosStart <= 0)
			return;
		int regPosEnd = fixedAppController.IndexOf("{", regPosStart);
		if (regPosEnd <= 0)
			return;

		string tempString = fixedAppController.Substring(0, regPosEnd);

		if (fixedAppController.IndexOf("AppotaGameSDK configurePushNotificationWithTokenData", System.StringComparison.Ordinal) <= 0){
			tempString += "{\n[AppotaGameSDK configurePushNotificationWithTokenData:deviceToken];";
		}

		tempString += fixedAppController.Substring(regPosEnd+1);
		fixedAppController = tempString;

		// Add header import
		int finalFixupStart = fixedAppController.IndexOf("#import <OpenGLES/EAGL.h", System.StringComparison.Ordinal);
		if(finalFixupStart <= 0)
			return;
		int finalFixupEnd = fixedAppController.IndexOf(">", finalFixupStart);
		if(finalFixupEnd <= 0)
			return;
        
		string finalFixedAppController = fixedAppController.Substring(0, finalFixupStart);

		if ( fixedAppController.IndexOf("AppotaSDK.h", System.StringComparison.Ordinal) <= 0 || fixedAppController.IndexOf("OnClanSDK.h", System.StringComparison.Ordinal) <= 0){
			// Base on kind of SDK
			if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <AppotaSDK/AppotaSDK.h>\n#import <OnClanSDK/OCSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
			else if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <AppotaSDK/AppotaSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
			else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <OnClanSDK/OCSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
		}

		finalFixedAppController += fixedAppController.Substring(finalFixupEnd+1);

        var writer = new StreamWriter(fullPath, false);
		writer.Write(finalFixedAppController);
        writer.Close();
        
    }

}
