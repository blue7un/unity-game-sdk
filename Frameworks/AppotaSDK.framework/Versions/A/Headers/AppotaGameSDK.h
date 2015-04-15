//
//  AppotaGameSDK.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/19/14.
//
//

#import <Foundation/Foundation.h>
#import "AppotaGameSDKCallback.h"

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
 *  @param property : YES -> show, no -> don't show
 */
- (void) setHideWelcomeView:(BOOL) property;
/**
 *  Show, hide payment button
 *
 *  @param isVisble
 */
- (void) setSDKButtonVisible:(BOOL)isVisble;
/**
 *  set keep login session
 *
 *  @param isVisble
 */
- (void) setKeepLoginSession:(BOOL) isKeepLoginSession;
/**
 *  Flag to show or disable auto show login dialog at application startup
 */
@property (readwrite) BOOL autoShowLoginDialog;
/**
 *  Delegate for callback function to handle PAYMENT, LOGIN callback
 */
@property (nonatomic, unsafe_unretained) id<AppotaGameSDKCallback> delegate;



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
+ (BOOL) checkUserLogin;

/**
 *  Logout function
 */
+ (void) logOut;
/**
 *  get User Infor
 *
 *  @return AppotaUserLoginResult object
 */
+ (AppotaUserLoginResult *) getUserInfo;

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
+ (void) showPaymentViewWithPackage:(NSString *)packageID;
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
 *  @param value        : Required = NO  . Description: The event value
 */
+ (void) sendEventWithCategory:(NSString*) categoryName withEventAction:(NSString*) action withLabel:(NSString*) label withValue:(NSNumber*) value;
/**
 *
 *
 *  @param viewName :  Required = YES . Description: NSString View name
 */
+ (void) sendViewWithName:(NSString*) viewName;

#pragma mark -- Other features
/**
 *  show history of transaction
 */
+(void)showTransactionHistory;

/**
 *  show user profile view
 */
+ (void) showProfileView;

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
+ (BOOL) handleOpenURL:(NSURL*) url
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
@end
