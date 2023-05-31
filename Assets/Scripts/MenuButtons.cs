using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
