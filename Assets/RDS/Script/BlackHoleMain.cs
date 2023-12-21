using JongWoo;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleMain : MonoBehaviour
{
    public Light Wanning;
    public bool isDanger = false;
    public BGM bgm;
    public AudioClip EmergencyBGM;
    public AudioClip Wind;

    float nowTime = 0;
    float holeTime = 5;
    float holePower = 1;
    float holeRadius = 25;
    int dangerCount = 0;
    //Vector3 targetPoint;
    IEnumerator blackHoleCo;
    [SerializeField] Vector3 holePos;
    Collider[] cols;
    List<Collider> colliders;
    Color wanningColor;

    void Start()
    {
        wanningColor = Color.red;
        holePos = transform.position - 2 * Vector3.up;
        blackHoleCo = BlackHoleCo();
        StartCoroutine(blackHoleCo);
    }
    private void Update()
    {
        if(holePower > 1)
        {
            isDanger = true;
            DangerSound();
            Wanning.color = wanningColor;
            Wanning.transform.Rotate(Vector3.left);
        }
    }

    IEnumerator BlackHoleCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // 홀이 열리는 주기(5초마다)
            while (nowTime <= holeTime) // 홀타임동안 진행
            {
                cols = Physics.OverlapSphere(holePos, holeRadius, 1 << 6 | 1 << 29);

                DisabledGravity(cols, false); // 탐지된 오브젝트의 중력꺼버림
                SetColArr(cols);
                nowTime += Time.deltaTime;
                yield return null;
            }
            DisabledGravity(cols, true); // 홀이 닫히면 중력 원래대로 복구
            ReSetColArr(cols);
            holePower++;
            nowTime = 0; // 진행 현재시간 초기화
        }
    }

    void DangerSound()
    {
        if (dangerCount > 1)
        {
            Debug.Log("댄저리턴");
            return;

        }
        if (isDanger)
        {
            Debug.Log("체인지");
            bgm.ExchangeBGM(false, EmergencyBGM);
            dangerCount++;
        }
    }

    public void SetColArr(Collider[] cols)
    {
        foreach (Collider col in cols)
        {
            if(col.GetComponent<Rigidbody>() != null && col.GetComponent<Rigidbody>().mass > holePower)
            {
                continue;
            }
            if (col.GetComponent<PlayerController>() != null)
            {
                Debug.Log("실험");
                col.GetComponent<PlayerController>().Speed = 0;
            }
            col.GetComponent<Rigidbody>().isKinematic = false;
            col.gameObject.transform.RotateAround(transform.position, Vector3.up, 0.5f); // 홀을 중심점으로 y축기준으로 1도로 돌림
            col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, holePos, holePower * Time.deltaTime); // 홀을 향해 이동transform.position
        }
    }
    public void ReSetColArr(Collider[] cols)
    {
        foreach (Collider col in cols)
        {
            //if (col.GetComponent<Player>().Weight > holePower)
            //    continue;
            if (col.GetComponent<PlayerController>() != null)
                col.GetComponent<PlayerController>().Speed = 2.5f;
        }
    }

    public void DisabledGravity(Collider[] cols, bool isEnabled)
    {
        foreach (Collider col in cols)
        {
            col.GetComponent<Rigidbody>().useGravity = isEnabled; // 중력 껐다켰다

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(holePos, holeRadius);
    }

}
