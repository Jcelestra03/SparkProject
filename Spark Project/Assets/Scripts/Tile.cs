using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public Color basedColor, offsetcolor;
    //public SpriteRenderer renderer;
    public GameObject hightlight;
    public GameObject spawner;
    public GameObject spawning;



    private void Start()
    {
        spawning = GameObject.Find("TileManager").GetComponent<TileManager>().enemy;
    }
    //public void Init(bool isOffset)
    //{
    //renderer.color = isOffset ? offsetcolor : basedColor;
    //}


    private void OnMouseEnter()
    {
        hightlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        hightlight.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject oj = Instantiate(spawning, gameObject.transform);
            oj.GetComponent<Transform>().position = spawner.transform.position;
        }
    }
}
