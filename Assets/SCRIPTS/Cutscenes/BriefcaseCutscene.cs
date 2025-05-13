using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BriefcaseCutscene : MonoBehaviour
{
    
    public GameObject Briefcase,BGmanagerAfterLightChange,BGmanager;
    public float Timed = 0.5f;
    public AudioClip Thump;
    public Animator anim;
    public Light2D globalLight2D;
    public Light2D[] lights;
    public GameObject[] objects;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera BriefcaseCamera;
    public Color NewColor;
    public float TargetIntensity = 2;
    public GameObject HintTrigger;
    float TransitionTime = 4.5f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BriefcaseFall());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
       
    }
    IEnumerator BriefcaseFall()
    {
        CutsceneManager.Instance.isActive = true;
        StartCoroutine(SwitchCameras());
        BGmanager.SetActive(false);
        BGmanagerAfterLightChange.SetActive(true);
        yield return new WaitForSeconds(2);
        Briefcase.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(Timed);
        Briefcase.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        AudioManager.instance.PlaySoundFXClip(Thump, transform, 1, 1);
        LightManager.instance.canFlicker = true;
        globalLight2D.intensity = 0.1f;
        foreach(Light2D light in lights)
        {
            light.color = NewColor;
            light.intensity = TargetIntensity;
        }
        foreach(GameObject obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().color = NewColor;
        }
        yield return new WaitForSeconds(2);
        HintTrigger.SetActive(true);
        yield return new WaitForSeconds(1);
        CutsceneManager.Instance.isActive = false;


    }
    IEnumerator SwitchCameras()
    {
        virtualCamera.Priority = 10;
        BriefcaseCamera.Priority = 11;
        yield return new WaitForSeconds(TransitionTime);
        virtualCamera.Priority = 10;
        BriefcaseCamera.Priority = 9;

    }
}
