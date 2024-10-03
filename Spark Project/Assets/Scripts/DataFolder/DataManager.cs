using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<DataPersistence> dataObjects;
    private FileDataHandler dataHandler;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataObjects = FindAllDataObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if(gameData == null)
        {
            NewGame();
            // get entail keys add to NumbersMason 
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
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        
    }

    private List<DataPersistence> FindAllDataObjects()
    {
        IEnumerable<DataPersistence> dataObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<DataPersistence>();
        return new List<DataPersistence>(dataObjects);
    }
}
