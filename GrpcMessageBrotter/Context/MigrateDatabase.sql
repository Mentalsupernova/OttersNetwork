create table if not exists UrlRecord(
     RecordId INTEGER Primary Key
    ,RecordUrl TEXT NOT NULL
    ,RecordDescription TEXT NULL
    ,RecordKeyWords TEXT NULL
    ,RecordWebsiteType TEXT NULL
    ,RecordMood TEXT NULL
    ,RecordColorScheme TEXT NULL
    ,RecordImage blob NULL 
);
