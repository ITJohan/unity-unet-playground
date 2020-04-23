using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TouchManager : NetworkBehaviour
{
    Dictionary<int, GameObject> fingerIdToShip = new Dictionary<int, GameObject>();
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch currentTouch = Input.touches[i];

                switch (currentTouch.phase)
                {
                    case TouchPhase.Began:
                        InitTouch(currentTouch);
                        break;
                }
            }
        }
    }

    private void InitTouch(Touch currentTouch)
    {
        Ray ray = Camera.main.ScreenPointToRay(currentTouch.position);
        RaycastHit hit;
        CmdLogOnServer("InitTouch");

        // Send out ray and see if ship got touched
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Ship")
            {
                // Save ship to dict
                GameObject shipObject = hit.transform.gameObject;
                fingerIdToShip.Add(currentTouch.fingerId, shipObject);

                // Set finger id to ship
                ShipHandler shipHandler = shipObject.GetComponent<ShipHandler>();
                CmdLogOnServer("Touched ship " + shipObject.name);
                //shipHandler.SetFingerId(currentTouch.fingerId);
            }
        }
    }

    [Command]
    void CmdLogOnServer(string msg)
    {
        Debug.Log(msg);
    }
}
