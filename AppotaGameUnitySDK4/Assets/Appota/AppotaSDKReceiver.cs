using UnityEngine;
using System.Collections;
using SimpleJSON;

public class AppotaSDKReceiver : MonoBehaviour {
	private static GameObject playGameObject;
	private static bool initialized;

	private static AppotaSDKReceiver _instance;
	
	// Singleton for SDK handler
	public static AppotaSDKReceiver Instance
	{
		get
		{
			if(_instance == null) _instance = new AppotaSDKReceiver();
			return _instance;
		}
	}
	
	public static void InitializeGameObjects()
	{
		if(!initialized)
		{
			playGameObject = new GameObject("AppotaSDKReceiver");
			playGameObject.AddComponent(typeof(AppotaSDKReceiver));
			//keep this game object around for all scenes
			DontDestroyOnLoad(playGameObject);
			initialized = true;
		}
	}
	
	public void OnLoginSuccess(string appotaSession)
	{
		// Get User info from AppotaSession
		AppotaSession appotaSessionObj = new AppotaSession(appotaSession);
		AppotaSession.Instance.UpdateInstance(appotaSessionObj);

		Debug.Log ("AppotaSDK: Did login");
	}
	
	public void OnLoginError(string error)
	{
		Debug.Log ("AppotaSDK: Login Error: " + error);
	}

	public void OnLogoutSuccess()
	{ 
		Debug.Log ("AppotaSDK: Did logout");
	}
	
	public void OnPaymentSuccess(string transactionResult)
	{
		// Parse Transaction result into class AppotaPaymentResult.cs
		AppotaPaymentResult paymentResult = new AppotaPaymentResult(transactionResult);

		// Parse amount, packageID, in AppPaymentResult
		Debug.Log ("AppotaSDK: Did payment");
		Debug.Log("Appota: " + transactionResult);
	}

	public void OnPaymentFailed(string error)
	{
		Debug.Log ("AppotaSDK: Payment Error: " + error);
	}

	public void OnCloseLoginView()
	{
		Debug.Log ("AppotaSDK: Close Login View");
	}
	
	public void GetPaymentState(string packageID)
	{
		Debug.Log ("AppotaSDK: Get Payment State");
		string paymentState = packageID;

		AppotaSDKHandler.Instance.SendStateToWrapper(paymentState);
	}

}
