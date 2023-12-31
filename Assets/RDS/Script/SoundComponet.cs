using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponet : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip,bool loop)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying == false)
            SoundManager.instance.RetrunPool(gameObject);
    }
}
