using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MusicManager : MonoBehaviour
{
    #region Static Instance
    private static MusicManager instance;
    public static MusicManager Instance
    {

        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned MusicManager", typeof(MusicManager)).GetComponent<MusicManager>();
                }
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        // Make sure we don't destroy this instance
        DontDestroyOnLoad(this.gameObject);

        //Create audio sources, and save them as references
        AddSources();

        // Loop the music tracks
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    private void AddSources()
    {
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        activeSource.clip = musicClip;
        //activeSource.volume = 1;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {       
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        if (newClip != activeSource.clip)
            StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        // Swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        // Set the fields of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;
        float v = ((float)GetComponent<Instilinger>().soundLevel / 100); //telle volume Instillenger med

        //Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (v - (t / transitionTime));
            yield return null;
        }

       

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // Fade in

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            if (v > (t / transitionTime))
            {
                activeSource.volume = (t / transitionTime);
                yield return null;
            }
            else
                activeSource.volume = v;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;

        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}