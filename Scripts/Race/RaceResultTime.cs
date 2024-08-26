using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
    {
        public static string SaveMark = "_player_best_time";
        public static string SaveGold = "_new_gold_time";

        public event UnityAction ResultUpdated;

        [SerializeField] private float goldTime;
        [SerializeField] private float goldTimeStep;
        private float playerRecordTime;
        private float currentTime;
        private float newGoldTime;

        public float GoldTime => goldTime;
        public float PlayerRecordTime => playerRecordTime;
        public float CurrentTime => currentTime;
        public bool RecordWasSet => playerRecordTime != 0;

        private RaceTimeTracker raceTimeTracker;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Awake()
        {
            Load();
            if (newGoldTime < goldTime && newGoldTime != 0)
                goldTime = newGoldTime;
        }

        private void Start()
        {
            raceStateTracker.Completed += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            float absoluteRecord = GetAbsoluteRecord();

            if (raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
            {
                playerRecordTime = raceTimeTracker.CurrentTime;
                goldTime = playerRecordTime - goldTimeStep;

                Save();
            }

            currentTime = raceTimeTracker.CurrentTime;

            ResultUpdated?.Invoke();
        }

        public float GetAbsoluteRecord()
        {
            if (playerRecordTime < goldTime && playerRecordTime != 0)
                return playerRecordTime;
            else
                return goldTime;
        }

        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
            newGoldTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveGold, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveGold, goldTime);
        }
    }
}