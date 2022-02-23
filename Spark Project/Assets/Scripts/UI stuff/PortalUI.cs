using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortalUI : MonoBehaviour
{
    const float tileSizewidth = 130;
    const float tileSizeheight = 90;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;
    List<Vector3> Pa1 = new List<Vector3>(); //reset
    List<Vector3> Pa2 = new List<Vector3>(); //reset

    public int gridSizeWidth = 3;
    public int gridSizeHeight = 2;
    [SerializeField] GameObject line;
    [SerializeField] GameObject inventoryItemPrefab;
    private GameObject button;
    //
    public void Start()
    {
        //rectTransform = GetComponent<RectTransform>();
        //Init(gridSizeWidth, gridSizeHeight);

        
        //PlaceItem(inventoryitem, 1, 1);
    }
    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizewidth, height * tileSizeheight);
        rectTransform.sizeDelta = size;
    }
    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizewidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeheight);

        return tileGridPosition;
    }
    //

    public void ItemStats(int PosX, int PosY , int index)
    {
        InventoryItem inventoryitem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        TextMeshProUGUI txt;
        index++;
        txt = inventoryitem.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = index.ToString();
        PlaceItem(inventoryitem, PosX, PosY);
    }

    public void PlaceItem(InventoryItem inventoryitem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryitem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        inventoryItemSlot[posX, posY] = inventoryitem;


        Vector2 position = new Vector2();
        position.x = posX * 130 + 130 / 2;
        position.y = -(posY * 90 + 90 / 2);

        rectTransform.localPosition = position;
    }
    public void linefinal(Vector3 first, Vector3 second)
    {
        if(Pa1 != null)
        {
            if (Pa1.Contains(first)) { return; }
        }
        Pa1.Add(first);
        Pa2.Add(second);
        int index = Pa1.IndexOf(first);
        GameObject RenLine = Instantiate(line, transform);
        RenLine.GetComponent<LineRen>().PlacePoints(first, second);
    }
    public void ButtonRestart()
    {
        Pa1.Clear();
        Pa2.Clear();

        //button = GameObject.Find("Pfront");
        //button.GetComponent<Button>().interactable = false;
        //button.GetComponent<Button>().interactable = true;
    }
}
