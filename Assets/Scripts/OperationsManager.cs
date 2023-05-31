using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationsManager : MonoBehaviour
{
    public static OperationsManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public List<string> operations = new List<string>();



}
