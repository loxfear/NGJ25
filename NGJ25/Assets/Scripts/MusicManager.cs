using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Playlist")]
    public AudioClip[] musicClips;

    [Header("Audio Settings")]
    public float fadeDuration = 2f;
    public float volume = 1f;

    [SerializeField] private AudioSource currentSource;
    [SerializeField] private AudioSource nextSource;

    private List<int> unplayedIndices = new List<int>();
    private int currentTrackIndex = -1;
    private bool isFading = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Setup audio sources
        if(!currentSource) currentSource = gameObject.AddComponent<AudioSource>();
        if(!currentSource) nextSource = gameObject.AddComponent<AudioSource>();

        currentSource.loop = false;
        nextSource.loop = false;

        currentSource.volume = volume;
        nextSource.volume = 0f;
    }

    void Start()
    {
        ResetPlaylist();
        PlayNextTrack();
    }

    void Update()
    {
        if (!isFading && !currentSource.isPlaying && musicClips.Length > 0)
        {
            StartCoroutine(FadeToNextTrack());
        }
    }

    void ResetPlaylist()
    {
        unplayedIndices.Clear();
        for (int i = 0; i < musicClips.Length; i++)
        {
            unplayedIndices.Add(i);
        }
    }

    int GetRandomTrackIndex()
    {
        if (unplayedIndices.Count == 0)
        {
            ResetPlaylist();
        }

        int rand = Random.Range(0, unplayedIndices.Count);
        int selectedIndex = unplayedIndices[rand];
        unplayedIndices.RemoveAt(rand);
        return selectedIndex;
    }

    void PlayNextTrack()
    {
        currentTrackIndex = GetRandomTrackIndex();
        PlayTrack(currentTrackIndex, currentSource);
    }

    IEnumerator FadeToNextTrack()
    {
        isFading = true;

        // Select and prepare next track
        int nextIndex = GetRandomTrackIndex();
        nextSource.clip = musicClips[nextIndex];
        nextSource.Play();

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = t / fadeDuration;
            currentSource.volume = Mathf.Lerp(volume, 0f, normalized);
            nextSource.volume = Mathf.Lerp(0f, volume, normalized);
            yield return null;
        }

        // Swap sources
        var temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;

        nextSource.Stop();
        nextSource.volume = 0f;

        currentTrackIndex = nextIndex;
        isFading = false;
    }

    void PlayTrack(int index, AudioSource source)
    {
        if (musicClips.Length == 0) return;

        source.clip = musicClips[index];
        source.volume = volume;
        source.Play();
    }
}
