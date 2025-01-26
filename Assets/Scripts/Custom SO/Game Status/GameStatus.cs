
using System;

[Serializable]
public class GameStatus
{
    public bool isGameOver;
    public int bubbleTotalToExplode;
    public int points;
    public int actualLevelIndex;
    public string levelRuleLabel;
    public string gameOverLabel;
}
