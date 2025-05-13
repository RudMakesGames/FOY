using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicThunderstorm : MonoBehaviour
{
    [SerializeField] private AudioClip SoundSfx;
    private float Timer;
    public float SoundTime = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= SoundTime)
        {
            AudioManager.instance.PlaySoundFXClip(SoundSfx,transform,1,Random.Range(0.9f,1));
            Timer = 0;
            SoundTime = Random.Range(10,15);
        }
    }
  
}
