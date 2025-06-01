using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPuzzle : MonoBehaviour, IPuzzle
{
    public bool PuzzleCompleted;
    [Header("The Puzzle UI ref")]
    [SerializeField]
    private Rotatetable[] Rotatables;
    
    public Response response;

    void CheckForCorrectSequence()
    {
        foreach (var r in Rotatables)
        {
            if(r.IsCorrect)
            {
                OnPuzzleComplete();
            }
        }
    }
    private void Update()
    {
        if(!PuzzleCompleted)
        {
            CheckForCorrectSequence();
        }
    }
    public void ClosePuzzle()
    {
        gameObject.SetActive(false);
    }

    public void OnPuzzleComplete()
    {
        
        Debug.Log("Completed Puzzle!");
        response?.OnPuzzleFinish();
        PuzzleCompleted = true;
    }

    public void OpenPuzzle()
    {
       gameObject.SetActive(true);
    }
}
