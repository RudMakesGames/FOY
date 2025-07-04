using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseVanish : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Entity"))
        {
            anim.SetTrigger("Vanish");
        }
    }
}
