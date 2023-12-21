using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ISM
{
    //AudioClip clip { get; }
}

public class SoundManager : Singleton<SoundManager>
{
    //public static SoundManager instance = null;
    public GameObject soundComponetPrefab;
    public Queue<GameObject> pool = new Queue<GameObject>();

    public AudioClip testclip;

    private void Awake()
    {
        base.Awake();
        Init();
    }
    private void Start()
    {
        /*
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        */
        //Init();
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

    public void Play(AudioClip clip, Transform target, bool loop)
    {
        if (pool.Count <= 2)
        {
            Init();
        }
        SoundComponet temp = Pop();
        temp.transform.parent = target;
        temp.transform.position = target.transform.position;
        temp.Play(clip,loop);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            instance.Play(testclip,this.transform, true);
        }
    }
}
