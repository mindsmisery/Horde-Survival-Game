using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _gameOverPanelCG;
    [SerializeField] CanvasGroup _pausePanelCG;
    CanvasGroup _cg;

    private void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void ShowInGameUI()
    {
        _cg.alpha = 1.0f;
        _cg.interactable = true;
        _cg.blocksRaycasts = true;
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanelCG.alpha = 1;
        _gameOverPanelCG.interactable = true;
        _gameOverPanelCG.blocksRaycasts = true;
    }

    public void ShowPausePanel()
    {
        _pausePanelCG.alpha = 1;
        _pausePanelCG.interactable = true;
        _pausePanelCG.blocksRaycasts = true;
    }

    public void HidePausePanel()
    {
        _pausePanelCG.alpha = 0;
        _pausePanelCG.interactable = false;
        _pausePanelCG.blocksRaycasts = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        HidePausePanel();
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ResetGame();
    }

}
