using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject Door;
    public bool isOpen;
    public Animator anim;
    [SerializeField]private AudioClip DoorOpen;
    public GameObject Alert;


    private void Start()
    {
       
        anim = gameObject.GetComponent<Animator>();
    }
    public void OpenDoor(GameObject obj)
    {
        
        if(!isOpen)
        {
            PlayerManager manager = obj.GetComponent<PlayerManager>();
            if (manager.KeysCount > 0)
            {
                isOpen = true;
                //add animation of door opening
                Door.GetComponent<SpriteRenderer>().enabled = false;
                Door.GetComponent<BoxCollider2D>().enabled = false;
                PlayerManager.Instance.UseKey();
                AudioManager.instance.PlaySoundFXClip(DoorOpen, transform, 1, 1);
                anim.SetBool("DoorOpen", true);
                TaskManager.instance.TaskComplete();
                Debug.Log("Door Unlocked");

            }
            else if (manager.KeysCount <= 0)
            {
                StartCoroutine(NoKey());
            }
        }
            
        
        

    }
    IEnumerator NoKey()
    {
        Debug.Log("NO KEY");
        Alert.SetActive(true);
        yield return new WaitForSeconds(2);
        Alert.SetActive(false);
    }
    /*
    public void OpenDoor()
    {
        PlayerManager.Instance.UseKey();
        anim.SetBool("DoorOpen", isOpen);
        DoorOpen.Play();
        Debug.Log("Door Unlocked");
        Destroy(Door);
    }*/
   
}
