using System.Collections.Generic;
using System.Linq;
using CoinSlash.Scripts.Core.Interfaces;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.Rendering;

namespace CoinSlash.Scripts.Systems.Network
{
    public class PhotonMatchmaker : MonoBehaviour, IMatchmaker
    {
        #region Fields
        private NetworkRunner _runner;
        #endregion

        #region Methods
        void Awake()
        {
            _runner = GetComponentInChildren<NetworkRunner>(true);
            if (_runner == null)
            {
                Debug.LogError("NetworkRunner component not found in children of this GameObject!");
            }
        }

        public async UniTask JoinRandomRoomAsync()
        {
            if (_runner.IsRunning)
            {
                Debug.LogWarning("There's already an active session");
                return;
            }

            var allCallbacks = GetComponents<INetworkRunnerCallbacks>();
            _runner.AddCallbacks(allCallbacks.ToArray());

            var result = await _runner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = "CoinSlashRoom_Shared",
                SceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
                PlayerCount = 2,
            });

            if (!result.Ok)
            {
                throw new System.Exception($"Creating a session is failed: {result.ShutdownReason}");
            }
        }

        public async UniTask JoinRankRoomAsync() { }

        public UniTask LeaveRoomAsync()
        {
            if (_runner == null || !_runner.IsRunning)
                return UniTask.CompletedTask;

            return _runner.Shutdown(shutdownReason: ShutdownReason.Ok).AsUniTask();
        }
        #endregion
    }
}