using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public enum TrackType
    {
        Circular,
        Sprint
    }

    public class TrackPointCircuit : MonoBehaviour
    {
        public event UnityAction<TrackPoint> TrackPointTriggered;
        public event UnityAction<int> LapCompleted;

        [SerializeField] private TrackType trackType;
        public TrackType TrackType => trackType;

        private TrackPoint[] points;

        private int lapsComplited = -1;

        private void Awake()
        {
            BuildCircuit();
        }

        private void Start()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered += OnTrackPointTriggered;
            }

            points[0].AssignAsTarget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered -= OnTrackPointTriggered;
            }
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.IsTarget == false) return;

            trackPoint.Passed();
            trackPoint.Next?.AssignAsTarget();

            TrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.IsLast)
            {
                lapsComplited++;

                if (trackType == TrackType.Sprint)
                {
                    LapCompleted?.Invoke(lapsComplited);
                }

                if (trackType == TrackType.Circular)
                {
                    if (lapsComplited > 0)
                    {
                        LapCompleted?.Invoke(lapsComplited);
                    }
                }
            }
        }

        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            points = TrackCircuitBuilder.Build(transform, trackType);
        }
    }
}