Appota Game SDK4 Classes (Unity3D)
====

-------
AppotaSDKHandler.cs
------

* #### Descriptions:
* #### Syntax: 
`AppotaSDKHandler.Instance.Function()` : need `Instance` to return right instance of `AppotaSDKHandler`


### 1. SDK Init functions: <a name = "init-function"> </a>

* `Init()` : Call this function in your first scene or when you want user to login
* `SetSDKButtonVisible(bool isVisible)` : Call this function to setting hide or show SDK floating button
* `SetKeepLoginSession(bool isKeepLoginSession)` : This function will control the Appota Login Session will be kept or removed at app lauching (when session's removed user has to login again when app start).
* `SetAutoShowLogin(bool autoShowLogin)` : This function will control the Appota Login View will be automatically show at app lauching (when user's not logged in). Or you have to call [ShowLoginView](#show-login-view) function to show the LoginView.
* `FinishSDK()` : Call this function in `OnApplicationQuit` script (only require in Android platform)

### 2. User functions: <a name = "user-function"> </a>

* `ShowLoginView()`: Show login view function (when user's not logged in)
* `Logout()`: Logout function
* `SwitchAccount()`: Call this function when you want to switch logged in user to other Appota User. Remember to check callback after switching successful (it'll be called in `OnLoginSuccess()` again)
* `ShowUserInfo()`: This function will show user profile view  
* `IsUserLogin()`: This function will return user logged in state
* `GetAppotaSession()`: Return `AppotaSession` if logged in
* `SetCharacter(string name, string server, string characterID)`: Set character function to support character management on web

### 3. Payment functions: <a name = "payment-function"> </a>

* `MakePayment()`: Show payment view with default list payment packages
* `MakePayment(string packageID)`: Show a specific package depends on your in-game mechanism

### 4. Analytics functions: <a name = "analytic-function"> </a>

* `SendView(string activityName)`:
* `SendEvent(string category,string action,string label)`:
* `SendEvent(string category,string action,string label, int value)`:

### 5. Notification functions: <a name = "notification-function"> </a>


* `SetPushGroup(string groupName)`: Register and set push group for push notification


-----

AppotaSDKReceiver.cs <a name = "appota-sdk-receiver"> </a>
-----
* #### Description:
  Getting results returned from Appota and handling by setting “result processing” in functions
* #### Usage: 
Set processing code inside these callback funtions

### 1. SDK callbacks: <a name = "sdk-callback"> </a>

* `OnCloseLoginView()`: Callback call when login view is closed

```c#

	public void OnCloseLoginView()
	{
		// Set processing code
	}
```

### 2. User callbacks: <a name = "user-callback"> </a>

* `OnLoginSuccess(string appotaSession)`: Callback call after login. `appotaSession` is user information in JSON format. For the convenience of developer, we parsed user information to class object `AppotaSession`

```c#

	public void OnLoginSuccess(string appotaSession)
	{
		// Parse user information to AppotaSession
		AppotaSession appotaSessionObj = new AppotaSession(appotaSession);
		AppotaSession.Instance.UpdateInstance(appotaSessionObj);
	
		// Set processing code
	}
```

* `OnLoginError(string error)`: Callback call when login failed. SDK native will send to Unity3D an error message.

```c#

	public void OnLoginError(string error)
	{	
		// @error message
		// Set processing code
	}
```

* `OnLogoutSuccess()`: Callback call when logout success.

```c#

	public void OnLogoutSuccess()
	{	
		// Set processing code
	}
```

### 3. Payment callback: <a name = "payment-callback"> </a>

* `OnPaymentSuccess(string transactionResult)`: Callback call after making payment successful. `transactionResult` is payment information in JSON format. For the convenience of developer, we parsed payment information to class object `AppotaPaymentResult`

```c#

	public void OnPaymentSuccess(string transactionResult)
	{	
		// Parse Transaction result into class AppotaPaymentResult.cs
		AppotaPaymentResult paymentResult = new AppotaPaymentResult(transactionResult);
		
		// Set processing code
	}
```

* `OnPaymentFailed(string error)`:  Callback call when making payment failed. SDK native will send to Unity3D an error message.

```c#

	public void OnPaymentFailed(string error)
	{	
		// @error message
		// Set processing code
	}
```

* `GetPaymentState(string packageID)`: 

------

AppotaSession.cs <a name = "appota-session"> </a>
-----
Object class about user information.

* (string) `AccessToken`: Login access token in string format.
* (string) `Email`: Email user.
* (string) `ExpireDate`: Expire date of access token in string format.
* (string) `RefreshToken`: 
* (string) `UserID`: User ID.
* (string) `UserName`: User name.

------

AppotaPaymentResult.cs <a name = "appota-payment-result"> </a>
----
Object class about payment information.

* (string) `PackageID`: Payment package ID.
* (string) `Currency`: Payment currency.
* (string) `Time`: Payment time.
* (string) `TransactionID`: Transaction ID
* (string) `Type`: Payment type.
* (string) `ProductID`: Product ID.
* (string) `MethodINAPP`: Method INAPP.
