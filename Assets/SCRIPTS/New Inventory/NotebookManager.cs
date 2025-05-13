using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    public GameObject notebookPanel;
    public TextMeshProUGUI notesText; // Text for clues and fragments
    public GameObject NoteBook;
    public Image displayImage; // Image to show with text
    public Button closeButton;
    public Button cluesButton; // Button for Clues
    public Button fragmentsButton; // Button for Fragments
    public Button nextPageButton;
    public Button prevPageButton;
    public TextMeshProUGUI pageNumberText;
    public Color OriginalColor;
    public Color TransitionColor;
    private bool isNotebookOpen = false;
    private List<NotebookPage> clues = new List<NotebookPage>();
    private List<NotebookPage> fragments = new List<NotebookPage>();

    private int currentPage = 0;
    private int itemsPerPage = 1; // Show one item per page

    public AudioClip Open;
    public AudioClip Close;

    private enum NotebookMode
    {
        Clues,
        Fragments
    }

    private NotebookMode currentMode = NotebookMode.Clues;

    void Start()
    {
        notebookPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseNotebook);
        cluesButton.onClick.AddListener(ToggleModeToClues);
        fragmentsButton.onClick.AddListener(ToggleModeToFragments);
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);
        UpdatePageNumber();
        DisplayCurrentModeContent(); // Initialize the display
        
    }

    public void OpenNotebook(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ToggleNotebook();
        }
    }

    public void ToggleNotebook()
    {
        isNotebookOpen = !isNotebookOpen;        
        notebookPanel.SetActive(isNotebookOpen);
        if (isNotebookOpen)
        {
            AudioManager.instance.PlaySoundFXClip(Open, transform, 1, 1);
            DisplayCurrentModeContent();
        }
        else
        {
            AudioManager.instance.PlaySoundFXClip(Close, transform, 1, 1);
        }
    }

    private void CloseNotebook()
    {
        isNotebookOpen = false;
        notebookPanel.SetActive(false);
    }

    private void ToggleModeToClues()
    {
        currentMode = NotebookMode.Clues;
        currentPage = 0; // Reset to the first page
        DisplayClues();
    }

    private void ToggleModeToFragments()
    {
        currentMode = NotebookMode.Fragments;
        currentPage = 0; // Reset to the first page
        DisplayFragments();
    }

    private void DisplayCurrentModeContent()
    {
        switch (currentMode)
        {
            case NotebookMode.Clues:
                DisplayClues();
                break;
            case NotebookMode.Fragments:
                DisplayFragments();
                break;
        }
    }

    private void DisplayClues()
    {
        if (clues.Count > 0)
        {
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Mathf.Min(startIndex + itemsPerPage, clues.Count);
            notesText.text = $"Clue {currentPage + 1}:\n";
            displayImage.gameObject.SetActive(clues[startIndex].image != null);
            notesText.text += clues[startIndex].text + "\n";
            displayImage.sprite = clues[startIndex].image;

            nextPageButton.gameObject.SetActive(currentPage < (clues.Count - 1) / itemsPerPage);
            prevPageButton.gameObject.SetActive(currentPage > 0);
            UpdatePageNumber();
            
        }
        else
        {
            notesText.text = "No Clues Available";
            displayImage.gameObject.SetActive(false);
            nextPageButton.gameObject.SetActive(false);
            prevPageButton.gameObject.SetActive(false);
            pageNumberText.text = "";
            
        }
    }

    private void DisplayFragments()
    {
        if (fragments.Count > 0)
        {
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Mathf.Min(startIndex + itemsPerPage, fragments.Count);
            notesText.text = $"Fragment {currentPage + 1}:\n";
            displayImage.gameObject.SetActive(fragments[startIndex].image != null);
            notesText.text += fragments[startIndex].text + "\n";
            displayImage.sprite = fragments[startIndex].image;

            nextPageButton.gameObject.SetActive(currentPage < (fragments.Count - 1) / itemsPerPage);
            prevPageButton.gameObject.SetActive(currentPage > 0);
            UpdatePageNumber();
            
        }
        else
        {
            notesText.text = "No Fragments Available";
            displayImage.gameObject.SetActive(false);
            nextPageButton.gameObject.SetActive(false);
            prevPageButton.gameObject.SetActive(false);
            pageNumberText.text = "";
            
        }
    }

    private void NextPage()
    {
        if (currentMode == NotebookMode.Clues && currentPage < (clues.Count - 1) / itemsPerPage)
        {
            currentPage++;
            DisplayClues();
            
        }
        else if (currentMode == NotebookMode.Fragments && currentPage < (fragments.Count - 1) / itemsPerPage)
        {
            currentPage++;
            DisplayFragments();
            
        }
        StartCoroutine(PageTransition());
    }

    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayCurrentModeContent();
            
        }
        StartCoroutine(PageTransition());
    }

    private void UpdatePageNumber()
    {
        pageNumberText.text = $"Page {currentPage + 1}";
    }

    public void AddClue(string clue, Sprite image)
    {
        clues.Add(new NotebookPage { text = clue, image = image });
        if (currentMode == NotebookMode.Clues)
        {
            DisplayClues();
        }
    }

    public void AddFragment(string fragment, Sprite image)
    {
        fragments.Add(new NotebookPage { text = fragment, image = image });
        if (currentMode == NotebookMode.Fragments)
        {
            DisplayFragments();
        }
    }
    IEnumerator PageTransition()
    {
        
        NoteBook.GetComponent<Image>().color = TransitionColor;
        yield return new WaitForSeconds(0.25f);
        NoteBook.GetComponent<Image>().color = OriginalColor;

    }
}

[System.Serializable]
public class NotebookPage
{
    public string text;
    public Sprite image;
}

