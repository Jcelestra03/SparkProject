using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private GameObject GM;

    private void Start()
    {
        GM = GameObject.Find("gameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            GM.GetComponent<GameManager>().win = true;
    }
}
