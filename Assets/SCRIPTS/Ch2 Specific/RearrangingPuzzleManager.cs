using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RearrangingPuzzleManager : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab for puzzle pieces
    public Sprite[] puzzleSprites; // Array of sprites for the puzzle pieces
    public Sprite[] correctOrder; // Array to set the correct order in the Inspector
    public int rows = 4;
    public int cols = 4;
    public float tileWidth = 100f; // Width of each tile
    public float tileHeight = 100f; // Height of each tile
    public float horizontalSpacing = 10f; // Horizontal spacing between tiles
    public float verticalSpacing = 10f; // Vertical spacing between tiles
    public GameObject ToyClue; // Reference to the win message UI element
   

    public List<TileInfo> currentArrangement = new List<TileInfo>(); // List to track current tile arrangement
    private GameObject[,] tiles;
    private Vector2Int firstTilePos;
    private GameObject firstTileClicked;

    [Header("NoticeBoard's Collider")]
    [SerializeField]
    private Collider2D coll2D;

    void Start()
    {
        // Ensure correctOrder is set in the Inspector before running
        if (correctOrder.Length != rows * cols)
        {
            Debug.LogError("The length of correctOrder array must match the number of tiles.");
            return;
        }

        CreatePuzzle();
        UpdateCurrentArrangement(); // Initialize current arrangement display
    }

    void CreatePuzzle()
    {
        tiles = new GameObject[rows, cols];
        List<Sprite> shuffledSprites = new List<Sprite>(puzzleSprites);
        ShuffleSprites(shuffledSprites);

        currentArrangement.Clear(); // Clear previous arrangement data

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.GetComponent<Image>().sprite = shuffledSprites[row * cols + col];

                // Calculate position using tileWidth, tileHeight, horizontalSpacing, and verticalSpacing
                float xPosition = col * (tileWidth + horizontalSpacing);
                float yPosition = -row * (tileHeight + verticalSpacing);
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

                tile.GetComponent<Button>().onClick.AddListener(() => OnTileClicked(tile));
                tiles[row, col] = tile;

                // Update current arrangement
                TileInfo tileInfo = new TileInfo
                {
                    sprite = shuffledSprites[row * cols + col],
                    position = new Vector2Int(row, col)
                };
                currentArrangement.Add(tileInfo);
            }
        }
    }

    void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = sprites.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            Sprite temp = sprites[i];
            sprites[i] = sprites[r];
            sprites[r] = temp;
        }
    }

    void OnTileClicked(GameObject clickedTile)
    {
        if (firstTileClicked == null)
        {
            // First tile clicked
            firstTileClicked = clickedTile;
            firstTilePos = GetTilePosition(clickedTile);
        }
        else
        {
            // Second tile clicked
            Vector2Int secondTilePos = GetTilePosition(clickedTile);
            SwapTiles(firstTilePos, secondTilePos);

            // Update current arrangement
            UpdateCurrentArrangement();

            // Check if puzzle is solved
            if (IsPuzzleSolved())
            {
                TriggerWinAction();
            }

            // Reset
            firstTileClicked = null;
        }
    }

    void SwapTiles(Vector2Int pos1, Vector2Int pos2)
    {
        GameObject tile1 = tiles[pos1.x, pos1.y];
        GameObject tile2 = tiles[pos2.x, pos2.y];

        // Swap tiles in the array
        tiles[pos1.x, pos1.y] = tile2;
        tiles[pos2.x, pos2.y] = tile1;

        // Swap tile positions
        Vector2 tempPos = tile1.GetComponent<RectTransform>().anchoredPosition;
        tile1.GetComponent<RectTransform>().anchoredPosition = tile2.GetComponent<RectTransform>().anchoredPosition;
        tile2.GetComponent<RectTransform>().anchoredPosition = tempPos;
    }

    Vector2Int GetTilePosition(GameObject tile)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (tiles[row, col] == tile)
                {
                    return new Vector2Int(row, col);
                }
            }
        }
        return new Vector2Int(-1, -1); // Error value
    }

    bool IsPuzzleSolved()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Sprite currentSprite = tiles[row, col].GetComponent<Image>().sprite;
                Sprite correctSprite = correctOrder[row * cols + col];
                if (currentSprite != correctSprite)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void TriggerWinAction()
    {
        // Display a win message or trigger any other action
        if (ToyClue != null)
        {
            ToyClue.SetActive(true);
            PlayerManager.Instance.ToyClueCount++;
        }
        Debug.Log("Puzzle Solved!");
    }
    public void CloseClue()
    {
        if(ToyClue != null)
        {
            ToyClue.SetActive(false);
            coll2D.enabled = false;
        }
    }
    void UpdateCurrentArrangement()
    {
        currentArrangement.Clear();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Sprite currentSprite = tiles[row, col].GetComponent<Image>().sprite;
                TileInfo tileInfo = new TileInfo
                {
                    sprite = currentSprite,
                    position = new Vector2Int(row, col)
                };
                currentArrangement.Add(tileInfo);
            }
        }
    }
}
