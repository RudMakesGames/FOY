using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTrap : MonoBehaviour
{
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ChaseSequence chase = Player.GetComponent<ChaseSequence>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SlowPlayer());
        }
    }

    IEnumerator SlowPlayer()
    {
        ChaseSequence chase = Player.GetComponent<ChaseSequence>();
        chase.Chasespeed = 3;
        yield return new WaitForSeconds(0.05f);
        chase.Chasespeed = 4;
    }
}

