using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;
    

    public PortalUI portalui;

    private int blockchange;
    private int count; 
    

    public Dictionary<Vector3,int> entail;
    List<Vector3> NumbersMason = new List<Vector3>();
    public GameObject[] tileprefabs;
    // Partners // Vectors // Placement
    //public Dictionary<Vector3, Vector3> partners;// TURNED INTO 2 LISTS
    public Dictionary<int,Vector3> pushP; //reset
    public Dictionary<Vector2Int, int> gridUI;
    List<int> IndexSave = new List<int>(); //reset
    List<Vector3> Partner1 = new List<Vector3>(); //reset
    List<Vector3> Partner2 = new List<Vector3>(); //reset
////          ,^,           ,^,
/////         / \___________/ \
/////        ||               ||
//          //   *         *   \\
//         //         V         \\
//        //    ^ ^ ^ ^ ^ ^ ^    \\
//       //  ^ ^ ^ ^ ^ ^ ^ ^ ^ ^  \\
    gridManager grid;
    private bool partnering;
    private bool p1;
    private bool p2;
    private bool outof;


    public bool editing;

    private bool xfine;
    private bool yfine;
    private int xpos;
    private int ypos;
    private int indexfinder;
    private bool Portalready;
 

    private void Start()
    {
        xpos = 0;
        ypos = 0;
        blockchange = 2;
        entail = new Dictionary<Vector3, int>();
        NumbersMason = new List<Vector3>();
        pushP = new Dictionary<int, Vector3>();
        gridUI = new Dictionary<Vector2Int,int >();
        
        editing = true;
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (GameObject.Find("Main Camera").GetComponent<InvenController>().nope == false)
            {
                editing = true;
            }
        }
        if (Portalready == false)
        {


            //
            if (editing == true)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    blockchange = blockchange + 1;
                    if (blockchange >= tileprefabs.Length)
                    {
                        blockchange = 0;
                    }
                    Debug.Log(tileprefabs[blockchange]);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);
                    Vector3 here = TargetTilemap.CellToWorld(clickPosition);
                    Vector3 outbounds;
                    outbounds = new Vector3(0, 0, 0);

                    if (clickPosition.x < 0 || clickPosition.x >= 100 || (clickPosition.y < 0 || clickPosition.y >= 100))
                    {
                        outof = true;
                    }
                    else
                    {
                        outof = false;
                    }
                    if (!outof)
                    {
                        if (entail.TryAdd(here, blockchange) == true)
                        {
                            if (blockchange == 7)
                            {

                            }
                            tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                            NumbersMason.Add(here);
                        }
                        else
                        {
                            entail[here] = blockchange;
                            tiles.Set(clickPosition.x, clickPosition.y, blockchange);

                        }
                    }

                }
            }
            //
        }
        //dropper(copy)
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = TargetTilemap.WorldToCell(worldPoint);

            Vector3 here = TargetTilemap.CellToWorld(clickPosition);

            entail.TryGetValue(here, out int block);
            blockchange = block;
            Debug.Log(tileprefabs[blockchange]);
        }
    }


    public void PortalCheck()
    {
        Portalreset();
        count = 0;
        indexfinder = 0;
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);
            if(block == 7)
            {
                //placement on UI//
                portalAdd();
                
            }
            count++;
        }
        
    }
    public void portalAdd()
    {
        Vector2 positionOnGrid = new Vector2();
        Vector2Int tileGet = new Vector2Int();
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
                    IndexSave.Add(count);
                    //UI
                    portalui.ItemStats(xpos, ypos);
                    positionOnGrid.x = xpos;
                    positionOnGrid.y = ypos;
                    tileGet.x = xpos;
                    tileGet.y = ypos;
                    gridUI.TryAdd(tileGet, indexfinder);
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
    }
    public void PP(Vector2Int position) // portal partner part 1
    {
        int count = 0;
        if(partnering == true) { count = 1; }
        else { count = 0; }
        //partners are determined by index in lists
        if(gridUI.ContainsKey(position))
        {
            gridUI.TryGetValue(position, out int portal);
            pushP.TryGetValue(portal, out Vector3 highlight);
            partnering = true;
            if (p1 == false && p2 == false)
            {
                Debug.Log(portal);
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
                
                Debug.Log("Partner has been made");
                partnering = false;
                p1 = false;
                p2 = false;
            }
            count++;
        }
        
    }
    private void Portalreset()
    {
        xpos = 0;
        ypos = 0;
        indexfinder = 0;
        pushP.Clear();
        IndexSave.Clear();
        Partner1.Clear();
        Partner2.Clear();
        p1 = false;
        p2 = false;
        foreach (Transform child in GameObject.Find("PortalGrid").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


    private void TileCheck()
    {
        int count = 0;
        while (count <= NumbersMason.Count-1)
        {
            entail.TryGetValue(NumbersMason[count], out int block);

            if(block != 7)
            {
                GameObject placed = Instantiate(tileprefabs[block]);
                Vector3 Posit;
                Posit = new Vector3(.5f, .5f, 0);
                placed.GetComponent<Transform>().position = NumbersMason[count] + Posit;
            }
            else
            {
                GameObject placed = Instantiate(tileprefabs[block]);
                Vector3 Posit;
                Posit = new Vector3(.5f, .5f, 0);
                placed.GetComponent<Transform>().position = NumbersMason[count] + Posit;
                //name change
                placed.name = (NumbersMason[count].ToString());
            }
            count++;
        }
    }
    public void PP2() //portal partnering part 2
    {
        int count = 0;
        if(Partner1 == null) { return; }
        if(Partner1.Count == Partner2.Count)
        {
            while(count <= Partner1.Count-1)
            {
                GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().partnerName = Partner2[count];
                GameObject.Find(Partner1[count].ToString()).GetComponent<Portal>().color = 1;
                GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().partnerName = Partner1[count];
                GameObject.Find(Partner2[count].ToString()).GetComponent<Portal>().color = 2;
                count++;
            }
        }
    }
    public void editMode()
    {
        editing = true;
        //self Destruction
    }
    public void startb()
    {
        editing = false;
        //if Portalready == true
        TileCheck();
        //portal partner function 
        PP2();
    }

}
