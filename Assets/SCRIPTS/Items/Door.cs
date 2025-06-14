using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform Destination;
    [SerializeField]
    private float Delay;
    private GameObject Player;
    public CinemachineConfiner2D confiner;
    public Collider2D CamBound;
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
        //add animation UI
        yield return new WaitForSeconds(Delay);
        Player.transform.position = Destination.position;
        confiner.m_BoundingShape2D = CamBound;
    }
}
