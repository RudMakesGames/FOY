using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVanish : MonoBehaviour
{
   
    public Animator anim;
    [Header("Item dropped by the boss")]
    public GameObject Fragment;
    public Transform Fp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChapterEnd());
        }
    }
    IEnumerator ChapterEnd()
    {
        Instantiate(Fragment,Fp.position,Quaternion.identity);
        anim.SetTrigger("Vanish");
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
       

    }
}
