using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private MusicLib musicLibrary;
    [SerializeField] private AudioSource musicSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusic(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }

    IEnumerator AnimateMusic(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * (1 / fadeDuration);
            musicSource.volume = Mathf.Lerp(1, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * (1 / fadeDuration);
            musicSource.volume = Mathf.Lerp(0, 1, percent);
            yield return null;
        }
    }
    public void StopMusic(float fadeDuration = 0.5f)
    {
        StartCoroutine(FadeOutMusic(fadeDuration));
    }
    IEnumerator FadeOutMusic(float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * (1 / fadeDuration);
            musicSource.volume = Mathf.Lerp(1, 0, percent);
            yield return null;
        }
        musicSource.Stop();
    }
}
