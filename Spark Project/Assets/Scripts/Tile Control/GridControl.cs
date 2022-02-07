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
    public Dictionary<Vector3, Vector3> partners;
    public Dictionary<Vector3,int> pushP; //clear
    //public Dictionary<int, int> gridUI; // later turn into 2 Lists;?
    List<int> UIx = new List<int>(); //clear
    List<int> UIy = new List<int>(); //clear
    List<int> IndexSave = new List<int>(); //clear
////          ,^,           ,^,
/////         / \___________/ \
/////        ||               ||
//          //   *         *   \\
//         //         V         \\
//        //    ^ ^ ^ ^ ^ ^ ^    \\
//       //  ^ ^ ^ ^ ^ ^ ^ ^ ^ ^  \\
    gridManager grid;   
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
        partners = new Dictionary<Vector3, Vector3>();
        pushP = new Dictionary<Vector3, int>();
        
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
                            //if(2 portals exist) - UI menu(portal) is avalible;
                            
                            // list or dictoary place key, leave value open/null;

                            //Force camera to spot World to cell 
                            //UI choose partner 
                            //dictionary.portal-ADD(here, int"partner")
                        }
                        tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                        NumbersMason.Add(here);
                    }
                    else
                    {
                        entail[here] = blockchange;
                        tiles.Set(clickPosition.x, clickPosition.y, blockchange);

                    }
                    //portalcheck();
                }

            }
        }
        //
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
        if ( Input.GetKeyDown(KeyCode.T))
        {
            //portalcheck();
            startb();
        }
    }

    public void PortalCheck()
    {
        Portalreset();
        count = 0;
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
        indexfinder = 0;
    }
    public void portalAdd()
    {
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
        if (xfine == true)
        {
            if (yfine == false)
            {
                if (ypos <= portalui.gridSizeHeight - 1)
                {
                    pushP.TryAdd(NumbersMason[count], indexfinder);
                    IndexSave.Add(count);
                    UIx.Add(xpos);
                    UIy.Add(ypos);
                    portalui.ItemStats(xpos, ypos);
                    Debug.Log(xpos);
                    Debug.Log(ypos);
                    ypos++;
                }
            }
        }
        else
        {
            ypos = 0;
            xpos++;
            portalAdd();
        }
        indexfinder++;
    }

    private void Portalreset()
    {
        xpos = 0;
        ypos = 0;
        pushP.Clear();
        UIx.Clear();
        UIy.Clear();
        IndexSave.Clear();
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
            count++;
        }
    }

    public void startb()
    {
        TileCheck();
        if (editing == false)
        {
            editing = true;
            //Self Destruction 
        }
        else
        {
            editing = false;
        }
    }

}
