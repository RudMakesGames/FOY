using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    Vector2 moveInput, movementMag;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0)
        {
            moveInput = new Vector2(horizontalInput, 0);
        }

        else if (Mathf.Abs(verticalInput) > 0)
        {
            moveInput = new Vector2(0f, verticalInput);
        }

        else
        {
            moveInput = Vector2.zero;
        }

        //moveInput = new Vector2 (horizontalInput, verticalInput);
        movementMag = moveInput * moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos + movementMag * Time.deltaTime;

        rb.MovePosition(newPos);
    }
}
