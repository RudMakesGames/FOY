using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public string Menu = "Main Menu";
    public float Timer = 10.15f;
    void Start()
    {
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Intro()
    {
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene(Menu);
    }
}
