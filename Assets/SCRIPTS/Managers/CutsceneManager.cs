using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    public bool isActive = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
