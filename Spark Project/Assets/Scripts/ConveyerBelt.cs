using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public float conveyerSpeed = 9.0f;

    private SpriteRenderer SR;
    private void Start()
    {
        transform.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position * 1.5f, conveyerSpeed * Time.deltaTime);
    }
}
