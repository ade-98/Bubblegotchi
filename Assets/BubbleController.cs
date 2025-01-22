using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleController : MonoBehaviour
{
    private Camera cameraToCastFrom;

    public Rigidbody2D myRigidBody;
    public float verticalPushOnClick = 2;
    public float horizontalPushOnClick = 5;

    private bool leftClickPressed = false;
    private bool rightClickPressed = false;

    void Start()
    {
        cameraToCastFrom = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        leftClickPressed = Input.GetMouseButtonDown(0);
        rightClickPressed = Input.GetMouseButtonDown(1);

        if (leftClickPressed)
        {
            Vector2 mousePosition = cameraToCastFrom.ScreenToWorldPoint(Input.mousePosition);

            // Perform a raycast at the mouse position.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject gameObject = hit.collider.gameObject;

                Vector2 objectPosV2 = new(gameObject.transform.position.x, 0);
                Vector2 mousePosV2 = new(mousePosition.x, 0);

                Vector2 xDirection = objectPosV2 - mousePosV2; // Get difference of x between bubble position and mouse click.

                myRigidBody.linearVelocity = new Vector2(xDirection.x * horizontalPushOnClick, 0);
                //Debug.Log($"Moving in direction: {myRigidBody.linearVelocity}");
            }
        }

        if (rightClickPressed)
        {
            myRigidBody.linearVelocity += new Vector2(0, verticalPushOnClick);
            //Debug.Log("Right-clicked");
        }


    }
}
