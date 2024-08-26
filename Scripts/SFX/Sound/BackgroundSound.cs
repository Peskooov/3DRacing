using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundSound : MonoBehaviour
    {
        [SerializeField][Range(0.0f, 1.0f)] private float volumeModifier;
        [SerializeField][Range(-3.0f, 3.0f)] private float pitchModifier;

        private AudioSource backgroundAudioSource;

        private void Start()
        {
            backgroundAudioSource = GetComponent<AudioSource>();
            backgroundAudioSource.volume = volumeModifier;
            backgroundAudioSource.pitch = pitchModifier;

            backgroundAudioSource.Play();
        }
    }
}