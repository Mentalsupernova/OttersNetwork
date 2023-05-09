create table if not exists UrlRecord(
     RecordId SERIAL Primary Key 
    ,RecordUrl text NOT NULL
    ,RecordDescription TEXT NULL
    ,RecordKeyWords TEXT NULL
    ,RecordWebsiteType TEXT NULL
    ,RecordMood TEXT NULL
    ,RecordColorScheme TEXT NULL
    ,RecordImage bytea NULL 
    ,RecordImageProcessed INTEGER NOT NULL 
    ,RecordTaken INTEGER  NOT NULL 
);
