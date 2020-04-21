using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Camera cam; 

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) return;
 
        cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
