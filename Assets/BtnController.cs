using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BtnController : MonoBehaviour
{
    public void LoadMainScene()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
