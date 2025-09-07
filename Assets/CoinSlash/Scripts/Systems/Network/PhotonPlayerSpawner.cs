using System;
using System.Collections.Generic;
using CoinSlash.Scripts.Core.Interfaces;
using Fusion;
using Fusion.Sockets;
using UnityCoreModules.Services;
using UnityEngine;

namespace CoinSlash.Scripts.Systems.Network
{
    /// <summary>
    /// Handles the spawning and despawning of player characters.
    /// </summary>
    public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        #region Fields
        [SerializeField] private NetworkObject _playerPrefab;
        #endregion

        #region Networked
        //It is networked because master client can be changed (playerleft, disconnected, etc.) in session time
        private int SpawnIndex { get; set; }
        #endregion

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"{player.PlayerId} is joined!");
            if (runner.IsSharedModeMasterClient) //only master client
            {
                Debug.Log("State Authority KONTROLÜ GEÇİLDİ. Spawn süreci Master Client tarafından başlatılıyor.");
                var spawnPointManager = ServiceLocator.Get<ISpawnPointManager>();
                if (spawnPointManager != null)
                {
                    int spawnIndex = SpawnIndex % spawnPointManager.GetSpawnPointCount();
                    SpawnIndex++; //Roblox, RemoteEvent: FireAllClient
                                  // Roblox, RemoteEvent: FireClient
                    Debug.Log($"Master Client, oyuncu {player.PlayerId} için {spawnIndex} index'i ile Rpc_SpawnPlayer'ı gönderiyor.");
                    Rpc_SpawnPlayer(runner, player, spawnIndex);
                }
                else
                {
                    Debug.LogError("SpawnPointManager servisi bulunamadı!");
                }
            }
            else
            {
                Debug.Log($"No authority");
            }
        }

        // RpcSources.StateAuthority: Sadece bu objenin otoritesine sahip olanın (Master Client) bu RPC'yi çağırabileceğini söyler.
        // RpcTargets.InputAuthority: RPC'nin, spawn edilecek olan objenin girdi yetkisini alacak olan oyuncuya gönderileceğini söyler.
        // Biz de zaten Spawn komutunda yetkiyi "player"a veriyoruz.
        [Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority)]
        private void Rpc_SpawnPlayer(NetworkRunner runner, PlayerRef player, int spawnIndex)
        {
            Debug.Log($"RPC received on client {player.PlayerId} to spawn at index {spawnIndex}.");
            var spawnPointManager = ServiceLocator.Get<ISpawnPointManager>();
            if (spawnPointManager != null)
            {
                Transform spawnPoint = spawnPointManager.GetSpawnPointByIndex(spawnIndex);

                NetworkObject playerObject = runner.Spawn(_playerPrefab, spawnPoint.position, spawnPoint.rotation, player);
                playerObject.name = $"Player_{player.PlayerId}";
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"Player {player.PlayerId} left the room.");
        }

        #region Unused Callbacks
        public void OnConnectedToServer(NetworkRunner runner) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        #endregion
    }
}