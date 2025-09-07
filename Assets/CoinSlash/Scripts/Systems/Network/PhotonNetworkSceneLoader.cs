using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace CoinSlash.Scripts.Systems.Network
{
    public class NetworkSceneLoader : MonoBehaviour, INetworkRunnerCallbacks
    {
        public void OnConnectedToServer(NetworkRunner runner)
        {
            if (runner.IsSharedModeMasterClient)
            {
                Debug.Log("Master client is connected. GameLevel is loading...");
                runner.LoadScene(SceneRef.FromIndex(2));
            }
        }
        
        #region Unused Callbacks
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason){}

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token){}

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data){}

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason){}

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}

        public void OnInput(NetworkRunner runner, NetworkInput input){}

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input){}

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress){}

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data){}

        public void OnSceneLoadDone(NetworkRunner runner){}

        public void OnSceneLoadStart(NetworkRunner runner){}

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList){}

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason){}

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}
        #endregion
    }
}