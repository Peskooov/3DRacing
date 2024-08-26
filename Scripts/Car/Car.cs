using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        public event UnityAction<string> GearChanged;

        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;


        [Header("Engine")]
        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float engineMaxTorque;
        [SerializeField] private float engineTorque;
        [SerializeField] private float engineRpm;
        [SerializeField] private float engineMinRpm;
        [SerializeField] private float engineMaxRpm;

        [Header("Gearbox")]
        [SerializeField] private float[] gears;
        [SerializeField] private float finalDriveRatio;

        //Debug
        [SerializeField] private int selectedGearIndex;
        [SerializeField] private float selectedGear;
        [SerializeField] private float rearGear;
        [SerializeField] private float upShiftEngineRpm;
        [SerializeField] private float downShiftEngineRpm;

        [SerializeField] private int maxSpeed;

        [SerializeField] private float linearVelocity;
        private CarChassis chasses;
        public Rigidbody Rigidbody => chasses == null ? GetComponent<Rigidbody>() : chasses.Rigidbody;

        public float EngineRpm => engineRpm;
        public float EngineMaxRpm => engineMaxRpm;

        public float LinearVelocity => chasses.LinearVelocity;
        public float NormalizeLinearVelocity => chasses.LinearVelocity / maxSpeed;
        public float WheelSpeed => chasses.GetWheelSpeed();
        public float MaxSpeed => maxSpeed;

        public float SteerControl;
        public float ThrottleControl;
        public float BrakeControl;

        private void Start()
        {
            chasses = GetComponent<CarChassis>();
        }

        private void Update()
        {
            linearVelocity = LinearVelocity;

            UpdateEngineTorque();

            AutoGearShift();

            if (LinearVelocity >= maxSpeed)
                engineTorque = 0;

            chasses.MotorTorque = engineTorque * ThrottleControl;
            chasses.SteerAngle = maxSteerAngle * SteerControl;
            chasses.BreakTorque = maxBrakeTorque * BrakeControl;
        }

        //GearBox
        public string GetSelectedGearName()
        {
            if (selectedGear == rearGear) return "R";
            if (selectedGear == 0) return "N";
            return (selectedGearIndex + 1).ToString();
        }

        private void AutoGearShift()
        {
            if (selectedGear < 0) return;
            if (engineRpm >= upShiftEngineRpm)
                UpGear();
            if (engineRpm < downShiftEngineRpm)
                DownGear();
        }

        public void UpGear()
        {
            ShiftGear(selectedGearIndex + 1);
        }

        public void DownGear()
        {
            ShiftGear(selectedGearIndex - 1);
        }

        public void ShiftToReverseGear()
        {
            selectedGear = rearGear;
            GearChanged?.Invoke(GetSelectedGearName());
        }

        public void ShiftToFirstGear()
        {
            ShiftGear(0);
        }

        public void ShiftToNetral()
        {
            selectedGear = 0;
            GearChanged?.Invoke(GetSelectedGearName());
        }

        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
            selectedGear = gears[gearIndex];
            selectedGearIndex = gearIndex;

            GearChanged?.Invoke(GetSelectedGearName());
        }

        private void UpdateEngineTorque()
        {
            engineRpm = engineMinRpm + Mathf.Abs(chasses.GetAverageRpm() * selectedGear * finalDriveRatio);
            engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

            engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
        }

        public void Reset()
        {
            chasses.Reset();

            chasses.MotorTorque = 0;
            chasses.BreakTorque = 0;
            chasses.SteerAngle = 0;

            ThrottleControl = 0;
            BrakeControl = 0;
            SteerControl = 0;
        }

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            Reset();

            transform.position = position;
            transform.rotation = rotation;
        }
    }
}