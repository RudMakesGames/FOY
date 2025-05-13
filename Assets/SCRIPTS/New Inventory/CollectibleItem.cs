using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string clueOrFragmentText; // Text to be added as a clue or fragment
    public Sprite image; // Image to be added with the clue or fragment
    public bool isClue; // Determines if the item is a clue
    public bool isFragment; // Determines if the item is a fragment

    private NotebookManager notebookManager;

    void Start()
    {
        // Find the NotebookManager in the scene
        notebookManager = FindObjectOfType<NotebookManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notebookManager != null)
        {
            if (isFragment)
            {
                notebookManager.AddFragment(clueOrFragmentText, image);
            }
            else if (isClue)
            {
                notebookManager.AddClue(clueOrFragmentText, image);
            }

            // Optionally destroy the item after interaction
            Destroy(gameObject);
        }
    }
    
        // Add note, clue, or fragment to the notebook
       
    
}
