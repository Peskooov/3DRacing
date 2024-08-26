using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIFinishInfoPanel : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Text currentPlayerTime;
        [SerializeField] private Text bestPlayerTime;

        private RaceStateTracker raceStateTracker;
        private RaceTimeTracker raceTimeTracker;
        private RaceResultTime raceResultTime;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        private void Start()
        {
            raceStateTracker.Completed += OnCompleted;
            panel.SetActive(false);
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnCompleted;
        }

        private void OnCompleted()
        {
            if (raceStateTracker.RaceState == RaceState.Passed)
            {
                currentPlayerTime.color = Color.white;

                currentPlayerTime.text = "Current: " + StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
                bestPlayerTime.text = "Best: " + StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRecord());

                if (raceTimeTracker.CurrentTime <= raceResultTime.GetAbsoluteRecord())
                {
                    bestPlayerTime.text = "Best: " + StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
                    currentPlayerTime.text = "New record: " + StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
                    currentPlayerTime.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);
                }

                panel.SetActive(true);
            }
        }
    }
}