using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public string Level = "Tutorial";
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PlayGame()); 
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += Time.deltaTime;
        if(slider.value >= 2 )
        {
            SceneManager.LoadScene(Level);
        }
    }
    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(1);
        slider.value = 0.25f;
        yield return new WaitForSeconds(1);
        slider.value = 0.5f;
        yield return new WaitForSeconds(1);
        slider.value = 0.75f;
        yield return new WaitForSeconds(1);
        slider.value = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Level);
    }
}
