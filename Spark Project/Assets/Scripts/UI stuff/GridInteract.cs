using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PortalUI))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip Block_placed;
    private AudioSource speaker;

    InvenController inventoryController;
    PortalUI itemGrid;

    public void OnPointerEnter(PointerEventData evenData)
    {
        GetComponent<AudioSource>().clip = Block_placed;
        GetComponent<AudioSource>().Play();

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
