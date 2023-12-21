using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;

public class TrashCamera : MonoBehaviourPun
{
    public VariableJoystick joystick;

    public CinemachineFreeLook cam;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            cam = GetComponent<CinemachineFreeLook>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        cam.m_YAxis.m_InputAxisValue = -joystick.Vertical;//Input.GetAxis("Vertical");
        cam.m_XAxis.m_InputAxisValue = -joystick.Horizontal;// Input.GetAxis("Horizontal");
    }
}
