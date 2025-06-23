using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PuzzleInteraction : MonoBehaviour
{
    bool isDragging=false;
    [SerializeField] Vector3 offSet;
    PaperPuzzle puzzleManager;

    [SerializeField] float snapDistance;
    [SerializeField] Vector2 correctPos;

    public UnityEvent puzzleCompleteCheck;

    private void Awake()
    {
        puzzleManager = GameObject.Find("PaperPuzzle").GetComponent<PaperPuzzle>();
    }
    private void Start()
    {
        correctPos = puzzleManager.GetCorrectPos(gameObject);
        puzzleCompleteCheck.AddListener(()=>puzzleManager.AreAllPiecesCorrectlyPlaced());
    }

    private void OnMouseDown()
    {
        offSet = transform.position- GetMousePos();
        isDragging = true;
    }
    private void OnMouseUp()
    {
        isDragging = false;
        float tempSnapDistance;

        if (!PaperPuzzle.oneSnapped)
            tempSnapDistance = 3.2f;
        else
            tempSnapDistance = snapDistance;

        if(Vector2.Distance(transform.position, correctPos)< tempSnapDistance)
        {
            transform.position = correctPos;
            PaperPuzzle.oneSnapped = true;
            puzzleCompleteCheck?.Invoke();
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMousePos() + offSet;
        }
    }

    Vector3 GetMousePos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 0;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
