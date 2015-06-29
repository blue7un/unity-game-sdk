using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class AppotaThreadHandler : MonoBehaviour {

	private Thread callbackThread;
	private AndroidJavaClass cls_MessageFromSDK;
	private AndroidJavaObject activityContext = null;
	private string lastPidTime = "";
	private static bool canCallbackThreadRun = false;

	private static AppotaThreadHandler _instance;
	
	// Singleton for Appota Thread handler
	public static AppotaThreadHandler Instance
	{
		get
		{
			if(_instance == null) _instance = new AppotaThreadHandler();
			return _instance;
		}
	}

	// When AppotaSDK has been started, Android will start a new activity and Unity activity would be paused. 
	// So we could not callback data to Unity activity. New thread and runOnUiThread will solve this problem. 
	public void Start() 
	{
		Debug.Log("AppotaSDK: Starting callback thread");
		AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		activityContext = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
		canCallbackThreadRun = true;
		
		callbackThread = new Thread(new ThreadStart(LoopCallbackThread));
		callbackThread.Start();
	}
	
	// Make a loop on UI Thread to call AndroidJNI functions
	void LoopCallbackThread() {
		int attachThread = AndroidJNI.AttachCurrentThread();
		if (attachThread != 0){
			Debug.Log("AppotaSDK: Attach thread failed");
			return;
		}
		
		while (true) {
			if (!canCallbackThreadRun){
				AndroidJNI.DetachCurrentThread();
				break;
			}
			
			try {
				activityContext.Call("runOnUiThread", new AndroidJavaRunnable(GetPaymentStateFromThread));
			}
			catch (AndroidJavaException e) {
				Debug.Log("AppotaSDK: " + e.ToString());
			} 
			catch (Exception e) {
				Debug.Log("AppotaSDK: " + e.ToString());
			}
			
			Thread.Sleep(1000);
		}
	}
	
	// Receive packageID and send PaymentState to AppotaSDK
	void GetPaymentStateFromThread() {
		cls_MessageFromSDK = new AndroidJavaClass("com.appota.gamesdk.v4.commons.Messages"); 
		string pid = cls_MessageFromSDK.GetStatic<string>("pid");
		if (pid.Equals("") || pid == null)
			return;
		
		// @pid: "packageID" + "@" + "unixtime"
		string packageID = pid.Split('@')[0];
		string pidTime = pid.Split('@')[1];
		
		if (!pidTime.Equals(lastPidTime)) {
			lastPidTime = pidTime;
			AppotaSDKReceiver.Instance.GetPaymentState(packageID);
		}
	}
	
	public void Stop() 
	{
		canCallbackThreadRun = false;
	}
}
