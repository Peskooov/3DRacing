using System;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class UIButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip hover;
        [SerializeField] private AudioClip click;

        private new AudioSource audio;
        private UIButton[] uIButtons;

        private void Start()
        {
            audio = GetComponent<AudioSource>();

            uIButtons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < uIButtons.Length; i++)
            {
                uIButtons[i].PointerEnter += OnPointerEnter;
                uIButtons[i].PointerClick += OnPointerClick;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < uIButtons.Length; i++)
            {
                uIButtons[i].PointerEnter -= OnPointerEnter;
                uIButtons[i].PointerClick -= OnPointerClick;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            audio.PlayOneShot(hover);
        }

        private void OnPointerClick(UIButton button)
        {
            audio.PlayOneShot(click);
        }
    }
}