using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingsMenu.activeSelf)
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
            
        }
    }
}
