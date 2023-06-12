using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private CanvasGroup UIGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;


    private void Update()
    {
        if (fadeIn)
        {
            if (UIGroup.alpha < 1)
            {
                UIGroup.alpha += Time.deltaTime;
                if (UIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (UIGroup.alpha >= 0)
            {
                UIGroup.alpha -= Time.deltaTime;
                if (UIGroup.alpha <= 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fadeIn= true;
        fadeOut = false;
    }

    public void OnPointerExit(PointerEventData eventData) 
    { 
        fadeOut= true;
        fadeIn= false;
    }
}
