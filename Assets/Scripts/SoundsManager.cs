using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    [SerializeField] private SoundLib soundLib;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioSource loopAudioSource;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound3D(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
        else
        {
            Debug.LogWarning("AudioClip is null.");
        }
    }

    public void PlaySound(string soundName, Vector3 position)
    {
        PlaySound3D(soundLib.getClipFromName(soundName), position);
    }

    public void PlaySound2D(string soundName)
    {
        audioSource.PlayOneShot(soundLib.getClipFromName(soundName));
    }
    public void PlayLoopingSound(string clipName)
    {
        AudioClip clip = soundLib.getClipFromName(clipName);
        if (clip == null){ 
            Debug.LogWarning("AudioClip is null.");
            return;
        }
        if (loopAudioSource.clip == clip && loopAudioSource.isPlaying)
        {
            return; // Already playing this clip
        }
        loopAudioSource.clip = clip;
        loopAudioSource.loop = true;
        loopAudioSource.Play();
    }
    public void StopLoopingSound()
    {
        if (loopAudioSource.isPlaying)
        {
            loopAudioSource.Stop();
        }
    }

}
