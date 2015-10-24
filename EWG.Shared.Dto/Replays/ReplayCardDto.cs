using System.Collections.Generic;

namespace EWG.Shared.Dto.Replays
{
    public class ReplayCardDto : ReplayDto
    {
        public int ScoreLimit { get; set; }
        public IList<AllianceDto> Alliances { get; set; }
    }
}