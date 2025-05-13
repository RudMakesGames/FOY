using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBearController : MonoBehaviour
{
    [SerializeField] private string collisionTag; 
    [SerializeField] private Transform DesiredLocation;   

    private Vector3 offset;
    private float zCoordinate;
    public bool isDraggable = true;  

    public static int AttachmentCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
           
            transform.position = DesiredLocation.position;
            isDraggable = false;
            AttachmentCount++;
        }
    }

    private void OnMouseDown()
    {
        if (isDraggable)
        {
            // Store the z-coordinate of the object
            zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Calculate the offset between the mouse position and the object position
            offset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        if (isDraggable)
        {
            // Move the object to the mouse position
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in the world
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
