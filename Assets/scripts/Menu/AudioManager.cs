using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource playSource;
    [SerializeField] AudioSource musicSource1;
    public AudioClip menu;
    public AudioClip game;
    public AudioClip Button;
    public AudioClip playS;
    public AudioClip footsteps;
    

    public static AudioManager instance;
    
    bool musicPlaying;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        musicSource.clip = menu;

        musicSource.Play();
    }


    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu" && musicPlaying == false)
        {
            musicSource.clip = menu;
            musicSource.Play();
            musicPlaying = true;
        }
        if (currentScene.name == "Game" && musicPlaying)
        {
           
            musicSource.clip = game;
            musicSource.Play();
            musicPlaying = false;
        }
    
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void Playplay(AudioClip clip)
    {
        playSource.PlayOneShot(clip);
    }

}