using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Racing
{
    public class CarCameraVignette : CarCameraComponent
    {
        [SerializeField] private PostProcessVolume postProcessVolume; // Ссылка на компонент Post Process Volume
        [SerializeField][Range(0.0f, 1.0f)] private float maxVignetteIntensity;

        private Vignette vignette;

        private void Start()
        {
            // Получаем компонент Vignette из профиля пост-обработки
            postProcessVolume.profile.TryGetSettings(out vignette);
            vignette.enabled.value = true;
        }

        private void Update()
        {
            vignette.intensity.value = Mathf.Clamp(car.NormalizeLinearVelocity, 0, maxVignetteIntensity);
        }
    }
}