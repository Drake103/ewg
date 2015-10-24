using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using EWG.Domain.Entities;
using EWG.Infrastructure.Dal.Repositories;
using EWG.Infrastructure.Services.ReplayParsing;
using EWG.Infrastructure.Services.Replays;
using EWG.Shared.Dto;
using EWG.Shared.Dto.Replays;

namespace EWG.Infrastructure.Services.Impl.Replays
{
    public class ReplayService : IReplayService
    {
        private readonly IReplayParser _parser;
        private readonly IReplayMapper _mapper;
        private readonly IReplayRepository _replayRepository;

        public ReplayService(
            IReplayParser parser,
            IReplayMapper mapper,
            IReplayRepository replayRepository
            )
        {
            _parser = parser;
            _mapper = mapper;
            _replayRepository = replayRepository;
        }

        public Replay SaveReplay(Stream replayFile, string filePath)
        {
            var hash = ComputeFileHash(replayFile);

            var parsedReplayDto = _parser.ParseFile(CopyStream(replayFile));

            var replay = _mapper.GetEntity(parsedReplayDto);

            replay.Link = filePath;
            replay.Title = "no title";
            replay.UploadDate = DateTime.Now;
            replay.FileHash = hash;

            _replayRepository.Save(replay);

            return replay;
        }

        private Guid ComputeFileHash(Stream replayFile)
        {
            var streamCopy = CopyStream(replayFile);

            using (var md5 = MD5.Create())
            {
                var fileHash = md5.ComputeHash(streamCopy);
                return new Guid(fileHash);
            }
        }

        private Stream CopyStream(Stream inputStream)
        {
            var startPosition = inputStream.Position;

            inputStream.Position = 0;
            MemoryStream streamCopy = new MemoryStream();
            inputStream.CopyTo(streamCopy);
            inputStream.Position = startPosition;

            streamCopy.Position = 0;
            return streamCopy;
        }

        public bool IsAlreadyUploaded(Stream replayFile, out string title)
        {
            var hash = ComputeFileHash(replayFile);
            return _replayRepository.IsAlreadyUploaded(hash, out title);
        }

        public ReplayCardDto GetReplayCard(int replayId)
        {
            var replayCardDto = _replayRepository.GetReplayCard(replayId);

            return replayCardDto;
        }

        public IList<ReplayDto> GetReplays(PagingInfo pagingInfo, string searchText)
        {
            return _replayRepository.GetForList(pagingInfo, searchText);
        }

        public IList<ReplayDto> GetReplaysByPlayerUser(int playerUserId, PagingInfo pagingInfo)
        {
            return _replayRepository.GetByPlayerUser(playerUserId, pagingInfo);
        }

        public int GetReplaysCount(string searchText)
        {
            return _replayRepository.GetTotalCount(searchText);
        }
    }
}