using System;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] wheelAxles;

        [SerializeField] private float wheelBaseLength;

        [SerializeField] private Transform centerOfMass;

        [Header("DownForce")]
        [SerializeField] private float downForceMin;
        [SerializeField] private float downForceMax;
        [SerializeField] private float downForceFactor;

        [Header("AngularDrag")]
        [SerializeField] private float angularDragMin;
        [SerializeField] private float angularDragMax;
        [SerializeField] private float angularDragFactor;

        public float SteerAngle;
        public float MotorTorque;
        public float BreakTorque;

        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

        private new Rigidbody rigidbody;
        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null)
                rigidbody.centerOfMass = centerOfMass.localPosition;

            for (int i = 0; i < wheelAxles.Length; i++)
                wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxle();
        }

        public float GetAverageRpm()
        {
            float sum = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                sum += wheelAxles[i].GetAverageRpm();
            }
            return sum / wheelAxles.Length;
        }
        public float GetWheelSpeed()
        {
            //2 * 0.1885f - is 2PR *3.6f
            return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
        }

        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
        }

        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }

        private void UpdateWheelAxle()
        {
            int amountMotorWheel = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                if (wheelAxles[i].IsMotor)
                    amountMotorWheel += 2;
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
                wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                wheelAxles[i].ApplyBreakTorque(BreakTorque);
            }
        }

        public void Reset()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}