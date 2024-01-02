using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleDoor : MonoBehaviour
{
    public GameObject DoorTop;
    public GameObject DoorBottom;
    public GameObject OPObj;
    public CinemachineVirtualCamera VirtualCamera;

    int Correctionvalue = 5;
    int CorrentionValue_Door = 20;
    float openTime = 0;
    float limitTime = 10;
    void Update()
    {
        OPObj.transform.position += Vector3.forward * Time.deltaTime * Correctionvalue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OPObj"))
        {
            StartCoroutine(DoorOpen());
        }
    }
    IEnumerator DoorOpen()
    {
        DoorTop.GetComponent<BoxCollider>().isTrigger = true;
        DoorBottom.GetComponent<BoxCollider>().isTrigger = true;
        DoorTop.GetComponent<Rigidbody>().isKinematic = false;
        DoorBottom.GetComponent<Rigidbody>().isKinematic = false;
        while (openTime <= limitTime)
        {
            DoorTop.gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * Time.deltaTime* CorrentionValue_Door;
            DoorBottom.gameObject.GetComponent<Rigidbody>().velocity = -Vector3.up * Time.deltaTime* CorrentionValue_Door;
            openTime += Time.deltaTime;
        }
        openTime = 0;
        yield return new WaitForSeconds(2f);
        while (openTime <= limitTime)
        {
            DoorTop.gameObject.GetComponent<Rigidbody>().velocity = -Vector3.up * Time.deltaTime * CorrentionValue_Door;
            DoorBottom.gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * Time.deltaTime * CorrentionValue_Door;
            openTime += Time.deltaTime;
        }
        VirtualCamera.Priority = 0;
        yield return new WaitForSeconds(5f);
        DoorTop.transform.position = new Vector3(DoorTop.transform.position.x,0, DoorTop.transform.position.z);
        DoorBottom.transform.position = new Vector3(DoorBottom.transform.position.x, 0, DoorBottom.transform.position.z);
        DoorTop.GetComponent<BoxCollider>().isTrigger = false;
        DoorBottom.GetComponent<BoxCollider>().isTrigger = false;
        DoorTop.GetComponent<Rigidbody>().isKinematic = true;
        DoorBottom.GetComponent<Rigidbody>().isKinematic = true;
        yield break;
    }
}
