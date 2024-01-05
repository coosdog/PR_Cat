using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.UIElements;
using TMPro;
public class UIManager : MonoBehaviour
{
    PhotonView pv;
    [SerializeField]
    Canvas selectUI;
    bool IsEnter => PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1;

    [SerializeField] TextMeshProUGUI ui;

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
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }

        else
        {
            //ui.GetComponent<Animator>().SetTrigger("Start");
            ui.gameObject.SetActive(true);
            StartCoroutine(PlayerLimitCo());
        }
    }
    IEnumerator PlayerLimitCo()
    {
        yield return new WaitForSeconds(2f);
        ui.gameObject.SetActive(false);
    }

    [PunRPC]
    public void LoadGame(int modeNum)
    {
        PhotonNetwork.LoadLevel(modeNum);
    }
}

