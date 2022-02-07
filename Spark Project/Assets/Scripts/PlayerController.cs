using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 10;
    public Transform checkpoint;

    // Amount of times you can respawn if set to -1 you can respawn infit times.
    [SerializeField] private int respawns = 0;
    // Health you respawn with after you died.
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
    private AudioSource speaker;

    public AudioClip PlayerDeath;
    public AudioClip PlayerJump;
    public AudioClip StarCollected;

    private Animator myAnimator;
    private SpriteRenderer myRenderer;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        savedMaxSpeed = maxSpeed;
        savedJumpHeight = jumpheight;

        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Respawn int: Every time the player dies the respawns int decreases if it reaches 0 and the player dies again the game gos in to the lose stat but if the respawns int is set to -1 the player has infit respawns.
        if (health <= 0 && respawns > 0 || health <= 0 && respawns == -1)
            Respawn();
        else if (health == 0 && respawns == 0)
            Dead();

        // Raycast checks if a enemy is below the player and destroys it.
        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 1), Vector2.down, 0.01f))
            if(Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 1), Vector2.down, 0.01f).transform.tag == "Enemy")
                Destroy(Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 1), Vector2.down, 0.01f).transform.gameObject);

        // Jump check.
        velocity = myRB.velocity;

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
            velocity.y = jumpheight;

        myRB.velocity = velocity;
    }

    // Indicates where the raycast for destroying enemies.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.5f, 1), 0.05f);
    }

    private void FixedUpdate()
    {
        // Movement and controls
        velocity = myRB.velocity;
        velocity.x += Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime;

        groundDetection = new Vector2(transform.position.x, transform.position.y - 0.5f);

        if (velocity.x >= maxSpeed)
            velocity.x = maxSpeed;
            myRenderer.flipX = true;

        if (velocity.x <= -maxSpeed)
            velocity.x = -maxSpeed;
            myRenderer.flipX = false;

        myRB.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Keeps track of the last checkpoint.
        if (collision.tag == "Checkpoint")
            checkpoint = collision.transform;
    }

    // If health reaches 0 player response at last checkpoint if there is no checkpoint player respons at 0.
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
        GameObject.Find("gameManager").GetComponent<GameManager>().lose = true;
    }
}
