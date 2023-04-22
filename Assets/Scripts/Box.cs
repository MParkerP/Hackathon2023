using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //screen bounds
    [SerializeField] private float horizontalBoundary = 8.4f;
    [SerializeField] private float verticalBoundary = 4.5f;
    
    //flags
    public bool canBeGrabbed;

    //references
    private PlayerController playerController;
    private Rigidbody2D blockRb;
    private Rigidbody2D playerRb;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        blockRb = GetComponent<Rigidbody2D>();
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
            playerController.speed = 3;
            blockRb.drag = 0;
            blockRb.angularDrag = 0.05f;
            blockRb.velocity = playerRb.velocity;
        }
        else
        {
            playerController.speed = 5;
            blockRb.drag = 20;
            blockRb.angularDrag = 1;
            blockRb.velocity = new Vector3(0, 0, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeGrabbed = true;
            playerController.canGrab = true;
            playerController.currentBlock = this.gameObject;
            Debug.Log("can grab");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            canBeGrabbed = false;
            playerController.canGrab = false;
            playerController.currentBlock = null;
            Debug.Log("can not grab");
        }
    }
    
}
