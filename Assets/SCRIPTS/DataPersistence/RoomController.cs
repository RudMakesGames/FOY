using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : TriggerInteractBase
{
    [Header("spawn TO")]
    [SerializeField]
    private SceneField _sceneToLoad;
    [SerializeField] private DoorToSpawnAt doorToSpawnTo;

    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    [Space(10f)]
    [Header("This Door")]
    public DoorToSpawnAt CurrentDoorPosition;
    
    public override void Interact()
    {
        Debug.Log("Teleporting");
        SceneSwapManager.SwapScneFromDoorUse(_sceneToLoad, doorToSpawnTo);

    }
}

