using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Mathbuds;
using JetBrains.Annotations;
using Random = System.Random;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //Singleton//
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }




    //Members and methods//

    public List<string> operations;
    public int[] generatedEquation;
    public List<string> fullEquation;

    public GameObject BoxPrefab;
    public Vector3 firstBlockSpawn = new Vector3(-5, 3);
    public Vector3 lastBlockSpawn = new Vector3(0, -2);

    public List<Vector3> possibleSpawns;

    private void Start()
    {
        operations = OperationsManager.instance.operations;
        generatedEquation = Mathbuds.ExpandedMath.generation(1);
        fullEquation = getFullEquation(generatedEquation);
        SetPossibleSpawns();
        spawnBlocks(fullEquation.Count, fullEquation);
    }

    private List<string> getFullEquation(int[] array)
    {
        string[] possibleOperators = {"", "x", "÷", "+", "—" };  //blank first one because table started at 1 not 0
        List<string> result = new List<string>();

        for(int i = 0; i < array.Length; i++)
        {   
            //if even index of array
            if (i % 2 == 0)
            {
                result.Add(array[i].ToString());
            }

            //if odd index of array
            else if(i % 2 != 0)
            {
                result.Add(possibleOperators[array[i]]);
            }
        }

        //add solution to the equation
        result.Add(Mathbuds.ExpandedMath.findSolution(generatedEquation).ToString());

        return result;
    }

    private void spawnBlocks(int numBlocks, List<string> equation)
    {
        Vector3 spawnPosition = firstBlockSpawn;

        for (int i = 0; i < numBlocks; i++)
        {
            GameObject thisBlock = Instantiate(BoxPrefab, possibleSpawns[i], BoxPrefab.transform.rotation);
            thisBlock.GetComponent<Box>().blockText = equation[i].ToString();
        }

        GameObject lastBlock = Instantiate(BoxPrefab, lastBlockSpawn, BoxPrefab.transform.rotation);
        lastBlock.GetComponent<Box>().blockText = "=";
    }

    private void SetPossibleSpawns()
    {
        List<Vector3> spawns= new List<Vector3>();
        spawns.Add(new Vector3(-5, 3));
        spawns.Add(new Vector3(-3.5f, 1));
        spawns.Add(new Vector3(-2, 3));
        spawns.Add(new Vector3(-0.5f, 1));
        spawns.Add(new Vector3(1, 3));
        spawns.Add(new Vector3(2.5f, 1));
        spawns.Add(new Vector3(4, 3));
        spawns.Add(new Vector3(5.5f, 1));
        spawns.Add(new Vector3(7, 3));

        List<Vector3> currentPossibleSpawns = new List<Vector3>();
        for (int i = 0; i < fullEquation.Count; i++)
        {
            currentPossibleSpawns.Add(spawns[i]);
        }

        Random rng = new Random();
        possibleSpawns = currentPossibleSpawns.OrderBy(a => rng.Next()).ToList();

    }


    







}
