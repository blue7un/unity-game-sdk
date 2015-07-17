using UnityEngine;
using System.IO;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class AppotaSetting : ScriptableObject
{

	const string appotaSettingsAssetName = "AppotaSettings";
	const string appotaSettingsPath = "Appota/Resources";
	const string appotaSettingsAssetExtension = ".asset";
	
	private static AppotaSetting instance;
	
	static AppotaSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load(appotaSettingsAssetName) as AppotaSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<AppotaSetting>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, appotaSettingsPath);
					if (!Directory.Exists(properPath))
					{
						AssetDatabase.CreateFolder("Assets/Appota", "Resources");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", appotaSettingsPath),
					                               appotaSettingsAssetName + appotaSettingsAssetExtension
					                               );
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	#region App Settings
	[HideInInspector] [SerializeField]
	private string facebookAppID;
	[HideInInspector] [SerializeField]
	private string facebookAppSecretID;
	[HideInInspector] [SerializeField]
	private string facebookAppLinkUrl;
	[HideInInspector] [SerializeField]
	private string twitterConsumerKey;
	[HideInInspector] [SerializeField]
	private string twitterConsumerSecret;
	[HideInInspector] [SerializeField]
	private string googleClientId;
	[HideInInspector] [SerializeField]
	private string googleClientSecretId;
	[HideInInspector] [SerializeField]
	private string inAppApiKey;
	[HideInInspector] [SerializeField]
	private string paymentState;
	[HideInInspector] [SerializeField]
	private string gameID;

	// AppFlyer configure key
	[HideInInspector] [SerializeField]
	private bool usingAppFlyer;
	[HideInInspector] [SerializeField]
	private string appleAppID;
	[HideInInspector] [SerializeField]
	private string appFlyerKey;

	// AdWords configure key
	[HideInInspector] [SerializeField]
	private bool usingAdWords;
	[HideInInspector] [SerializeField]
	private string adWordsConversionID;
	[HideInInspector] [SerializeField]
	private string adWordsLabel;
	[HideInInspector] [SerializeField]
	private string adWordsValue;
	[HideInInspector] [SerializeField]
	private bool adWordsIsRepeatable;

	public static string FacebookAppID
	{
		get { return Instance.facebookAppID; }
		set
		{
			if (Instance.facebookAppID != value)
			{
				Instance.facebookAppID = value;
				DirtyEditor();
			}
		}
	}

	public static string FacebookAppSecretID
	{
		get { return Instance.facebookAppSecretID; }
		set
		{
			if (Instance.facebookAppSecretID != value)
			{
				Instance.facebookAppSecretID = value;
				DirtyEditor();
			}
		}
	}

	public static string FacebookAppLinkUrl
	{
		get { return Instance.facebookAppLinkUrl; }
		set
		{
			if (Instance.facebookAppLinkUrl != value)
			{
				Instance.facebookAppLinkUrl = value;
				DirtyEditor();
			}
		}
	}

	public static string TwitterConsumerKey
	{
		get { return Instance.twitterConsumerKey; }
		set
		{
			if (Instance.twitterConsumerKey != value)
			{
				Instance.twitterConsumerKey = value;
				DirtyEditor();
			}
		}
	}

	public static string TwitterConsumerSecret
	{
		get { return Instance.twitterConsumerSecret; }
		set
		{
			if (Instance.twitterConsumerSecret != value)
			{
				Instance.twitterConsumerSecret = value;
				DirtyEditor();
			}
		}
	}

	public static string GoogleClientId
	{
		get { return Instance.googleClientId; }
		set
		{
			if (Instance.googleClientId != value)
			{
				Instance.googleClientId = value;
				DirtyEditor();
			}
		}
	}

	public static string GoogleClientSecretId
	{
		get { return Instance.googleClientSecretId; }
		set
		{
			if (Instance.googleClientSecretId != value)
			{
				Instance.googleClientSecretId = value;
				DirtyEditor();
			}
		}
	}
	
	public static string InAppApiKey
	{
		get { return Instance.inAppApiKey; }
		set
		{
			if (Instance.inAppApiKey != value)
			{
				Instance.inAppApiKey = value;
				DirtyEditor();
			}
		}
	}

	public static string PaymentState
	{
		get { return Instance.paymentState; }
		set
		{
			if (Instance.paymentState != value)
			{
				Instance.paymentState = value;
				DirtyEditor();
			}
		}
	}

	public static string GameID
	{
		get { return Instance.gameID; }
		set
		{
			if (Instance.gameID != value)
			{
				Instance.gameID = value;
				DirtyEditor();
			}
		}
	}

	// AppFlyer configure key
	public static bool UsingAppFlyer
	{
		get { return Instance.usingAppFlyer; }
		set
		{
			if (Instance.usingAppFlyer != value)
			{
				Instance.usingAppFlyer = value;
				DirtyEditor();
			}
		}
	}

	public static string AppleAppID
	{
		get { return Instance.appleAppID; }
		set
		{
			if (Instance.appleAppID != value)
			{
				Instance.appleAppID = value;
				DirtyEditor();
			}
		}
	}

	public static string AppFlyerKey
	{
		get { return Instance.appFlyerKey; }
		set
		{
			if (Instance.appFlyerKey != value)
			{
				Instance.appFlyerKey = value;
				DirtyEditor();
			}
		}
	}

	// AdsWork configure key
	public static bool UsingAdWords
	{
		get { return Instance.usingAdWords; }
		set
		{
			if (Instance.usingAdWords != value)
			{
				Instance.usingAdWords = value;
				DirtyEditor();
			}
		}
	}

	public static string AdWordsConversionID
	{
		get { return Instance.adWordsConversionID; }
		set
		{
			if (Instance.adWordsConversionID != value)
			{
				Instance.adWordsConversionID = value;
				DirtyEditor();
			}
		}
	}

	public static string AdWordsLabel
	{
		get { return Instance.adWordsLabel; }
		set
		{
			if (Instance.adWordsLabel != value)
			{
				Instance.adWordsLabel = value;
				DirtyEditor();
			}
		}
	}

	public static string AdWordsValue
	{
		get { return Instance.adWordsValue; }
		set
		{
			if (Instance.adWordsValue != value)
			{
				Instance.adWordsValue = value;
				DirtyEditor();
			}
		}
	}

	public static bool AdWordsIsRepeatable
	{
		get { return Instance.adWordsIsRepeatable; }
		set
		{
			if (Instance.adWordsIsRepeatable != value)
			{
				Instance.adWordsIsRepeatable = value;
				DirtyEditor();
			}
		}
	}

	private static void DirtyEditor()
	{
		#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);
		#endif
	}
	
	#endregion
	
}
