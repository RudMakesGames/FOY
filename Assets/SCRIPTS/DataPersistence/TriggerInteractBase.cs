using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TriggerInteractBase : MonoBehaviour, IInteractable
{
    public GameObject player { get; set; }
    public bool CanInteract { get; set; }

    public virtual void Interact() { }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(CanInteract)
            {
                
                Interact();
                Debug.Log("Interacted!");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CanInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanInteract = false;
        }
    }
}
