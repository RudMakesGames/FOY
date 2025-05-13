using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveTrigger : MonoBehaviour
{
    public bool WaitforsomeTime = false;
    public bool playSequentialSetActive = false;
    public float Time;
    [SerializeField]
    private GameObject ObjectToSetActive;
    [SerializeField]
    private AudioClip Hearbeat;

    [Header("Letters ref")]
    [SerializeField]
    private GameObject one, two, three;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(WaitforsomeTime)
            {
                StartCoroutine(SetActiveAfterSomeTime());
            }
            else if(playSequentialSetActive)
            {
               StartCoroutine(SequentialAction());
            }
            else if(!WaitforsomeTime && !playSequentialSetActive)
            {
                ObjectToSetActive.SetActive(true);
            }
            
        }
    }
    IEnumerator SetActiveAfterSomeTime()
    {
        yield return new WaitForSeconds(Time);
        ObjectToSetActive.SetActive(true);
    }

    IEnumerator SequentialAction()
    {
        yield return new WaitForSeconds(Time);
        ObjectToSetActive.SetActive(true);
        one.SetActive(true);
        AudioManager.instance.PlaySoundFXClip(Hearbeat, transform, 1, 1);
        yield return new WaitForSeconds(1f);
        two.SetActive(true);
        yield return new WaitForSeconds(1f);
        three.SetActive(true);
        yield return new WaitForSeconds(1f);
        one.gameObject.GetComponent<Image>().color = Color.red;
        two.gameObject.GetComponent<Image>().color = Color.red;
        three.gameObject.GetComponent<Image>().color = Color.red;
    }
}
