using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PaperHolder
{
    public int index;
    public GameObject PaperPiece;
    public List<GameObject> NeighbourPieces;
    public Vector2 correctPos;
}

public class PaperPuzzle : MonoBehaviour, IPuzzle
{
    public List<PaperHolder> puzzlePaper = new List<PaperHolder>();
    private Dictionary<GameObject, Vector2> piecePosDict;

    public static bool oneSnapped = false;
    public static bool allPiecesCorrect = false;
    [SerializeField]
    private PaperAppearEffect appearEffect;
    [SerializeField]
    GameObject CompletedPuzzle;
    public Response response;
    [SerializeField] private GameObject Puzzle;

    private void Awake()
    {
        piecePosDict = new Dictionary<GameObject, Vector2>();

        foreach (PaperHolder p in puzzlePaper)
        {
            if (p.PaperPiece != null)
            {
                piecePosDict[p.PaperPiece] = p.correctPos;
                Debug.Log($"Piece Mapped: {p.PaperPiece.name} → {p.correctPos}");
            }
        }

        appearEffect = GameObject.Find("CompletedpaperPuzzle")?.GetComponent<PaperAppearEffect>();
    }

    public Vector2 GetCorrectPos(GameObject puzzlePiece)
    {
        if (piecePosDict.TryGetValue(puzzlePiece, out Vector2 pos))
        {
            return pos;
        }

        // fallback if something went wrong
        return puzzlePiece.GetComponent<RectTransform>().anchoredPosition;
    }

    public void AreAllPiecesCorrectlyPlaced()
    {
        foreach (var entry in piecePosDict)
        {
            GameObject piece = entry.Key;
            Vector2 correctPos = entry.Value;

            RectTransform rt = piece.GetComponent<RectTransform>();
            if (rt == null) continue;

            float dist = Vector2.Distance(rt.anchoredPosition, correctPos);
            if (dist > 10f) 
            {
                allPiecesCorrect = false;
                return;
            }
        }

        OnPuzzleComplete();
    }

    public void OpenPuzzle()
    {
        Puzzle.SetActive(true);
    }

    public void ClosePuzzle()
    {
        Puzzle.SetActive(false);
    }

    public void OnPuzzleComplete()
    {
        allPiecesCorrect = true;
        Debug.Log("Puzzle Complete!");
        CompletedPuzzle?.SetActive(true);
        appearEffect?.startEffect();
        ClosePuzzle();
        response?.OnPuzzleFinish();
    }
}   
