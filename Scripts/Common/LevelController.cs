using UnityEngine;

namespace Racing
{
    public class LevelController : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private int levelIndex; // Индекс текущего уровня

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            raceStateTracker.Completed += OnLevelCompleted;
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            PlayerPrefs.SetInt("LevelCompleted_" + levelIndex, 1);
            PlayerPrefs.Save();
        }
    }
}