using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [SerializeField] private float horizontalBoundary = 8.4f;
    [SerializeField] private float verticalBoundary = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0);
        }

        if (transform.position.x < -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0);
        }

        if (transform.position.y > verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundary, 0);
        }

        if (transform.position.y < -verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, -verticalBoundary, 0);
        }
    }
}
