using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using static RoomController;

public class DeathManager : MonoBehaviour
{
    [Header("spawn TO")]
    [SerializeField]
    private SceneField _sceneToLoad;
    [SerializeField] private DoorToSpawnAt doorToSpawnTo;

    [Header("DeathScreen")]
    public GameObject DeathScreen;
    [Header("ReplayButton")]
    public GameObject replay;
    [Header("FadeIn Animator")]
    public Animator SceneTransition;
    [Header("Sfx")]
    public AudioClip Stab;
    [Header("Background Sound")]
    public GameObject Bgsound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("TrapCH2"))
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        EventSystem.current.SetSelectedGameObject(replay);
        AudioManager.instance.PlaySoundFXClip(Stab, transform, 1, 1);
        Bgsound.SetActive(false);
        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<ChaseSequence>().Chasespeed = 0f;
        GetComponent<Collider2D>().enabled = false;
        SceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneTransition.SetTrigger("Start");
        DeathScreen.SetActive(true);
        Bgsound.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ShadowCaster2D>().enabled = false;

    }
    public void Ressurrect()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        
        SceneSwapManager.SwapScneFromDoorUse(_sceneToLoad, doorToSpawnTo);
        yield return new WaitForSeconds(1f);
        DeathScreen.SetActive(false);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<ShadowCaster2D>().enabled = true;
        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<ChaseSequence>().Chasespeed = 4f;
        

    }
}
