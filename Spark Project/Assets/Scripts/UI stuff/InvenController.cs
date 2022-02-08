using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenController : MonoBehaviour
{
    public PortalUI selectedItemGrid;
    public GridControl Hub;
    public bool nope; 


    public void Update()
    {
        if(selectedItemGrid == null) { return; }
        if(Input.GetMouseButtonDown(0))
        {
            Hub.PP(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
            
        }
    }
}
