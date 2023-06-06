using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
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
}
