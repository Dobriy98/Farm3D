using UnityEngine;
using Utils;

namespace UI
{
    public abstract class UIElement<TComponent, TData> : MonoBehaviour
    {
        public string key;
        
        protected TComponent Component;

        protected ObservableUI<TData> ObservableUI;

        private void Awake()
        {
            Component = GetComponent<TComponent>();

            ObservableUI = gameObject.FindObservableUI<TData>(key);
            if (ObservableUI == null)
            {
                Debug.LogError($"Can not locale {key} Observable UI",gameObject);
                return;
            }
            ObservableUI.Listeners += SetValue;
        }

        protected abstract void SetValue(TData arg);
    }
}