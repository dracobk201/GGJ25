using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using ScriptableObjectArchitecture;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private GameObject[] bubblePrefabs;
    
    [SerializeField] private LevelDataReference levelData;
    [SerializeField] private GameEvent noMoreBubbles;
    private List<BubbleObject> bubblesToSpawn;
    private Camera gameCamera;
    private float actualTime;
    private bool isCollectionEmpty = false;

    private void Awake()
    {
        gameCamera = Camera.main;
        actualTime = 0f;
    }

    private void Update()
    {
        if (levelData == null || gameStatus.Value.isGameOver) return;

        actualTime += Time.deltaTime * levelData.Value.timeSpeedFactor;
        if (actualTime > levelData.Value.spawnerTimer)
        {
            GenerateBubbles(levelData.Value.amountPerGeneration);
            actualTime = 0f;
        }
    }

    public void InitializeList(LevelData levelToUse)
    {
        levelData.Value = levelToUse;
        bubblesToSpawn = new List<BubbleObject>();
        foreach (BubbleAmountData bubbleAmount in levelData.Value.bubbles)
        {
            for (int i = 0; i < bubbleAmount.quantity; i++)
            {
                BubbleObject bubble = new BubbleObject();
                bubble.isSpawned = false;
                bubble.type = bubbleAmount.type;
                bubblesToSpawn.Add(bubble);
            }
        }
        bubblesToSpawn = bubblesToSpawn.OrderBy(_ => Guid.NewGuid()).ToList();
    }

    private void GenerateBubbles(int generationAmount = 1)
    {
        for (int i = 0; i < generationAmount; i++)
        {
            BubbleObject bubbleToSpawn = bubblesToSpawn.FirstOrDefault(b => !b.isSpawned);

            if (bubbleToSpawn == null && !isCollectionEmpty)
            {
                isCollectionEmpty = true;
                noMoreBubbles.Raise();
                return;
            }
            Vector3 randomPosition = GetRandomPositionOutsideScreen();

            int arrayIndex = bubbleToSpawn.type switch
            {
                BubbleTypeEnum.Type1 => 0,
                BubbleTypeEnum.Type2 => 1,
                BubbleTypeEnum.Type3 => 2,
                BubbleTypeEnum.Type4 => 3,
                _ => 0,
            };
            GameObject bubbles = Instantiate(bubblePrefabs[arrayIndex], randomPosition, Quaternion.identity, transform);
            bubbles.name = $"{bubblePrefabs[arrayIndex].name} {i}";

            bubbleToSpawn.isSpawned = true;

            BubbleData bubbleInfo = new BubbleData();
            bubbleInfo.BubbleLifespan = 10f;
            bubbleInfo.BubbleMovementSpeed = UnityEngine.Random.Range(0.001f, 0.3f);
            bubbleInfo.BubbleRotationSpeed = UnityEngine.Random.Range(3f, 50f);
            bubbleInfo.GivenDirection = GetRandomPositionInsideScreen();
            bubbleInfo.BubbleType = bubbleToSpawn.type;
            bubbleInfo.pointsToGive = levelData.Value.bubblesToWin.Contains(bubbleToSpawn.type) ? 
                levelData.Value.pointsToWin :
                -levelData.Value.pointsToLose;
            bubbleInfo.timeSpeedFactor = 1;

            int bubbleRotationOrientation;
            if (UnityEngine.Random.value > 0.5f)
            {
                bubbleRotationOrientation = 1;
            }
            else
            {
                bubbleRotationOrientation = -1;
            }
            bubbleInfo.BubbleOrientation = bubbleRotationOrientation;

            bubbles.GetComponent<BubbleBehaviour>().Setup(this, bubbleInfo);
        }
    }

    private Vector3 GetRandomPositionOutsideScreen()
    {
        Vector3 screenPosition = Vector3.zero;
        float margin = 20;

        if (UnityEngine.Random.value > 0.5f)
        {
            screenPosition.y = UnityEngine.Random.Range(-margin, Screen.height + margin);
            if (UnityEngine.Random.value > 0.5f)
            {
                screenPosition.x = Screen.width + margin;
            }
            else
            {
                screenPosition.x = -margin;
            }
        }
        else
        {
            screenPosition.x = UnityEngine.Random.Range(-margin, Screen.width + margin);
            if (UnityEngine.Random.value > 0.5f)
            {
                screenPosition.y = Screen.height + margin;
            }
            else
            {
                screenPosition.y = -margin;
            }
        }
        screenPosition.z = gameCamera.farClipPlane / 2;
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }

    private Vector3 GetRandomPositionInsideScreen()
    {
        Vector3 screenPosition = Vector3.zero;

        screenPosition.y = UnityEngine.Random.Range(0, Screen.height);
        screenPosition.x = UnityEngine.Random.Range(0, Screen.width);
        screenPosition.z = gameCamera.farClipPlane / 2;
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }
}