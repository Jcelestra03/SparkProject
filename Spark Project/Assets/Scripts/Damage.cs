using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;
    public float attackRecur = 1;

    private bool insideT;
    private bool insideC;
    private float coolDown;
    private Collision2D mem;
    private Collider2D mem2;

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= attackRecur * Time.deltaTime;

        if (coolDown <= 0 && insideT)
        {
            mem2.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 1f;
        }

        if (coolDown <= 0 && insideC)
        {
            mem.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            insideC = true;
            mem = collision;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            insideC = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            insideT = true;
            mem2 = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            insideT = false;
        }
    }
}
