using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public float spawnerTimer;
    public float timeSpeedFactor;
    public int amountPerGeneration;
    public List<BubbleAmountData> bubbles;
    public int pointsToWin;
    public List<BubbleTypeEnum> bubblesToWin;
    public int pointsToLose;
    public string levelRuleLabel;
    public string levelWonLabel;
    public string levelLoseLabel;
    public int pointsForVictory;

}
