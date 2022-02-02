using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D RB;
    public int dir;

    private void FixedUpdate()
    {
        Vector2 temp = RB.velocity;
        temp = Vector2.left * speed * dir;
        RB.velocity = temp;

        Bounds();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.gameObject.GetComponent<PlayerController>().health -= damage;

        if (collision.transform.tag != "Bullet")
            Destroy(gameObject);
    }

    void Bounds()
    {
        if (transform.position.x > 400 || transform.position.y > 400 || transform.position.z > 400 || transform.position.x < -400 || transform.position.y < -400|| transform.position.z < -400)
            Destroy(gameObject);
    }
}
