using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    GameManager gameManager;
    private static AudioManager instance = null;
    private AudioSource audioSource;

    public AudioClip relaxSong;
    public AudioClip fightSong;
    public AudioClip endSong;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        audioSource.volume = gameManager.volume;
    }

    public void ChangeAudio(AudioClip audio)
    {
        if(audio != null && audioSource != null)
        {
            audioSource.clip = audio;
            audioSource.Play();
        }
    }
}
