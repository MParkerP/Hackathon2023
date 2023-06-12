using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void PlayGame()
    {
        OperationsManager.instance.validPossibleSettings = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ConditionalResetScene()
    {
        if (OperationsManager.instance.validPossibleSettings)
        {
            OperationsManager.instance.validPossibleSettings = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.instance.allBoxes = new List<GameObject>();
            GameManager.instance.hasCorrectEquation = false;

        }
    }

    public void ResetScene()
    {
        OperationsManager.instance.validPossibleSettings = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.hasCorrectEquation= false;
        GameManager.instance.allBoxes = new List<GameObject>();
    }

    public void goMainMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddOrRemoveOperation(string operation)
    {

        if(OperationsManager.instance.operations.Contains(operation))
        {
            OperationsManager.instance.operations.Remove(operation);
        }
        else
        {
            OperationsManager.instance.operations.Add(operation);
        }
    }

    public void AddOrRemoveTempOperation(string operation)
    {

        if (OperationsManager.instance.possibleOperations.Contains(operation))
        {
            OperationsManager.instance.possibleOperations.Remove(operation);
        }
        else
        {
            OperationsManager.instance.possibleOperations.Add(operation);
        }
    }

    public void SetOperationsFromTemp()
    {
        if (OperationsManager.instance.validPossibleSettings)
        {
            List<string> operations = new List<string>(OperationsManager.instance.possibleOperations);
            OperationsManager.instance.operations = operations;
        }
        
    }

    public void SetDifficulty(int diff)
    {
        OperationsManager.instance.difficulty = diff;
    }

    public void SetTempDifficulty(int diff)
    {
        OperationsManager.instance.possibleDifficulty = diff;
    }

    public void SetDifficultyFromTemp()
    {
        if (OperationsManager.instance.validPossibleSettings)
        {
            int difficulty = OperationsManager.instance.possibleDifficulty;
            OperationsManager.instance.difficulty = difficulty;
        }
    }

    public void ResetSettings()
    {
        OperationsManager.instance.difficulty = 1;
        OperationsManager.instance.operations = new List<string>();
        OperationsManager.instance.possibleDifficulty = 0;
        OperationsManager.instance.possibleOperations = new List<string>();
        GameManager.instance.allBoxes = new List<GameObject>();
        AudioManager.instance.ambience.Stop();
    }

    public void SetEquationIncorrect()
    {
        GameManager.instance.hasCorrectEquation = false;
    }

    public void HideCorrectMenu()
    {
        GameManager.instance.correctAnswerScreen.GetComponent<CanvasGroup>().alpha = 0;
        GameManager.instance.correctAnswerScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    

}
