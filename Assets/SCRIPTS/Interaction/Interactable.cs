using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Inventory;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;



public class Interactable : MonoBehaviour
{
    public GameObject InteractionCircle;
    public Animator anim;
    public bool isInRange;
    public UnityEvent interaction;

    
    private void Update()
    {
       
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interacted");
            
            if(isInRange)
            {
                anim.SetBool("IsInteracted", true);
                interaction.Invoke();
               
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           
            isInRange = true;
            Debug.Log("InRange");
            PlayerManager.Instance.NotifyPlayer();
            InteractionCircle.SetActive(true);
            
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isInRange = false;
            Debug.Log("OutofRange");
            PlayerManager.Instance.DeNotifyPlayer();
            InteractionCircle.SetActive(true);
            anim.SetBool("IsInteracted", false);


        }
    }

}
