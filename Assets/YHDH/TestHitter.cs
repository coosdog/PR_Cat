using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
    public class TestHitter : MonoBehaviour, IHitable
    {
        public GameObject Obj => throw new System.NotImplementedException();
        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void Hit(IAttackable attackable, Vector3 attackerPos)
        {
            rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                Hit(attackable, other.transform.position);
            }
        }
    }
}


