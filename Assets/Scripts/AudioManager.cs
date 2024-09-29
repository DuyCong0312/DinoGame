using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;

    public AudioClip buttonClip;
    public AudioClip hitClip;
    public AudioClip scoreReachClip;

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxAudioSource.clip = sfxClip;
        sfxAudioSource.PlayOneShot(sfxClip);
    }

}
