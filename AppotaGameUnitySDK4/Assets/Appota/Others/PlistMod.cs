using UnityEngine;
using System.IO;
using System.Xml;

namespace Appota
{
	public class PlistMod
	{
		private static XmlNode FindPlistDictNode(XmlDocument doc)
		{
			var curr = doc.FirstChild;
			while(curr != null)
			{
				if(curr.Name.Equals("plist") && curr.ChildNodes.Count == 1)
				{
					var dict = curr.FirstChild;
					if(dict.Name.Equals("dict"))
						return dict;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
		private static XmlElement AddChildElement(XmlDocument doc, XmlNode parent, string elementName, string innerText=null)
		{
			var newElement = doc.CreateElement(elementName);
			if(!string.IsNullOrEmpty(innerText))
				newElement.InnerText = innerText;
			
			parent.AppendChild(newElement);
			return newElement;
		}
		
		private static bool HasKey(XmlNode dict, string keyName)
		{
			var curr = dict.FirstChild;
			while(curr != null)
			{
				if(curr.Name.Equals("key") && curr.InnerText.Equals(keyName))
					return true;
				curr = curr.NextSibling;
			}
			return false;
		}
		
		private static string GetInerTextElement(XmlNode dict, string keyName){
			var curr = dict.FirstChild;
			while(curr != null)
			{
				if(curr.Name.Equals("key") && curr.InnerText.Equals(keyName)){
					curr = curr.NextSibling;
					if (curr.Name.Equals("string")){
						Debug.Log("InnerText: " + curr.InnerText);
						return curr.InnerText;
					}
				}
				curr = curr.NextSibling;
			}
			return "";
		}
		
		public static void UpdatePlist(string path, string apikey, string fbAppId, string fbAppLinkUrl, string twitterConsumerKey, string twitterConsumerSecret, string googleClientID, string googleClientSecret)
		{
			const string fileName = "Info.plist";
			string fullPath = Path.Combine(path, fileName);
			
			if(string.IsNullOrEmpty(fbAppId) || fbAppId.Equals("0"))
			{
				Debug.LogError("You didn't specify a Facebook app ID.");
			}
			
			if(string.IsNullOrEmpty(twitterConsumerKey) || string.IsNullOrEmpty(twitterConsumerSecret))
			{
				Debug.LogError("You didn't specify a Twitter Consummer Key or Secret Key.");
			}
			
			if(string.IsNullOrEmpty(googleClientID) || string.IsNullOrEmpty(googleClientSecret))
			{
				Debug.LogError("You didn't specify a Google Client ID or Google Client Secret.");
			}
			
			var doc = new XmlDocument();
			doc.Load(fullPath);
			
			var dict = FindPlistDictNode(doc);
			if(dict == null)
			{
				Debug.LogError("Error parsing " + fullPath);
				return;
			}
			
			//add the app id to the plist
			//the xml should end up looking like this
			if(!HasKey(dict, "AppotaAPIKey"))
			{
				AddChildElement(doc, dict, "key", "AppotaAPIKey");
				AddChildElement(doc, dict, "string", apikey);
			}
			
			/*
            <key>FacebookAppID</key>
            <string>YOUR_APP_ID</string>
             */
			if(!HasKey(dict, "FacebookAppID"))
			{
				AddChildElement(doc, dict, "key", "FacebookAppID");
				AddChildElement(doc, dict, "string", fbAppId);
			}
			
			if(!HasKey(dict, "FacebookAppLinkUrl"))
			{
				AddChildElement(doc, dict, "key", "FacebookAppLinkUrl");
				AddChildElement(doc, dict, "string", fbAppLinkUrl);
			}
			
			if(!HasKey(dict, "TWITTER_CONSUMER_KEY"))
			{
				AddChildElement(doc, dict, "key", "TWITTER_CONSUMER_KEY");
				AddChildElement(doc, dict, "string", twitterConsumerKey);
			}
			
			if(!HasKey(dict, "TWITTER_CONSUMER_SECRET"))
			{
				AddChildElement(doc, dict, "key", "TWITTER_CONSUMER_SECRET");
				AddChildElement(doc, dict, "string", twitterConsumerSecret);
			}
			
			if(!HasKey(dict, "GOOGLE_CLIENT_ID"))
			{
				AddChildElement(doc, dict, "key", "GOOGLE_CLIENT_ID");
				AddChildElement(doc, dict, "string", googleClientID);
			}
			
			if(!HasKey(dict, "GOOGLE_CLIENT_SECRET"))
			{
				AddChildElement(doc, dict, "key", "GOOGLE_CLIENT_SECRET");
				AddChildElement(doc, dict, "string", googleClientSecret);
			}

			// Support for iOS 9
			if(!HasKey(dict, "NSAllowsArbitraryLoads"))
			{
				AddChildElement(doc, dict, "key", "NSAllowsArbitraryLoads");
				AddChildElement(doc, dict, "true");
            }
			
			
			//here's how the custom url scheme should end up looking
			/*
             <key>CFBundleURLTypes</key>
             <array>
                 <dict>
                     <key>CFBundleURLSchemes</key>
                     <array>
                         <string>fbYOUR_APP_ID</string>
                     </array>
                 </dict>
             </array>
            */
			if(!HasKey(dict, "CFBundleURLTypes"))
			{
				AddChildElement(doc, dict, "key", "CFBundleURLTypes");
				var urlSchemeTop = AddChildElement(doc, dict, "array");
				{
					var urlSchemeDict = AddChildElement(doc, urlSchemeTop, "dict");
					{
						AddChildElement(doc, urlSchemeDict, "key", "CFBundleURLSchemes");
						var innerArray = AddChildElement(doc, urlSchemeDict, "array");
						{
							AddChildElement(doc, innerArray, "string", "fb" + fbAppId);
							AddChildElement(doc, innerArray, "string", GetInerTextElement(dict, "CFBundleIdentifier"));
						}
					}
				}
			}
			
			doc.Save(fullPath);
			
			//the xml writer barfs writing out part of the plist header.
			//so we replace the part that it wrote incorrectly here
			var reader = new StreamReader(fullPath);
			string textPlist = reader.ReadToEnd();
			reader.Close();
			
			int fixupStart = textPlist.IndexOf("<!DOCTYPE plist PUBLIC", System.StringComparison.Ordinal);
			if(fixupStart <= 0)
				return;
			int fixupEnd = textPlist.IndexOf('>', fixupStart);
			if(fixupEnd <= 0)
				return;
			
			string fixedPlist = textPlist.Substring(0, fixupStart);
			fixedPlist += "<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">";
			fixedPlist += textPlist.Substring(fixupEnd+1);
			
			var writer = new StreamWriter(fullPath, false);
			writer.Write(fixedPlist);
			writer.Close();
		}
	}
}