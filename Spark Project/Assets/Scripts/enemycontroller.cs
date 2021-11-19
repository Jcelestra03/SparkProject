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
    private int idleDir = -1;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playertarget = GameObject.Find("Player");
        detector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //this is the broke boy i think it could also have to do somthing with the dirswop or ideal dir or just somthing in the movement systum with idle
        if (!Physics2D.CircleCast(transform.position + new Vector3(0, -0.7f, 1), 0.1f, transform.position))
        {
            DirSwop();
            Debug.Log("-+");
        }
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
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.7f, 1), 0.1f) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.tag == "player"))
        {
            isfollowing = true;
            detector.radius = 5;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isfollowing && (collision.gameObject.tag == "player"))
            detector.radius = 2.5f;
    }

    private void DirSwop()
    {
        bool swop = false;
        swop = !swop;

        if (swop == true)
            idleDir = 1;
        else if (swop == false)
            idleDir = -1;
    }
}
