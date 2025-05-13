using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_INPUT : MonoBehaviour
{
    public GameObject Back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OptionsMenu()
    {

        EventSystem.current.SetSelectedGameObject(Back);
    }
}
