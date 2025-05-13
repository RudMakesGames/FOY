using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeddyGameManager : MonoBehaviour
{
    [SerializeField]
    private Animator anim,SceneTransition;
    [SerializeField]
    AudioClip Voice;
    [SerializeField]
    private GameObject VhsEffect;

    private bool hasPlayedAudio = false;

    public string Tutorial = "Tutorial";
    void Update()
    {
     if(TeddyBearController.AttachmentCount == 3 &&!hasPlayedAudio)
        {
            StartCoroutine(LoadLevel());
        }
        
    }

    IEnumerator LoadLevel()
    {
        hasPlayedAudio = true;
        VhsEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySoundFXClip(Voice, transform, 1, 1); 
        yield return new WaitForSeconds(1f);
        SceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Tutorial);
        SceneTransition.SetTrigger("Start");
        TeddyBearController.AttachmentCount = 0;
    }
}
