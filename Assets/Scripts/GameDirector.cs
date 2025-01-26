using ScriptableObjectArchitecture;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private LevelDataCollection levelsList;
    [SerializeField] private LevelDataGameEvent setupLevel;
    private LevelData actualLevel;
    private float maxTime = 10f;
    private float timer = 0;
    private bool startTimer = false;

    private void Awake()
    {

        gameStatus.Value.actualLevelIndex = PlayerPrefs.GetInt("actualLevelIndex", 0);
        gameStatus.Value.isGameOver = false;
        gameStatus.Value.points = 0;
        LevelData levelData = levelsList[gameStatus.Value.actualLevelIndex].Value;
        actualLevel = levelData;
        foreach (BubbleAmountData bubbleAmount in levelData.bubbles)
        {
            if (levelData.bubblesToWin.Contains(bubbleAmount.type))
            {
                gameStatus.Value.bubbleTotalToExplode += bubbleAmount.quantity;
            }
        }
        gameStatus.Value.levelRuleLabel = levelData.levelRuleLabel;
    }

    private void Start()
    {
        setupLevel.Raise(levelsList[gameStatus.Value.actualLevelIndex].Value);
    }

    private void Update()
    {
        if (startTimer && !gameStatus.Value.isGameOver)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                gameStatus.Value.isGameOver = true;
                bool isAWin = gameStatus.Value.points > actualLevel.pointsForVictory;
                if (isAWin)
                {
                    gameStatus.Value.gameOverLabel = actualLevel.levelWonLabel;
                    PlayerPrefs.SetInt("actualLevelIndex", gameStatus.Value.actualLevelIndex + 1);
                }
                else
                {
                    gameStatus.Value.gameOverLabel = actualLevel.levelLoseLabel;
                    PlayerPrefs.SetInt("actualLevelIndex", gameStatus.Value.actualLevelIndex);
                }

            }
        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void ApplyPoints(int points)
    {
        gameStatus.Value.points += points;
    }

}
