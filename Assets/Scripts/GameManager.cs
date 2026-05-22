using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] InGameUIManager _inGameUIManager;
    [SerializeField] MainMenuManager _mainMenuManager;
    bool _isGamePaused;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        Time.timeScale = 0f; // Pauses game when it is launched
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _inGameUIManager.ShowGameOverPanel();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        _inGameUIManager.ShowInGameUI();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                _inGameUIManager.ResumeGame();
                _isGamePaused = false;
            } else
            {
                _mainMenuManager.PauseGame();
                _isGamePaused = true;
            }
        }
    }
}
