using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;
    public float attackRecur = 1;

    public bool inside;

    public float coolDown;

    private void Update()
    {
        if (coolDown > 0)
            coolDown -= attackRecur * Time.deltaTime;

        //if (inside)
        //{
        //    Collision2D collision;
        //    collision.transform.GetComponent<PlayerController>().health -= damage;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inside = true;

        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inside = false;
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            collision.transform.GetComponent<PlayerController>().health -= damage;
            coolDown = 0.1f;
        }
    }
}
