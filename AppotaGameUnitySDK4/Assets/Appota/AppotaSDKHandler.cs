using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Text;

public class AppotaSDKHandler {
	
	private static AppotaSDKHandler _instance;
	
	// Singleton for SDK handler
	public static AppotaSDKHandler Instance
	{
		get
		{
			if(_instance == null) _instance = new AppotaSDKHandler();
			return _instance;
		}
	}
	
	#if UNITY_IPHONE
	
	// SDK functions
	[DllImport("__Internal")]
	private static extern void init();
	
	[DllImport("__Internal")]
	private static extern void setSDKButtonVisibility(bool isVisible);
	
	[DllImport("__Internal")]
	private static extern void setKeepLoginSession(bool isKeepLoginSession);

	// Notification functions
	[DllImport("__Internal")]
	private static extern bool setHideWelcomeView(bool isHideWelcomeView);
	
	[DllImport("__Internal")]
	private static extern void inviteFacebookFriends();
	
	// User Functions
	[DllImport("__Internal")]
	private static extern void setAutoShowLogin(bool autoShowLogin);
	
	[DllImport("__Internal")]
	private static extern void showUserInfoView();

	[DllImport("__Internal")]
	private static extern void showRegisterView();

	[DllImport("__Internal")]
	private static extern void showTransactionHistory();

	[DllImport("__Internal")]
	private static extern void showLoginView();
	
	[DllImport("__Internal")]
	private static extern void showGoogleLogin();
	
	[DllImport("__Internal")]
	private static extern void showFacebookLogin();
	
	[DllImport("__Internal")]
	private static extern void showTwitterLogin();
	
	[DllImport("__Internal")]
	private static extern void switchAccount();
	
	[DllImport("__Internal")]
	private static extern void logout();
	
	[DllImport("__Internal")]
	private static extern bool isUserLoggedIn();
	
	// Payment functions
	[DllImport("__Internal")]
	private static extern void showPaymentView();
	
	[DllImport("__Internal")]
	private static extern void showPaymentViewWithPackageID(string packageID);
	
	[DllImport("__Internal")]
	private static extern void sendStateToWrapper(string state);
	
	[DllImport("__Internal")]
	private static extern void setCharacter(string characterName, string characterID, string serverName, string serverID);
	
	[DllImport("__Internal")]
	private static extern void closePaymentView();
	
	// Track functions
	[DllImport("__Internal")]
	private static extern void sendEventWithCategoryWithValue(string category, string action, string label, int value);

	[DllImport("__Internal")]
	private static extern void sendEventWithCategory(string category, string action, string label);
	
	[DllImport("__Internal")]
	private static extern void sendViewWithName(string name);
	
	// Notification functions
	[DllImport("__Internal")]
	private static extern bool registerPushNotificationWithGroupName(string name);

	// Facebook App Event functions 
	[DllImport("__Internal")]
	private static extern void fbLogEvent(string name);

	[DllImport("__Internal")]
	private static extern void fbLogEventWithParameter(string name, double value, string parameters);

	#region SDK functions
	/*
	 * Call this function in your first scene or when you want user to login
	 * */
	public void Init(){
		AppotaSDKReceiver.InitializeGameObjects ();

		init();
		Debug.Log("Called init iOS ");
	}
	
	/*
	 * Call this function to setting hide or show SDK floating button
	 * */
	public void SetSDKButtonVisibility(bool isVisible){
		setSDKButtonVisibility(isVisible);
	}
	
	/*
	 * This function will control the Appota Login Session will be kept or removed at app lauching
	 * (when session's removed user has to login again when app start). 
	 * */
	public void SetKeepLoginSession(bool isKeepLoginSession){
		setKeepLoginSession(isKeepLoginSession);
	}
	
	/*
	 * This function will control the Appota Login View will be automatically show at app lauching (when user's not logged in)
	 * Or you have to call [ShowLoginView](#show-login-view) function to show the LoginView.
	 * */
	public void SetAutoShowLoginDialog(bool autoShowLogin) {
		setAutoShowLogin(autoShowLogin);
	}

	/*
	 * Call this function in `OnApplicationQuit` script
	 * */
	public void FinishSDK()
	{
		
	}
	#endregion
	
	#region User functions
	
	/*
	 * Show login view function (when user's not logged in)
	 * */
	public void ShowLoginView()
	{
		showLoginView();
	}
	
	/*
	 * Logout function
	 * */
	public void Logout() {
		Debug.Log ("Start logout");
		logout();
	}
	
	/*
	 * Call this function when you want to switch logged in user to other Appota User. 
	 * Remember to check callback after switching successful (it'll be called in OnLoginSucceed again)
	 * */
	public void SwitchAccount()
	{
		switchAccount();
	}
	
	/*
	 * This function will show register view  
	 * */
	public void ShowRegisterView()
	{
		showRegisterView();
	}

	/*
	 * This function will show transaction history view  
	 * */
	public void ShowTransactionHistory()
	{
		showTransactionHistory();
	}

	/*
	 * This function will show user profile view  
	 * */
	public void ShowUserInfoView()
	{
		showUserInfoView();
	}
	
	/*
	 * This function will return user logged in state
	 * */
	public bool IsUserLoggedIn() {
		return isUserLoggedIn();
	}
	
	/*
	 * Return AppotaSession if logged in
	 * */
	public AppotaSession GetAppotaSession() {
		return AppotaSession.Instance;
	}
	
	/*
	 * Set character function to support character management on web
	 * */
	public void SetCharacter(string characterName, string characterID, string serverName, string serverID) {
		setCharacter(characterName, characterID, serverName, serverID);
	}
	
	public void ShowFacebookLogin()
	{
		showFacebookLogin();
	}
	
	public void ShowGoogleLogin()
	{
		showGoogleLogin();
	}
	
	public void ShowTwitterLogin()
	{
		showTwitterLogin();
	}
	
	public void InviteFacebookFriends()
	{
		inviteFacebookFriends();
	}

	public void SetHideWelcomeView(bool autoHideWelcomeView) {
		setHideWelcomeView (autoHideWelcomeView);
	}

	#endregion
	
	#region Payment functions
	/*
	 * Show payment view with default list payment packages
	 * */
	public void ShowPaymentView()
	{
		showPaymentView();
	}
	
	/*
	 * Show a specific package depends on your in-game mechanism
	 * */
	public void ShowPaymentViewWithPackageID(string packageID)
	{
		showPaymentViewWithPackageID(packageID);
	}
	
	public void SendStateToWrapper(string state) {
		sendStateToWrapper(state);
	}

	/*
	 * This function will close payment view  
	 * */
	public void ClosePaymentView()
	{
		closePaymentView();
	}
	#endregion
	
	#region Analytics functions
	public void SendView(string name) {
		sendViewWithName(name);
	}
	
	public void SendEvent(string category,string action,string label) {
		sendEventWithCategory(category, action, label);
	}
	
	public void SendEvent(string category,string action,string label, int value) {
		sendEventWithCategoryWithValue(category, action, label, value);
	}
	#endregion
	
	#region Notification functions
	public void SetPushGroup(string groupName) {
		registerPushNotificationWithGroupName(groupName);
	}
	#endregion

	#region Facebook App Events functions
	public void FBLogEvent(string name) {
		fbLogEvent(name);
	}

	public void FBLogEventWithParameter(string name, double value, Dictionary<string, string> dictionary) {
		fbLogEventWithParameter(name, value, ConvertDictionaryToString(dictionary));
	}
	#endregion
	#endif

	#if UNITY_ANDROID
	private AndroidJavaClass cls_AppotaUnityHandler;
	
	#region SDK functions
	public void Init(){
		AppotaSDKReceiver.InitializeGameObjects ();
		AndroidJNI.AttachCurrentThread ();
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		
		Debug.Log("Start init Android");
		
		cls_AppotaUnityHandler.CallStatic("Init");
		
		Debug.Log("Called init Android ");
	}
	
	public void SetAutoShowLoginDialog(bool autoShowLogin) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = autoShowLogin;
		cls_AppotaUnityHandler.CallStatic("SetAutoShowLoginDialog", args);
	}
	
	public void SetKeepLoginSession(bool isKeepLoginSession){
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isKeepLoginSession;
		cls_AppotaUnityHandler.CallStatic("SetKeepLoginSession", args);
	}
	
	// Have to call this function before Init() function.
	public void SetHideWelcomeView(bool autoHideWelcomeView) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = autoHideWelcomeView;
		cls_AppotaUnityHandler.CallStatic("SetHideWelcomeView", args);
	}

	public void SetSDKButtonVisibility(bool isVisibility) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isVisibility;
		cls_AppotaUnityHandler.CallStatic("SetSDKButtonVisibility", args);
    }
	
	public void FinishSDK()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("FinishSDK");
	}
	
	#endregion
	
	#region User functions
	public void Logout() {
		Debug.Log ("Start logout");
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");

		cls_AppotaUnityHandler.CallStatic("Logout");
	}
	
	public void SwitchAccount()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("SwitchAccount");
	}
	
	public void ShowUserInfoView()
	{
		AppotaThreadHandler.Instance.Start();

		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowUserInfoView");
	}

	public void ShowRegisterView()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowRegisterView");
	}

	public void ShowTransactionHistory()
	{
		AppotaThreadHandler.Instance.Start();

		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowTransactionHistory");
	}
	
	public void ShowLoginView()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowLoginView");
	}
	
	public void ShowFacebookLogin()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowLoginFacebook");
	}
	
	public void ShowGoogleLogin()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowLoginGoogle");
	}
	
	public void ShowTwitterLogin()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowLoginTwitter");
	}
	
	public void InviteFacebookFriends()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("InviteFacebookFriend");
	}
	
	public bool IsUserLoggedIn() {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		return cls_AppotaUnityHandler.CallStatic<bool>("IsUserLoggedIn");
	}

	public void SetCharacter(string characterName, string characterID, string serverName, string serverID) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[4];
		args [0] = characterName;
		args [1] = characterID;
		args [2] = serverName;
		args [3] = serverID;
		cls_AppotaUnityHandler.CallStatic("SetCharacter", args);
	}
	
	/*
	 * Return AppotaSession if logged in
	 * */
	public AppotaSession GetAppotaSession() {
		return AppotaSession.Instance;
	}
	#endregion
	
	#region Payment functions
	public void ShowPaymentView()
	{
		AppotaThreadHandler.Instance.Start();

		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowPaymentView");
	}

	public void ShowPaymentViewWithPackageID(string packageID)
	{
		AppotaThreadHandler.Instance.Start();

		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		
		object[] args = new object[1];
		args [0] = packageID;
		
		cls_AppotaUnityHandler.CallStatic("ShowPaymentViewWithPackageID", args);
	}

	
	public void ClosePaymentView()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ClosePaymentView");
	}

	public void SendStateToWrapper(string state) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = state;
		cls_AppotaUnityHandler.CallStatic("SendStateToWrapper", args);
	}
	#endregion
	
	#region Analytics functions
	public void SendView(string activityName) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = activityName;
		cls_AppotaUnityHandler.CallStatic("SendView", args);
	}
	
	public void SendEvent(string category,string action,string label) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[3];
		args [0] = category;
		args [1] = action;
		args [2] = label;
		cls_AppotaUnityHandler.CallStatic("SendEvent", args);
	}
	
	public void SendEvent(string category,string action,string label, int value) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[4];
		args [0] = category;
		args [1] = action;
		args [2] = label;
		args [3] = value;
		cls_AppotaUnityHandler.CallStatic("SendEvent", args);
	}
	#endregion
	
	#region Notification functions
	public void SetPushDeviceToken(string message) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = message;
		cls_AppotaUnityHandler.CallStatic("SetPushDeviceToken", args);
	}
	
	public void SetPushGroup(string groupName) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = groupName;
		cls_AppotaUnityHandler.CallStatic("SetPushGroup", args);
	}
	#endregion

	#region Facebook App Events functions
	public void ActivateFBAppEvent()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ActivateFBAppEvent");
	}
	
	public void DeactivateFBAppEvent()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("DeactivateFBAppEvent");
	}

	public void FBLogEvent(string name) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = name;
		cls_AppotaUnityHandler.CallStatic("FBLogEvent", args);
	}
	
	public void FBLogEventWithParameter(string name, double value, Dictionary<string, string> dictionary) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[3];
		args [0] = name;
		args [1] = value;
		args [2] = ConvertDictionaryToString(dictionary);
		cls_AppotaUnityHandler.CallStatic("FBLogEventWithParameter", args);
	}
	#endregion

	#region Other Functions
	public void ConfigureAppFlyer() {
		if (AppotaSetting.UsingAppFlyer) {
			cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
			object[] args = new object[1];
			args [0] = AppotaSetting.AppFlyerKey;
			cls_AppotaUnityHandler.CallStatic("ConfigureAppFlyer", args);
		}
	}

	public void ConfigureAdwords() {
		if (AppotaSetting.UsingAdWords) {
			cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
			object[] args = new object[4];
			args [0] = AppotaSetting.AdWordsConversionID;
			args [1] = AppotaSetting.AdWordsLabel;
			args [2] = AppotaSetting.AdWordsValue;
			args [3] = AppotaSetting.AdWordsIsRepeatable;
			cls_AppotaUnityHandler.CallStatic("ConfigureAdwords", args);
		}
	}

	#endregion
	#endif

	private string ConvertDictionaryToString(Dictionary<string, string> dictionary) {
		StringBuilder builder = new StringBuilder();
		foreach (KeyValuePair<string, string> pair in dictionary)
		{
			builder.Append(pair.Key).Append(":").Append(pair.Value).Append(';');
		}
		string result = builder.ToString();
		
		// Remove the final delimiter
		result = result.TrimEnd(';');
		
		return result;
	}
}
