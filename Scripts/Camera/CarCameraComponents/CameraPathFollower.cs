using UnityEngine;

namespace Racing
{
    public class CameraPathFollower : CarCameraComponent
    {
        [SerializeField] private Transform path;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private float movementSpeed;

        private Vector3[] points;
        private int pointIndex;

        private void Start()
        {
            points = new Vector3[path.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = path.GetChild(i).position;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, points[pointIndex], movementSpeed * Time.deltaTime);

            if (transform.position == points[pointIndex])
            {
                if (pointIndex == points.Length - 1)
                    pointIndex = 0;
                else
                    pointIndex++;
            }

            transform.LookAt(lookTarget);
        }

        public void StartMoveToNearestPoint()
        {
            float minDistance = float.MaxValue;

            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, points[i]);
                if (distance < minDistance)
                    minDistance = distance;
                pointIndex = i;
            }
        }

        public void SetLookTarget(Transform target)
        {
            lookTarget = target;
        }
    }
}