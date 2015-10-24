using System.Collections.Generic;

namespace EWG.Shared.Dto.Replays
{
    public class AllianceDto
    {
        public AllianceDto()
        {
            Players = new List<PlayerDto>();
        }

        public AllianceDto(IList<PlayerDto> players)
        {
            Players = players;
        }

        public IList<PlayerDto> Players { get; set; }
    }
}