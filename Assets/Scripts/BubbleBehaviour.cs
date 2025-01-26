using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private Sprite[] bubbleOptions;
    [SerializeField] private SpriteRenderer bubbleSprite;
    [SerializeField] private IntGameEvent applyPoints;
    private BubbleData bubbleData;
    public readonly float radio = 0.5f;

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
        Destroy(gameObject);
    }

    public void HandleImpact (Vector2 impactPosition)
    {
        if (gameStatus.Value.isGameOver) { return; }
        float distance = Vector2.Distance(transform.position, impactPosition);
        if (distance <= radio)
        {
            applyPoints.Raise(bubbleData.pointsToGive);
            Destroy(gameObject);
        }
    }
}