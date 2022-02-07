using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameObject.Find("gameManager").GetComponent<GameManager>().stars++;
            Destroy(gameObject);
        }
    }
}
