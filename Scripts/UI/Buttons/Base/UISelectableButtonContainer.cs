using UnityEngine;

namespace Racing
{
    public class UISelectableButtonContainer : MonoBehaviour
    {
        [SerializeField] private Transform buttonContainer;

        public bool Interactable = true;
        public void SetInteractable(bool interactable) => Interactable = interactable;

        private UISelectableButton[] buttons;

        private int selectButtonIndex = 0;

        private void Start()
        {
            buttons = buttonContainer.GetComponentsInChildren<UISelectableButton>();

            if (buttons == null)
                Debug.LogError("buttons is null");

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter += OnPointerEnter;
            }

            if (Interactable == false) return;

            buttons[selectButtonIndex].SetFocuse();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter -= OnPointerEnter;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        private void SelectButton(UIButton button)
        {
            if (Interactable == false) return;

            buttons[selectButtonIndex].SetUnFocuse();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (button == buttons[i])
                {
                    selectButtonIndex = i;
                    button.SetFocuse();
                    break;
                }
            }
        }

        public void SelectNext()
        {
            // управление с клавиатуры
        }

        public void SelectPrevious()
        {

        }
    }
}