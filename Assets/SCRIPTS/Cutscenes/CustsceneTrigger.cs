using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CustsceneTrigger : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneController.instance.NextLev();
    }

    // Update is called once per frame
    
    
        
        
        

    
    void Update()
    {
        
    }
}
