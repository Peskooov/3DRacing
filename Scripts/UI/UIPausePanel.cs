using UnityEngine;

namespace Racing
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject panel;

        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            panel.SetActive(false);
            pauser.PauseStateChange += OnPauseStateChanged;
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                pauser.ChangePauseState();
            }
        }

        private void OnDestroy()
        {
            pauser.PauseStateChange -= OnPauseStateChanged;
        }

        private void OnPauseStateChanged(bool isPause)
        {
            panel.SetActive(isPause);
        }

        public void UnPause()
        {
            pauser.UnPause();
        }
    }
}