using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class GlobalDependenciesContainer : Dependency
    {
        [SerializeField] private Pauser pauser;

        private static GlobalDependenciesContainer instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected override void BindAll(MonoBehaviour mono)
        {
            Bind<Pauser>(pauser, mono);
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            FindAllObjectToBind();
        }
    }
}