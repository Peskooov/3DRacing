using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class GearShiftSound : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField][Range(0.0f, 1.0f)] private float volumeModifier;
        [SerializeField][Range(-3.0f, 3.0f)] private float pitchModifier;

        private AudioSource gearShiftAudioSource;

        private void Start()
        {
            gearShiftAudioSource = GetComponent<AudioSource>();
            gearShiftAudioSource.volume = volumeModifier;
            gearShiftAudioSource.pitch = pitchModifier;
        }

        private void OnEnable()
        {
            if (car != null)
            {
                car.GearChanged += OnGearChanged;
            }
        }

        private void OnDisable()
        {
            if (car != null)
            {
                car.GearChanged -= OnGearChanged;
            }
        }

        private void OnGearChanged(string gear)
        {
            if (gear != "N" && car.LinearVelocity > 5)
                gearShiftAudioSource.Play();
        }
    }
}