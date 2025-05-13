using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangManController : MonoBehaviour
{
    [Header("Chest")]
    public GameObject Chest;
    [Header("Key UI prompt")]
    public GameObject keyPrompt;
    [Header("HangMan Puzzle Ref")]
    [SerializeField]
    private GameObject PuzzleRef;
    [Header("Chest Open UI")]
    [SerializeField]
    private GameObject ChestUI_Image;
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject KeyboardContainer;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] HangManStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWord;
    private string word;
    private int incorrectGuesses, correctGuesses;

    // Start is called before the first frame update

    void Start()
    {
        
        InitialiseButtons();
        InitialiseGame();
    }
    private void InitialiseGame()
    {
      if(KeyboardContainer)
        {
            incorrectGuesses = 0;
            correctGuesses = 0;
            foreach (Button child in KeyboardContainer.GetComponentsInChildren<Button>())
            {
                child.interactable = true;
            }
            foreach (Transform child in wordContainer.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
            foreach (GameObject stage in HangManStages)
            {
                stage.SetActive(false);
            }
            word = generateWord().ToUpper();
            foreach (char letter in word)
            {
                var temp = Instantiate(letterContainer, wordContainer.transform);
            }
        }
        
    }
    private string generateWord()
    {
        string[] wordList = possibleWord.text.Split("\n");
        string line = wordList[Random.Range(0, wordList.Length - 1)];
        return line.Substring(0, line.Length - 1);
    }

    // Update is called once per frame
    private void InitialiseButtons()
    {
        for (int i = 65; i <= 90;i ++)
        {
            CreateButton(i);
        }
    }
    private void CreateButton(int i)
    {
       if(KeyboardContainer && letterButton)
        {
            GameObject temp = Instantiate(letterButton, KeyboardContainer.transform);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
            temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter((((char)i).ToString())); });
        }
       
    }
    private void CheckLetter(string InputLetter)
    {
        if(wordContainer)
        {
            bool letterinWord = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (InputLetter == word[i].ToString())
                {
                    letterinWord = true;
                    correctGuesses++;
                    wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = InputLetter;
                }
            }
            if (letterinWord == false)
            {
                incorrectGuesses++;
                HangManStages[incorrectGuesses - 1].SetActive(true);
            }
            CheckOutcome();
        }
      
    }
    private void CheckOutcome()
    {
        if(wordContainer)
        {
            if (correctGuesses == word.Length)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
                }
                StartCoroutine(PuzzleCompletion());




                //Invoke("InitialiseGame", 3f);
            }
            if (incorrectGuesses == HangManStages.Length)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                    // wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
                }
                Invoke("InitialiseGame", 3f);
            }
        }
        
    }
    private IEnumerator PuzzleCompletion()
    {
        yield return new WaitForSeconds(1);
        PuzzleManager.instance.isPuzzleActive = false;
        PuzzleRef.SetActive(false);
        ChestUI_Image.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PuzzleRef)
            {
                PuzzleRef.SetActive(false);
                PuzzleManager.instance.isPuzzleActive = false;
            }
           

        }

    }
    public void CutsceneEnd()
    {
        StartCoroutine(ExitCutscene());
    }
    IEnumerator ExitCutscene()
    {
        ChestUI_Image.SetActive(false);
        PlayerManager.Instance.PickupKey();
        PuzzleManager.instance.isPuzzleActive = false;
        Destroy(Chest);
        if(keyPrompt != null)
        keyPrompt.SetActive(true);
        yield return new WaitForSeconds(3);
        if (keyPrompt != null)
            keyPrompt.SetActive(false);
        TaskManager.instance.TaskComplete();
       
        



    }
}
