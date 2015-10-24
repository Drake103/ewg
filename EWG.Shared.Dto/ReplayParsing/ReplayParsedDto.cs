using System.Collections.Generic;

namespace EWG.Shared.Dto.ReplayParsing
{
    public class ReplayParsedDto : BaseDto
    {
        public ReplayParsedDto()
        {
            Players = new List<PlayerParsedDto>();
        }

        public int IsNetworkMode { get; set; }
        public string Version { get; set; }
        public string GameMode { get; set; }
        public string Map { get; set; }
        public int NbMaxPlayer { get; set; }
        public int NbAI { get; set; }
        public string GameType { get; set; }
        public int Private { get; set; }
        public int InitMoney { get; set; }
        public int TimeLimit { get; set; }
        public int ScoreLimit { get; set; }
        public string ServerName { get; set; }
        public string VictoryCond { get; set; }
        public string NationConstraint { get; set; }
        public string ThematicConstraint { get; set; }
        public string DateConstraint { get; set; }
        public string IncomeRate { get; set; }
        public int AllowNbObs { get; set; }
        public string Seed { get; set; }

        public IList<PlayerParsedDto> Players { get; set; }
    }
}