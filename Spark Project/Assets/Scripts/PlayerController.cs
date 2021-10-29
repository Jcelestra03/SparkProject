using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private Vector2 groundDetection;





    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = maxSpeed / 2;

        velocity = myRB.velocity;

        velocity.x += Input.GetAxisRaw("Horizontal") * acceleration;

        if (velocity.x >= maxSpeed)
            velocity.x = maxSpeed;

        if (velocity.x <= -maxSpeed)
            velocity.x = -maxSpeed;

        myRB.velocity = velocity;
    }
}
