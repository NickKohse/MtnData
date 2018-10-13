BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `User` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT NOT NULL,
	`Email`	TEXT NOT NULL UNIQUE,
	`Username`	TEXT NOT NULL UNIQUE,
	`Password`	TEXT NOT NULL,
	`Type`	INTEGER NOT NULL
);
INSERT INTO `User` VALUES (1,'Nick','n@k.com','nick','pass',1);
INSERT INTO `User` VALUES (3,'nick','n@k.k','test','Password1',2);
INSERT INTO `User` VALUES (4,'qwe','qwe@qwe.qwe','test2','Password2',2);
INSERT INTO `User` VALUES (5,'yeet','yeet@yeet.yeet','yeet','Password1',2);
CREATE TABLE IF NOT EXISTS `TripReport` (
	`Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`UserId`	INTEGER NOT NULL,
	`DestId`	INTEGER NOT NULL,
	`Date`	TEXT NOT NULL,
	`Time`	INTEGER NOT NULL,
	`Result`	INTEGER NOT NULL,
	`Description`	INTEGER NOT NULL,
	FOREIGN KEY(`DestId`) REFERENCES `Destination`(`Id`),
	FOREIGN KEY(`UserId`) REFERENCES `User`(`Id`)
);
CREATE TABLE IF NOT EXISTS `Destination` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT NOT NULL UNIQUE,
	`Region`	TEXT,
	`EvGain`	INTEGER,
	`Distance`	REAL NOT NULL,
	`Coords`	TEXT,
	`EndCoords`	TEXT,
	`PDiff`	INTEGER NOT NULL,
	`TDiff`	INTEGER NOT NULL,
	`PeakEv`	INTEGER,
	`Verified`	INTEGER NOT NULL,
	`Description`	TEXT NOT NULL
);
INSERT INTO `Destination` VALUES (2,'TestLocation','TestRegion',1234,123.0,'NS: 33 EW: 33','NS: 33 EW: 32',5,5,5555,0,'This is a test region.');
INSERT INTO `Destination` VALUES (3,'TestLocation2','TestRegion2',1234,123.449996948242,'NS: 44.44 EW: -44.44','NS: -44.44 EW: 44.44',3,3,8848,0,'another test location');
INSERT INTO `Destination` VALUES (4,'TestLocation3','TestRegion3',123,222.2,'NS: 23 EW: 23','NS: 23 EW: 23',4,1,4444,0,'another one');
INSERT INTO `Destination` VALUES (5,'yeet','test region',222,222.22,'NS: 2 EW: 2','NS: 2 EW: 2',2,2,2222,0,'2222');
CREATE TABLE IF NOT EXISTS `Comment` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`DestId`	INTEGER NOT NULL,
	`UserId`	INTEGER NOT NULL,
	`Time`	INTEGER NOT NULL,
	`Text`	TEXT NOT NULL,
	FOREIGN KEY(`DestId`) REFERENCES `Destination`(`Id`),
	FOREIGN KEY(`UserId`) REFERENCES `User`(`Id`)
);
INSERT INTO `Comment` VALUES (1,2,5,1537837025,'test comment');
INSERT INTO `Comment` VALUES (2,2,5,1537837258,'test comment2');
INSERT INTO `Comment` VALUES (3,2,5,1537837379,'test comment3');
INSERT INTO `Comment` VALUES (4,2,5,1537837385,'test comment3');
INSERT INTO `Comment` VALUES (5,3,3,1538871662,'Comment1');
CREATE TABLE IF NOT EXISTS `Admins` (
	`UserId`	INTEGER NOT NULL,
	`Region`	TEXT NOT NULL,
	FOREIGN KEY(`UserId`) REFERENCES `User`(`Id`)
);
COMMIT;
