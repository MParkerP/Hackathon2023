using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public OperationsHolder operationHolder;

    private void Start()
    {
        operationHolder = GameObject.Find("OperationsHolder").GetComponent<OperationsHolder>();
    }

    public ArrayList selectedOperators = new ArrayList();
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

    public void addToOperators(string op)
    {
        selectedOperators.Add(op);
    }

    public void setOperators()
    {
        operationHolder.operators = selectedOperators;
    }
}
