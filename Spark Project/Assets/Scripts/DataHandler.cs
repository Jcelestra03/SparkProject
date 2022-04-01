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
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        save = new SaveData();
        temp = new SaveData();
    }

    private void Update()
    {
        //Save();
        temp.NumbersMason = cam.GetComponent<GridControl>().NumbersMason;
        string json = JsonUtility.ToJson(temp);
        //Debug.Log(json);
    }

    public void Save()
    {
        TileChash = new List<int>();
        List<Vector3> num = cam.GetComponent<GridControl>().NumbersMason;
        save.NumbersMasonX.Clear();
        save.NumbersMasonY.Clear();
        for (int i = 0; i <= num.Count-1; i++)
        {
            //if (save.TileList.Contains(i) != null)
              //  save.TileList.Remove(i);

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
        cam.GetComponent<GridControl>().entail = new Dictionary<Vector3, int>();
        cam.GetComponent<GridControl>().entail.Clear();
        int count = 0;
        //cam.GetComponent<GridControl>().hardClear();
        while (count <= save.NumbersMasonX.Count-1)
        {
            newMason.Add(new Vector3(save.NumbersMasonX[count], save.NumbersMasonY[count], 0));
            if(save.NumbersMason == null) { return; }
            cam.GetComponent<GridControl>().hardLoad(newMason[count], save.TileList[count]);
            count++;
        }
        //for (int i = 0; i < save.TileList.Count-1; i++)
        //{
        //    cam.GetComponent<GridControl>().entail.TryAdd(save.NumbersMason[i], save.TileList[i]);
        //}
        
        cam.GetComponent<GridControl>().NumbersMason = new List<Vector3>();
        cam.GetComponent<GridControl>().NumbersMason.Clear();
        cam.GetComponent<GridControl>().NumbersMason = save.NumbersMason;
    }

    public class SaveData
    {
        public List<int> TileList = new List<int>();
        public List<Vector3> NumbersMason = new List<Vector3>();
        public List<int> NumbersMasonX = new List<int>();
        public List<int> NumbersMasonY = new List<int>();
    }
}
