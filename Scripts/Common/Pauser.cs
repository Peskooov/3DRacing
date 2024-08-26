using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool IsPause => isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_SceneLoader;
    }

    private void SceneManager_SceneLoader(Scene scene, LoadSceneMode sceneMode)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPause)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPause) return;

        Time.timeScale = 0;
        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }

    public void UnPause()
    {
        if (!isPause) return;

        Time.timeScale = 1;
        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }
}