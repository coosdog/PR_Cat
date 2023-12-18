using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public AudioClip attackSound;

    private void Start()
    {
        SoundManager.instance.Play(attackSound);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.instance.Play(attackSound);
        }
    }
}
