//
//  AppotaSDKDevConfigObject.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 10.11.14.
//
//

#import "AppotaBaseObject.h"

typedef enum {
    AppotaLoginNormalMethod,
    AppotaQuickLoginMethod,
    AppotaLoginSocialMethod,
    AppotaLoginFBMethod,
    AppotaGoogleMethod,
    AppotaTwitterMethod,
    AppotaLoginOnclanMethod,
} AppotaLoginMethod;

@class AppotaPaymentCollectionObject_V4;
@class AppotaPaymentPackage_V4;
@class AppotaTrackObject;

@interface AppotaDevConfigObject : AppotaBaseObject
- (NSArray*) getListLoginMethod;

- (NSArray*) getListPaymentPackage;
//TODO: HIEUTRINH
- (NSMutableArray *) getListPackageID;

- (BOOL) isValidPackageID:(NSString *)packageID;

- (AppotaPaymentPackage_V4 *) getPackagewithItPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithListPaymentMethods:(NSArray*) listPaymentMethods
                                        withCountryCode:(NSString*) countryCode;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod withPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithMethod:(NSString*) paymentMethod
                            withCountryCode:(NSString*) countryCode;

- (NSArray*) filterPackageWithPaymentCollection:(AppotaPaymentCollectionObject_V4*) paymentCollection andAppotaTrackObject:(AppotaTrackObject *) trackObject;

- (NSArray*) filterPackageWithPaymentCollection:(AppotaPaymentCollectionObject_V4*) paymentCollection  andAppotaTrackObject:(AppotaTrackObject *)trackObject withPackageID:(NSString *)packageID;

//- (NSArray*) getListPaymentMethods;
//- (NSString*) getMoneyCurrencyWithPaymentMethod:(PAYMENT_METHOD) pMethod
//                                 withContryCode:(NSString*) countryCode;
//- (NSArray*) getListAmountWithPaymentMethod:(PAYMENT_METHOD) pMethod
//                            withCountryCode:(NSString*) countryCode;
//- (AppotaGameCurrency*) getGameCurrency;
@end
