using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D RB;
    public Vector2 temp;
    public int dir;
    public float bounds = 400;

    private void Start()
    {
        temp = RB.velocity;
        temp = Vector2.left * speed * dir;
        RB.velocity = temp;
    }

    private void FixedUpdate()
    {
        Bounds();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<ExtraPlayerScript>().health -= damage;
            collision.transform.GetComponent<ExtraPlayerScript>().damageDone = true;
        }

        if (collision.transform.tag != "Bullet")
            Destroy(gameObject);
    }

    void Bounds()
    {
        if (transform.position.x > bounds || transform.position.y > bounds || transform.position.z > bounds || transform.position.x < 0 || transform.position.y < 0 || transform.position.z < 0)
            Destroy(gameObject);
    }
}


