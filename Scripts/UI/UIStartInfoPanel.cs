using UnityEngine;

namespace Racing
{
    public class UIStartInfoPanel : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            gameObject.SetActive(true);
            raceStateTracker.PreparationStarted += OnPreparationStarted;
        }

        private void OnDestroy()
        {
            raceStateTracker.PreparationStarted -= OnPreparationStarted;
        }

        private void OnPreparationStarted()
        {
            if (raceStateTracker.RaceState == RaceState.Preparation)
                gameObject.SetActive(true);

            else
                gameObject.SetActive(false);
        }
    }
}