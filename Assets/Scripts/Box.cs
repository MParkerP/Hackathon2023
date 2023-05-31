using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using System;
using TMPro;

public class Box : MonoBehaviour
{
    public bool canBeGrabbed;
    public bool isGrabbed;
    public bool isNearBlock;
    public GameObject player;
    public PlayerController playerController;
    public Rigidbody2D boxRb;

    public GameObject leftBlock;
    public GameObject rightBlock;
    public RelativeJoint2D rightJoint;

    public float blockWidth;
    public SpriteRenderer blockRenderer;

    public bool hasBlockAttached;
    public List<GameObject> attachedObjects;

    public TextMeshProUGUI visibleText;
    public string blockText;
    public bool holdingNumber;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        boxRb = GetComponent<Rigidbody2D>();
        blockRenderer= GetComponent<SpriteRenderer>();
        blockWidth = blockRenderer.bounds.size.x;
        setBlockText(blockText);

    }

    private void Update()
    {
        //if player is grabbing this block, create connection between player and the block
        if (isGrabbed)
        {
            if (player.GetComponent<FixedJoint2D>() == null) 
            {
                FixedJoint2D playerJoint = player.AddComponent<FixedJoint2D>();
                playerJoint.enableCollision = true; //IMPORTANT
                playerJoint.connectedBody = boxRb;
            }

            //if attached to other block, remove joint from this to right block and from left to this block
            if (hasBlockAttached)
            {
                detachBlock(this.gameObject, leftBlock);
            }
        }


        //check if block was detached but not removed from nearby blocks
        if (isNearBlock && !hasBlockAttached && !playerController.isGrabbing)
        {
            if (rightBlock != null)
            {
                attachBlock(rightBlock, rightJoint);
            }
            
            if (leftBlock != null)
            {
                Box leftBlockScript = leftBlock.GetComponent<Box>();
                leftBlockScript.attachBlock(leftBlockScript.rightBlock, leftBlockScript.rightJoint);
            }
        }

        //set block as having nothing attached if side blocks are both null
        if (rightBlock == null && leftBlock == null)
        {
            hasBlockAttached = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player hand touching block
        if (collision.CompareTag("Hand"))
        {
            canBeGrabbed = true;
            playerController.canGrab = true;
            playerController.currentBlock = this.gameObject;
        }

        //block enters left detector of other block
        if (collision.CompareTag("LeftDetector"))
        {
            isNearBlock = true;
            rightBlock = collision.gameObject.transform.parent.gameObject;
            attachBlock(rightBlock, rightJoint);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //player hands leave box
        if (collision.CompareTag("Hand"))
        {
            canBeGrabbed = false;
            playerController.canGrab = false;
            playerController.currentBlock = null;
        }

        if (collision.CompareTag("LeftDetector"))
        {
            isNearBlock = false;
            rightBlock.GetComponent<Box>().leftBlock= null;
            rightBlock = null;

        }
    }
    

    //METHODS//

    //return the middle y position betweeen two blocks
    private float getMiddleY(GameObject object1, GameObject object2)
    {
        return (object1.transform.position.y + object2.transform.position.y) / 2;
    }

    //attach block together by creating joints and changing positions
    private void attachBlock(GameObject block, RelativeJoint2D joint)
    {
        Box blockScript = block.GetComponent<Box>();

        //create a joint for the block
        joint = this.gameObject.AddComponent<RelativeJoint2D>();

        //make the joint attach to the block
        joint.connectedBody = block.GetComponent<Rigidbody2D>();
        joint.enableCollision = true;
        joint.autoConfigureOffset = false;
        joint.linearOffset = new Vector2(blockWidth, 0);

        //flag this block as attached to another block
        hasBlockAttached = true;

        //update the other blocks information as well
        blockScript.hasBlockAttached = true;
        blockScript.leftBlock = this.gameObject;
        blockScript.isNearBlock = true;

        //make sure player no longer holding onto block
        playerController.LetGo();
    }

    //break joint from the block to the next, and the block before that is grabbing it
    private void detachBlock(GameObject block1, GameObject block2)
    {
        UnityEngine.Object.Destroy(block1.GetComponent<RelativeJoint2D>());

        if (block2 != null)
        {
            UnityEngine.Object.Destroy(block2.GetComponent<RelativeJoint2D>());
        }

        hasBlockAttached= false;
    }

    //set the visible text of the block in the scene
    private void setBlockText(string text)
    {
        char character = (char)text[0];
        visibleText.text = blockText;

        //if the symbol is not a number make the font bigger and move it up for readability
        if(!char.IsNumber(character))
        {
            visibleText.fontSize = 1.25f;
            visibleText.transform.position += new Vector3(0,0.175f);
        }
    }




}
