using JongWoo;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleMain : MonoBehaviour
{
    public Light Wanning;

    float nowTime = 0;
    float holeTime = 5;
    float holePower = 1;
    float holeRadius = 25;
    //Vector3 targetPoint;
    IEnumerator blackHoleCo;
    [SerializeField] Vector3 holePos;
    Collider[] cols;
    List<Collider> colliders;
    AudioSource audio;
    Color wanningColor;

    void Start()
    {
        wanningColor = Color.red;
        audio = GetComponent<AudioSource>();
        holePos = transform.position - 2 * Vector3.up;
        blackHoleCo = BlackHoleCo();
        StartCoroutine(blackHoleCo);
    }
    private void Update()
    {
        if(holePower > 1)
        {
            Wanning.color = wanningColor;
            audio.enabled = true;
            Wanning.transform.Rotate(Vector3.left);
        }
    }

    IEnumerator BlackHoleCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Ȧ�� ������ �ֱ�(5�ʸ���)
            while (nowTime <= holeTime) // ȦŸ�ӵ��� ����
            {
                cols = Physics.OverlapSphere(holePos, holeRadius, 1 << 6 | 1 << 29);

                DisabledGravity(cols, false); // Ž���� ������Ʈ�� �߷²�����
                SetColArr(cols);
                nowTime += Time.deltaTime;
                yield return null;
            }
            DisabledGravity(cols, true); // Ȧ�� ������ �߷� ������� ����
            ReSetColArr(cols);
            holePower++;
            nowTime = 0; // ���� ����ð� �ʱ�ȭ
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
                Debug.Log("����");
                col.GetComponent<PlayerController>().Speed = 0;
            }
            col.GetComponent<Rigidbody>().isKinematic = false;
            col.gameObject.transform.RotateAround(transform.position, Vector3.up, 0.5f); // Ȧ�� �߽������� y��������� 1���� ����
            col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, holePos, holePower * Time.deltaTime); // Ȧ�� ���� �̵�transform.position
        }
    }
    public void ReSetColArr(Collider[] cols)
    {
        foreach (Collider col in cols)
        {
            //if (col.GetComponent<Player>().Weight > holePower)
            //    continue;
            if (col.GetComponent<PlayerController>() != null)
                col.GetComponent<PlayerController>().Speed = 300;
        }
    }

    public void DisabledGravity(Collider[] cols, bool isEnabled)
    {
        foreach (Collider col in cols)
        {
            col.GetComponent<Rigidbody>().useGravity = isEnabled; // �߷� �����״�

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(holePos, holeRadius);
    }

}