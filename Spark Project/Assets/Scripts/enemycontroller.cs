using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{

    private Rigidbody2D myRB;
    private Vector2 velocity;
    public float movementSpeed = 2;
    public bool isfollowing = false;
    public GameObject playertarget;







    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playertarget = GameObject.Find("player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;

        lookPos.Normalize();
        velocity = myRB.velocity;
        if (!isfollowing)
            velocity.x = 0;
        if (isfollowing)
        {
            velocity.x = lookPos.x * movementSpeed;

        }
        myRB.velocity = velocity;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isfollowing && (collision.gameObject.name == "player"))
            isfollowing = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (isfollowing && (collision.gameObject.name == "player"))
            isfollowing = false;

    }
}
