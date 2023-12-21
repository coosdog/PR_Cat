using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public abstract class TesBullet : MonoBehaviour, IAttackable
    {        
        private void Update()
        {
            // transform.Translate(transform.forward * 1f);
        }
        public int Atk => atk;

        public GameObject EffectParticle => effectParticle;
        [SerializeField] GameObject effectParticle;

        private int atk;

        public abstract void Attack(TestPlayer player, Vector3 attackerPos);

        public abstract void SpawnEffect();
    }
}

