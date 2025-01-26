using ScriptableObjectArchitecture;
using System.Collections;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private GameStatusReference gameStatus;
    [SerializeField] private Sprite[] bubbleOptions;
    [SerializeField] private SpriteRenderer bubbleSprite;
    [SerializeField] private IntGameEvent applyPoints;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioPops;
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
            audioSource.clip = audioPops[Random.Range(0, audioPops.Length - 1)];
            audioSource.volume = Random.Range(0.3f, 0.8f);
            audioSource.pitch = Random.Range(0.4f, 1f);
            audioSource.Play();

            applyPoints.Raise(bubbleData.pointsToGive);
            Invoke(nameof(DelayedDestroy), 0.1f);
        }
    }

    private void DelayedDestroy()
    {
        Destroy(gameObject);
    }
}