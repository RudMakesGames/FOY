using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator anim;
    public string SceneName;
    public static SceneController instance;

    private void Start()
    {
        anim.SetTrigger("Start");
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
         
    }
    // Start is called before the first frame update
    public void NextLev()
    {
        StartCoroutine(LoadLev());
    }
    IEnumerator LoadLev()
    {
        anim.SetTrigger("End");
        Move.DisableActions();
        yield return new WaitForSeconds(0.8F);
        SceneManager.LoadScene(SceneName);
        anim.SetTrigger("Start");
        Move.EnableActions();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
