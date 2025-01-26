using System;
using UnityEngine;

[Serializable]
public class BubbleData
{
    public float BubbleLifespan;
    public float BubbleMovementSpeed;
    public float BubbleRotationSpeed;
    public float timeSpeedFactor;
    public int BubbleOrientation;
    public Vector3 GivenDirection;
    public BubbleTypeEnum BubbleType;
    public int pointsToGive;
}