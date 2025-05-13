using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    public Animator anim;
    public string Level3 = "Tutorial3.5";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MazeComplete());
        }
    }
    IEnumerator MazeComplete()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Level3);

    }
}
