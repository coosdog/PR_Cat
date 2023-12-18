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
                cam.transform.localRotation * Vector3.right * variableJoystick.Horizontal; //1215 카메라방향으로 수정 유대선
            direction.y = 0f;
            direction = direction.normalized;
            rb.AddForce( direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            this.transform.rotation = Quaternion.LookRotation(direction);//1215 카메라방향으로 수정 유대선
            if (Vector3.Magnitude(direction) <= 0.5)
            {
                animator.SetFloat("RelativeSpeed", 0.5f);   //조이스틱 벡터 크기에 따른 애니메이션 변경 1206수빈
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
