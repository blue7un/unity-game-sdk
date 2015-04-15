using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

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
	private static extern void setSDKButtonVisible(bool isVisible);
	
	[DllImport("__Internal")]
	private static extern void setKeepLoginSession(bool isKeepLoginSession);

//	// Notification functions
//	[DllImport("__Internal")]
//	private static extern bool setHideWelcomeView(bool isHideWelcomeView);
	
	[DllImport("__Internal")]
	private static extern void inviteFacebookFriends();
	
	// User Functions
	[DllImport("__Internal")]
	private static extern void setAutoShowLogin(bool autoShowLogin);



	[DllImport("__Internal")]
	private static extern void showUserInfoView();
	
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
	private static extern void showTransactionHistory();
	
	[DllImport("__Internal")]
	private static extern void logOut();
	
	[DllImport("__Internal")]
	private static extern bool checkUserLogin();
	
	// Payment functions
	[DllImport("__Internal")]
	private static extern void showPaymentView();
	
	[DllImport("__Internal")]
	private static extern void showPaymentViewWithPackage(string packageID);
	
	[DllImport("__Internal")]
	private static extern void sendStateToWrapper(string state);
	
	[DllImport("__Internal")]
	private static extern void setCharacter(string name, string server, string characterID);
	
	[DllImport("__Internal")]
	private static extern void closePaymentView();
	
	// Track functions
	[DllImport("__Internal")]
	private static extern void sendEventWithCategory(string category, string action, string label, int value);
	
	[DllImport("__Internal")]
	private static extern void sendViewWithName(string name);
	
	// Notification functions
	[DllImport("__Internal")]
	private static extern bool registerPushNotificationWithGroupName(string name);



	#region SDK functions
	/*
	 * Call this function in your first scene or when you want user to login
	 * */
	public void Init(){
		init();
		Debug.Log("Called init iOS ");
	}
	
	/*
	 * Call this function to setting hide or show SDK floating button
	 * */
	public void SetSDKButtonVisible(bool isVisible){
		setSDKButtonVisible(isVisible);
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
	public void SetAutoShowLogin(bool autoShowLogin) {
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
		logOut();
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
	 * This function will show user profile view  
	 * */
	public void ShowUserInfo()
	{
		showUserInfoView();
	}
	
	/*
	 * This function will return user logged in state
	 * */
	public bool IsUserLogin() {
		return checkUserLogin();
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
	public void SetCharacter(string name, string server, string characterID) {
		setCharacter(name, server, characterID);
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
	
//	// Have to call this function before Init() function.
//	public void HideWelcomeView(bool autoHideWelcomeView) {
//		setHideWelcomeView (autoHideWelcomeView);
//	}

	#endregion
	
	#region Payment functions
	/*
	 * Show payment view with default list payment packages
	 * */
	public void MakePayment()
	{
		showPaymentView();
	}
	
	/*
	 * Show a specific package depends on your in-game mechanism
	 * */
	public void MakePayment(string packageID)
	{
		showPaymentViewWithPackage(packageID);
	}
	
	public void SendStateToWrapper(string state) {
		sendStateToWrapper(state);
	}
	#endregion
	
	#region Analytics functions
	public void SendView(string name) {
		sendViewWithName(name);
	}
	
	public void SendEvent(string category,string action,string label) {
		
	}
	
	public void SendEvent(string category,string action,string label, int value) {
		sendEventWithCategory(category, action, label, value);
	}
	#endregion
	
	#region Notification functions
	public void SetPushGroup(string groupName) {
		registerPushNotificationWithGroupName(groupName);
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
	
	public void SetAutoShowLogin(bool autoShowLogin) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = autoShowLogin;
		cls_AppotaUnityHandler.CallStatic("SetAutoShowLogin", args);
	}
	
	public void SetKeepLoginSession(bool isKeepLoginSession){
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isKeepLoginSession;
		cls_AppotaUnityHandler.CallStatic("SetKeepLoginSession", args);
	}
	
//	// Have to call this function before Init() function.
//	public void HideWelcomeView(bool autoHideWelcomeView) {
//		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
//		object[] args = new object[1];
//		args [0] = autoHideWelcomeView;
//		cls_AppotaUnityHandler.CallStatic("HideWelcomeView", args);
//	}
	
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
	
	public void ShowUserInfo()
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_AppotaUnityHandler.CallStatic("ShowUserInfo");
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
	
	public bool IsUserLogin() {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		return cls_AppotaUnityHandler.CallStatic<bool>("IsUserLogin");
	}
	
	/*
	 * Return AppotaSession if logged in
	 * */
	public AppotaSession GetAppotaSession() {
		return AppotaSession.Instance;
	}
	#endregion
	
	#region Payment functions
	public void MakePayment(string packageID)
	{
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		
		object[] args = new object[1];
		args [0] = packageID;
		
		cls_AppotaUnityHandler.CallStatic("MakePayment", args);
	}
	
	public void SetCharacter(string name, string server, string characterID) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[3];
		args [0] = name;
		args [1] = server;
		args [2] = characterID;
		cls_AppotaUnityHandler.CallStatic("SetCharacter", args);
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
//	public void SetPushDeviceToken(string message) {
//		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
//		object[] args = new object[1];
//		args [0] = message;
//		cls_AppotaUnityHandler.CallStatic("SetPushDeviceToken", args);
//	}
	
	public void SetPushGroup(string groupName) {
		cls_AppotaUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = groupName;
		cls_AppotaUnityHandler.CallStatic("SetPushGroup", args);
	}
	#endregion
	
	#endif
	
	
	#if UNITY_WP8
	
	public delegate void AppotaSDK();
	public AppotaSDK _Init = null;
	public AppotaSDK _Logout = null;
	public AppotaSDK _MakePayment	= null;
	public AppotaSDK _ShowUserInfo	= null;
	public AppotaSDK _ShowLoginView	= null;
	public Action<bool> _SetAutoShowLogin	= null;
	
	
	public void Init(){
		
		if(string.IsNullOrEmpty(AppotaSetting.InAppApiKey)){
			Debug.LogError("Missing Apikey. Please check your ID Setting.");
		} else
		if (string.IsNullOrEmpty(AppotaSetting.NoticeURL)){
			Debug.LogError("Missing NoticeUrl. Please check your ID Setting.");
		} else 
		if (string.IsNullOrEmpty(AppotaSetting.ConfigURL)){
			Debug.LogError("Missing ConfigUrl. Please check your ID Setting.");
		}
		
		if (_Init != null)
		{
			_Init();
		}
	}
	
	public void Logout() {
		if (_Logout != null)
		{
			_Logout();
		}
	}
	
	public void MakePayment()
	{
		if (_MakePayment != null)
		{
			_MakePayment();
		}
	}
	
	public void ShowUserInfo()
	{
		if (_ShowUserInfo != null)
		{
			_ShowUserInfo();
		}
	}
	
	public void ShowLoginView()
	{
		if (_ShowLoginView != null)
		{
			_ShowLoginView();
		}
	}
	
	public void SetAutoShowLogin(bool isAutoShowLogin) {
		if (_SetAutoShowLogin != null)
		{
			_SetAutoShowLogin(isAutoShowLogin);
		}
	}
	
	#endif
}
