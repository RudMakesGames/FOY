using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public static Trap Instance;
    [Header("RespawningPosition")]
    public Transform respawnPoint;
    [Header("Position the Entity Respawns to")]
    public Transform EntityRespawnPos;
    [Header("EntityPosition")]
    public Transform EntityPos;
    [Header("Player Reference")]
    public GameObject Player;
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
    private Collider2D coll2d;
    public bool isCollidable;
    public string CurrentLevel;
    public int DamageAmount;
    [SerializeField]
    private FollowPlayer followPlayer;
    private void Awake()
    {if(Instance == null)
        Instance = this;
      coll2d = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            HealthSystem.instance.TakeDamage(DamageAmount);
            AudioManager.instance.PlaySoundFXClip(Stab, transform, 0.5f, Random.Range(0.9f,1));
            if (HealthSystem.instance.CurrentHealth <=0)
            {
                
                StartCoroutine(Death());
                EntityPos.position = EntityRespawnPos.position;
                if (followPlayer != null)
                    followPlayer.isChasing = false;
            }
        }
    }
    private void Update()
    {
        if(isCollidable)
        {
            coll2d.isTrigger = false;
        }
    }
    public void Die()
    {
        StartCoroutine (Death());
    }
    IEnumerator Death()
    {
        AudioManager.instance.PlaySoundFXClip(Stab, transform, 0.5f, Random.Range(0.9f, 1));
        EventSystem.current.SetSelectedGameObject(replay);
        Bgsound.SetActive(false);
        Player.GetComponent<Animator>().SetBool("isDead",true);
        Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        SceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneTransition.SetTrigger("Start");
        DeathScreen.SetActive(true);
        Bgsound.SetActive(true) ;
        Player.GetComponent<ChaseSequence>().Chasespeed = 0f;
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Player.GetComponent<ShadowCaster2D>().enabled = false;   
    }
    public void RespawnPlayer()
    {
        if(CurrentLevel == "")
        {
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Player.GetComponent<SpriteRenderer>().enabled = true;
            Player.GetComponent<ShadowCaster2D>().enabled = true;
            Player.GetComponent<Animator>().SetBool("isDead", false);
            Player.transform.position = respawnPoint.position;
            DeathScreen.SetActive(false);
            Player.GetComponent<ChaseSequence>().Chasespeed = 4f;
            if (EntityPos != null)
            {
                EntityPos.position = EntityRespawnPos.position;
            }
            if (followPlayer != null)
                followPlayer.isChasing = true;
        }
        else if(CurrentLevel != "")
        {
            SceneManager.LoadScene(CurrentLevel);
            Debug.Log("Scene Restarted");
        }
     


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Death());
            EntityPos.position = EntityRespawnPos.position;
            if (followPlayer != null)
                followPlayer.isChasing = false;
        }
    }
}
