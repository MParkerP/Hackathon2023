using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public GameObject thisButton;

    private void Start()
    {
 
    }

    public void swapButons(GameObject otherButton)
    {
        thisButton.SetActive(false);
        otherButton.SetActive(true);
    }

}
