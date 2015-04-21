//
//  AppotaPaymentResult.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/19/14.
//
//

#import <Foundation/Foundation.h>

#import "AppotaBaseObject.h"

@interface AppotaPaymentResult : AppotaBaseObject
- (float)getAmountPaymentResult;
- (NSString *)transactionID;
- (NSString *)type;
- (NSString *)currency;
- (NSString *)time;
- (NSString *)packageID;
//Payment Apple
- (NSString *)appleProductID;
@end
