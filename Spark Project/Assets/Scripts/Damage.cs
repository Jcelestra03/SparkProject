using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;
    public float attackRate = 1;
    public bool trigger = true;
    public bool solidCollider = true;

    private bool insideT;
    private bool insideC;
    private float coolDown;
    private Collision2D mem;
    private Collider2D mem2;

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= attackRate * Time.deltaTime;

        if (coolDown <= 0 && insideT)
        {
            mem2.transform.GetComponent<ExtraPlayerScript>().health -= damage;
            mem2.transform.GetComponent<ExtraPlayerScript>().damageDone = true;
            coolDown = 1f;
        }

        if (coolDown <= 0 && insideC)
        {
            mem.transform.GetComponent<ExtraPlayerScript>().health -= damage;
            mem.transform.GetComponent<ExtraPlayerScript>().damageDone = true;
            coolDown = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && solidCollider)
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
        if (collision.gameObject.tag == "Player" && trigger)
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
