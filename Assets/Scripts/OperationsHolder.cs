using System.Collections;
using UnityEngine;

public class OperationsHolder : MonoBehaviour
{
    public ArrayList operators = new ArrayList();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
