using UnityEngine;

namespace Racing
{
    public static class TrackCircuitBuilder
    {
        public static TrackPoint[] Build(Transform trackTransform, TrackType trackType)
        {
            TrackPoint[] points = new TrackPoint[trackTransform.childCount];

            ResetPoints(trackTransform, points);

            MakeLinks(points, trackType);

            MarkPoint(points, trackType);

            return points;
        }

        private static void ResetPoints(Transform trackTransform, TrackPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

                if (points[i] == null)
                {
                    Debug.LogError("Trackpoint is null");
                    return;
                }

                points[i].Reset();
            }
        }

        private static void MakeLinks(TrackPoint[] points, TrackType trackType)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].Next = points[i + 1];
            }

            if (trackType == TrackType.Circular)
            {
                points[points.Length - 1].Next = points[0];
            }
        }

        private static void MarkPoint(TrackPoint[] points, TrackType trackType)
        {
            points[0].IsFirst = true;

            if (trackType == TrackType.Sprint)
            {
                points[points.Length - 1].IsLast = true;
            }

            if (trackType == TrackType.Circular)
            {
                points[0].IsLast = true;
            }
        }
    }
}