using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_Main : MonoBehaviour
{
    //Operations Selection
    public List<Toggle> operationToggles = new List<Toggle>();
    public Toggle plusToggle;
    public Toggle minusToggle;
    public Toggle divideToggle;
    public Toggle timesToggle;

    //Difficulty Selection
    public List<Toggle> difficultyToggles = new List<Toggle>();
    public ToggleGroup diffcultyToggleGroup;

    public GameObject warningMessage;
    public GameObject divisionWarning;
    public AudioMixerGroup soundEffectsGroup;

    private void Start()
    {
        StartCoroutine(UnmuteClickSound());
        SetSettings();
        OperationsManager.instance.possibleOperations = new List<string>(OperationsManager.instance.operations);
    }

    private void SetOperationToggles()
    {
        List<string> operations = OperationsManager.instance.operations;

        if (operations.Contains("/"))
        {
            divideToggle.isOn= true;
        }
        else if (!operations.Contains("/"))
        {
            divideToggle.isOn= false;
        }

        if (operations.Contains("*"))
        {
            timesToggle.isOn = true;
        }
        else if (!operations.Contains("*"))
        {
            timesToggle.isOn= false;
        }

        if (operations.Contains("+"))
        {
            plusToggle.isOn = true;
        }
        else if (!operations.Contains("+"))
        {
            plusToggle.isOn = false;
        }

        if (operations.Contains("-"))
        {
            minusToggle.isOn = true;
        }
        else if (!operations.Contains("-"))
        {
            minusToggle.isOn = false;
        }


    }

    public void SetSettings()
    {
        foreach(Toggle toggle in difficultyToggles)
        {
            toggle.isOn = false;
        }
        int difficulty = OperationsManager.instance.difficulty;
        difficultyToggles[difficulty - 1].isOn = true;
        SetOperationToggles();
    }

    public void GMSetOperators()
    {
        GameManager.instance.SetOperations();
    }

 

    //ensure that at least one difficulty toggle is selected
    public bool checkValidDifficulty()
    {
        foreach (Toggle toggle in difficultyToggles)
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
        if (OperationsManager.instance.possibleOperations.Count > 0)
        {
            return true;
        }

        return false;
    }

    public bool checkValidDivision()
    {
        if (OperationsManager.instance.possibleDifficulty != 1)
        {
            if (OperationsManager.instance.possibleOperations.Contains("/") && OperationsManager.instance.possibleOperations.Count == 1)
            {
                return false;
            }
        }

        return true;
    }

    public void checkValidSettings()
    {
        bool result = checkValidDifficulty() && checkValidOperations() && checkValidDivision();
        OperationsManager.instance.validPossibleSettings = result;
        if (result == false)
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

        if (result == true)
        {
            if (warningMessage.activeSelf)
            {
                warningMessage.SetActive(false);
            }
        }
    }

    private IEnumerator UnmuteClickSound()
    {
        AudioManager.instance.clickSoundEffect.volume = 0;
        yield return new WaitForSeconds(1f);
        AudioManager.instance.clickSoundEffect.volume = 0.25f;
    }
}
