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

        public override void Attack(TestPlayer player)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            StartCoroutine(StartColdCo(pc));
        }

        public override void SpawnEffect()
        {

        }

        private void Update()
        {
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
}
