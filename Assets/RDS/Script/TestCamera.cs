using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;

public class TestCamera : MonoBehaviour
{
    public Temp.TestPlayer player;

    void Update()
    {
        transform.position = new Vector3(0, 8, player.transform.position.z - 10);
    }
}
