//
//  AppotaGameSDK+Internal.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/20/14.
//
//

#import "AppotaGameSDK.h"
#import "AppotaSDKConst.h"
/**
 State of AppotaGameSDK to provide correct GUI
 */
#ifdef APPOTAGameSDK_BUILD_ANE
#ifdef __cplusplus
extern "C" {
#endif
    typedef void *      FREContext;
#ifdef __cplusplus
}
#endif
#endif

typedef enum {
    AppotaGameSDKNormalState,
    AppotaGameSDKUserLoginState,
    AppotaGameSDKLoadingState
} AppotaGameSDKState;
@class AppotaTrackObject;
@interface AppotaGameSDK (Internal)

#pragma mark - Getter function
+ (AppotaGameSDKState) getAppotaGameSDKState;

+ (NSString*) getAppotaAPIKey;

+ (NSString*) getGoogleClientID;

+ (NSString*) getGoogleClientSecret;

+ (NSString*) getTwitterConsumerKey;

+ (NSString*) getTwitterConsumerSecret;

+ (NSString*) getPaymentTarget;

+ (NSString*) getValidDNS;
/**
 *  Get list payment - return list of AppotaSDKPaymentCollection object
 *  Parsed with JSON value and payment config option
 *  @return list of AppotaSDKPaymentCollection
 */
+ (NSArray*) getListPayment;

#ifdef APPOTAGameSDK_BUILD_ANE
+ (FREContext )getANEContext;
+ (void) setANEContext:(FREContext )context;
#endif

/**
 *  Get list payment - return list of AppotaSDKPaymentCollection object
    with packageID
 *  Parsed with JSON value and payment config option
 *  @return list of AppotaSDKPaymentCollection
 */
+ (NSArray *) getListPaymentWithPackageID:(NSString *)packageID;
#pragma mark - Init function
/**
 *  Load remote config (from track and json config all at once and then update them to database)
 */
+ (void) loadRemoteConfig;

+ (void) getAndSetTrackObjectWithCompletionBlock:(AppotaSDKObjectBlock) completionBlock
                                  withErrorBlock:(AppotaSDKErrorBlock) errorBlock;

// Init with trackobjet function
- (void) initAfterRetrieveFirstTrackObject:(AppotaTrackObject*) trackObject;
//show check update
- (void) checkUpdateWithDict:(NSDictionary *)dictionary;
/**
 *
 *  Handle facebook url in AppDelegate
 *  @param url open url callback
 */
+ (void) handleFacebookOpenURL:(NSURL*) url;

+ (void) handleAppotaOpenURL:(NSURL*) url;

/**
 *  Error stacktrace function
 */
- (void) addErrorStackTrace;
@end
