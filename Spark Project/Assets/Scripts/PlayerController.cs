using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 10;
    public Transform checkpoint;

    [SerializeField] private int respawns = 0;
    [SerializeField] private int respawnHealth = 1;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpheight = 6.5f;

    private float savedMaxSpeed;
    private float savedJumpHeight;
    private float groundDetectDistance = 0.01f;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private Vector2 groundDetection;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        savedMaxSpeed = maxSpeed;
        savedJumpHeight = jumpheight;

    }

    void Update()
    {
        if (health <= 0 && respawns > 0 || health <= 0 && respawns == -1)
            Respawn();
        else if (health == 0 && respawns == 0)
            Dead();

        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.41f, 1), Vector2.down, 0.01f))
            if(Physics2D.Raycast(transform.position + new Vector3(0, -0.41f, 1), Vector2.down, 0.01f).transform.tag == "Enemy")
                Destroy(Physics2D.Raycast(transform.position + new Vector3(0, -0.41f, 1), Vector2.down, 0.01f).transform.gameObject);

        velocity = myRB.velocity;

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
            velocity.y = jumpheight;

        myRB.velocity = velocity;
    }

    private void FixedUpdate()
    {
        velocity = myRB.velocity;
        velocity.x += Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime;

        groundDetection = new Vector2(transform.position.x, transform.position.y - 0.5f);

        if (velocity.x >= maxSpeed)
            velocity.x = maxSpeed;

        if (velocity.x <= -maxSpeed)
            velocity.x = -maxSpeed;

        myRB.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
            checkpoint = collision.transform;
    }

    private void Respawn()
    {
        health = respawnHealth;
        if (checkpoint == null)
            transform.position = Vector2.zero;
        else
            transform.position = checkpoint.position;

        if (respawns != -1)
        respawns--;
    }

    private void Dead()
    {
        Debug.Log("dead");
        //let the game manager know where dead
    }
}
