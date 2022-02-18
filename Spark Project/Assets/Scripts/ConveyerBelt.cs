using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public float conveyerSpeed = 9.0f;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.name.Contains("left"))
            collision.GetComponent<Rigidbody2D>().velocity -= new Vector2(conveyerSpeed, 0);
        else
            collision.GetComponent<Rigidbody2D>().velocity += new Vector2(conveyerSpeed, 0);
    }
}
