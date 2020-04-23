using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TouchManager : NetworkBehaviour
{
    // Variables
    Dictionary<int, GameObject> fingerIdToShip = new Dictionary<int, GameObject>();
    Camera cam;

    // Set finger id to -1 on ship and remove from dict
    void DeregisterShip(Touch currenTouch)
    {
        
    }

    // Set up ship and finger id if touch on ship
    void InitTouch(Touch currentTouch)
    {
        Ray ray = cam.ScreenPointToRay(currentTouch.position);
        RaycastHit hit;

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
                shipHandler.FingerId = currentTouch.fingerId;
            }
        }
    }

    void RegisterTouchToShip(Touch currentTouch)
    {
        int currentFingerId = currentTouch.fingerId;
        
        if (fingerIdToShip.ContainsKey(currentFingerId))
        {
            GameObject shipObject = fingerIdToShip[currentFingerId];

            // Handle if ship is destroyed while drawing path
            if (shipObject != null)
            {
                ShipHandler shipHandler = shipObject.GetComponent<ShipHandler>();
                if (currentFingerId == shipHandler.FingerId)
                {
                    Vector3 point = cam.ScreenToWorldPoint(new Vector3(currentTouch.position.x, currentTouch.position.y, cam.transform.position.y - shipObject.transform.position.y));
                    shipHandler.AddPointToPath(point);
                }
            }
        }
    }

    // Set up camera at start
    void Start()
    {
        // Disable GetMouseButtonDown(0) to register on touch
        Input.simulateMouseWithTouches = false;
        cam = GetComponent<Camera>();

        if (isLocalPlayer) return;
        cam.enabled = false;
    }

    // Handles all touches on screen each frame
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
                    case TouchPhase.Moved:
                        RegisterTouchToShip(currentTouch);
                        break;
                    case TouchPhase.Ended:
                        DeregisterShip(currentTouch);
                        break;
                }
            }
        }
    }
}
