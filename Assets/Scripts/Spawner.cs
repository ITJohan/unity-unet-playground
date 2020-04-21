using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    public GameObject prefab;

    public override void OnStartServer()
    {
        GameObject entity = Instantiate(prefab, new Vector3(4f, 1f, 4f), transform.rotation);
        NetworkServer.Spawn(entity);
        GameObject entity2 = Instantiate(prefab, new Vector3(7f, 1f, 4f), transform.rotation);
        NetworkServer.Spawn(entity2);
        GameObject entity3 = Instantiate(prefab, new Vector3(4f, 1f, 7f), transform.rotation);
        NetworkServer.Spawn(entity3);
    }
}
