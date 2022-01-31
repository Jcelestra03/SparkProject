using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PortalUI))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InvenController inventoryController;
    PortalUI itemGrid;

    public void OnPointerEnter(PointerEventData evenData)
    {
        inventoryController.selectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        inventoryController.selectedItemGrid = null;
    }

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InvenController)) as InvenController;
        itemGrid = GetComponent<PortalUI>();
    }
}
