using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI : Selectable
{
    BaseEventData m_BaseEvent;
    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted(m_BaseEvent) == true)
        {
            GameObject.Find("Main Camera").GetComponent<GridControl>().editing = false;
        }
        else
            GameObject.Find("Main Camera").GetComponent<GridControl>().editing = true;
    }
}
