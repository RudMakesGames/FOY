using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UtilityRoomPuzzleResponse : Response, IResponse
{
    [SerializeField]
    GameObject ClueForNextPuzzle;
    [SerializeField]
    Light2D[] lights;
    [SerializeField]
    AudioSource Sounds;
    public void PuzzleResponse()
    {
        ClueForNextPuzzle.SetActive(true);
        foreach(Light2D light in lights)
        {
            light.intensity = 1;
        }
        Sounds.Play();
    }
    public override void OnPuzzleFinish()
    {
        PuzzleResponse();
    }
}
