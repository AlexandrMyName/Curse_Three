using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIO_Game : MonoBehaviour
{
    private  AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void Play_Step()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
