Appota Game SDK4 Classes (Unity3D)
====

-------
AppotaSDKHandler.cs
------

* #### Descriptions:
* #### Syntax: 
`AppotaSDKHandler.Instance.Function()` : 需要 `Instance` 来返回`AppotaSDKHandler`的正确性


### 1. 初始化SDK: <a name = "init-function"> </a>

* `Init()` : 该功能出现在第一界面或者玩家想登录时
* `SetSDKButtonVisible(bool isVisible)` : 开启该功能来设置隐藏或者显示浮动按钮
* `SetKeepLoginSession(bool isKeepLoginSession)` : 该功能将控制Appota的登录会话 （Login Session），当开启应用时被保留或者更换（当功能被删除时玩家打开应用后要重新登录）
* `SetAutoShowLogin(bool autoShowLogin)` : 该功能将控制Appota的登录界面（ Login View）在开启应用时将自动刷出(当用户没有登陆) 或者你要开启[ShowLoginView](#show-login-view)功能。
* `FinishSDK()` : 在`OnApplicationQuit`里开启 (只要求安卓平台)

### 2. 用户功能: <a name = "user-function"> </a>

* `ShowLoginView()`: 该功能显示登陆界面（当玩家没有登陆时）
* `Logout()`: 退出功能
* `SwitchAccount()`: 用该功能来更改登陆账号.当更改账号成功时记得查看返回(在`OnLoginSuccess()`再一次显示)
* `ShowUserInfo()`: 该功能将刷出玩家信息界面  
* `IsUserLogin()`: 该功能返回玩家登录状态
* `GetAppotaSession()`: 若登录就返回AppotaSession
* `SetCharacter(string name, string server, string characterID)`: 设置角色功能支持在网站管理角色

### 3. 支付功能: <a name = "payment-function"> </a>

* `MakePayment()`: 可以默认支付包来显示支付界面(配how payment view with default list payment packages
* `MakePayment(string packageID)`: 按你们游戏内包的机制你可以显示某一种支付界面

### 4. 分析功能: <a name = "analytic-function"> </a>

* `SendView(string activityName)`:
* `SendEvent(string category,string action,string label)`:
* `SendEvent(string category,string action,string label, int value)`:

### 5. 通知功能: <a name = "notification-function"> </a>


* `SetPushGroup(string groupName)`: 注册并且设置群推送通知的功


-----

AppotaSDKReceiver.cs <a name = "appota-sdk-receiver"> </a>
-----
* #### Description:
  从Appota取得返回及·通过功能里的“result processing”处理
* #### Usage: 
在调动功能设置代码

### 1. SDK callbacks: <a name = "sdk-callback"> </a>

* `OnCloseLoginView()`: 调动在登陆界面关闭时

```c#

	public void OnCloseLoginView()
	{
		// Set processing code
	}
```

### 2. User callbacks: <a name = "user-callback"> </a>

* `OnLoginSuccess(string appotaSession)`: 登陆之后的调动. `appotaSession`玩家在JSON格式的信息。为了让研发者方便操作，我们分析玩家信息到`AppotaSession`

```c#

	public void OnLoginSuccess(string appotaSession)
	{
		// Parse user information to AppotaSession
		AppotaSession appotaSessionObj = new AppotaSession(appotaSession);
		AppotaSession.Instance.UpdateInstance(appotaSessionObj);
	
		// Set processing code
	}
```

* `OnLoginError(string error)`: 调动在登录失败时. SDK native 将一个错误通知到Unity3D。

```c#

	public void OnLoginError(string error)
	{	
		// @error message
		// Set processing code
	}
```

* `OnLogoutSuccess()`: 调动在登录成功。

```c#

	public void OnLogoutSuccess()
	{	
		// Set processing code
	}
```

### 3. 支付调动: <a name = "payment-callback"> </a>

* `OnPaymentSuccess(string transactionResult)`: 调动在支付成功. `transactionResult` 是JSON 格式的支付信息. 为了让研发者方便操作，我们分析支付信息到`AppotaPaymentResult`

```c#

	public void OnPaymentSuccess(string transactionResult)
	{	
		// Parse Transaction result into class AppotaPaymentResult.cs
		AppotaPaymentResult paymentResult = new AppotaPaymentResult(transactionResult);
		
		// Set processing code
	}
```

* `OnPaymentFailed(string error)`:  调动在支付失败时。SDK native 将一个错误通知到Unity3D。

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
玩家信息Object class.

* (string) `AccessToken`: access token为字符串格式。
* (string) `Email`: 用户邮箱
* (string) `ExpireDate`: 字符穿格式的access token的限期.
* (string) `RefreshToken`: 
* (string) `UserID`: 用户ID.
* (string) `UserName`: 用户名称.

------

AppotaPaymentResult.cs <a name = "appota-payment-result"> </a>
----
Object class about payment information.

* (string) `PackageID`: 支付包ID.
* (string) `Currency`: 支付货币.
* (string) `Time`: 支付时间.
* (string) `TransactionID`: 交易 ID
* (string) `Type`: 支付类型.
* (string) `ProductID`: 物品 ID.
* (string) `MethodINAPP`: INAPP方式.
