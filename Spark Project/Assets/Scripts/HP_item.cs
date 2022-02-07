using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_item : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().health += healAmount;
            Destroy(gameObject);
        }
    }
}
