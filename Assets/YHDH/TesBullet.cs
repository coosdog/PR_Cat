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

        public GameObject EffectParticle => effectParticle_Fire;
        [SerializeField] GameObject effectParticle_Fire;
        [SerializeField] GameObject effectParticle_Hit;
        public GameObject EffectParticle_Hit => effectParticle_Hit;

        protected int atk;
        protected int fireSpeed;

        public abstract void Attack(TestPlayer player, Vector3 attackerPos);

        public abstract void SpawnEffect();
    }
}

