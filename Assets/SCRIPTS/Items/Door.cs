using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PerspectiveType;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Destination;
    [SerializeField] private float Delay;
    [SerializeField]
    private GameObject Player,Isometric,Topdown;
    public CinemachineConfiner2D confiner;
    public Collider2D CamBound;
    public Perspective Perspective;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Topdown = FindObjectOfType<TopDownMovement>().gameObject;
    }
    private void Start()
    {
        
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
        Topdown.transform.position = Player.transform.position;
    }

    public void SetPlayerPerspective(Perspective perspective)
    {
        Player.GetComponent<PerspectiveChanger>().perspective = perspective;
    }
}
