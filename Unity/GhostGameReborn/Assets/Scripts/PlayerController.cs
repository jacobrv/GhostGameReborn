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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
}
