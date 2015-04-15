using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;

[CustomEditor(typeof(AppotaSetting))]
public class IDController : EditorWindow {

	static string _facebookID;
	static string _facebookSecretID;
	static string _twitterKey;
	static string _twitterSecret;
	static string _googleID;
	static string _googleSecretID;
	static string _gameID;
	static string _inAppApiKey;
	private AppotaSetting instance;
	static bool isUsingOnClanSDK = false;
	static bool isUsingAppotaSDK = false;
	static int minHeight;
	static int minWidth;

	string version = "Version: 0.1";

	public Texture2D appotaLogo;
	
	private static IDController windows;
	void OnEnable()
	{
		appotaLogo = Resources.LoadAssetAtPath("Assets/Appota/Resources/appota_logo.png", typeof(Texture2D)) as Texture2D;
		
	}
	
	[MenuItem ("Appota/Configurations")]
	static void Init(){
		isUsingOnClanSDK = System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null;
		isUsingAppotaSDK = System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null;
		
		windows = GetWindow(typeof (IDController), false, "Appota", true) as IDController;
		
		if (isUsingAppotaSDK && isUsingOnClanSDK){
			windows.minSize = new Vector2(400, 620);
			windows.maxSize = new Vector2(600, 620);
		} else 
		if (isUsingAppotaSDK){
			windows.minSize = new Vector2(400, 450);
			windows.maxSize = new Vector2(600, 450);
		} else 
		if (isUsingOnClanSDK){
			windows.minSize = new Vector2(400, 450);
			windows.maxSize = new Vector2(600, 450);
		}
		
		windows.Show();
		
		EditorWindow.GetWindow(typeof (IDController)).Show();

		_facebookID = AppotaSetting.FacebookAppID;
		_facebookSecretID = AppotaSetting.FacebookAppSecretID;
		_twitterKey = AppotaSetting.TwitterConsumerKey;
		_twitterSecret = AppotaSetting.TwitterConsumerSecret;
		_googleID = AppotaSetting.GoogleClientId;
		_googleSecretID = AppotaSetting.GoogleClientSecretId;
		_gameID = AppotaSetting.GameID;
		_inAppApiKey = AppotaSetting.InAppApiKey;
		
	}
	
	void OnGUI()
	{
		GUILayout.Label(appotaLogo,GUILayout.MaxHeight(120), GUILayout.MaxWidth(400));
		
		//GUILayout.Space(20);
		
		EditorGUILayout.BeginVertical();
		
		if (PenaltyEditorTools.DrawHeader("Appota Settings"))
		{
			_inAppApiKey = EditorGUILayout.TextField("API Key", _inAppApiKey);

			if (isUsingOnClanSDK) {
				_gameID = EditorGUILayout.TextField("Game ID", _gameID);
			}
		}
		
		GUILayout.Space(10);
		
		if (PenaltyEditorTools.DrawHeader("Social Settings"))
		{
			_facebookID = EditorGUILayout.TextField("Facebook ID", _facebookID);
			#if UNITY_WP8
			_facebookSecretID = EditorGUILayout.TextField("Facebook Secret ID", _facebookSecretID);
			#endif
			_twitterKey = EditorGUILayout.TextField("Twitter Key", _twitterKey);
			_twitterSecret = EditorGUILayout.TextField("Twitter Secret", _twitterSecret);
			_googleID = EditorGUILayout.TextField("Google Client ID", _googleID);
			_googleSecretID = EditorGUILayout.TextField("Google Client Secret ID", _googleSecretID);
		}
		
		GUILayout.Space(20);
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginHorizontal();
		Color myStyleColor = new Color(190f / 255, 240f / 255, 143f / 255);
		GUI.backgroundColor = myStyleColor;
		
		if (GUILayout.Button("Update Setting!", GUILayout.ExpandWidth(true), GUILayout.MinWidth(250), GUILayout.MinHeight(50)))
		{
			
			SaveSetting();
		}
		GUILayout.Space(10);
		GUI.backgroundColor = Color.white;
		
		if (GUILayout.Button("Cancel!", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
		{
			windows.Close();
			//Cancel Setting;
		}
		
		EditorGUILayout.EndHorizontal();
		
		GUILayout.Space(10);

		GUILayout.Label (version, EditorStyles.label);
	}
	
	//Save or Update Settings Data
	
	void SaveSetting()
	{
		AppotaSetting.FacebookAppID = _facebookID;
		AppotaSetting.FacebookAppSecretID = _facebookSecretID;
		AppotaSetting.TwitterConsumerKey = _twitterKey;
		AppotaSetting.TwitterConsumerSecret = _twitterSecret;
		AppotaSetting.GoogleClientId = _googleID;
		AppotaSetting.GoogleClientSecretId = _googleSecretID;
		AppotaSetting.InAppApiKey = _inAppApiKey;
		AppotaSetting.GameID = _gameID;
		ManifestMod.GenerateManifest();
		Debug.Log("Complete setting!!!");
		
	}
	
}
