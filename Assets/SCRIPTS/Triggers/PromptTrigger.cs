using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PromptTrigger : MonoBehaviour
{
    public GameObject prompt;
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 0.1f;
    private bool isFading = false;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isFading)
            {
                if(spriteRenderer != null)
                {
                    isFading = true;
                    StartCoroutine(FadeOut());
                }
                else
                {
                    prompt.SetActive(false);
                    Destroy(gameObject);
                }
               
                
            }
        }
    }
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color InitialColor = spriteRenderer.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(InitialColor.r, InitialColor.g, InitialColor.b, 1 - t);
            yield return null;
        }
        prompt.SetActive(false);
        Destroy(gameObject);
    }

}

