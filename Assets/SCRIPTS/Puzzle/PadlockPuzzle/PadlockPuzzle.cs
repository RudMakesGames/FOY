using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadlockPuzzle : MonoBehaviour
{
    public bool PuzzleCompleted;
    [Header("The Puzzle UI ref")]
    public TextMeshProUGUI[] Textnumber;
    public string desiredNo;
    public int[] number;

  
    public AudioClip ChangeNumber;
    public AudioClip CaseOpen;
    

    [Header("Optional")]
    public GameObject Task3;

    [SerializeField]
    private Response response;
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
        TaskManager.instance?.TaskComplete();
        Task3?.SetActive(true);
        response.OnPuzzleFinish();
    }
    
}
