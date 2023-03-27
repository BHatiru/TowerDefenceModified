using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const string KEY = "GameConfigOption";
    public const string LOAD_KEY = "LoadGame";

    public bool IsEasyGame;

    [Header("Configs")]
    [SerializeField] private GameConfigData _easyConfig;
    [SerializeField] private GameConfigData _hardConfig;

    [Header("References")]
    [SerializeField] private MainBuilding _mainBuildings;
    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private ReturnScreen _looseScreen;
    [SerializeField] private ReturnScreen _winScreen;
    [SerializeField] private ReturnScreen _pauseScreen;
    [SerializeField] private SaveLoadSystem _saveLoadSystem;

    public GameConfigData GetCurrentConfig()
    {
        return IsEasyGame ? _easyConfig: _hardConfig;
    }

    private void Start()
    {
        SceneEventSystem.Instance.GameLoose += OnGameLoose;
        SceneEventSystem.Instance.GameWin += OnGameWin;
        SceneEventSystem.Instance.GamePaused += OnGamePaused;
        SceneEventSystem.Instance.GamePaused += _saveLoadSystem.SaveGame;
        StartGame();

    }

   

    private void StartGame(){
        bool isNewGame = PlayerPrefs.GetInt(LOAD_KEY)==0;
        if(isNewGame){
                    IsEasyGame = PlayerPrefs.GetInt(KEY) == 0;

        _mainBuildings.Initialize(GetCurrentConfig().MainBuildingHealth, GetCurrentConfig().MainBuildingHealth);
        _waveSystem.Initialize(GetCurrentConfig().WaveData, 0);
        ResourceSystem.Initialize(GetCurrentConfig().StartBalance);
        }else{
            _saveLoadSystem.LoadGame();
        }
    }

    private void OnGameWin()
    {
        Time.timeScale = 0f;
        _winScreen.Show();
    }

    private void OnGameLoose()
    {
        Time.timeScale = 0f;
        _looseScreen.Show();
    }

    private void OnGamePaused()
    {
        _pauseScreen.Show();
    }
}
