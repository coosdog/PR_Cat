using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Singleton<T> : MonoBehaviourPun where T : Singleton<T>
{
    public static T instance;
    protected void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);    
    }
}
