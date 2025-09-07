using UnityEngine;

namespace CoinSlash.Scripts.UI.Base
{
    public abstract class UIView : MonoBehaviour
    {
        public bool IsVisible { get; private set; }

        public virtual void Show()
        {
            SetVisible(true);
        }
        public virtual void Hide()
        {
            SetVisible(false);
        }

        protected virtual void SetVisible(bool state)
        {
            gameObject.SetActive(state);
            IsVisible = state;
        }
    }
}