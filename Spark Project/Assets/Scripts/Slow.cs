using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    [SerializeField] private float slowAmount = 5;
    private Vector2 velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            float drag = collision.gameObject.GetComponent<Rigidbody2D>().drag;
            drag += slowAmount;
            collision.gameObject.GetComponent<Rigidbody2D>().drag = drag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            float drag = collision.gameObject.GetComponent<Rigidbody2D>().drag;
            drag -= slowAmount;
            collision.gameObject.GetComponent<Rigidbody2D>().drag = drag;
        }
    }
}
