using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public class MeleeAttackItem : Item, IAttackable
    {
        public int Atk => weapon.Atk;

        public GameObject EffectParticle => weapon.EffectParticle;

        public void Attack(TestPlayer player, Vector3 attackerPos)
        {
            if (weapon == null) // 주인이 있을 때만 공격
                return;            
            weapon.Attack(player, attackerPos);
        }

        public void SpawnEffect()
        {
            weapon.SpawnEffect();
        }
    }

}
