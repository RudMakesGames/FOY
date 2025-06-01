using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleResponse : Response, IResponse
{
    public void PuzzleResponse()
    {
        Debug.Log("Do this");
    }
    public override void OnPuzzleFinish()
    {
        PuzzleResponse();
    }
}
