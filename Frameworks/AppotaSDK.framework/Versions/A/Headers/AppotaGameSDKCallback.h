//
//  AppotaSDKCallback.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/18/14.
//
//

#import <Foundation/Foundation.h>
#import "AppotaPaymentResult.h"
#import "AppotaUserLoginResult.h"

@protocol AppotaGameSDKCallback <NSObject>
@optional
//- (void) didFinishRegister:(NSDictionary*) userInfoDict;
/*
 * Callback after close loginview
 */
- (void) didCloseLoginView;
@required


/**
 *  Get payment state base on AppotaPaymentPackage
 *
 *  @return PAYMENT_STATE
 */

- (NSString*) getPaymentState:(NSString *) packageID;

/*
 * Callback after login
 */
- (void) didFinishLogin:(AppotaUserLoginResult*) userLoginResult;
/*
 * Callback when login error
 */
- (void) didLoginErrorWithMessage:(NSString *)message withError:(NSError *)error;
/*
 * Callback after logout
 */
- (void) didLogOut:(NSString*) userName;

- (void) didPaymentSuccessWithResult:(AppotaPaymentResult*) paymentResult withPackage:(NSString *) packageID;

- (void) didPaymentErrorWithMessage:(NSString *)message withError:(NSError *)error;


@end