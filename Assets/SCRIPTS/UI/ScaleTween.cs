using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    [SerializeField]
    private GameObject UI_Element;
    [SerializeField]
    private float time;

    private void Start()
    {
        LeanTween.scale(UI_Element,  new Vector3(1, 1, 1), time);
    }

}
