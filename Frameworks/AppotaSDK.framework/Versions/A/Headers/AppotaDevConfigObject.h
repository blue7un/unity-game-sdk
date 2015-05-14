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
// get list login method
- (NSArray*) getListLoginMethod;
// check valid devconfig object :
- (BOOL) isValidDevConfigObject;
// check if has Login method
- (BOOL) isHasLoginMethod:(AppotaLoginMethod ) loginMethod;
//get listPayment package
- (NSArray*) getListPaymentPackage;
//get list packageId
- (NSMutableArray *) getListPackageID;
//check valid packageId
- (BOOL) isValidPackageID:(NSString *)packageID;
//get package with packageId
- (AppotaPaymentPackage_V4 *) getPackageWithItPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithListPaymentMethods:(NSArray*) listPaymentMethods
                                        withCountryCode:(NSString*) countryCode;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod withPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithMethod:(NSString*) paymentMethod
                            withCountryCode:(NSString*) countryCode;

- (NSArray*) filterPackageWithPaymentCollection:(AppotaPaymentCollectionObject_V4*) paymentCollection andAppotaTrackObject:(AppotaTrackObject *) trackObject;

- (NSArray*) filterPackageWithPaymentCollection:(AppotaPaymentCollectionObject_V4*) paymentCollection  andAppotaTrackObject:(AppotaTrackObject *)trackObject withPackageID:(NSString *)packageID;

@end
