using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    public float attackSpeed = 10;
    public float idleSpeed = 5;

    private bool isfollowing = false;
    private GameObject playertarget;
    private Rigidbody2D myRB;
    private Vector2 velocity;
    private CircleCollider2D detector;
    private int idleDir = 1;
    private bool swop = true;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playertarget = GameObject.Find("Player");
        detector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!Physics2D.Raycast(transform.position + new Vector3(0, -0.7f, 1), Vector2.right, 0.1f))
            DirSwop();
    }

    void FixedUpdate()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;
        lookPos.Normalize();
        velocity = myRB.velocity;

        if (!isfollowing)
            velocity.x = idleDir * idleSpeed * 50 * Time.deltaTime;
        else
            velocity.x = lookPos.x * attackSpeed * 50 * Time.deltaTime;

        myRB.velocity = velocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.7f, 1), 0.05f) ;
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
            detector.radius = 2.5f;
    }

    private void DirSwop()
    {
        if (!swop)
        {
            idleDir = 1;
            swop = true;
        }
        else
        {
            idleDir = -1;
            swop = false;
        }
    }
}
