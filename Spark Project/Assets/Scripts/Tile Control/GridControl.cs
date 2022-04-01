using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class GridControl : MonoBehaviour
{

    //Calling Fields 

    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;
    gridManager grid;
    public PortalUI portalui;
    public GameObject mytxt;
    
    //       PORTAL VARIABLES 
    public Dictionary<int,Vector3> pushP; // (PORTAL ADD) reset
    //       PORTAL PARTNERING
    private bool Portalready;    // PREVENTS FROM PASSING PORTAL PARTNERING ONCE STARTED
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

    public GameObject[] tileprefabs; // LIBRARY OF TILES 
    public Dictionary<Vector3,int> entail;  // KEEPS TRACK OF TILE AND BLOCK TYPE 
    public List<Vector3> NumbersMason = new List<Vector3>(); // KEEPS TRACK OF DICTIONARY VECTOR3
    public List<Vector3> TheNumbers = new List<Vector3>();
    public int blockchange = 0; // THE PAINT TOOL TILE CHANGE
    private int count;      //FOR IF && WHILE LOOPS (NON LOCAL)
    private bool outof;     //PREVENTS BUILDING OUTSIDE OF GRID
    public bool editing;    //PREVENTS BUILDING WHILE IN BUILD
    public bool gamestart;  //PREVENTS BUILDING WHILE IN GAME

    //AUDIO
    public AudioClip ItemPicked;
    private AudioSource speaker;
    
    private void Start()
    {
        xpos = 0;
        ypos = 0;
        entail = new Dictionary<Vector3, int>();
        NumbersMason = new List<Vector3>();
        pushP = new Dictionary<int, Vector3>();
        gridUI = new Dictionary<Vector2Int,int >();
        editing = true;
    }
    public void Update()
    {
        if (gamestart == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameObject.Find("Main Camera").GetComponent<InvenController>().nope == false){ editing = true; }
                else { editing = false; }
            }
        }
        if (Portalready == false)
        {
            if (editing == true) 
            {
                if (gamestart == false) 
                {
                    if (Input.GetMouseButton(0))
                    {

                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);
                        Vector3 here = TargetTilemap.CellToWorld(clickPosition);
                        Vector3 outbounds;
                        outbounds = new Vector3(0, 0, 0);
                        if (clickPosition.x < 0 || clickPosition.x >= 100 || (clickPosition.y < 0 || clickPosition.y >= 100))
                            outof = true;
                        else outof = false;

                        if (!outof)
                        {
                            if (entail.TryAdd(here, blockchange) == true)
                            {
                                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                                NumbersMason.Add(here);

                                GetComponent<AudioSource>().clip = ItemPicked;
                                GetComponent<AudioSource>().Play();
                            }
                            else
                            {
                                entail[here] = blockchange;
                                //find index of NumbersMason(here/Vector3)
                                //get index push into NumberMason and TheNumbers
                                //delete NumbersMasonIndex 
                                //TheNumbers change index's int
                                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                            }
                            RemoveNumber();
                        }
                    }
                }
            }
            //
        }
    }
    //eraser 
    public void hardreset()
    {
        blockchange = 0;
    }
    public void hardClear()
    {
        int count = 0;
        while (count <= NumbersMason.Count-1)
        {
            Vector3Int clickPosition = TargetTilemap.WorldToCell(NumbersMason[count]);
            tiles.Set(clickPosition.x, clickPosition.y, 0);
        }
    }
    public void hardLoad(Vector3 here, int block)
    {
        Vector3Int clickPosition = TargetTilemap.WorldToCell(here);
        if (entail.TryAdd(here, block) == true)
        {
            Debug.Log("true");

            tiles.Set(clickPosition.x, clickPosition.y, block);
        }
        else
        {
            Debug.Log("tryadd false");
            entail[here] = block;
            tiles.Set(clickPosition.x, clickPosition.y, block);
        }
    }
    public void PortalCheck()
    {
        Portalreset();
        int count = 0;
        indexfinder = 0;
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);
            if(block == 16) //Portal Tile
            {
                pushP.TryAdd(indexfinder, NumbersMason[count]);
                indexfinder++;
            }
            count++;
        }
        
    }//checks for amount of portals, >>>portalAdd
    public void portalAdd()
    {
        //Vector2 positionOnGrid = new Vector2();
        //Vector2Int tileGet = new Vector2Int();
        if (xpos <= portalui.gridSizeWidth - 1 && ypos <= portalui.gridSizeHeight - 1)
        {
            xfine = true;
            yfine = false;
        }
        else if (xpos >= portalui.gridSizeWidth - 1 && ypos >= portalui.gridSizeHeight - 1)
        {
            Portalready = true;
            return;
        }
        else
        {
            xfine = false;
            yfine = true;
        }
        //
        if (xfine == true)
        {
            if (yfine == false)
            {
                if (ypos <= portalui.gridSizeHeight - 1)
                {
                    pushP.TryAdd(indexfinder, NumbersMason[count]);
                    ypos++;
                }
            }
        }
        //
        else
        {
            ypos = 0;
            xpos++;
            portalAdd();
        }
        indexfinder++;
    }//Adds portals to UIgrid and Lists and Dictionaries

    public void PP2() //portal partnering part 2
    {
        int count = 0;
        if(Partner1 == null) { return; }
        while (count <= Partner2.Count-1)
        {
            if (count != 0)
            {
                if (count != 1)
                    if (count % 2 != 0 && (Partner2.Count % 2) != 0) { return; }
            }
            GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().partnerName = Partner2[count];
            GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().color = 1;
            GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().partnerName = Partner1[count];
            GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().color = 2;
            count++;
        }
    }
    //Verison 2 of Simple portal
    public void PP4()
    {
        int count = 0;
        //partners are determined by index in lists
        while (count <= pushP.Count-1)
        {
            pushP.TryGetValue(count, out Vector3 highlight);
            if (p1 == false && p2 == false)
            {
                Partner1.Add(highlight);
                p1 = true;
            }
            else if (p1 == true && p2 == false)
            {
                Partner2.Add(highlight);
                p2 = true;
            }
            if (p1 == true && p2 == true)
            {
                p1 = false;
                p2 = false;
            }
            count++;
        }
    }    
    public void PartnerLines()
    {
        int count = 0;
        if(Partner1 == null) { return; }
        while (count <= Partner1.Count - 1)
        {
            if (count != 0 || count != 1)
            {
                if (count % 2 != 0 && Partner2.Count % 2 != 0) { return; }
            }
            portalui.linefinal(Partner1[count], Partner2[count]);
            count++;
        }
    }
    public void NumbersPortal(Vector3 here)
    {
        if (PortalNumber.Contains(here))
        {
            count--;
            int index = PortalNumber.IndexOf(here);
            PortalNumber.RemoveAt(index);
            Destroy(GameObject.Find(here.ToString()));
        }
        else
        {
            count++;
            TextMeshPro number;
            PortalNumber.Add(here);
            GameObject txt = Instantiate(mytxt);
            //instanciate
            txt.name = here.ToString();
            //rename to here
            txt.transform.position = here;
            //transform to here
            number = txt.GetComponent<TextMeshPro>();
            number.text = count.ToString();
        }
    }
    public void killnumbers()
    {
        int count = 0;
        while(count <= PortalNumber.Count-1)
        {
            Destroy(GameObject.Find(PortalNumber[count].ToString()));
            count++;
        }
        foreach(Vector3 child in PortalNumber)
            Destroy(GameObject.Find(child.ToString()));
    }
    private void Portalreset()
    {
        xpos = 0;
        ypos = 0;
        indexfinder = 0;
        pushP.Clear();
        Partner1.Clear();
        Partner2.Clear();
        p1 = false;
        p2 = false;
        foreach (Transform child in GameObject.Find("PortalGrid").transform)
            Destroy(child.gameObject);
    }

    // spawns all known tiles
    public void TileCheck()
    {
        int count = 0;
        int count2 = 1;
        RemoveNumber();
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);
            if(block != 16) //not Portal Tile
            {
                GameObject placed = Instantiate(tileprefabs[block]);
                Vector3 Posit;
                Posit = new Vector3(.5f, .5f, 0);
                placed.GetComponent<Transform>().position = NumbersMason[count] + Posit;
            }
            else
            {
                GameObject placed = Instantiate(tileprefabs[block]);
                TextMeshPro txt;
                txt = placed.GetComponentInChildren<TextMeshPro>();
                txt.text = count2.ToString();
                Vector3 Posit;
                Posit = new Vector3(.5f, .5f, 0);
                placed.GetComponent<Transform>().position = NumbersMason[count] + Posit;
                placed.name = (NumbersMason[count].ToString());
                count2++;
            }
            count++;
        }
    }

    private void RemoveNumber()
    {
        int count = 0;
        while(count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);
            if (block == 0)
            {
                entail.Remove(NumbersMason[count]);
                TheNumbers.Add(NumbersMason[count]);
            }
            count++;
        }
        count = 0;
        while(count <= TheNumbers.Count-1)
        {
            int index = NumbersMason.IndexOf(TheNumbers[count]);
            NumbersMason.RemoveAt(index);
            count++;
        }
        TheNumbers.Clear();
        //start loop 
        //check entail containing of vector3
        //check entail of vector3 of block 
        //if block == 0 , find index of vector3 NumbersMason
        //delete of index 
        //Count loop
    }

    //Self Destruction of prefabs
    public void editMode()
    {
        gamestart = false;
        editing = true;
    }
    public void startb()
    {
        PortalCheck();
        killnumbers();
        gamestart = true;
        editing = false;
        TileCheck(); 
        PP4();
        //here
        PP2();
    }
}
