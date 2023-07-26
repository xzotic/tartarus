using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Config", menuName = "Guns/Audio Config", order = 5)]
public class AudioConfigSO : ScriptableObject
{
    [Range(0, 1f)]
    public float volume = 1f;
    public AudioClip[] fireClips;
    public AudioClip emptyClip;
    public AudioClip reloadClip;
    public AudioClip[] impactClips;

    public void PlayShootingClip(AudioSource audioSource)
    {
        audioSource.PlayOneShot(fireClips[Random.Range(0, fireClips.Length)], volume);
    }

    public void PlayNoAmmoClip(AudioSource audioSource)
    {
        audioSource.PlayOneShot(emptyClip, volume);
    }

    public void PlayReloadClip(AudioSource audioSource)
    {
        audioSource.PlayOneShot(reloadClip, volume);
    }

    public void PlayImpactClip(AudioSource audioSource)
    {
        audioSource.PlayOneShot(impactClips[Random.Range(0, fireClips.Length)], volume);
    }
}
