using System.Collections.Generic;
using System.IO;
using EWG.Domain.Entities;
using EWG.Shared.Dto;
using EWG.Shared.Dto.Replays;

namespace EWG.Infrastructure.Services.Replays
{
    public interface IReplayService
    {
        Replay SaveReplay(Stream replayFile, string filePath);
        bool IsAlreadyUploaded(Stream replayFile, out string title);
        ReplayCardDto GetReplayCard(int replayId);
        IList<ReplayDto> GetReplays(PagingInfo pagingInfo, string searchText);
        IList<ReplayDto> GetReplaysByPlayerUser(int playerUserId, PagingInfo pagingInfo);
        int GetReplaysCount(string searchText);
    }
}