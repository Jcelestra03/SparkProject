using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public float nockBack = 5;
    public int damage = 1;

    private float coolDown;

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= 1 * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<Rigidbody2D>().AddRelativeForce(-collision.relativeVelocity * nockBack);
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<Rigidbody2D>().AddRelativeForce(-collision.relativeVelocity * nockBack);
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }
}
