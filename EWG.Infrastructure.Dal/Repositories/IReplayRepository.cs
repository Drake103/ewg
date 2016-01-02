using System;
using System.Collections.Generic;
using EWG.Domain.Entities;
using EWG.Shared.Dto;
using EWG.Shared.Dto.Replays;

namespace EWG.Infrastructure.Dal.Repositories
{
    public interface IReplayRepository : IGenericRepository<Replay>
    {
        IList<ReplayDto> GetForList(PagingInfo pagingInfo, string searchText);
        IList<ReplayDto> GetByPlayerUser(int playerUserId, PagingInfo pagingInfo);
        Replay GetByHash(Guid fileHash);
        ReplayCardDto GetReplayCard(int replayId);
        int GetTotalCount(string searchText);
        bool IsAlreadyUploaded(Guid fileHash, out string title);
    }
}