using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using JongWoo;
using Temp;

public class CarMove : MonoBehaviour, IAttackable
{
    int moveSpeed = 30;
    int attackPower = 750;
    public AudioClip carAudio;

    public int Atk => attackPower;    

    public GameObject EffectParticle => throw new System.NotImplementedException();

    // Update is called once per frame
    private void Start()
    {
        //SoundManager.instance.Play(carAudio, this.transform);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
    }
    public void SpawnEffect()
    {
        
    }

    public void Attack(TestPlayer player, Vector3 attackerPos)
    {
        //Vector3 dir = Vector3.forward;
        Vector3 dir = (player.transform.position - (Vector3.back * 10)).normalized;
        //dir = dir + (player.transform.position - attackerPos).normalized;
        player.StunCnt += Atk;
        player.GetComponent<Rigidbody>().AddForce(-dir * Atk* Time.deltaTime, ForceMode.Impulse);
    }
}
