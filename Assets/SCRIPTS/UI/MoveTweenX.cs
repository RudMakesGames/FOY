using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTweenX : MonoBehaviour
{

    [SerializeField]
    private GameObject UI_Element;
    [SerializeField]
    private Vector3 ObjectPosition;

    [SerializeField]
    private float desiredLocation;
    [SerializeField]
    private float time;
   

    public void MoveUI()
    {
        
        LeanTween.moveX(UI_Element,desiredLocation,time);
      
    }
    private void Start()
    {
        
        MoveUI();
    }
    private void OnEnable()
    {
        MoveUI();
        Debug.Log("Enabled");
    }
    private void OnDisable()
    {
        gameObject.transform.position = ObjectPosition;
    }


}
