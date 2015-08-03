//
//  AppotaGameSDK+Advance.h
//  AppotaSDK
//
//  Created by Hieu on 12/25/14.
//
//

#import "AppotaGameSDK.h"
#import "AppotaSDKConst.h"

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
+ (void)inviteFacebookFriendsWithCompleteBlock:(AppotaSDKDictionaryBlock) resultBlock
                                 andErorrBlock:(AppotaSDKErrorBlock) errorBlock;
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
/**
 *  Login Appota with username and password  use Delegate
 *
 *  @param userName
 *  @param passWord
 */
+ (void) loginAppotaWithUsername:(NSString *) userName
                        passWord:(NSString *) passWord;
/**
 *  Login Appota with username and password use block
 *
 *  @param userName
 *  @param passWord
 *  @param completionBlock
 *  @param errorBlock
 */
+ (void) loginAppotaWithUsername:(NSString *) userName
                        passWord:(NSString *) passWord
             withCompletionBlock:(AppotaSDKUserLoginResultObjectBlock ) completionBlock
                   andErrorBlock:(AppotaSDKErrorBlock ) errorBlock;
/**
 *  register Appota use Delegate for Callback success or
 *
 *  @param userName
 *  @param passWord
 *  @param email
 */
+ (void) registerAppotaWithUsername:(NSString *) userName
                           passWord:(NSString *) passWord
                           andEmail:(NSString *) email;
/**
 *  Register Appota use Block for Callback succees
 *
 *  @param userName
 *  @param passWord
 *  @param email
 *  @param coml
 */
+ (void) registerAppotaWithUsername:(NSString *) userName
                           passWord:(NSString *) passWord
                           andEmail:(NSString *) email
                   withComleteBlock:(AppotaSDKUserLoginResultObjectBlock) completionBlock
                      andErrorBlock:(AppotaSDKErrorBlock ) errorBlock;
@end
