using System;

namespace EWG.Shared.Dto.Replays
{
    public class ReplayDto : BaseDto
    {
        public DateTime UploadDate { get; set; }
        public string Title { get; set; }
        public int PlayersCount { get; set; }
        public string MapName { get; set; }

        public string VictoryConditionName { get; set; }
        public string VictoryConditionPublicCode { get; set; }

        public string GameVersion { get; set; }
        public int DownloadsCounter { get; set; }
    }
}