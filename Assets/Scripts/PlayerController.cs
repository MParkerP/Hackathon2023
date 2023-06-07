using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //player movement
    private float verticalInput;
    private float horizontalInput;
    public Rigidbody2D playerRb;
    public float speed = 5;

    //edge collider
    private EdgeCollider2D screenEdge;

    //grabbing
    public bool isGrabbing;
    public GameObject currentBlock;
    public GameObject previousBlock;
    public bool canGrab;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        ScreenBounds();
    }

    //player non-movement input
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentBlock!= null)
            {
                isGrabbing = true;
                previousBlock = currentBlock;
                currentBlock.GetComponent<Box>().isGrabbed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            LetGo();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.instance.setEquationFromBlocks();
            GameManager.instance.checkCorrect();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.ResetAllBlocks();
            LetGo();
        }
    }

    //player movement
    private void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //set sprite rotation according to player movement
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
    

    //METHODS//


    //convert viewport corners into worldspace to get list of screen corner points
    private List<Vector2> getScreenCorners()
    {
        Camera mainCam = Camera.main;
        List<Vector2> points = new List<Vector2>();
        points.Add(mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)));
        points.Add(mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)));
        points.Add(mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0)));
        points.Add(mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0)));
        points.Add(mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)));

        return points;
    }

    //set camera edge collider to use screen corners as vertices
    private void ScreenBounds()
    {
        screenEdge = Camera.main.GetComponent<EdgeCollider2D>();
        screenEdge.SetPoints(getScreenCorners());
    }

    //make player let go of block
    public void LetGo()
    {
        if (previousBlock!=null)
        {
            previousBlock.GetComponent<Box>().isGrabbed = false;
        }
        isGrabbing = false;
        Object.Destroy(GetComponent<FixedJoint2D>());
    }

}
