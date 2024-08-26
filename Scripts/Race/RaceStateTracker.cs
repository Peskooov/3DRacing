using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public enum RaceState
    {
        Preparation,
        CountDown,
        Race,
        Passed
    }

    public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
    {
        public event UnityAction PreparationStarted;
        public event UnityAction Started;
        public event UnityAction Completed;
        public event UnityAction<TrackPoint> TrackPointPassed;
        public event UnityAction<int> LapCompleted;

        [SerializeField] private Timer countDownTimer;
        [SerializeField] private int lapsToComplete;

        public Timer CountDownTimer => countDownTimer;

        private TrackPointCircuit trackPointCircuit;

        public void Construct(TrackPointCircuit obj) => trackPointCircuit = obj; 

        private RaceState raceState;
        public RaceState RaceState => raceState;

        private void StartState(RaceState raceState)
        {
            this.raceState = raceState;
        }

        private void Start()
        {
            StartState(RaceState.Preparation);

            countDownTimer.enabled = false;
            countDownTimer.Finished += OnCountDownTimerFinished;

            trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
            trackPointCircuit.LapCompleted += OnLapCompleted;
        }

        private void OnDestroy()
        {
            countDownTimer.Finished -= OnCountDownTimerFinished;

            trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
            trackPointCircuit.LapCompleted -= OnLapCompleted;
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            TrackPointPassed?.Invoke(trackPoint);
        }

        private void OnCountDownTimerFinished()
        {
            StartRace();
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (trackPointCircuit.TrackType == TrackType.Sprint)
            {
                CompleteRace();
            }

            if (trackPointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == lapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }

        public void LaunchPreparationStart()
        {
            if (raceState != RaceState.Preparation) return;
            StartState(RaceState.CountDown);

            countDownTimer.enabled = true;

            PreparationStarted?.Invoke();
        }
        private void StartRace()
        {
            if (raceState != RaceState.CountDown) return;
            StartState(RaceState.Race);

            Started?.Invoke();
        }

        private void CompleteRace()
        {
            if (raceState != RaceState.Race) return;
            StartState(RaceState.Passed);

            Completed?.Invoke();
        }
        private void CompleteLap(int lapAmount)
        {
            LapCompleted?.Invoke(lapAmount);
        }
    }
}