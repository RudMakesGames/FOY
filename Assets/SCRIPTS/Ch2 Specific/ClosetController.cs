using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClosetController : MonoBehaviour
{
    [Header("Closet UI")]
    public GameObject ClosetUI;

    [Header("Text for when Player didnt solve NoticeBoard Puzzle")]
    public GameObject CluePrompt;

   


    public void OpenCloset()
    {
        if(PlayerManager.Instance.ToyClueCount>0)
        {
            StartCoroutine(ShowCluePrompt());
        }
        else
        {
            ClosetUI.SetActive(true);
            PuzzleManager.instance.isPuzzleActive = true;
        }
       
    }
    public void CloseCloset()
    {
        ClosetUI.SetActive(false);
        PuzzleManager.instance.isPuzzleActive = true;
    }
    IEnumerator ShowCluePrompt()
    {
        CluePrompt.SetActive(true);
        yield return new WaitForSeconds(3);
        CluePrompt.SetActive(false);
    }
}
