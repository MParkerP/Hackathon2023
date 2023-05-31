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
using System.Data;

public class GameManager : MonoBehaviour
{
    //Singleton//
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }




    //Members//


    public List<string> operations;
    public int[] generatedEquation;
    public List<string> fullEquation;
    public List<string> equationFromBlocks;
    public string stringBlockEquation;

    public bool hasCorrectEquation = false;

    public GameObject BoxPrefab;
    public Vector3 firstBlockSpawn = new Vector3(-5, 3);
    public Vector3 lastBlockSpawn = new Vector3(0, -2);

    public List<Vector3> possibleSpawns;

    private void Start()
    {
        //operations = OperationsManager.instance.operations;
        generatedEquation = Mathbuds.ExpandedMath.generation(1);
        fullEquation = getFullEquation(generatedEquation);
        SetPossibleSpawns();
        spawnBlocks(fullEquation.Count, fullEquation);
    }


    //Methods//


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

    private List<string> getEquationFromBlocks()
    {
        GameObject block1 = GameObject.Find("BoxPrefab(Clone)");
        Box box1  = block1.GetComponent<Box>();

        while (box1.leftBlock!= null)
        {
            block1 = box1.leftBlock;
            box1 = block1.GetComponent<Box>();
        }

        List<string> result = new List<string>();
        result.Add(box1.blockText);

        while (box1.rightBlock!= null)
        {
            string value = box1.rightBlock.GetComponent<Box>().blockText;

            switch (value)
            {
                case "÷":
                    value = "/";
                    break;

                case "x":
                    value = "*";
                    break;

                case "—":
                    value = "-";
                    break;
            }
            
            result.Add(value);
            block1 = box1.rightBlock;
            box1 = block1.GetComponent<Box>();
        }

        return result;
    }

    public void setEquationFromBlocks()
    {
        List<string> listEquation = getEquationFromBlocks();
        equationFromBlocks = listEquation;

        string result = "";
        for (int i = 0; i < listEquation.Count; i++)
        {
            result += listEquation[i];
        }

        stringBlockEquation = result;

    }

    public bool EvaluateEquation(string equation)
    {
        Debug.Log("im working");
        if (equationFromBlocks.Count != fullEquation.Count + 1)
        {
            return false;
        }

        string[] halves = equation.Split("=");
        string lhs = halves[0];
        string rhs = halves[1];
        double lhResult = Convert.ToDouble(new DataTable().Compute(lhs, null));
        double rhResult = Convert.ToDouble(new DataTable().Compute(rhs, null));
        
        return lhResult == rhResult;
    }

    public void checkCorrect()
    {
        hasCorrectEquation = EvaluateEquation(stringBlockEquation);
        Debug.Log(hasCorrectEquation);
    }










}
