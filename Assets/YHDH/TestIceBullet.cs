using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public class TestIceBullet : TesBullet
    {
        public IEnumerator StartColdCo(PlayerController pc)
        {
            float defalutSpeed = pc.Speed;
            pc.Speed = 0f;
            yield return new WaitForSeconds(3f);
            pc.Speed = defalutSpeed;
        }

        public override void Attack(TestPlayer player, Vector3 attackerPos)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            //StartCoroutine(StartColdCo(pc));
            float defalutSpeed = pc.Speed;
            player.StunCnt += atk;
            SpawnEffect();
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
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
}
