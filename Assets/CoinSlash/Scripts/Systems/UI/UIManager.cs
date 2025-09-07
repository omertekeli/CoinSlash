using System;
using System.Collections.Generic;
using System.Linq;
using CoinSlash.Scripts.Core.Interfaces;
using CoinSlash.Scripts.UI.Base;
using CoinSlash.Scripts.UI.MainMenu;
using UnityEngine;

namespace CoinSlash.Scripts.Systems.UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        #region Fields
        [Header("View Prefabs")]
        [SerializeField] private List<UIView> _viewPrefabs;

        private readonly Dictionary<Type, IPresenter> _activePresenters = new();
        private readonly Dictionary<Type, UIView> _viewInstances = new();
        private readonly Dictionary<Type, Func<UIView, IPresenter>> _presenterFactories = new();
        #endregion
        
        #region Unity Methods
        private void Start()
        {
            ShowScreen<MainMenuPresenter>();
        }

        private void OnDestroy()
        {
            foreach (var presenter in _activePresenters.Values)
            {
                presenter.Disable();
            }
            _activePresenters.Clear();
            _viewInstances.Clear();
        }
        #endregion

        #region Public Methods
        public void Initialize() { }
        
        public void RegisterPresenterFactory<T>(Func<UIView, IPresenter> factoryMethod) where T : class, IPresenter
        {
            _presenterFactories[typeof(T)] = factoryMethod;
        }

        public void ShowScreen<T>() where T : class, IPresenter
        {
            if (_activePresenters.ContainsKey(typeof(T)))
                return;

            Type viewType = GetViewTypeForPresenter<T>();
            UIView viewPrefab = _viewPrefabs.FirstOrDefault(v => v.GetType() == viewType);
            if (viewPrefab == null)
                return;

            UIView viewInstance = Instantiate(viewPrefab, transform);
            IPresenter presenter = CreatePresenterForView<T>(viewInstance);

            _activePresenters.Add(typeof(T), presenter);
            _viewInstances.Add(viewType, viewInstance);

            presenter.Enable();
        }

        public void HideScreen<T>() where T : class, IPresenter
        {
            if (!_activePresenters.TryGetValue(typeof(T), out IPresenter presenter))
                return;
            
            Type viewType = GetViewTypeForPresenter<T>();
            if (!_viewInstances.TryGetValue(viewType, out UIView viewInstance))
                return;
            
            presenter.Disable();

            _activePresenters.Remove(typeof(T));
            _viewInstances.Remove(viewType);
            Destroy(viewInstance.gameObject);
        }
        #endregion

        private IPresenter CreatePresenterForView<T>(UIView view) where T : class, IPresenter
        {
            if (_presenterFactories.TryGetValue(typeof(T), out var factory))
            {
                return factory(view);
            }
            throw new InvalidOperationException($"No factory method registered for presenter {typeof(T).Name}");
        }

        private Type GetViewTypeForPresenter<T>() where T : class, IPresenter
        {
            string presenterName = typeof(T).Name;
            string viewName = presenterName.Replace("Presenter", "View");
            return typeof(T).Assembly
                            .GetTypes()
                            .FirstOrDefault(
                                t => t.Name == viewName &&
                                typeof(UIView).IsAssignableFrom(t)
                            );
        }
    }
}