//
//  AppotaGameSDK.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/19/14.
//
//

#import <Foundation/Foundation.h>
#import "AppotaGameSDKCallback.h"
#import "AppotaSDKConst.h"

@interface AppotaGameSDK : NSObject

/**
 *  Singleton method return AppotaGameDK instance
 *  AppotaGameSDK instance will be used during application lifecycle
 *  @return AppotaGameSDK object
 */
+ (instancetype)sharedInstance;

#pragma mark -- Configuration method

/**
 *  enable/disable show welcome like game center
 *
 *  @param property : YES -> hide, no -> show Welcome View
 */
- (void) setHideWelcomeView:(BOOL) property;
/**
 *  Show, hide payment button
 *
 *  @param isVisble
 */
- (void) setSDKButtonVisibility:(BOOL)isVisble;
/**
 *  set keep login session
 *
 *  @param isVisble
 */
- (void) setKeepLoginSession:(BOOL) isKeepLoginSession;
/**
 *  Flag to show or disable auto show login dialog at application startup
 */
- (void) setAutoShowLoginDialog:(BOOL)autoShowLoginDialog;
/**
 *  Delegate for callback function to handle PAYMENT, LOGIN callback
 */
@property (nonatomic, weak) id<AppotaGameSDKCallback> delegate;



#pragma mark -- SDK feature method

#pragma mark -- Init feature
/**
 *  Function to configure AppotaGameSDK when application start
 *  Should be placed in "AppDelegate.m" - "application:didFinishLaunchingWithOptions:"
 */
+ (void) configure;

#pragma mark -- Login feature
/**
 *  show LoginView
 */
+ (void) showLoginView;
/**
 *  show RegisterView
 */
+ (void) showRegisterView;

/**
 *  switch Account function
 */
+ (void) switchAccount;
/**
 *  Function to check user login state
 *
 *  @return YES - NO based on user login state
 */
+ (BOOL) isUserLoggedIn;

/**
 *  Logout function.
 */
+ (void) logOut;
/**
 *  get User Infor
 *
 *  @return AppotaUserLoginResult object
 */
+ (AppotaUserLoginResult *) getUserInfo;

/**
 *  show history of transaction
 */
+(void)showTransactionHistory;

/**
 *  show user profile view
 */
+ (void) showUserInfoView;

#pragma mark -- Payment feature
/**
 *  show payment View
 */
+ (void) showPaymentView;
/**
 *  Show payment View with packageId
 *
 *  @param packageID :NSString
 */
+ (void) showPaymentViewWithPackageID:(NSString *)packageID;
/**
 *  Close payment View 
 *
 *  @param:  non
 */
+ (void) closePaymentView;

#pragma mark -- Tracking features
/**
 *  tracking
 *
 *  @param categoryName : Required = YES . Description: The event category
 *  @param action       : Required = YES . Description: The event action
 *  @param label        : Required = NO  . Description: The event label
 */
+ (void)sendEventWithCategory:(NSString *)categoryName withEventAction:(NSString *)action withLabel:(NSString *)label;
/**
 *  tracking
 *
 *  @param categoryName : Required = YES . Description: The event category
 *  @param action       : Required = YES . Description: The event action
 *  @param label        : Required = NO  . Description: The event label
 *  @param value        : Required = NO  . Description: The event value
 */
+ (void) sendEventWithCategory:(NSString*) categoryName withEventAction:(NSString*) action withLabel:(NSString*) label withValue:(int) value;
/**
 *
 *
 *  @param viewName :  Required = YES . Description: NSString View name
 */
+ (void) sendViewWithName:(NSString*) viewName;

#pragma mark -- Other function class and properties
/**
 *  Handle open url called in AppDelegate - function application:openURL:sourceApplication:annotation:
 *
 *  @param url               url callback
 *  @param sourceApplication source Application
 *  @param annotation
 *
 *  @return Callback state
 */
+ (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL*) url
     sourceApplication:(NSString*) sourceApplication
            annotation:(id) annotation;

#pragma mark -- Notification
/**
 *  Register push notification with Group Name
 */
+ (void) registerPushNotificationWithGroupName:(NSString*) groupName;
/**
 *  Register push notification with Group Name
 */
+ (NSString*) configurePushNotificationWithTokenData:(NSData*) deviceTokenData;
/**
 *  handle push notification with Group Name
 */
+ (void) handlePushNotification:(NSDictionary *)receiveDictionary;
/**
 *  Set Character;
 */
+ (void) setCharacterWithCharacterName:(NSString *)characterName characterID:(NSString *)characterID
               serverName:(NSString *)serverName serverID:(NSString *)serverID
          onCompleteBlock:(AppotaSDKDictionaryBlock ) completeBlock
             onErrorBlock:(AppotaSDKErrorBlock ) errorBlock;
@end
