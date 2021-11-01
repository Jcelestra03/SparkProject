using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float jumpheight = 6.5f;
    public float groundDetectDistance = .1f;
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

        groundDetection = new Vector2(transform.position.x, transform.position.y - 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            velocity.y = jumpheight;
        }

        if (velocity.x >= maxSpeed)
            velocity.x = maxSpeed;

        if (velocity.x <= -maxSpeed)
            velocity.x = -maxSpeed;

        myRB.velocity = velocity;
    }
}
