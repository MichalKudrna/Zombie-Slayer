using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;
    [SerializeField]private string fileName;
    private List<IDataPersistance> dataPersistanceObjects;
    public static DataPersistanceManager instance { get; private set; }
    private FileDataHandler dataHandler;
    private string profileID ="";
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, "save");
    }
    public void SaveGame()
    {   
        foreach (IDataPersistance dataPersistanceObj in FindAllDataPersistanceObjects())
        {
            dataPersistanceObj.SaveData(ref gameData);
        }
        gameData.cas = Player.cas;
        gameData.level = Player.currentLevel;
        gameData.diff = Player.difficulty;
        gameData.arena = Player.arena;
        gameData.hp = Player.hp;
        dataHandler.Save(gameData, profileID);
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load(profileID);
        foreach (IDataPersistance dataPersistanceObj in FindAllDataPersistanceObjects()) {
            dataPersistanceObj.LoadData(gameData);
        }
        Player.currentLevel = gameData.level;
        Player.difficulty = gameData.diff;
        Player.arena = gameData.arena;
        Player.hp = gameData.hp;
        Player.cas = gameData.cas;
    }
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        if(scene.name != "Menu")
            LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.profileID = newProfileId;
    }
}
