using Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;
    private RoomController.DoorToSpawnAt _doorToSpawnTo;
    [SerializeField]
    private Animator SceneTransition;
    private GameObject _Player;
    private Collider2D _playerColl;
    private Collider2D _DoorCollider;
    private Vector3 _playerSpawnPosition;
    private static bool _loadFromDoor;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        _Player = GameObject.FindGameObjectWithTag("Player");
        _playerColl = _Player.GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public static void SwapScneFromDoorUse(SceneField myScene,RoomController.DoorToSpawnAt doorToSpawnAt)
    {
        _loadFromDoor = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene,doorToSpawnAt));
    }
    private IEnumerator FadeOutThenChangeScene(SceneField myScene,RoomController.DoorToSpawnAt doorToSpawnAt = RoomController.DoorToSpawnAt.None)
    {
        Move.DisableActions();
        SceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneTransition.SetTrigger("Start");
        _doorToSpawnTo = doorToSpawnAt;
        SceneManager.LoadScene(myScene);
        Move.EnableActions();
       
       


    }
    private void FindDoor(RoomController.DoorToSpawnAt doorSpawnNumber)
    {
        RoomController[] doors = FindObjectsOfType<RoomController>();
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].CurrentDoorPosition == doorSpawnNumber)
            {
                _DoorCollider = doors[i].gameObject.GetComponent<Collider2D>();
                CalculateSpawnPosition();
                return;
            }
        }
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        SceneTransition.SetTrigger("Start");
        if(_loadFromDoor)
        {
            FindDoor(_doorToSpawnTo);
            _Player.transform.position = _playerSpawnPosition;
            _loadFromDoor = false;


        }
    }
    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerColl.bounds.extents.y;
        _playerSpawnPosition = _DoorCollider.transform.position - new Vector3(0F,colliderHeight,0f);
    }
}
