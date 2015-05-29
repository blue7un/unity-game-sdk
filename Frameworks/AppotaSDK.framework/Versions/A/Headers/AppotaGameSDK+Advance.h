//
//  AppotaGameSDK+Advance.h
//  AppotaSDK
//
//  Created by Hieu on 12/25/14.
//
//

#import "AppotaGameSDK.h"
#import "AppotaSDKConst.h"
#import <FacebookSDK/FacebookSDK.h>

typedef void (^AppotaInviteFriendBlock) (FBWebDialogResult result, NSURL *resultURL, NSError *error);

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
+ (void)inviteFacebookFriendsWithCompleteBlock:(AppotaInviteFriendBlock) inviteBlock;
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
