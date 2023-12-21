using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using JongWoo;
using System;

public abstract class ButtonMode
{
    protected Button owner;
    public ButtonMode(Button owner)
    {
        this.owner = owner;
    }
    public abstract void Function();
}
public class StartButton : ButtonMode
{
    public StartButton(Button owner) : base(owner)
    { }
    public override void Function()
    {
        if (owner.isCreate == false)
        {
            Debug.Log("¿€µø");
            for (int i = 0; i < owner.Maker.Length; i++)
            {
                owner.Maker[i].StartMake();
                //Maker[i].gameObject.SetActive(false);
            }
                owner.isCreate = true;
        }
    }
}
public class EndButton : ButtonMode
{
    public EndButton(Button owner) : base(owner)
    { }
    public override void Function()
    {
        for (int i = 0; i < owner.Maker.Length; i++)
        {
            owner.Maker[i].CancelInvoke();
        }
    }
}

[Flags]
public enum ButtonType
{
    Start = 1 << 0,
    End = 1 << 1
}
public class Button : MonoBehaviour
{
    public CreateCar[] Maker = new CreateCar[2];
    public ButtonType buttonType;
    public List<ButtonMode> buttons = null;

    public bool isCreate = false;

    bool isCheckType(ButtonType type)
    {
        return (buttonType & type) != 0;
    }
    private void Start()
    {
        buttons = new List<ButtonMode>();
        if (isCheckType(ButtonType.Start))
            buttons.Add(new StartButton(this));
        if (isCheckType(ButtonType.End))
            buttons.Add(new EndButton(this));
        //SC.audioSource.loop = true;
    }
    public void Action()
    {
        foreach (ButtonMode b in buttons)
        {
            b.Function();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Temp.TestPlayer>() != null)
        {
            Action();
        }
        if (other.GetComponent<Bounce>() != null)
        {
            Action();
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Temp.TestPlayer>() != null)
        {
            for (int i = 0; i < Maker.Length; i++)
            {
                Maker[i].StartMake();
                //Maker[i].gameObject.SetActive(true);
            }
        }
    }
    */
}
