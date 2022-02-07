using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{



    public void OnPointerEnter(PointerEventData evenData)
    {
        GameObject.Find("Main Camera").GetComponent<GridControl>().editing = false;
        GameObject.Find("Main Camera").GetComponent<InvenController>().nope = true;
    }
    public void OnPointerExit(PointerEventData evenData)
    {
        GameObject.Find("Main Camera").GetComponent<InvenController>().nope = false;
    }
    public void SetOff()
    {
        this.gameObject.SetActive(true);
    }
    public void SetON()
    {
        this.gameObject.SetActive(false);
    }
}
