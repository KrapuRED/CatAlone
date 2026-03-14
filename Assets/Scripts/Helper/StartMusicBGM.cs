using UnityEngine;

public class StartMusicBGM : MonoBehaviour
{
    public string musicName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.instance.PlayMusicBackground(musicName);
    }
}
