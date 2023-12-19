using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using JongWoo;

public class CarMove : MonoBehaviour
{
    int moveSpeed = 30;
    int attackPower = 1000;
    public AudioClip carAudio;
    // Update is called once per frame
    private void Start()
    {
        //SoundManager.instance.Play(carAudio, this.transform);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Temp.TestPlayer>() != null) 
        {
            collision.gameObject.SetActive(false);
            Debug.Log("충돌!");
            //collision.rigidbody.AddForce(new Vector3(0,0,-1000)*Time.deltaTime,ForceMode.Impulse);
            //수정필요 뭔가이상함. 차후 기절수치로 조절
        }
    }
}
