using System.Collections.Generic;
using CoinSlash.Scripts.Core.Interfaces;
using UnityCoreModules.Services;
using UnityEngine;

namespace CoinSlash.Scripts.Systems.Local
{
    public class SpawnPointManager : MonoBehaviour, ISpawnPointManager
    {
        #region Fields
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        private int _nextSpawnPointIndex = 0;
        #endregion

        private void Awake()
        {
            ServiceLocator.Register<ISpawnPointManager>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<ISpawnPointManager>();
        }

        public Transform GetSpawnPointByIndex(int index)
        {
            if (_spawnPoints.Count == 0 || index < 0 || index >= _spawnPoints.Count)
            {
                Debug.LogError("Spawn list empty or invalid index!");
                return transform; 
            }
            return _spawnPoints[index];
        }

        public int GetSpawnPointCount()
        {
            return _spawnPoints.Count;
        }
    }
}