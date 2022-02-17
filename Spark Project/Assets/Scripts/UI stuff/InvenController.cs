using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenController : MonoBehaviour
{
    public PortalUI selectedItemGrid;
    public GridControl grid;
    public bool nope;

    private void Awake()
    {
        grid = GameObject.Find("Main Camera").GetComponent<GridControl>();
    }
    public void Update()
    {
        if(selectedItemGrid == null) { return; }
        if(Input.GetMouseButtonDown(0))
        {
            //grid.PP(selectedItemGrid.GetTileGridPosition(Input.mousePosition));  
        }
    }
}
