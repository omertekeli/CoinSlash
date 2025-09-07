namespace CoinSlash.Scripts.UI.Base
{
    /// <summary>
    /// A non-generic base interface for all presenters.
    /// Used for storing presenters of different types in a collection.
    /// </summary>
    public interface IPresenter
    {
        void Enable();
        void Disable();
    }
}