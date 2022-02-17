using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalControl : MonoBehaviour
{
    gridManager grid;
    public PortalUI portalui;
    public GameObject mytxt;




    //       PORTAL VARIABLES 
    public Dictionary<int, Vector3> pushP; // (PORTAL ADD) reset
    //       PORTAL PARTNERING
    private bool Portalready;    // PREVENTS FROM PASSING PORTAL PARTNERING ONCE STARTED
    private bool partnering;    // (PP)
    private bool p1;    // (PP) WHEN PARTNER 1 IS ASSIGNED
    private bool p2;    // (PP) WHEN PARTNER 2 IS ASSIGNED
    private bool xfine; // (PORTAL ADD) WHEN PORTAL UI GRID COLUMN IS READY
    private bool yfine; // (PORTAL ADD) WHEN PORTAL UI GRID ROW IS READY
    private int xpos;   // (PORTAL ADD) TAKES THE PORTAL UI GRID VECTOR2 INTO SEPERATE INT
    private int ypos;   // (PORTAL ADD) TAKES THE PORTAL UI GRID VECTOR2 INTO SEPERATE INT
    private int indexfinder; // ANOTHER FORM OF COUNT 
    public List<Vector3> Partner1 = new List<Vector3>(); //(PP2) reset
    public List<Vector3> Partner2 = new List<Vector3>(); //(PP2) reset
    List<Vector3> PortalNumber = new List<Vector3>(); // (NUMBERS PORTAL) RESET?!?!?

    public Dictionary<Vector2Int, int> gridUI; //(PORTAL ADD) 
    List<int> IndexSave = new List<int>(); //reset

    private int count;


    /////////////////////////// Graveyard of dead portal mechanics 
    ////public void PP(Vector2Int position) // portal partner part 1
    ////{
    ////    int count = 0;
    ////    if (partnering == true) { count = 1; }
    ////    else { count = 0; }
    ////    //partners are determined by index in lists
    ////    if (gridUI.ContainsKey(position))
    ////    {
    ////        gridUI.TryGetValue(position, out int portal);
    ////        pushP.TryGetValue(portal, out Vector3 highlight);
    ////        partnering = true;
    ////        if (p1 == false && p2 == false)
    ////        {
    ////            Partner1.Add(highlight);
    ////            p1 = true;
    ////        }
    ////        else if (p1 == true && p2 == false)
    ////        {
    ////            Partner2.Add(highlight);
    ////            p2 = true;
    ////        }
    ////        if (p1 == true && p2 == true)
    ////        {
    ////            PartnerLines();
    ////            partnering = false;
    ////            p1 = false;
    ////            p2 = false;
    ////        }
    ////        count++;
    ////    }

    ////}
    ////public void PP2() //portal partnering part 2
    ////{
    ////    int count = 0;
    ////    if (Partner1 == null) { return; }
    ////    if (Partner1.Count == Partner2.Count)
    ////    {
    ////        while (count <= Partner1.Count - 1)
    ////        {
    ////            GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().partnerName = Partner2[count];
    ////            GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().color = 1;
    ////            GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().partnerName = Partner1[count];
    ////            GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().color = 2;
    ////            count++;
    ////        }
    ////    }
    ////}
}
