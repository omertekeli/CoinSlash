using CoinSlash.Scripts.Core.Interfaces;
using CoinSlash.Scripts.Systems.Network;
using CoinSlash.Scripts.Systems.UI;
using UnityCoreModules.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoinSlash.Scripts.Bootstrap
{
    [DefaultExecutionOrder(-999)]
    public class InitLoader : MonoBehaviour
    {
        #region Fields
        [Header("Persistent System Prefabs")]
        [SerializeField] private UIManager _uiManagerPrefab;
        [SerializeField] private PhotonMatchmaker _networkSystemPrefab;
        
        private static bool _isInitialized = false;
        #endregion

        private void Awake()
        {

            if (_isInitialized)
            {
                return;
            }
            _isInitialized = true;

            var networkSystemInstance = Instantiate(_networkSystemPrefab);
            DontDestroyOnLoad(networkSystemInstance.gameObject);
            ServiceLocator.Register<IMatchmaker>(networkSystemInstance);

            var uiManagerInstance = Instantiate(_uiManagerPrefab);
            DontDestroyOnLoad(uiManagerInstance.gameObject);
            ServiceLocator.Register<IUIManager>(uiManagerInstance);

            uiManagerInstance.Initialize();

            Debug.Log("Core systems initialized. Loading Main Menu...");
            SceneManager.LoadScene("MainMenu");
        }
    }
}