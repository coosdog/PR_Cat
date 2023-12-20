using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip Bgm;
    void Start()
    {
        SoundManager.instance.Play(Bgm, this.transform, false);
    }
    public void ExchangeBGM(bool isloop , AudioClip NextAudio = null )
    {
        Debug.Log("ÀÌ¸ö ¹ßµ¿");
        SoundManager.instance.RetrunPool(GetComponentInChildren<SoundComponet>().gameObject);
        SoundManager.instance.Play(NextAudio, this.transform,isloop);
    }
}
