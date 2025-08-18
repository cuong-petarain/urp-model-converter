using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        // something 1
        // something 2
    }
}
