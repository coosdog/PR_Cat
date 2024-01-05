using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Temp;

public class ElectricBullet : TesBullet
{
    IEnumerator<TestPlayer> shock;


    IEnumerator TurnOffAniCo(TestPlayer player)
    {
        player.animComponent.Animator.SetBool("ElectBool", true);
        yield return new WaitForSeconds(2f);
        player.animComponent.Animator.SetBool("ElectBool", false);
    }

    private void Start()
    {
        atk = 10;
        fireSpeed = 8;


        if (EffectParticle != null)
        {
            GameObject flashInstance = Instantiate(EffectParticle, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            ParticleSystem flashTime = flashInstance.GetComponent<ParticleSystem>();
            Destroy(flashInstance, flashTime.main.duration);
        }
        Destroy(gameObject, 3);

    }

    public override void Attack(TestPlayer player, Vector3 attackPos)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        float defaultSpeed = pc.Speed;
        //pc.Speed = 0f;
        player.StunCnt += atk;
        //StartCoroutine(TurnOffAniCo(player));
        //pc.Speed = defaultSpeed;  //12.19 JongWoo: pc.Speed must be return where after this Animator Or after Ragdoll script
        SpawnEffect();
    }

    public override void SpawnEffect()
    {
        if (EffectParticle_Hit != null)
        {
            GameObject hitInstance = Instantiate(EffectParticle_Hit, gameObject.transform.position, Quaternion.identity);
            ParticleSystem hitTime = hitInstance.GetComponent<ParticleSystem>();
            Destroy(hitInstance, hitTime.main.duration);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * fireSpeed * Time.deltaTime);
    }
}
