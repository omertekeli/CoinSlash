using System;
using CoinSlash.Scripts.Core.Interfaces;
using CoinSlash.Scripts.UI.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CoinSlash.Scripts.UI.MainMenu
{
    public class MainMenuPresenter : UIPresenter<MainMenuView>
    {
        private readonly IMatchmaker _matchmaker;

        public MainMenuPresenter(MainMenuView view, IMatchmaker matchmaker) : base(view)
        {
            _matchmaker = matchmaker;
        }

        public override void Enable()
        {
            base.Enable();
            View.OnPlayClicked += HandlePlayClicked;
            View.SetPlayButtonInteractable(true);
        }

        public override void Disable()
        {
            base.Disable();
            View.OnPlayClicked -= HandlePlayClicked;
        }

        private void HandlePlayClicked()
        {
            StartMatchmakingAsync().Forget();
        }

        private async UniTaskVoid StartMatchmakingAsync()
        {
            try
            {
                Debug.Log("Starting matchmaking via Presenter...");
                await _matchmaker.JoinRandomRoomAsync();
                Debug.Log("Matchmaking successful! Scene transition will be handled by the runner.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Matchmaking failed: {e.Message}");
                View.SetPlayButtonInteractable(true);
            }
        }
    }
}