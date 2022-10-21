using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Components
    Rigidbody2D rb;

    // Player
    float walkSpeed = 4;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    public List<FollowController> followingDots;
    public string team;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        followingDots = new List<FollowController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
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
            if(fc && !followingDots.Contains(fc)) {
                if(followingDots.Count == 0) {
                    fc.wayPoint = rb;
                    fc.followDistance = 1.5f;
                }
                else {
                    fc.wayPoint = followingDots[followingDots.Count-1].rb;
                    fc.followDistance = 0.7f;
                }
                followingDots.Add(fc);
            }
        }

                
        if(contact.gameObject.TryGetComponent(out GoalController gc)) {
            Debug.Log("Goal Controller Found.");
            if(gc) {
                
                if(gc.team == team) {
                    followingDots.ForEach((dot)=>{
                        Destroy(dot.rb.gameObject);
                    });
                    followingDots.Clear();
                }
            }
        }

    }
}
