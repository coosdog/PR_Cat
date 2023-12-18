using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

    [SerializeField] private VariableJoystick joystick;
    [SerializeField] public GrabButton grabButton;
    [SerializeField] public AttackButton attackButton;    

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        joystick = GetComponentInChildren<VariableJoystick>();
        grabButton = GetComponentInChildren<GrabButton>();
        attackButton   = GetComponentInChildren<AttackButton>();

    }

    
}
