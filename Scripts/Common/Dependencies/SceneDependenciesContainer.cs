using UnityEngine;

namespace Racing
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private RaceStateTracker raceStateTracker;
        [SerializeField] private RaceTimeTracker raceTimeTracker;
        [SerializeField] private RaceResultTime raceResultTime;
        [SerializeField] private CarInputControl carInputControl;
        [SerializeField] private TrackPointCircuit trackPointCircuit;
        [SerializeField] private Car car;
        [SerializeField] private CarCameraController carCameraController;

        private void Awake()
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
            Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
            Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
            Bind<TrackPointCircuit>(trackPointCircuit, monoBehaviourInScene);
            Bind<Car>(car, monoBehaviourInScene);
            Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
        }
    }
}