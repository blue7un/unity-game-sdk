using UnityEngine;
using System.IO;
using System.Xml;

namespace Appota
{
	public class MainXAMLMod : MonoBehaviour {
		
		const string stringHeader = "using System;\nusing APTCallback; \nusing APTPaymentResult;\nusing APTPaymentService;";
		const string stringImplClass = @"MainPage : PhoneApplicationPage, AppotaSDKCallback
    {";
		const string stringFunctionInit = @"
			SetupGeolocator();
            SDKInit();
        }";
		const string stringParagraph = 
			@"
		
		AppotaGameSDK gameSDK;

        private void SDKInit()
        {
  			AppotaSDKHandler.Instance._Init += _AppotaSDKHandler__InitSDK;
            AppotaSDKHandler.Instance._Logout += _AppotaSDKHandler__Logout;
            AppotaSDKHandler.Instance._MakePayment += _AppotaSDKHandler__MakePayment;
            AppotaSDKHandler.Instance._ShowLoginView += _AppotaSDKHandler__ShowLoginView;
            AppotaSDKHandler.Instance._ShowUserInfo += _AppotaSDKHandler__ShowUserInfo;
            AppotaSDKHandler.Instance._SwitchAccount += _AppotaSDKHandler__SwitchAccount;
            AppotaSDKHandler.Instance._SetAutoShowLogin += _AppotaSDKHandler__SetAutoShowLogin;
        }

  		private void _AppotaSDKHandler__InitSDK()
        {

   			Dispatcher.BeginInvoke(() =>
            {
				if (gameSDK == null) gameSDK = new AppotaGameSDK
             	(
	                 AppotaSetting.InAppApiKey,
	                 AppotaSetting.SandboxApiKey,
	                 AppotaSetting.IsUsingSandbox,
	                 AppotaSetting.NoticeURL,
	                 AppotaSetting.ConfigURL,
	                 AppotaSetting.FacebookAppID,
	                 AppotaSetting.FacebookAppSecretID,
	                 AppotaSetting.GoogleClientId,
	                 AppotaSetting.GoogleClientSecretId,
	                 AppotaSetting.TwitterConsumerKey,
	                 AppotaSetting.TwitterConsumerSecret,
	                 this
             	);
            });
           
        }


        private void _AppotaSDKHandler__SwitchAccount()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.SwitchAccount();
            });
        }

        void _AppotaSDKHandler__SetAutoShowLogin(bool obj)
        {
 			Dispatcher.BeginInvoke(() =>
            {
 				gameSDK.SetAutoShowLogin(obj);
			});
        }

        void _AppotaSDKHandler__ShowUserInfo()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.ShowUserInfo();
            });
        }

        void _AppotaSDKHandler__ShowLoginView()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.ShowLogin();

            });
        }

        void _AppotaSDKHandler__MakePayment()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.MakePayment();
            });
        }

        void _AppotaSDKHandler__Logout()
        {
            gameSDK.LogoutAccount();
        }

        //callback
        public void onPaymentError(string message)
        {
        }

        public void onPaymentSuccess(TransactionResult result)
        {
            AppotaSDKReceiver.Instance.OnPaymentSuccess(result.getAmount());
        }

        public void onUserLoginError(string message)
        {
        }

        public void onUserLoginSuccess(UserLoginResult result)
        {
            AppotaSDKReceiver.Instance.OnLoginSuccess(result.toJson());
        }

        public void onUserRegisterError(string message)
        {
        }

        public void onUserRegisterSuccess(UserLoginResult result)
        {
        }

        public void OnSwitchAccountSuccess(UserLoginResult result)
        {
        }
	}
}";
		
		public static void UpdateMainPageXAML(string path){
			
			var reader = new StreamReader(path);
			
			if (reader == null){
				Debug.Log("Can not find MainPage.xaml.xml in path: " + path);
				return;
			}
			
			string text = reader.ReadToEnd();
			reader.Close();
			
			if(text.IndexOf("SDKInit();", System.StringComparison.Ordinal) > 0){
				return;
			}
			
			int indexHeaderStart = text.IndexOf("using", System.StringComparison.Ordinal);
			int indexHeaderEnd = text.IndexOf(";", indexHeaderStart);
			
			string fixedText = text.Substring(0, indexHeaderStart);
			fixedText += stringHeader;
			fixedText += text.Substring(indexHeaderEnd + 1);
			
			int indexImplClassStart = fixedText.IndexOf("MainPage", System.StringComparison.Ordinal);
			int indexImplClassEnd = fixedText.IndexOf("{", indexImplClassStart);
			
			string fixedText1 = fixedText.Substring(0, indexImplClassStart);
			fixedText1 += stringImplClass;
			fixedText1 += fixedText.Substring(indexImplClassEnd + 1);
			
			int indexSDKInitStart = fixedText1.IndexOf("SetupGeolocator();", System.StringComparison.Ordinal);
			int indexSDKInitSEnd = fixedText1.IndexOf("}", indexSDKInitStart);
			
			string fixedText2 = fixedText1.Substring(0, indexSDKInitStart);
			fixedText2 += stringFunctionInit;
			fixedText2 += fixedText1.Substring(indexSDKInitSEnd + 1);
			
			string fixedText3 = fixedText2.Substring(0, fixedText2.Length - 4);
			fixedText3 += stringParagraph;
			
			var writer = new StreamWriter(path, false);
			writer.Write(fixedText3);
			Debug.Log ("text 3: " + fixedText3);
			
			writer.Close();
		}
	}
}