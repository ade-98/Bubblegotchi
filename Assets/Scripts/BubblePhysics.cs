using UnityEngine;

public class BubblePhysics : MonoBehaviour
{
    public float moveForce = 5.0f;
    public float randomForceMaxInterval = 1.0f;
    public float buoyancyStrength = 15f;
    public float dragFactor = 0.05f;
    public float gravityScale = 0.2f;

    private Rigidbody2D bubbleRb;
    private float timeSinceLastMove;
    private Vector2 currentRandomDirection;

    void Start()
    {
        bubbleRb = GetComponent<Rigidbody2D>();
        bubbleRb.gravityScale = gravityScale;
        bubbleRb.mass = 0.1f;
        bubbleRb.linearVelocity = Vector2.zero;
        timeSinceLastMove = 0.0f;
        currentRandomDirection = GetRandomDirection();
    }

    void FixedUpdate()
    {
        timeSinceLastMove += Time.fixedDeltaTime;

        // Checks if time since bubble has changed directions is greater than the max interval value
        if (timeSinceLastMove >= randomForceMaxInterval)
        {
            currentRandomDirection = GetRandomDirection();
            timeSinceLastMove = 0.0f;
        }

        ApplySmoothMovement();
        ApplyBuoyancy();
        ApplyDrag();
    }

    // Gets a random direction for the bubble to move
    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void ApplySmoothMovement()
    {
        // Adds force based on the random direction it is given
        bubbleRb.AddForce(currentRandomDirection * moveForce, ForceMode2D.Force);
    }

    // Adds force from the fan
    public void ApplyFanForce(Vector2 fanDirection, float forceToApply)
    {
        bubbleRb.AddForce(fanDirection * forceToApply, ForceMode2D.Force);

        // The direction the bubble is wandering in is overwritten by the direction the fan pushed it in
        currentRandomDirection = fanDirection;
        timeSinceLastMove = Random.Range(-1.0f, -5.0f);
    }

    // Will float up naturally but it can still move in other directions so it can wander around
    private void ApplyBuoyancy()
    {
        Vector2 buoyantForce = Vector2.up * buoyancyStrength * bubbleRb.mass;
        bubbleRb.AddForce(buoyantForce);
    }

    private void ApplyDrag()
    {
        if (bubbleRb.linearVelocity.sqrMagnitude > 0.1f)
        {
            Vector2 dragForce = -bubbleRb.linearVelocity.normalized * dragFactor * bubbleRb.linearVelocity.sqrMagnitude;
            bubbleRb.AddForce(dragForce);
        }
    }
}
