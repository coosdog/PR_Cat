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
        public float Atk => atk;

        public GameObject EffectParticle => effectParticle;
        [SerializeField] GameObject effectParticle;

        private float atk;

        public abstract void Attack(TestPlayer player);

        public abstract void SpawnEffect();
    }
}

