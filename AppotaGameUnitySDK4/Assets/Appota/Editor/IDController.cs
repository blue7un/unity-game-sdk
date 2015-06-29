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
	// AppFlyer key
	static bool _usingAppFlyer;
	static string _appleAppID;
	static string _appFlyerKey;
	// AdWords key
	static bool _usingAdWords;
	static string _adWordsConversionID;
	static string _adWordsLabel;
	static string _adWordsValue;
	static bool _adWordsIsRepeatable;

	private AppotaSetting instance;
	static bool isUsingOnClanSDK = false;
	static bool isUsingAppotaSDK = false;
	static int minHeight;
	static int minWidth;

	string version = "Version: 4.0.7";

	public Texture2D appotaLogo;
	
	private static IDController windows;
	void OnEnable()
	{
		appotaLogo = AssetDatabase.LoadAssetAtPath("Assets/Appota/Resources/appota_logo.png", typeof(Texture2D)) as Texture2D;
		
	}
	
	[MenuItem ("Appota/Configurations")]
	static void Init(){
		isUsingOnClanSDK = System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null;
		isUsingAppotaSDK = System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null;
		
		windows = GetWindow(typeof (IDController), false, "Appota", true) as IDController;

		windows.minSize = new Vector2(400, 550);
		windows.maxSize = new Vector2(600, 700);

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

		_usingAppFlyer = AppotaSetting.UsingAppFlyer;
		_appleAppID = AppotaSetting.AppleAppID;
		_appFlyerKey = AppotaSetting.AppFlyerKey;

		_usingAdWords = AppotaSetting.UsingAdWords;
		_adWordsConversionID = AppotaSetting.AdWordsConversionID;
		_adWordsLabel = AppotaSetting.AdWordsLabel;
		_adWordsValue = AppotaSetting.AdWordsValue;
		_adWordsIsRepeatable = AppotaSetting.AdWordsIsRepeatable;
	}
	
	void OnGUI()
	{
		GUILayout.Label(appotaLogo,GUILayout.MaxHeight(120), GUILayout.MaxWidth(400));
		
		GUILayout.Space(20);
		GUILayout.Label (version, EditorStyles.label);
		
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
		
		GUILayout.Space(10);

		if (PenaltyEditorTools.DrawHeader("Other Settings"))
		{
			// AppFlyer Configure
			if (_usingAppFlyer) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
			_usingAppFlyer = EditorGUILayout.Toggle("Using AppFlyer",_usingAppFlyer);
			GUI.backgroundColor = Color.white;
			if (_usingAppFlyer) {
				_appleAppID = EditorGUILayout.TextField("Apple AppID", _appleAppID);
				_appFlyerKey = EditorGUILayout.TextField("AppFlyer Key", _appFlyerKey);
				GUILayout.Space(20);
			}

			// AdWords Configure
			if (_usingAdWords) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
			_usingAdWords = EditorGUILayout.Toggle("Using AdWords",_usingAdWords);
			GUI.backgroundColor = Color.white;
			if (_usingAdWords) {
				_adWordsConversionID = EditorGUILayout.TextField("ConversionID", _adWordsConversionID);
				_adWordsLabel = EditorGUILayout.TextField("Label", _adWordsLabel);
				_adWordsValue = EditorGUILayout.TextField("Value", _adWordsValue);
				if (_adWordsIsRepeatable) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
				_adWordsIsRepeatable = EditorGUILayout.Toggle("IsRepeatable",_adWordsIsRepeatable);
				GUI.backgroundColor = Color.white;
			}


		}
		
		GUILayout.Space(10);

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

		AppotaSetting.UsingAppFlyer = _usingAppFlyer;
		AppotaSetting.AppleAppID = _appleAppID;
		AppotaSetting.AppFlyerKey = _appFlyerKey;
		
		AppotaSetting.UsingAdWords = _usingAdWords;
		AppotaSetting.AdWordsConversionID = _adWordsConversionID;
		AppotaSetting.AdWordsLabel = _adWordsLabel;
		AppotaSetting.AdWordsValue = _adWordsValue;
		AppotaSetting.AdWordsIsRepeatable = _adWordsIsRepeatable;
		ManifestMod.GenerateManifest();
		Debug.Log("Complete setting!!!");
		
	}
	
}
