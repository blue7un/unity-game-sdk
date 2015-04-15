//
//  ApplicationData.h
//  Appvn
//
//  Created by tuent on 2/6/14.
//  Copyright (c) 2014 Appota. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface AppotaBaseObject : NSObject <NSCoding>
@property (nonatomic, copy) NSDictionary* objectDict;

/**
 *  Support function to get array of value from key and array of object
 *
 *  @param key        key description
 *  @param listObject listObject description
 *
 *  @return return value array
 */
+ (NSArray*) createListValueWithKey:(NSString*) key
                     withListObject:(NSArray*) listObject;

// Public function for init and validate
+ (NSArray*) createListDataFromListDict:(NSArray*) listDict;

+ (BOOL) validateObjectDict:(NSDictionary*) objectDict;

// Life circle function
- (NSString*)description;
- (id) initWithObjectDict:(NSDictionary*) applicationDict;
- (id) objectForKey:(NSString*) key;
- (NSString*) getObjectMessage;

// Encoding function
// Save object to default database with default key
+ (void) archiveObject:(id) object;

// Save object to default database with provided key
+ (void) archiveObject:(id) object
               withKey:(NSString*) key;

// Get object from default database with class
+ (id) getArchivedObjectWithClass:(Class) c;

// Get object from default datase with key
+ (id) getArchiveObjectWithKey:(NSString*) key;

+ (NSString*) getObjectKeyFromClass:(Class) c;
@end
