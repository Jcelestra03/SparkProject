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
        playertarget = GameObject.Find("player");
        detector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;
        lookPos.Normalize();
        velocity = myRB.velocity;

        if (!isfollowing) 
            velocity.x = idleDir * (idleSpeed * 50) * Time.deltaTime;
            
        if (isfollowing)
            velocity.x = lookPos.x * (attackSpeed * 50) * Time.deltaTime;
            
        myRB.velocity = velocity;

        if (!Physics2D.CircleCast(transform.position * new Vector2(-0.8f, -0.7f), 1, -transform.position))
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
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
        {
            detector.radius = 2.5f;
        } 
    }

    private void Movement()
    {
        
    }
}
