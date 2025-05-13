using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChestController : MonoBehaviour
{
    [Header("Puzzle Ref")]
    public GameObject ChestPuzzle;
    

    // Update is called once per frame
    public void OpenChest()
    {
        ChestPuzzle.SetActive(true);
        PuzzleManager.instance.isPuzzleActive = true;
    }
    void Update()
    {
        
    }
}
