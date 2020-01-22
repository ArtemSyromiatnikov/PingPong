using PingPong.Sdk.Resources;

namespace PingPong.Sdk
{
    public interface IApiClient
    {
        IPlayersResource Players { get; }
        IGamesResource   Games   { get; }
    }
}