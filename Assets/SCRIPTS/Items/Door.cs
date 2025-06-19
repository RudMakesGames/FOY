using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PerspectiveType;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Destination;
    [SerializeField] private float Delay;
    private GameObject Player,Isometric,Topdown;
    public CinemachineConfiner2D confiner;
    public Collider2D CamBound;
    public Perspective Perspective;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void GoToTheDestination()
    {
        StartCoroutine(TeleportToAnotherRoom());
    }

    IEnumerator TeleportToAnotherRoom()
    {
        yield return new WaitForSeconds(Delay);
        SetPlayerPerspective(Perspective);
        Player.transform.position = Destination.position;
        confiner.m_BoundingShape2D = CamBound;
    }

    public void SetPlayerPerspective(Perspective perspective)
    {
        Player.GetComponent<PerspectiveChanger>().perspective = perspective;
    }
}
