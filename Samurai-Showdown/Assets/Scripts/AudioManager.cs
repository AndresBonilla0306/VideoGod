using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.Log("Más de un AudioManager, esa es mala");
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
    public void PlayInLoop(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void DetenerAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
