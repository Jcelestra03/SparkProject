using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBlocks : MonoBehaviour
{
    public float fadeSpeed = 1;
    public float minFade = 0.3f;

    private float fadeSpeedMule = 1;
    private Color SRcolor;
    private bool visible;

    void Start()
    {
        SRcolor = gameObject.GetComponent<SpriteRenderer>().color;
        fadeSpeedMule = fadeSpeed;
    }

    void Update()
    {
        if (visible)
            if (fadeSpeedMule > minFade)
                fadeSpeedMule -= fadeSpeed * Time.deltaTime;

        if (!visible)
            if (fadeSpeedMule <= 1)
                fadeSpeedMule += fadeSpeed * Time.deltaTime; 

        SRcolor.a = fadeSpeedMule;
        gameObject.GetComponent<SpriteRenderer>().color = SRcolor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            visible = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            visible = false;
    }
}
