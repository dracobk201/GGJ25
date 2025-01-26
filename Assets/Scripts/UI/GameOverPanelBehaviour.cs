using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private TMP_Text gameOverText;

    private void Update()
    {
        if (gameStatus.Value.isGameOver && gameCanvasGroup.alpha == 0)
        {
            gameCanvasGroup.alpha = 1;
            gameCanvasGroup.interactable = true;
            gameCanvasGroup.blocksRaycasts = true;
            gameOverText.text = gameStatus.Value.gameOverLabel;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
