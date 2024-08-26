using UnityEngine;

namespace Racing
{
    public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        [SerializeField] private new Camera camera;

        private Car car;
        public void Construct(Car obj) => car = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private CameraPathFollower cameraPathFollower;
        private CarCameraFollow cameraFollower;
        private CarCameraFovCorrector cameraFovCorrectorer;
        private CarCameraShaker cameraShaker;
        private CarCameraBlur cameraBlur;
        private CarCameraVignette cameraVignette;

        private void Awake()
        {
            cameraPathFollower = GetComponent<CameraPathFollower>();
            cameraFollower = GetComponent<CarCameraFollow>();
            cameraFovCorrectorer = GetComponent<CarCameraFovCorrector>();
            cameraShaker = GetComponent<CarCameraShaker>();
            cameraBlur = GetComponent<CarCameraBlur>();
            cameraVignette = GetComponent<CarCameraVignette>();

            cameraFollower.SetProperties(car, camera);
            cameraFovCorrectorer.SetProperties(car, camera);
            cameraShaker.SetProperties(car, camera);
            cameraBlur.SetProperties(car, camera);
            cameraVignette.SetProperties(car, camera);
        }

        private void Start()
        {
            raceStateTracker.PreparationStarted += OnPreparationStarted;
            raceStateTracker.Completed += OnCompleted;

            cameraPathFollower.enabled = true;
            cameraFollower.enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.PreparationStarted -= OnPreparationStarted;
            raceStateTracker.Completed -= OnCompleted;
        }

        private void OnPreparationStarted()
        {
            cameraFollower.enabled = true;
            cameraPathFollower.enabled = false;
        }

        private void OnCompleted()
        {
            cameraPathFollower.enabled = true;
            cameraPathFollower.StartMoveToNearestPoint();
            cameraPathFollower.SetLookTarget(car.transform);

            cameraFollower.enabled = false;
        }
    }
}