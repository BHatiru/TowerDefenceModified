using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Button _resumeButton; 
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(ContinueGame);
        _exitButton.onClick.AddListener(ExitGame);
        
    }
    private void Start()
    {
        SceneEventSystem.Instance.GamePaused += PauseGame;
    }
    private void ExitGame()
    {
        Application.Quit();
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        _root.SetActive(false);
    }
    public void PauseGame(){
        Debug.Log("Game paused");
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
            SceneEventSystem.Instance.NotifyGamePaused();
        }
    }
}
