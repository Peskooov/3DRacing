using UnityEngine;

namespace Racing
{
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private float currentTime;
        public float CurrentTime => currentTime;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceCompleted;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            enabled = true;
            currentTime = 0;
        }

        private void OnRaceCompleted()
        {
            enabled = false;
        }
    }
}