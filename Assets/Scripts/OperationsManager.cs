using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationsManager : MonoBehaviour
{
    public static OperationsManager instance { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int difficulty = 1;
    public List<string> operations = new List<string>();

    public int possibleDifficulty = 0;
    public List<string> possibleOperations = new List<string>();



}
