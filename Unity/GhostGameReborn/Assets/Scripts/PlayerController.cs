using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerController : NetworkBehaviour
{

    // Components
    Rigidbody2D rb;

    // Player
    float walkSpeed = 4;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    public List<FollowController> followingDots;

    public NetworkVariable<int> team = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<ulong> playerId = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnNetworkSpawn() {
        base.OnNetworkSpawn();

        rb = gameObject.GetComponent<Rigidbody2D>();
        followingDots = new List<FollowController>();     

        if(IsOwner){
            playerId.Value = OwnerClientId;
            team.Value = (int)OwnerClientId % 2;
            CameraController cc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            cc.followTarget = rb;
            Debug.Log("Found the camera!!!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(!IsOwner) return;

        if(inputHorizontal != 0 || inputVertical != 0) {
            if(inputHorizontal != 0 && inputVertical != 0) {
                rb.velocity = new Vector2(inputHorizontal * walkSpeed * speedLimiter, inputVertical * walkSpeed * speedLimiter);
            }
            else {
                rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);
            }
            
        }
        else {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {  

        Rigidbody2D contact = collision.attachedRigidbody;


        if(contact.gameObject.TryGetComponent(out FollowController fc)) {
            Debug.Log("Follow Controller Found.");
            if(fc && !followingDots.Contains(fc) && fc.wayPoint == null) {
                fc.SetFollowTargetServerRpc(playerId.Value);
                followingDots.Add(fc);
            }
        }

                
        if(contact.gameObject.TryGetComponent(out GoalController gc)) {
            Debug.Log("Goal Controller Found.");
            if(gc) {
                
                if(gc.team == team.Value) {
                    DepositDotsServerRpc(OwnerClientId);
                }
            }
        }

    }

     [ServerRpc]
    public void DepositDotsServerRpc(ulong playerId) {
        FollowController[] fcs = FindObjectsOfType<FollowController>();
        for(int i = 0; i<fcs.Length; i++) {
            if(fcs[i].ownerPlayerId.Value == playerId) {
                Destroy(fcs[i].gameObject);
                Debug.Log("Destroying Dot with Owner: "+playerId);
            }
        }

        PlayerController[] pcs = FindObjectsOfType<PlayerController>();
        for(int i = 0; i<pcs.Length; i++) {
            if(pcs[i].OwnerClientId == playerId) {
                pcs[i].followingDots.Clear();
            }
        }
    }
}
