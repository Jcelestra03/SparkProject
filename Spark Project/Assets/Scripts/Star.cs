using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private AudioSource speaker;
    public AudioClip StarCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GetComponent<AudioSource>().clip = StarCollected;
            GetComponent<AudioSource>().Play();

            GameObject.Find("gameManager").GetComponent<GameManager>().stars++;
            Destroy(gameObject);
        }
    }
}
