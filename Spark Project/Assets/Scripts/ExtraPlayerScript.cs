using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPlayerScript : MonoBehaviour
{
    public int health = 10;
    public Transform checkpoint;
    public bool canMove = true;
    public LayerMask groundLayer;
    public bool damageDone;

    public AudioClip PlayerDeath;
    public AudioClip PlayerJump;
    public AudioClip EnemyDeath;

    // Amount of times you can respawn if set to -1 you can respawn infit times.
    [SerializeField] private int respawns = 0;
    // Health you respawn with after you died.
    [SerializeField] private int respawnHealth = 1;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpheight = 6.5f;

    private bool inAir;
    private bool faceDir;
    private Animator anim;
    private float savedMaxSpeed;
    private float savedJumpHeight;
    private float groundDetectDistance = 0.01f;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private Vector2 groundDetection;
    private AudioSource speaker;
    private Animator myAnimator;
    private SpriteRenderer myRenderer;
    private float deadTime = 2.24f;
    private float damageTicker;
    private bool played;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        savedMaxSpeed = maxSpeed;
        savedJumpHeight = jumpheight;

        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (damageDone && canMove)
        {
            damageTicker = 0.1f;
            myRenderer.color = Color.red;
            damageDone = false;
        }

        if (damageTicker > 0)
        {
            damageTicker -= 1f * Time.deltaTime;
        }
        else
            myRenderer.color = Color.white;

        // Respawn int: Every time the player dies the respawns int decreases if it reaches 0 and the player dies again the game gos in to the lose stat but if the respawns int is set to -1 the player has infit respawns.
        if (health <= 0 && respawns > 0 || health <= 0 && respawns == -1)
            Respawn();
        else if (health <= 0 && respawns == 0)
            Dead();

        // Raycast checks if a enemy is below the player and destroys it.
        if (Physics2D.Raycast(transform.position + new Vector3(0, -1.05f, 1), Vector2.down, 0.01f))
            if (Physics2D.Raycast(transform.position + new Vector3(0, -1.05f, 1), Vector2.down, 0.01f).transform.tag == "Enemy")
            {
                gameObject.AddComponent<AudioSource>();
                GetComponent<AudioSource>().clip = EnemyDeath;
                GetComponent<AudioSource>().Play();
                Destroy(Physics2D.Raycast(transform.position + new Vector3(0, -1.05f, 1), Vector2.down, 0.01f).transform.gameObject);
            }
        // Jump check.
        velocity = myRB.velocity;

        if (Input.GetKeyDown(KeyCode.Space) && !inAir && canMove)
        {
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = PlayerJump;
            GetComponent<AudioSource>().Play();
            velocity.y = jumpheight;
            inAir = true;
        }
        else if (Physics2D.Raycast(groundDetection, Vector2.down, 0.001f, groundLayer))
        {
            inAir = false;
        }
        else
            inAir = true;

        myRB.velocity = velocity;

        if (inAir == true && canMove == true)
        {
            anim.SetBool("Player_Is_Jumping", true);
            anim.SetBool("Player_Is_Idle", false);
            anim.SetBool("Player_Is_Walking", false);
        }
        else if (inAir == false && canMove == true)
        {
            anim.SetBool("Player_Is_Jumping", false);
        }
    }

    // Indicates where the raycast for destroying enemies.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(0, -1.0f, 1), 0.05f);
    }

    private void FixedUpdate()
    {
        // Movement and controls
        if (canMove)
        {
            velocity = myRB.velocity;
            velocity.x += Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime;

            groundDetection = new Vector2(transform.position.x, transform.position.y - 1.0f);

            if (velocity.x >= maxSpeed)
                velocity.x = maxSpeed;

            if (velocity.x <= -maxSpeed)
                velocity.x = -maxSpeed;

            myRB.velocity = velocity;

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                // Moveing right.
                if (inAir == false)
                {
                    anim.SetBool("Player_Is_Idle", false);
                    anim.SetBool("Player_Is_Walking", true);
                }

                if (faceDir == true)
                {
                    myRenderer.flipX = false;
                    faceDir = false;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                // Moveing left.
                if (inAir == false)
                {
                    anim.SetBool("Player_Is_Idle", false);
                    anim.SetBool("Player_Is_Walking", true);
                }

                if (faceDir == false)
                {
                    myRenderer.flipX = true;
                    faceDir = true;
                }
            }
            else
            {
                // Ideal
                if (inAir == false)
                {
                    anim.SetBool("Player_Is_Idle", true);
                    anim.SetBool("Player_Is_Walking", false);
                }
            }
        }
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
        deadTime -= 1 * Time.deltaTime;
        Debug.Log("dead");

        canMove = false;
        anim.SetBool("Player_is_Dying", true);

        if (deadTime <= 0 && !played)
        {
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = PlayerDeath;
            GetComponent<AudioSource>().Play();

            GameObject.Find("gameManager").GetComponent<GameManager>().lose = true;
            played = true;
        }
    }
}
