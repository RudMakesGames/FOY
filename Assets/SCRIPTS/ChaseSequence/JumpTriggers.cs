using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggers : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Entity"))
        {
            anim.SetBool("EntityJumping", true);
           

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Entity"))
        {
            anim.SetBool("EntityJumping", false);
           
           
        }
    }

    void Update()
    {
        
    }
}
