using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap TargetTilemap;
    [SerializeField] tilesmanager tiles;
    public PortalUI portalui;
    public AudioClip ItemPicked;
    private AudioSource speaker;
    private int blockchange = 0;
    private int count;
    //public TextMeshPro txt;
    public GameObject mytxt;
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
    List<Vector3> PortalNumber = new List<Vector3>();
////          ,^,           ,^,
/////         / \___________/ \
/////        ||               ||
//          //   *         *   \\
//         //         V         \\
//        //    ^ ^ ^ ^ ^ ^ ^    \\
//       //  ^ ^ ^ ^ ^ ^ ^ ^ ^ ^  \\
//                   cat
    gridManager grid;
    private bool partnering;
    

    private bool p1;
    private bool p2;
    private bool outof;
    public bool editing;
    public bool gamestart;
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
                if (GameObject.Find("Main Camera").GetComponent<InvenController>().nope == false)
                {
                    editing = true;
                }
                else { editing = false; }
            }
        }
        //else if(gamestart == false && GameObject.Find("Main Camera").GetComponent<InvenController>().nope == false) { editing = true; }
        if (Portalready == false)
        {
            //
            if (editing == true)
            {
                if (gamestart == false)
                {   
                    blockchange = GameObject.Find("Dropdown").GetComponent<dropblock>().Block;
                    if (Input.GetMouseButton(0))
                    {
                        this.gameObject.AddComponent<AudioSource>();
                        this.GetComponent<AudioSource>().clip = ItemPicked;
                        this.GetComponent<AudioSource>().Play();

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
                                    //spawn prefab on here / force name and text value; 
                                    //or put in function reference discord
                                    //NumbersPortal(here);
                                }
                                tiles.Set(clickPosition.x, clickPosition.y, blockchange);
                                NumbersMason.Add(here);
                            }
                            else
                            {
                                entail.TryGetValue(here, out int block);
                                if(block == 7)
                                {
                                    //NumbersPortal(here);
                                    //delete prefab by name
                                }
                                entail[here] = blockchange;
                                tiles.Set(clickPosition.x, clickPosition.y, blockchange);

                            }
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
    } // checks for when tiles are placed and updates lists and dictionaries 


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
        
    }//checks for amount of portals, >>>portalAdd
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

                    //portalui.ItemStats(xpos, ypos, indexfinder);
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
    }//Adds portals to UIgrid and Lists and Dictionaries
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
                PartnerLines();
                Debug.Log("Partner has been made");
                partnering = false;
                p1 = false;
                p2 = false;
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
    public void PP3()
    {
        int count = 0;
        int count2 = 1;
        while(count <= pushP.Count-1)
        {
            //find 
            pushP.TryGetValue(count, out Vector3 hey);
            pushP.TryGetValue(count2, out Vector3 listen);
            //if pushP last index is and odd number;
            //if(pushP.count%2 != 0)
            //if ( count2 >= pushP.count
            if(pushP.Count%2 != 0)
            {
                if(count2 >= pushP.Count) { return; }
            }
            GameObject.Find(hey.ToString()).GetComponent<Portal>().partnerName = listen;
            GameObject.Find(listen.ToString()).GetComponent<Portal>().partnerName = hey;
            GameObject.Find(hey.ToString()).GetComponent<Portal>().color = 1;
            GameObject.Find(listen.ToString()).GetComponent<Portal>().color = 2;
            count++;
            count++;
            count2++;
            count2++;
        }
    }
    public void PartnerLines()
    {
        int count = 0;
        while (count <= Partner1.Count - 1)
        {
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
            //delete PortalNumber.Index;
            PortalNumber.RemoveAt(index);
            Destroy(GameObject.Find(here.ToString()));
            //Delete GameObject 
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
            GameObject.Destroy(GameObject.Find(PortalNumber[count].ToString()));
            count++;
        }
        //foreach(Vector3 child in PortalNumber)
        //{
        //    GameObject.Destroy(GameObject.Find(child.ToString()));
        //}
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
        //foreach (Transform child in GameObject.Find("PortalGrid").transform)
        //{
          //  GameObject.Destroy(child.gameObject);
        //}
    } // on portal check button hard reset everything
    private void TileCheck()
    {
        int count = 0;
        int count2 = 1;
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
                TextMeshPro txt;
                txt = placed.GetComponentInChildren<TextMeshPro>();
                txt.text = count2.ToString();
                Vector3 Posit;
                Posit = new Vector3(.5f, .5f, 0);
                placed.GetComponent<Transform>().position = NumbersMason[count] + Posit;
                //name change
                placed.name = (NumbersMason[count].ToString());
                count2++;
            }
            count++;
        }
    } // spawns all known tiles
    
    public void editMode()
    {
        gamestart = false;
        editing = true;
        //self Destruction
    }
    public void startb()
    {
        PortalCheck();
        //killnumbers();
        gamestart = true;
        editing = false;
        //if Portalready == true
        TileCheck();
        //portal partner function 
        //PP2();
        PP3();
    }
}
