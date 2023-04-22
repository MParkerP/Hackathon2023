using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private float horizontalBoundary = 8.4f;
    [SerializeField] private float verticalBoundary = 4.5f;

    private Rigidbody2D playerRb;
    public float speed;

    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        playerRb.velocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);
    }
}
