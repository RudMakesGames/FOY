using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("Colliders ref")]
    [SerializeField]
    private Collider2D[] collider2Ds;
    private bool IsCollidersOn = false;

    [Header("Access Stage Prompt(UI Text)")]
    [SerializeField]
    private GameObject StageAccessPrompt;


    public void EnableStageColliders()
    {
        if (!IsCollidersOn)
        {
            foreach (var collider2d in collider2Ds)
            {
                collider2d.enabled = true;
            }
            IsCollidersOn = true;
        }
        else 
        {
            foreach (var collider2d in collider2Ds)
            {
                collider2d.enabled = false;
            }
            IsCollidersOn = false;
        }

      

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StageAccessPrompt!= null)
        {
            StageAccessPrompt.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (StageAccessPrompt != null)
        {
            StageAccessPrompt.SetActive(false);
        }
           
    }

}
