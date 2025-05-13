using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    public string ContinueSceneName = "Continue";
    public string MenuSceneName = "Main Menu";
    public static Persistence Instance;
    private void Awake()
    {
     DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Register to scene load events
            SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is one where we want to stop persistence
        if (scene.name == ContinueSceneName || scene.name == MenuSceneName)
        {
            // Destroy the persistent object
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unregister from the scene loaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
