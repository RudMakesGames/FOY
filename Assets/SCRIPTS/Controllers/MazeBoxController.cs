using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeBoxController : MonoBehaviour
{
    public Animator anim;
    public string MazeLevel = "Maze";
    public void MazePuzzle()
    {
        
       StartCoroutine(Maze());
    }
    IEnumerator Maze()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(MazeLevel);
    }
}
