using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MathBuds;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int a;
    public int b;
    public int c;
    public string d;
    public string e = "=";
    public bool isCorrect;

    public Vector3[] spawnPositions = { new Vector3(-4, 2, 0), new Vector3(-2, 3.5f, 0), new Vector3(0, 2, 0), new Vector3(2, 3.5f, 0), new Vector3(4, 2, 0) };
    public int[] randomArray;

    public string[] currentOrder;

    public float block0x;
    public float block1x;
    public float block2x;
    public float block3x;
    public float block4x;
    public GameObject pauseMenu;

    public int[] equationInts;

    private void Awake()
    {
        equationInts = ExpandedMath.generation(1);
        a = equationInts[0];
        b = equationInts[2];
        c = ExpandedMath.findSolution(equationInts);
        switch (equationInts[1])
        {
            case 1:
                d = "*";
                break;

            case 2:
                d = "/";
                break;

            case 3:
                d = "+";
                break;

            case 4:
                d = "-";
                break;
        }

    }
    private void Start()
    {
        randomArray = new int[5] { 0, 1, 2, 3, 4 };
        System.Random rng = new System.Random();

        for (int i = 4; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            int temp = randomArray[i];
            randomArray[i] = randomArray[j];
            randomArray[j] = temp;
        }
    }

    public float[] getCurrentOrder()
    {
        float[] currentOrder = new float[5];

        block0x = GameObject.Find("Block").transform.position.x;
        currentOrder[0] = block0x;

        block1x = GameObject.Find("Block (1)").transform.position.x;
        currentOrder[1] = block1x;

        block2x = GameObject.Find("Block (2)").transform.position.x;
        currentOrder[2] = block2x;

        block3x = GameObject.Find("Block (3)").transform.position.x;
        currentOrder[3] = block3x;

        block4x = GameObject.Find("Block (4)").transform.position.x;
        currentOrder[4] = block4x;

        for (int i = 0; i < currentOrder.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < currentOrder.Length; j++)
            {
                if (currentOrder[j] < currentOrder[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                float temp = currentOrder[i];
                currentOrder[i] = currentOrder[minIndex];
                currentOrder[minIndex] = temp;
            }
        }

        return currentOrder;

    }

    public string[] currentValueOrder()
    {
        float[] currentOrder = getCurrentOrder();
        string[] currentValueOrder = new string[5];

        for (int i = 0; i < 5; i++)
        { 
            if (currentOrder[i] == block0x)
            {
                currentValueOrder[i] = GameObject.Find("Block").GetComponent<Box>().value;
            }
            else if (currentOrder[i] == block1x)
            {
                currentValueOrder[i] = GameObject.Find("Block (1)").GetComponent<Box>().value;
            }
            else if (currentOrder[i] == block2x)
            {
                currentValueOrder[i] = GameObject.Find("Block (2)").GetComponent<Box>().value;
            }
            else if (currentOrder[i] == block3x)
            {
                currentValueOrder[i] = GameObject.Find("Block (3)").GetComponent<Box>().value;
            }
            else if (currentOrder[i] == block4x)
            {
                currentValueOrder[i] = GameObject.Find("Block (4)").GetComponent<Box>().value;
            }
        }

        return currentValueOrder;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && allSnapping()) 
        {
            isCorrect = checkCorrect();
            Debug.Log(isCorrect);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && isCorrect)
        {
            /*Debug.Log("Regenerating");
            equationInts = ExpandedMath.generation(1);
            a = equationInts[0];
            b = equationInts[2];
            c = ExpandedMath.findSolution(equationInts);
            switch (equationInts[1])
            {
                case 1:
                    d = "*";
                    break;

                case 2:
                    d = "/";
                    break;

                case 3:
                    d = "+";
                    break;

                case 4:
                    d = "-";
                    break;
            }*/

        }
    }

    public bool allSnapping()
    {
        return (GameObject.Find("Block").GetComponent<Box>().snapping &&
                GameObject.Find("Block (1)").GetComponent<Box>().snapping &&
                GameObject.Find("Block (2)").GetComponent<Box>().snapping &&
                GameObject.Find("Block (3)").GetComponent<Box>().snapping &&
                GameObject.Find("Block (4)").GetComponent<Box>().snapping); 
    }

    public bool checkCorrect()
    {
        string[] operators = new string[4];
        operators[0] = "+";
        operators[1] = "-";
        operators[2] = "*";
        operators[3] = "/";

        string op = "";
        string[] reference = currentValueOrder();
        for (int i = 0; i < 4; i++)
        {
            int index = Array.IndexOf(reference, operators[i]);
            if (index >= 0)
            {
                op = operators[i];
            }
        }
        int firstNum;
        int secondNum;
        int answer;

        if (reference[3] == "=")
        {
            if (int.TryParse(reference[0], out firstNum))
            {
                firstNum = int.Parse(reference[0]);
            }

            if (int.TryParse(reference[4], out answer))
            {
                answer = int.Parse(reference[4]);
            }

            if (int.TryParse(reference[2], out secondNum))
            {
                secondNum = int.Parse(reference[2]);
            }
        }
        else
        {
            if (int.TryParse(reference[2], out firstNum))
            {
                firstNum = int.Parse(reference[2]);
            }

            if (int.TryParse(reference[0], out answer))
            {
                answer = int.Parse(reference[0]);
            }

            if (int.TryParse(reference[4], out secondNum))
            {
                secondNum = int.Parse(reference[4]);
            }
        }
        

        

        switch(op)
        {
            case "+":
                return (firstNum + secondNum) == answer;

            case "-":
                return (firstNum - secondNum) == answer;

            case"*":
                return(firstNum * secondNum) == answer;

            case"/":
                return (firstNum / secondNum) == answer;

        }

        return false;

    }
}
