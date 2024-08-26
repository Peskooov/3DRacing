using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        [SerializeField] private Car car;

        [SerializeField] private float pitchModifier;
        [SerializeField] private float volumeModifier;
        [SerializeField] private float rpmModifier;

        [SerializeField] private float basePitch = 1.0f;
        [SerializeField] private float baseVolume = 0.4f;

        private AudioSource engineAudioSource;

        private void Start()
        {
            engineAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            engineAudioSource.pitch = basePitch + pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
            engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
        }
    }
}