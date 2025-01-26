using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MainMenuBubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;

    private List<BubbleObject> bubblesToSpawn;
    private Camera gameCamera;
    private float maxTime = 1f;
    private float actualTime;
    private bool isCollectionEmpty = false;

    private void Awake()
    {
        gameCamera = Camera.main;
        actualTime = 0f;
    }

    private void Update()
    {

        actualTime += Time.deltaTime;
        if (actualTime > maxTime)
        {
            GenerateBubbles(UnityEngine.Random.Range(1,10));
            actualTime = 0f;
        }
    }

    private void GenerateBubbles(int generationAmount = 1)
    {
        for (int i = 0; i < generationAmount; i++)
        {
            Vector3 randomPosition = GetRandomPositionOutsideScreen();

            GameObject bubbles = Instantiate(bubblePrefab, randomPosition, Quaternion.identity, transform);
            bubbles.name = $"{bubblePrefab.name} {i}";


            BubbleData bubbleInfo = new BubbleData();
            bubbleInfo.BubbleLifespan = 10f;
            bubbleInfo.BubbleMovementSpeed = UnityEngine.Random.Range(0.01f, 1f);
            bubbleInfo.BubbleRotationSpeed = UnityEngine.Random.Range(3f, 70f);
            bubbleInfo.GivenDirection = GetRandomPositionInsideScreen();
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

            bubbles.GetComponent<MainMenuBubbleBehaviour>().Setup(bubbleInfo);
        }
    }

    private Vector3 GetRandomPositionOutsideScreen()
    {
        Vector3 screenPosition = Vector3.zero;
        float margin = 60f;

        // Choose a random side of the screen
        if (UnityEngine.Random.value > 0.5f)
        {
            screenPosition.x = UnityEngine.Random.Range(-margin, Screen.width + margin);
        }
        else
        {
            screenPosition.x = UnityEngine.Random.value > 0.5f ? Screen.width + margin : -margin;
        }

        // Set the depth based on the camera's far clip plane
        screenPosition.z = gameCamera.farClipPlane / 2;

        // Convert screen position to world position
        return gameCamera.ScreenToWorldPoint(screenPosition);
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
