using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject partner;
    public int color;
    public List<object> storage;
    public List<object> localStorage;
    public AudioClip teleportation;
    public Vector3 partnerName;
    public bool partnerbool;

    private SpriteRenderer sprite;
    private AudioSource speaker;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        partnerbool = false;
        storage = new List<object>();
        localStorage = new List<object>();

        //switch (color)
        //{
        //    case 0:
        //        gameObject.name = "Portal(BLUE)";
        //        partner = GameObject.Find("Portal(RED)");
        //        sprite.color = Color.cyan;
        //        break;
        //    case 1:
        //        gameObject.name = "Portal(RED)";
        //        partner = GameObject.Find("Portal(BLUE)");
        //        sprite.color = Color.red;
        //        break;
        //}
    }

    void Update()
    {
        //if (partner == null)
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            gameObject.name = "Portal(BLUE)";
        //            partner = GameObject.Find("Portal(RED)");
        //            sprite.color = Color.cyan;
        //            break;
        //        case 1:
        //            gameObject.name = "Portal(RED)";
        //            partner = GameObject.Find("Portal(BLUE)");
        //            sprite.color = Color.red;
        //            break;
        //    }
        //}
        if (partnerbool == false)
        {
            if (partnerName == null) { return; }
            else { partner = GameObject.Find(partnerName.ToString()); partnerbool = true; }
        }
        else 
        {
            if (color == 0) { return; }
            else
            {
                if (partner != null)
                {
                    switch (color)
                    {
                        case 1:
                            gameObject.name = "Portal(BLUE)";
                            sprite.color = Color.cyan;
                            break;
                        case 2:
                            gameObject.name = "Portal(RED)";
                            sprite.color = Color.red;
                            break;
                    }
                }
            }
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(partner == null) { return; }
        if (!storage.Contains(collision.gameObject))
        {
            partner.GetComponent<Portal>().storage.Add(collision.gameObject);
            storage.Add(collision.gameObject);
            localStorage.Add(collision.gameObject);

            if (collision != collision.isTrigger)
                collision.gameObject.transform.position = partner.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (partner == null) { return; }
        if (storage.Contains(collision.gameObject) && !localStorage.Contains(collision.gameObject))
        {
            storage.Remove(collision.gameObject);
            partner.GetComponent<Portal>().storage.Remove(collision.gameObject);
            partner.GetComponent<Portal>().localStorage.Remove(collision.gameObject);
        }
    }
}
