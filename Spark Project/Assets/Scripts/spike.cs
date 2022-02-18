using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    private AudioSource speaker;
    public AudioClip Spike_activated;

    // Object's velocity set to on impact.
    public float nockBack = 5;
    // Damage on impact.
    public int damage = 1;

    private float coolDown;

    private void Update()
    {
        // coolDown tiker.
        if (coolDown > 0)
            coolDown -= 1 * Time.deltaTime;
    }

    // If and object enters the spikes collider it is pushed in the direction it came from and damage it applied.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            Vector2 temp = collision.transform.GetComponent<Rigidbody2D>().velocity;

            temp = (-collision.relativeVelocity * nockBack * 1);

            collision.transform.GetComponent<Rigidbody2D>().velocity = temp;


            collision.transform.GetComponent<ExtraPlayerScript>().health -= damage;
            collision.transform.GetComponent<ExtraPlayerScript>().damageDone = true;
            coolDown = 0.1f;

            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().clip = Spike_activated;
            GetComponent<AudioSource>().Play();
        }
    }

    // If and object stays in the spikes collider it is pushed in the direction it came from and damage it applied.
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && coolDown <= 0)
        {
            Vector2 temp = collision.transform.GetComponent<Rigidbody2D>().velocity;

            temp = Vector2.up * nockBack;

            collision.transform.GetComponent<Rigidbody2D>().velocity = temp;

            collision.transform.GetComponent<ExtraPlayerScript>().health -= damage;
            collision.transform.GetComponent<ExtraPlayerScript>().damageDone = true;
            coolDown = 0.1f;
        }
    }
}
