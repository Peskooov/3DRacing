using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Racing
{
    public class CarCameraBlur : CarCameraComponent
    {
        [SerializeField] private PostProcessVolume postProcessVolume; // ������ �� ��������� Post Process Volume
        [SerializeField] private float minMotionBlurSpeed;
        [SerializeField] private float maxShutterAngle;
        [SerializeField] private int maxSampleCount;

        private MotionBlur motionBlur;

        //motionBlur.shutterAngle.value = 270f;  ���� �������
        //motionBlur.sampleCount.value = 10;     ���������� �������

        private void Start()
        {
            postProcessVolume.profile.TryGetSettings(out motionBlur); ; // ������ �� ��������� Post Process Volume

            motionBlur.enabled.value = true;
            motionBlur.sampleCount.value = maxSampleCount;
        }

        private void Update()
        {
            if (car.LinearVelocity >= minMotionBlurSpeed)
                motionBlur.shutterAngle.value = Mathf.Clamp(car.LinearVelocity, 0, maxShutterAngle);

            else
                motionBlur.shutterAngle.value = 0;
        }
    }
}