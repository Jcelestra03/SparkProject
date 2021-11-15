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

    private float groundDetectDistance = .1f;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private Vector2 groundDetection;
    private bool canjump;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (health <= 0 && respawns > 0 || health <= 0 && respawns == -1)
            Respawn();
        else if (health == 0 && respawns == 0)
            Dead();

        velocity = myRB.velocity;
        velocity.x += Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime;

        groundDetection = new Vector2(transform.position.x, transform.position.y - 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            velocity.y = jumpheight;
            canjump = false;
        }
        
        if (velocity.x >= maxSpeed)
            velocity.x = maxSpeed;

        if (velocity.x <= -maxSpeed)
            velocity.x = -maxSpeed;

        myRB.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // game over if no respawn marker present, else player respawn at nearest respawn marker
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
