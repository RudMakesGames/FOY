using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField]
    GameObject Puzzle;
    public bool PuzzleCompleted;
    [Header("The Puzzle UI ref")]
    [SerializeField]
    private Rotatetable[] Rotatables;

    
    public Response response;

    void CheckForCorrectSequence()
    {
        foreach (var r in Rotatables)
        {
            if (!r.IsCorrect)
            {
                return; // Exit early if any is incorrect
            }
        }

        // All rotatables are correct
        OnPuzzleComplete();
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
       Puzzle.SetActive(false);
    }

    public void OnPuzzleComplete()
    {
       
        Debug.Log("Completed Puzzle!");
        response?.OnPuzzleFinish();
        PuzzleCompleted = true;
        foreach (var r in Rotatables)
        {
            r.AllowedToReset = false;
        }
        ClosePuzzle();
    }

    public void OpenPuzzle()
    {
      Puzzle.SetActive(true);
    }
}
