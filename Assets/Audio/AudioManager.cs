using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{   
    public AudioClip[] playlist;
    public AudioSource audioSource;
    [SerializeField] AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

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
