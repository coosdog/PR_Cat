using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponet : MonoBehaviour
{
    public AudioSource audioSource;

    public void Play(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying == false)
            SoundManager.instance.RetrunPool(gameObject);
    }
}
