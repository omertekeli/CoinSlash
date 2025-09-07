using System;
using CoinSlash.Scripts.UI.Base; 
using UnityEngine;
using UnityEngine.UI;

namespace CoinSlash.Scripts.UI.MainMenu
{
    public class MainMenuView : UIView
    {
        #region Fields
        [Header("UI Elements")]
        [SerializeField] private Button _playButton;
        #endregion
        
        public event Action OnPlayClicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(HandlePlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(HandlePlayButtonClick);
        }
        
        private void HandlePlayButtonClick()
        {
            _playButton.interactable = false;
            OnPlayClicked?.Invoke();
        }

        public void SetPlayButtonInteractable(bool isInteractable)
        {
            _playButton.interactable = isInteractable;
        }
    }
}