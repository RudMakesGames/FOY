using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickTimeTrigger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _CameraShakeCam;
    
    private CinemachineBasicMultiChannelPerlin _MultiChannelPerlin;
    [SerializeField]
    private GameObject EventIcon;
    [SerializeField]
    private Sprite NormalSprite, SuccessSprite, FailureSprite;
    [SerializeField]
    private float Timer = 1.5f;
    [SerializeField]
    private float TimeSlowfactor;
    private bool HasPressedCorrectButton;
    private bool CanPressButton;
    public UnityEvent QuickTimeEvent;
    public UnityEvent FailureEvent;

    private void Start()
    {
        _MultiChannelPerlin = _CameraShakeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }
    private void EnableNormalCamera()
    {
        
        _MultiChannelPerlin.m_AmplitudeGain = 0f;
        _MultiChannelPerlin.m_FrequencyGain = 0f;
    }
    private void EnableCameraShake()
    {
       
        _MultiChannelPerlin.m_AmplitudeGain = 1f;
        _MultiChannelPerlin.m_FrequencyGain = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = TimeSlowfactor;
            EnableNormalCamera();
            EventIcon.SetActive(true);
            HasPressedCorrectButton = false; 
            CanPressButton = true;
            StartCoroutine(StartCountdown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            EnableCameraShake();
            EventIcon.SetActive(false);
            CanPressButton = false;
            EventIcon.GetComponent<Image>().sprite = NormalSprite;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && CanPressButton && !HasPressedCorrectButton)
        {
            HasPressedCorrectButton = true;
            Debug.Log("Pressed");
        }
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSecondsRealtime(Timer); 
        CanPressButton = false;
        Debug.LogWarning("Cannot press any input for event");

        if (HasPressedCorrectButton)
        {
            if(EventIcon.TryGetComponent<Image>(out Image icon))
            {
                EventIcon.GetComponent<Image>().sprite = SuccessSprite;
                QuickTimeEvent.Invoke();
                Debug.Log("Success!");
            }
            else
            {
                QuickTimeEvent.Invoke();
                Debug.Log("Success!");
            }
           
        }
        else
        {
            if (EventIcon.TryGetComponent<Image>(out Image icon))
            {
                EventIcon.GetComponent<Image>().sprite = FailureSprite;
                FailureEvent.Invoke();
                Debug.Log("Failure!");
            }
            else
            {
                FailureEvent.Invoke();
                Debug.Log("Failure!");

            }
                
        }

        yield return new WaitForSecondsRealtime(1f); 
        Time.timeScale = 1f;
        EventIcon.SetActive(false);
    }

}
