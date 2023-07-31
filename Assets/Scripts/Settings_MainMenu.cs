using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_MainMenu : MonoBehaviour
{
    public List<Toggle> difficultyToggles;
    public GameObject warningMessage;

    public GameObject divisionWarning;

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

    //ensure that more than division is selected on higher difficulties
    public bool checkValidDivision()
    {
        if (OperationsManager.instance.difficulty != 1)
        {
            if (OperationsManager.instance.operations.Contains("/") && OperationsManager.instance.operations.Count == 1) 
            {
                return false;
            }
        }

        return true;
    }

    public bool checkValidSettings()
    {
        return checkValidDifficulty() && checkValidOperations() && checkValidDivision();
    }

    public void AttemptStartGame()
    {
        if (checkValidSettings())
        {
            //turn off warning message if on
            if (warningMessage.activeSelf)
            {
                warningMessage.SetActive(false);
            }

            if (divisionWarning.activeSelf)
            {
                divisionWarning.SetActive(false);
            }

            //load the game scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (!checkValidDivision())
            {
                if (warningMessage.activeSelf)
                {
                    warningMessage.SetActive(false);
                }

                if (!divisionWarning.activeSelf)
                {
                    divisionWarning.SetActive(true);
                }
            }
            else
            {
                if (divisionWarning.activeSelf)
                {
                    divisionWarning.SetActive(false);
                }

                if (!warningMessage.activeSelf)
                {
                    warningMessage.SetActive(true);
                }
            }
        }
    }
}
