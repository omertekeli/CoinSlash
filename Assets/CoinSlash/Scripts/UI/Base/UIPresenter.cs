namespace CoinSlash.Scripts.UI.Base
{
    public abstract class UIPresenter<TView> : IPresenter where TView : UIView
    {
        protected readonly TView View;

        protected UIPresenter(TView view)
        {
            View = view;
        }

        public virtual void Enable() => View.Show();
        public virtual void Disable() => View.Hide();
    }
}