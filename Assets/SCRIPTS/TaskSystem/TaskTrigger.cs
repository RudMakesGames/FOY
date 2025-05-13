using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TaskTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player (tag or name check)
        if (other.CompareTag("Player"))
        {
            TaskManager.instance.UpdateTask();
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
