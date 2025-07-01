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
                SideScrollerMode();
                break;
            case Perspective.TopDown:
                TopDownMode();
                break;
            case Perspective.Isometric:
               IsometricMode();
                break;
        }
    }

    private void DisableSideScroller()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        InteractionEye.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;      
        GetComponent<PlayerInput>().enabled = false;
    }

    private void EnableSideScroller()
    {
        InteractionEye.SetActive(true);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
    }
    private void SideScrollerMode()
    {
        CamFollow.m_Lens.OrthographicSize = 5;
        CamFollow.Follow = gameObject.transform;
        EnableSideScroller();
        TopDownCharacter.SetActive(false);
        IsometricCharacter.SetActive(false);
    }
    private void TopDownMode()
    {
        CamFollow.m_Lens.OrthographicSize = 6;
        TopDownCharacter.transform.position = gameObject.transform.position;
        DisableSideScroller();
        CamFollow.Follow = TopDownCharacter.transform;
        TopDownCharacter.SetActive(true);
        IsometricCharacter.SetActive(false);
    }
    private void IsometricMode()
    {
        CamFollow.m_Lens.OrthographicSize = 8;
        DisableSideScroller();
        IsometricCharacter.transform.position = gameObject.transform.position;
        CamFollow.Follow = IsometricCharacter.transform;
        TopDownCharacter.SetActive(false);
        IsometricCharacter.SetActive(true);
    }
}
