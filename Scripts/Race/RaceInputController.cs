using UnityEngine;

namespace Racing
{
    public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        private CarInputControl carInputControl;
        public void Construct(CarInputControl obj) => carInputControl = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceFinished;

            carInputControl.enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
            carInputControl.enabled = true;
        }
        private void OnRaceFinished()
        {
            carInputControl.Stop();
            carInputControl.enabled = false;
        }
    }
}