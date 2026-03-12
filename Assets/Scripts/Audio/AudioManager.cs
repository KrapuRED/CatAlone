using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio SoundEffect & Music Config")]
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] protected SoundEffectManager _soundEffectManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    public void StopSoundEffect()
    {
        _soundEffectManager.StopPlayAudio();
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        _soundEffectManager.PlaySoundEffect(soundEffectName);
    }

    public void PlayMusicBackground (string musicTrackName)
    {

    }
}
