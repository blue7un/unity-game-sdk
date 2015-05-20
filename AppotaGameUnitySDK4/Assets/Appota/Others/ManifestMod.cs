using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class ManifestMod
{
	// Appota Meta Data Element 
	public const string AppotaApikeyMetaDataName = "com.appota.apiKey";
	public const string AppotaGameIDMetaDataName = "com.onclan.gameId";

	// Social Elements Name
	public const string LoginActivityName = "com.appota.facebook.LoginActivity";
	public const string ApplicationIdMetaDataName = "com.facebook.sdk.ApplicationId";
	public const string TwitterKeyMetaDataName = "com.appota.twitter.consumer.key";
	public const string TwitterSecretMetaDataName = "com.appota.twitter.consumer.secret";

	// OnClan Elements Name
	public const string OnClanActivityName = "com.onclan.android.core.OnClanActivity";
	public const string OnClanLoginActivityName = "com.onclan.android.home.LoginActivity";
	public const string OnClanChatServiceName = "com.onclan.android.chat.mqtt.ChatService";

	// Appota Elements Name
	public const string AppotaBaseActivityName = "com.appota.gamesdk.v4.ui.BaseSDKActivty";

	public static void GenerateManifest()
	{
		var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
		
		// only copy over a fresh copy of the AndroidManifest if one does not exist
		if (!File.Exists(outputFile))
		{
			Debug.LogError("AndroidManifest.xml missing!!! Please put it in Assets/Plugin/Android.");
		}
		UpdateManifest(outputFile);
	}
	
	private static XmlNode FindChildNode(XmlNode parent, string name)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null)
		{
			if (curr.Name.Equals(name))
			{
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}

	private static XmlNode FindNodeWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
	{
		XmlNode curr = parent.FirstChild;
		while (curr != null)
		{
			if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
			{
				return curr;
			}
			curr = curr.NextSibling;
		}
		return null;
	}
	
	private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
	{
		var curr = parent.FirstChild;
		while (curr != null)
		{
			if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
			{
				return curr as XmlElement;
			}
			curr = curr.NextSibling;
		}
		return null;
	}
	
	public static void UpdateManifest(string fullPath)
	{

		XmlDocument doc = new XmlDocument();
		doc.Load(fullPath);
		
		if (doc == null)
		{
			Debug.LogError("Couldn't load " + fullPath);
			return;
		}
		
		XmlNode manNode = FindChildNode(doc, "manifest");
		XmlNode dict = FindChildNode(manNode, "application");

		if (dict == null)
		{
			Debug.LogError("Error parsing " + fullPath);
			return;
		}

		string ns = dict.GetNamespaceOfPrefix("android");

		#region Modify Social Elements 
		//add the login activity
		XmlElement loginElement = FindElementWithAndroidName("activity", "name", ns, LoginActivityName, dict);
		if (loginElement == null)
		{
			loginElement = CreateFBLoginElement(doc, ns);
			dict.AppendChild(loginElement);
		}

		//add the app id
		//<meta-data android:name="com.facebook.sdk.ApplicationId" android:value="\ 409682555812308" />
		XmlElement appIdElement = FindElementWithAndroidName("meta-data", "name", ns, ApplicationIdMetaDataName, dict);
		if (appIdElement == null)
		{
			appIdElement = doc.CreateElement("meta-data");
			appIdElement.SetAttribute("name", ns, ApplicationIdMetaDataName);
			dict.AppendChild(appIdElement);
		}
		appIdElement.SetAttribute("value", ns, "\\ " + AppotaSetting.FacebookAppID); // stupid hack to be string format

		//add the TwitterConsumerKey
		//<meta-data android:name="com.appota.gamesdk.twitter.consumer.key" android:value="YOUR_CONSUMER_KEY" />
		XmlElement TwitterKeyElement = FindElementWithAndroidName("meta-data", "name", ns, TwitterKeyMetaDataName, dict);
		if (TwitterKeyElement == null)
		{
			TwitterKeyElement = doc.CreateElement("meta-data");
			TwitterKeyElement.SetAttribute("name", ns, TwitterKeyMetaDataName);
			dict.AppendChild(TwitterKeyElement);
		}
		TwitterKeyElement.SetAttribute("value", ns, "" + AppotaSetting.TwitterConsumerKey); 

		//add the TwitterConsumerSecret
		//<meta-data android:name="com.appota.gamesdk.twitter.consumer.secret" android:value="YOUR_SECRET_KEY" />
		XmlElement TwitterSecretElement = FindElementWithAndroidName("meta-data", "name", ns, TwitterSecretMetaDataName, dict);
		if (TwitterSecretElement == null)
		{
			TwitterSecretElement = doc.CreateElement("meta-data");
			TwitterSecretElement.SetAttribute("name", ns, TwitterSecretMetaDataName);
			dict.AppendChild(TwitterSecretElement);
		}
		TwitterSecretElement.SetAttribute("value", ns, "" + AppotaSetting.TwitterConsumerSecret); 
		#endregion

		#region Appota Meta Data Elements
		// Add Apikey 
		XmlElement AppotaApikeyElement = FindElementWithAndroidName("meta-data", "name", ns, AppotaApikeyMetaDataName, dict);
		if (AppotaApikeyElement == null)
		{
			AppotaApikeyElement = doc.CreateElement("meta-data");
			AppotaApikeyElement.SetAttribute("name", ns, AppotaApikeyMetaDataName);
			dict.AppendChild(AppotaApikeyElement);
		}
		AppotaApikeyElement.SetAttribute("value", ns, "" + AppotaSetting.InAppApiKey); 

		// Add GameID 
		XmlElement AppotaGameIDElement = FindElementWithAndroidName("meta-data", "name", ns, AppotaGameIDMetaDataName, dict);
		if (AppotaGameIDElement == null)
		{
			AppotaGameIDElement = doc.CreateElement("meta-data");
			AppotaGameIDElement.SetAttribute("name", ns, AppotaGameIDMetaDataName);
			dict.AppendChild(AppotaGameIDElement);
		}
		AppotaGameIDElement.SetAttribute("value", ns, "" + AppotaSetting.GameID); 

		#endregion

		// Creating Elements is depend on which SDKs was used 
		if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
			CreateAppotaElements(doc, dict, ns);
			CreateOnClanElements(doc, dict, ns);
		}
		else if (System.Type.GetType("AppotaSDKHandler,Assembly-CSharp") != null) {
			CreateAppotaElements(doc, dict, ns);
			RemoveOnClanElements(dict, ns);
		}
		else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
			CreateOnClanElements(doc, dict, ns);
			RemoveAppotaElements(dict, ns);
		}

		Debug.Log("Complete setting manifest!!!");
		
		doc.Save(fullPath);
	}

	#region Social Elements
	private static XmlElement CreateFBLoginElement(XmlDocument doc, string ns)
	{
		//<activity android:name="com.facebook.LoginActivity" android:theme="@android:style/Theme.Translucent.NoTitleBar" android:label="@string/app_name" />
		XmlElement activityElement = doc.CreateElement("activity");
		activityElement.SetAttribute("name", ns, LoginActivityName);
		activityElement.SetAttribute("label", ns, "@string/app_name");
		activityElement.SetAttribute("theme", ns, "@android:style/Theme.Translucent.NoTitleBar");
		activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
		return activityElement;
	}
	#endregion

	#region AppotaSDK Activity Elements 
	private static void CreateAppotaElements(XmlDocument doc, XmlNode dict, string ns)
	{
		XmlElement AppotaBaseActivityElement = FindElementWithAndroidName("activity", "name", ns, AppotaBaseActivityName, dict);
		if (AppotaBaseActivityElement == null)
		{
			AppotaBaseActivityElement = CreateBaseActivityElement(doc, ns);
			dict.AppendChild(AppotaBaseActivityElement);
		}
	}

	private static void RemoveAppotaElements(XmlNode dict, string ns)
	{
		XmlElement AppotaBaseActivityElement = FindElementWithAndroidName("activity", "name", ns, AppotaBaseActivityName, dict);
		if (AppotaBaseActivityElement != null)
		{
			dict.RemoveChild((XmlNode)AppotaBaseActivityElement);
		}
	}

	private static XmlElement CreateBaseActivityElement(XmlDocument doc, string ns)
	{
		//<activity android:name="com.appota.gamesdk.UserActivity" android:configChanges="orientation|keyboardHidden|screenSize" android:launchMode="singleTask" android:theme="@style/Theme.Appota.GameSDK" android:windowSoftInputMode="adjustPan">
		XmlElement activityElement = doc.CreateElement("activity");
		activityElement.SetAttribute("name", ns, AppotaBaseActivityName);
		activityElement.SetAttribute("configChanges", ns, "orientation|keyboardHidden|screenSize");
		activityElement.SetAttribute("theme", ns, "@android:style/Theme.Dialog");
		activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
        return activityElement;
    }

	#endregion

	#region OnClan Activity Elements 
	private static void CreateOnClanElements(XmlDocument doc, XmlNode dict, string ns)
	{
		XmlElement OnClanActivityElement = FindElementWithAndroidName("activity", "name", ns, OnClanActivityName, dict);
		if (OnClanActivityElement == null)
		{
			OnClanActivityElement = CreateOnClanActivityElement(doc, ns);
			dict.AppendChild(OnClanActivityElement);
		}

		XmlNode OnClanLoginActivityElement = FindNodeWithAndroidName("activity", "name", ns, OnClanLoginActivityName, dict);
		if (OnClanLoginActivityElement == null)
		{
			OnClanLoginActivityElement = CreateOnClanLoginActivityElement(doc, ns);
			dict.AppendChild(OnClanLoginActivityElement);
		}

		XmlElement OnClanChatServiceElement = FindElementWithAndroidName("service", "name", ns, OnClanChatServiceName, dict);
		if (OnClanChatServiceElement == null)
		{
			OnClanChatServiceElement = CreateOnClanChatServiceElement(doc, ns);
			dict.AppendChild(OnClanChatServiceElement);
		}
	}

	private static void RemoveOnClanElements(XmlNode dict, string ns)
	{
		XmlElement OnClanActivityElement = FindElementWithAndroidName("activity", "name", ns, OnClanActivityName, dict);
		if (OnClanActivityElement != null)
		{
			dict.RemoveChild((XmlNode)OnClanActivityElement);
		}
		
		XmlNode OnClanLoginActivityElement = FindNodeWithAndroidName("activity", "name", ns, OnClanLoginActivityName, dict);
		if (OnClanLoginActivityElement != null)
		{
			dict.RemoveChild(OnClanLoginActivityElement);
		}
		
		XmlElement OnClanChatServiceElement = FindElementWithAndroidName("service", "name", ns, OnClanChatServiceName, dict);
		if (OnClanChatServiceElement != null)
		{
			dict.RemoveChild((XmlNode)OnClanChatServiceElement);
		}
	}

	private static XmlElement CreateOnClanActivityElement(XmlDocument doc, string ns)
	{
		//<activity android:name="com.onclan.android.core.OnClanActivity" android:configChanges="orientation|keyboardHidden|screenSize" android:theme="@android:style/Theme.Dialog">
		XmlElement activityElement = doc.CreateElement("activity");
		activityElement.SetAttribute("name", ns, OnClanActivityName);
		activityElement.SetAttribute("configChanges", ns, "orientation|keyboardHidden|screenSize");
		activityElement.SetAttribute("theme", ns, "@android:style/Theme.Dialog");
		activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
        return activityElement;
    }
    
    private static XmlNode CreateOnClanLoginActivityElement(XmlDocument doc, string ns)
	{
		XmlNode activityNode = doc.CreateNode("element", "activity", "");

		XmlAttribute attr = doc.CreateAttribute("name", ns);
		attr.Value = OnClanLoginActivityName;
		activityNode.Attributes.SetNamedItem(attr);

		XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");

		XmlElement actionElement = doc.CreateElement("action");
		actionElement.SetAttribute("name", ns, "com.onclan.android.sdk.login");

		XmlElement categoryElement = doc.CreateElement("category");
		categoryElement.SetAttribute("name", ns, "android.intent.category.DEFAULT");

		XmlElement dataElement = doc.CreateElement("data");
		dataElement.SetAttribute("host", ns, "sdk");
		dataElement.SetAttribute("scheme", ns, "onclan");

		intentFilterNode.AppendChild(actionElement);
		intentFilterNode.AppendChild(categoryElement);
		intentFilterNode.AppendChild(dataElement);

		activityNode.AppendChild(intentFilterNode);
		return activityNode;
	}

	private static XmlElement CreateOnClanChatServiceElement(XmlDocument doc, string ns)
	{
		//<service android:name="com.onclan.android.chat.mqtt.ChatService" />
		XmlElement serviceElement = doc.CreateElement("service");
		serviceElement.SetAttribute("name", ns, OnClanChatServiceName);
		serviceElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
		return serviceElement;
	}
	#endregion

	#region AppFlyers Elements 
	private static XmlNode CreateAppFlyerReceiverElement(XmlDocument doc, string ns)
	{
		XmlNode activityNode = doc.CreateNode("element", "receiver", "");
		
		XmlAttribute attr = doc.CreateAttribute("name", ns);
		attr.Value = "com.appsflyer.MultipleInstallBroadcastReceiver";
		activityNode.Attributes.SetNamedItem(attr);

		XmlAttribute attrExporter = doc.CreateAttribute("exported", ns);
		attrExporter.Value = "true";
		activityNode.Attributes.SetNamedItem(attrExporter);

		XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");
		
		XmlElement actionElement = doc.CreateElement("action");
		actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
		
		intentFilterNode.AppendChild(actionElement);
		
		activityNode.AppendChild(intentFilterNode);
		return activityNode;
	}
	#endregion

	#region AdsWorks Elements 
	private static XmlNode CreateAdsWorkReceiverElement(XmlDocument doc, string ns)
	{
		XmlNode activityNode = doc.CreateNode("element", "receiver", "");
		
		XmlAttribute attr = doc.CreateAttribute("name", ns);
		attr.Value = "com.google.ads.conversiontracking.InstallReceiver";
		activityNode.Attributes.SetNamedItem(attr);

		XmlAttribute attrExporter = doc.CreateAttribute("exported", ns);
		attrExporter.Value = "true";
		activityNode.Attributes.SetNamedItem(attrExporter);
		
		XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");
		
		XmlElement actionElement = doc.CreateElement("action");
		actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
		
		intentFilterNode.AppendChild(actionElement);
		
		activityNode.AppendChild(intentFilterNode);
		return activityNode;
	}
	#endregion
}
