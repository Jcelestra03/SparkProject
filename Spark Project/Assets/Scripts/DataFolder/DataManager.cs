using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataManager : MonoBehaviour
{
    private GameData gameData;

    private List<DataPersistence> dataObjects;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.dataObjects = FindAllDataObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if(this.gameData == null)
        {
            NewGame();
        }

        foreach(DataPersistence dataPersistenceObj in dataObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (DataPersistence dataPersistenceObj in dataObjects)
        {
            dataPersistenceObj.Saved(ref gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<DataPersistence> FindAllDataObjects()
    {
        IEnumerable<DataPersistence> dataObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<DataPersistence>();
        return new List<DataPersistence>(dataObjects);
    }
}
