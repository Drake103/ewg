namespace EWG.Domain.Entities
{
    public class Player : EntityBase
    {
        public virtual PlayerUser PlayerUser { get; set; }
        public virtual double PlayerElo { get; set; }
        public virtual int PlayerRank { get; set; }
        public virtual int PlayerLevel { get; set; }
        public virtual string PlayerName { get; set; }
        public virtual string PlayerTeamName { get; set; }
        public virtual string PlayerAvatar { get; set; }
        public virtual string PlayerIALevel { get; set; }
        public virtual bool PlayerReady { get; set; }
        public virtual string PlayerDeckName { get; set; }
        public virtual string PlayerDeckContent { get; set; }
        public virtual int PlayerAlliance { get; set; }
        public virtual bool PlayerIsEnteredInLobby { get; set; }
        public virtual int PlayerScoreLimit { get; set; }
        public virtual int PlayerIncomeRate { get; set; }

        public virtual int PlayerNumber { get; set; }
        public virtual Replay Replay { get; set; }
    }
}