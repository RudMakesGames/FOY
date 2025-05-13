using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;
    public bool canFlicker = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
