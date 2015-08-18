ChangeLog
=====
## 0. Changes from SDK
- Config SDK: Add `Other Setting` and change GUI, only require `Appota APIKey` for Appota setting, remove `ClientID`, `ClientSecret`, `UsingSandBox`
- Class name `AppotaSDKHanler` and `AppotaSDKReceiver` not change
- Function change:

|SDK3|SDK4|Change|
|-----|-----|-----|
|`SetAutoShowLogin`|`SetAutoShowLoginDialog`|Auto show login dialog at SDK init|
|`SetState`|`GetPaymentState(string packageID)`|Change machenism implement `GetPaymentState(String)` callback|
|`GetUserID`|`GetAppotaSession`|Return `AppotaSession` object, to get userId, userName, ... by `session.UserID`, `session.UserName`|
|`ShowPaymentButton`, `HidePaymentButton`|`SetSDKButtonVisibility(BOOL)`|call `SetSDKButtonVisibility(BOOL)` true or false to hide or show floating button|
|`OnSwitchAccountSuccess`, `OnLogoutSuccess`|Removed|When user call switch account, and switched it'll callback in `OnLoginSuccess`, logout immidiatedly without callback when `logOut` called|
|`MakePayment`|`ShowPaymentView`, `ShowPaymentViewWithPackageID(String)`|Change to `ShowPaymentView` and `ShowPaymentViewWithPackageID(String)`|
|`AppotaSession`|`AppotaSession`| changed to single ton `AppotaSession.Instance`|

- New function:


## 1. SDK

**Version 4.0.8:**

- Fixed error update user info (iOS)
- Fixed crash Facebook sdk ver 4.4.0 (iOS)
- Remove require current password for update password(Android)
- Add setHidePaymentView functions(Only effect by clicking SDK Floating button)
- Update Facebook frameworks(iOS)
- Fix state null issues
- Add some functions to UIThread, fix bugs: "Can't create handler inside thread looper.prepare" 
- Update Plist for iOS 9 

**Version 4.0.7:**

- Add FBAppLinkURL in Plist (iOS)
- Fix bank issues(iOS)
- Fix callback issues (Android)
- Fix Payment package not showing(Android)
- Add loading in banks payment (Android)
- Add UseSmallSDKButton() function (Android)
- Add retry dialog for GooglePlay Payment if not success
- Change amout type to float in order to display transaction result for Google Play Payment
- Add namespace to avoid conflicting with Facebook SDK

**Version 4.0.6:**

- Add support tracking with AppFlyer and Adwords
- Sync name functions in iOS and Android platform

**Version 4.0.5:**

- Update Facebook App Event features

**Version 4.0.4:**

- Add ActiveCode module

**Version 4.0.3:**

Update Android jar with:

- Function Init() should be called before settings other configures.
- AutoShowLoginView is true by default.

**Version 4.0.2**

- Sync functions in iOS and Android platform </br>
- Update iOS frameworks and Android jar</br>
- Fix Show User Info crash</br>

**Version 4.0.1:**

- Pre-release version for AppotaGameSDK 4 for Unity

## 2. Server

- Add `revenue` parameter in IPN callback to measure revenue of current payment method type `CARD`, `BANK`, ...
- Reimplement your hash checking function to add `revenue` parameter (it will be add in `a-z` order)
- For detail please read wiki about IPN for each payment method [https://github.com/appota/unity-game-sdk/wiki/Passive-Confirmation-via-IPN](https://github.com/appota/unity-game-sdk/wiki/Passive-Confirmation-via-IPN)
