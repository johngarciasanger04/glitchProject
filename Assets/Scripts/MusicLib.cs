using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string trackName;
    public AudioClip clip;
}

public class MusicLib : MonoBehaviour
{
    public MusicTrack[] musicTracks;

    public AudioClip GetClipFromName(string name)
    {
        foreach (var track in musicTracks)
        {
            if (track.trackName == name)
            {
                return track.clip;
            }
        }
        return null;
    }

}
