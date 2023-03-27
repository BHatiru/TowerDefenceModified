using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private SpawnEnemySystem _spawnEnemySystem;
    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private Game _game;
    [SerializeField] private WaveSystem _waveSystem;

    private void Start()
    {
        Application.quitting += SaveGame;
    }

    public void SaveGame()
    {
        GameSaveInfo saveInfo = new GameSaveInfo();
        saveInfo.Towers = _buildSystem.GetSaveInfo();
        saveInfo.Enemies = _spawnEnemySystem.GetSaveInfo();
        saveInfo.MainBuildingHealth = _mainBuilding.Health;
        saveInfo.Balance = ResourceSystem.GameBalance;
        saveInfo.IsEasyGame = _game.IsEasyGame;
        saveInfo.CurrentWaveIndex = _waveSystem.CurrentWaveIndex;

        if (!Directory.Exists(GetSaveDirectory()))
        {
            Directory.CreateDirectory(GetSaveDirectory());
        }

        string serializedData = JsonUtility.ToJson(saveInfo);
        File.WriteAllText(GetSavePath(), serializedData);

    }

    public void LoadGame()
    {
        if(!File.Exists(GetSavePath())){
            UnityEngine.Debug.LogError("Can't find save file");
            return;
        }
        string serialData = File.ReadAllText(GetSavePath());
        GameSaveInfo savedInfo = JsonUtility.FromJson<GameSaveInfo>(serialData);
        if(savedInfo==null){
            UnityEngine.Debug.LogError("Can't deserialize save file");
            return;
        }
        _game.IsEasyGame = savedInfo.IsEasyGame;
        _buildSystem.LoadTowers(savedInfo.Towers);
        _spawnEnemySystem.LoadEnemies(savedInfo.Enemies);
        _mainBuilding.Initialize(savedInfo.MainBuildingHealth, _game.GetCurrentConfig().MainBuildingHealth);
        ResourceSystem.Initialize(savedInfo.Balance);
        _waveSystem.Initialize(_game.GetCurrentConfig().WaveData  ,savedInfo.CurrentWaveIndex);
    }

    private string GetSaveDirectory()
    {
        return Path.Combine(Application.dataPath, "SaveTD");
    }

    private string GetSavePath()
    {
        return Path.Combine(GetSaveDirectory(), "save.json");
    }
}

[System.Serializable]
public class TowerSaveInfo
{
    public TowerData.TowerType TowerType;
    public int CellId;
    public int Level;
}

[System.Serializable]
public class EnemySaveInfo
{
    public float Health;
    public Vector3 Position;
    public string Name;
}

[System.Serializable]
public class GameSaveInfo
{
    public List<TowerSaveInfo> Towers;
    public List<EnemySaveInfo> Enemies;
    public float MainBuildingHealth;
    public float Balance;
    public bool IsEasyGame;
    public int CurrentWaveIndex;
}