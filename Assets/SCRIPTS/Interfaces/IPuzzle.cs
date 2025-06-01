using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle 
{
    void OpenPuzzle();
    void ClosePuzzle();

    void OnPuzzleComplete();
}
