using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadlockManager : MonoBehaviour,IPuzzle
{
    public bool PuzzleCompleted;
    [Header("The Puzzle UI ref")]
    public GameObject PuzzlePrefab;
    public TextMeshProUGUI[] Textnumber;
    public string desiredNo;
    public int[] number;

    [Header("Ref for the object which carries the puzzle")]
    public GameObject Briefcase;
    [Header("Ref for the open object UI")]
    public GameObject BriefcaseCutscene;
    [Header("Text for confirming the puzzle was completed")]
    public GameObject keyPrompt;
    [Header("Case specific iten which needs to be enabled after the Puzzle is complete")]
    public GameObject SetActiveObject;
    public AudioClip ChangeNumber;
    public AudioClip CaseOpen;
    public GameObject PadlockHintTrigger;

    [Header("Optional")]
    public GameObject Task3;
    public void ChangeNo(int index)
    {
        number[index]++;
        AudioManager.instance.PlaySoundFXClip(ChangeNumber, transform, 1, 1);
        if (number[index] > 9)
        {
            number[index] = 0;
        }
        Textnumber[index].text = number[index].ToString();
        CorrectGuess();
    }
    public void MinusChangeNo(int index)
    {
        number[index]--;
        AudioManager.instance.PlaySoundFXClip(ChangeNumber, transform, 1, 1);
        if (number[index] < 0)
        {
            number[index] = 9;
        }
        Textnumber[index].text = number[index].ToString();
        CorrectGuess();
    }
    private void CorrectGuess()
    {
        for (int i = 0; i < desiredNo.Length; i++)
        {
            if (number[i].ToString() != desiredNo[i].ToString())
            {
                Debug.Log("Incorrect " + i);
                return;
            }
        }

        OnPuzzleComplete();

    }
    IEnumerator GetKey()
    {
        if(PuzzleCompleted)
        {
            yield return new WaitForSeconds(1);
            Destroy(PuzzlePrefab);
            
            PlayerManager.Instance.PickupKey();
            Debug.Log("Key pickedup");
            
            PuzzleCompleted = false;
            
            BriefcaseCutscene.SetActive(true);
            yield return new WaitForSeconds(3);
            
            
           
           OnPuzzleComplete();

        }
    }
    public void CutsceneEnd()
    {
        StartCoroutine(ExitCutscene());
    }
    IEnumerator ExitCutscene()
    {
        BriefcaseCutscene.SetActive(false );
        PuzzleManager.instance.isPuzzleActive = false;
        Destroy(Briefcase);
        keyPrompt.SetActive(true);
        yield return new WaitForSeconds(3);
        keyPrompt.SetActive(false);
        SetActiveObject.SetActive(true );
        PadlockHintTrigger.SetActive(false);
        
        

    }

    private void Update()
    {if(PuzzleCompleted)

        {
            StartCoroutine(GetKey());
        }
    
    }

    public void OpenPuzzle()
    {
       gameObject.SetActive(true);
    }

    public void ClosePuzzle()
    {
        gameObject.SetActive(false);
    }

    public void OnPuzzleComplete()
    {
        Debug.Log("PuzzleCorrected");
        PuzzleCompleted = true;
        AudioManager.instance.PlaySoundFXClip(CaseOpen, transform, 1, 1);
        TaskManager.instance.TaskComplete();
        Task3?.SetActive(true); 
    }
}