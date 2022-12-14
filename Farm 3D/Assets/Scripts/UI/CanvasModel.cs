using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class CanvasModel : MonoBehaviour
    {
        public bool isActiveOnStart;

        protected Canvas Canvas;

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
            Init();
        }

        protected virtual void Init()
        {
            Show(isActiveOnStart);
        }

        protected virtual void Show(bool show)
        {
            Canvas.enabled = show;
        }
    }
}