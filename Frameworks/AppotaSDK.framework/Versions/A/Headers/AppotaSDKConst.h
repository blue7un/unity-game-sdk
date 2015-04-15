//
//  AppotaSDKConst.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 7/1/14.
//  Copyright (c) 2014 DB-Interactive. All rights reserved.
//

#ifndef AppotaSDK_AppotaSDKConst_h
#define AppotaSDK_AppotaSDKConst_h


#define AppotaSDK_DEBUG NO

//#ifdef AppotaSDK_DEBUG
//#define OCLog(fmt, ...) NSLog((@"%s [Line %d] " fmt), __PRETTY_FUNCTION__, __LINE__, ##__VA_ARGS__)
//#ifndef AppotaSDK_DEBUG
//#define OCLog(fmt, ...)
//#endif
//#endif

typedef void (^AppotaSDKStringBlock)(NSString *returnString);
typedef void (^AppotaSDKArrayBlock)(NSArray *list);
typedef void (^AppotaSDKDictionaryBlock)(NSDictionary *dict);
typedef void (^AppotaSDKBOOLBlock)(BOOL b);
typedef void (^AppotaSDKIntBlock)(int t);
typedef void (^AppotaSDKVoidBlock) (void);
typedef void (^AppotaSDKObjectBlock) (id object);
typedef void (^AppotaSDKObjectMessageBlock) (id object, NSString* message);
typedef void (^AppotaSDKErrorBlock) (NSError *error);
typedef void (^AppotaSDKObjectMessageHandler) (id object, NSError *error , NSString *message);
typedef void (^AppotaSDKObjectHandler) (id object, NSError *error);

/*
 * Event handler cho kết quả trả về của Appota API
 */
//typedef void(^AppotaPaymentHandler)(NSDictionary *apiDict, AppotaPaymentState status, NSError *error);

/*
 * Event hander for Top-up in Webview
 */
//typedef void (^AppotaTopUpWebHandler)(BOOL status, NSDictionary *resultDict, AppotaTopupErrorCode error_code);
//
///*
// * Event handler cho AppotaEngine
// */
//typedef void (^AppotaRequestHandler)(AppotaEngine *appApi, AppotaRequestState status, NSError *error);

#define OC_SDK_VERSION @"1.1"
#define OC_SDK_BUILD @"1"
#define AppotaSDK_API_VERSION @"1.0"

//#define ONCLAN_APP_SCHEMA @"onclan"
//#define ONCLAN_ITUNES_LINK @"https://itunes.apple.com/app/id776649990"

#define APPOTA_DIALOG_PAYMENT_TAG 1430


#define APPOTA_SCHEMA @"appota"
#define APPOTA_DRAGVIEW_TAG 1428

#define APPOTA_OAUTH_REQUEST_TOKEN_URL @"https://id.appota.com/oauth/request_token"
#define APPOTA_FACEBOOK_OAUTH_REQUEST_TOKEN_URL @"https://id.appota.com/social/facebook_oauth?callback="

#define APPOTA_APP_REQUEST_TOKEN_URL @"https://id.appota.com/app/request_token"
#define APPOTA_OAUTH_ACCESS_TOKEN_URL @"https://id.appota.com/oauth/access_token"
#define APPOTA_APP_ACCESS_TOKEN_URL @"https://id.appota.com/app/access_token"
#define APPOTA_APP_REFRESH_TOKEN_URL @"https://id.appota.com/oauth/refresh_token"

//
#define APPOTA_LANGUAGE_KEY @"kAppotaLangKey"
#define APPOTA_SDK_VERSION @"4.0"
#define APPOTA_SDK_BUILD 40
#define APPOTA_GAME_DEVICE_TOKEN_KEY @"appota_game_push_device_token"
#define APPOTA_LOGIN_DICT_KEY_SAVED @"key_appota_login_dict_saved"
#define APPOTA_API_CONFIG_KEY @"appota_api_config_key"
#define APPOTA_USER_DEVICE_KEY @"gamota_samedevice_user"
//#define kAppotaCountryLocationKey @"kAppotaCountryLocationKey"

#define APPOTA_CLOSE_PUSH_KEY @"appota_post_close_notification"
#define APPOTA_NORMAL_LOGIN_USERNAME_KEY @"appota_normal_login_username_key"

//Debug mode
//#define DEBUG_MODE YES

#define DEBUG_APPOTA_RESOURCE NO

#define APPOTA_LOG NO
//#define APPOTA_BUILD_UNITY

// Device Macro"Giao dịch thất bại" = "Transaction failed";


#define IS_IOS6_AND_UP ([[UIDevice currentDevice].systemVersion floatValue] >= 6.0)

#define SYSTEM_VERSION_EQUAL_TO(v)                  ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedSame)
#define SYSTEM_VERSION_GREATER_THAN(v)              ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedDescending)
#define SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(v)  ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] != NSOrderedAscending)
#define SYSTEM_VERSION_LESS_THAN(v)                 ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedAscending)
#define SYSTEM_VERSION_LESS_THAN_OR_EQUAL_TO(v)     ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] != NSOrderedDescending)


//Device
#define IS_IPAD (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
#define IS_IPHONE (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone)
#define IS_IPHONE_4 (IS_IPHONE && [[UIScreen mainScreen] bounds].size.height == 480.0f)
#define IS_IPHONE_5 (IS_IPHONE && [[UIScreen mainScreen] bounds].size.height == 568.0f)
#define IS_IPHONE_6 (IS_IPHONE && [[UIScreen mainScreen] bounds].size.height == 667.f)
#define IS_IPHONE_6PLUS (IS_IPHONE && [[UIScreen mainScreen] bounds].size.height == 736.f)



//Orientation
#define IS_PORTRAIT ([UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationPortrait || [UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationPortraitUpsideDown)
#define IS_LANDSCAPE ([UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationLandscapeRight || [UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationLandscapeLeft)
#define LANDSCAPE_RIGHT ([UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationLandscapeRight)
#define LANDSCAPE_LEFT ([UIApplication sharedApplication].statusBarOrientation == UIDeviceOrientationLandscapeLeft)






#endif
