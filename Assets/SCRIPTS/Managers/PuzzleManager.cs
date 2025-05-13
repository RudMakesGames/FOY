using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public bool isPuzzleActive;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
