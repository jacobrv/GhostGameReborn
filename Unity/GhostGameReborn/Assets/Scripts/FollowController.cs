using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{

    public Rigidbody2D wayPoint;
    private Vector2 wayPointPos;
    private float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPoint) {
            wayPointPos = new Vector2(wayPoint.transform.position.x, wayPoint.transform.position.y);
            //Here, the zombie's will follow the waypoint.
            transform.position = Vector2.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
        }
        
    }
}
