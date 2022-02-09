using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    // Enemy speed when aggroed.
    public float attackSpeed = 10;
    // Enemy speed when docile.
    public float roamSpeed = 5;
    // Amount of time between jumps.
    public float jumpCoolDown = 0.5f;
    // When aggroed layer mask for side raycasts.
    public LayerMask layerObjAvoid;
    // When aggroed layer mask for bottom raycasts.
    public LayerMask layerPlayer;

    public AudioClip EnemyDeath;
    public AudioClip WallColliding;

    private float jumpCoolDownMule;
    private bool isfollowing = false;
    private GameObject playertarget;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private CircleCollider2D detector;
    private int roamDir = 1;
    private int pursuitDir;
    private bool swop = true;
    private float jumpheight = 6.5f;
    private float coolDown;
    private AudioSource speaker;
	
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        
        detector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // Jump cool down ticker.
        if (jumpCoolDownMule > 0)
            jumpCoolDownMule -= 1 * Time.deltaTime;

        //Searches for the player in the scene if failed to find player on start.
        if (playertarget == null)
            playertarget = GameObject.FindGameObjectWithTag("Player");

        // Direction swop cool down ticker.
        if (coolDown > 0)
            coolDown -= 1 * Time.deltaTime;

        // When the enemy is docile raycasts are used to change direction when walking in to a wall or change direction on a cliff.
        if (!isfollowing)
        {
            if (!Physics2D.Raycast(transform.position + new Vector3(0, -0.7f, 1), Vector2.down, 0.1f))
                DirSwop();

            if (Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 1), Vector2.right, 0.1f))
                DirSwop();

            if (Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0, 1), Vector2.left, 0.1f))
                DirSwop();
        }

        // When the enemy is aggroed raycasts are used to jump over obstacles to get to the player as well as jump on the players head you are able to change the layer Mask to change enemy behavior.
        if (isfollowing)
        {
            if (Physics2D.Raycast(transform.position + new Vector3(1f, 0, 1), Vector2.right, 0.1f, layerObjAvoid))
                Jump();

            if (Physics2D.Raycast(transform.position + new Vector3(-1f, 0, 1), Vector2.left, 0.1f, layerObjAvoid))
                Jump();

            if (Physics2D.Raycast(transform.position + new Vector3(0, -0.7f, 1), Vector2.down, 0.1f, layerPlayer))
                Jump();
        }
    }

    void FixedUpdate()
    {
        // Locates the player and changes the enemy's direction to go tords the player if the enemy is within 0.1 of the player it will stop pursuing.
        if (playertarget == null) { return; }
        else
        {
            Vector3 lookPos = playertarget.transform.position - transform.position;
            lookPos.Normalize();
            if (transform.position.x < playertarget.transform.position.x + 0.1f && transform.position.x > playertarget.transform.position.x - 0.1f)
                pursuitDir = 0;
            else if (lookPos.x == Mathf.Abs(lookPos.x))
                pursuitDir = 1;
            else
                pursuitDir = -1;
        }
        // Changes the enemy's speed from roaming speed to its pursuing speed.
        velocity = myRB.velocity;

        if (!isfollowing)
            velocity.x = roamDir * roamSpeed * 50 * Time.deltaTime;
        else if (isfollowing)
            velocity.x = pursuitDir * attackSpeed * 50 * Time.deltaTime;

        myRB.velocity = velocity;
    }

    // Indicates where the raycasts are.
    private void OnDrawGizmosSelected()
    {
        // Roaming raycasts indicators.
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + new Vector3(0.5f, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(-0.5f, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.7f, 1), 0.05f);

        // Aggro raycast indicators.
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(1, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(-1, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.41f, 1), 0.05f);
    }

    // If trigger collides with player enemy set to aggro.
    // When aggroed the player tirgger collider radius is set to 5.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.tag == "Player"))
        {
            isfollowing = true;
            detector.radius = 5;
        }   
    }

    // If the player leaves the trigger colliger enemy set to docile stat.
    // When docile the player tirgger collider radius is set to 2.5.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isfollowing && (collision.gameObject.tag == "Player"))
        {
            isfollowing = false;
            detector.radius = 2.5f;
        }
    }

    // Changes movement direction when called on and coolDown is 0.
    private void DirSwop()
    {
        if (coolDown <= 0)
        {
            if (!swop)
            {
                roamDir = 1;
                swop = true;
                coolDown = 0.1f;
            }
            else
            {
                roamDir = -1;
                swop = false;
                coolDown = 0.1f;
            }
        }
    }

    // Aplies jump velocity to enemy when called on and jumpCoolDownMule is 0.
    void Jump()
    {
        velocity = myRB.velocity;

        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.41f, 1), Vector2.down, 0.01f) && jumpCoolDownMule <= 0)
        {
            velocity.y = jumpheight;
            jumpCoolDownMule = jumpCoolDown;
        }
            
        myRB.velocity = velocity;
    } 
}
