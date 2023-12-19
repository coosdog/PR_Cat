using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Animator animator;


    private void Start()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
        {
            Vector3 direction = cam.transform.localRotation * Vector3.forward * variableJoystick.Vertical +
                cam.transform.localRotation * Vector3.right * variableJoystick.Horizontal; //1215 ī�޶�������� ���� ���뼱
            direction.y = 0f;
            direction = direction.normalized;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            this.transform.rotation = Quaternion.LookRotation(direction);//1215 ī�޶�������� ���� ���뼱
            if (Vector3.Magnitude(direction) <= 0.5)
            {
                animator.SetFloat("RelativeSpeed", 0.5f);   //���̽�ƽ ���� ũ�⿡ ���� �ִϸ��̼� ���� 1206����
            }
            else
            {
                animator.SetFloat("RelativeSpeed", 1f);
            }
        }
        else
        {
            animator.SetFloat("RelativeSpeed", 0f);
        }




    }
}