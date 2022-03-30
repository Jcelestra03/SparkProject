using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public GameObject cam;
    public SaveData save;
    public SaveData temp;

    private List<int> TileChash;
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
        Debug.Log(json);
    }

    public void Save()
    {
        TileChash = new List<int>();
        List<Vector3> num = cam.GetComponent<GridControl>().NumbersMason;

        for (int i = 0; i <= num.Count-1; i++)
        {
            //if (save.TileList.Contains(i) != null)
              //  save.TileList.Remove(i);

            cam.GetComponent<GridControl>().entail.TryGetValue(num[i], out int result);

            TileChash.Add(result);
        }

        save.TileList = TileChash;
        save.NumbersMason = num;
        string json = JsonUtility.ToJson(save);
    }

    public void Load()
    {
        cam.GetComponent<GridControl>().entail = new Dictionary<Vector3, int>();
        cam.GetComponent<GridControl>().entail.Clear();
        for (int i = 0; i < save.TileList.Count-1; i++)
        {
            cam.GetComponent<GridControl>().entail.TryAdd(save.NumbersMason[i], save.TileList[i]);
        }
        
        cam.GetComponent<GridControl>().NumbersMason = new List<Vector3>();
        cam.GetComponent<GridControl>().NumbersMason = save.NumbersMason;
    }

    public class SaveData
    {
        public List<int> TileList = new List<int>();
        public List<Vector3> NumbersMason = new List<Vector3>();
    }
}
