using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isometricMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpDuration;
    Rigidbody2D rb;

    Vector2 moveInput, movementMag;

    public bool isJumping, canJump;
    [SerializeField]
    GameObject shadow;

    Collider2D playerCollider;

    Vector3 originalScale;

    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        isJumping = false;
        originalScale = transform.localScale;
        shadow.SetActive(false);

        canJump = true;

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /*if (Mathf.Abs(horizontalInput) > 0)
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
        }*/

        moveInput = new Vector2 (horizontalInput, verticalInput).normalized;
        movementMag = moveInput * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    void Jump()
    {
        isJumping = true;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos+movementMag*Time.deltaTime;

        rb.MovePosition(newPos);

        if (isJumping && canJump)
        {
            StartCoroutine(jumpCoroutine());
        }
    }


    IEnumerator jumpCoroutine()
    {
        isJumping = true;
        canJump = false;
        shadow.SetActive(true);
        playerCollider.enabled = false;

        float elapsedTime = 0f;
        Vector3 startScale = originalScale;
        Vector3 jumpScale = originalScale * 1.2f; //isko change krna for making the affect amplified

        while (elapsedTime < jumpDuration / 2) // first half increase 
        {
            float progress = elapsedTime / (jumpDuration / 2);
            float curveValue = jumpCurve.Evaluate(progress);

            transform.localScale = Vector3.Lerp(startScale, jumpScale, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2) //second half mai decrease krrha
        {
            float progress = elapsedTime / (jumpDuration / 2);
            float curveValue = jumpCurve.Evaluate(1 - progress);

            transform.localScale = Vector3.Lerp(startScale, jumpScale, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;

        playerCollider.enabled = true;
        shadow.SetActive(false);
        isJumping = false;
        canJump = true;
    }
}
