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
/** *
 *  get list payment object

 *  @return NSArray
 */
+ (NSArray*) getListPaymentObject;
/**
 *  invite facebook friends
 */
+ (void)inviteFacebookFriendsWithCompleteBlock:(AppotaSDKDictionaryBlock) resultBlock
                                 andErorrBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  Show facebook login without Appota login view use Delegate
 */
+ (void) showFacebookLogin;
/**
 *  Show facebook Login without Appota Login View use Block
 *`
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showFacebookLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                              andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  Show facebook Login without Appota Login View with permissions use Block
 *
 *  @param permissions
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showFacebookLoginWithPermissions:(NSArray *)permissions
                     andWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  show google login without Appota Login View use Delegate
 */
+ (void) showGoogleLogin;
/**
 *  show google login without Appota Login View use Block
 *
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showGoogleLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  show Twitter login without Appota Login view use Delegate
 */
+ (void) showTwitterLogin;
/**
 *  Show Twitter login without Appota Login view use Block
 *
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showTwitterLoginWithCompleteBlock:(AppotaSDKUserLoginResultObjectBlock) completeBlock
                             andErrorBlock:(AppotaSDKErrorBlock) errorBlock;
/**
 *  Login Appota without Appota Login View with username and password  use Delegate
 *
 *  @param userName
 *  @param passWord
 */
+ (void) loginAppotaWithUsername:(NSString *) userName
                        passWord:(NSString *) passWord;
/**
 *  Login Appota  without Appota Login View with username and password use block
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
 *  register Appota  without Appota Login View use Delegate for Callback success
 *
 *  @param userName
 *  @param passWord
 *  @param email
 */
+ (void) registerAppotaWithUsername:(NSString *) userName
                           passWord:(NSString *) passWord
                           andEmail:(NSString *) email;
/**
 *  Register Appota   without Appota Login View use Block for Callback succees
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
/**
 *  quick login without Appota Loginview use Delegate for callback success
 */
+ (void) quickLogin;
/**
 *  Quick login without Appota loginView use Blocking
 *
 *  @param comletionBlock
 *  @param errorBlock
 */
+ (void) quickLoginWithComleteBlock:(AppotaSDKUserLoginResultObjectBlock) comletionBlock
                     andErrorBlock:(AppotaSDKErrorBlock) errorBlock;

@end
