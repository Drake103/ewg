/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     15.04.2015 22:56:56                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Players') and o.name = 'FK_PLAYER_REF_PLAYERUSER')
alter table Players
   drop constraint FK_PLAYER_REF_PLAYERUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Players') and o.name = 'FK_PLAYER_REF_REPLAY')
alter table Players
   drop constraint FK_PLAYER_REF_REPLAY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Replays') and o.name = 'FK_REPLAY_REF_GAMEMAP')
alter table Replays
   drop constraint FK_REPLAY_REF_GAMEMAP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Replays') and o.name = 'FK_REPLAY_REF_GAMEMODE')
alter table Replays
   drop constraint FK_REPLAY_REF_GAMEMODE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Replays') and o.name = 'FK_REPLAY_REF_GAMETYPE')
alter table Replays
   drop constraint FK_REPLAY_REF_GAMETYPE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Replays') and o.name = 'FK_REPLAY_REF_VICTORYCONDITION')
alter table Replays
   drop constraint FK_REPLAY_REF_VICTORYCONDITION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DicGameMaps')
            and   type = 'U')
   drop table DicGameMaps
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DicGenericDictionaryItems')
            and   type = 'U')
   drop table DicGenericDictionaryItems
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IdentityUsers')
            and   type = 'U')
   drop table IdentityUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PlayerUsers')
            and   type = 'U')
   drop table PlayerUsers
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Players')
            and   name  = 'idx_players_replayId'
            and   indid > 0
            and   indid < 255)
   drop index Players.idx_players_replayId
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Players')
            and   type = 'U')
   drop table Players
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Replays')
            and   name  = 'idx_replays_fileHash'
            and   indid > 0
            and   indid < 255)
   drop index Replays.idx_replays_fileHash
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Replays')
            and   type = 'U')
   drop table Replays
go

/*==============================================================*/
/* Table: DicGameMaps                                           */
/*==============================================================*/
create table DicGameMaps (
   id                   smallint             identity,
   name                 nvarchar(40)         not null,
   resourceName         nvarchar(40)         not null,
   publicCode           nvarchar(40)         not null,
   dateStart            datetime             not null,
   dateEnd              datetime             null,
   constraint PK_DICGAMEMAPS primary key (id)
)
go

/*==============================================================*/
/* Table: DicGenericDictionaryItems                             */
/*==============================================================*/
create table DicGenericDictionaryItems (
   id                   smallint             identity,
   discriminator        nvarchar(40)         not null,
   name                 nvarchar(40)         not null,
   resourceName         nvarchar(40)         not null,
   publicCode           nvarchar(40)         not null,
   dateStart            datetime             not null,
   dateEnd              datetime             null,
   constraint PK_DICGENERICDICTIONARYITEMS primary key (id)
)
go

/*==============================================================*/
/* Table: IdentityUsers                                         */
/*==============================================================*/
create table IdentityUsers (
   id                   int                  identity,
   email                nvarchar(80)         not null,
   username             nvarchar(80)         not null,
   hashedPassword       nvarchar(80)         not null,
   constraint PK_IDENTITYUSERS primary key (id)
)
go

/*==============================================================*/
/* Table: PlayerUsers                                           */
/*==============================================================*/
create table PlayerUsers (
   id                   int                  identity,
   eugenUserId          int                  not null,
   name                 nvarchar(50)         not null,
   constraint PK_PLAYERUSERS primary key (id)
)
go

/*==============================================================*/
/* Table: Players                                               */
/*==============================================================*/
create table Players (
   id                   int                  identity,
   replayId             int                  not null,
   playerNumber         smallint             not null,
   playerUserId         int                  not null,
   playerElo            numeric(15,8)        not null,
   playerRank           int                  not null,
   playerLevel          smallint             not null,
   playerName           nvarchar(50)         not null,
   playerTeamName       nvarchar(50)         not null,
   playerAvatar         nvarchar(255)        not null,
   playerIaLevel        nvarchar(50)         not null,
   playerReady          bit                  not null,
   playerDeckName       nvarchar(50)         not null,
   playerDeckContent    nvarchar(255)        not null,
   playerAlliance       smallint             not null,
   playerIsEnteredInLobby bit                  not null,
   playerScoreLimit     int                  not null,
   playerIncomeRate     int                  not null,
   constraint PK_PLAYERS primary key (id)
)
go

/*==============================================================*/
/* Index: idx_players_replayId                                  */
/*==============================================================*/
create index idx_players_replayId on Players (
replayId ASC
)
go

/*==============================================================*/
/* Table: Replays                                               */
/*==============================================================*/
create table Replays (
   id                   int                  identity,
   title                nvarchar(80)         not null,
   uploadDate           datetime             not null,
   isNetworkMode        bit                  not null,
   version              nvarchar(20)         not null,
   gameMapId            smallint             not null,
   gameModeId           smallint             not null,
   maxPlayers           smallint             not null,
   ai                   bit                  not null,
   gameTypeId           smallint             not null,
   private              bit                  not null,
   initMoney            int                  not null,
   scoreLimit           int                  not null,
   serverName           nvarchar(63)         null,
   victoryConditionId   smallint             not null,
   nationConstraint     nvarchar(20)         not null,
   thematicConstraint   nvarchar(20)         not null,
   dateConstraint       nvarchar(20)         not null,
   incomeRate           smallint             not null,
   allowObservers       bit                  not null,
   seed                 nvarchar(40)         not null,
   link                 nvarchar(255)        not null,
   fileHash             uniqueidentifier     not null,
   downloadsCounter     int                  not null,
   constraint PK_REPLAYS primary key (id)
)
go

/*==============================================================*/
/* Index: idx_replays_fileHash                                  */
/*==============================================================*/
create unique index idx_replays_fileHash on Replays (
fileHash ASC
)
include (id)
go

alter table Players
   add constraint FK_PLAYER_REF_PLAYERUSER foreign key (playerUserId)
      references PlayerUsers (id)
go

alter table Players
   add constraint FK_PLAYER_REF_REPLAY foreign key (replayId)
      references Replays (id)
go

alter table Replays
   add constraint FK_REPLAY_REF_GAMEMAP foreign key (gameMapId)
      references DicGameMaps (id)
go

alter table Replays
   add constraint FK_REPLAY_REF_GAMEMODE foreign key (gameModeId)
      references DicGenericDictionaryItems (id)
go

alter table Replays
   add constraint FK_REPLAY_REF_GAMETYPE foreign key (gameTypeId)
      references DicGenericDictionaryItems (id)
go

alter table Replays
   add constraint FK_REPLAY_REF_VICTORYCONDITION foreign key (victoryConditionId)
      references DicGenericDictionaryItems (id)
go

