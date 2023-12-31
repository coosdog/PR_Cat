using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public class Obstacle : MonoBehaviour, IHitable
    {
        public GameObject Obj => throw new System.NotImplementedException();
        public float obstacleHp;
        


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {                
                Hit(attackable, other.transform.position);
            }
        }
        public void Hit(IAttackable attackable, Vector3 attackerPos)
        {
            obstacleHp -= attackable.Atk;
            attackable.SpawnEffect();
        }

    }

}
