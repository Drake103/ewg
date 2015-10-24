using System;
using System.Collections.Generic;
using System.Linq;
using EWG.Domain.Entities;
using EWG.Infrastructure.Dal.Repositories;
using EWG.Shared.Dto;
using EWG.Shared.Dto.Replays;

namespace EWG.Infrastructure.Dal.Impl.Repositories
{
    public class ReplayRepository : GenericRepository<Replay>, IReplayRepository
    {
        public ReplayRepository(ICrudRepository<Replay> crud) : base(crud)
        {
        }

        public IList<ReplayDto> GetForList(PagingInfo pagingInfo, string searchText)
        {
            var query = _crud.Get();

            query = FilterBySearchText(query, searchText).OrderByDescending(x => x.UploadDate);

            return Fetch(query, pagingInfo);
        }

        private static IQueryable<Replay> FilterBySearchText(IQueryable<Replay> query, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return query;

            searchText = searchText.ToLowerInvariant();

            return query.Where(x => x.GameMap.Name.ToLowerInvariant().Contains(searchText)
                                    || x.ServerName.ToLowerInvariant().Contains(searchText)
                                    || x.Title.ToLowerInvariant().Contains(searchText));
        } 

        public IList<ReplayDto> GetByPlayerUser(int playerUserId, PagingInfo pagingInfo)
        {
            var query = from replay in _crud.Get()
                        from player in replay.Players
                        where player.PlayerUser.Id == playerUserId
                        select replay;

            query = query.OrderByDescending(x => x.UploadDate);

            return Fetch(query, pagingInfo);
        }

        public ReplayCardDto GetReplayCard(int replayId)
        {
            var query = from replay in _crud.Get()
                        where replay.Id == replayId
                        select replay;

            var replayCardDto = query.Select(x => new ReplayCardDto
            {
                Id = x.Id,
                UploadDate = x.UploadDate,
                PlayersCount = x.Players.Count(),
                MapName = x.GameMap.Name,
                Title = x.Title,
                VictoryConditionName = x.VictoryCondition.Name,
                GameVersion = x.Version,
                ScoreLimit = x.ScoreLimit,
                DownloadsCounter = x.DownloadsCounter
            }).SingleOrDefault();

            if (replayCardDto == null)
            {
                throw new NotSupportedException("ReplayNotFound");
            }

            replayCardDto.Alliances = new List<AllianceDto>();

            var alliancePlayers = (from replay in query
                                     from p in replay.Players
                                     select new PlayerDto
                                     {
                                         Id = p.Id,
                                         PlayerUserId = p.PlayerUser.Id,
                                         PlayerElo = p.PlayerElo,
                                         PlayerRank = p.PlayerRank,
                                         PlayerLevel = p.PlayerLevel,
                                         PlayerName = p.PlayerName,
                                         PlayerTeamName = p.PlayerTeamName,
                                         PlayerAvatar = p.PlayerAvatar,
                                         PlayerIALevel = p.PlayerIALevel,
                                         PlayerReady = p.PlayerReady,
                                         PlayerDeckName = p.PlayerDeckName,
                                         PlayerDeckContent = p.PlayerDeckContent,
                                         PlayerAlliance = p.PlayerAlliance,
                                         PlayerIsEnteredInLobby = p.PlayerIsEnteredInLobby,
                                         PlayerScoreLimit = p.PlayerScoreLimit,
                                         PlayerIncomeRate = p.PlayerIncomeRate
                                     }).ToList().GroupBy(x => x.PlayerAlliance);

            foreach (var groupedByAlliance in alliancePlayers)
            {
                var players = groupedByAlliance.ToList();
                players.ForEach(x =>
                {
                    x.PlayerElo = Math.Round(x.PlayerElo);
                    x.DeckInfo = DeckContentHelper.GetDeckInfo(x.PlayerDeckContent);
                }); // round ELO
                replayCardDto.Alliances.Add(new AllianceDto(players));
            }

            return replayCardDto;
        }

        public int GetTotalCount(string searchText)
        {
            var query = _crud.Get();
            query = FilterBySearchText(query, searchText);
            return query.Count();
        }

        public bool IsAlreadyUploaded(Guid fileHash, out string title)
        {
            title = null;
            var replay = _crud.Get().FirstOrDefault(x => x.FileHash == fileHash);
            if (replay == null) return false;

            title = replay.Title;
            return true;
        }

        private static IList<ReplayDto> Fetch(IQueryable<Replay> query, PagingInfo pagingInfo)
        {
            var dtoQuery = query.Select(x => new ReplayDto
            {
                Id = x.Id,
                UploadDate = x.UploadDate,
                PlayersCount = x.Players.Count(),
                MapName = x.GameMap.Name,
                Title = x.Title,
                VictoryConditionName = x.VictoryCondition.Name,
                GameVersion = x.Version
            });

            if (pagingInfo == PagingInfo.All)
            {
                return dtoQuery.ToList();
            }

            return dtoQuery.Skip(pagingInfo.StartIndex).Take(pagingInfo.PageSize).ToList();
        } 
    }
}