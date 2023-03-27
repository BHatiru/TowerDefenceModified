using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _easyGameButton;
    [SerializeField] private Button _hardGameButton;
    [SerializeField] private Button _exitGameButton;

    private void Awake()
    {
        CheckSavedValues();
        _easyGameButton.onClick.AddListener(SetEasyGame);
        _hardGameButton.onClick.AddListener(SetHardGame);
        _startGameButton.onClick.AddListener(StartGame);
        _loadGameButton.onClick.AddListener(LoadGame);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void LoadGame()
    {
        PlayerPrefs.SetInt(Game.LOAD_KEY, 1);
        SceneManager.LoadScene(1);
    }

    private void CheckSavedValues()
    {
        if (PlayerPrefs.HasKey(Game.KEY))
        {
            if (PlayerPrefs.GetInt(Game.KEY) == 0)
                SetEasyGame();
            else
                SetHardGame();
        }
        else
        {
            SetEasyGame();
        }
    }

    private void SetHardGame()
    {
        PlayerPrefs.SetInt(Game.KEY, 1);
        _easyGameButton.image.color = Color.white;
        _hardGameButton.image.color = Color.cyan;
    }

    private void SetEasyGame()
    {
        PlayerPrefs.SetInt(Game.KEY, 0);
        _hardGameButton.image.color = Color.white;
        _easyGameButton.image.color = Color.cyan;
    }

    private void StartGame()
    {
        PlayerPrefs.SetInt(Game.LOAD_KEY, 0);
        SceneManager.LoadScene(1);
    }
}
