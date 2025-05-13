using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlassController : MonoBehaviour
{
    public GameObject InteractBarrier;
    public AudioClip GlassBreak;
    public Animator anim;
    public GameObject GlassShards;
    public GameObject glassBreakDialogue;
    public void BreakGlass()
    {
        StartCoroutine(GlassDestroyed());

    }
    IEnumerator GlassDestroyed()
    {

        gameObject.GetComponent<PlayerInput>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        anim.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySoundFXClip(GlassBreak, transform, 1, 1);
        yield return new WaitForSeconds(1.25f);
        anim.SetTrigger("Start");
        GlassShards.SetActive(true);
        gameObject.SetActive(false);
        glassBreakDialogue.SetActive(true);
        Destroy(InteractBarrier);



    }
}
