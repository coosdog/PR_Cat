using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

public class MoneStartScene : MonoBehaviour
{
     CinemachineDollyCart dolly;
     CinemachineVirtualCamera vCam;
    //public Monster monmon;

    private void Start()
    {
        dolly = GetComponent<CinemachineDollyCart>();
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if(dolly.m_Position > 86)
        {
            vCam.Priority = 0;
            //monmon.GetComponent<Monster>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}
