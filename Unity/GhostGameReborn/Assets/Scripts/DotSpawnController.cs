using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DotSpawnController : NetworkBehaviour
{

    public float secondsBetweenSpawn = 30f;
    public float elapsedTime = 0.0f;
    public Transform dotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsServer) {
            return;    
        }

        elapsedTime += Time.deltaTime;
 
        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;
            SpawnDot();
        }
    }

    void SpawnDot()
    {
        Vector3 spawnPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0f);
        Transform newDot = Instantiate(dotPrefab, spawnPosition, Quaternion.Euler (0, 0, 0));
        newDot.GetComponent<NetworkObject>().Spawn(true);
    }
}
