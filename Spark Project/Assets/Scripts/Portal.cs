using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject partner;
    public int color;
    public float wait;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();

        switch (color)
        {
            case 0:
                gameObject.name = "Portal(BLUE)";
                partner = GameObject.Find("Portal(RED)");
                sprite.color = Color.cyan;
                break;
            case 1:
                gameObject.name = "Portal(RED)";
                partner = GameObject.Find("Portal(BLUE)");
                sprite.color = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wait < 0)
        {
            collision.transform.position = partner.transform.position;
            partner.GetComponent<Portal>().wait = 0.5f;
        }
    }
}
