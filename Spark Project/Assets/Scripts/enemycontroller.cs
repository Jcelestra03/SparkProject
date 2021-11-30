using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    public float attackSpeed = 10;
    public float roamSpeed = 5;

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

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playertarget = GameObject.Find("player");
        detector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (coolDown > 0)
            coolDown -= 1 * Time.deltaTime;

        if (!isfollowing)
        {
            if (!Physics2D.Raycast(transform.position + new Vector3(0, -0.7f, 1), Vector2.down, 0.1f))
                DirSwop();

            if (Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 1), Vector2.right, 0.1f))
                DirSwop();

            if (Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0, 1), Vector2.left, 0.1f))
                DirSwop();
        }   

        if (isfollowing)
        {
            if (Physics2D.Raycast(transform.position + new Vector3(1f, 0, 1), Vector2.right, 0.1f))
                Jump();

            if (Physics2D.Raycast(transform.position + new Vector3(-1f, 0, 1), Vector2.left, 0.1f))
                Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;
        lookPos.Normalize();
        if ( lookPos.x == Mathf.Abs(lookPos.x))
            pursuitDir = 1;
        else
            pursuitDir = -1;

        velocity = myRB.velocity;

        if (!isfollowing)
            velocity.x = roamDir * roamSpeed * 50 * Time.deltaTime;
        else if (isfollowing)
            velocity.x = pursuitDir * attackSpeed * 50 * Time.deltaTime;

        myRB.velocity = velocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + new Vector3(0.5f, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(-0.5f, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.7f, 1), 0.05f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(1, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(-1, 0, 1), 0.05f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.41f, 1), 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.tag == "Player"))
        {
            isfollowing = true;
            detector.radius = 5;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isfollowing && (collision.gameObject.tag == "Player"))
        {
            isfollowing = false;
            detector.radius = 2.5f;
        }
    }
    
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

    void Jump()
    {
        velocity = myRB.velocity;

        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.41f, 1), Vector2.down, 0.01f))
            velocity.y = jumpheight;

        myRB.velocity = velocity;
    } 
}
