using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;
    public float attackRecur = 1;

    private float coolDown;

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= attackRecur * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }
}
