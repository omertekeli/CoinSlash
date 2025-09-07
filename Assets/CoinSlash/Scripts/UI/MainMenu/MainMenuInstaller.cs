using CoinSlash.Scripts.Core.Interfaces;
using CoinSlash.Scripts.Systems.UI;
using UnityCoreModules.Services;
using UnityEngine;

namespace CoinSlash.Scripts.UI.MainMenu
{
    /// <summary>
    /// Registers the MainMenu module's presenters with the UIManager
    /// </summary>
    public class MainMenuInstaller : MonoBehaviour
    {
        private void Awake()
        {
            // 'as UIManager' to get the concrete class with the Register method
            var uiManager = ServiceLocator.Get<IUIManager>() as UIManager;
            if (uiManager == null)
                return;

            // Register the factory method for the MainMenuPresenter
            uiManager.RegisterPresenterFactory<MainMenuPresenter>(view =>
            {
                var matchmaker = ServiceLocator.Get<IMatchmaker>();
                return new MainMenuPresenter(view as MainMenuView, matchmaker);
            });
        }
    }
}