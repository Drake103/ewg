using EWG.Domain.Entities;
using EWG.Shared.Dto.ReplayParsing;

namespace EWG.Infrastructure.Services.ReplayParsing
{
    public interface IReplayMapper
    {
        Replay GetEntity(ReplayParsedDto replayParsedDto);
    }
}