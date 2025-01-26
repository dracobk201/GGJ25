
using System;

[Serializable]
public class GameStatus
{
    public bool isGameOver;
    public bool didYouWin;
    public int bubbleTotalToExplode;
    public int points;
    public int neededPoints;
    public int actualLevelIndex;
    public float overwrittenSpeed;
    public string levelRuleLabel;
    public string gameOverLabel;
}
