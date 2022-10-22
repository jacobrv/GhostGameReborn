using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FollowController : NetworkBehaviour
{

    public Transform wayPoint;
    public Transform rb;
    private Vector2 wayPointPos;
    private Vector2 myPos;
    public NetworkVariable<ulong> ownerPlayerId = new NetworkVariable<ulong>(999);
    private float speed = 4.0f;
    public float followDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsServer) {
            return;
        }

        if(wayPoint) {
            wayPointPos = new Vector2(wayPoint.position.x, wayPoint.position.y);
            myPos = new Vector2(transform.position.x, transform.position.y);
            var dis = Vector2.Distance(wayPointPos, myPos);
            if(dis > followDistance) {
                transform.position = Vector2.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision) {  
        
    }

    [ServerRpc]
    public void SetFollowTargetServerRpc(ulong playerId) {
        PlayerController[] pcs = FindObjectsOfType<PlayerController>();
        for(int i = 0; i<pcs.Length; i++) {
            if(pcs[i].playerId.Value == playerId) {

                if(pcs[i].followingDots.Count==0)
                {
                    wayPoint = pcs[i].gameObject.transform;
                    this.followDistance = 1.5f;
                }
                else {
                    wayPoint = pcs[i].followingDots[pcs[i].followingDots.Count-1].transform;
                    this.followDistance = 0.7f;
                }
                ownerPlayerId.Value = playerId;
                Debug.Log("Setting dot to follow: "+ownerPlayerId.Value);
                pcs[i].followingDots.Add(this);
                break;
            }
        }
    }
}
