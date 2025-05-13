using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    [SerializeField]
    private GameObject UI_Element;
    [SerializeField]
    private Vector2 ObjectPosition;

    [SerializeField]
    private Vector2 desiredLocation;
    [SerializeField]
    private float time;


    public void MoveUI()
    {

        LeanTween.moveLocal(UI_Element, desiredLocation, time);

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
        Debug.Log("Disabled");
    }

}
