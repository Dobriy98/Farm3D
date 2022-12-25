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
            Show(isActiveOnStart);
        }

        protected void Show(bool show)
        {
            Canvas.enabled = show;
        }
    }
}