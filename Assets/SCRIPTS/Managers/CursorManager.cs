using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    public Texture2D cursorTexture; // Assign the custom cursor texture in the Inspector
    public Vector2 hotSpot = Vector2.zero; // Set the point in the cursor image to be the hotspot (usually top-left corner)

    void Start()
    {
        // Set the custom cursor when the game starts
        SetCursor();
    }

    void SetCursor()
    {
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        }
        else
        {
            Debug.LogWarning("Cursor texture is not assigned.");
        }
    }
}
