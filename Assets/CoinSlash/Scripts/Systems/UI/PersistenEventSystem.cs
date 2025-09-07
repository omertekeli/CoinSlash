using UnityEngine;

namespace CoinSlash.Scripts.Systems.UI
{
    public class PersistentEventSystem : MonoBehaviour
    {
        private static PersistentEventSystem _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}