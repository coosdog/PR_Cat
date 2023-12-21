using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

public class TestBody : MonoBehaviour, IHitable
{
    TestPlayer toParent;
    public Rigidbody rb;


    void Start()
    {
        toParent= GetComponentInParent<TestPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IAttackable attackable))
        {
            Vector3 attackPos = other.transform.position;
            Hit(attackable, attackPos);
        }
    }
    public GameObject Obj
    {
        get => gameObject;
    }


    public void Hit(IAttackable attackable, Vector3 attackPos)
    {
        attackable.Attack(toParent, attackPos);
    }
  

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            rb.AddForce(Vector3.forward * 1000, ForceMode.Impulse);
        }
    }

}
