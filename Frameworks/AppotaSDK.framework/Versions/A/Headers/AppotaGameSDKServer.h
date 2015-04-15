//
//  AppotaGameSDKServer.h
//  AppotaSDK
//
//  Created by Tue Nguyen on 1/16/15.
//
//

#import <Foundation/Foundation.h>

@interface AppotaGameServer : NSObject
@property (nonatomic, readonly) NSString *zoneID;
@property (nonatomic, readonly) NSString *zoneName;
@property (nonatomic, readonly) NSString *serverID;
@property (nonatomic, readonly) NSString *serverAddress;
@property (nonatomic, readonly) NSString *serverName;
@property (nonatomic, readonly) NSString *serverDisplayName;
@end

@interface AppotaGameSDKServer : NSObject
+ (NSArray*) getListGameServer;
@end
