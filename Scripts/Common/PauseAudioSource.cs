using Racing;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
{
    private new AudioSource audio;

    private Pauser pauser;
    public void Construct(Pauser obj) => pauser = obj;


    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChanged;
    }
    private void OnPauseStateChanged(bool isPause)
    {
        if (!isPause)
            audio.Play();

        if (isPause)
            audio.Stop();
    }
}