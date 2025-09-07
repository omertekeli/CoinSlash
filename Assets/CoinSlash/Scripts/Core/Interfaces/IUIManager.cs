using CoinSlash.Scripts.UI.Base;

namespace CoinSlash.Scripts.Core.Interfaces
{
    public interface IUIManager
    {
        void Initialize();
        void ShowScreen<T>() where T : class, IPresenter;
        void HideScreen<T>() where T : class, IPresenter;
    }
}