using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class  MusicAlbum
{
    public string NameMusic;
    public AudioClip MusicClip;
}

public class MusicLibrary : MonoBehaviour
{
    [SerializeField] private List<MusicAlbum> musicAlbums = new List<MusicAlbum>();

    public AudioClip GetMusicClipByName(string name)
    {
        foreach (var album in musicAlbums)
        {
            if (album.NameMusic == name)
                return album.MusicClip;
        }
        Debug.LogWarning($"[MusicLibrary - GetMusicClipByName] Music with name {name} not found.");
        return null;
    }
}
