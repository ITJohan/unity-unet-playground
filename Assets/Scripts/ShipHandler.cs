﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipHandler : NetworkBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(190f, 0f, 200f));
    }

    // Update is called once per frame
    void Update()
    {
    }
}