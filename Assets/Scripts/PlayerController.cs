using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //player movement
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D playerRb;
    public float speed = 5;

    //screen bounds
    [SerializeField] private float horizontalBoundary = 8.4f;
    [SerializeField] private float verticalBoundary = 4.5f;

    //grabbing
    public GameObject currentBlock;
    public bool canGrab;
    public bool isGrabbing;





    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
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


        if (Input.GetKey(KeyCode.Space))
        {
            if (currentBlock != null)
            {
                isGrabbing = true;
                speed = 3;            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isGrabbing = false;
            speed = 5;
        }
    }

    private void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (!isGrabbing)
        {
            switch (verticalInput, horizontalInput)
            {
                case (1, 0):
                    transform.rotation = Quaternion.Euler(0, 0, 270); //moving up
                    break;

                case (-1, 0):
                    transform.rotation = Quaternion.Euler(0, 0, 90); //moving down
                    break;

                case (0, 1):
                    transform.rotation = Quaternion.Euler(0, 0, 180); //moving right
                    break;

                case (0, -1):
                    transform.rotation = Quaternion.Euler(0, 0, 0); //moving left
                    break;



                case (1, -1):
                    transform.rotation = Quaternion.Euler(0, 0, 315); //moving up-left
                    break;

                case (1, 1):
                    transform.rotation = Quaternion.Euler(0, 0, 225); //moving up-right
                    break;

                case (-1, -1):
                    transform.rotation = Quaternion.Euler(0, 0, 45); //moving down-left
                    break;

                case (-1, 1):
                    transform.rotation = Quaternion.Euler(0, 0, 135); //moving down-right
                    break;
            }
        }

        playerRb.velocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);
    }

}
