using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    [Header("Task UI")]
    public GameObject TaskUI;
    public TextMeshProUGUI TaskText;

    public string[] Tasks; // Array of task strings
    private int currentTaskIndex = 0; // Index of the current task
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        // Initialize Task UI

    }
    public void UpdateTask()
    {
        if (currentTaskIndex < Tasks.Length - 1)
        {
            TaskUI.SetActive(true);
            currentTaskIndex++;
            UpdateTaskUI();
        }
        else
        {
            // Optionally, you can hide or deactivate the Task UI when done
            TaskUI.SetActive(false);
        }
    }

    public void UpdateTaskUI()
    {
        if (TaskText != null)
        {
            TaskText.text = Tasks[currentTaskIndex];
            TaskUI.SetActive(true); // Ensure the Task UI is active
        }
    }

    public void TaskComplete()
    {
        TaskUI.SetActive(false);
    }


}
