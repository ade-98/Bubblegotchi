using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform bubble;
    [SerializeField] private float fanForce = 0.5f;
    [SerializeField] private float maxDistanceToBubble = 5.0f;

    private BubblePhysics bubblePhysics;

    private Vector2 direction;

    void Start()
    {
        bubblePhysics = bubble.GetComponent<BubblePhysics>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePos.x, mousePos.y);

        Cursor.visible = false;

        LookAtBubble();
        AddForceToBubble();
    }

    // Fan will always be looking in the direction of the bubble
    private void LookAtBubble()
    {
        direction = bubble.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // On left click the fan will push the bubble in the direction it is facing
    private void AddForceToBubble()
    {
        bool leftClickDown = Input.GetMouseButton(0);

        if (leftClickDown && GetDistanceToBubble() < maxDistanceToBubble)
        {
            float inverseDistance = 1 / GetDistanceToBubble();

            Vector2 fanDirection = direction.normalized;

            // Calculation so that more force is applied when the fan is closer to the bubble
            float forceToApply = fanForce * inverseDistance;

            bubblePhysics.ApplyFanForce(fanDirection, forceToApply);
        }
    }

    private float GetDistanceToBubble()
    {
        Vector2 fanPos = transform.position;
        Vector2 bubblePos = bubble.position;

        return Vector2.Distance(bubblePos, fanPos);
    }
}
