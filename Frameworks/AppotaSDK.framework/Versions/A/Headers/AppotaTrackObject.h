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
- (NSString*) getCountryCode;
- (AppotaCountryCode) getCountryCodeType;
- (BOOL) isHideHeadProfile;
- (BOOL) isDebugMode;
- (NSString*) getGATrackingID;
- (NSString*) getPID;
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
