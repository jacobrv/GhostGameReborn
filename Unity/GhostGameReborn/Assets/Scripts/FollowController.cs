using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{

    public Rigidbody2D wayPoint;
    public Rigidbody2D rb;
    private Vector2 wayPointPos;
    private Vector2 myPos;
    private float speed = 4.0f;
    public float followDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPoint) {
            wayPointPos = new Vector2(wayPoint.transform.position.x, wayPoint.transform.position.y);
            myPos = new Vector2(transform.position.x, transform.position.y);
            var dis = Vector2.Distance(wayPointPos, myPos);
            if(dis > followDistance) {
                transform.position = Vector2.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
            }
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision) {  
        
    }
}
