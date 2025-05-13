using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerSupportUI : MonoBehaviour
{
    
    public GameObject CurrentObject;
    public EventSystem eventSystem;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(CurrentObject);
    }
    private void OnDisable()
    {
        eventSystem.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
