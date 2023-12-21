using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

public class BB_Bullet : TesBullet
{
    public override void Attack(TestPlayer player, Vector3 attackPos)
    {
        player.StunCnt += atk;
        SpawnEffect();
    }

    public override void SpawnEffect()
    {
        //SoundManager.instance.Play("ºñºñÅº Â÷Ä¬Â÷Ä¬")
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        atk = 1;
        fireSpeed = 3;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.forward * fireSpeed * Time.deltaTime);

    }
}
