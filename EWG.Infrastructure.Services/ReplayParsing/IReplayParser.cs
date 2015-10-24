using System.IO;
using EWG.Shared.Dto.ReplayParsing;

namespace EWG.Infrastructure.Services.ReplayParsing
{
    public interface IReplayParser
    {
        ReplayParsedDto ParseFile(Stream replayFile);
    }
}