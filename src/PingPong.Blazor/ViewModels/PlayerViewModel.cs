using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Wins { get; }
        public int Losses { get; }
        
        public int Total => Wins + Losses;

        public bool HasWinrate => Total > 0;
        
        public double Winrate =>
            Total > 0
                ? 100.0 * Wins / Total
                : 0;

        public string WinrateCss =>
            (Total, Winrate) switch
            {
                (0, _)                       => "winrate-na",
                var (_, rate) when rate < 25 => "winrate-low",
                var (_, rate) when rate < 75 => "winrate-avg",
                _                            => "winrate-high"
            };
        
        public string FullName => $@"{FirstName} {LastName}".Trim();

        public PlayerViewModel(PlayerInfo player)
        {
            Id = player.Id;
            FirstName = player.FirstName;
            LastName = player.LastName;
            Wins = player.Wins;
            Losses = player.Losses;
        }
    }
}