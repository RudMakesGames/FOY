using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusicController : MonoBehaviour
{
    public GameObject Bgsound;
    public GameObject BossMusic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Bgsound.SetActive(false);
            BossMusic.SetActive(true);
        }
    }
}
