﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipHandler : NetworkBehaviour
{
    // Variables
    int fingerId;
    List<Vector3> path = new List<Vector3>();
    LineRenderer lineRenderer;
    Rigidbody rigidBody;
    Vector3 currentDirection;
    public float speed = 2f;

    // Setter for speed
    public float Speed
    {
        set { speed = value; }
    }

    // Getter and setter for fingerId
    public int FingerId
    {
        get { return fingerId; }
        set { fingerId = value; }
    }
   
    // Setter for currentDirection
    public Vector3 CurrentDirection
    {
        set { currentDirection = value; }
    }

    // Adds a point to the path and redraws the path
    public void AddPointToPath(Vector3 point)
    {
        path.Add(point);
        DrawLines();
    }
    
    // Clear the path
    public void ClearPath()
    {
        path.Clear();
    }

    void DrawLines()
    {
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray());
    }
    
    // Update movement
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        HandleMovement();
    }

    // Handle movement based on path exist
    void HandleMovement()
    {
        if (path.Count == 0)
        {
            MoveFreely();
        }
        else
        {
            MoveToPoint();
        }
    }

    // Make object go in a straight line after last point
    void MoveFreely()
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = currentDirection;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentDirection, transform.up), 0.15f);
    }

    // Move to the next point in path
    void MoveToPoint()
    {
        rigidBody.isKinematic = true;

        // Move one step towards current point
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, path[0], step);

        if (transform.position.Equals(path[0]))
        {
            // Delete point if reached
            path.RemoveAt(0);

            // Recalculate line renderer
            DrawLines();
        }
        else
        {
            // Save ccurrent direction
            currentDirection = (path[0] - transform.position).normalized * speed;

            // Face right direction
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentDirection, transform.up), 0.15f);
        }
    }

    // Register components on start
    void Start()
    {
        if (!isServer)
        {
            return;
        }

        lineRenderer = GetComponent<LineRenderer>();
        rigidBody = GetComponent<Rigidbody>();

        // TEMPORARY (to get start velocity)
        currentDirection = new Vector3(speed, 0f, 0f);
    }

}

