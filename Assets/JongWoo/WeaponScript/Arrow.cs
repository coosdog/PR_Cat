using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Temp;

public class Arrow : TesBullet
{

    public override void Attack(TestPlayer player, Vector3 attackPos)
    {
        player.StunCnt += atk;
        SpawnEffect();
    }

    public override void SpawnEffect()
    {
       //SoundManager.instance.Play("석궁발사소리")
       Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        atk = 5;
        fireSpeed = 5;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.forward * fireSpeed * Time.deltaTime);
    }
}
