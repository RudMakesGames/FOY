using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UIPuzzleInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;
    private bool isDragging = false;

    private PaperPuzzle puzzleManager;

    [SerializeField] private float snapDistance = 50f; // in pixels
    private Vector2 correctPos;
    bool CanInteract = true;
    public UnityEvent puzzleCompleteCheck;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        puzzleManager = GameObject.Find("PaperPuzzle").GetComponent<PaperPuzzle>();
    }

    private void Start()
    {
        correctPos = puzzleManager.GetCorrectPos(gameObject);
        puzzleCompleteCheck.AddListener(() => puzzleManager.AreAllPiecesCorrectlyPlaced());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(CanInteract)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector2 localPoint);
            offset = rectTransform.anchoredPosition - localPoint;
            isDragging = true;
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        if(CanInteract)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector2 localPoint);
            rectTransform.anchoredPosition = localPoint + offset;
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(CanInteract)
        {
            isDragging = false;

            float tempSnapDistance = PaperPuzzle.oneSnapped ? snapDistance : 150f;

            if (Vector2.Distance(rectTransform.anchoredPosition, correctPos) < tempSnapDistance)
            {
                rectTransform.anchoredPosition = correctPos;
                PaperPuzzle.oneSnapped = true;
                CanInteract = false;
                puzzleCompleteCheck?.Invoke();
            }
        }
        
    }
}
