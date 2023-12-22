using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;


public class Trap : MonoBehaviour, IAttackable
{
    MovingTrap CheckMoving;
    public int AttackDamege;
    public int Atk => AttackDamege;

    private void Start()
    {
        CheckMoving = GetComponentInParent<MovingTrap>();
    }

    public GameObject EffectParticle => throw new System.NotImplementedException();

    public void Attack(TestPlayer player, Vector3 attackerPos)
    {
        Debug.Log("함정작동");
        Vector3 dir;
        //Vector3 dir = (player.transform.position - attackerPos).normalized;
        if (!CheckMoving.isTest) { dir = Vector3.right; }
        else { dir = Vector3.left; }
        player.StunCnt -= Atk;
        player.GetComponent<Rigidbody>().AddForce(dir * Atk * Time.deltaTime, ForceMode.Impulse);
    }

    public void SpawnEffect()
    {

    }

}
