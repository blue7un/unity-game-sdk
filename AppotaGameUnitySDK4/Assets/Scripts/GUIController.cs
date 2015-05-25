using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	float ScreenWidth;
	float ScreenHeight;
	string text = "UserID: ";

	// Use this for initialization
	void Start () {
		#if UNITY_IPHONE
		AppotaSDKHandler.Instance.Init();
		AppotaSDKHandler.Instance.SetAutoShowLoginDialog(false);
		#endif

		#if UNITY_ANDROID
		AppotaSDKHandler.Instance.Init();
		AppotaSDKHandler.Instance.SetAutoShowLoginDialog(false);

		#endif

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	void OnApplicationQuit(){
		#if UNITY_ANDROID
		AppotaSDKHandler.Instance.FinishSDK();
		#endif
	}

	void OnGUI () {

		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;

		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 30;
		
		if(GUI.Button(new Rect(ScreenWidth / 3, 40,ScreenWidth / 3,ScreenHeight / 10), "Login", customButton)) {
			AppotaSDKHandler.Instance.ShowLoginView ();
		}
		
		if(GUI.Button(new Rect(ScreenWidth / 3, 50 + ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Logout", customButton)) {
			AppotaSDKHandler.Instance.Logout (false);
		}

		if(GUI.Button(new Rect(ScreenWidth / 3, 60 + 2 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Switch Account", customButton)) {
			AppotaSDKHandler.Instance.SwitchAccount ();
		}

		AppotaSession appotaSession = AppotaSDKHandler.Instance.GetAppotaSession();
		text = "UserID: " + appotaSession.UserID;
		GUI.TextArea(new Rect(10, 40, ScreenWidth / 4, ScreenHeight / 10), text, 200, customButton);

		if(AppotaSDKHandler.Instance.IsUserLoggedIn()){	
			if(GUI.Button(new Rect(ScreenWidth / 3, 70 + 3 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "User Info", customButton)) {
				AppotaSDKHandler.Instance.ShowUserInfoView ();
			}

			if(GUI.Button(new Rect(ScreenWidth / 3, 80 + 4 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Payment", customButton)) {
				AppotaSDKHandler.Instance.ShowPaymentView ();
			}

			if(GUI.Button(new Rect(ScreenWidth / 3, 90 + 5 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Payment With Package", customButton)) {
				AppotaSDKHandler.Instance.ShowPaymentViewWithPackageID ("app.pkid.tym4K");
			}

		}
	}
}
