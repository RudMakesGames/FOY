using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSequence : MonoBehaviour
{
    public GameObject ChaseSound;
    public GameObject BGsoundManager;
    public GameObject activeCamera;
    public GameObject ShakingCam;
    public float Chasespeed = 3f;
    public float PlayerSpeed = 0f;
    private Animator anim;
    public bool shouldMove = false;
    public static ChaseSequence instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(shouldMove)
        {
            anim.SetBool("IsSprinting", true);
            transform.Translate(Vector3.right * Chasespeed * Time.deltaTime);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chase"))
        {
            BGsoundManager.SetActive(false);
            ChaseSound.SetActive(true);
            activeCamera.SetActive(false);
            ShakingCam.SetActive(true);
            shouldMove = true;
            gameObject.GetComponent<Move>().speed = PlayerSpeed;
            gameObject.GetComponent<Move>().CanChangeDir = false;
        }
       
       
    }
}
