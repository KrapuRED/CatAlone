using UnityEngine;
using System.Collections.Generic;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    [SerializeField] private SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private List<AudioSource> soundEffectSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySoundEffect(string soundName)
    {
        AudioClip clipSoundEffect = soundEffectLibrary.GetAudioClipByName(soundName);

        AudioSource freeSource = FreeSoundEffectSource();

        freeSource.PlayOneShot(clipSoundEffect);

    }

    private AudioSource FreeSoundEffectSource()
    {
        foreach(AudioSource source in soundEffectSources)
        {
            if (!source.isPlaying)
                return source;
        }

        return soundEffectSources[0];
    }

    public void StopPlayAudio()
    {
        foreach (AudioSource source in soundEffectSources)
        {
            if (source.isPlaying)
                source.Stop();
        }
    }
}
