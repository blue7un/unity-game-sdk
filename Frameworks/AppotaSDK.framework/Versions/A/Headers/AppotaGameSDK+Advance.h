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
typedef void (^AppotaSDKUserLoginResultObjectBlock) (AppotaUserLoginResult *object);
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

+ (void) showFacebookLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                              andErrorBlock:(AppotaSDKErrorBlock) errorBlock;

+ (void) showFacebookLoginWithPermissions:(NSArray *)permissions
                     andWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  show google login
 */
+ (void) showGoogleLogin;

+ (void) showGoogleLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  show Twitter login
 */
+ (void) showTwitterLogin;
+ (void) showTwitterLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                             andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
@end
