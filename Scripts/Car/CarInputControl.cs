using System;
using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private AnimationCurve breakCurve;
        [SerializeField] private AnimationCurve steerCurve;

        [SerializeField][Range(0.0f, 1.0f)] private float autoBreakStrength = 0.5f;

        private float wheelSpeed;
        private float verticalAxis;
        private float horizontalAxis;
        private float handBreakAxis;

        private void Update()
        {
            wheelSpeed = car.WheelSpeed;

            UpdateAxis();

            UpdateThrottleAndBreak();
            UpdateSteer();

            UpdateAutoBreak();

            //Debug
            if (Input.GetKeyDown(KeyCode.E))
                car.UpGear();
            if (Input.GetKeyDown(KeyCode.Q))
                car.DownGear();
        }

        private void UpdateThrottleAndBreak()
        {
            if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
            {
                car.ThrottleControl = Mathf.Abs(verticalAxis);
                car.BrakeControl = 0;
            }
            else
            {
                car.ThrottleControl = 0;
                car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
            }

            //Gears
            if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
                car.ShiftToReverseGear();
            if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
                car.ShiftToFirstGear();
        }

        private void UpdateSteer()
        {
            car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
        }

        private void UpdateAutoBreak()
        {
            if (verticalAxis == 0)
                car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBreakStrength;
        }

        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            handBreakAxis = Input.GetAxis("Jump");
        }

        public void Reset()
        {
            verticalAxis = 0;
            horizontalAxis = 0;
            handBreakAxis = 0;

            car.ThrottleControl = 0;
            car.SteerControl = 0;
            car.BrakeControl = 0;
        }

        public void Stop()
        {
            Reset();

            car.BrakeControl = 1;
        }
    }
}