using Cysharp.Threading.Tasks;

namespace CoinSlash.Scripts.Core.Interfaces
{
    public interface IMatchmaker
    {
        UniTask JoinRandomRoomAsync();
        UniTask JoinRankRoomAsync();
        UniTask LeaveRoomAsync();
    }
}