using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDialogueTriggers : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Entity"))
        StartCoroutine(StartDialogue());
    }
    private IEnumerator StartDialogue()
    {
        EnemyDialogue.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        EnemyDialogue.SetActive(false);
    }
}
