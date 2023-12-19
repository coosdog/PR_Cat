using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using JongWoo;
public class Button : MonoBehaviour
{
    public CreateCar[] Maker = new CreateCar[2];

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Temp.TestPlayer>() != null)
        {
            Debug.Log("µé¾î¿È");
            for(int i = 0; i < Maker.Length; i++) 
            {
                Maker[i].CancelInvoke();
                //Maker[i].gameObject.SetActive(false);
            }
        }
        if(other.GetComponent<Bounce>() != null)
        {
            for (int i = 0; i < Maker.Length; i++)
            {
                Maker[i].CancelInvoke();
            }
        }
    }
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
}
