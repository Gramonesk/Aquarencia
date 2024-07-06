//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using FileData;
//public class DataPersistenceManager : MonoBehaviour
//{
//    [Header("File Storage Config")]
//    [SerializeField] private string fileName;
//    [SerializeField] private string JPGFolder;
//    [SerializeField] private bool useEncryption;

//    [Header("Texture Image Config")]
//    [SerializeField] Texture2D reference;
//    private GameData gameData;
//    private ImageData imgData;
//    private List<IImageReader> dataPersistenceImages;
//    private List<IDataPersistence> dataPersistenceObjects;
//    private FileDataHandler dataHandler;
//    public static DataPersistenceManager Instance { get; private set; }
//    private void Awake()
//    {
//        if(Instance != null)
//        {
//            Debug.LogError("more than 1 singleton");
//        }
//        Instance = this;
//    }
//    void Start()
//    {
//        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, JPGFolder,useEncryption, reference);
//        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
//        this.dataPersistenceImages = FindAllDataPersistenceImages();
//        LoadGame();
//    }
//    public void NewGame()
//    {
//        this.gameData = new GameData();
//        this.imgData = new ImageData();
//    }
//    public void LoadGame()
//    {
//        this.gameData = dataHandler.Load();
//        //Load Saved game from file data data
//        if(this.gameData == null)
//        {
//            Debug.Log("No data");
//            NewGame();
//        }
//        else
//        {
//            this.imgData = dataHandler.LoadData(gameData);
//        }
//        foreach(IDataPersistence dataPersistence in dataPersistenceObjects)
//        {
//            dataPersistence.LoadData(gameData);
//        }
//    }
//    public void SaveGame()
//    {
//        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
//        {
//            dataPersistence.SaveData(ref gameData);
//        }
//        foreach(IImageReader reader in dataPersistenceImages)
//        {
//            reader.SaveData(ref imgData);
//        }
//        dataHandler.Save(gameData);
//        dataHandler.SaveData(gameData, imgData);
//    }
//    private List<IDataPersistence> FindAllDataPersistenceObjects()
//    {
//        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().
//        OfType<IDataPersistence>();
//        return new List<IDataPersistence>(dataPersistenceObjects);
//    }
//    private List<IImageReader> FindAllDataPersistenceImages()
//    {
//        IEnumerable<IImageReader> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().
//        OfType<IImageReader>();
//        return new List<IImageReader>(dataPersistenceObjects);
//    }
//    private void OnApplicationQuit()
//    {
//        SaveGame();
//    }
//}
