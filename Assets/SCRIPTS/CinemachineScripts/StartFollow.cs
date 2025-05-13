using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StartFollow : MonoBehaviour
{
    public CinemachineVirtualCamera CameraFollow;
    public CinemachineVirtualCamera LockedCamera;
    public GameObject BriefcaseFollow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CameraFollow.Priority = 11;
            LockedCamera.Priority = 8;
            BriefcaseFollow.SetActive(true);
           
        }
        
           
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
