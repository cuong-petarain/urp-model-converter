using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [Tooltip("Material with the dissolve shader")]
    public Material dissolveMaterial;
    
    [Tooltip("Time in seconds for the complete dissolve effect")]
    public float dissolveTime = 2.0f;
    
    [Tooltip("Delay before starting the dissolve effect")]
    public float startDelay = 0.0f;
    
    [Tooltip("Whether to automatically start the dissolve effect")]
    public bool autoStart = true;
    
    [Tooltip("Whether to loop the dissolve effect")]
    public bool loop = false;
    
    private float dissolveAmount = 0.0f;
    private float timer = 0.0f;
    private bool isDissolving = false;
    private Material instanceMaterial;
    private Renderer objectRenderer;
    
    // Property ID for faster property access
    private static readonly int DissolveAmountID = Shader.PropertyToID("_DissolveAmount");
    
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        
        // Create an instance of the material to avoid affecting other objects
        if (dissolveMaterial != null)
        {
            instanceMaterial = new Material(dissolveMaterial);
            objectRenderer.material = instanceMaterial;
        }
        else if (objectRenderer.material.HasProperty(DissolveAmountID))
        {
            // Use the existing material if it has the dissolve property
            instanceMaterial = objectRenderer.material;
        }
        else
        {
            Debug.LogError("No dissolve material assigned and the current material doesn't support dissolve!");
            enabled = false;
            return;
        }
        
        // Initialize dissolve amount
        dissolveAmount = 0.0f;
        instanceMaterial.SetFloat(DissolveAmountID, dissolveAmount);
        
        // Start dissolve effect after delay if autoStart is enabled
        if (autoStart)
        {
            Invoke("StartDissolve", startDelay);
        }
    }
    
    void Update()
    {
        if (isDissolving)
        {
            // Increment timer
            timer += Time.deltaTime;
            
            // Calculate dissolve amount based on timer
            dissolveAmount = Mathf.Clamp01(timer / dissolveTime);
            
            // Update material
            instanceMaterial.SetFloat(DissolveAmountID, dissolveAmount);
            
            // Check if dissolve is complete
            if (dissolveAmount >= 1.0f)
            {
                if (loop)
                {
                    // Reset for looping
                    timer = 0.0f;
                    dissolveAmount = 0.0f;
                    instanceMaterial.SetFloat(DissolveAmountID, dissolveAmount);
                }
                else
                {
                    isDissolving = false;
                }
            }
        }
    }
    
    /// <summary>
    /// Start the dissolve effect
    /// </summary>
    public void StartDissolve()
    {
        timer = 0.0f;
        dissolveAmount = 0.0f;
        isDissolving = true;
    }
    
    /// <summary>
    /// Reset the dissolve effect
    /// </summary>
    public void ResetDissolve()
    {
        timer = 0.0f;
        dissolveAmount = 0.0f;
        isDissolving = false;
        instanceMaterial.SetFloat(DissolveAmountID, dissolveAmount);
    }
    
    /// <summary>
    /// Set the dissolve amount directly (0-1)
    /// </summary>
    public void SetDissolveAmount(float amount)
    {
        dissolveAmount = Mathf.Clamp01(amount);
        instanceMaterial.SetFloat(DissolveAmountID, dissolveAmount);
    }
    
    void OnDestroy()
    {
        // Clean up the instance material to prevent memory leaks
        if (instanceMaterial != null && instanceMaterial != dissolveMaterial)
        {
            Destroy(instanceMaterial);
        }
    }
}