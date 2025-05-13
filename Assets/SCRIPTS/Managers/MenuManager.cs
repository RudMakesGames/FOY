using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int CurrentSceneIndex;
    private int SceneToContinue;
    [Header("The scene it Goes back to")]
    public string mainmenuscenename = "Continue";
    [Header("The level loaded after new game is selected")]
    public string FirstLev = "Tutorial";
    public string Chapter2 = "School";
   
    
    public void PlayGame()
    {
        SceneManager.LoadScene(FirstLev);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Time.timeScale = 1.0f;
        Application.Quit();

    }
    public void BacktoMenu()
    {
        
        Time.timeScale = 1.0f;
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", CurrentSceneIndex);
        SceneManager.LoadScene(mainmenuscenename);

    }
    public void OpenGame()
    {
        Invoke("PlayGame", 0.5f);
    }
    public void CloseGame()
    {
        Invoke("QuitGame", 0.5f);
    }
    public void Continue()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", CurrentSceneIndex);
        SceneManager.LoadScene("SavedScene");
    }
    public void ContinueButton()
    {
        Invoke("ContinueGame", 0.5f);
    }
    public void ContinueGame()
    {
        SceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if(SceneToContinue!= 1)
        {
            if(SceneToContinue > 15 && SceneToContinue < 21)
            {
                SceneManager.LoadScene(Chapter2);
            }
            else
            {
                SceneManager.LoadScene(SceneToContinue);
            }
           

           
           
        }
        else
        {
            
            return;
        }
            
    }
    

}
