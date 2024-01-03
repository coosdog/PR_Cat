using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    PhotonView pv;
    [SerializeField]
    Canvas selectUI;
    bool IsEnter => PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount != 1;

    private void Start()
    {
        pv = GetComponent<PhotonView>(); 

        if (PhotonNetwork.IsMasterClient)
        {
            selectUI.gameObject.SetActive(true);
        }
    }
    public void EnterGameMode(int modeNum)
    {
        if (IsEnter)
        {
            pv.RPC("LoadGame", RpcTarget.AllBuffered, modeNum);
        }
        else
        {
            Debug.Log("마스터가 아니거나 플레이어가 1명인 경우");
            // 버튼을 비활성화
        }
    }

    
    [PunRPC]
    public void LoadGame(int modeNum)
    {
        PhotonNetwork.LoadLevel(modeNum);
    }
}

