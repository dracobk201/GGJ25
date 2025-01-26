using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private TMP_Text gameOverTitleText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text neededPointsText;
    [SerializeField] private TMP_Text RestartButtonText;

    private void Update()
    {
        if (gameStatus.Value.isGameOver && gameCanvasGroup.alpha == 0)
        {
            gameCanvasGroup.alpha = 1;
            gameCanvasGroup.interactable = true;
            gameCanvasGroup.blocksRaycasts = true;
            gameOverText.text = gameStatus.Value.gameOverLabel;
            gameOverTitleText.text = gameStatus.Value.didYouWin ? "Win!" : "Game Over";
            RestartButtonText.text = gameStatus.Value.didYouWin ? "Let's Go" : "Restart Level";
            neededPointsText.text = !gameStatus.Value.didYouWin ? $"You need {gameStatus.Value.neededPoints} points" : "";
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
