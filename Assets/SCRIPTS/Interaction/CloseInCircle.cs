using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CloseInCircle : MonoBehaviour
{
    [SerializeField]
    private Light2D circleLight;

    [SerializeField]
    private float reductionInterval = 7f; 
    [SerializeField]
    private float smoothTransitionDuration = 1f; 

    [SerializeField]
    private string level; 

    [SerializeField]
    private Animator anim; 

    private float timer = 0f;
    private float targetRadius;
    private float initialRadius;
    private bool isDepleting = false;

    public float VolumeInc = 0.05f;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        anim.SetTrigger("Start");
        initialRadius = circleLight.pointLightOuterRadius;
        targetRadius = initialRadius; 
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= reductionInterval)
        {
            
            if (!isDepleting)
            {
                StartCoroutine(SmoothTransitionToNewRadius(circleLight.pointLightOuterRadius - 1));
                timer = 0f;
                reductionInterval--;
                audioSource.volume = audioSource.volume + VolumeInc;
            }
        }

       
        if (isDepleting)
        {
            float elapsedTime = Time.time - timer;
            float transitionProgress = Mathf.Clamp01(elapsedTime / smoothTransitionDuration);
            circleLight.pointLightOuterRadius = Mathf.Lerp(targetRadius + 1, targetRadius, transitionProgress);

            
            if (transitionProgress >= 1f)
            {
                isDepleting = false; // Transition complete
            }
        }
    }

    private IEnumerator SmoothTransitionToNewRadius(float newRadius)
    {
        isDepleting = true;
        targetRadius = newRadius;
        float startRadius = circleLight.pointLightOuterRadius;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / smoothTransitionDuration);
            circleLight.pointLightOuterRadius = Mathf.Lerp(startRadius, targetRadius, t);
            yield return null;
        }

        circleLight.pointLightOuterRadius = targetRadius; 

        if (targetRadius <= 0f)
        {
            StartCoroutine(RestartLevel());
            audioSource.Stop();
        }
    }

    IEnumerator RestartLevel()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
        
    }

}
