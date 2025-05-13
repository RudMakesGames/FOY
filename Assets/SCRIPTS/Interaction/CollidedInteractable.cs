using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CollidedInteractable : MonoBehaviour
{
    public bool isInRange = false;
    public UnityEvent interaction;
    public GameObject InteractionCircle;
    public Animator anim;
    private void Update()
    {

    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (isInRange == true)
        {
           if(context.performed)
            {
                anim.SetBool("IsInteracted", true);
                interaction.Invoke();
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            collision.gameObject.GetComponent<PlayerManager>().NotifyPlayer();
            Debug.Log("InRange");
            InteractionCircle.SetActive(true);
         
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<PlayerManager>().DeNotifyPlayer();
            Debug.Log("OutofRange");
            InteractionCircle.SetActive(false);
        }
    }
}
