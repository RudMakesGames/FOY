using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAppearEffect : MonoBehaviour
{
    [SerializeField] float appearTime = 0.75f;

    SpriteRenderer paperTex;

    int dissolveAmt = Shader.PropertyToID("_DissolveAmt");
    int horizontalAmt = Shader.PropertyToID("_HorizontalDissolve");

    Material mat;

    private void Start()
    {
        paperTex = GetComponent<SpriteRenderer>();
        mat = paperTex.material;

       // StartCoroutine(Appear());
    }

    public void startEffect()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        //yield return new WaitForSeconds(5f);
        float elapsedTime = 0f;
        while(elapsedTime < appearTime+1f)
        {
            elapsedTime += Time.deltaTime;

            
            float lerpedHorizontalVal = Mathf.Lerp(1.1f,0, (elapsedTime / appearTime));
            yield return null;

            float lerpedDissolveVal = Mathf.Lerp(1.1f, 0, (elapsedTime / (appearTime+0.85f)));


            mat.SetFloat("_DissolveAmt", lerpedDissolveVal);
           // Debug.Log(lerpedDissolveVal);
            mat.SetFloat("_HorizontalDissolve", lerpedHorizontalVal);
           // Debug.Log(lerpedHorzontalVal);

            yield return null;
        }

    }
}
