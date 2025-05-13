using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed = 3f;
    public Rigidbody2D rb;
    void Start()
    {
        
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
