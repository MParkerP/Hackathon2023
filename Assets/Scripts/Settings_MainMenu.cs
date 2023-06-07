using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_MainMenu : MonoBehaviour
{
    public List<Toggle> difficultyToggles;
    public GameObject warningMessage;

    //ensure that at least one difficulty toggle is selected
    public bool checkValidDifficulty()
    {
        foreach(Toggle toggle in difficultyToggles)
        {
            if (toggle.isOn)
            {
                return true;
            }
        }

        return false;
    }

    //ensure that at least one operation has been selected
    public bool checkValidOperations()
    {
        if (OperationsManager.instance.operations.Count > 0)
        {
            return true;
        }

        return false;
    }

    public bool checkValidSettings()
    {
        return checkValidDifficulty() && checkValidOperations();
    }

    public void AttemptStartGame()
    {
        if (checkValidSettings())
        {
            //turn of warning message if on
            if (warningMessage.activeSelf)
            {
                warningMessage.SetActive(false);
            }

            //load the game scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //turn on warning message if off
            if (!warningMessage.activeSelf)
            {
                warningMessage.SetActive(true);
            }
        }
    }
}
