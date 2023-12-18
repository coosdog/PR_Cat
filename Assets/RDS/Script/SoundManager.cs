using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public GameObject soundComponetPrefab;
    public Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Init();
    }
    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(soundComponetPrefab);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }
    public void RetrunPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        pool.Enqueue(returnObj);
    }
    public SoundComponet Pop()
    {
        //GameObject popObj = pool.Dequeue();
        //popObj.SetActive(true);
        pool.Peek().SetActive(true);
        return pool.Dequeue().GetComponent<SoundComponet>();
    }

    public void Play(AudioClip clip,Transform target = null)
    {
        SoundComponet temp = Pop();
        temp.transform.parent = target;
        temp.Play(clip);
    }

}
