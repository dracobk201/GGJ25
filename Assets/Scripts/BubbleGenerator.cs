using ScriptableObjectArchitecture;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameEvent bubbleHit;
    [SerializeField] private GameObject[] bubblePrefabs;
    [SerializeField] private LevelDataReference levelData;
    private Camera gameCamera;
    private float actualTime;

    private void Awake()
    {
        gameCamera = Camera.main;
        actualTime = 0f;
    }

    private void Update()
    {
        actualTime += Time.deltaTime * levelData.Value.timeSpeedFactor;
        if (actualTime > levelData.Value.spawnerTimer)
        {
            GenerateBubbles(levelData.Value.amountPerGeneration);
            actualTime = 0f;
        }
    }

    private void GenerateBubbles(int generationAmount = 1)
    {
        //TODO: Make this with a Object Pool and swappable sprites
        for (int i = 0; i < generationAmount; i++)
        {
            int randomIndex = Random.Range(0, bubblePrefabs.Length);
            Vector3 randomPosition = GetRandomPositionOutsideScreen();
            GameObject bubbles = Instantiate(bubblePrefabs[randomIndex], randomPosition, Quaternion.identity, transform);
            bubbles.name = $"{bubblePrefabs[randomIndex].name} {i}";

            BubbleData bubbleInfo = new BubbleData();
            bubbleInfo.BubbleLifespan = 10f;
            bubbleInfo.BubbleMovementSpeed = Random.Range(0.00001f, 0.5f);
            bubbleInfo.BubbleRotationSpeed = Random.Range(3f, 50f);
            bubbleInfo.GivenDirection = GetRandomPositionInsideScreen();

            int bubbleRotationOrientation;
            if (Random.value > 0.5f)
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

        if (Random.value > 0.5f)
        {
            screenPosition.y = Random.Range(-margin, Screen.height + margin);
            if (Random.value > 0.5f)
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
            screenPosition.x = Random.Range(-margin, Screen.width + margin);
            if (Random.value > 0.5f)
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

        screenPosition.y = Random.Range(0, Screen.height);
        screenPosition.x = Random.Range(0, Screen.width);
        screenPosition.z = gameCamera.farClipPlane / 2;
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }
}