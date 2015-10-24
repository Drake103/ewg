namespace EWG.Shared.Dto.ReplayParsing
{
    public class PlayerParsedDto : BaseDto
    {
        public int PlayerEugenId
        {
            get
            {
                if (PlayerUserId == ulong.MaxValue) return -1;

                return (int) PlayerUserId;
            }
        }

        public ulong PlayerUserId { get; set; }
        public double PlayerElo { get; set; }
        public int PlayerRank { get; set; }
        public int PlayerLevel { get; set; }
        public string PlayerName { get; set; }
        public string PlayerTeamName { get; set; }
        public string PlayerAvatar { get; set; }
        public string PlayerIALevel { get; set; }
        public int PlayerReady { get; set; }
        public string PlayerDeckName { get; set; }
        public string PlayerDeckContent { get; set; }
        public int PlayerAlliance { get; set; }
        public int PlayerIsEnteredInLobby { get; set; }
        public int PlayerScoreLimit { get; set; }
        public int PlayerIncomeRate { get; set; }
        public int PlayerNumber { get; set; }
    }
}