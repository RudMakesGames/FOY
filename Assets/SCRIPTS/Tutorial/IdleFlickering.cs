using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleFlickering : MonoBehaviour
{
    [SerializeField]
    private float Timer;
    private void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            
                LightManager.instance.canFlicker = false;
            Timer = 0;
            
        }
        else
        {
            Timer += Time.deltaTime;
            if (Timer > 5)
            {
                LightManager.instance.canFlicker = true;
            }
        }

        
        if (Input.anyKey)
        {
            LightManager.instance.canFlicker = false;
            Timer = 0;
        }
        else
        {
            Timer += Time.deltaTime;
            if (Timer > 5)
            {
                LightManager.instance.canFlicker = true;
            }
        }
        
        

    }
}
