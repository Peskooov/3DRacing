using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected bool Interactable;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        private bool focuse = false;
        public bool Focuse => focuse;

        public virtual void SetFocuse()
        {
            if (Interactable == false) return;
            focuse = true;
        }

        public virtual void SetUnFocuse()
        {
            if (Interactable == false) return;
            focuse = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerClick?.Invoke(this);
            OnClick?.Invoke();
        }
    }
}