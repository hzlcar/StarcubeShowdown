/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Manages all audio files including looping music and oneshot sound effects
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public List<AudioClip> backgroundLoops = new List<AudioClip>();
    public List<AudioClip> playerSounds = new List<AudioClip>();
    public List<AudioClip> enemySounds = new List<AudioClip>();

    public float fadeOutDuration = 2.0f;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        BeginMusicInNewScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {

    }

    public void PlayMusic(AudioClip sound, float volume)
    {
        if (audioSource1.isPlaying)
        {
            audioSource2.clip = sound;
            audioSource2.volume = volume;
            audioSource2.Play();
        }
        else
        {
            audioSource1.clip = sound;
            audioSource1.volume = volume;
            audioSource1.Play();
        } 
    }

    public void PlaySound(AudioClip sound)
    {
        print("Good job");
    }

    private void BeginMusicInNewScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        switch (sceneName)
        {
            case "TitleScene":
                PlayMusic(backgroundLoops[0], 0.2f);
                break;
            case "GameScene":
                PlayMusic(backgroundLoops[1], 0.2f);
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Call your function when a new scene is loaded
        BeginMusicInNewScene(scene.name);
    }

    public IEnumerator FadeOutMusic()
    {
        AudioSource currAudioSource;
        if (audioSource1.isPlaying)
        {
            currAudioSource = audioSource1;
        }
        else
        {
            currAudioSource = audioSource2;
        }

        float startVolume = currAudioSource.volume;

        // Gradually decrease the volume over time
        while (currAudioSource.volume > 0)
        {
            currAudioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }
        currAudioSource.volume = 0;
    }


}

