using UnityEngine;

namespace CoinSlash.Scripts.Core.Interfaces
{
    public interface ISpawnPointManager
    {
        Transform GetSpawnPointByIndex(int index);
        int GetSpawnPointCount();
    }
}