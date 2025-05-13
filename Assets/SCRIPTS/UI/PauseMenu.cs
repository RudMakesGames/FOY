using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;     
using Inventory;

public class PauseMenu : MonoBehaviour
{
    public string MenuScene = "Main Menu";
    public GameObject PauseMenuUI;
    
    public void Resume()
    {
        if(Move.GameIsPaused == true)
        {
            Resum();
            
        }
       
    }
   
    void Resum()
    {
        PauseMenuUI.SetActive(false);
        Move.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
