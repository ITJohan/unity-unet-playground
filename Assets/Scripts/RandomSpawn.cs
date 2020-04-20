using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(0f, 10f), 1f, Random.Range(0f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
