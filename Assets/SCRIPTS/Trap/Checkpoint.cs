using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public BoxCollider2D trigger;

    [SerializeField]
    private Trap trap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            trap.respawnPoint = transform;
            trigger.enabled = false;
        } 
    }
}
