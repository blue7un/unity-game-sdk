using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor.Appota;
#endif
using System.IO;
using Appota;

public static class XCodePostProcess
{

#if UNITY_EDITOR
	[PostProcessBuild(999)]
	public static void OnPostProcessBuild( BuildTarget target, string pathToBuiltProject )
	{
		BuildTarget _target;
#if UNITY_5
		_target = BuildTarget.iOS;
#else
		_target = BuildTarget.iPhone;
#endif
		if (target == _target) {
            
            // Create a new project object from build target
			XCProject project = new XCProject( pathToBuiltProject );
			
			// Find and run through all projmods files to patch the project.
			// Please pay attention that ALL projmods files in your project folder will be excuted!
			string[] files = Directory.GetFiles( Application.dataPath, "*.projmods", SearchOption.AllDirectories );
			foreach( string file in files ) {
				UnityEngine.Debug.Log("ProjMod File: "+file);
				project.ApplyMod( file );
			}
			
			// Config while building project
			PlistMod.UpdatePlist(pathToBuiltProject, AppotaSetting.InAppApiKey, AppotaSetting.FacebookAppID, AppotaSetting.FacebookAppLinkUrl, AppotaSetting.TwitterConsumerKey, AppotaSetting.TwitterConsumerSecret, AppotaSetting.GoogleClientId, AppotaSetting.GoogleClientSecretId);
			AppControllerMod.UpdateUnityAppController(pathToBuiltProject);
			
			//TODO implement generic settings as a module option
			project.overwriteBuildSetting("CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Distribution", "Release");
			
			// Finally save the xcode project
			project.Save();

		}
		else if (target == BuildTarget.WP8Player){
			string[] s = Application.dataPath.Split('/');
			string projectName = s[s.Length - 2];
			projectName = projectName.Replace(" ", "");

			MainXAMLMod.UpdateMainPageXAML(pathToBuiltProject + "/" + projectName + "/MainPage.xaml.cs");
			
			// Copy library files 
			if(!File.Exists(pathToBuiltProject + "/" + projectName + "/AppotaLib/APTPaymentService.dll"))
   				FileUtil.CopyFileOrDirectory (Application.dataPath + "/Plugins/WP8/APTPaymentService.dll", pathToBuiltProject + "/" + projectName + "/AppotaLib/APTPaymentService.dll");
			if(!File.Exists(pathToBuiltProject + "/" + projectName + "/AppotaLib/Hammock.WindowsPhone.dll"))
				FileUtil.CopyFileOrDirectory (Application.dataPath + "/Plugins/WP8/Hammock.WindowsPhone.dll", pathToBuiltProject + "/" + projectName + "/AppotaLib/Hammock.WindowsPhone.dll");
			if(!File.Exists(pathToBuiltProject + "/" + projectName + "/AppotaLib/ICSharpCode.SharpZipLib.WindowsPhone.dll"))
				FileUtil.CopyFileOrDirectory (Application.dataPath + "/Plugins/WP8/ICSharpCode.SharpZipLib.WindowsPhone.dll", pathToBuiltProject + "/" + projectName + "/AppotaLib/ICSharpCode.SharpZipLib.WindowsPhone.dll");
			if(!File.Exists(pathToBuiltProject + "/" + projectName + "/AppotaLib/PayPal.Checkout.SDK-WindowsPhone8.dll"))
				FileUtil.CopyFileOrDirectory (Application.dataPath + "/Plugins/WP8/PayPal.Checkout.SDK-WindowsPhone8.dll", pathToBuiltProject + "/" + projectName + "/AppotaLib/PayPal.Checkout.SDK-WindowsPhone8.dll");
			
		}
		else {
			Debug.LogWarning("Target is not iPhone or WP8. XCodePostProcess will not run");
			return;
		}

	}
#endif

	public static void Log(string message)
	{
		UnityEngine.Debug.Log("PostProcess: "+message);
	}
}
