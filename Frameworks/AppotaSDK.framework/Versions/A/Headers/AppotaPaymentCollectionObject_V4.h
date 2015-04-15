//
//  AppotaSDKPayment_V4.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 07.11.14.
//
//

#import "AppotaBaseObject.h"

typedef enum {
    PAYMENT_SMS = 0,
    PAYMENT_PREMIUM_SMS = 1,
    PAYMENT_CARD = 2,
    PAYMENT_INTERNET_BANKING = 3,
    PAYMENT_PAYPAL = 4,
    PAYMENT_UNIPIN = 7,
    PAYMENT_APPLE = 8,
    PAYMENT_EWALLET = 9,
    PAYMENT_METHOD_INVALID = 10,
} PAYMENT_METHOD;

typedef NS_ENUM(NSInteger, SMSTYPE) {
    SMSTYPE_UNKNOW = -1,
    SMSTYPE_MOMT,
    SMSTYPE_OTP,
    SMSTYPE_DIRECT
};
// Collection of payment object with same payment machenism
@interface AppotaPaymentCollectionObject_V4 : AppotaBaseObject {
    
}

- (id) initWithPaymentMethod:(PAYMENT_METHOD) paymentMethod;

@property (nonatomic, strong) NSArray *listAmount;
@property (readonly) PAYMENT_METHOD paymentMethod;

- (NSString*) getPaymentCollectionImageName;
- (NSString*) getPaymentCollectionName;
- (NSString*) getVendorName;

+(PAYMENT_METHOD) getPaymentMethodFromString:(NSString*) pMethod;
+ (NSString*) getStringFromPaymentMethod:(PAYMENT_METHOD) pMethod;
- (SMSTYPE)getSMSType;
@end
