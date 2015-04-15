//
//  AppotaGameSDK+Advance.h
//  AppotaSDK
//
//  Created by Hieu on 12/25/14.
//
//

#import "AppotaGameSDK.h"
#import <FacebookSDK/FacebookSDK.h>

@class AppotaPaymentCollectionObject_V4;
@interface AppotaGameSDK (Advance)
/**
 *  get list payment object
 *
 *  @return NSArray
 */
+ (NSArray*) getListPaymentObject;
/**
 *  invite facebook friends
 */
+ (void)inviteFacebookFriends;
/**
 *  show facebook login
 */
+ (void) showFacebookLogin;
/**
 *  show google login
 */
+ (void) showGoogleLogin;
/**
 *  show Twitter login
 */
+ (void) showTwitterLogin;
@end
