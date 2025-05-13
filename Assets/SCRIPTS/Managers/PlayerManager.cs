using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject Notification;
    public int KeysCount;
    public static PlayerManager Instance;
    public int BroomCount;
    public int ToyClueCount;
    public int PhotoCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PickupKey()
    {
        KeysCount++;
        Debug.Log("Picked Up");
    }
    public void UseKey()
    {
        KeysCount--;
        Debug.Log("Used Key");
    }
    public void NotifyPlayer()
    {
        Notification.SetActive(true);
    }
    public void DeNotifyPlayer()
    { Notification.SetActive(false); }
    public void PickupBroom()
    {
        BroomCount++;
    }
    public void UseBroom()
    {
        BroomCount--;
    }
}
