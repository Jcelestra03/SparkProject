using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public Color basedColor, offsetcolor;
    //public SpriteRenderer renderer;
    public GameObject hightlight;


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
}
