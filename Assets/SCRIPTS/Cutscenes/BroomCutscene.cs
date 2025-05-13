using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BroomCutscene : MonoBehaviour
{
    public GameObject Trap;
    public PlayableDirector Cutscene1;
    public GameObject BriefcaseScene;
    public GameObject BriefcaseDialogue;
    public GameObject Note;
    public GameObject NoteItem,Task2,NewRespawnPoint, NewRespawnPoint2;
    public Animator anim;
    public AudioClip NoteDrop;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(PlayerManager.Instance.BroomCount > 0)
            {
                StartCoroutine(Clean());
            }

        }
    }
    private IEnumerator Clean()
    {
        Cutscene1.Play();
        CutsceneManager.Instance.isActive = true;
        yield return new WaitForSeconds(2);
        Destroy(Trap);
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySoundFXClip(NoteDrop, transform, 1, 1);
        TaskManager.instance.TaskComplete();
        PlayerManager.Instance.UseBroom();
        yield return new WaitForSeconds(0.5f);
        BriefcaseScene.SetActive(true);
        BriefcaseDialogue.SetActive(true);
        CutsceneManager.Instance.isActive = false;
        Note.SetActive(true);
        anim.SetTrigger("Notefall");
        NoteItem.SetActive(true);
        Task2.SetActive(true);
        NewRespawnPoint.SetActive(true);
        NewRespawnPoint2.SetActive(true);
        Destroy(gameObject);
    }

}
