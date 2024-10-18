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
    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        audioSource.volume = volume;
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
