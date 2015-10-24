using System;
using System.Collections.Generic;
using EWG.Domain.Entities;
using EWG.Domain.Entities.Dictionaries;
using EWG.Infrastructure.Dal.Repositories;
using EWG.Infrastructure.Services.ReplayParsing;
using EWG.Shared.Dto.ReplayParsing;

namespace EWG.Infrastructure.Services.Impl.ReplayParsing
{
    public class ReplayMapper : IReplayMapper
    {
        private readonly IGenericDictionaryItemRepository<GameMode> _gameModeRepository;
        private readonly IGenericDictionaryItemRepository<GameMap> _gameMapRepository;
        private readonly IGenericDictionaryItemRepository<GameType> _gameTypeRepository;
        private readonly IGenericDictionaryItemRepository<VictoryCondition> _victoryConditionRepository;
        private readonly IPlayerUserRepository _playerUserRepository;

        public ReplayMapper(
            IGenericDictionaryItemRepository<GameMode> gameModeRepository,
            IGenericDictionaryItemRepository<GameMap> gameMapRepository,
            IGenericDictionaryItemRepository<GameType> gameTypeRepository,
            IGenericDictionaryItemRepository<VictoryCondition> victoryConditionRepository,
            IPlayerUserRepository playerUserRepository)
        {
            _gameModeRepository = gameModeRepository;
            _gameMapRepository = gameMapRepository;
            _gameTypeRepository = gameTypeRepository;
            _victoryConditionRepository = victoryConditionRepository;
            _playerUserRepository = playerUserRepository;
        }

        public Replay GetEntity(ReplayParsedDto replayParsedDto)
        {
            var replay = new Replay
            {
                IsNetworkMode = Convert.ToBoolean(replayParsedDto.IsNetworkMode),
                Version = replayParsedDto.Version,
                GameMode = _gameModeRepository.GetByPublicCode(replayParsedDto.GameMode),
                GameMap = _gameMapRepository.GetByPublicCode(replayParsedDto.Map),
                MaxPlayers = replayParsedDto.NbMaxPlayer,
                AI = Convert.ToBoolean(replayParsedDto.NbAI),
                GameType = _gameTypeRepository.GetByPublicCode(replayParsedDto.GameType),
                Private = Convert.ToBoolean(replayParsedDto.Private),
                InitMoney = replayParsedDto.InitMoney,
                ScoreLimit = replayParsedDto.ScoreLimit,
                ServerName = replayParsedDto.ServerName,
                VictoryCondition = _victoryConditionRepository.GetByPublicCode(replayParsedDto.VictoryCond),
                NationConstraint = replayParsedDto.NationConstraint ?? "0",
                ThematicConstraint = replayParsedDto.ThematicConstraint ?? "0",
                DateConstraint = replayParsedDto.DateConstraint ?? "0",
                IncomeRate = int.Parse(replayParsedDto.IncomeRate),
                AllowObservers = Convert.ToBoolean(replayParsedDto.AllowNbObs),
                Seed = replayParsedDto.Seed
            };

            if (replay.GameMode == null)
            {
                throw new Exception(string.Format(
                    "Game mode not found ('{0}')",
                    replayParsedDto.GameMode)
                    );
            }

            if (replay.GameMap == null)
            {
                throw new Exception(string.Format(
                    "Game map not found ('{0}')",
                    replayParsedDto.Map)
                    );
            }

            if (replay.GameType == null)
            {
                throw new Exception(string.Format(
                    "Game type not found ('{0}')",
                    replayParsedDto.GameType)
                    );
            }

            if (replay.VictoryCondition == null)
            {
                throw new Exception(
                    string.Format("Victory condition not found  ('{0}')",
                    replayParsedDto.VictoryCond)
                    );
            }

            replay.Players = new List<Player>();
            foreach (var playerDto in replayParsedDto.Players)
            {
                var player = GetPlayer(playerDto);
                player.Replay = replay;

                replay.Players.Add(player);
            }

            return replay;
        }

        private Player GetPlayer(PlayerParsedDto playerParsedDto)
        {
            var player = new Player
            {
                PlayerElo = playerParsedDto.PlayerElo,
                PlayerRank = playerParsedDto.PlayerRank,
                PlayerLevel = playerParsedDto.PlayerLevel,
                PlayerName = playerParsedDto.PlayerName,
                PlayerTeamName = playerParsedDto.PlayerTeamName,
                PlayerAvatar = playerParsedDto.PlayerAvatar,
                PlayerIALevel = playerParsedDto.PlayerIALevel,
                PlayerReady = Convert.ToBoolean(playerParsedDto.PlayerReady),
                PlayerDeckName = playerParsedDto.PlayerDeckName,
                PlayerDeckContent = playerParsedDto.PlayerDeckContent,
                PlayerAlliance = playerParsedDto.PlayerAlliance,
                PlayerIsEnteredInLobby = Convert.ToBoolean(playerParsedDto.PlayerIsEnteredInLobby),
                PlayerScoreLimit = playerParsedDto.PlayerScoreLimit,
                PlayerIncomeRate = playerParsedDto.PlayerIncomeRate,
                PlayerNumber = playerParsedDto.PlayerNumber
            };

            var playerUser = _playerUserRepository.GetPlayerUserByEugenUserId(playerParsedDto.PlayerEugenId);
            if (playerUser == null)
            {
                playerUser = new PlayerUser
                {
                    EugenUserId = playerParsedDto.PlayerEugenId,
                    Name = playerParsedDto.PlayerName
                };

                _playerUserRepository.Save(playerUser);
            }

            player.PlayerUser = playerUser;

            return player;
        }
    }
}