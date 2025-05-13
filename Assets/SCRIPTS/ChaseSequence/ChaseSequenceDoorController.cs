using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class ChaseSequenceDoorController : MonoBehaviour
{
    [SerializeField]
    private AudioClip DoorOpening;

    private Collider2D coll2d;
    [SerializeField]
    private Sprite DoorOpenSprite;

    private void Start()
    {
        coll2d = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DoorOpenSprite;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            AudioManager.instance.PlaySoundFXClip(DoorOpening, transform, 1, 1);
            coll2d.enabled = false;

        }
    }
}
