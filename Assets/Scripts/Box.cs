using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using MathBuds;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    //screen bounds
    [SerializeField] private float horizontalBoundary = 8.4f;
    [SerializeField] private float verticalBoundary = 4.5f;
    
    //flags
    public bool canBeGrabbed;
    public bool rightSnapping;
    public bool leftSnapping;
    public bool playerTouching;
    public bool snapping;

    //references
    public PlayerController playerController;
    private Rigidbody2D blockRb;
    public Rigidbody2D playerRb;
    private float blockWidth;
    private SpriteRenderer blockRend;
    private GameObject leftBlock;
    private GameObject rightBlock;

    //math stuff
    public GameManager gameManager;
    public string value = "";
    public TextMeshProUGUI blockText;
    public Vector3 position;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        blockRb = GetComponent<Rigidbody2D>();
        blockRend = GetComponent<SpriteRenderer>();
        blockWidth = blockRend.bounds.size.x;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        switch(this.gameObject.name)
        {
            case "Block":
                value = gameManager.a.ToString();
                position = gameManager.spawnPositions[gameManager.randomArray[0]];
                break;

            case "Block (1)":
                value = gameManager.b.ToString();
                position = gameManager.spawnPositions[gameManager.randomArray[1]];
                break;

            case "Block (2)":
                value = gameManager.c.ToString();
                position = gameManager.spawnPositions[gameManager.randomArray[2]];
                break;

            case "Block (3)":
                value = gameManager.d;
                position = gameManager.spawnPositions[gameManager.randomArray[3]];
                break;

            case "Block (4)":
                value = gameManager.e;
                position = gameManager.spawnPositions[gameManager.randomArray[4]];
                break;
        }

        blockText.text = value;
        this.gameObject.transform.position = position;

        
    }

    // Update is called once per frame
    void Update()
    {
        //keep block in bounds
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
        
        //player grabbing block
        if (playerController.isGrabbing && playerController.currentBlock == this.gameObject)
        {
            blockRb.drag = 0;
            blockRb.angularDrag = 0.05f;
            blockRb.velocity = playerRb.velocity;
        }
        else
        {
            blockRb.drag = 20;
            blockRb.angularDrag = 1;
            blockRb.velocity = new Vector3(0, 0, 0);
        }

        if(!playerController.isGrabbing && !playerTouching)
        {            
            if (rightSnapping)
            {
                transform.position = new Vector3(rightBlock.transform.position.x - blockWidth/1.8f, rightBlock.transform.position.y, 0);
            }
            else if (leftSnapping)
            {
                transform.position = new Vector3(leftBlock.transform.position.x + blockWidth/1.8f, leftBlock.transform.position.y, 0);
            }


        }

        if (leftSnapping || rightSnapping)
        {
            snapping = true;
        }
        else
        {
            snapping = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            leftSnapping = false;
            rightSnapping = false;
            transform.position = position;

            //if (gameManager.isCorrect)
/*            {
                Debug.Log("Regenerating");
                gameManager.equationInts = ExpandedMath.generation(1);
                gameManager.a = gameManager.equationInts[0];
                gameManager.b = gameManager.equationInts[2];
                gameManager.c = ExpandedMath.findSolution(gameManager.equationInts);
                switch (gameManager.equationInts[1])
                {
                    case 1:
                        gameManager.d = "*";
                        break;

                    case 2:
                        gameManager.d = "/";
                        break;

                    case 3:
                        gameManager.d = "+";
                        break;

                    case 4:
                        gameManager.d = "-";
                        break;
                }

                switch (this.gameObject.name)
                {
                    case "Block":
                        value = gameManager.a.ToString();
                        position = gameManager.spawnPositions[gameManager.randomArray[0]];
                        break;

                    case "Block (1)":
                        value = gameManager.b.ToString();
                        position = gameManager.spawnPositions[gameManager.randomArray[1]];
                        break;

                    case "Block (2)":
                        value = gameManager.c.ToString();
                        position = gameManager.spawnPositions[gameManager.randomArray[2]];
                        break;

                    case "Block (3)":
                        value = gameManager.d;
                        position = gameManager.spawnPositions[gameManager.randomArray[3]];
                        break;

                    case "Block (4)":
                        value = gameManager.e;
                        position = gameManager.spawnPositions[gameManager.randomArray[4]];
                        break;
                }

           }*/

            blockText.text = value;
            this.gameObject.transform.position = position;

            gameManager.isCorrect = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeGrabbed = true;
            playerController.canGrab = true;
            playerController.currentBlock = this.gameObject;
        }

        if (other.CompareTag("BlockDetector"))
        {
            if (other.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                leftSnapping= true;
                leftBlock = other.gameObject;
            }

            if (other.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                rightSnapping = true;
                rightBlock = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeGrabbed = false;
            playerController.canGrab = false;
            playerController.currentBlock = null;
        }

        if (other.CompareTag("BlockDetector"))
        {
            if (other.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                leftSnapping = false;
                leftBlock = null;
            }

            if (other.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                rightSnapping = false;
                rightBlock = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerTouching = false;
        }
    }
}