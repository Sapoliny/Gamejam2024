using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour //cortesia do chatgpt (nao é que isto seja complexo)
{
    // Array to hold multiple audio clips
    public AudioClip[] songs;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Index to track the currently playing clip
    private int currentClipIndex = -1;

    // Time to wait before restarting the song (in seconds)
    public float restartDelay = 3.0f;

    private float volume;

    public static BackgroundMusic instance;

    void Awake()
    {
        // Implement Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent destruction on scene load
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy any additional instances
            return;  // Prevent further execution
        }
    }
    void Start()
    {
        // Prevent this object from being destroyed on scene load
        DontDestroyOnLoad(gameObject);

        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the GameObject.");
        }

        PlayAudioById(0);

        volume = PlayerPrefs.GetInt("Volume", 100) / 100f;
        SetVolume(volume);

        if (PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            Mute();
        }
    }

    void Update()
    {
        // Check if audio finished playing and if there is a valid clip
        if (!audioSource.isPlaying && currentClipIndex != -1)
        {
            StartCoroutine(RestartAudioAfterDelay());
        }
    }

    // Play an audio clip by its ID (array index)
    public void PlayAudioById(int id)
    {
        if (id >= 0 && id < songs.Length)
        {
            if (songs[id] != null)
            {
                currentClipIndex = id;
                audioSource.clip = songs[id];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioClip at index " + id + " is null.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid audio clip ID.");
        }
    }

    // Pause the currently playing audio
    public void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            Debug.LogWarning("No audio is currently playing to pause.");
        }
    }

    // Resume playing the currently paused audio
    public void ResumeAudio()
    {
        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            Debug.LogWarning("No audio is paused or no audio clip is set.");
        }
    }

    // Stop the currently playing audio
    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            currentClipIndex = -1; // Reset current clip index when stopped
            StopAllCoroutines();   // Stop the auto-restart coroutine if manually stopped
        }
        else
        {
            Debug.LogWarning("No audio is currently playing to stop.");
        }
    }

    // Change the volume of the audio source (0.0 to 1.0)
    public void SetVolume(float volumeIn)
    {
        volume = volumeIn;
        PlayerPrefs.SetInt("Volume", (int)(volumeIn * 100f));
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            audioSource.volume = volume;
        }
        
    }

    public void Mute()
    {
        audioSource.volume = 0;
        PlayerPrefs.SetInt("Muted", 1);
    }

    public void UnMute()
    {
        audioSource.volume = volume;
        PlayerPrefs.SetInt("Muted", 0);
    }

    // Get the current volume level
    public float GetVolume()
    {
        return audioSource.volume;
    }

    // Check if an audio is currently playing
    public bool IsAudioPlaying()
    {
        return audioSource.isPlaying;
    }

    // Get the currently playing audio ID (array index)
    public int GetCurrentAudioId()
    {
        return currentClipIndex;
    }

    // Coroutine to restart the audio after a delay
    private IEnumerator RestartAudioAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(restartDelay);

        // Restart the currently playing clip if it's valid
        if (currentClipIndex != -1 && songs[currentClipIndex] != null)
        {
            audioSource.Play();
        }
    }
}
