using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DataHandler : MonoBehaviour
{
    public GameObject cam;
    public SaveData save;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        save = new SaveData();
    }

    public void Save()
    {
        save.entail = cam.GetComponent<GridControl>().entail;
        save.NumbersMason = cam.GetComponent<GridControl>().NumbersMason;
    }

    public void Load()
    {
        cam.GetComponent<GridControl>().entail = save.entail;
        cam.GetComponent<GridControl>().NumbersMason = save.NumbersMason;
    }

    public class SaveData
    {
        public Dictionary<Vector3, int> entail;
        public List<Vector3> NumbersMason;
    }
}
