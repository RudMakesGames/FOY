using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomController : MonoBehaviour
{
    //public GameObject broomPrompt;
    public GameObject BroomCutscene;
    public AudioClip BroomPickup;

   public void ObtainBroom(GameObject obj)
    {
        PlayerManager manager = obj.GetComponent<PlayerManager>();
        manager.PickupBroom();
       BroomPicker();
        

        
    }
    private void BroomPicker()
    {
        BroomCutscene.SetActive(true);
        AudioManager.instance.PlaySoundFXClip(BroomPickup, transform, 1, 1);
        
        Destroy(gameObject);

    }
}
