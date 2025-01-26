using TMPro;
using UnityEngine;

public class GamePanelBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private TMP_Text ruleText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text prestigeText;
    private int timesBeated;

    private void Start()
    {
        timesBeated = PlayerPrefs.GetInt("timesBeated", 0);
    }

    private void Update()
    {
        if (!gameStatus.Value.isGameOver)
        {
            ruleText.text = gameStatus.Value.levelRuleLabel;
            prestigeText.text = timesBeated > 0 ? "Wait, again?" : "";
            pointsText.text = gameStatus.Value.points.ToString("D3");
            
        } else {
            if (gameCanvasGroup.alpha == 1)
            {
                gameCanvasGroup.alpha = 0;
                gameCanvasGroup.interactable = false;
                gameCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}
 