using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public float nockBack = 5;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<Rigidbody2D>().AddRelativeForce(-collision.relativeVelocity * nockBack);
            collision.transform.GetComponent<PlayerController>().health -= damage;
        }
    }
}
