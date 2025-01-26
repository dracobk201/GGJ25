using ScriptableObjectArchitecture;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private LevelDataCollection levelsList;
    [SerializeField] private LevelDataGameEvent setupLevel;
    private LevelData actualLevel;
    private float maxTime = 5f;
    private float timer = 0;
    private bool startTimer = false;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("actualLevelIndex", 0);
        bool isBeated = index >= levelsList.Count;
        int timesBeated = PlayerPrefs.GetInt("timesBeated", 0);
        if (isBeated)
        {
            timesBeated++;
            PlayerPrefs.SetInt("timesBeated", timesBeated);
        }
        gameStatus.Value.overwrittenSpeed = 1 + (0.25f * timesBeated);
        gameStatus.Value.actualLevelIndex = isBeated ? 0 : index;
        gameStatus.Value.isGameOver = false;
        gameStatus.Value.points = 0;
        gameStatus.Value.didYouWin = false;
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
        gameStatus.Value.neededPoints = levelData.pointsForVictory;
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
                    gameStatus.Value.didYouWin = true;
                    PlayerPrefs.SetInt("actualLevelIndex", gameStatus.Value.actualLevelIndex + 1);
                }
                else
                {
                    gameStatus.Value.gameOverLabel = actualLevel.levelLoseLabel;
                    gameStatus.Value.didYouWin = false;
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
