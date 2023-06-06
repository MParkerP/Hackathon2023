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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        List<string> operations = new List<string>(OperationsManager.instance.possibleOperations);
        OperationsManager.instance.operations = operations;
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
        int difficulty = OperationsManager.instance.possibleDifficulty;
        OperationsManager.instance.difficulty = difficulty;
    }

    public void ResetSettings()
    {
        OperationsManager.instance.difficulty = 1;
        OperationsManager.instance.operations = new List<string>();
        OperationsManager.instance.possibleDifficulty = 0;
        OperationsManager.instance.possibleOperations = new List<string>();
    }

}
