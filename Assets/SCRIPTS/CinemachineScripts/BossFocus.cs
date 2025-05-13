using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BossFocus : MonoBehaviour
{
    public CinemachineVirtualCamera PlayerCam;
    public CinemachineVirtualCamera BossCam;
    float TransitionTime = 3f;
    public int PlayerCamPriority = 10;
    public int BossCamPriority = 11;
    public int BossCamInitialPriority = 8;
    [Header("Collider Ref")]
    public GameObject Collider;


    private void Start()
    {
        PlayerCam.Priority = PlayerCamPriority;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SwitchCam());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator SwitchCam()
    {
        CutsceneManager.Instance.isActive = true;
        PlayerCam.Priority = PlayerCamPriority;
        BossCam.Priority = BossCamPriority;
        yield return new WaitForSeconds(TransitionTime);
        BossCam.Priority = BossCamInitialPriority;
        CutsceneManager.Instance.isActive = false;
        if (Collider != null)
        {
            Collider.SetActive(true);
        }


    }
}
