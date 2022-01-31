using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject partner;
    public int color;
    public List<object> storage;
    public List<object> localStorage;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();

        storage = new List<object>();
        localStorage = new List<object>();

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

    void Update()
    {
        if (partner == null)
        {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!storage.Contains(collision.gameObject))
        {
            partner.GetComponent<Portal>().storage.Add(collision.gameObject);
            storage.Add(collision.gameObject);
            localStorage.Add(collision.gameObject);

            if (collision != collision.isTrigger)
                collision.gameObject.transform.position = partner.transform.position;

            Debug.Log("teleport");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (storage.Contains(collision.gameObject) && !localStorage.Contains(collision.gameObject))
        {
            storage.Remove(collision.gameObject);
            partner.GetComponent<Portal>().storage.Remove(collision.gameObject);
            partner.GetComponent<Portal>().localStorage.Remove(collision.gameObject);
        }
    }
}
