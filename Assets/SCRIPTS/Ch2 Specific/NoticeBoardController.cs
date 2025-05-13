using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class NoticeBoardController : MonoBehaviour
{
    [Header("Puzzle reference")]
    [SerializeField]
    private GameObject NoticeBoardUI;


    public void NoticeBoard()
    {
        
        
            NoticeBoardUI.SetActive(true);
            PuzzleManager.instance.isPuzzleActive = true;
        
   
    }
    public void CloseNoticeBoard()
    {
        NoticeBoardUI.SetActive(false);
        PuzzleManager.instance.isPuzzleActive = false;
    }
    
}
