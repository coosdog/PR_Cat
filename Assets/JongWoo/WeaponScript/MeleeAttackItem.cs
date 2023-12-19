using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public class MeleeAttackItem : Item, IAttackable
    {
        public float Atk => weapon.Atk;

        public GameObject EffectParticle => weapon.EffectParticle;

        public void Attack(TestPlayer player)
        {
            if (weapon == null)
                return;            
            weapon.Attack(player);
        }

        public void SpawnEffect()
        {
            weapon.SpawnEffect();
        }
    }

}
