using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D followTarget;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        this.transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -10);
    }
}
