using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class dropblock : MonoBehaviour
{
    public int Block;
    public TMP_Dropdown myD;

    public List<GameObject> buttons = new List<GameObject>();
    private GameObject here;
    private void Start()
    {
        here = gameObject;
    }
    public void blockchange()
    {
        Block = myD.value;
    }
    
    public void tilechange()
    {
        if (buttons.Contains(here))
        {
            int index;
            index = buttons.IndexOf(here);
            GameObject.Find("Main Camera").GetComponent<GridControl>().blockchange = index;
        }        
    }
}
