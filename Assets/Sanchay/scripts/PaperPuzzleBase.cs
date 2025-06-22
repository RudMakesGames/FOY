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
public class PaperPuzzleBase : MonoBehaviour
{
    public List<PaperHolder> puzzlePaper = new List<PaperHolder>();
    Dictionary<GameObject, Vector2> piecePosDict;
    public static bool oneSnapped = false;

    public static bool allPiecesCorrect=false;
    PaperAppearEffect appearEffect;

    private void Awake()
    {
        piecePosDict = new Dictionary<GameObject, Vector2>();

        foreach(PaperHolder p in puzzlePaper)
        {
            if(p.PaperPiece!=null && p.correctPos!=null)
            {
                piecePosDict[p.PaperPiece] = p.correctPos;
            }
            Debug.Log("Piece Mapped: " + p.PaperPiece + " Mapped Pos: " + p.correctPos);
        }
        appearEffect = GameObject.Find("paperPuzzle").GetComponent<PaperAppearEffect>();
    }

    public Vector2 GetCorrectPos(GameObject puzzlePiece)
    {
        if(piecePosDict.ContainsKey(puzzlePiece))
        {
            return piecePosDict[puzzlePiece];
        }

        else return puzzlePiece.transform.position;
    }

    public void AreAllPiecesCorrectlyPlaced()
    {
        foreach (var p in piecePosDict)
        {
            GameObject piece = p.Key;
            Vector2 correctPos = p.Value;

            float dist = Vector2.Distance(piece.transform.position, correctPos);
            if (dist > 0.15f)
            {
                allPiecesCorrect = false;
                return;
            }
        }

        allPiecesCorrect = true;
        Debug.Log("puzzle Complete");
        appearEffect.startEffect();
    }

}
