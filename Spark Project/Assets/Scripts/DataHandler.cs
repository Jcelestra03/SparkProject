using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataHandler : MonoBehaviour
{
    public GameObject cam;
    public SaveData save;
    public SaveData temp;

    private List<int> TileChash;
    private List<Vector3> newMason = new List<Vector3>();

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        save = new SaveData();
        temp = new SaveData();
    }


    private void Update()
    {
        temp.NumbersMason = cam.GetComponent<GridControl>().NumbersMason;
        string json = JsonUtility.ToJson(temp);
    }

    public void Save()
    {
        TileChash = new List<int>();
        List<Vector3> num = cam.GetComponent<GridControl>().NumbersMason;
        save.NumbersMasonX.Clear();
        save.NumbersMasonY.Clear();
        for (int i = 0; i <= num.Count-1; i++)
        {
            cam.GetComponent<GridControl>().entail.TryGetValue(num[i], out int result);
            TileChash.Add(result);
        }
        save.TileList = TileChash;
        for(int i = 0; i <= num.Count-1; i++)
        {
            save.NumbersMasonX.Add(Mathf.FloorToInt(num[i].x));
            save.NumbersMasonY.Add(Mathf.FloorToInt(num[i].y));
        }
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
    }



    public void Load()
    {
        newMason.Clear();
        cam.GetComponent<GridControl>().resetLists();
        for (int i = 0; i <= save.NumbersMasonX.Count-1; i++)
        {
            newMason.Add(new Vector3(save.NumbersMasonX[i], save.NumbersMasonY[i], 0));
            if (save.NumbersMason == null) { return; }
            cam.GetComponent<GridControl>().hardLoad(newMason[i], save.TileList[i]);
            Debug.Log(save.TileList[i]);
        }
    }
    public class SaveData
    {
        public List<int> TileList = new List<int>();
        public List<Vector3> NumbersMason = new List<Vector3>();
        public List<int> NumbersMasonX = new List<int>();
        public List<int> NumbersMasonY = new List<int>();
    }

    
}
