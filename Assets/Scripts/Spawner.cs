using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    public GameObject prefab;

    public override void OnStartServer()
    {
        NetworkServer.Spawn(Instantiate(prefab, new Vector3(100f, 10f, 225f), transform.rotation));
        NetworkServer.Spawn(Instantiate(prefab, new Vector3(300f, 10f, 225f), transform.rotation));
        NetworkServer.Spawn(Instantiate(prefab, new Vector3(100f, 10f, 75f), transform.rotation));
        NetworkServer.Spawn(Instantiate(prefab, new Vector3(300f, 10f, 75f), transform.rotation));
    }
}
