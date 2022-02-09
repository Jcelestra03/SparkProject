using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text starText;
    public bool win;
    public bool lose;
    public int stars = 0;
    List<GameObject> uIs = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void AddPoint()
    {
        stars += 1;
        starText.text = stars.ToString() + " Stars:";
    }
    //UI script sends their gameobject name 
    //then gets added to a list 
    // upon moveUI find all instances of list.getcomponent.UI.uiOFF or uiON
    public void UIlist(GameObject ui){ uIs.Add(ui); Debug.Log(ui); }

    public void moveUI()
    {
        int count = 0;
        GameObject local;
        while (count <= uIs.Count - 1)
        {
            local = GameObject.Find(uIs[count].ToString());
            Debug.Log(local);
            local.GetComponent<UI>().UIOFF();
            count++;
        }
        //during Go/Start move everything but Edit 
        //then move health and star counter into canvas
    }
    public void backUI()
    {
        int count = 0;
        GameObject local;
        while (count <= uIs.Count - 1)
        {
            local = GameObject.Find(uIs[count].ToString());
            local.GetComponent<UI>().UION();
            count++;
        }
        //during Go/Start move everything but Edit 
        //then move health and star counter into canvas
    }
}
