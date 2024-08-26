using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Racing
{
    public class CarCameraBlur : CarCameraComponent
    {
        [SerializeField] private PostProcessVolume postProcessVolume; // Ссылка на компонент Post Process Volume
        [SerializeField] private float minMotionBlurSpeed;
        [SerializeField] private float maxShutterAngle;
        [SerializeField] private int maxSampleCount;

        private MotionBlur motionBlur;

        //motionBlur.shutterAngle.value = 270f;  Угол затвора
        //motionBlur.sampleCount.value = 10;     Количество выборок

        private void Start()
        {
            postProcessVolume.profile.TryGetSettings(out motionBlur); ; // Ссылка на компонент Post Process Volume

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