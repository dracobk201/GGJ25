using System.Collections;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private int damageToGive;
    [SerializeField] private int pointsToGive;
    private BubbleData bubbleData;
    public float timeSpeedFactor;
    public readonly float radio = 0.5f;

    public void Setup(BubbleGenerator generator, BubbleData data)
    {
        bubbleData = new BubbleData();
        bubbleData.BubbleLifespan = data.BubbleLifespan;
        bubbleData.BubbleMovementSpeed = data.BubbleMovementSpeed;
        bubbleData.BubbleRotationSpeed = data.BubbleRotationSpeed;
        bubbleData.GivenDirection = data.GivenDirection - transform.position;
        bubbleData.BubbleOrientation = data.BubbleOrientation;

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
        Vector3 movement = bubbleData.BubbleMovementSpeed * Time.deltaTime * bubbleData.GivenDirection * timeSpeedFactor;
        transform.position += movement;
    }

    private void Rotate()
    {
        Vector3 newRotation = new Vector3(0, 0, bubbleData.BubbleRotationSpeed * Time.deltaTime * bubbleData.BubbleOrientation * timeSpeedFactor);
        transform.Rotate(newRotation);
    }

    private IEnumerator SelfDestroyBubble()
    {
        yield return new WaitForSeconds(bubbleData.BubbleLifespan);
        Destroy(gameObject);
    }

    public void HandleImpact (Vector2 impactPosition)
    {
        float distance = Vector2.Distance(transform.position, impactPosition);
        if (distance <= radio)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}