using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject FullscreenToggle;
    public GameObject OptionsMenu;
    public GameObject MainMenu;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("FullScreenOn");
    }
    public void Accept(InputAction.CallbackContext context)

    {
        if(context.performed)
        {
            SetFullScreen(true);
            
        }
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        if(context.performed)
        { OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);

        }
    }
    public void ChangeVolume(InputAction.CallbackContext context, float Volume)
    {
        if (context.performed)
        {
            audioMixer.SetFloat("Volume", Volume);
        }
    }
    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
        Debug.Log("QualityChanged");
    }
}
