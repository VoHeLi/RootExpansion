using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{   
    
    public AudioSource audioSource;
    [SerializeField] AudioMixer audioMixer;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("GlobalVolume", volume);
    }
    public void SetVolumeMusic(float volume_music)
    {
        audioMixer.SetFloat("MusicVolume", volume_music);
    }

    public void SetVolumeEffects(float volume_effects)
    {
        audioMixer.SetFloat("EffectsVolume", volume_effects);
    }
}
