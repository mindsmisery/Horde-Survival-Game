using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _mainMenuButtonsCG;
    [SerializeField] CanvasGroup _quitConfirmationCG;
    [SerializeField] InGameUIManager _inGameUIManager;
    CanvasGroup _mainMenuCG;
    [SerializeField] CanvasGroup _settingsMenuCG;

    void Awake()
    {
        _mainMenuCG = GetComponent<CanvasGroup>();
        OpenMainMenu();
    }

    void CanvasGroupSetState(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1.0f : 0.0f;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }
    public void Play()
    {
        CloseMainMenu();
        GameManager.Instance.StartGame();
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpenQuitConfirmation()
    {
        CanvasGroupSetState(_mainMenuButtonsCG, false);
        CanvasGroupSetState(_quitConfirmationCG, true);
    }

    public void CloseQuitConfirmation()
    {
        CanvasGroupSetState(_mainMenuButtonsCG, true);
        CanvasGroupSetState(_quitConfirmationCG, false);
    }

    public void OpenMainMenu()
    {
        CanvasGroupSetState(_mainMenuCG, true);
    }
    public void CloseMainMenu()
    {
        CanvasGroupSetState(_mainMenuCG, false);
    }

    public void SettingsMenuToggleMain(bool open)
    {
        CanvasGroupSetState(_mainMenuButtonsCG, !open);
        CanvasGroupSetState(_settingsMenuCG, open);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _inGameUIManager.ShowGameOverPanel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _inGameUIManager.ShowPausePanel();
    }
}
