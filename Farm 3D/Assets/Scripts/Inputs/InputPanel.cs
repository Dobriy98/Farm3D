using System;
using System.Collections.Generic;
using Inputs.Interfaces;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inputs
{
    public class InputPanel : MonoBehaviour, IMouseService, IPointerDownHandler
    {
        public event Action<IMouseRightClickable> OnRightClick;
        public event Action<IMouseLeftClickable> OnLeftClick;
        public event Action OnHitNothing;

        private Camera _mainCamera;
        
        private const int RightClickKey = 1;
        private const int LeftClickKey = 0;
        private const float MaxDistance = 100;


        public void Initialize(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetMouseButtonDown(RightClickKey) && !IsMouseOverUIWithIgnores())
            {
                RaycastClickable(OnRightClick);
            }
            else if (Input.GetMouseButtonDown(LeftClickKey) && !IsMouseOverUIWithIgnores())
            {
                RaycastClickable(OnLeftClick);
            }
        }

        private void RaycastClickable<T>(Action<T> action) where T : IClickable
        {
            if (TryRaycastSmth(out var hit))
            {
                if (hit.collider.TryGetComponent<T>(out var component))
                {
                    component.Hit = hit;
                    action?.Invoke(component);
                }
            }
        }

        private bool TryRaycastSmth(out RaycastHit rayHit)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance))
            {
                rayHit = hit;
                return true;
            }
            OnHitNothing?.Invoke();

            rayHit = new RaycastHit();
            return false;
        }

        private bool IsMouseOverUIWithIgnores()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            for (int i = 0; i < raycastResultsList.Count; i++)
            {
                if (raycastResultsList[i].gameObject.TryGetComponent<IMouseUIClick>(out var mouseUIClick))
                {
                    mouseUIClick.Click();
                }
                else
                {
                    raycastResultsList.RemoveAt(i);
                    i--;
                }
            }
            return raycastResultsList.Count > 0;
        }
    }
}
