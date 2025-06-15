using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using static PerspectiveType;

public class PerspectiveChanger : MonoBehaviour
{
    [SerializeField] private GameObject TopDownCharacter;
    [SerializeField] private GameObject IsometricCharacter;
    [SerializeField]
    private GameObject InteractionEye;
    public Perspective perspective;
    private Perspective lastPerspective;

    [SerializeField]
    private CinemachineVirtualCamera CamFollow;
 
    private void Start()
    {
        lastPerspective = Perspective.SideScroller; // default
        ApplyPerspective(perspective);
    }

    private void Update()
    {
        if (perspective != lastPerspective)
        {
            ApplyPerspective(perspective);
            lastPerspective = perspective;
        }
    }

    private void ApplyPerspective(Perspective perspective)
    {
        switch (perspective)
        {
            case Perspective.SideScroller:
                CamFollow.Follow = gameObject.transform;
                EnableSideScroller();
                TopDownCharacter.SetActive(false);
                IsometricCharacter.SetActive(false);
                break;
            case Perspective.TopDown:
                DisableSideScroller();
                CamFollow.Follow = TopDownCharacter.transform;
                TopDownCharacter.SetActive(true);
                IsometricCharacter.SetActive(false);
                break;
            case Perspective.Isometric:
                DisableSideScroller();
                CamFollow.Follow = IsometricCharacter.transform;               
                TopDownCharacter.SetActive(false);
                IsometricCharacter.SetActive(true);
                break;
        }
    }

    private void DisableSideScroller()
    {
        InteractionEye.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerInput>().enabled = false;
    }

    private void EnableSideScroller()
    {
        InteractionEye.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
    }
}
