using UnityEngine;

namespace Racing
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelButtons; // Массив кнопок для выбора уровней

        private bool isPreviousLevelCompleted;

        private void Start()
        {
            LoadLevelCompletionStatus();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F4))
            {
                DeleteSave();
                LoadLevelCompletionStatus();
            }
        }

        private void LoadLevelCompletionStatus()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i == 0)
                {
                    levelButtons[i].SetActive(true);
                }
                else
                {
                    isPreviousLevelCompleted = PlayerPrefs.GetInt("LevelCompleted_" + (i - 1), 0) == 1;
                    levelButtons[i].SetActive(isPreviousLevelCompleted);
                }
            }
        }

        private void DeleteSave()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                PlayerPrefs.DeleteKey("LevelCompleted_" + i);
            }
        }
    }
}