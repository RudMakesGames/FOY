using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public GameObject NoteImage;
    public AudioClip NotePickup;
    public GameObject NotePrefab;
    public GameObject InventoryPrompt;
    [SerializeField]
    //private Collider2D NoteCollider;

    public void GetNote()
    {
        StartCoroutine(PickupNote());

    }
    IEnumerator PickupNote()
    {
        //NoteCollider.enabled = false;
        NoteImage.SetActive(true);
        AudioManager.instance.PlaySoundFXClip(NotePickup, transform, 1, 1);
        NotePrefab.SetActive(true);
        //NotePrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        //NotePrompt.SetActive(false);
        
    }
    IEnumerator CloseNote()
    { NoteImage.SetActive(false);
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<Collider2D>().enabled = false;
      InventoryPrompt.SetActive(true);
      yield return new WaitForSeconds(3);
      gameObject.SetActive(false);
      InventoryPrompt.SetActive(false);
     
    }
    public void ExitNote()
    {
        StartCoroutine (CloseNote());
    }
    
}
