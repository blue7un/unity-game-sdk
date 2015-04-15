//
//  AppotaPaymentPackage_V4.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 1/27/15.
//
//

#import "AppotaBaseObject.h"
#import "AppotaPaymentCollectionObject_V4.h"

typedef enum : NSUInteger {
    AppotaGameCurrencyStringType,
    AppotaGameCurrencyImageType,
} AppotaGameCurrencyType;

@interface AppotaGameCurrency : NSObject
@property (readonly) AppotaGameCurrencyType type;
@property (nonatomic, strong) NSString *data;
@end

@interface AppotaPaymentPackage_V4 : AppotaBaseObject
@property (readwrite) PAYMENT_METHOD selectedPaymentMethod;
- (AppotaGameCurrency*) getGameCurrency;
- (int) getPackageAmount;
- (NSString *) getMoneyCurrencyWithPaymentMethod:(NSString *) paymentMethod;
- (NSString *) getMoneyAmountWithPaymentMethod:(NSString *) paymentMethod;
- (NSString *) getContryCodeWithPaymentMethod:(NSString *) paymentMethod;
- (NSArray *) getListPaymentMethod;
-(NSString *)getProductAppleID;

@end
