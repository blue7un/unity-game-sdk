//
//  AppotaSDKTrackObject.h
//  AppotaSDK
//
//  Created by Hieu on 8/4/14.
//
//

#import "AppotaBaseObject.h"

#define kAPPOTA_CARRIER_ALL @"APPOTA_CARRIER_ALL"

typedef NS_ENUM(NSInteger, AppotaCountryCode) {
    AppotaCountryCodeVN,
    AppotaCountryCodeIndonesia,
    AppotaCountryCodeUnknown
};

@class AppotaLanguageObject;
@interface AppotaTrackObject : AppotaBaseObject
//get country code
- (NSString*) getCountryCode;
//get country code type
- (AppotaCountryCode) getCountryCodeType;
// validate track object
- (BOOL) isValidTrackObject;
//hide head profile
- (BOOL) isHideHeadProfile;
//check isDebugmode
- (BOOL) isDebugMode;
//check enable active code
- (BOOL) isEnableActiveCode;
// get GA tracking id
- (NSString*) getGATrackingID;
//get pid
- (NSString*) getPID;
// get list paymentMethod name
- (NSArray*) getListPaymentMethodName;

/**
 *
 *
 *  @return list of payment config
 */
- (NSArray*) getPaymentConfigList;
/**
 *  @return Payment config URL config on developer portal
 */
- (NSString*) getPaymentConfigURL;

/**
 *
 *
 *  @return List AppotaLanguageObject
 */
- (NSArray*) getListLanguageObject;

/**
 *
 *
 *  @param languageID
 *
 *  @return AppotaLanguageObject
 */
- (AppotaLanguageObject*) getLanguageObjectWithID:(NSString*) languageID;

/**
 *  Gameserver dict
 *
 *  @return game server dictionary
 */
- (NSDictionary*) getGameServerDict;
/**
 *  get Carrier name from CountryCode Type
 *
 *  @return carrier Name
 */
- (NSString *) getCarrierNameFromCountryCodeType;
@end
