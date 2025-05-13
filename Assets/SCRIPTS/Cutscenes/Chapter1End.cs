using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1End : MonoBehaviour
{
    public string Cutscene = "Chapter1End";
    public Animator anim;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EndLevel());
        }
    }

    // Update is called once per frame
    IEnumerator EndLevel()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
       SceneManager.LoadScene(Cutscene);
    }
}
