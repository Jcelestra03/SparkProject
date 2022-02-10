using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 OGpos;
    private Vector3 Newpos;
    private RectTransform rectTransform;
    GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("gameManager").GetComponent<GameManager>();
        Newpos = GameObject.Find("gameManager").GetComponent<Transform>().position;
        rectTransform = this.GetComponent<RectTransform>();
        OGpos = rectTransform.position;
        gamemanager.UIlist(this.gameObject);
    }
    public void UIOFF()
    {
        if (this.gameObject.name.Contains("Edit")) { return; }
        rectTransform.position = Newpos;
    }

    public void UION()
    {
        if (this.gameObject.name.Contains("Edit")) { return; }
        rectTransform.position = OGpos;
    }
    public void OnPointerEnter(PointerEventData evenData)
    {
        GameObject.Find("Main Camera").GetComponent<GridControl>().editing = false;
        GameObject.Find("Main Camera").GetComponent<InvenController>().nope = true;
    }
    public void OnPointerExit(PointerEventData evenData)
    {
        GameObject.Find("Main Camera").GetComponent<GridControl>().editing = true;
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
