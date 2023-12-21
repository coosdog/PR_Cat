using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip Bgm;
    void Start()
    {
        SoundManager.instance.Play(Bgm, this.transform, true);
    }
    public void ExchangeBGM(bool isloop , AudioClip NextAudio = null )
    {
        SoundManager.instance.RetrunPool(GetComponentInChildren<SoundComponet>().gameObject);
        SoundManager.instance.Play(NextAudio, this.transform,isloop);
    }
    public void RemoveBGM()
    {
        SoundManager.instance.RetrunPool(GetComponentInChildren<SoundComponet>().gameObject);
    }
}