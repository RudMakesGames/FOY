using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelLoad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneController.instance.NextLev();
        }
    }
}
