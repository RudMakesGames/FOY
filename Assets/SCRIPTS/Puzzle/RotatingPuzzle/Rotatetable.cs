using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotatetable : MonoBehaviour
{
    [SerializeField] private Sprite NormalSprite;
    [SerializeField] private Sprite CorrectSprite;
    [SerializeField] private float CorrectAngle;
    [SerializeField] private float rotationStep = 15f;
    [SerializeField] private float rotationSpeed = 180f; // degrees per second
    [SerializeField] private float inactivityResetTime = 4f;

    public bool IsCorrect = false;
    public bool canRotate = true;

    private float lastRotationTime;
    private Quaternion targetRotation;
    private bool isRotating = false;
    bool AllowedToReset = true;
    private void Start()
    {
        targetRotation = transform.rotation;
    }

    public void RotateObject()
    {
        if (!canRotate || isRotating) return;

        lastRotationTime = Time.time;

        float newZ = transform.localEulerAngles.z + rotationStep;
        targetRotation = Quaternion.Euler(0, 0, newZ % 360);
        StartCoroutine(RotateSmoothly());

        
        StopCoroutine("ResetAfterInactivity");
        StartCoroutine("ResetAfterInactivity");
    }

    private IEnumerator RotateSmoothly()
    {
        isRotating = true;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;

        CheckIfCorrect();
    }

    private IEnumerator ResetAfterInactivity()
    {
        yield return new WaitForSeconds(inactivityResetTime);

        if (Time.time - lastRotationTime >= inactivityResetTime)
        { 
            if(AllowedToReset)
            {
                targetRotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(RotateSmoothly());
                canRotate = true;
                IsCorrect = false;
                GetComponent<Image>().sprite = NormalSprite; // Optionally reset sprite
            }

            
        }
    }

    private void CheckIfCorrect()
    {
        float currentZ = transform.localEulerAngles.z;

        if (Mathf.Abs(Mathf.DeltaAngle(currentZ, CorrectAngle)) < 1f)
        {
            canRotate = false;
            IsCorrect = true;
            Debug.Log("Correct Rotation : " + CorrectAngle);
            GetComponent<Image>().sprite = CorrectSprite;
            AllowedToReset = false;
        }
    }
}
