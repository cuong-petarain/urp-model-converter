using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TouchPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    [Tooltip("Controls how smoothly the player accelerates and decelerates.")]
    public float lerpSmoothness = 5f;

    [Header("Dash")]
    public float dashSpeed = 10f;
    [Tooltip("The time in seconds it takes to recover from a dash.")]
    public float dashRecoverDuration = 0.2f;

    // Private variables to manage state
    private Rigidbody rb;
    private Vector2 touchPosition;
    private bool isTouching;
    private float dashTimer;

    private void Awake()
    {
        // Get the Rigidbody component once at the start
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // This method now handles all input detection every frame
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        // Check if there is at least one finger touching the screen
        if (Input.touchCount > 0)
        {
            isTouching = true;
            Touch touch = Input.GetTouch(0); // Get the first touch
            touchPosition = touch.position;  // Store its screen position

            // Check if the touch just began and it's the second tap (a double tap)
            if (touch.phase == TouchPhase.Began && touch.tapCount == 2)
            {
                TriggerDash();
            }
        }
        else
        {
            // If there are no fingers on the screen, we are not touching
            isTouching = false;
        }
    }

    private void TriggerDash()
    {
        // Starts the dash timer when a double tap is detected
        dashTimer = dashRecoverDuration;
    }

    private void FixedUpdate()
    {
        // --- THIS PHYSICS LOGIC REMAINS THE SAME ---

        // Determine the current target speed (dash or normal)
        float currentSpeed = (dashTimer > 0) ? dashSpeed : moveSpeed;

        // Decrease the dash timer if it's active
        if (dashTimer > 0)
        {
            dashTimer -= Time.fixedDeltaTime;
        }

        // Default to a zero velocity to smoothly slow down
        Vector3 targetVelocity = Vector3.zero;

        // If a touch is active, calculate the target velocity
        if (isTouching)
        {
            if (touchPosition.x < Screen.width / 2)
            {
                targetVelocity = Vector3.left * currentSpeed;
            }
            else
            {
                targetVelocity = Vector3.right * currentSpeed;
            }
        }

        // Smoothly transition between current and target velocity
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * lerpSmoothness);
    }
}