# Appota game 的第四版本SDK unity 

<b>Appota Game的第四版本sdk unity已经开始展开，正在研发预览版模式。第三版本还在repository [appota-unity-game-sdk3](https://github.com/appota/unity-game-sdk3).
若你从SDK3更新到SDK4 。烦请查看 [Uprade Document](Uprade.md)</b>

##重点

* **Appota 登陆** – 支持 Appota, Google, Facebook, Twitter, Guest各种登陆方式
* **Appota 支付** – 支持: SMS, Card, E-Bank, Apple 等支付防止.
* **Appota Analytics** – 可收集、可视化及了解用户和数据(session, active, error log ...)的大规模服务.
* 为iOS, Android, WindowPhone各个平台支持 ***推送通知 （Push Notification）***.

## 概括
1. [集成SDK](#head1-integrate-sdk)
2. [Client API](#head1-client-api)
    * [初始化并且配置SDK](#head2-init-sdk)
    * [用户功能相关函数](#head2-user-function)
    * [支付功能相关函数](#head2-payment-function)
    * [分析功能相关函数](#head2-analytic-function)
    * [推送通知（Push notification）](#head2-push-notification-function)
3. [类型相关文档（Class document）](#head1-class-document)
4. [FAQ and 说明](#head1-faq)
##1. 集成SDK <a name = "head1-integrate-sdk"> </a>
概范[sample apps](AppotaGameUnitySDK4/) 是可以为你设置的独立项目。你可以现有的应用集成Unity SDK.
###1.1.初始要求<a name = "head2-prerequisites">  </a>
开始 Appota Game Unity SDK， 你可以设置SDK并且用一个新项目开始建设或者以一个现有的应用集成Unity SDK。你可以试试例子来了解SDK的运行机制。
要求 Unity 4.5 或者更新版本

###1.2.获取 app api key 和 client key <a name="head2-obtain-app-api-key-and-client-key"> </a>

* 从应用管理页面导出 `ClientKey`, `APIKey`, `ClientSecret` 等Appota应用 <a name="head3-appota-appid"> </a>。若你还没向Appota注册Appota应用，你要[建设新应用](https://developer.appota.com/beta/).
* 获取 Facebook Application ID  <a name="head3-facebook-appid"> </a>. [如何建设、获取及设置IOS的Facebook app info 具体查看(https://developers.facebook.com/docs/ios/getting-started)。若你访问不到Facebook Developer页面，烦请向我们客服 联系(sale@appota.com) 来获取 facebook appid。
* 获取 Facebook App Link <a name = "head3-facebook-app-link"> </a>. [如何建设iOS的Facebook app link 具体查看](https://developers.facebook.com/docs/app-invites/ios) -> [App Link Tool](https://developers.facebook.com/quickstarts/?platform=app-links-host).
* 获取 Google Client ID 和 Client Secret <a name = "head3-google-appid"> </a>. [如何为iOS建设、 获取 及设置 Google app 具体查看](https://developers.google.com/+/mobile/ios/getting-started)。若你访问不到Google Developer页面，烦请向客服联系(sale@appota.com) 来获取 client ID 和 client Secret key。 
* 获取 Twitter Consumer Key 和Twitter Consumer Secret Key <a name= "head3-twitter-appid"> </a>. [如何为iOS建设、 获取 及设置 Twitter app 具体查看](https://apps.twitter.com/).

###1.3. 配置项目的XCode <a name="head2-configure-your-unity-project"> </a>

####1.3.1. 输入 plugin
![](images/import.gif)
####1.3.2. 配置Unity项目
- 用在[集成SDK]建设的`FacebookAppID`, `ApiKey`, `GoogleClientID`, `GoogleClientSecret` (#head1-integrate-sdk) )来更新设置

<img src="images/config.png" style="width:400;height:400">

###1.4. 配置 Frameworks (iOS用的) <a name="head2-configure-your-xcode-project"> </a>

用iOS平台的SDK, 拉并且放 <code>AppotaSDK.framework</code>, <code>AppotaBundle.bundle</code>, <code>FBSDKLoginKit.framework</code>,
<code>FBSDKShareKit.framework</code> 和
<code>FBSDKCoreKit.framework</code> 到 your Xcode project (project 从Unity3D 获取 )
在checkbox打钩: “Copy items into destination group's folder (if needed)”.
##2. Client API <a name="head1-client-api"> </a>
###2.1初始化并且设置 SDK <a name = "head2-init-sdk"> </a>
相关class 和功能：  
[AppotaSDKHandler](class-document/UnityClasses.md#init-function)  
[AppotaSDKReceiver](class-document/UnityClasses.md#appota-sdk-receiver)    
Appota SDK初始化当应用通过[AppotaSDKHandler](class-document/UnityClasses.html#init-function)要调动`AppotaSDKHandler.Instance.Init()`class时要出现，因此大部分时间`AppotaSDKHandler.Instance.Init()`将出现在第一个界面

```
AppotaSDKHandler.Instance.Init();
```
有一些控制SDK flows的功能，必须调用在`AppotaSDKHandler.Instance.Init之后()`
 `SetKeepLoginSession:(BOOL)` <a name="set-keep-login-session"> </a> 该功能将控制 Appota 登陆，控制当应用运营时，登陆会话 将被留存或者删除(若删除登陆会话，当应用开始运营时玩家要重新登陆).

	**注** :若不调用该功能， 系统就默认保留登陆会话。
	
------
- `SetAutoShowLoginDialog:(BOOL)` <a name="set-auto-show-login-dialog"> </a> 该功控制 Appota 登陆界面当应用运营时是否自动显示 （当用户还没登陆时） 或者你要调用 [[AppotaGameSDK showLoginView]](#show-login-view)函数来刷出登陆界面。

  **注* :若不调用该功能，就默认在当应用开始运营，用户还没登陆时自动显示登陆界面（Login View)。
  
-----
  
- 当你要显示或者隐藏SDK浮动按钮就调用该功能`SetSDKButtonVisibility:(BOOL)` <a name = "set-sdk-button-visible"> </a>。

  **注** :若不调用该功能，系统就默认打开应用后显示SDK浮动按钮。

-----

- 在`OnApplicationQuit` script调用`FinishSDK()` 函数 

###2.2. 用户相关函数 <a name="head2-user-function"> </a>
相关class 和功能：

- [AppotaSDKHandler](class-document/UnityClasses.md#user-function)
- [AppotaSDKReceiver](class-document/UnityClasses.md#user-callback)
- [AppotaSession](class-document/UnityClasses.md#appota-session)

当完成SDK初始化时，开启使用登录功能。
####2.2.1. 显示登陆界面 <a name = "head2-show-login-view"> </a> 
按上面所提的内容，登陆界面被[SetAutoShowLoginDialog(BOOL)](#set-auto-show-login-dialog)控制。若你不要登陆界面自动显示，当需要时可以手动调用 `ShowLoginView` 

- **显示登陆界面**

```
AppotaSDKHandler.Instance.ShowLoginView();
```
<a name="show-login-view"> </a>

Appota 登陆界面含有 5个登录方式， 如下： 
**Facebook**, **Google**, **Twitter**, **Appota User** and **快熟登陆** 

![Appota Login Dialog](images/login_dialog.png)

实施 [`OnLoginSucceed`](#did-login-succeed) 返回来确认登陆成功：
![Appota Login Mechanism](images/unity_login_user_sequence.png) 

- **退出函数**
 
 调用下面函数来退出
 
```
AppotaSDKHandler.Instance.Logout();
```
- **切换账号功能**

Gọi hàm này khi user đã login sẽ show một Login View and cho phép login vào tài khoản khác. Khi switch account thành công [`OnLoginSucceed`](#did-login-succeed) callback sẽ được gọi lại, vì vậy hãy đăng xuất tài khoản game của bạn và xác 获取 lại với tài khoản đã switch mới.
```
AppotaSDKHandler.Instance.SwitchAccount();
```

![Appota Switch Account Mechanism](images/unity_switch_user_sequence.png)

- **显示用户信息的功能**

调用该函数来显示用户信息  

```
AppotaSDKHandler.Instance.ShowUserInfo();
```
- **检查用户登录**

该函数将返回用户是否登陆的状态。用户若已经登录就返回 YES，还没登陆将返回NO。

```
AppotaSDKHandler.Instance.IsUserLogin();
```
- **获取 Appota会话**

T用户已经登录，就返回 Appota会话

```
AppotaSDKHandler.Instance.GetAppotaSession();
```
- **设置角色**

设置角色函数是用于支持在网站管理角色

```
public void SetCharacter(string name, string server, string characterID)
```

AppotaSDK 为在[`AppotaSDKReceiver`](class-document/UnityClasses.md#appota-sdk-receiver)定界的登陆提供四个callbacks delegate，  在`Appdelegate.m`实施这些功能来处理回调的登陆结果

**登陆成功回调**  <a name = "did-login-succeed"> </a>

- 从`AppotaSession`获取 Appota 用户信息，然后上传到服务器来确认并且创造游戏用户。关于在服上用户集成，具体查看 [集成用户](https://https://github.com/appota/ios-game-sdk/wiki/Integrate-user-system)

- **注** :进行集成玩家之前，要记得在服务器确认Appota UserID, UserName 和 Access token

```
public void  OnLoginSucceed(string appotaSession)
```

|参数|描述|  
|-------|-----------|  
|appotaSession|`appotaSession` 是 json 字符串，其包括所有用户信息， 把字符串换成AppotaSession容易取得玩家信息(`AppotaSession.AccessToken`, `AppotaSession.UserName`) |

----
**登录失败回调**  

在登陆发生问题时回调 

```
public void  OnLoginError(string error);
```

----
**退出回调**  

退出之后回调
```
public void  OnLogoutSuccess(string userName)
```
|参数|描述|  
|-------|-----------|  
|userName|`userName` 退出的用户名称|

----
**关闭登陆界面回调**  

在玩家关闭登陆界面时回调

```
public void  OnCloseLoginView();
```

###2.2. 支付功能 <a name="head2-payment-function"> </a>

相关class 和功能:

- [AppotaSDKHandler - payment function](class-document/UnityClasses.md#payment-function)
- [AppotaSDKReceiver](class-document/UnityClasses.md#payment-callback)
- [AppotaPaymentResult](class-document/UnityClasses.md#appota-payment-result)

要使用 AppotaSDK 支付功能，你要了解Appota支付机制. 请查看Appota支付机制在 [Appota Payment Document](https://github.com/appota/ios-game-sdk/wiki/Passive-Confirmation-via-IPN) 和支付配置在 [Appota Developer Portal](https://developer.appota.com/beta/).

####2.2.1. 显示支付界面 <a name="head3-show-payment-view"> </a>

-----
有三个支付界面的显示方式。 每个界面显示一个支付包或者一个支付列表。每种支付包含有游戏内金额或者货币的信息。

**显示默认支付列表界面**

你可以默认支付包列表显示支付界面 (该列表配置在[Appota Developer Portal](https://developer.appota.com/beta/) )

```
AppotaSDKHandler.Instance.MakePayment()
```
![](images/list_item.gif)

**. 以一个默认包显示 支付界面（payment view）**
您可以用属于您游戏机制的一个指定包显示支付界面

```
AppotaGameSDK.Instance.MakePayment(string packageID)
```

![](images/one_item.gif)

----
**从SDK按钮显示支付界面**  

![](images/list_item.gif)

####2.2.2 处理支付回调 <a name="head3-handle-payment-callback"> </a>

你要实施并且修改在 `AppotaSDKReceiver`里所有的回调。

**支付成功后的回调**

按支付机制(APN or IPN,具体查看[Appota Payment](https://github.com/appota/ios-game-sdk/wiki/Integrate-payment-system), 向用户确认支付或者通知支付成功

```
public void OnPaymentSuccess(string transactionResult);
```
|参数|描述|  
|-------|-----------|  
|`transactionResult`|是json字符串，其包括所有支付信息 (金额, 游戏货币, package　id), 字符串兑换成 `AppotaPaymentResult`容易获取支付信息|

----
**Payment state的回调**

**PAYMENT_STATE** 要求我们 SDK 反馈相应 package的信息.按照您的 payment package，SDK 实施该函数并且反馈一个对的 payment state。 [了解Payment state](#head3-payment-state).  

比如： packageID: com.gold.package1 - (被界定在 developer页面 )相当于Y服用户X充值1000元宝 。于是payment state为： 1000_gold_X_Y (按照您的格式).

注： PAYMENT_STATE不超过150个字符即可

```
public void GetPaymentState(string packageID);
```
|参数|描述|  
|-------|-----------|  
|`packageID`|`packageID`是已支付的 package ， packageID 被界定在支付配置过程中|

###2.3. 分析函数 <a name="head2-analytic-function"> </a>

当您进入一个 view ， 将该 view 发给 tracking

```
public void SendView(string name);
```
**Tracking event函数**  
当用户做一个活动时将发送活动追踪

```
public void SendEvent(string category,string action,string label)
```
界定一个操作的 `category`, `action`, `label` (action)

###2.3. 推送通知功能 <a name="head3-push-notification-function"> </a>

**注册群推送通知的功能**
  
使用群名称来选择推送用户群(比如：只推送1服的用户，群名称将为= "server 1").

```
public void setPushGroup(string groupName)
```

##3. Class Document <a name="head1-class-document"> </a>
- [AppotaSDKHandler](class-document/UnityClasses.md#init-function)
- [AppotaSDKReceiver](class-document/UnityClasses.md#appota-sdk-receiver)
- [AppotaSession](class-document/UnityClasses.md#appota-session)
- [AppotaPaymentResult](class-document/UnityClasses.md#appota-payment-result)

##4. FAQ and Glossary <a name="head1-faq"></a>
- `IPN` 是Appota系统为玩家新增元宝的机制. 具体查看： [IPN](https://github.com/appota/ios-game-sdk/wiki/Passive-Confirmation-via-IPN)
- `PackageID`每个游戏内的支付包应该附带 package ID (提供SDKTool) 来确认包。
- `PaymentState` <a name = "head3-payment-state"> </a> 要在`getPaymentStateWithPackageID:` 函数实施。 使用`PackageID`和游戏服务器信息来建设 PaymentState.  
比如 packageID:  `com.gold.package1` - ((在集成过程中界定)对应于1000元宝档位和Y服的X玩家。支付状态设为： com.gold.package1_1000_gold_X_Y (按你的格式). 