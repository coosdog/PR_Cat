using JetBrains.Annotations;
using JongWoo;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointHandler :MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrriger;
    RectTransform rectTransform;

    public float startTime;
    public float dropTime = 1f;

    public static Action grabAct;
    public static Action dropAct;


    IEnumerator startcol;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        startcol = Startcol();
    }

    IEnumerator Startcol()
    {        
        while (true)
        {
            startTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startTime = 0f;
        StartCoroutine(startcol);
        rectTransform.sizeDelta= new Vector2(80f, 80f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(startcol);
        if(startTime > dropTime)
        {
            dropAct();
            startTime = 0f;
            return;
        }
        else  grabAct();

        rectTransform.sizeDelta = new Vector2(100f, 100f);

    }

}
