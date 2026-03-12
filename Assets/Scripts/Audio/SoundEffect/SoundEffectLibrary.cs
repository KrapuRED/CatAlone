using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundEffect
{
    public string soundName;
    public List<AudioClip> clipSoundEffects = new List<AudioClip>();
}

public class SoundEffectLibrary : MonoBehaviour
{
    public List<SoundEffect> soundEffects = new List<SoundEffect>();

    public AudioClip GetAudioClipByName(string soundName)
    {
        for (int i = 0; i < soundEffects.Count; i++)
        {
            if (soundEffects[i].soundName == soundName)
            {
                int randomIndex = Random.Range(0, soundEffects[i].clipSoundEffects.Count);
                return soundEffects[i].clipSoundEffects[randomIndex];
            }
        }

        return null;
    }
}
