using UnityEngine;

/// <summary>
/// Applies a constant custom gravity force to a Rigidbody.
/// This allows for per-object gravity modifications without changing global physics settings.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("The multiplier for the gravity force. 1 is normal gravity, >1 is stronger, <1 is weaker, 0 is no gravity, and negative values reverse gravity.")]
    public float gravityScale = 1.0f;

    // We use a Rigidbody for all physics operations.
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravityForce = Physics.gravity * gravityScale;
        rb.AddForce(gravityForce, ForceMode.Acceleration);
    }
}
