using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; 
    public float Movespeed = 5f;
    public bool isChasing = true;
    private void Update()
    {if(isChasing)
        {
            if (transform.position.x > player.position.x)
            {
                transform.position += Vector3.left * Movespeed * Time.deltaTime;
            }
            if (transform.position.x < player.position.x)
            {
                transform.position += Vector3.right * Movespeed * Time.deltaTime;
            }
        }
       

    }
    public void SlowDown()
    {
       StartCoroutine(SlowEntityDown());
    }
    public void SpeedUp()
    {
        StartCoroutine (SpeedEntityUp());
    }

    IEnumerator SlowEntityDown()
    {
        Movespeed = 5f;
        yield return new WaitForSecondsRealtime(1);
        Movespeed = 5.5f;

    }
    IEnumerator SpeedEntityUp()
    {
        Movespeed = 7.5f;
        yield return new WaitForSecondsRealtime(2.5f);
        Movespeed = 5.5f;
    }
}
