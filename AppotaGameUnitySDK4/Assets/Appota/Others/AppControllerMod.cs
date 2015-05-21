using UnityEngine;
using System.IO;

public class AppControllerMod 
{
    public static void UpdateUnityAppController(string path)
    {
        const string filePath = "Classes/UnityAppController.mm";
        string fullPath = Path.Combine(path, filePath);
        
        var reader = new StreamReader(fullPath);
        string AppControllerSource = reader.ReadToEnd();
        reader.Close();
        
		// Add header import
		AppControllerSource = AddHeaderImportFramework(AppControllerSource);

		// Add Appota Handler Callback
		AppControllerSource = AddAppotaHandlerCallback(AppControllerSource);

		// Add callback register Push Notification
		AppControllerSource = AddCallbackRegisterPushNotifications(AppControllerSource);

		// Call Facebook activeApp() inside applicationDidBecomeActive function 
		AppControllerSource = AddFacebookAppEvents(AppControllerSource);

		// Add callback register Push Notification with Token data
		AppControllerSource = AddCallbackRegisterPushNotificationWithTokenData(AppControllerSource);

		if (AppotaSetting.UsingAppFlyer) 
			AppControllerSource = AddAppFlyerConfigure(AppControllerSource);

		if (AppotaSetting.UsingAdWords)
			AppControllerSource = AddAdWordsConfigure(AppControllerSource);

        var writer = new StreamWriter(fullPath, false);
		writer.Write(AppControllerSource);
        writer.Close();
        
    }

	private static string AddAppotaHandlerCallback(string AppControllerSource) {
		int fixupStart = AppControllerSource.IndexOf("openURL:", System.StringComparison.Ordinal);
		if(fixupStart <= 0)
			return AppControllerSource;
		int fixupMid = AppControllerSource.IndexOf("return", fixupStart);
		if(fixupMid <= 0)
			return AppControllerSource;
		int fixupEnd = AppControllerSource.IndexOf(';', fixupMid);
		if(fixupEnd <= 0)
			return AppControllerSource;
		
		string fixedAppController = AppControllerSource.Substring(0, fixupMid);
		
		if (AppControllerSource.IndexOf("AppotaGameSDK handleOpenURL", System.StringComparison.Ordinal) <= 0){
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
		}
		fixedAppController += AppControllerSource.Substring(fixupEnd+1);
		return fixedAppController;
	}

	private static string AddHeaderImportFramework(string AppControllerSource) {
		int finalFixupStart = AppControllerSource.IndexOf("#import <OpenGLES/EAGL.h", System.StringComparison.Ordinal);
		if(finalFixupStart <= 0)
			return AppControllerSource;
		int finalFixupEnd = AppControllerSource.IndexOf(">", finalFixupStart);
		if(finalFixupEnd <= 0)
			return AppControllerSource;
		
		string finalFixedAppController = AppControllerSource.Substring(0, finalFixupStart);
		
		if ( AppControllerSource.IndexOf("AppotaSDK.h", System.StringComparison.Ordinal) <= 0 || AppControllerSource.IndexOf("OnClanSDK.h", System.StringComparison.Ordinal) <= 0){
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
			
			// Import Facebook header
			finalFixedAppController += "\n#import <FacebookSDK/FacebookSDK.h>";
		}
		
		finalFixedAppController += AppControllerSource.Substring(finalFixupEnd+1);
		return finalFixedAppController;
	}

	private static string AddCallbackRegisterPushNotifications(string AppControllerSource) {
		// Add callback register Push Notification
		int notiPosStart = AppControllerSource.IndexOf("didReceiveRemoteNotification", System.StringComparison.Ordinal);
		if (notiPosStart <= 0)
			return AppControllerSource;
		int notiPosEnd = AppControllerSource.IndexOf("{", notiPosStart);
		if (notiPosEnd <= 0)
			return AppControllerSource;
		
		string pushNotiString = AppControllerSource.Substring(0, notiPosEnd);
		
		if (AppControllerSource.IndexOf("AppotaGameSDK handlePushNotification", System.StringComparison.Ordinal) <= 0){
			pushNotiString += "{\n\t[AppotaGameSDK handlePushNotification:userInfo];";
		}
		
		pushNotiString += AppControllerSource.Substring(notiPosEnd+1);
		return pushNotiString;
	}

	private static string AddCallbackRegisterPushNotificationWithTokenData(string AppControllerSource) {
		int regPosStart = AppControllerSource.IndexOf("didRegisterForRemoteNotificationsWithDeviceToken", System.StringComparison.Ordinal);
		if (regPosStart <= 0)
			return AppControllerSource;
		int regPosEnd = AppControllerSource.IndexOf("{", regPosStart);
		if (regPosEnd <= 0)
			return AppControllerSource;
		
		string tempString = AppControllerSource.Substring(0, regPosEnd);
		
		if (AppControllerSource.IndexOf("AppotaGameSDK configurePushNotificationWithTokenData", System.StringComparison.Ordinal) <= 0){
			tempString += "{\n\t[AppotaGameSDK configurePushNotificationWithTokenData:deviceToken];";
		}
		
		tempString += AppControllerSource.Substring(regPosEnd+1);
		return tempString;
	}

	private static string AddFacebookAppEvents(string AppControllerSource) {
		int fbActivePosStart = AppControllerSource.IndexOf("applicationDidBecomeActive", System.StringComparison.Ordinal);
		if (fbActivePosStart <= 0)
			return AppControllerSource;
		int fbActivePosEnd = AppControllerSource.IndexOf("{", fbActivePosStart);
		if (fbActivePosEnd <= 0)
			return AppControllerSource;
		
		string fbActiveString = AppControllerSource.Substring(0, fbActivePosEnd);
		
		if (AppControllerSource.IndexOf("FBAppEvents activateApp", System.StringComparison.Ordinal) <= 0){
			fbActiveString += "{\n\t[FBAppEvents activateApp];";
		}
		
		fbActiveString += AppControllerSource.Substring(fbActivePosEnd+1);
		return fbActiveString;
	}

	private static string AddAppFlyerConfigure(string AppControllerSource) {
		// Add header import
		Debug.Log("Add AppFlyer Configure");
		string finalAppController = "";

		int fixupStart = AppControllerSource.IndexOf("#import <OpenGLES/EAGL.h", System.StringComparison.Ordinal);
		if(fixupStart <= 0)
			return AppControllerSource;
		int fixupEnd = AppControllerSource.IndexOf(">", fixupStart);
		if(fixupEnd <= 0)
			return AppControllerSource;
		
		string fixedAppController = AppControllerSource.Substring(0, fixupStart);
		
		if ( AppControllerSource.IndexOf("AppsFlyerTracker.h", System.StringComparison.Ordinal) <= 0){
			fixedAppController += "\n#import \"AppsFlyerTracker.h\"\n#import <OpenGLES/EAGL.h>";
		}
		
		fixedAppController += AppControllerSource.Substring(fixupEnd+1);
		finalAppController = fixedAppController;

		// Add configure 
		int regPosStart = finalAppController.IndexOf("didFinishLaunchingWithOptions", System.StringComparison.Ordinal);
		if (regPosStart <= 0)
			return AppControllerSource;
		int regPosEnd = finalAppController.IndexOf("{", regPosStart);
		if (regPosEnd <= 0)
			return AppControllerSource;
		
		string tempString = finalAppController.Substring(0, regPosEnd);
		
		if (finalAppController.IndexOf("AppsFlyerTracker sharedTracker", System.StringComparison.Ordinal) <= 0){
			tempString += "{\n\t[AppsFlyerTracker sharedTracker].appleAppID = @\"" + AppotaSetting.AppleAppID + "\";";
			tempString += "\n\t[AppsFlyerTracker sharedTracker].appsFlyerDevKey = @\"" + AppotaSetting.AppFlyerKey + "\";";
		}
		
		tempString += finalAppController.Substring(regPosEnd+1);
		finalAppController = tempString;

		// Add track install 
		int posStart = finalAppController.IndexOf("applicationDidBecomeActive", System.StringComparison.Ordinal);
		if (posStart <= 0)
			return AppControllerSource;
		int posEnd = finalAppController.IndexOf("{", posStart);
		if (posEnd <= 0)
			return AppControllerSource;
		
		string trackString = finalAppController.Substring(0, posEnd);
		
		if (finalAppController.IndexOf("[AppsFlyerTracker sharedTracker] trackAppLaunch", System.StringComparison.Ordinal) <= 0){
			trackString += "{\n\t[[AppsFlyerTracker sharedTracker] trackAppLaunch];";
		}
		
		trackString += finalAppController.Substring(posEnd+1);
		finalAppController = trackString;

		return finalAppController;
	}

	private static string AddAdWordsConfigure(string AppControllerSource) {
		// Add header import
		Debug.Log("Add AdWords Configure");
		string finalAppController = "";
		
		int fixupStart = AppControllerSource.IndexOf("#import <OpenGLES/EAGL.h", System.StringComparison.Ordinal);
		if(fixupStart <= 0)
			return AppControllerSource;
		int fixupEnd = AppControllerSource.IndexOf(">", fixupStart);
		if(fixupEnd <= 0)
			return AppControllerSource;
		
		string fixedAppController = AppControllerSource.Substring(0, fixupStart);
		
		if ( AppControllerSource.IndexOf("ACTReporter.h", System.StringComparison.Ordinal) <= 0){
			fixedAppController += "\n#import \"ACTReporter.h\"\n#import <OpenGLES/EAGL.h>";
		}
		
		fixedAppController += AppControllerSource.Substring(fixupEnd+1);
		finalAppController = fixedAppController;
		
		// Add configure 
		int regPosStart = finalAppController.IndexOf("didFinishLaunchingWithOptions", System.StringComparison.Ordinal);
		if (regPosStart <= 0)
			return AppControllerSource;
		int regPosEnd = finalAppController.IndexOf("{", regPosStart);
		if (regPosEnd <= 0)
			return AppControllerSource;
		
		string tempString = finalAppController.Substring(0, regPosEnd);
		
		if (finalAppController.IndexOf("ACTConversionReporter reportWithConversionID", System.StringComparison.Ordinal) <= 0){
			string isRepeatable = AppotaSetting.AdWordsIsRepeatable?"YES":"NO";
			tempString += "{\n\t[ACTAutomatedUsageTracker enableAutomatedUsageReportingWithConversionID:@\"" + AppotaSetting.AdWordsConversionID +"\"];" +
				"\n\t[ACTConversionReporter reportWithConversionID:@\"" + AppotaSetting.AdWordsConversionID 
				+ "\" label:@\"" + AppotaSetting.AdWordsLabel + "\" value:@\"" + AppotaSetting.AdWordsValue + "\" isRepeatable:" 
				+ isRepeatable + "];";
		}
		
		tempString += finalAppController.Substring(regPosEnd+1);
		finalAppController = tempString;
		return finalAppController;
	}
}
