using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private Sprite[] bubbleOptions;
    [SerializeField] private SpriteRenderer bubbleSprite;
    [SerializeField] private IntGameEvent applyPoints;
    [SerializeField] private AudioClip[] audioPops;
    [SerializeField] private AudioClipGameEvent soundToPlay;

    [SerializeField] private GameObject internalItem;
    private BubbleData bubbleData;
    public readonly float radio = 0.5f;

    private void Awake()
    {
        bubbleData = new BubbleData();
        bubbleData.BubbleLifespan = 10f;
        bubbleData.BubbleType = BubbleTypeEnum.Cake;
        bubbleData.BubbleRotationSpeed = Random.Range(3f, 50f);
        bubbleData.BubbleMovementSpeed = Random.Range(0.01f, 0.3f);
        bubbleData.timeSpeedFactor = 1;
        bubbleData.BubbleOrientation = Random.value > 0.5f ? 1 : -1;
        bubbleData.GivenDirection = new Vector3(0,5,0);
        bubbleData.pointsToGive = 1;
    }

    private void Start()
    {
        LeanTween.rotateAround(internalItem, Vector3.forward, 180f, 5f).setEase(LeanTweenType.easeInBack);
    }

    public void Setup(BubbleGenerator generator, BubbleData data)
    {
        bubbleData = data;
        bubbleData.GivenDirection = data.GivenDirection - transform.position;

        bubbleSprite.sprite = bubbleOptions[Random.Range(0, bubbleOptions.Length-1)];

        StopCoroutine(SelfDestroyBubble());
        StartCoroutine(SelfDestroyBubble());
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 movement = bubbleData.BubbleMovementSpeed * bubbleData.timeSpeedFactor * Time.deltaTime * bubbleData.GivenDirection;
        transform.position += movement;
    }

    private void Rotate()
    {
        Vector3 newRotation = new Vector3(0, 0, bubbleData.BubbleRotationSpeed * Time.deltaTime * bubbleData.BubbleOrientation * bubbleData.timeSpeedFactor);
        transform.Rotate(newRotation);
    }

    private IEnumerator SelfDestroyBubble()
    {
        yield return new WaitForSeconds(bubbleData.BubbleLifespan);
        DestroyBubble();
    }

    public void HandleImpact (Vector2 impactPosition)
    {
        if (gameStatus.Value.isGameOver) { return; }
        float distance = Vector2.Distance(transform.position, impactPosition);
        if (distance <= radio)
        {
            applyPoints.Raise(bubbleData.pointsToGive);
            Invoke(nameof(DestroyBubble), 0.01f);
        }
    }

    private void DestroyBubble()
    {
        soundToPlay.Raise(audioPops[Random.Range(0, audioPops.Length - 1)]);
        Destroy(gameObject);
    }
}