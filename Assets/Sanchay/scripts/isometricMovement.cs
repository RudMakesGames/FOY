using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isometricMovement : MonoBehaviour
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

        moveInput = new Vector2 (horizontalInput, verticalInput);
        movementMag = moveInput * moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos+movementMag*Time.deltaTime;

        rb.MovePosition(newPos);
    }
}
