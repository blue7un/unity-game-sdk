//
//  AppotaUserLoginResult.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 11/22/14.
//
//

#import "AppotaBaseObject.h"

@interface AppotaUserLoginResult : AppotaBaseObject
- (NSString*) userName;
- (NSString*) userID;
- (NSString*) accessToken;
- (NSString*) email;
- (BOOL) isRequireUpdateInfor;
- (BOOL) isHasEmail;
@end
