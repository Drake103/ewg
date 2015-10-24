INSERT INTO [dbo].[DicGenericDictionaryItems] (discriminator, name, resourceName, publicCode, dateStart, dateEnd)
VALUES
	(N'GameMode', N'gameMode00', N'gameMode00', N'0', GETDATE(), null),
	(N'GameMode', N'gameMode01', N'gameMode01', N'1', GETDATE(), null),
	(N'GameMode', N'gameMode02', N'gameMode02', N'2', GETDATE(), null),
    (N'GameMode', N'gameMode03', N'gameMode03', N'3', GETDATE(), null),
	
	(N'GameType', N'gameType00', N'gameType00', N'0', GETDATE(), null),
	(N'GameType', N'gameType01', N'gameType01', N'1', GETDATE(), null),
	(N'GameType', N'gameType02', N'gameType02', N'2', GETDATE(), null),
	
	(N'VictoryCondition', N'VictoryCondition00', N'VictoryCondition00', N'0', GETDATE(), null),
	(N'VictoryCondition', N'Destruction', N'Destruction', N'1', GETDATE(), null),
	(N'VictoryCondition', N'VictoryCondition02', N'VictoryCondition02', N'2', GETDATE(), null),
    (N'VictoryCondition', N'VictoryCondition03', N'VictoryCondition03', N'3', GETDATE(), null),
    (N'VictoryCondition', N'Conquest', N'Conquest', N'4', GETDATE(), null)

INSERT INTO [dbo].[DicGameMaps] (name, resourceName, publicCode, dateStart, dateEnd)
VALUES
	(N'Conquete_2x3_Tohoku', N'Conquete_2x3_Tohoku', N'Conquete_2x3_Tohoku', GETDATE(), null),
	(N'Conquete_3x3_Gangjin', N'Conquete_3x3_Gangjin', N'Conquete_3x3_Gangjin', GETDATE(), null),
	(N'Conquete_3x2_Sangju', N'Conquete_3x2_Sangju', N'Conquete_3x2_Sangju', GETDATE(), null),
    (N'Destruction_3x3_Muju', N'Destruction_3x3_Muju', N'Destruction_3x3_Muju', GETDATE(), null),
    (N'Destruction_5x3_Marine_1_small_Terrestre', N'Destruction_5x3_Marine_1_small_Terrestre', N'Destruction_5x3_Marine_1_small_Terrestre', GETDATE(), null),
    (N'Conquete_3x3_Muju', N'Conquete_3x3_Muju', N'Conquete_3x3_Muju', GETDATE(), null),
    (N'Conquete_2x2_port_Wonsan_Terrestre', N'Conquete_2x2_port_Wonsan_Terrestre', N'Conquete_2x2_port_Wonsan_Terrestre', GETDATE(), null),
    (N'Conquete_2x3_Hwaseong', N'Conquete_2x3_Hwaseong', N'Conquete_2x3_Hwaseong', GETDATE(), null),
    (N'Conquete_3x2_Taean', N'Conquete_3x2_Taean', N'Conquete_3x2_Taean', GETDATE(), null)